using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum PieceState {
	STATIONARY,
	NEW_WAYPOINT,
	MOVING_UP,
	MOVING_DOWN,
	MOVING_ELLIPSE,
	MOVING_STRAIGHT
}

public class PieceMovementController : MonoBehaviour {
	GameManager _gameManager;
	Transform tr;
	PieceState _state;
	PieceState State {
		get { return _state; }
		set {
			switch (value) {
			case PieceState.STATIONARY:
				{
					_gameManager.IsAnimated = false;
					break;
				}
			default:
				{
					_gameManager.IsAnimated = true;
					break;
				}
			}

			_state = value;
		}
	}
		
	Vector3 coordinate;
	Queue<Vector3> path = new Queue<Vector3> ();
	Vector3 axis;
	Vector3 center;
	float b;
	float t;
	float step;
	public Vector3 Coordinate { 
		get { return coordinate; }
		set {
			var coord = GetWorldPosition (value);

			path.Enqueue (coord);
			State = PieceState.NEW_WAYPOINT;
		}
	}

	void Awake () {
		_gameManager = GameObject.FindGameObjectWithTag ("GameController").GetComponent<GameManager> ();
		tr = GetComponent<Transform> ();
		State = PieceState.STATIONARY;
	}

	void Start () {
		//step = 0.03f;
		step = 1f;
		t = 0f;
		b = 1.5f;
		//path = new Queue<Vector3> ();
	}

	void Update () {
		switch (State) {
		case PieceState.NEW_WAYPOINT:
			{
				if (path.Count == 0) {
					State = PieceState.STATIONARY;
				} else {
					coordinate = path.Dequeue ();

					if (coordinate != tr.position) {
						if (coordinate.y == tr.position.y) {
							axis = coordinate - tr.position;
							center = tr.position + axis / 2f;
							t = 0f;
							State = PieceState.MOVING_ELLIPSE;
						} else {
							State = PieceState.MOVING_STRAIGHT;
						}
					}
				}
				break;
			}
		case PieceState.MOVING_STRAIGHT:
			{
				if (tr.position != Coordinate) {
					var distToTarget = Vector3.Distance (tr.position, Coordinate);
					if (distToTarget < .05f) {
						_gameManager.PlayMoveSound ();
						tr.position = Coordinate;
						//State = PieceState.STATIONARY;
						State = PieceState.NEW_WAYPOINT;
					} else {
						tr.position = Vector3.Lerp (tr.position, Coordinate, 0.05f);
					}
				} else {
					//State = PieceState.STATIONARY;
					State = PieceState.NEW_WAYPOINT;
				}
				break;
			}
		case PieceState.MOVING_ELLIPSE:
			{
				t += step * Time.deltaTime;
				Vector3 pos = tr.position;
				pos += axis * step * Time.deltaTime;
				pos.y = center.y + b * Mathf.Sin (t * Mathf.PI);

				tr.position = pos;

				if (t >= 1f) {
					_gameManager.PlayMoveSound ();
					tr.position = coordinate;
					//State = PieceState.STATIONARY;
					State = PieceState.NEW_WAYPOINT;
				}

				break;
			}
		case PieceState.STATIONARY:
			break;
		}
	}

	static Vector3 GetWorldPosition (Vector3 coords) {
		return new Vector3 (-5.5f + coords.x, -9.75f + 10 * coords.z, -3.5f + coords.y);
	}
}
