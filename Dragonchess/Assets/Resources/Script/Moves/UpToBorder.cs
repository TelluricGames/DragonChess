using UnityEngine;
using System.Collections.Generic;
using System;

public class UpToBorder : BasicMove
{
	Step move;
	public UpToBorder(Step baseMove) {
		move = baseMove;
		move.RemoveConstraint (typeof(BlockAllyAndVoidCapture));
	}


	private bool IsOnBorder(Vector3 pos, Board[] boards) {
		return (pos.z >= 0 && pos.z < boards.Length && pos.y >= 0 && pos.y < boards [0].GetHeight () && pos.x >= 0 && pos.x < boards [0].GetLength ());
	}

	private bool IsEmptyCell(Vector3 pos, Board[] boards) {
		return (boards [(int)pos.z] [(int)pos.x, (int)pos.y].Piece == null);
	}

	private bool IsEnemy(Vector3 pos, Board[] boards, Color ourColor) {
		return (boards [(int)pos.z] [(int)pos.x, (int)pos.y].Piece.GetComponent<Piece> ().Color != ourColor);
	}

	protected override List<Vector3> CalcEndPoints (Vector3 pos, Board[] boards)
	{
		List<Vector3> way = new List<Vector3>(){pos};
		Color ourColor = boards[(int)pos.z][(int)pos.x, (int)pos.y].Piece.GetComponent<Piece>().Color;

		while (true) {
			Vector3 currentPos = way[way.Count-1];
			List<Vector3> nextPositions = move.GetMovesFrom (currentPos, boards);
			if (nextPositions.Count == 0)
				break;
			Vector3 nextPos = nextPositions [0];
			way.Add (nextPos);
			if (!IsOnBorder (nextPos, boards) || (!IsEmptyCell (nextPos, boards) && IsEnemy (nextPos, boards, ourColor))) {
				break;
			}
			if (IsOnBorder(nextPos, boards) && !IsEmptyCell(nextPos, boards) && !IsEnemy(nextPos, boards, ourColor)) {
				break;
			}
		}
		return way;
			
	}
	public override Move Clone ()
	{
		return new UpToBorder ((Step)move.Clone()).AddConstraint(manualAddedConstraints.ToArray());
	}
}

