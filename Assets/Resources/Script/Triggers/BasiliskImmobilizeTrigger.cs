using UnityEngine;
using System.Collections.Generic;

public class BasiliskImmobilizeTrigger : Trigger
{
	#region Trigger implementation

	private Cell GetCell(Vector3 pos, Board[] boards){
		
		return boards [(int)pos.z] [(int)pos.x, (int)pos.y];
	}

	private List<Vector3> oldBasiliskPositions = new List<Vector3> ();


	public void apply (Vector3 start, Vector3 end, Board[] boards)
	{

		//If basilisk moves
		if (GetCell (end, boards).Piece.GetComponent<Piece> ().GetType () == typeof(Basilisk)) {
			var startPieceAbove = GetCell (start + Step.UP, boards).Piece;
			if (startPieceAbove != null && startPieceAbove.GetComponent<Piece>().Color != GetCell (end, boards).Piece.GetComponent<Piece> ().Color) {
				startPieceAbove.GetComponent<Piece> ().GetFormedMove ().RemoveConstraint (typeof(Immobilized));
				startPieceAbove.GetComponent<Piece> ().GetFormedCapture ().RemoveConstraint (typeof(Immobilized));
			}
			var endPieceAbove = GetCell (end + Step.UP, boards).Piece;
			if (endPieceAbove != null && endPieceAbove.GetComponent<Piece>().Color != GetCell (end, boards).Piece.GetComponent<Piece> ().Color) {
				endPieceAbove.GetComponent<Piece> ().GetFormedMove ().AddConstraint (new Immobilized ());
				endPieceAbove.GetComponent<Piece> ().GetFormedCapture ().AddConstraint (new Immobilized ());
			}
			return;
		}

		//If basilisk is under you
		var activePiece = GetCell (end, boards).Piece.GetComponent<Piece>();
		if (end.z != 0
			&& GetCell (end + Step.DOWN, boards).Piece != null
			&& GetCell (end + Step.DOWN, boards).Piece.GetComponent<Piece> ().Color != activePiece.Color
			&& GetCell (end + Step.DOWN, boards).Piece.GetComponent<Piece> ().GetType () == typeof (Basilisk)) {
			activePiece.GetFormedMove ().AddConstraint (new Immobilized ());
			activePiece.GetFormedCapture ().AddConstraint (new Immobilized ());
			return;
		}

		//If basilisk was captured
		var newBasiliskPositions = PieceFinder.findPiecesOnBoards (typeof(Basilisk), Color.WHITE, boards);
		newBasiliskPositions.AddRange(PieceFinder.findPiecesOnBoards (typeof(Basilisk), Color.BLACK, boards));
		if (newBasiliskPositions.Count != oldBasiliskPositions.Count) {
			var difference = oldBasiliskPositions.Find((Vector3 el) => (!newBasiliskPositions.Contains(el)));
			if (oldBasiliskPositions.Contains (difference)) {
				GetCell (difference + Step.UP, boards).Piece.GetComponent<Piece> ().GetFormedMove ().RemoveConstraint (typeof(Immobilized));
				GetCell (difference + Step.UP, boards).Piece.GetComponent<Piece> ().GetFormedCapture ().RemoveConstraint (typeof(Immobilized));
			}
			oldBasiliskPositions = newBasiliskPositions;
		}



	}

	#endregion


}

