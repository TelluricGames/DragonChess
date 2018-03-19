using UnityEngine;
using System.Collections.Generic;
using System;
public class PieceFinder
{
	public static List<Vector3> findPiecesOnBoards (Type pieceType, Color pieceColor, Board[] boards) {
		List<Vector3> positions = new List<Vector3> ();
		for (int boardId = 0; boardId < boards.Length; boardId++)
			for (int x = 0; x < boards [boardId].GetLength (); x++)
				for (int y = 0; y < boards [boardId].GetHeight (); y++)
					/* It dies here cause of null cell */
					if (!boards [boardId] [x, y].IsEmpty && boards [boardId] [x, y].Piece.GetComponent<Piece> ().GetType () == pieceType &&  boards [boardId] [x, y].Piece.GetComponent<Piece> ().Color == pieceColor)
						positions.Add (new Vector3 (x, y, boardId));
		return positions;
	}
	public static List<Vector3> findAllPieces(Color color, Board[] boards){
		List<Vector3> positions = new List<Vector3> ();
		for (int boardId = 0; boardId < boards.Length; boardId++)
			for (int x = 0; x < boards [boardId].GetLength (); x++)
				for (int y = 0; y < boards [boardId].GetHeight (); y++)
					if (!boards [boardId] [x,y].IsEmpty && boards [boardId] [x,y].Piece.GetComponent<Piece> ().Color == color)
						positions.Add (new Vector3 (x, y, boardId));
		return positions;
	}
}

