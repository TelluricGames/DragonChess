using UnityEngine;
using System.Collections.Generic;

public class StepChecker
{
	private static Cell GetCell(Vector3 pos, Board[]boards) {
		return boards [(int)pos.z] [(int)pos.x, (int)pos.y];
	}

	public static bool IsCheckAfterTurn(Vector3 startPoint, Vector3 endPoint, Color checkForThisColor, Board[] boards) {
		GameObject savedPiece = GetCell (endPoint, boards).Piece;

		GetCell(endPoint, boards).Piece = GetCell(startPoint, boards).Piece;
		GetCell(startPoint, boards).Piece = null;
		bool checkResult = isCheckForSide (checkForThisColor, boards);
		GetCell(startPoint, boards).Piece = GetCell(endPoint, boards).Piece;
		GetCell(endPoint, boards).Piece = savedPiece;

		return checkResult;
	}

	public static bool isCheckForSide (Color color, Board[] boards) {
		List<Vector3> kingPosList = PieceFinder.findPiecesOnBoards (typeof(King), color, boards);
		if (kingPosList.Count != 1)
			throw new DragonChessException ("King wasn't found or they are too many! Number kings is: " + kingPosList.Count);
		Vector3 kingPos = kingPosList.ToArray () [0];

		for (int boardId = 0; boardId < boards.Length; boardId++)
			for (int x = 0; x < boards [boardId].GetLength (); x++)
				for (int y = 0; y < boards [boardId].GetHeight (); y++)
					if (WillBeKingKilledByPieceOn (new Vector3(x,y,boardId), kingPos, boards))
						return true;
		return false;
	}

	private static bool WillBeKingKilledByPieceOn(Vector3 attackerPos, Vector3 kingPos, Board[] boards) {

		Color oppositeKingColor = (GetCell(kingPos, boards).Piece.GetComponent<Piece> ().Color == Color.WHITE) ? Color.BLACK : Color.WHITE;
		Cell oppositePieceCell = GetCell(attackerPos, boards);
		if (oppositePieceCell.IsEmpty || oppositePieceCell.Piece.GetComponent<Piece> ().Color != oppositeKingColor)
			return false; 
		oppositePieceCell.Piece.GetComponent<Piece> ().GetFormedCapture().RemoveConstraint (typeof(CheckConstraint));
		List<Vector3> endPoints = oppositePieceCell.Piece.GetComponent<Piece> ().GetAvailableCaptures (attackerPos, boards);
		oppositePieceCell.Piece.GetComponent<Piece> ().GetFormedCapture().AddConstraint (new CheckConstraint());

		foreach (Vector3 capturePoint in endPoints) {
			if (capturePoint == kingPos)
				return true;
		}

		return false;
	}

	public static bool isCheckMateForSide(Color color, Board[] boards){
		List<Vector3> ourPiecesPositions = PieceFinder.findAllPieces (color, boards);
		foreach (Vector3 piecePos in ourPiecesPositions) {
			Piece piece = GetCell(piecePos, boards).Piece.GetComponent<Piece>();
			if (piece.GetAvailableMoves (piecePos, boards).Count != 0 || piece.GetAvailableCaptures(piecePos, boards).Count != 0)
				return false;
		}
		return true;
	}
}

