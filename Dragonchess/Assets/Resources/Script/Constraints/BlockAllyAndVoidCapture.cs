using UnityEngine;
using System.Collections.Generic;

public class BlockAllyAndVoidCapture : Constraint
{
	#region Constraint implementation

	public void ChangeMove (List<Vector3> endPoints, Vector3 startPoint, Board[] boards)
	{
		Color ourColor = boards [(int)startPoint.z] [(int)startPoint.x, (int)startPoint.y].Piece.GetComponent<Piece>().Color;
		endPoints.RemoveAll(point => (boards[(int)point.z][(int)point.x,(int)point.y].IsEmpty
									 || boards[(int)point.z][(int)point.x,(int)point.y].Piece.GetComponent<Piece>().Color == ourColor));
	}

	#endregion

}


