using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraKeyboardInputController : MonoBehaviour {

	static string[] keyNames = new string[]{ "Board1", "Board2", "Board3" };

	MainGameCamera _cm;
	float movementSpeed { get { return _cm.movementSpeed; }	}
	float rotationSpeed { get { return _cm.rotationSpeed; } }
	float scaleSpeed { get { return _cm.scaleSpeed; } }

	void Start () {
		_cm = GetComponent<MainGameCamera> ();
	}
	
	void Update () {
		float X = Input.GetAxis ("Horizontal") * movementSpeed;
		float Y = Input.GetAxis ("Vertical") * movementSpeed;

		_cm.Move (new Vector2 (X, Y));

		var rotate = Input.GetAxis ("Rotate");
		if (rotate != 0)
			_cm.Rotate (rotate * rotationSpeed);

		var zoom = Input.GetAxis ("Zoom");
		if (zoom != 0) {
			_cm.Zoom (zoom * scaleSpeed);
		}

		if (Input.GetButtonDown ("Reset"))
			_cm.ResetPosition ();

		CheckBoardsChange ();
	}

	void CheckBoardsChange() {
		for (int i = 0; i < keyNames.Length; i++) {
			if (Input.GetButtonDown (keyNames [i])) {
				_cm.GoToBoard (i);
			}
		}
	}
}
