using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine;

public class ButtonManager : MonoBehaviour {

	//public GameObject resumeBtnContainer;
	public Button startButton;
	public Button resumeButton;
	public Button settingsButton;
	public Button exitButton;
	public GameObject settingsPanel;

	void Start()
	{
		startButton.GetComponentInChildren<Text>().text = Texts.GetString ("StartGameButtonText");
		startButton.onClick.AddListener (StartGame);

		resumeButton.GetComponentInChildren<Text>().text = Texts.GetString ("ResumeGameButtonText");
		resumeButton.onClick.AddListener (ResumeGame);
		resumeButton.interactable = false;

		settingsButton.GetComponentInChildren<Text>().text = Texts.GetString ("SettingsButtonText");
		settingsButton.onClick.AddListener (ShowSettings);

		exitButton.GetComponentInChildren<Text>().text = Texts.GetString ("ExitButtonText");
		exitButton.onClick.AddListener (Exit);
	}

	void Exit() {
		//print("Exit was pressed"); 
		#if UNITY_EDITOR
		UnityEditor.EditorApplication.isPlaying = false;
		#else
		Application.Quit();
		#endif
	}

	void ResumeGame()
	{
		if (SceneManager.GetSceneByName("dragonchess").IsValid()) {
			gameObject.SetActive (false);
			foreach (GameObject go in SceneManager.GetSceneByName("dragonchess").GetRootGameObjects()) {
				if (go.name == "GUIOverlay") {
					MonoBehaviour.print ("I found it!");
					go.SetActive (true);
				}
				if (go.name == "Main Camera") {
					MonoBehaviour.print ("I found camera!");
					go.SetActive (true);
				}
			}
		}
	}

	void StartGame()
	{
		if (!SceneManager.GetSceneByName("dragonchess").isLoaded) {
			print ("Loading scene");
			resumeButton.interactable = true;
			//resumeBtnContainer.SetActive (true);
			gameObject.SetActive (false);
			SceneManager.LoadScene ("dragonchess", LoadSceneMode.Additive);

		} else {
			gameObject.SetActive (false);
			ResumeGame ();
			foreach (GameObject go in SceneManager.GetSceneByName("dragonchess").GetRootGameObjects()) {
				if (go.name == "GameManager") {
					MonoBehaviour.print ("I found gamemanager!");
					go.GetComponent<GameManager> ().StartNewGame ();
				}
			}
		}
	}

	void ShowSettings () {
		gameObject.SetActive (false);
		settingsPanel.SetActive (true);
	}

	void Update() {
	}
}