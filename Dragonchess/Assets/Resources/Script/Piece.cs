using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Color {
	BLACK, WHITE, NONE
}

public interface Piece {
	Color Color { get; set; }
	GameManager GameManager { get; set; }
	Vector3 Coordinate { get; set; }

	string GetName();
	List<Vector3> GetAvailableMoves(Vector3 startPosition, Board[] boards);
	List<Vector3> GetAvailableCaptures(Vector3 startPosition, Board[] boards);
	Move GetFormedCapture();
	Move GetFormedMove();
	void Init(Color color);
	void Move ();
	void Destroy();
}
