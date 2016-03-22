using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System.IO;

public class BrickLevelGenerator : MonoBehaviour {

	public Dropdown loadOptionDropDown;
	public GameObject[] Bricks;
	public Transform startPosition;
	public int rows = 16;
	public int columns = 18;

	float offsetx;
	float offsety;
	Vector3 position;
	BoxCollider2D brickCollider;
	GameObject[] bricks;
	public static int numberOfBricks = 0;

	List<BrickXml> loadedBrickXml = new List<BrickXml>();
	public List<BrickXml> brickListXml = new List<BrickXml> ();

	List<GameObject> BrickList = new List<GameObject>();

	// Use this for initialization
	void Start () {
		
		SavedMapFilesDirectory ();

	}

	void Update()
	{
/*		if (numberOfBricks == 0) {
			Debug.Log ("All Bricks Destroyed");
		}*/
	}

	public void ResetMap()
	{
		bricks = Bricks;
//		brickCollider = bricks [0].GetComponent<BoxCollider2D> ();
		//BrickList.Clear();
		foreach (GameObject ite in BrickList) 
		{
			Destroy (ite.gameObject);
		}
		BrickList.Clear ();

		for (int i = 0; i < loadedBrickXml.Count; i++) 
		{
			int num = loadedBrickXml [i].brickNumber;

			if (num != 0) {
				float posx = loadedBrickXml [i].brickPosX;
				float posy = loadedBrickXml [i].brickPosY;

				BrickList.Add(Instantiate (bricks [num], new Vector3 (posx, posy, 0f), Quaternion.identity) as GameObject);
			} 
			else 
			{
				
			}
		}
	}

	public void LoadMap()
	{
		string path;

		path = loadOptionDropDown.captionText.text;
		//Debug.Log (loadOptionDropDown.captionText.text);
		
		loadedBrickXml = XMLObjectSerializer.Deserialize<List<BrickXml>>(Application.dataPath + "/XmlMaps/" + path);

		ResetMap ();
	}

	public void SavedMapFilesDirectory()
	{		
		DirectoryInfo pathFolder = new DirectoryInfo (Application.dataPath + "/XmlMaps/");
		FileInfo[] fileInfo = pathFolder.GetFiles ("*.xml", SearchOption.AllDirectories);
		loadOptionDropDown.GetComponent<Dropdown> ().options.Clear ();

		foreach (FileInfo file in fileInfo) 
		{
			loadOptionDropDown.options.Add (new Dropdown.OptionData (file.Name));
		}

		int tempValue = loadOptionDropDown.value;
		loadOptionDropDown.value = loadOptionDropDown.value + 1;
		loadOptionDropDown.value = tempValue;

	}

}
