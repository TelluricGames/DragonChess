using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class OverlayManager : MonoBehaviour {

	enum OverlayState {
		CLOSED,
		OPENED_PIECE_LIST,
		OPENED_LOG
	}

	public static OverlayManager Instance = null;
	public Button returnToMenuButton;
	public Button logButton;
	public Button showCapturedPiecesButton;
	public Button goUPButton;
	public Button goDownButton;
	public Canvas infoFrame;
	public Canvas logFrame;
	public Canvas pieceListFrame;
	
	GameObject _camera;


	private OverlayState state = OverlayState.CLOSED;

	void Awake() {
		if (Instance != null)
			throw new DragonChessException ("Overlay manager was already instanciated");
		Instance = this;

		returnToMenuButton.onClick.AddListener(ReturnToMenu);
		var camera = MainGameCamera.Instance;
		_camera = camera.gameObject;
		goUPButton.onClick.AddListener (camera.GoUp);
		goDownButton.onClick.AddListener (camera.GoDown);
		showCapturedPiecesButton.onClick.AddListener (ShowCapturedGuys);
		logButton.onClick.AddListener (ShowLog);
	}

	public void SetLabel(string text, UnityEngine.Color c) {
		var textElem = infoFrame.GetComponentInChildren<Text> ();
		textElem.text = text;
		textElem.color = c;

		returnToMenuButton.GetComponentInChildren<Text> ().text = Texts.GetString ("GoToMenuButtonText");
		logButton.GetComponentInChildren<Text> ().text = Texts.GetString ("LogButtonText");
		showCapturedPiecesButton.GetComponentInChildren<Text> ().text = Texts.GetString ("CapturedPiecesButtonText");
		goUPButton.GetComponentInChildren<Text> ().text = Texts.GetString ("BoardUpButtonText");
		goDownButton.GetComponentInChildren<Text> ().text = Texts.GetString ("BoardDownButtonText");
		logFrame.GetComponent<LogListGenerator> ().Init (GameManager.Instance.Logger.GetLog ());
	}


	void ShowLog() {
		if (state == OverlayState.OPENED_LOG) {
			state = OverlayState.CLOSED;
			logFrame.enabled = false;
		} else if (state == OverlayState.CLOSED) {
			state = OverlayState.OPENED_LOG;
			logFrame.enabled = true;
		} else {
			ShowCapturedGuys ();
			ShowLog ();
		}
	}

	void ShowCapturedGuys() {
		if (state == OverlayState.OPENED_PIECE_LIST) {
			state = OverlayState.CLOSED;
			pieceListFrame.enabled = false;
		} else if(state == OverlayState.CLOSED){
			state = OverlayState.OPENED_PIECE_LIST;
			pieceListFrame.enabled = true;
		}else {
			ShowLog ();
			ShowCapturedGuys ();
		}
	}

	public void ClearLogs() {

		logFrame.GetComponent<LogListGenerator> ().Clear ();
		pieceListFrame.GetComponent<PieceListGenerator> ().Clear();
	}

	void ReturnToMenu() {
		if (SceneManager.GetSceneByName("Menu").IsValid()) {
			GameManager.Instance.Pause ();
			gameObject.SetActive (false);
			_camera.SetActive (false);
			foreach (GameObject go in SceneManager.GetSceneByName("menu").GetRootGameObjects()) {
				if (go.tag == "MenuContainer") {
					go.SetActive (true);
				}
			}
		} else {
			throw new DragonChessException ("There's no menu");
		}
	}

	/*
	public void SetLog(ObservableCollection<string> logs) {
		//logFrame.GetComponent<LogListGenerator> ().GenerateList (logs);
	}
	*/

	public void SetCapturedList(List<PieceLog> logs) {
		pieceListFrame.GetComponent<PieceListGenerator> ().GenerateList (logs);
		
	}
}
