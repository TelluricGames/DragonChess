using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomAIPlayer : Player {
	#region implemented abstract members of Player
	public override void Init (Color color, Board[] boards, GameManager gameManager)
	{
		PlayerType = PlayerType.AI;
		Color = color;
		_boards = boards;
		_gameManager = gameManager;
	}
	
	public override void DoTurn () {
		var piecesPosList = PieceFinder.findAllPieces (Color, _boards);
		//First three tries working like this: RANDOM(select piece)->RANDOM(select move)
		for (int i = 0; i < 3; i++) {
			int selectedPiece = Random.Range (0, piecesPosList.Count);
			var turns = new List<Vector3> ();
			var piece = GetPiece (piecesPosList [selectedPiece]);
			var startPos = piecesPosList [selectedPiece];
			turns.AddRange (piece.GetAvailableMoves (startPos, _boards));
			turns.AddRange (piece.GetAvailableCaptures (startPos, _boards));
			if (turns.Count != 0) {
				int selectedEndPoint = Random.Range (0, turns.Count);
				_gameManager.DoTurn (ConstructMove (piecesPosList [selectedPiece], turns [selectedEndPoint]));
				return;
			}
		}


		//Fallback to the RANDOM(get all pathes)
		var allTurns = new Dictionary<Vector3, List<Vector3>>(piecesPosList.Count);
		int totalCount = 0;
		for (int i = 0; i < piecesPosList.Count; i++) {
			allTurns [piecesPosList [i]] = new List<Vector3> ();
			var piece = GetPiece (piecesPosList [i]);
			var startPos = piecesPosList [i];
			allTurns [piecesPosList [i]].AddRange (piece.GetAvailableMoves (startPos, _boards));
			allTurns [piecesPosList [i]].AddRange (piece.GetAvailableCaptures (startPos, _boards));
			totalCount += allTurns[piecesPosList [i]].Count;
		}

		int skipped = 0;
		int selectedMove = Random.Range (0, totalCount);
		for (int i = 0; i < piecesPosList.Count; i++) {
			if (skipped + allTurns[piecesPosList [i]].Count > selectedMove) {
				_gameManager.DoTurn (ConstructMove (piecesPosList [i], allTurns[piecesPosList[i]][selectedMove-skipped]));
				return;
			}
			skipped += allTurns [piecesPosList [i]].Count;
		}

		throw new DragonChessException ("Can't choose the turn. Algorithm's problem");
	}
	#endregion

	PlayerMove ConstructMove (Vector3 start, Vector3 end) {
		PlayerMove move;
		move.start = start;
		move.end = end;

		return move;
	}

	private Piece GetPiece(Vector3 pos) {
		return _boards [(int)pos.z] [(int)pos.x, (int)pos.y].Piece.GetComponent<Piece> ();
	}
}
