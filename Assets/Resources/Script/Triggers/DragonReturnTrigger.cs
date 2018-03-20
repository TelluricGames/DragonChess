using UnityEngine;
using System.Collections;

public class DragonReturnTrigger : Trigger
{
	private Cell GetCell(Vector3 pos, Board[] boards){
		return boards [(int)pos.z] [(int)pos.x, (int)pos.y];
	}

	#region Trigger implementation
	public void apply (Vector3 start, Vector3 end, Board[] boards)
	{
		if (GetCell (end, boards).Piece.GetComponent<Piece> ().GetType () != typeof(Dragon) || end.z != 1)
			return;

		//Dragon teleporting
		boards [(int)start.z] [(int)start.x, (int)start.y].Piece = boards [(int)end.z] [(int)end.x, (int)end.y].Piece;
		boards [(int)end.z] [(int)end.x, (int)end.y].Piece = null;

		boards [(int)start.z] [(int)start.x, (int)start.y].Piece.GetComponent<Piece> ().Move ();
	}
	#endregion

}

