using UnityEngine;
using System.Collections.Generic;

public class AbsoluteMove : BasicMove
{
	private Vector3 endPoint;

	public AbsoluteMove(Vector3 end) {
		endPoint = end;
	}

	#region implemented abstract members of BasicMove
	public override Move Clone ()
	{
		return new AbsoluteMove (endPoint).AddConstraint(manualAddedConstraints.ToArray());
	}
	#endregion

	protected override List<Vector3> CalcEndPoints (Vector3 start, Board[] boards)
	{
		return new List<Vector3> (){ endPoint };
	}
}

