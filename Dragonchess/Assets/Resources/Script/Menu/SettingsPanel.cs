using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingsPanel : MonoBehaviour {
	Settings settings;
	public GameObject mainMenuPanel;
	public Text saveAngleAndPositionText;
	public Toggle saveAngleAndPositionToggle;
	public Text rotateCameraOnPlayerChangeText;
	public Toggle rotateCameraOnPlayerChangeToggle;
	public Text volumeText;
	public Slider volumeSlider;
	public Text toMainMenuText;

	void Start () {
		settings = Settings.Instance;
		saveAngleAndPositionText.text = Texts.GetString ("SaveAngleAndPositionText");
		saveAngleAndPositionToggle.isOn = settings.SaveAngleAndPositionOnBoardChange;
		rotateCameraOnPlayerChangeText.text = Texts.GetString ("RotateCameraOnPlayerChangeText");
		rotateCameraOnPlayerChangeToggle.isOn = settings.RotateCameraOnChangePlayer;
		volumeText.text = Texts.GetString ("VolumeText");
		volumeSlider.normalizedValue = settings.SoundVolume;
		toMainMenuText.text = Texts.GetString ("MainMenuButtonText");
	}

	public void ChangeSaveAngleAndPosition () {
		settings.SaveAngleAndPositionOnBoardChange = saveAngleAndPositionToggle.isOn;
	}

	public void ChangeRotateCameraOnPlayerChange () {
		settings.RotateCameraOnChangePlayer = rotateCameraOnPlayerChangeToggle.isOn;
	}

	public void ChangeVolume () {
		settings.SoundVolume = volumeSlider.normalizedValue;
	}

	public void GoToMainMenu () {
		gameObject.SetActive (false);
		mainMenuPanel.SetActive (true);
	}
}
