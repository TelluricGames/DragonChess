using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mage : BasicPiece {

	public override void Init (Color color) {
		Color = color;

		Step.setConstsFor(color);

		DefaultConstraints.SetupBuildDefaults ();
		move = new Or (
			new Or (
				new UpToBorder(new Step(Step.FORWARD)),
				new UpToBorder(new Step(Step.BACKWARD)),
				new UpToBorder(new Step(Step.LEFT)),
				new UpToBorder(new Step(Step.RIGHT)),
				new UpToBorder(new Step(Step.RIGHT_FORWARD)),
				new UpToBorder(new Step(Step.RIGHT_BACKWARD)),
				new UpToBorder(new Step(Step.LEFT_FORWARD)),
				new UpToBorder(new Step(Step.LEFT_BACKWARD))
			).AddConstraint(new StartLevelConstraint(2)),
			new Or (
				new Step(Step.FORWARD),
				new Step(Step.BACKWARD),
				new Step(Step.LEFT),
				new Step(Step.RIGHT)
			).AddConstraint(new StartLevelConstraint(1, 3)),
			new Or (
				new Step(Step.UP),
				new Step(Step.DOWN),
				new Follow(new Step(Step.UP), new Step(Step.UP)),
				new Follow(new Step(Step.DOWN), new Step(Step.DOWN))
			)
		);

		DefaultConstraints.SetupBuildDefaults(new BlockAllyAndVoidCapture());
		capture = move.Clone();

		addBasicConstraints ();
	}
}
