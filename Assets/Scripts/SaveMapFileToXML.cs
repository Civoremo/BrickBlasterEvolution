using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SaveMapFileToXML : MonoBehaviour {

	string path = Application.dataPath + "/brickMapLevel.xml";
	BrickMapEditor mapEditor = new BrickMapEditor();
	List<BrickXml> brickXml = new List<BrickXml>();

	void Start()
	{
		brickXml = mapEditor.brickListXml;
	}

	public void SaveMapToXmlFile()
	{
		XMLObjectSerializer.Serialize (brickXml, path);
	}

}
