using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class PieceListGenerator : MonoBehaviour {

	private List<GameObject> createdLogs = new List<GameObject> ();
	public Transform whiteCaptureGrid;
	public Transform blackCaptureGrid;
	private GameObject listElementPrefab;

	private Dictionary<Type, string> pieceCodes;

	// Use this for initialization
	void Start () {
		pieceCodes = new Dictionary<Type, string> ();

		pieceCodes.Add (Type.GetType("Sylph"), "S");
		pieceCodes.Add (Type.GetType("Griffon"), "G");
		pieceCodes.Add (Type.GetType("Dragon"), "R");
		pieceCodes.Add (Type.GetType("Warrior"), "W");
		pieceCodes.Add (Type.GetType("Oliphant"), "O");
		pieceCodes.Add (Type.GetType("Unicorn"), "U");
		pieceCodes.Add (Type.GetType("Hero"), "H");
		pieceCodes.Add (Type.GetType("Thief"), "T");
		pieceCodes.Add (Type.GetType("Cleric"), "C");
		pieceCodes.Add (Type.GetType("Mage"), "M");
		pieceCodes.Add (Type.GetType("King"), "K");
		pieceCodes.Add (Type.GetType("Paladin"), "P");
		pieceCodes.Add (Type.GetType("Dwarf"), "D");
		pieceCodes.Add (Type.GetType("Basilisk"), "B");
		pieceCodes.Add (Type.GetType("Elemental"), "E");
		listElementPrefab = Resources.Load ("Prefabs/CaptureElement") as GameObject;
	}


	public void Clear() {
		foreach (Transform child in whiteCaptureGrid.transform) {
			GameObject.Destroy (child.gameObject);
		}

		foreach (Transform child in blackCaptureGrid.transform) {
			GameObject.Destroy (child.gameObject);
		}

	}

	public void GenerateList(List<PieceLog> logs) {
		if (logs == null)
			throw new DragonChessException ("Pieces list doesn't exist");


		if (logs.Count == createdLogs.Count)
			return;

		//Add more elements to the list of GameObjects
		for (int i = createdLogs.Count; i < logs.Count; i++) {

			var obj = GameObject.Instantiate (listElementPrefab, new Vector3 (0, 0, 0), Quaternion.identity);
			createdLogs.Add (obj);
			var txt = obj.GetComponentInChildren<Text> ();
			string str = "";
			pieceCodes.TryGetValue (logs [i].type, out str);
			txt.text = str;

			if (logs [i].color == Color.WHITE) {
				obj.transform.SetParent(whiteCaptureGrid);
			} 
			if (logs [i].color == Color.BLACK) {
				obj.transform.SetParent(blackCaptureGrid);
			}

		}

		//TODO: Rewrite text on created blocks 

	}

}
