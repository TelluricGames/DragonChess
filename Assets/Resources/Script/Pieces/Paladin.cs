using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paladin : BasicPiece {

	public override void Init (Color color) {
		Color = color;

		Step.setConstsFor(color);

		DefaultConstraints.SetupBuildDefaults ();
		move = new Or (
			new RoundMove (),

			new Or (
				new Step (Step.FORWARD + Step.RIGHT_FORWARD),
				new Step (Step.RIGHT + Step.RIGHT_FORWARD),
				new Step (Step.RIGHT + Step.RIGHT_BACKWARD),
				new Step (Step.BACKWARD + Step.RIGHT_BACKWARD),
				new Step (Step.BACKWARD + Step.LEFT_BACKWARD),
				new Step (Step.LEFT + Step.LEFT_BACKWARD),
				new Step (Step.LEFT + Step.LEFT_FORWARD),
				new Step (Step.FORWARD + Step.LEFT_FORWARD)
			).AddConstraint (new StartLevelConstraint (2)),

			new Step(Step.UP + 2f * Step.FORWARD),
			new Step(Step.UP + 2f * Step.BACKWARD),
			new Step(Step.UP + 2f * Step.LEFT),
			new Step(Step.UP + 2f * Step.RIGHT),
			new Step(Step.DOWN + 2f * Step.FORWARD),
			new Step(Step.DOWN + 2f * Step.BACKWARD),
			new Step(Step.DOWN + 2f * Step.LEFT),
			new Step(Step.DOWN + 2f * Step.RIGHT),

			new Step(2f * Step.UP + Step.FORWARD),
			new Step(2f * Step.UP + Step.BACKWARD),
			new Step(2f * Step.UP + Step.LEFT),
			new Step(2f * Step.UP + Step.RIGHT),
			new Step(2f * Step.DOWN + Step.FORWARD),
			new Step(2f * Step.DOWN + Step.BACKWARD),
			new Step(2f * Step.DOWN + Step.LEFT),
			new Step(2f * Step.DOWN + Step.RIGHT)
		);

		DefaultConstraints.SetupBuildDefaults(new BlockAllyAndVoidCapture());
		capture = new Or (
			new RoundMove (),

			new Or (
				new Step (Step.FORWARD + Step.RIGHT_FORWARD),
				new Step (Step.RIGHT + Step.RIGHT_FORWARD),
				new Step (Step.RIGHT + Step.RIGHT_BACKWARD),
				new Step (Step.BACKWARD + Step.RIGHT_BACKWARD),
				new Step (Step.BACKWARD + Step.LEFT_BACKWARD),
				new Step (Step.LEFT + Step.LEFT_BACKWARD),
				new Step (Step.LEFT + Step.LEFT_FORWARD),
				new Step (Step.FORWARD + Step.LEFT_FORWARD)
			).AddConstraint (new StartLevelConstraint (2))
		);

		addBasicConstraints ();
	}
}
