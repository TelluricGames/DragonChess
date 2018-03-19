using UnityEngine;
using System.Collections.Generic;

public class StartLevelConstraint : Constraint
{
	private int[] allowedLevels;

	public StartLevelConstraint(params int[] levels) {
		allowedLevels = new int[levels.Length];

		int i = 0;
		foreach (int level in levels) {
			allowedLevels [i] = level - 1;
			i++;
		}
	}


	#region Constraint implementation

	public void ChangeMove (List<Vector3> endPoints, Vector3 startPoint, Board[] boards)
	{
		foreach (int level in allowedLevels) {
			if (startPoint.z == level) return;
		}
		endPoints.Clear ();
	}

	#endregion
}

