using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;

public class FocusMouse : MonoBehaviour {

	void Update () {
		if (EventSystem.current.currentSelectedGameObject == null)
		{
			Debug.Log("Reselecting first input");
			EventSystem.current.SetSelectedGameObject(EventSystem.current.firstSelectedGameObject);
		}
	}
}
