using UnityEngine;
using System.Collections.Generic;
using System;

public abstract class BasicMove : Move
{
	protected List<Constraint> defaultConstraints = DefaultConstraints.CloneDefaultSet();
	protected List<Constraint> manualAddedConstraints = new List<Constraint> ();

	protected abstract List<Vector3> CalcEndPoints(Vector3 start, Board[] boards);
	public abstract Move Clone ();

	public List<Vector3> GetMovesFrom (Vector3 startPosition, Board[] boards)
	{
		List<Vector3> endPoints = CalcEndPoints (startPosition, boards);

		foreach(Constraint c in defaultConstraints) {
			c.ChangeMove (endPoints, startPosition, boards);
		}

        /* Workaround to avoid InvalidOperationException from (presumably) CheckConstraint */
        var cloneConstraints = new List<Constraint>(manualAddedConstraints);
        foreach (Constraint c in cloneConstraints)
        {
            c.ChangeMove(endPoints, startPosition, boards);
        }
        /*
        foreach (Constraint c in manualAddedConstraints)
        {
            vs.Add(c.GetType().ToString());
            c.ChangeMove(endPoints, startPosition, boards);
        }
        */

		return endPoints;
	}

	private bool isNotAddedConstraintType(Type addingConstraintType) {
		foreach (Constraint c in manualAddedConstraints) {
			if (c.GetType () == addingConstraintType)
				return false;
		}
		return true;
	}

	public Move AddConstraint (params Constraint[] c)
	{
		foreach (Constraint constraint in c) {
			if (isNotAddedConstraintType(constraint.GetType())) 
				manualAddedConstraints.Add(constraint);
		}

		return this;
	}


	public void RemoveConstraint (Type constraintClass)
	{
		foreach (Constraint c in manualAddedConstraints) {
			if (c.GetType() == constraintClass) {
				manualAddedConstraints.Remove(c);
				break;
			}
		}

		foreach (Constraint c in defaultConstraints) {
			if (c.GetType() == constraintClass) {
					defaultConstraints.Remove(c);
				break;
			}
		}

	}
	
}

