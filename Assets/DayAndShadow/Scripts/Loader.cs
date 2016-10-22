using UnityEngine;
using System.Collections;

public class Loader : MonoBehaviour {

	public void LoadScene(int level) {
		PlayerPrefs.SetInt ("level", level);
		Application.LoadLevel(1);
	}
	public void LoadMenu(int menu) {
		Application.LoadLevel (menu);
	}
	public void Quit() {
		Application.Quit ();
	}

	void Start() {
		Cursor.lockState = CursorLockMode.Locked;
		Cursor.visible = false;
	}

	void Update(){
		if (Input.GetButtonDown ("Quit"))
			Quit ();
	}
}
