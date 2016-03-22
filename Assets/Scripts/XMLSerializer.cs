using UnityEngine;
using System.Xml.Serialization;
using System.Xml;
using System.IO;

public class XMLObjectSerializer {

	public static void Serialize(object item, string path)
	{
		XmlSerializer serializer = new XmlSerializer (item.GetType ());
		StreamWriter writer = new StreamWriter (path);
		serializer.Serialize (writer.BaseStream, item);
		writer.Close ();
	}

	public static itemType Deserialize<itemType>(string path)
	{
		XmlSerializer serializer = new XmlSerializer (typeof(itemType));
		StreamReader reader = new StreamReader (path);
		itemType deserialized = (itemType)serializer.Deserialize (reader.BaseStream);
		reader.Close ();
		return deserialized;
	}
}
