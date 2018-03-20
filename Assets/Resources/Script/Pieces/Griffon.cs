using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Griffon : BasicPiece {

	public override void Init (Color color) {
		Color = color;

		Step.setConstsFor(color);

		DefaultConstraints.SetupBuildDefaults ();
		move = new Or (
			new Or (
				new Step(Step.FORWARD * 3f + Step.RIGHT * 2f),
				new Step(Step.FORWARD * 2f + Step.RIGHT * 3f),
				new Step(Step.BACKWARD * 2f + Step.RIGHT * 3f),
				new Step(Step.BACKWARD * 3f + Step.RIGHT * 2f),
				new Step(Step.BACKWARD * 3f + Step.LEFT * 2f),
				new Step(Step.BACKWARD * 2f + Step.LEFT * 3f),
				new Step(Step.FORWARD * 2f + Step.LEFT * 3f),
				new Step(Step.FORWARD * 3f + Step.LEFT * 2f),

				new Step(Step.TRIAGONAL_DOWN_LEFT_BACKWARD),
				new Step(Step.TRIAGONAL_DOWN_LEFT_FORWARD),
				new Step(Step.TRIAGONAL_DOWN_RIGHT_BACKWARD),
				new Step(Step.TRIAGONAL_DOWN_RIGHT_FORWARD)
			).AddConstraint(new StartLevelConstraint(3)),
			new Or (
				new Step(Step.RIGHT_FORWARD),
				new Step(Step.RIGHT_BACKWARD),
				new Step(Step.LEFT_BACKWARD),
				new Step(Step.LEFT_FORWARD),

				new Step(Step.TRIAGONAL_UP_LEFT_BACKWARD),
				new Step(Step.TRIAGONAL_UP_LEFT_FORWARD),
				new Step(Step.TRIAGONAL_UP_RIGHT_BACKWARD),
				new Step(Step.TRIAGONAL_UP_RIGHT_FORWARD)
			).AddConstraint(new StartLevelConstraint(2))
		);

		DefaultConstraints.SetupBuildDefaults(new BlockAllyAndVoidCapture());
		capture = move.Clone();

		addBasicConstraints ();
	}
}