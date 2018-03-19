using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Initializer : MonoBehaviour {
	AudioSource AudioSource;
	void Awake () {
		Application.targetFrameRate = 60;
        DontDestroyOnLoad(this);

		#if UNITY_STANDALONE || UNITY_EDITOR
		Application.runInBackground = true;
		#else
		Screen.autorotateToLandscapeLeft = true;
		Screen.autorotateToLandscapeRight = true;
		Screen.autorotateToPortrait = false;
		Screen.autorotateToPortraitUpsideDown = false;
		Screen.orientation = ScreenOrientation.LandscapeLeft;
		#endif
	}

	void Start() {
		AudioSource = GetComponent<AudioSource>();
	}

	void Update() {
		AudioSource.volume = Settings.Instance.SoundVolume;
    }
}
