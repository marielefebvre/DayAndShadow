using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Grid : MonoBehaviour {
	
	public GameObject tilePrefab;
	public Sprite[] tileTextures;

	public int w = 11;
	public int h = 15;
	public List<GameObject> tiles;

	private CSVReader csvReader;

	public void InitTiles () {
		csvReader = gameObject.GetComponent<CSVReader> ();
		csvReader.loadLevel (PlayerPrefs.GetInt ("level"), w, h);

		tiles = new List<GameObject> ();
		for (int y = 0; y < h; y++) {
			for (int x = 0; x < w; x++) {
				GameObject block;
				block = GameObject.Instantiate(tilePrefab) as GameObject;
				block.transform.position = new Vector3(x, y, 0);

				Tile blockTile = block.GetComponent<Tile>();
				blockTile.type = getTileType(x,y);

				if (blockTile.type == Tile.TileType.NORMAL) {
					block.GetComponent<SpriteRenderer>().sprite = tileTextures[(int)Tile.TileType.NORMAL];//NIGHT
				} else {
					block.GetComponent<SpriteRenderer>().sprite = tileTextures[(int)blockTile.type];
				}

				tiles.Add(block);
			}
		}
	}

	public void Sunrise() {
		foreach (GameObject tile in tiles) {
			if (tile.GetComponent<Tile>().type == Tile.TileType.NORMAL)
				tile.GetComponent<SpriteRenderer>().sprite = tileTextures[tileTextures.Length - 1];//the day texture is the last one, ugly...
		}
	}
	public void Sunset() {
		foreach (GameObject tile in tiles) {
			if (tile.GetComponent<Tile>().type == Tile.TileType.NORMAL)
				tile.GetComponent<SpriteRenderer>().sprite = tileTextures[(int)Tile.TileType.NORMAL];
		}
	}

	public Tile getClosestTile(Vector3 pos){
		return getTile((int)pos.x, (int)pos.y);
	}
	public bool isClosestTileOOB(Vector3 pos){
		int currX = (int)pos.x;
		int currY = (int)pos.y;
		if (currX < 0 || currX >= w
		    || currY < 0 || currY >= h)
		    return true;
		return false;
	}

	public Tile getStartTile(){
		return getTile(w / 2, 0);
	}
	public Tile getArrivalTile(){
		return getTile(w / 2, h);
	}

	private Tile getTile(int x, int y) {
		return tiles [y * w + x].GetComponent<Tile>();
	}
	private Tile.TileType getTileType(int x, int y) {
		char value = csvReader.content [(h-1-y) * w + x];
		if (value < '0' || value > '3')
			Debug.LogWarning("WRONG TILE TYPE : " + value);
		return (Tile.TileType)(value - 48);
	}

}
