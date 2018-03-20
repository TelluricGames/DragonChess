using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayersChoicePanel : MonoBehaviour {
	public MenuManager MenuManager;
	public GameObject MainMenuPanel;

	public Text ChoosePlayersText;
	public Text WhitePlayerText;
	public Text WhiteHumanText;
	public Text WhiteAIText;
	public Text BlackHumanText;
	public Text BlackAIText;
	public Toggle WhiteHumanToggle;
	public Toggle WhiteAIToggle;
	public Text BlackPlayerText;
	public Toggle BlackHumanToggle;
	public Toggle BlackAIToggle;
	public Button StartGameButton;
	public Text StartGameButtonText;
	public Button CancelButton;
	public Text CancelButtonText;

	GameObject _overlayManager = null;
	GameObject _gameManager = null;
	GameObject _camera = null;

	void Start () {
		ChoosePlayersText.text = Texts.GetString ("ChoosePlayersText");
		WhitePlayerText.text = Texts.GetString ("WhitePlayerText");
		BlackPlayerText.text = Texts.GetString ("BlackPlayerText");
		var HumanText = Texts.GetString ("HumanText");
		WhiteHumanText.text = HumanText;
		BlackHumanText.text = HumanText;
		var AIText = Texts.GetString ("AIText");
		WhiteAIText.text = AIText;
		BlackAIText.text = AIText;

		WhiteHumanToggle.isOn = true;
		WhiteAIToggle.isOn = false;
		BlackHumanToggle.isOn = true;
		BlackAIToggle.isOn = false;

		StartGameButtonText.text = Texts.GetString ("StartGameButtonText");
		CancelButtonText.text = Texts.GetString ("CancelButtonText");
	}

	public void WhiteHumanToggleValueChanged () {
		var currentValue = WhiteHumanToggle.isOn;
		WhiteAIToggle.isOn = !currentValue;
	}

	public void WhiteAIToggleValueChanged () {
		var currentValue = WhiteAIToggle.isOn;
		WhiteHumanToggle.isOn = !currentValue;
	}

	public void BlackHumanToggleValueChanged () {
		var currentValue = BlackHumanToggle.isOn;
		BlackAIToggle.isOn = !currentValue;
	}

	public void BlackAIToggleValueChanged () {
		var currentValue = BlackAIToggle.isOn;
		BlackHumanToggle.isOn = !currentValue;
	}

	public void StartGame () {
		bool IsWhiteHuman = WhiteHumanToggle.isOn;
		bool IsBlackHuman = BlackHumanToggle.isOn;

		var scene = SceneManager.GetSceneByName ("dragonchess");
		if (scene.isLoaded) {
			print ("Pressed start");
			_overlayManager.SetActive (true);
			_camera.SetActive (true);
			_gameManager.SetActive (true);
			var gm = _gameManager.GetComponent<GameManager> ();
			gm.StopAllCoroutines ();
			gm.StartNewGame (IsWhiteHuman, IsBlackHuman);
			print ("Disabling menu");
			MenuManager.Disable ();
		} else {
			StartCoroutine (LoadScene (IsWhiteHuman, IsBlackHuman));
		}
	}

	IEnumerator LoadScene (bool IsWhiteHuman, bool IsBlackHuman) {
		var ao = SceneManager.LoadSceneAsync ("dragonchess", LoadSceneMode.Additive);
		yield return ao;

		var gm = GameManager.Instance;
		_gameManager = gm.gameObject;
		gm.StartNewGame (IsWhiteHuman, IsBlackHuman);

		var camera = MainGameCamera.Instance;
		_camera = camera.gameObject;		

		foreach (GameObject go in SceneManager.GetSceneByName("dragonchess").GetRootGameObjects()) {
			if (go.name == "GUIOverlay") {
				_overlayManager = go;
			}
		}

		MenuManager.Disable ();
	}

	public void GoBack () {
		gameObject.SetActive (false);
		MainMenuPanel.SetActive (true);
	}
}
