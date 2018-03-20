using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using UnityEngine;
using UnityEngine.UI;

public class LogListGenerator : MonoBehaviour {

	public Transform logsGrid;
	List<GameObject> createdLogs = new List<GameObject> ();
	static GameObject listElementPrefab;
	ObservableCollection<string> _log;

	void Start () {
		listElementPrefab = Resources.Load ("Prefabs/LogElement") as GameObject;
	}

	public void Init (ObservableCollection<string> log) {
		_log = log;
		log.CollectionChanged += LogChanged;
	}

	public void Clear() {
		foreach (var logEl in createdLogs) {
			Destroy (logEl.gameObject);
		}
		createdLogs.Clear ();
	}

	void LogChanged (object sender,	NotifyCollectionChangedEventArgs e) {
		if (e.Action != NotifyCollectionChangedAction.Add) {
			print ("Added to collection");
		}

		/*
		if (_log.Count == createdLogs.Count)
			return;
		*/

		//Add more elements to the list of GameObjects
		for (int i = createdLogs.Count; i < _log.Count; i++) {
			if (listElementPrefab == null)
				listElementPrefab = Resources.Load ("Prefabs/LogElement") as GameObject;
			var obj = Instantiate (listElementPrefab, new Vector3 (0, 0, 0), Quaternion.identity);
			var textObj = obj.GetComponentInChildren<Text> ();
			textObj.text += _log [i];
			createdLogs.Add (obj);
			obj.transform.SetParent (logsGrid);
		}

		//Rewrite text on created blocks 
		int index = _log.Count - 1;
		foreach (string log in _log) {
			createdLogs [index].GetComponentInChildren<Text> ().text = log;
			index--;
		}
	}

	/*
	public void GenerateList(ObservableCollection<string> logs) {
	//public NotifyCollectionChangedEventHandler GenerateList () {
		if (logs.Count == createdLogs.Count)
			return;

		//Add more elements to the list of GameObjects
		for (int i = createdLogs.Count; i < logs.Count; i++) {
			if (listElementPrefab == null)
				listElementPrefab = Resources.Load ("Prefabs/LogElement") as GameObject;
			var obj = GameObject.Instantiate (listElementPrefab, new Vector3 (0, 0, 0), Quaternion.identity);
			var textObj = obj.GetComponentInChildren<Text> ();
			textObj.text += logs[i];
			createdLogs.Add (obj);
			obj.transform.SetParent(logsGrid);
		}

		//Rewrite text on created blocks 
		int index = logs.Count-1;
		foreach (string log in logs) {
			createdLogs [index].GetComponentInChildren<Text> ().text = log;
			index--;
		}
	}
	*/	
}
