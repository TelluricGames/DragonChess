using UnityEngine;
using System.Collections;

public class King : BasicPiece
{
	public override void Init (Color color) {
		Color = color;

		DefaultConstraints.SetupBuildDefaults ();
		move = new Or (
			new RoundMove().AddConstraint(new StartLevelConstraint(2)),
			new Step(Step.UP),
			new Step(Step.DOWN)
		);

		DefaultConstraints.SetupBuildDefaults (new BlockAllyAndVoidCapture ());
		capture = move.Clone ();
	
		addBasicConstraints ();
	}
}

