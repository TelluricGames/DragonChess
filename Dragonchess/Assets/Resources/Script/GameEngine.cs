using UnityEngine;
using System.Collections.Generic;

public enum EngineResponse {
	OK,
	UNAVAILABLE_TURN,
	CHECK,
	CHECK_MATE
}

public class GameEngine
{
	private Color activePlayer = Color.WHITE;
	public Color ActivePlayer {
		get { return activePlayer; }
	}
	private Board[] boards;
	private List<Trigger> activeTriggers = new List<Trigger>(){new TransformPieceTrigger() ,new BasiliskImmobilizeTrigger(), new DragonReturnTrigger()};

	public GameEngine(Board[] boards) {
		this.boards = boards;
	}

	private Cell GetCell(Vector3 pos) {
		return boards [(int)pos.z] [(int)pos.x, (int)pos.y];
	}

	private void movePiece(Vector3 start, Vector3 end) {
		var startPiece = GetCell(start).Piece;
		var endPiece = GetCell (end).Piece;
		if (endPiece != null)
			endPiece.GetComponent<Piece> ().Destroy ();
		GetCell(end).Piece = startPiece;
		GetCell(start).Piece = null;

		GetCell (end).Piece.GetComponent<Piece> ().Move ();
	}


	public EngineResponse DoTurn(Vector3 start, Vector3 end) {
		if (activePlayer == Color.NONE)
			throw new DragonChessException ("Game isn't running now! It was ended :/");
		Cell startCell = GetCell(start);
		if (startCell == null || startCell.Piece.GetComponent<Piece>().Color != activePlayer)
			return EngineResponse.UNAVAILABLE_TURN;

		List<Vector3> endPoints;
		if (GetCell (end).Piece == null) {
			//It's move
			endPoints = startCell.Piece.GetComponent<Piece> ().GetAvailableMoves (start, boards);
		} else {
			//It's capture
			endPoints = startCell.Piece.GetComponent<Piece> ().GetAvailableCaptures (start, boards);
		}

		if (!endPoints.Contains (end))
			return EngineResponse.UNAVAILABLE_TURN;
		movePiece (start, end);

		//Apply triggers

		foreach (Trigger t in activeTriggers) {
			t.apply (start, end, boards);
		}

		Color oppositeColor = (activePlayer == Color.WHITE) ? Color.BLACK : Color.WHITE;
		if (StepChecker.isCheckForSide (oppositeColor, boards)) {
			if (StepChecker.isCheckMateForSide (oppositeColor, boards)) {
				MonoBehaviour.print ("CHECK MATE. Winner is " + ActivePlayer);
				activePlayer = Color.NONE;
				return EngineResponse.CHECK_MATE;
			} else {
				activePlayer = oppositeColor;
				return EngineResponse.CHECK;
			}
		}

		activePlayer = oppositeColor;
		return EngineResponse.OK;
	}

}

