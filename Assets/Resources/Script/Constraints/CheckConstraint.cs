using UnityEngine;
using System.Collections.Generic;

public class CheckConstraint : Constraint
{
	#region Constraint implementation
	public void ChangeMove (List<Vector3> endPoints, Vector3 startPoint, Board[] boards)
	{
		Color myColor = boards [(int)startPoint.z] [(int)startPoint.x, (int)startPoint.y].Piece.GetComponent<Piece>().Color;
		endPoints.RemoveAll (point => StepChecker.IsCheckAfterTurn(startPoint, point, myColor, boards));
	}
	#endregion


}

