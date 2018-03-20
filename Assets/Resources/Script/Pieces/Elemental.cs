using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elemental : BasicPiece {

	public override void Init (Color color) {
		Color = color;

		Step.setConstsFor(color);

		DefaultConstraints.SetupBuildDefaults ();
		move = new Or (
			new Or (
				new RoundMove(),

				new Follow(new Step(Step.FORWARD), new Step(Step.FORWARD)),
				new Follow(new Step(Step.BACKWARD), new Step(Step.BACKWARD)),
				new Follow(new Step(Step.LEFT), new Step(Step.LEFT)),
				new Follow(new Step(Step.RIGHT), new Step(Step.RIGHT))
			).AddConstraint(new StartLevelConstraint(1)),
			new Or (
				new Follow(new Step(Step.DOWN), new Step(Step.FORWARD)),
				new Follow(new Step(Step.DOWN), new Step(Step.BACKWARD)),
				new Follow(new Step(Step.DOWN), new Step(Step.LEFT)),
				new Follow(new Step(Step.DOWN), new Step(Step.RIGHT))
			).AddConstraint(new StartLevelConstraint(2))
		);

		DefaultConstraints.SetupBuildDefaults(new BlockAllyAndVoidCapture());
		capture = new Or (
			new Or (
				new Step(Step.FORWARD),
				new Step(Step.BACKWARD),
				new Step(Step.LEFT),
				new Step(Step.RIGHT),

				new Follow(new Step(Step.FORWARD), new Step(Step.FORWARD)),
				new Follow(new Step(Step.BACKWARD), new Step(Step.BACKWARD)),
				new Follow(new Step(Step.LEFT), new Step(Step.LEFT)),
				new Follow(new Step(Step.RIGHT), new Step(Step.RIGHT)),

				new Follow(new Step(Step.FORWARD), new Step(Step.UP)),
				new Follow(new Step(Step.BACKWARD), new Step(Step.UP)),
				new Follow(new Step(Step.LEFT), new Step(Step.UP)),
				new Follow(new Step(Step.RIGHT), new Step(Step.UP))
			).AddConstraint(new StartLevelConstraint(1)),
			new Or (
				new Follow(new Step(Step.DOWN), new Step(Step.FORWARD)),
				new Follow(new Step(Step.DOWN), new Step(Step.BACKWARD)),
				new Follow(new Step(Step.DOWN), new Step(Step.LEFT)),
				new Follow(new Step(Step.DOWN), new Step(Step.RIGHT))
			).AddConstraint(new StartLevelConstraint(2))
		);

		addBasicConstraints ();
	}
}
