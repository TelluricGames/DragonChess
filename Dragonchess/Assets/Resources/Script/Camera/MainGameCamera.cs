using System;
using UnityEngine;

enum CameraState
{
	FIRST_MOVE, MOVING, IDLE, DISABLED
}

public class MainGameCamera : MonoBehaviour
{
	public static MainGameCamera Instance { get; private set; } = null;
	public Transform target;
	public float movementSpeed;
	public float rotationSpeed;
	public float scaleSpeed;
	public Transform [] boards;
	bool SaveAngleAndPosition {
		get { return Settings.Instance.SaveAngleAndPositionOnBoardChange; }
	}
	bool RotateOnChangePlayer {
		get { return Settings.Instance.RotateCameraOnChangePlayer; }
	}
	Vector3 dest;

	GameManager _gameManager;
	CameraState _state;
	CameraState State {
		get { return _state; }
		set {
			switch (value) {
				case CameraState.FIRST_MOVE: {
						_gameManager.IsAnimated = true;
						dest = target.position + ((_activePlayer == Color.WHITE) ? new Vector3 (0, 7, -5) : new Vector3 (0, 7, 5));

						break;
					}
				case CameraState.MOVING: {
						_gameManager.IsAnimated = true;

						Transform tr = GetComponent<Transform> ();
						if (SaveAngleAndPosition) {
							float ver = -100f;
							if (tr.position.y <= 0)
								ver = tr.position.y + 7f;
							else if (tr.position.y <= 10)
								ver = tr.position.y - 3f;
							else
								ver = tr.position.y - 13f;
							//dest = new Vector3 (tr.position.x, target.position.y + 3f, tr.position.z);
							dest = new Vector3 (tr.position.x, target.position.y + ver + 3f, tr.position.z);
						} else {
							dest = target.position + ((_activePlayer == Color.WHITE) ? new Vector3 (0, 7, -5) : new Vector3 (0, 7, 5));
						}

						break;
					}
				case CameraState.DISABLED:
					break;
				default: {
						_gameManager.IsAnimated = false;
						break;
					}
			}

			_state = value;
		}
	}
	Color _activePlayer;

	void Awake () {
		if (Instance != null)
			throw new DragonChessException ("Game manager awakened, but it's Instance is already set");

		Instance = this;
	}

	void Start () {
		if (target == null)
			State = CameraState.DISABLED;

		if (Input.touchSupported)
			gameObject.AddComponent<CameraTouchInputController> ();

		gameObject.AddComponent<CameraKeyboardInputController> ();
	}

	public void Init (Transform [] newBoards, int initBoard = 0)
	{
		_gameManager = GameObject.FindGameObjectWithTag ("GameController").GetComponent<GameManager> ();
		_activePlayer = _gameManager.ActivePlayerColor;

		boards = newBoards;

		if (initBoard > 0 && initBoard < boards.Length) {
			target = boards [initBoard];
		} else {
			target = boards [0];
		}

		State = CameraState.FIRST_MOVE;
	}

	void Update ()
	{
		switch (State) {
			case CameraState.FIRST_MOVE: {
					Transform tr = GetComponent<Transform> ();

					tr.position = Vector3.Lerp (tr.position, dest, 0.05f * 60f * Time.deltaTime);
					if (target.position.y < tr.position.y)
						tr.rotation = Quaternion.Slerp (tr.rotation, Quaternion.LookRotation (target.position - tr.position), 0.2f);

					if (Vector3.Distance (tr.position, dest) < .05f) {
						tr.position = dest;
						tr.LookAt (target);
						_gameManager.IsAnimated = false;
						State = CameraState.IDLE;
					}

					break;
				}
			case CameraState.MOVING: {
					Transform tr = GetComponent<Transform> ();

					tr.position = Vector3.Lerp (tr.position, dest, 0.05f * 60f * Time.deltaTime);
					if (!SaveAngleAndPosition && target.position.y < tr.position.y)
						tr.rotation = Quaternion.Slerp (tr.rotation, Quaternion.LookRotation (target.position - tr.position), 0.2f);

					if (Vector3.Distance (tr.position, dest) < .05f) {
						tr.position = dest;
						if (!SaveAngleAndPosition)
							tr.LookAt (target);
						_gameManager.IsAnimated = false;
						State = CameraState.IDLE;
					}

					break;
				}
		}

		if (RotateOnChangePlayer && _activePlayer != _gameManager.ActivePlayerColor && _state != CameraState.DISABLED) {
			_activePlayer = _gameManager.ActivePlayerColor;
			State = CameraState.MOVING;
		}
	}

	public void GoToBoard (int board)
	{
		if (board < 0 || board >= boards.Length)
			//return;
			throw new IndexOutOfRangeException ("Board number is out of bounds");

		target = boards [board];
		State = CameraState.MOVING;
	}

	public void GoUp ()
	{
		for (int i = 0; i < boards.Length; i++) {
			if (target == boards [i]) {
				if (i != boards.Length - 1) {
					target = boards [i + 1];
					State = CameraState.MOVING;
				}

				break;
			}
		}
	}

	public void GoDown ()
	{
		for (int i = 0; i < boards.Length; i++) {
			if (target == boards [i]) {
				if (i != 0) {
					target = boards [i - 1];
					State = CameraState.MOVING;
				}

				break;
			}
		}
	}

	public void Disable ()
	{
		State = CameraState.DISABLED;
	}

	public void Move (Vector2 pos)
	{
		pos *= 30f * Time.deltaTime;
		if (State == CameraState.IDLE) {
			var tr = GetComponent<Transform> ();
			tr.Rotate (new Vector3 (-60, 0));
			tr.Translate (new Vector3 (pos.x, 0, pos.y), Space.Self);
			tr.Rotate (new Vector3 (60, 0));

			CheckBoundaries ();
		}
	}

	public void Zoom (float scale)
	{
		scale *= 30f * Time.deltaTime;
		if (State == CameraState.IDLE) {
			var tr = GetComponent<Transform> ();
			if (scale > 0f) {
				if (tr.position.y > 3f + target.position.y) {
					tr.Translate (Vector3.forward * scale);
				}
			} else {
				if (tr.position.y < 10f + target.position.y) {
					tr.Translate (Vector3.forward * scale);
				}
			}

			CheckBoundaries ();
		}
	}

	public void Rotate (float degree)
	{
		degree *= 30f * Time.deltaTime;
		if (State == CameraState.IDLE) {
			var tr = GetComponent<Transform> ();
			tr.RotateAround (target.position, target.up, degree);

			CheckBoundaries ();
		}
	}

	public void Rotate (bool clockwise)
	{
		if (State == CameraState.IDLE) {
			var tr = GetComponent<Transform> ();
			tr.RotateAround (target.position, target.up, clockwise ? rotationSpeed : -rotationSpeed);

			CheckBoundaries ();
		}
	}

	public void ResetPosition ()
	{
		State = CameraState.MOVING;
	}

	void CheckBoundaries ()
	{
		var tr = GetComponent<Transform> ();
		float x = tr.position.x;
		float y = tr.position.y;
		float z = tr.position.z;

		if (x < target.position.x - 10f)
			x = target.position.x - 10f;
		else if (x > target.position.x + 10f)
			x = target.position.x + 10f;

		if (y < target.position.y + 3f)
			y = target.position.y + 3f;
		else if (y > target.position.y + 10f)
			y = target.position.y + 10f;

		if (z < target.position.z - 10f)
			z = target.position.z - 10f;
		else if (z > target.position.z + 10f)
			z = target.position.z + 10f;

		tr.position = new Vector3 (x, y, z);
	}
}
