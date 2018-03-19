using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Step : BasicMove {
	private Vector3 dir;

	public Step(Vector3 direction) {
		dir = direction;
	}

	public override Move Clone() {
		return new Step (dir).AddConstraint (manualAddedConstraints.ToArray());
	}

	protected override List<Vector3> CalcEndPoints (Vector3 start, Board[] boards)
	{
		return new List<Vector3>(){start + dir};		
	}


	public static void setConstsFor (Color color)
	{
		if (color == Color.WHITE) {
			RIGHT 	 = new Vector3( 1, 0, 0);
			LEFT 	 = new Vector3(-1, 0, 0);
			FORWARD  = new Vector3( 0, 1, 0);
			BACKWARD = new Vector3( 0,-1, 0);
		} 
		if (color == Color.BLACK) {
			RIGHT 	 = new Vector3(-1, 0, 0);
			LEFT 	 = new Vector3( 1, 0, 0);
			FORWARD  = new Vector3( 0,-1, 0);
			BACKWARD = new Vector3( 0, 1, 0);
		}
		buildAllSteps ();	
	}

	private static void buildAllSteps() {
		//Recalculate triagonal and diagonal moves for new forward/backward left/right system
		RIGHT_FORWARD  = RIGHT+FORWARD;
		RIGHT_BACKWARD = RIGHT+BACKWARD;
		LEFT_FORWARD   = LEFT+FORWARD;
		LEFT_BACKWARD  = LEFT+BACKWARD;

		TRIAGONAL_UP_LEFT_FORWARD     = UP+LEFT+FORWARD;
		TRIAGONAL_UP_RIGHT_FORWARD 	  = UP+RIGHT+FORWARD;
		TRIAGONAL_UP_LEFT_BACKWARD 	  = UP+LEFT+BACKWARD;
		TRIAGONAL_UP_RIGHT_BACKWARD	  = UP+RIGHT+BACKWARD;
		TRIAGONAL_DOWN_LEFT_FORWARD   = DOWN+LEFT+FORWARD;
		TRIAGONAL_DOWN_RIGHT_FORWARD  = DOWN+RIGHT+FORWARD;
		TRIAGONAL_DOWN_LEFT_BACKWARD  = DOWN+LEFT+BACKWARD;
		TRIAGONAL_DOWN_RIGHT_BACKWARD = DOWN+RIGHT+BACKWARD;
		
	}

	//Default values for consts
	public static Vector3 UP   = new Vector3( 0, 0, 1);
	public static Vector3 DOWN = new Vector3( 0, 0,-1);

	//Rotateable block
	public static Vector3 RIGHT 		 = new Vector3( 1, 0, 0);
	public static Vector3 LEFT 		  	 = new Vector3(-1, 0, 0);
	public static Vector3 FORWARD 		 = new Vector3( 0, 1, 0);
	public static Vector3 BACKWARD 	  	 = new Vector3( 0,-1, 0);
	public static Vector3 RIGHT_FORWARD  = RIGHT+FORWARD;
	public static Vector3 RIGHT_BACKWARD = RIGHT+BACKWARD;
	public static Vector3 LEFT_FORWARD   = LEFT+FORWARD;
	public static Vector3 LEFT_BACKWARD  = LEFT+BACKWARD;

	//Triagonal moves
	public static Vector3 TRIAGONAL_UP_LEFT_FORWARD   	 = UP+LEFT+FORWARD;
	public static Vector3 TRIAGONAL_UP_RIGHT_FORWARD 	 = UP+RIGHT+FORWARD;
	public static Vector3 TRIAGONAL_UP_LEFT_BACKWARD 	 = UP+LEFT+BACKWARD;
	public static Vector3 TRIAGONAL_UP_RIGHT_BACKWARD	 = UP+RIGHT+BACKWARD;
	public static Vector3 TRIAGONAL_DOWN_LEFT_FORWARD 	 = DOWN+LEFT+FORWARD;
	public static Vector3 TRIAGONAL_DOWN_RIGHT_FORWARD  = DOWN+RIGHT+FORWARD;
	public static Vector3 TRIAGONAL_DOWN_LEFT_BACKWARD  = DOWN+LEFT+BACKWARD;
	public static Vector3 TRIAGONAL_DOWN_RIGHT_BACKWARD = DOWN+RIGHT+BACKWARD;
}



