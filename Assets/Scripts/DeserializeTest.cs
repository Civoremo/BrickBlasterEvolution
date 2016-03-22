using UnityEngine;
using System.Collections;

public class DeserializeTest : MonoBehaviour {

	void Start()
	{
		BrickXml Brick = XMLObjectSerializer.Deserialize<BrickXml> (Application.dataPath + "/brick.xml");
		Debug.Log (Brick.brickNumber);
	}
}
