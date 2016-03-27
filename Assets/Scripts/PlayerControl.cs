using UnityEngine;
using System.Collections;

public class PlayerControl : MonoBehaviour {

	public float speed;
	Vector2 position;

	// Use this for initialization
	void Start () {
		position = transform.position;
	}
	
	// Update is called once per frame
	void LateUpdate () {
		float moveX = Input.GetAxis ("Horizontal");
		position.x += moveX * Time.deltaTime * speed;
		transform.position = position;
	}
}
