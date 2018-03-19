using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UIElement : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler {
	public bool IsPointerOver { get; set; }

	#region IPointerEnterHandler implementation
	public void OnPointerEnter (PointerEventData eventData)
	{
		IsPointerOver = true;
		//print ("Pointer enter, id = " + eventData.pointerId);
	}
	#endregion

	#region IPointerExitHandler implementation
	public void OnPointerExit (PointerEventData eventData)
	{
		IsPointerOver = false;
		//print ("Pointer exit, id = " + eventData.pointerId);
	}
	#endregion
}
