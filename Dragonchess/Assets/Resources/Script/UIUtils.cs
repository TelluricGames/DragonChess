using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIUtils : MonoBehaviour {
	List<UIElement> uiElements = null;

	void OnEnable () {
		SceneManager.sceneLoaded += OnSceneLoaded;
	}

	void OnDisable () {
		SceneManager.sceneLoaded -= OnSceneLoaded;
	}

	public bool IsPointerOverUIElement () {
		if (uiElements == null) {
			FillUIElementsList ();
		}
		//FillUIElementsList ();

		foreach (var elem in uiElements) {
			if (elem.IsPointerOver)
				return true;
		}

		return false;
	}

	void OnSceneLoaded (Scene scene, LoadSceneMode mode) {
		FillUIElementsList ();
	}

	void FillUIElementsList () {
		uiElements = new List<UIElement> ();

		foreach (var elem in GameObject.FindGameObjectsWithTag("UI")) {
			var comp = elem.GetComponent<UIElement> ();

			if (comp != null)
				uiElements.Add (comp);
		}
	}
}
