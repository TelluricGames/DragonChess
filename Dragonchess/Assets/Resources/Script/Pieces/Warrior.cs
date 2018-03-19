using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Warrior : BasicPiece {

	public override void Init (Color color) {
		Color = color;

		Step.setConstsFor(color);

		DefaultConstraints.SetupBuildDefaults ();
		move = new Step(Step.FORWARD).AddConstraint(new StartLevelConstraint(2));

		DefaultConstraints.SetupBuildDefaults(new BlockAllyAndVoidCapture());
		capture = new Or (
			new Step(Step.RIGHT_FORWARD),
			new Step(Step.LEFT_FORWARD)
		).AddConstraint(new StartLevelConstraint(2));

		addBasicConstraints ();
	}
}
