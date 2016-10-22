using UnityEngine;
using System.Collections;

public class Tile : MonoBehaviour {

	public enum TileType
	{
		NORMAL,
		COVERED,
		START,
		ARRIVAL,
	}
	public TileType type = TileType.NORMAL;
}
