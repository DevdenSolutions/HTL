using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragGO : MonoBehaviour
{
	private Vector3 screenPoint;
	private Vector3 offset;

	void OnMouseDown() // when the mouse is pressed
	{
		screenPoint = Camera.main.WorldToScreenPoint(gameObject.transform.position); // gets the center of the screen
		offset = gameObject.transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z)); // offsets that point by mouse position
	}

	void OnMouseDrag() // when the mouse is dragged
	{
		Vector3 cursorPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z); // gets the new position of the mouse
		Vector3 cursorPosition = Camera.main.ScreenToWorldPoint(cursorPoint) + offset; // makes the "cursorPosition" variable the mouse position
		transform.position = cursorPosition; // sets the position of the object to the "cursorPosition" variable
	}
}
