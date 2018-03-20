using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Thief : BasicPiece {

	public override void Init (Color color) {
		Color = color;

		Step.setConstsFor(color);

		DefaultConstraints.SetupBuildDefaults ();
		move = new Or (
			new UpToBorder(new Step(Step.RIGHT_FORWARD)),
			new UpToBorder(new Step(Step.RIGHT_BACKWARD)),
			new UpToBorder(new Step(Step.LEFT_FORWARD)),
			new UpToBorder(new Step(Step.LEFT_BACKWARD))
		).AddConstraint(new StartLevelConstraint(2));

		DefaultConstraints.SetupBuildDefaults(new BlockAllyAndVoidCapture());
		capture = move.Clone();

		addBasicConstraints ();
	}
}
