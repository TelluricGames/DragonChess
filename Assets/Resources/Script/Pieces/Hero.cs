using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hero : BasicPiece {

	Vector3? oldpos = null;

	public override void Init (Color color) {
		Color = color;

		Step.setConstsFor(color);

		DefaultConstraints.SetupBuildDefaults ();
		move = new Or (
			new Step(Step.RIGHT_FORWARD),
			new Step(Step.RIGHT_BACKWARD),
			new Step(Step.LEFT_BACKWARD),
			new Step(Step.LEFT_FORWARD),

			new Step(2f * Step.RIGHT_BACKWARD),
			new Step(2f * Step.RIGHT_FORWARD),
			new Step(2f * Step.LEFT_BACKWARD),
			new Step(2f * Step.LEFT_FORWARD),

			new Step(Step.TRIAGONAL_DOWN_LEFT_BACKWARD),
			new Step(Step.TRIAGONAL_DOWN_LEFT_FORWARD),
			new Step(Step.TRIAGONAL_DOWN_RIGHT_BACKWARD),
			new Step(Step.TRIAGONAL_DOWN_RIGHT_FORWARD),
			new Step(Step.TRIAGONAL_UP_LEFT_BACKWARD),
			new Step(Step.TRIAGONAL_UP_LEFT_FORWARD),
			new Step(Step.TRIAGONAL_UP_RIGHT_BACKWARD),
			new Step(Step.TRIAGONAL_UP_RIGHT_FORWARD)
		).AddConstraint(new StartLevelConstraint(2));

		DefaultConstraints.SetupBuildDefaults(new BlockAllyAndVoidCapture());
		capture = move.Clone();

		addBasicConstraints ();
	}

	public override List<Vector3> GetAvailableMoves (Vector3 startPosition, Board[] boards)
	{
		if (startPosition.z == 1) {
			oldpos = startPosition;

			return move.GetMovesFrom (startPosition, boards);
		} else {
			if (!oldpos.HasValue)
				throw new UnassignedReferenceException ("Hero's old position must be used but it's not assigned");
			
			//return new List<Vector3> () { oldpos.Value };
			DefaultConstraints.SetupBuildDefaults();
			var move = new AbsoluteMove (oldpos.Value);
			move.AddConstraint (new BlockConstraint (), new CheckConstraint ());
			return move.GetMovesFrom (startPosition, boards);
		}
	}

	public override List<Vector3> GetAvailableCaptures (Vector3 startPosition, Board[] boards)
	{
		if (startPosition.z == 1) {
			oldpos = startPosition;

			return capture.GetMovesFrom (startPosition, boards);
		} else {
			if (!oldpos.HasValue)
				throw new UnassignedReferenceException ("Hero's old position must be used but it's not assigned");

			DefaultConstraints.SetupBuildDefaults(new BlockAllyAndVoidCapture());
			var move = new AbsoluteMove (oldpos.Value);
			move.AddConstraint (new BlockAllyAndVoidCapture (), new CheckConstraint ());
			return move.GetMovesFrom (startPosition, boards);
		}
	}
}
