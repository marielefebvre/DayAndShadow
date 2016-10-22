using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

	public float playerSpeed = 1;

	public enum Direction
	{
		UP,
		DOWN,
		LEFT,
		RIGHT,
		DIRECTION_COUNT
	}
	public Direction currentDirection;

	public GameManager gameManager;

	private Vector2 playerDirection;
	private bool isResting = false;

	void Start () {
		currentDirection = Direction.UP;
		playerDirection = new Vector2(0, 1);
	}
	
	public void OnTurn () {
		Move ();
	}

	void Move() {
		if (isResting) {
			Debug.Log ("REST");
			return;
		}
		switch (currentDirection) {
		case Direction.UP:
			playerDirection.x = 0;
			playerDirection.y = 1;
			break;
		case Direction.DOWN:
			playerDirection.x = 0;
			playerDirection.y = -1;
			break;
		case Direction.LEFT:
			playerDirection.x = -1;
			playerDirection.y = 0;
			break;
		case Direction.RIGHT:
			playerDirection.x = 1;
			playerDirection.y = 0;
			break;
		default:
			Debug.LogWarning ("direction wtf");
			break;
		}
		transform.Translate (new Vector3 (
			playerDirection.x,
			playerDirection.y,
			0));

		if (gameManager.grid.isClosestTileOOB (transform.position)) {
			transform.Translate (new Vector3 (
				-playerDirection.x,
				-playerDirection.y,
				0));
			switch (currentDirection) {
			case Direction.UP:
			case Direction.LEFT:
				currentDirection++;
				break;
			case Direction.DOWN:
			case Direction.RIGHT:
				currentDirection--;
				break;
			default:
				Debug.LogWarning ("direction wtf");
				Rest ();
				break;
			}
		}
	}

	public void Rest() {
		if (isResting)
			return;

		isResting = true;
		currentDirection = Direction.UP;//too frustrating otherwise
	}
	public void StopResting() {
		isResting = false;
	}
	public bool IsResting() {
		return isResting;
	}

	public void SnapToTile(GameObject tile) {
		transform.position = tile.transform.position;
		transform.Translate(0,0,-1);
	}

	public Vector3 getPlayerPosition() {
		return transform.position;
	}
}
