using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;
using System.IO;

public class BrickMapEditor : MonoBehaviour {

	public List<BrickXml> brickListXml = new List<BrickXml> ();

	public InputField inputSaveText;
	public Dropdown loadOptionDropDown;

	public GameObject[] Bricks;
	public GameObject defaultBrick;
	public Transform initialPosition;
	public Transform brickLegendPosition;
	int rows = 16;
	int columns = 18;
	float offSetX;
	float offSetY;
	Vector3 position;
	Vector3 legendPosition;
	BoxCollider2D brickCollider;
	SpriteRenderer sr;
	SpriteRenderer srBase;
	int numberSelected;

	GameObject[] bricks;
	List<GameObject> defaultBricksMap = new List<GameObject>();

	// Use this for initialization
	void Start () {

		SavedMapFilesDirectory ();
		ResetMap ();

	}
	
	// Update is called once per frame
	void Update () {
		
		BrickSelect ();

		if (Input.GetMouseButtonDown (0)) {		
			//
			//Debug.Log ("Pressed Mouse Button, Casting Ray");
			CastRay ();
		}
	}

	// compare the defaultBrickMap position with XmlBrick positions and change the value of brickNumber in the XmlFile
	// defaultBrickPosistion is passed in from the rayCast after a hit occurs
	public void AdjustBrickSetting(Transform position)
	{
		Transform location = position;
		float posX;
		float posY;
		posX = location.position.x;
		posY = location.position.y;

		for (int i = 0; i < brickListXml.Count; i++) 
		{
			if (brickListXml [i].brickPosX == posX && brickListXml [i].brickPosY == posY) {
				brickListXml [i].brickNumber = numberSelected;
				//Debug.Log ("XML Data: " + brickListXml [i].brickNumber + " Pos: " + brickListXml[i].brickPosX + " " + brickListXml[i].brickPosY + "\n"
				//	+ "Default Data: " + hit.collider.transform.position.x + " " + hit.collider.transform.position.y);
			} 
			else 
			{
			}
		}
	}

	public RaycastHit2D hit;

	void CastRay()
	{
		Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
		hit = Physics2D.Raycast(ray.origin, ray.direction, Mathf.Infinity);

		if (hit) {
		
			//Debug.Log (ray.origin, hit.point);
			//Debug.Log ("Hit Object: " + hit.collider.gameObject.name + " Position: " + hit.collider.gameObject.transform.position);
			srBase = hit.collider.gameObject.GetComponent<SpriteRenderer> ();
			sr = bricks [numberSelected].GetComponent<SpriteRenderer> ();
			AdjustBrickSetting (hit.collider.transform);
			srBase.sprite = sr.sprite;
		}
	}

	public void BrickSelect()
	{
		if (Input.GetKeyUp ("1")) {
			numberSelected = 0;
			Debug.Log ("" + numberSelected + " Default");
		}
		if (Input.GetKeyUp ("2")) {
			numberSelected = 1;
			Debug.Log ("" + numberSelected + " red");
		}
		if (Input.GetKeyUp ("3")) {
			numberSelected = 2;
			Debug.Log ("" + numberSelected + " green");
		}
		if (Input.GetKeyUp ("4")) {
			numberSelected = 3;
			Debug.Log ("" + numberSelected + " blue");
		}
		if (Input.GetKeyUp ("5")) {
			numberSelected = 4;
			Debug.Log ("" + numberSelected + " darkPink");
		}
		if (Input.GetKeyUp ("6")) {
			numberSelected = 5;
			Debug.Log ("" + numberSelected + " lightBlue");
		}
		if (Input.GetKeyUp ("7")) {
			numberSelected = 6;
			Debug.Log ("" + numberSelected + " yellow");
		}
		if (Input.GetKeyUp ("8")) {
			numberSelected = 7;
			Debug.Log ("" + numberSelected + " pink");
		}
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

	}

	public void SaveMap()
	{
		if (!string.IsNullOrEmpty(inputSaveText.text)) {
			string path = inputSaveText.text;
			XMLObjectSerializer.Serialize (brickListXml, Application.dataPath + "/XmlMaps/" + path + ".xml");
			Debug.Log ("File Saved as " + inputSaveText.text);
			inputSaveText.text = "";
			SavedMapFilesDirectory ();
		} 
		else 
		{
			Debug.Log ("Name to save to is missing!");
		}
	}

	public void LoadMap()
	{
		SpriteRenderer defaultSR;
		SpriteRenderer XmlSR;
		string path;

		path = loadOptionDropDown.captionText.text;
		//Debug.Log (loadOptionDropDown.captionText.text);

		List<BrickXml> loadedBrickXml = new List<BrickXml>();
		loadedBrickXml = XMLObjectSerializer.Deserialize<List<BrickXml>>(Application.dataPath + "/XmlMaps/" + path);

/*		for (int i = 0; i < loadedBrickXml.Count; i++) 
		{
			Debug.Log (loadedBrickXml[i].brickNumber + " " + loadedBrickXml[i].brickPosX + " " + loadedBrickXml[i].brickPosY);
		}
*/
		for (int i = 0; i < defaultBricksMap.Count; i++) 
		{
			defaultSR = defaultBricksMap [i].GetComponent<SpriteRenderer>();

			XmlSR = bricks [loadedBrickXml [i].brickNumber].GetComponent<SpriteRenderer>();
			defaultSR.sprite = XmlSR.sprite;
		}
	}

	public void ResetMap()
	{
		position = initialPosition.position;
		legendPosition = brickLegendPosition.position;
		bricks = Bricks;
		brickCollider = bricks [0].GetComponent<BoxCollider2D> ();
		offSetX = brickCollider.size.x;
		offSetY = brickCollider.size.y;

		defaultBricksMap.Clear ();
		brickListXml.Clear ();

		for (int i = 0; i < columns; i++) {

			for (int j = 0; j < rows; j++) {

				defaultBricksMap.Add(Instantiate (defaultBrick, position, Quaternion.identity) as GameObject);

				BrickXml brickXml = new BrickXml();
				brickXml.brickNumber = 0;
				brickXml.brickPosX = position.x;
				brickXml.brickPosY = position.y;
				brickListXml.Add (brickXml);

				position.x += offSetX;
			}

			position.x = initialPosition.position.x;
			position.y -= offSetY;
		}

		for (int i = 0; i < bricks.Length; i++) {

			Instantiate (bricks [i], legendPosition, Quaternion.identity);
			legendPosition.y -= offSetY;
		}

/*		for (int i = 0; i < brickListXml.Count; i++) 
		{
			Debug.Log (defaultBricksMap[i].transform.position);
			Debug.Log (brickListXml[i].brickNumber + " " + brickListXml[i].brickPosX + " " + brickListXml[i].brickPosY);
		}*/
	}

	public void ResetApplication()
	{
		SceneManager.LoadScene (SceneManager.GetActiveScene ().name);
	}
}
