using UnityEngine;
using UnityEditor;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;

public class GameEngineTests {




	[UnityTest]
	public IEnumerator CasualStepTest() {
		Board[] boards = new Board[3];
		for (int i = 0; i < 3; i++) {
			boards [i] = new Board ();
			boards [i].Init (i);
		}
		GameEngine eng = new GameEngine (boards);
		EngineResponse resp;
		resp = eng.DoTurn (new Vector3 (0, 6, 1), new Vector3 (0, 5, 1));
		Assert.AreEqual (resp, EngineResponse.OK);
		yield return null;
	}

}
