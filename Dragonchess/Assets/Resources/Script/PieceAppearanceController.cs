using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PieceAppearanceController : MonoBehaviour {
	static Material blackPieceMaterial = null;
	static Material whitePieceMaterial = null;

	void Awake () {
		if (blackPieceMaterial == null)
			blackPieceMaterial = Resources.Load ("Materials/Piece/BlackPieceMaterial") as Material;

		if (whitePieceMaterial == null)
			whitePieceMaterial = Resources.Load ("Materials/Piece/WhitePieceMaterial") as Material;
	}
	
	public void Init (Color color) {
		if (color == Color.NONE)
			throw new UnassignedReferenceException ("Appearance controller is being initiated with an absent color");

		if (color == Color.BLACK) {
			var tr = GetComponent<Transform> ();
			tr.Rotate (Vector3.up, 180);
		}
		
		var mat = color == Color.BLACK ? blackPieceMaterial : whitePieceMaterial;

		var mr = GetComponent<MeshRenderer> ();
		if (mr != null)
			mr.material = mat;

		foreach (var childmr in GetComponentsInChildren<MeshRenderer>()) {
			childmr.material = mat;
		}
	}
}
