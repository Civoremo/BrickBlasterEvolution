using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Serialization;

public class XmlTestFile : MonoBehaviour {

	public List<BrickXml> BricksLists = new List<BrickXml>();

	public GameObject[] Bricks;
	public Transform startPosition;
	public int rows = 10;
	public int columns = 5;

	float offsetx;
	float offsety;
	Vector3 position;
	BoxCollider2D brickCollider;
	GameObject[] bricks;
	int randomNumber;

	// Use this for initialization
	void Start () {
		bricks = Bricks;
		brickCollider = bricks [0].GetComponent<BoxCollider2D>();
		offsetx = brickCollider.size.x;
		offsety = brickCollider.size.y;
		position = startPosition.position;

		for (int i = 0; i < columns; i++) {
			for (int j = 0; j < rows; j++) {
				randomNumber = Random.Range (0, Bricks.Length);
				Instantiate(bricks[randomNumber], position, Quaternion.identity);

				//assign values and add object to List<BrickXml> to be serialized later
				BrickXml brick = new BrickXml ();
				brick.brickNumber = randomNumber;
				brick.brickPosX = position.x;
				brick.brickPosY = position.y;
				BricksLists.Add (brick);
				//end assigning

				position.x += offsetx;
				//numberOfBricks++;
			}
			position.x = startPosition.position.x;
			position.y -= offsety;
		}

		// testing purposes only
		for (int i = 0; i < BricksLists.Count; i++) 
		{
			//Debug.Log (BricksLists [i].brickNumber + " " + BricksLists [i].brickPosX + " " + BricksLists [i].brickPosY);
		}
		XMLObjectSerializer.Serialize (BricksLists, Application.dataPath + "/bricksList.xml");

	}
}
