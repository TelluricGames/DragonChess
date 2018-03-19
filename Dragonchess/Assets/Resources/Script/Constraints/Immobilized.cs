using UnityEngine;
using System.Collections.Generic;

public class Immobilized : Constraint
{
	#region Constraint implementation
	public void ChangeMove (List<Vector3> endPoints, Vector3 startPoint, Board[] boards)
	{
		endPoints.Clear ();
	}
	#endregion
}


