using UnityEngine;
using System.Collections;

public class ReturnMenu : MonoBehaviour {

	public void Return() {
		Application.LoadLevel (0);
	}

	void Start() {
		Cursor.lockState = CursorLockMode.Locked;
		Cursor.visible = false;
	}

	void Update(){
		if (Input.GetButtonDown ("Quit"))
			Return ();
	}
}
