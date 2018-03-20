using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine;

public class MainMenuPanel : MonoBehaviour {

	public MenuManager menuManager;
	public Button startButton;
	public Button resumeButton;
	public Button settingsButton;
	public Button exitButton;
	public GameObject settingsPanel;
	public GameObject playersChoicePanel;

	void Start()
	{
		startButton.GetComponentInChildren<Text>().text = Texts.GetString ("StartGameButtonText");
		startButton.onClick.AddListener (ShowPlayersChoicePanel);

		resumeButton.GetComponentInChildren<Text>().text = Texts.GetString ("ResumeGameButtonText");
		resumeButton.onClick.AddListener (ResumeGame);
		resumeButton.interactable = false;

		settingsButton.GetComponentInChildren<Text>().text = Texts.GetString ("SettingsButtonText");
		settingsButton.onClick.AddListener (ShowSettings);

		exitButton.GetComponentInChildren<Text>().text = Texts.GetString ("ExitButtonText");
		exitButton.onClick.AddListener (Exit);
	}

	void Exit() {
		#if UNITY_EDITOR
		UnityEditor.EditorApplication.isPlaying = false;
		#else
		Application.Quit();
		#endif
	}

	void ResumeGame()
	{
		if (SceneManager.GetSceneByName("dragonchess").IsValid()) {
			menuManager.Disable ();
			var gm = GameManager.Instance;
			//gm.gameObject.SetActive (true);
			MainGameCamera.Instance.gameObject.SetActive (true);
			OverlayManager.Instance.gameObject.SetActive (true);
			gm.Resume ();
		}
	}

	void OnEnable () {
		if (SceneManager.GetSceneByName("dragonchess").isLoaded) {
			resumeButton.interactable = true;
		} else {
			resumeButton.interactable = false;
		}
	}

	void ShowPlayersChoicePanel() {
		gameObject.SetActive (false);
		playersChoicePanel.SetActive (true);
	}

	void ShowSettings () {
		gameObject.SetActive (false);
		settingsPanel.SetActive (true);
	}
}