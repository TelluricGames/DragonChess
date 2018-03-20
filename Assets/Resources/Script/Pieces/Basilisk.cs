using UnityEngine;
using System.Collections.Generic;

public class Basilisk : BasicPiece
{
	public override void Init (Color color) {
		Color = color;

		Step.setConstsFor(color);

		DefaultConstraints.SetupBuildDefaults ();
		move = new Or (
			new Step(Step.FORWARD),
			new Step(Step.LEFT_FORWARD),
			new Step(Step.RIGHT_FORWARD),
			new Step(Step.BACKWARD)
		).AddConstraint(new StartLevelConstraint(1));

		DefaultConstraints.SetupBuildDefaults(new BlockAllyAndVoidCapture());
		capture = new Or (
			new Step(Step.FORWARD),
			new Step(Step.LEFT_FORWARD),
			new Step(Step.RIGHT_FORWARD)
		).AddConstraint(new StartLevelConstraint(1));

		addBasicConstraints ();
	}
}

