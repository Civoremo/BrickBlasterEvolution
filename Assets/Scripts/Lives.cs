using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Lives : MonoBehaviour {

	public Transform livePosition;
	public GameObject Heart;

	public List<GameObject> heartList = new List<GameObject>();
	public List<Vector3> heartPositions = new List<Vector3> ();

	GameObject heart;
	Vector3 position;
	float offsetX;

	// Use this for initialization
	void Start () {

		heart = Heart;
		position = livePosition.position;
		offsetX = heart.GetComponent<BoxCollider2D> ().size.x;

		for (int i = 0; i < 3; i++) 
		{
			heartList.Add (Instantiate (heart, position, Quaternion.identity) as GameObject);
			heartPositions.Add (position);
			position.x += offsetX + 0.5f;
		}
	}
	
	// Update is called once per frame
	void Update () {
	

	}

}
