using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RoundMove : BasicMove
{
	private Vector3[] directionsSet;

	public RoundMove() {
		directionsSet = new Vector3[]{Step.FORWARD, Step.LEFT_FORWARD, Step.LEFT, Step.LEFT_BACKWARD, Step.BACKWARD, Step.RIGHT_BACKWARD, Step.RIGHT, Step.RIGHT_FORWARD};
	}


	public override Move Clone() {
		return new RoundMove ().AddConstraint (manualAddedConstraints.ToArray());
	}

	protected override List<Vector3> CalcEndPoints (Vector3 start, Board[] boards)
	{
		List<Vector3> endPoints = new List<Vector3>();
		foreach (Vector3 dir in directionsSet) {
			endPoints.Add (dir + start);
		}
		return endPoints;
	}
}

