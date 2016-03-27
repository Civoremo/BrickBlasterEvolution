using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BallMovement : MonoBehaviour {
	
	Lives liveScript;
	public Transform initialPos;
	public float force;
	bool released = false;
	Vector3 initialPosition;
	int numLives = 2;
//	Rigidbody2D ballRigid2D;

	// Use this for initialization
	void Start () {
		liveScript = gameObject.GetComponent<Lives> ();
		
	}
	
	// Update is called once per frame
	void LateUpdate () {

		if (released == false) {
			initialPosition = initialPos.position;
			initialPosition.y += 0.5f;
			transform.position = initialPosition;

			if(Input.GetButtonDown("Jump") && numLives >= 0)
			{
				GetComponent<Rigidbody2D>().AddForce(new Vector2(1f, 0.5f) * Time.deltaTime * force);

				//ballRigid2D.velocity = new Vector2(1f, 0.5f) * Time.deltaTime * force;
				released = true;

				if(numLives >= 0)
				{
				Destroy (liveScript.heartList [numLives].gameObject);
				numLives--;
				}
			}
		}

		if (released == true) 
		{
			//transform.position += (new Vector3 (1f, 0.5f, 0f) * Time.deltaTime * force);
			if (Input.GetMouseButtonDown (1)) 
			{
				released = false;
			}
		}
	}

	public void OnTriggerEnter2D(Collider2D other)
	{
		if (other.gameObject.tag == "BrickObject") 
		{
			Destroy (other.gameObject, 0.1f);
		}

	}

/*	public void OnCollisionEnter2D(Collision2D other)
	{
		if (other.gameObject.tag == "Brick") {
			Destroy (other.gameObject, 0.1f);
			BrickLevelGenerator.numberOfBricks--;
		}
	}*/
}
