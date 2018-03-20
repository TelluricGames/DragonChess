using UnityEngine;
using System.Collections;

public class TransformPieceTrigger : Trigger
{
	private Cell GetCell(Vector3 pos, Board[] boards){
		return boards [(int)pos.z] [(int)pos.x, (int)pos.y];
	}	

	public void apply (Vector3 start, Vector3 end, Board[] boards)
	{
		Piece endPiece = GetCell (end, boards).Piece.GetComponent<Piece>();
		if (endPiece.GetType () != typeof(Warrior))
			return;
		if ((endPiece.Color == Color.WHITE && end.y == boards[(int)end.z].GetHeight()-1) || (endPiece.Color == Color.BLACK && end.y == 0)) {
			Color c = endPiece.Color;
			endPiece.Coordinate = new Vector3 (0, 20, 0);
			endPiece.Move ();

			if (c == Color.BLACK) {
				end.y = 7 - end.y;
			}

			boards [(int)end.z].PlacePiece ("Hero", (int)end.x, (int)end.y, (int)end.z, c);
			if (c == Color.BLACK) {
				end.y = 7 - end.y;
			}
			var hero = GetCell (end, boards).Piece;

			var heroPiece = hero.GetComponent<Piece> ();
			//heroPiece.Coordinate = end;
			hero.transform.position = new Vector3 (0, 20, 0);
			heroPiece.Move ();
		}
	}
}

