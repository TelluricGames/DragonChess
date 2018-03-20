using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerType {
	HUMAN,
	AI
}

public abstract class Player : MonoBehaviour {
	public Color Color { get; protected set; }
	public PlayerType PlayerType { get; protected set; }
	protected Board[] _boards;
	protected GameManager _gameManager;

	public abstract void Init (Color color, Board[] boards, GameManager gameManager);
	public abstract void DoTurn ();
}
