using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sylph : BasicPiece {

	static Move[] WhiteLevelTwoMoves = new Move[] {
		new Step(Step.UP),
		new AbsoluteMove(new Vector3(0, 1, 2)),
		new AbsoluteMove(new Vector3(2, 1, 2)),
		new AbsoluteMove(new Vector3(4, 1, 2)),
		new AbsoluteMove(new Vector3(6, 1, 2)),
		new AbsoluteMove(new Vector3(8, 1, 2)),
		new AbsoluteMove(new Vector3(10, 1, 2))
	};

	static Move[] BlackLevelTwoMoves = new Move[] {
		new Step(Step.UP),
		new AbsoluteMove(new Vector3(0, 6, 2)),
		new AbsoluteMove(new Vector3(2, 6, 2)),
		new AbsoluteMove(new Vector3(4, 6, 2)),
		new AbsoluteMove(new Vector3(6, 6, 2)),
		new AbsoluteMove(new Vector3(8, 6, 2)),
		new AbsoluteMove(new Vector3(10, 6, 2))
	};

	public override void Init (Color color) {
		Color = color;

		Step.setConstsFor(color);

		DefaultConstraints.SetupBuildDefaults ();
		move = new Or (
			new Or (
				new Step(Step.LEFT_FORWARD),
				new Step(Step.RIGHT_FORWARD)
			).AddConstraint(new StartLevelConstraint(3)),
			new Or (
				color == Color.BLACK ? BlackLevelTwoMoves : WhiteLevelTwoMoves
			).AddConstraint(new StartLevelConstraint(2))
		);

		DefaultConstraints.SetupBuildDefaults(new BlockAllyAndVoidCapture());
		capture = new Or (
			new Step (Step.DOWN),
			new Step (Step.FORWARD)
		).AddConstraint (new StartLevelConstraint (3));

		addBasicConstraints ();
	}
}
