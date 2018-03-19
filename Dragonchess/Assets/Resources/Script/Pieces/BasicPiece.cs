using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections.Generic;

public abstract class BasicPiece : MonoBehaviour, Piece, IPointerClickHandler
{
	private Color c;
	public Color Color {
		get { 
			if (c == Color.NONE)
				throw new UnassignedReferenceException ("Piece's color is accessed before it's properly assigned");
			return c;
			}
		set {
			c = value;

			if (value != Color.NONE) {
				var pac = GetComponent<PieceAppearanceController> ();
				if (pac != null)
					pac.Init (c);
			}
		}
	}

	protected Move move;
	protected Move capture;

	public GameManager GameManager { get; set; }

	Vector3 coordinate;
	public Vector3 Coordinate { 
		get { return coordinate; }
		set {
			coordinate = value;
			//Move ();
		}
	}

	public BasicPiece() {
		Color = Color.NONE;
	}

	public abstract void Init (Color color);

	protected void addBasicConstraints() {
		move.AddConstraint (new BlockConstraint(),new CheckConstraint());
		capture.AddConstraint (new BlockAllyAndVoidCapture(), new CheckConstraint ());
	}

	public string GetName ()
	{
		return this.GetType().Name;
	}
	public virtual List<Vector3> GetAvailableMoves (Vector3 startPosition, Board[] boards)
	{
		return move.GetMovesFrom (startPosition, boards);
	}
	public virtual List<Vector3> GetAvailableCaptures (Vector3 startPosition, Board[] boards)
	{
		return capture.GetMovesFrom (startPosition, boards);
	}

	public Move GetFormedCapture ()
	{
		return capture;
	}

	public Move GetFormedMove ()
	{
		return move;
	}

	void Awake () {
		if (GameManager == null) {
			GameManager = GameObject.FindGameObjectWithTag ("GameController").GetComponent<GameManager> ();;
		}
	}

	#region IPointerClickHandler implementation

	public void OnPointerClick (PointerEventData eventData)
	{
		var player = GameManager.ActivePlayer;

		if (player != null && player.PlayerType == PlayerType.HUMAN) {
			var hp = player as HumanPlayer;
			hp.CellClicked (Coordinate, this);
		}
	}

	#endregion

	public void Move () {
		GetComponent<PieceMovementController> ().Coordinate = Coordinate;
	}

	public void Destroy () {
		gameObject.SetActive (false);
	}
}

