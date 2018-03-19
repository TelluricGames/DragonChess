using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HumanPlayer : Player {

	Vector3? _selectedCell;
	PlayerMove? _move;

	#region implemented abstract members of Player
	public override void Init (Color color, Board[] boards, GameManager gameManager)
	{
		PlayerType = PlayerType.HUMAN;
		Color = color;
		_boards = boards;
		_gameManager = gameManager;
		_selectedCell = null;
		_move = null;
	}
	#endregion

	public void CellClicked (Vector3 coords, Piece piece) {
		if (_gameManager.State != GameManagerState.PLAYING)
			return;

		if (_selectedCell.HasValue) {
			var sv = _selectedCell.Value;
			var scell = _boards [(int)sv.z] [(int)sv.x, (int)sv.y];
			var sp = scell.Piece.GetComponent<Piece> ();
			var cc = _boards [(int)coords.z] [(int)coords.x, (int)coords.y];
			ClearBoards ();

			var am = sp.GetAvailableMoves (sv, _boards);
			var ac = sp.GetAvailableCaptures (sv, _boards);
			if (am.Contains (coords) || ac.Contains (coords)) {
				PlayerMove pm;
				pm.start = sv;
				pm.end = coords;
				_move = new PlayerMove? (pm);
			} else {
				if (coords != sv && piece != null) {
					var sc = sp.Color;
					if (piece.Color == sc) {					
						CellClicked (coords, piece);
					}
				}
			}
		} else {
			var cell = _boards [(int)coords.z] [(int)coords.x, (int)coords.y];
			if (!cell.IsEmpty && cell.Piece.GetComponent<Piece> ().Color == Color) {
				_selectedCell = coords;

				var am = piece.GetAvailableMoves (coords, _boards);
				var ac = piece.GetAvailableCaptures (coords, _boards);

				_boards [(int)coords.z] [(int)coords.x, (int)coords.y].HighlightSelect ();

				foreach (var move in am) {
					_boards [(int)move.z] [(int)move.x, (int)move.y].HighlightMove ();
				}

				foreach (var capture in ac) {
					_boards [(int)capture.z] [(int)capture.x, (int)capture.y].HighlightCapture ();
				}
			}
		}
	}

	void ClearBoards() {
		_selectedCell = new Vector3? ();

		foreach (var board in _boards) {
			board.Clear ();
		}
	}

	#region implemented abstract members of Player
	public override void DoTurn ()
	{
		StartCoroutine (WaitForItCoroutine());
	}
	#endregion

	IEnumerator WaitForItCoroutine () {
		while (!_move.HasValue)
			yield return null;

		_gameManager.DoTurn (_move.Value);
		_move = new PlayerMove? ();
	}
}
