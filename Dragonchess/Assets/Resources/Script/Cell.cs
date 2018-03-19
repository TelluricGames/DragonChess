using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Cell : MonoBehaviour, IPointerClickHandler {
	
	static GameManager GameManager { get; set; }

	GameObject piece;
	public GameObject Piece { 
		get { return piece; }
		set {
			/*
			if (piece != null && value != null && piece != value) {
				piece.GetComponent<Piece> ().Destroy ();
			}
			*/
			piece = value;
			if (value != null) {
				piece.SetActive (true);
				piece.GetComponent<Piece> ().Coordinate = Coordinates;
			}
		}
	}
	public Vector3 Coordinates { get; set; }
	public bool IsEmpty {
		get { return Piece == null; }
	}

	CellAppearanceController _cac;

	void Awake () {
		GameManager = GameObject.FindGameObjectWithTag ("GameController").GetComponent<GameManager> ();
		_cac = GetComponent<CellAppearanceController> ();
	}

	public Cell() {
		piece = null;
	}

	/*
	public void OnMouseDown () {
		if (IsEmpty) {
			GameManager.CellClicked (Coordinates, null);
		} else {
			GameManager.CellClicked (Coordinates, Piece.GetComponent<Piece> ());
		}
	}
	*/

	#region IPointerClickHandler implementation

	public void OnPointerClick (PointerEventData eventData)
	{
		var player = GameManager.ActivePlayer;

		if (player != null && player.PlayerType == PlayerType.HUMAN) {
			var hp = player as HumanPlayer;
			if (IsEmpty) {
				hp.CellClicked (Coordinates, null);
			} else {
				hp.CellClicked (Coordinates, Piece.GetComponent<Piece> ());
			}
		}
	}

	#endregion

	public void HighlightSelect () {
		_cac.State = CellState.SELECTED;
	}

	public void HighlightMove () {
		_cac.State = CellState.MOVE;
	}

	public void HighlightCapture () {
		_cac.State = CellState.CAPTURE;
	}

	public void HighlightClear () {
		_cac.State = CellState.IDLE;
	}

	public void Destroy () {
		if (piece != null) {
			GameObject.Destroy (piece);
		}

		GameObject.Destroy (gameObject);
	}
}
