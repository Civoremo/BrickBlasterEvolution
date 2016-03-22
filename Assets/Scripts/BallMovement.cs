using UnityEngine;
using System.Collections;

public class BallMovement : MonoBehaviour {

	public Transform initialPos;
	public float force;
	bool released = false;
	Vector3 initialPosition;
//	Rigidbody2D ballRigid2D;

	// Use this for initialization
	void Start () {
//		ballRigid2D = GetComponent<Rigidbody2D> ();
	}
	
	// Update is called once per frame
	void LateUpdate () {

		if (released == false) {
			initialPosition = initialPos.position;
			initialPosition.y += 0.5f;
			transform.position = initialPosition;

			if(Input.GetButtonDown("Jump"))
			{
				GetComponent<Rigidbody2D>().AddForce(new Vector2(1f, 0.5f) * Time.deltaTime * force);
				//ballRigid2D.velocity = new Vector2(1f, 0.5f) * Time.deltaTime * force;
				released = true;
			}
		}

		if (BrickLevelGenerator.numberOfBricks == 0) {
			//Application.LoadLevel (Application.loadedLevel);
		}
	}

	public void OnCollisionEnter2D(Collision2D other)
	{
		if (other.gameObject.tag == "Brick") {
			Destroy (other.gameObject, 0.1f);
			BrickLevelGenerator.numberOfBricks--;
		}
	}
}
