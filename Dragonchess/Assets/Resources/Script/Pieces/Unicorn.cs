using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unicorn : BasicPiece {

	public override void Init (Color color) {
		Color = color;

		Step.setConstsFor(color);

		DefaultConstraints.SetupBuildDefaults ();
		move = new Or (
			new Step(Step.FORWARD + Step.RIGHT_FORWARD),
			new Step(Step.RIGHT + Step.RIGHT_FORWARD),
			new Step(Step.RIGHT + Step.RIGHT_BACKWARD),
			new Step(Step.BACKWARD + Step.RIGHT_BACKWARD),
			new Step(Step.BACKWARD + Step.LEFT_BACKWARD),
			new Step(Step.LEFT + Step.LEFT_BACKWARD),
			new Step(Step.LEFT + Step.LEFT_FORWARD),
			new Step(Step.FORWARD + Step.LEFT_FORWARD)
		).AddConstraint(new StartLevelConstraint(2));

		DefaultConstraints.SetupBuildDefaults(new BlockAllyAndVoidCapture());
		capture = move.Clone ();

		addBasicConstraints ();
	}
}
