using UnityEngine;
using System.Collections.Generic;

public class DefaultConstraints 
{
	private static List<Constraint> DEFAULT_CONSTRAINT_SET = new List<Constraint> () {new BoardBorderConstraint()};
	public static void SetupBuildDefaults (params Constraint[] constraints) {
		DEFAULT_CONSTRAINT_SET = new List<Constraint> () {new BoardBorderConstraint()};

		//if (constraints.Length != 0)
			DEFAULT_CONSTRAINT_SET.AddRange (constraints);
	}

	public static List<Constraint> CloneDefaultSet() {
		List<Constraint> newSet = new List<Constraint> ();
		newSet.AddRange (DEFAULT_CONSTRAINT_SET);
		return newSet;
	}
}

