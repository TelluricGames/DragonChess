using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board : MonoBehaviour {
	static Dictionary<string, GameObject> piecesPrefabs;
	static Vector3 offset = new Vector3 (-5.5f, 0.25f, -3.5f);

	private Cell[,] cells;
	public Cell this[int i, int j] {
		get { return cells [i, j]; }
		set { cells [i, j] = value; }
	}

	void Awake() {
		piecesPrefabs = new Dictionary<string, GameObject> ();

		string[] names = new string[] {
			"Sylph",
			"Griffon",
			"Dragon",
			"Warrior",
			"Oliphant",
			"Unicorn",
			"Hero",
			"Thief",
			"Cleric",
			"Mage",
			"King",
			"Paladin",
			"Dwarf",
			"Basilisk",
			"Elemental"
		};

		foreach (string name in names) {
			piecesPrefabs.Add(name, Resources.Load ("Prefabs/Pieces/" + name) as GameObject);
		}
	}

	public void Init (int level) {
		cells = new Cell[12, 8];
		CreateCells(level);
		Populate (level);
	}

	void CreateCells (int level) {
		var cellPrefab = Resources.Load ("Prefabs/Cell") as GameObject;

		var tr = GetComponent<Transform> ();

		for (int i = 0; i < 12; i++) {
			for (int j = 0; j < 8; j++) {
				bool isWhite;
				if ((i + j) % 2 == 0)
					isWhite = true;
				else
					isWhite = false;
				
				GameObject cellObj = Object.Instantiate (cellPrefab, tr.position + offset + new Vector3 (i, 0f, j), Quaternion.identity);
				cellObj.GetComponent<CellAppearanceController> ().Init (isWhite);

				var cell = cellObj.GetComponent<Cell>();
				cell.Coordinates = new Vector3 (i, j, level);
				cells [i, j] = cell;
			}
		}
	}

	void Populate(int level) {
		switch (level) {
		case 0:
			PopulateLower ();
			break;
		case 1:
			PopulateMiddle ();
			break;
		case 2:
			PopulateUpper ();
			break;
		}
	}

	void PopulateUpper() {
		foreach (var color in new Color[] {Color.WHITE, Color.BLACK}) {
			for (int i = 0; i < 12; i += 2)
				PlacePiece ("Sylph", i, 1, 2, color);
			PlacePiece ("Griffon", 2, 0, 2, color);
			PlacePiece ("Griffon", 10, 0, 2, color);
			PlacePiece ("Dragon", 6, 0, 2, color);
		}
	}

	void PopulateMiddle() {
		foreach (var color in new Color[] {Color.WHITE, Color.BLACK}) {
			for (int i = 0; i < 12; i++)
				PlacePiece ("Warrior", i, 1, 1, color);

			PlacePiece ("Oliphant", 0, 0, 1, color);
			PlacePiece ("Oliphant", 11, 0, 1, color);

			PlacePiece ("Unicorn", 1, 0, 1, color);
			PlacePiece ("Unicorn", 10, 0, 1, color);

			PlacePiece ("Hero", 2, 0, 1, color);
			PlacePiece ("Hero", 9, 0, 1, color);

			PlacePiece ("Thief", 3, 0, 1, color);
			PlacePiece ("Thief", 8, 0, 1, color);

			PlacePiece ("Cleric", 4, 0, 1, color);
			PlacePiece ("Mage", 5, 0, 1, color);
			PlacePiece ("King", 6, 0, 1, color);
			PlacePiece ("Paladin", 7, 0, 1, color);
		}
	}

	void PopulateLower() {
		foreach (var color in new Color[] {Color.WHITE, Color.BLACK}) {
			for (int i = 1; i < 12; i += 2)
				PlacePiece ("Dwarf", i, 1, 0, color);
			PlacePiece ("Basilisk", 2, 0, 0, color);
			PlacePiece ("Basilisk", 10, 0, 0, color);
			PlacePiece ("Elemental", 6, 0, 0, color);
		}
	}

	public void PlacePiece (string name, int i, int j, int z, Color color) {
		var pieceObj = Object.Instantiate (piecesPrefabs [name], GetPiecePositionInSpace (i, j, color), Quaternion.identity);
		var piece = pieceObj.GetComponent<Piece> ();
		piece.Init (color);

		var coords = GetPiecePositionInGame (i, j, z, color);
		piece.Coordinate = coords;
		cells [(int) coords.x, (int) coords.y].Piece = pieceObj;
	}

	Vector3 GetPiecePositionInSpace (int i, int j, Color color) {
		var tr = GetComponent<Transform> ();
		if (color == Color.WHITE)
			return tr.position + offset + new Vector3 (i, 0f, j);
		if (color == Color.BLACK)
			return tr.position + offset + new Vector3 (i, 0f, 7f - j);
		throw new UnassignedReferenceException ("By no means NONE color can be here");
	}

	Vector3 GetPiecePositionInGame (int i, int j, int z, Color color) {
		if (color == Color.WHITE)
			return new Vector3 (i, j, z);
		if (color == Color.BLACK)
			return new Vector3 (i, 7 - j, z);
		throw new UnassignedReferenceException ("By no means NONE color can be here");
	}

	public int GetLength() {
		return GetDimensionSize (0);
	}

	public int GetHeight() {
		return GetDimensionSize (1);
	}

	int GetDimensionSize(int dimension) {
		if (cells != null) {
			return cells.GetLength (dimension);
		} else {
			throw new UnassignedReferenceException ("Cells weren't created");
		}
	}

	public void Clear() {
		foreach (var cell in cells) {
			cell.HighlightClear ();
		}
	}

	public void Destroy() {
		foreach (var cell in cells) {
			cell.Destroy ();
		}

		GameObject.Destroy (gameObject);
	}
}
