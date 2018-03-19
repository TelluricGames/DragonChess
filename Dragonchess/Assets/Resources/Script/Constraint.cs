using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface Constraint {
	void ChangeMove(List<Vector3> endPoints, Vector3 startPoint, Board[] boards);
}
