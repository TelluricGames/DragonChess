using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Oliphant : BasicPiece {

	public override void Init (Color color) {
		Color = color;

		Step.setConstsFor(color);

		DefaultConstraints.SetupBuildDefaults ();
		move = new Or (
			new UpToBorder(new Step(Step.FORWARD)),
			new UpToBorder(new Step(Step.BACKWARD)),
			new UpToBorder(new Step(Step.LEFT)),
			new UpToBorder(new Step(Step.RIGHT))
		).AddConstraint(new StartLevelConstraint(2));

		DefaultConstraints.SetupBuildDefaults(new BlockAllyAndVoidCapture());
		capture = move.Clone();

		addBasicConstraints ();
	}
}
