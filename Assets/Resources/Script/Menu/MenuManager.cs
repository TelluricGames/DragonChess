using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour {

	public GameObject mainMenuPanel;
	public GameObject playersChoicePanel;
	public GameObject settingsPanel;
	public GameObject menuCamera;

	void Start () {
		Init ();
	}

	void Init () {
		mainMenuPanel.SetActive (true);
		playersChoicePanel.SetActive (false);
		settingsPanel.SetActive (false);
		menuCamera.SetActive (true);
	}

	void OnEnable () {
		Init ();
	}

	public void Disable() {
		gameObject.SetActive (false);
	}
}
