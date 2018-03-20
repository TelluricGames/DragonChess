using UnityEngine;
using System.Collections.Generic;

public class Follow : BasicMove
{
	Move firstMove;
	Move nextMove;

	public Follow(Move first, Move next){
		firstMove = first;
		nextMove = next;
	}

	#region implemented abstract members of BasicMove

	public override Move Clone ()
	{
		return new Follow (firstMove.Clone(), nextMove.Clone()).AddConstraint (manualAddedConstraints.ToArray());
	}

	#endregion

	protected override List<Vector3> CalcEndPoints (Vector3 start, Board[] boards)
	{
		List<Vector3> firstMoveOut = firstMove.GetMovesFrom (start, boards);
		List<Vector3> outDots = new List<Vector3> ();
		foreach (Vector3 nextMoveStart in firstMoveOut) {
			outDots.AddRange (nextMove.GetMovesFrom (nextMoveStart, boards));
		}

		return outDots;
	}

}

