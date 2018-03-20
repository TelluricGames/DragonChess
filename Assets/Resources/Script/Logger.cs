using System;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEngine;

enum LoggerState {
	IDLE,
	PREPARED
}

public struct PieceLog {
	public Color color;
	public Type type;
	public Vector3 position;
}

public class Logger {
	public ObservableCollection<string> Log { get; private set; }
	public List<PieceLog> CapturedPieces { get; private set; }

	LoggerState _state;
	Vector3 _startPosition;
	Piece _movingPiece;
	Vector3 _endPosition;
	Piece _capturedPiece;
	int _counter;

	static Dictionary<Type, string> pieceCodes;

	static Logger () {
		pieceCodes = new Dictionary<Type, string> ();

		pieceCodes.Add (Type.GetType("Sylph"), "S");
		pieceCodes.Add (Type.GetType("Griffon"), "G");
		pieceCodes.Add (Type.GetType("Dragon"), "R");
		pieceCodes.Add (Type.GetType("Warrior"), "W");
		pieceCodes.Add (Type.GetType("Oliphant"), "O");
		pieceCodes.Add (Type.GetType("Unicorn"), "U");
		pieceCodes.Add (Type.GetType("Hero"), "H");
		pieceCodes.Add (Type.GetType("Thief"), "T");
		pieceCodes.Add (Type.GetType("Cleric"), "C");
		pieceCodes.Add (Type.GetType("Mage"), "M");
		pieceCodes.Add (Type.GetType("King"), "K");
		pieceCodes.Add (Type.GetType("Paladin"), "P");
		pieceCodes.Add (Type.GetType("Dwarf"), "D");
		pieceCodes.Add (Type.GetType("Basilisk"), "B");
		pieceCodes.Add (Type.GetType("Elemental"), "E");
	}

	public Logger () {
		Log = new ObservableCollection<string> ();
		CapturedPieces = new List<PieceLog> ();
		_counter = 0;

		Clear ();
	}

	public void PrepareMove (Vector3 start, Vector3 end, Board[] boards) {
		if (_state != LoggerState.IDLE)
			throw new DragonChessException ("Logger is in an illegal state");

		_startPosition = start;
		_movingPiece = boards [(int)start.z] [(int)start.x, (int)start.y].Piece.GetComponent<Piece> ();
		_endPosition = end;
		var capturedPieceObj = boards [(int)end.z] [(int)end.x, (int)end.y].Piece;
		if (capturedPieceObj != null)
			_capturedPiece = capturedPieceObj.GetComponent<Piece> ();

		_state = LoggerState.PREPARED;
	}

	public void LogMove (bool isCheck, bool isCheckMate) {
		if (_state != LoggerState.PREPARED)
			throw new DragonChessException ("Logger is in an illegal state");

		_counter++;
		StringBuilder sb = new StringBuilder ();

		sb.Append (_counter).Append (". ");

		var movingPieceType = _movingPiece.GetType ();

		sb.Append (pieceCodes [movingPieceType]);

		if (_endPosition.z != 1 || movingPieceType != Type.GetType("Dragon")) {
			sb.Append (GetPositionNotation (_startPosition));
		}

		if (_capturedPiece == null) {
			sb.Append ("-");
		} else {
			sb.Append ("x");
			sb.Append (pieceCodes [_capturedPiece.GetType ()]);
			LogCapture ();
		}
		sb.Append (GetPositionNotation (_endPosition));

		if (movingPieceType == Type.GetType("Warrior") && _endPosition.y == 7) {
			sb.Append ("(H)");
		}

		if (isCheckMate) {
			sb.Append (" mate");
		} else if (isCheck) {
			sb.Append (" ch");
		}

		Log.Add (sb.ToString ());
		Clear ();
		_state = LoggerState.IDLE;
	}

	void LogCapture () {
		CapturedPieces.Add (ConstructPieceLog (_capturedPiece));
	}

	PieceLog ConstructPieceLog (Piece piece) {
		PieceLog res;
		res.color = piece.Color;
		res.position = piece.Coordinate;
		res.type = piece.GetType ();

		return res;
	}


	public void Clear () {
		_startPosition = -Vector3.one;
		_movingPiece = null;
		_endPosition = -Vector3.one;
		_capturedPiece = null;

		_state = LoggerState.IDLE;
	}

	public ObservableCollection<string> GetLog () {
		return Log;
	}

	static string GetPositionNotation (Vector3 pos) {
		StringBuilder sb = new StringBuilder ();
		sb.Append ((int)(pos.z + 1));
		sb.Append ((char)(pos.x + 'a'));
		sb.Append ((int)(pos.y + 1));

		return sb.ToString ();
	}
}
