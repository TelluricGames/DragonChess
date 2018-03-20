using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Settings {

	static Settings instance = null;
	public static Settings Instance {
		get {
			if (instance == null)
				instance = new Settings ();
			return instance;
		}
	}

	public bool SaveAngleAndPositionOnBoardChange { get; set; }
	public bool RotateCameraOnChangePlayer { get; set; }
	float soundVolume = 1f;
	public float SoundVolume {
		get { return soundVolume; }
		set {
			if (value < 0f)
				soundVolume = 0f;
			else if (value > 1f)
				soundVolume = 1f;
			else
				soundVolume = value;
		}
	}

	private Settings () {
		SaveAngleAndPositionOnBoardChange = false;
		RotateCameraOnChangePlayer = false;
		SoundVolume = 1f;
	}


}
