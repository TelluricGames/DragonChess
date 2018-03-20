using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dragon : BasicPiece {

	public override void Init (Color color) {
		Color = color;

		Step.setConstsFor(color);

		DefaultConstraints.SetupBuildDefaults ();
		move = new Or (
			new Step(Step.FORWARD),
			new Step(Step.BACKWARD),
			new Step(Step.LEFT),
			new Step(Step.RIGHT),
			new UpToBorder(new Step(Step.RIGHT_FORWARD)),
			new UpToBorder(new Step(Step.RIGHT_BACKWARD)),
			new UpToBorder(new Step(Step.LEFT_BACKWARD)),
			new UpToBorder(new Step(Step.LEFT_FORWARD))
		).AddConstraint(new StartLevelConstraint(3));

		DefaultConstraints.SetupBuildDefaults(new BlockAllyAndVoidCapture());
		capture = new Or (
			new Step(Step.FORWARD),
			new Step(Step.BACKWARD),
			new Step(Step.LEFT),
			new Step(Step.RIGHT),
			new UpToBorder(new Step(Step.RIGHT_FORWARD)),
			new UpToBorder(new Step(Step.RIGHT_BACKWARD)),
			new UpToBorder(new Step(Step.LEFT_BACKWARD)),
			new UpToBorder(new Step(Step.LEFT_FORWARD)),

			/* special moves */
			new Step(Step.DOWN),
			new Step(Step.DOWN + Step.FORWARD),
			new Step(Step.DOWN + Step.BACKWARD),
			new Step(Step.DOWN + Step.LEFT),
			new Step(Step.DOWN + Step.RIGHT)
		).AddConstraint(new StartLevelConstraint(3));

		addBasicConstraints ();
	}
}
