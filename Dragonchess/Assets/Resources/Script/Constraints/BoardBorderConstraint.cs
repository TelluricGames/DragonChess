using System;
using System.Collections.Generic;
using UnityEngine;

public class BoardBorderConstraint : Constraint
{
	#region Constraint implementation
	public void ChangeMove (List<Vector3> endPoints, Vector3 startPoint, Board[] boards)
	{
		endPoints.RemoveAll(point => point.z < 0 || point.z >= boards.Length || point.x < 0 || point.x >= boards [(int)point.z].GetLength () || point.y < 0 || point.y >= boards[(int)point.z].GetHeight ());
	}
	#endregion

}
