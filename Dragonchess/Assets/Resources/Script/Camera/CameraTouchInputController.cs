using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CameraTouchInputController : MonoBehaviour {

	MainGameCamera _cm;
	UIUtils uiUtils;

	void Awake () {
		SimpleGesture.WhilePinching (ZoomOut);
		SimpleGesture.WhileStretching (ZoomIn);
		SimpleGesture.WhileTwisting (Rotate);
		SimpleGesture.While1FingerPanning (Move);
	}

	void Start () {
		_cm = GetComponent<MainGameCamera> ();
		uiUtils = GameObject.FindGameObjectWithTag ("GameController").GetComponent<UIUtils> ();
	}

	public void Move (GestureInfoPan info) {
		if (!uiUtils.IsPointerOverUIElement())
			_cm.Move (-info.deltaDirection * 0.01f);
	}

	public void Rotate (GestureInfoTwist info) {
		if (!uiUtils.IsPointerOverUIElement())
			_cm.Rotate (info.deltaDistance * 0.1f * (info.clockwise ? -1f : 1f));
	}

	public void ZoomIn (GestureInfoZoom info) {
		if (!uiUtils.IsPointerOverUIElement())
			Zoom (info.deltaDistance * 0.02f);
	}

	public void ZoomOut (GestureInfoZoom info) {
		if (!uiUtils.IsPointerOverUIElement())
			Zoom (-info.deltaDistance * 0.02f);
	}

	void Zoom (float scale) {
		if (!uiUtils.IsPointerOverUIElement())
			_cm.Zoom (scale);
	}
}
