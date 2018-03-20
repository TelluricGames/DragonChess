using System.Collections.Generic;
using System;
using UnityEngine;

public interface Move {
	List<Vector3> GetMovesFrom(Vector3 startPosition, Board[] boards);
	Move AddConstraint(params Constraint[] c);
	void RemoveConstraint(Type constraintType);
	Move Clone();
}
