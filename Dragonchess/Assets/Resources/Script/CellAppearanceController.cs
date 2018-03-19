using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum CellState {
	NONE,
	IDLE,
	SELECTED,
	MOVE,
	CAPTURE
}

public class CellAppearanceController : MonoBehaviour {
	
	static Dictionary <string, Material> whiteMaterials = new Dictionary <string, Material> ();
	static Dictionary <string, Material> blackMaterials = new Dictionary <string, Material> ();
	public bool IsWhite { get; set; }

	CellState _state;
	public CellState State {
		get { return _state; }
		set {
			_state = value;
			ApplyState ();
		}
	}

	void Awake () {
		var folder = "Materials/Cell/";
		if (whiteMaterials.Count == 0) {
			whiteMaterials.Add ("Idle", Resources.Load (folder + "WhiteCellIdleMaterial") as Material);
			whiteMaterials.Add ("Select", Resources.Load (folder + "WhiteCellSelectMaterial") as Material);
			whiteMaterials.Add ("Move", Resources.Load (folder + "WhiteCellMoveMaterial") as Material);
			whiteMaterials.Add ("Capture", Resources.Load (folder + "WhiteCellCaptureMaterial") as Material);
		}

		if (blackMaterials.Count == 0) {
			blackMaterials.Add ("Idle", Resources.Load (folder + "BlackCellIdleMaterial") as Material);
			blackMaterials.Add ("Select", Resources.Load (folder + "BlackCellSelectMaterial") as Material);
			blackMaterials.Add ("Move", Resources.Load (folder + "BlackCellMoveMaterial") as Material);
			blackMaterials.Add ("Capture", Resources.Load (folder + "BlackCellCaptureMaterial") as Material);
		}
	}

	CellAppearanceController() {
		_state = CellState.NONE;
	}

	public void Init (bool isWhite) {
		IsWhite = isWhite;
		State = CellState.IDLE;
	}

	void ApplyState () {
		var dic = IsWhite ? whiteMaterials : blackMaterials;
		var mr = GetComponent<MeshRenderer> ();

		switch (State) {
		case CellState.IDLE:
			mr.material = dic ["Idle"];
			break;
		case CellState.SELECTED:
			mr.material = dic ["Select"];
			break;
		case CellState.MOVE:
			mr.material = dic ["Move"];
			break;
		case CellState.CAPTURE:
			mr.material = dic ["Capture"];
			break;
		}
	}
}
