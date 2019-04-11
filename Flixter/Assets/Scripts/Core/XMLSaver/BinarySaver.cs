///Created by Neodrop. neodrop@unity3d.ru
using System.IO;
using System.Text;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class BinarySaver {
	public static void Save(object obj, string fileName) {
		FileStream fs = new FileStream(fileName, FileMode.Create);
		BinaryFormatter formatter = new BinaryFormatter();
		try {
			formatter.Serialize(fs, obj);
		}
		catch (SerializationException e) {
			Debug.Log("Failed to serialize. Reason: " + e.Message);
			throw;
		}
		finally {
			fs.Close();
		}
	}

	public static object Load(string fileName) {
		if (!File.Exists(fileName))
			return null;

		FileStream fs = new FileStream(fileName, FileMode.Open);
		object obj = null;
		try {
			BinaryFormatter formatter = new BinaryFormatter();
			obj = (object)formatter.Deserialize(fs);
		}
		catch (SerializationException e) {
			Debug.Log("Failed to deserialize. Reason: " + e.Message);
			throw;
		}
		finally {
			fs.Close();
		}
		return obj;
	}
}