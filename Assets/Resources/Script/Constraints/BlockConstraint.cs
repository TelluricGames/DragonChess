using UnityEngine;
using System.Collections.Generic;


class BlockConstraint : Constraint
{
	#region Constraint implementation

	public void ChangeMove (List<Vector3> endPoints, Vector3 startPoint, Board[] boards)
	{
		endPoints.RemoveAll (point => !boards [(int)point.z] [(int)point.x, (int)point.y].IsEmpty);
	}

	#endregion


}


