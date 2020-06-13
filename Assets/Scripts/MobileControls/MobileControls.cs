using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MobileControls : MonoBehaviour {

	public void OnBeginDrag(PointerEventData data) {
		Debug.Log("Drag" + data.position);
	}
}
