using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.IO;

public class CSVReader : MonoBehaviour {

	public string csvFilePath;
	public static string level2 =
		@"0,0,0,0,0,3,0,0,0,0,0
0,0,0,0,0,0,0,0,1,0,0
0,0,1,0,0,0,0,0,0,0,0
0,0,0,0,0,0,0,0,0,0,0
0,0,0,0,0,0,0,1,0,0,0
0,0,0,0,0,0,0,0,0,0,0
0,0,0,0,0,0,0,0,0,0,1
0,0,0,0,0,0,0,0,0,0,0
0,0,0,0,0,0,0,0,0,0,0
0,0,0,0,0,1,0,0,0,0,0
0,0,0,0,0,0,0,0,0,1,0
0,0,1,0,0,0,0,0,0,0,0
0,0,0,0,0,0,0,0,0,0,0
0,0,0,0,0,0,0,0,0,0,0
0,0,0,0,0,2,0,0,0,0,0";
	public static string level1 =
		@"0,0,0,0,0,3,0,0,0,0,0
0,0,0,0,0,0,0,0,1,0,0
0,0,1,0,0,0,0,0,0,0,0
0,0,0,0,0,0,0,0,0,0,0
0,1,0,0,0,0,0,1,0,0,0
0,0,0,0,0,0,0,0,0,0,0
0,0,0,0,0,0,0,0,0,0,1
0,0,1,0,0,0,0,0,0,0,0
0,0,0,0,0,0,0,0,0,0,0
0,1,0,0,0,1,0,0,0,0,0
0,0,0,0,0,0,0,0,0,1,0
0,0,1,0,0,0,0,0,0,0,0
0,0,0,0,0,0,0,0,0,0,0
0,0,0,0,0,0,0,0,0,0,0
0,0,0,0,0,2,0,0,0,0,0";
	public static string level3 =
		@"0,0,0,0,0,3,0,0,0,0,0
0,0,0,0,0,0,0,0,0,0,0
0,0,1,0,0,0,0,0,0,0,0
0,0,0,0,0,0,0,0,0,0,0
0,0,0,0,0,0,0,1,0,0,0
0,0,0,0,1,0,0,0,0,0,0
0,0,0,0,0,0,0,1,0,0,1
0,0,0,0,0,1,0,0,0,0,0
0,0,0,0,0,0,0,0,0,0,0
0,0,0,0,0,1,0,0,0,0,0
0,0,0,0,0,0,0,0,0,0,0
0,0,1,0,0,0,0,0,1,0,0
0,0,0,0,0,0,0,0,0,0,0
0,0,0,0,0,0,0,0,0,0,0
0,0,0,0,0,2,0,0,0,0,0";

	public string csvFile; // Reference of CSV file
	public string contentArea; // Reference of contentArea where records are displayed
	public string content; // Reference of contentArea where records are displayed

	private char rowSeperater = '\n'; // It defines line seperate character
	private char tileSeperator = ','; // It defines field seperate chracter

	public void loadLevel (int i, int w, int h) {//the size are used to check
		//csvFilePath = Application.dataPath + "/CSVs/level";
		//readFile (csvFilePath + i + ".csv");
		switch (i) {
		case 1 :
			csvFile = level1;
			break;
		case 2 :
			csvFile = level2;
			break;
		case 3 :
			csvFile = level3;
			break;
		default :
			Debug.LogWarning("Unknown level");
			csvFile = level1;
			break;
		}
		extractData (w, h);
	}

	private void readFile(string path) {
		csvFile = File.ReadAllText (path);
	}

	private void extractData(int w, int h)
	{
		contentArea = "";
		content = "";

		string[] rows = csvFile.Split (rowSeperater);
		if (rows.Length != h)
			Debug.LogWarning("WRONG ROW COUNT");
		foreach (string row in rows)
		{
			string[] tiles = row.Split(tileSeperator);
			if (tiles.Length != w)
				Debug.LogWarning("WRONG TILE COUNT : " + tiles.Length);
			foreach(string tile in tiles)
			{
				contentArea += tile + "\t";
				content += tile[0];
			}
			contentArea += '\n';
		}
	}
}
