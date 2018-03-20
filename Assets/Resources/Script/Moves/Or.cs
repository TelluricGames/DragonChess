using UnityEngine;
using System.Collections.Generic;

public class Or : BasicMove
{
	private Move[] moves;

	public Or(params Move[] variableMoves) {
		moves = variableMoves;
	}

	public override Move Clone() {
		List<Move> clonedMoves = new List<Move> ();
		foreach(Move m in moves) {
			clonedMoves.Add (m.Clone ());	
		}

		return new Or (clonedMoves.ToArray ()).AddConstraint(manualAddedConstraints.ToArray());
	}

	protected override List<Vector3> CalcEndPoints (Vector3 start, Board[] boards)
	{
		List<Vector3> outDots = new List<Vector3> ();

		foreach (Move move in moves) {
			outDots.AddRange (move.GetMovesFrom (start, boards));
		}

		return outDots;
	}

}

