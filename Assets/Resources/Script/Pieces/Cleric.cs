using UnityEngine;
using System.Collections;

public class Cleric : BasicPiece
{
	public override void Init (Color color) {
		Color = color;

		DefaultConstraints.SetupBuildDefaults ();

		move = new Or (
			new RoundMove(),
			new Step(Step.UP),
			new Step(Step.DOWN)
		);

		DefaultConstraints.SetupBuildDefaults(new BlockAllyAndVoidCapture());
		capture = move.Clone ();

		addBasicConstraints ();
	}
}

