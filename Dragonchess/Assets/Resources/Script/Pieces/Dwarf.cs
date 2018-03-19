using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dwarf : BasicPiece {

	public override void Init (Color color) {
		Color = color;

		Step.setConstsFor(color);

		DefaultConstraints.SetupBuildDefaults ();
		move = new Or (
			new Or (
				new Step(Step.FORWARD),
				new Step(Step.LEFT),
				new Step(Step.RIGHT)
			).AddConstraint(new StartLevelConstraint(1, 2)),
			new Step(Step.DOWN).AddConstraint(new StartLevelConstraint(2))
		);

		DefaultConstraints.SetupBuildDefaults(new BlockAllyAndVoidCapture());
		capture = new Or (
			new Or (
				new Step(Step.LEFT_FORWARD),
				new Step(Step.RIGHT_FORWARD)
			).AddConstraint(new StartLevelConstraint(1, 2)),
			new Step(Step.UP).AddConstraint(new StartLevelConstraint(1))
		);

		addBasicConstraints ();
	}
}
