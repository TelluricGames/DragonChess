using System.Collections;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Threading;

public enum GameManagerState {
	PLAYING,
	PAUSED,
	FINISHED
	/*
	ANIMATION,
	IDLE,
	PAUSED
	*/
}

public struct PlayerMove {
	public Vector3 start;
	public Vector3 end;
}

public class GameManager : MonoBehaviour {
	static AudioClip chessMoveSound;
	static GameObject boardPrefab;

	GameEngine _gameEngine;
	Board[] _boards;
	OverlayManager _overlayManager;
	MainGameCamera _camera;
    Player[] _players;
	public GameManagerState State { get; set; }
	public object StateLock = new object ();
	public static GameManager Instance { get; private set; } = null;
	public Player ActivePlayer {
		get {
			foreach (var player in _players) {
				if (player.Color == ActivePlayerColor)
					return player;
			}

			return null;
		}
	}
	public Color ActivePlayerColor {
		get { return _gameEngine.ActivePlayer; }
	}
    public Logger Logger { get; private set; }
    public bool IsAnimated { get; set; }

    void Awake () {
		if (Instance != null)
			throw new DragonChessException("Game manager awakened, but it's Instance is already set");

		Instance = this;

		Application.targetFrameRate = 60;

		chessMoveSound = Resources.Load ("Sounds/ChessMoveSound") as AudioClip;
		boardPrefab = Resources.Load ("Prefabs/Board") as GameObject;
		_overlayManager = GameObject.FindGameObjectWithTag ("GUI").GetComponent<OverlayManager> ();
	}

	public void StartNewGame (bool IsWhiteHuman, bool IsBlackHuman) {
		Player playerOne;
		if (IsWhiteHuman)
			playerOne = gameObject.AddComponent<HumanPlayer> ();
		else 
			playerOne = gameObject.AddComponent<RandomAIPlayer> ();

		Player playerTwo;
		if (IsBlackHuman)
			playerTwo = gameObject.AddComponent<HumanPlayer> ();
		else
			playerTwo = gameObject.AddComponent<RandomAIPlayer> ();

		_players = new Player[] { playerOne, playerTwo };

		StartNewGame ();
	}

	public void StartNewGame () {
		StopAllCoroutines ();
		var camera = GameObject.FindGameObjectWithTag ("MainCamera").GetComponent<MainGameCamera> ();
		camera.Disable ();
		if (_boards != null) {
			DestroyBoards ();
			DestroyPieces ();
		}
		_boards = new Board[3];
		_gameEngine = new GameEngine (_boards);
		State = GameManagerState.PLAYING;
		Logger = new Logger ();
		CreateBoards ();
		InitBoards ();
		_overlayManager.ClearLogs ();
		_overlayManager.SetLabel (Texts.GetString("WhiteTurnText"), UnityEngine.Color.white);
		_players[0].Init (Color.WHITE, _boards, this);
		_players[1].Init (Color.BLACK, _boards, this);

		//StartCoroutine (Play());
		Play ();
	}

	void CreateBoards() {
		GameObject middleBoardObj = GameObject.Instantiate (boardPrefab, new Vector3 (0, 0, 0), Quaternion.identity);
		GameObject upperBoardObj = GameObject.Instantiate (boardPrefab, new Vector3 (0, 10, 0), Quaternion.identity);
		GameObject lowerBoardObj = GameObject.Instantiate (boardPrefab, new Vector3 (0, -10, 0), Quaternion.identity);

		SetUpCamera (lowerBoardObj, middleBoardObj, upperBoardObj);

		_boards[0] = lowerBoardObj.GetComponent<Board> ();
		_boards[1] = middleBoardObj.GetComponent<Board> ();
		_boards[2] = upperBoardObj.GetComponent<Board> ();
	}

	void InitBoards () {
		for (int i = 0; i < _boards.Length; i++) {
			_boards [i].Init (i);
		}
	}

	void DestroyBoards () {
		for (int i = 0; i < _boards.Length; i++) {
			_boards [i].Destroy ();
		}
	}

	void DestroyPieces() {
		foreach (var piece in GameObject.FindGameObjectsWithTag("Piece")) {
			GameObject.Destroy (piece.gameObject);
		}
	}

	void SetUpCamera(params GameObject[] boards) {
		var camera = GameObject.FindGameObjectWithTag ("MainCamera");
		List<Transform> boardsTransforms = new List<Transform> ();

		foreach (GameObject board in boards) {
			boardsTransforms.Add (board.GetComponent<Transform> ());
		}

		if (camera != null) {
			var sc = camera.GetComponent<MainGameCamera> ();
			if (sc != null) {
				sc.Init (boardsTransforms.ToArray(), 1);
				_camera = sc;
			}
		}
	}
    
	public void DoTurn (PlayerMove move) {
		StartCoroutine (DoTurnRoutine (move));
	}

	IEnumerator DoTurnRoutine (PlayerMove move) {
		Logger.PrepareMove (move.start, move.end, _boards);

		if (ActivePlayer.PlayerType == PlayerType.AI) {
			_camera.GoToBoard ((int)move.end.z);

			while (IsAnimated)
				yield return null;
		}

		EngineResponse response = _gameEngine.DoTurn (move.start, move.end);

		while (IsAnimated)
			yield return null;

		if (response == EngineResponse.CHECK) {
			_overlayManager.SetLabel (Texts.GetString ("CheckText"), UnityEngine.Color.red);
			Logger.LogMove (true, false);
		} else if (response == EngineResponse.CHECK_MATE) {
			State = GameManagerState.FINISHED;
			_overlayManager.SetLabel (Texts.GetString ("CheckmateText"), UnityEngine.Color.red);
			Logger.LogMove (false, true);
		} else {
			if (ActivePlayerColor == Color.BLACK) {
				_overlayManager.SetLabel (Texts.GetString ("BlackTurnText"), UnityEngine.Color.black);
			} else {
				_overlayManager.SetLabel (Texts.GetString ("WhiteTurnText"), UnityEngine.Color.white);
			}
			Logger.LogMove (false, false);
		}

		while (IsAnimated)
			yield return null;

		_overlayManager.SetCapturedList (Logger.CapturedPieces);

		if (State == GameManagerState.PLAYING) {
			foreach (var player in _players) {
				if (player.Color == ActivePlayerColor) {
					player.DoTurn ();
					break;
				}
			}
		}
	}

	void Play () {
		if (State == GameManagerState.PLAYING && !IsAnimated)
			ActivePlayer.DoTurn ();
	}

	public void Pause () {
		if (State == GameManagerState.PLAYING) {
			State = GameManagerState.PAUSED;
		}
	}

	public void Resume () {
		if (State == GameManagerState.PAUSED) {
			State = GameManagerState.PLAYING;
			Play ();
		}
	}

	private void Update () {
		if (Input.GetKeyDown (KeyCode.Z)) {
			if (State == GameManagerState.PLAYING) {
				Pause ();
			} else if (State == GameManagerState.PAUSED) {
				Resume ();
			}
		}
	}

	public void PlayMoveSound () {
		var audio = GetComponent<AudioSource> ();
		audio.clip = chessMoveSound;
		var settings = Settings.Instance;
		audio.volume = settings.SoundVolume;
		audio.Play ();
	}
}
