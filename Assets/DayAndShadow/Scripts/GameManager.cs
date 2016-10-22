using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {
	
	public enum GameState
	{
		PLAY,
		VICTORY,
		DEATH,
		MENU
	}

	public GameState gameState = GameState.PLAY;
	public Grid grid;

	public PlayerController player;
	public Camera mainCamera;

	public int sunriseHour = 12;
	public int sunsetHour = 15;
	public float clockSpeed = 0.5f;//number of seconds by turn

	public Text clockDisplay;
	public GameObject endMenu;
	public int score;

	public AudioClip ingameClip;
	public AudioClip menuClip;
	private int alreadyIngame;//true 1, false 0
	private GameObject audioObj;

	private Tile playerTile;

	private int clock = 0;
	private bool shouldRest = true;

	void Start () {
		alreadyIngame = PlayerPrefs.GetInt ("alreadyIngame");
		audioObj = GameObject.Find ("Audio");
		if (alreadyIngame == 0) {
			audioObj.GetComponent<AudioSource> ().clip = ingameClip;
			audioObj.GetComponent<AudioSource> ().Play ();
			alreadyIngame = 1;
			PlayerPrefs.SetInt ("alreadyIngame", alreadyIngame);
		}

		Cursor.lockState = CursorLockMode.Locked;
		Cursor.visible = false;
		mainCamera.transform.position = new Vector3 (grid.w * 2 / 3, grid.h / 2, -10);

		grid.InitTiles ();
		score = 0;
		playerTile = grid.getStartTile ();

		player.gameManager = this;
		player.SnapToTile (playerTile.gameObject);

		HudPrint ();

		StartCoroutine("OnTurn");
	}
	public void ReturnMenu(){
		alreadyIngame = 0;
		PlayerPrefs.SetInt ("alreadyIngame", alreadyIngame);
		audioObj.GetComponent<AudioSource> ().clip = menuClip;
		audioObj.GetComponent<AudioSource> ().Play ();
		Application.LoadLevel ("Menu");
	}

	void Update () {
		if (Input.GetButtonDown ("Quit")) {
			ReturnMenu();
		}

		if (Input.GetAxis ("Vertical") > 0) {
			player.currentDirection = PlayerController.Direction.UP;
		} else if (Input.GetAxis ("Vertical") < 0) {
			player.currentDirection = PlayerController.Direction.DOWN;
		} else if (Input.GetAxis ("Horizontal") < 0) {
			player.currentDirection = PlayerController.Direction.LEFT;
		} else if (Input.GetAxis ("Horizontal") > 0) {
			player.currentDirection = PlayerController.Direction.RIGHT;
		}
	}

	void HudPrint() {
		clockDisplay.text = "Sunrise : " + sunriseHour + "\n";
		clockDisplay.text += "Sunset : " + sunsetHour + "\n";
		clockDisplay.text += "\nHour : " + clock + "\n";
		clockDisplay.text += "Direction :\n   " + player.currentDirection.ToString () + "\n";
		if (player.IsResting())
			clockDisplay.text += "\nZZZzzzz....\n";
	}


	IEnumerator OnTurn() {
		for (;;) {
			playerTile = grid.getClosestTile(player.getPlayerPosition());
			CheckRest();
			CheckWin();

			if (clock == sunriseHour)
				Sunrise();
			if (clock == sunsetHour)
				Sunset();

			player.OnTurn ();

			clock++;
			score++;

			HudPrint();
			
			yield return new WaitForSeconds (clockSpeed);
		}
	}

	void CheckRest() {
		if ((shouldRest && playerTile.type == Tile.TileType.COVERED)
		    || playerTile.type == Tile.TileType.ARRIVAL) {
			print ("rest");
			player.Rest();
		}
	}
	void CheckWin() {
		if (playerTile.type == Tile.TileType.ARRIVAL) {
			gameState = GameState.VICTORY;
			StopCoroutine("OnTurn");
			endMenu.SetActive(true);
		}
	}

	void Sunrise() {
		Debug.Log("Sunrise");

		grid.Sunrise ();
		player.Rest ();

		if (playerTile.type == Tile.TileType.NORMAL) {
			gameState = GameState.DEATH;
			StopCoroutine("OnTurn");
			endMenu.SetActive(true);
		}
	}
	void Sunset() {
		Debug.Log("Sunset");
		
		grid.Sunset ();
		player.StopResting ();

		clock = 0;
	}

}
