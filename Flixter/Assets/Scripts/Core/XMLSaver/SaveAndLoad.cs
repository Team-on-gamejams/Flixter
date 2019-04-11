///Created by Neodrop. neodrop@unity3d.ru
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SaveAndLoad : MonoBehaviour {
	public string fileName = "SaveMe.neo";
	public string fileNameXML = "SaveMe.xml";

	Rect redRect, greenRect, blueRect, resetRect, loadRect, resetXMLRect, loadXMLRect;

	void Awake() {
		redRect = new Rect(10, 10, 100, 30);
		greenRect = new Rect(redRect.x + redRect.width + 10, redRect.y, redRect.width, redRect.height);
		blueRect = new Rect(greenRect.x + greenRect.width + 10, redRect.y, redRect.width, redRect.height);
		resetRect = new Rect(blueRect.x + blueRect.width + 10, redRect.y, redRect.width, redRect.height);
		loadRect = new Rect(resetRect.x + resetRect.width + 10, redRect.y, redRect.width, redRect.height);
		resetXMLRect = new Rect(loadRect.x + loadRect.width + 10, redRect.y, redRect.width, redRect.height);
		loadXMLRect = new Rect(resetXMLRect.x + resetXMLRect.width + 10, redRect.y, redRect.width, redRect.height);
	}

	GameObject go = null;
	List<GameObject> objects = new List<GameObject>();

	List<ObjectSaver> GetObjectsToSave() {
		List<ObjectSaver> toSave = new List<ObjectSaver>();
		int count = objects.Count;
		for (int i = 0; i < count; i++) {
			GameObject obj = objects[i];
			TypeHolder th = obj.GetComponent<TypeHolder>();
			toSave.Add(new ObjectSaver(obj, th.type));
		}

		for (int i = 0; i < count; i++)
			Destroy(objects[i]);
		objects.Clear();
		return toSave;
	}

	void CreateObjectsFromList(List<ObjectSaver> toLoad) {
		foreach (ObjectSaver obj in toLoad) {
			GameObject g = GameObject.CreatePrimitive(obj.GetObjectType());
			g.transform.position = obj.position;
			g.GetComponent<Renderer>().material.color = obj.color;
			g.name = obj.objectName;
			TypeHolder th = g.AddComponent<TypeHolder>();
			th.type = obj.GetObjectType();
			objects.Add(g);
		}
	}

	void OnGUI() {
		bool created = false;

		if (GUI.RepeatButton(redRect, "Capsule")) {
			go = GameObject.CreatePrimitive(PrimitiveType.Capsule);
			TypeHolder typeH = go.AddComponent<TypeHolder>();
			typeH.type = PrimitiveType.Capsule;
			go.name = "Capsule" + go.GetInstanceID();
			created = true;
		}

		if (GUI.RepeatButton(greenRect, "Cube")) {
			go = GameObject.CreatePrimitive(PrimitiveType.Cube);
			TypeHolder typeH = go.AddComponent<TypeHolder>();
			typeH.type = PrimitiveType.Cube;
			go.name = "Cube" + go.GetInstanceID();
			created = true;
		}

		if (GUI.RepeatButton(blueRect, "Cylinder")) {
			go = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
			TypeHolder typeH = go.AddComponent<TypeHolder>();
			typeH.type = PrimitiveType.Cylinder;
			go.name = "Cylinder" + go.GetInstanceID();
			created = true;
		}

		/*
        if (GUI.Button(resetRect, "SAVE"))
            BinarySaver.Save(GetObjectsToSave(), fileName);

        if (GUI.Button(loadRect, "LOAD"))
        {
            List<ObjectSaver> toLoad = BinarySaver.Load(fileName) as List<ObjectSaver>;
            if (toLoad == null)
            {
                Debug.Log("No Binary File Found");
                return;
            }

            CreateObjectsFromList(toLoad);
        }
        */

		if (GUI.Button(resetXMLRect, "SAVE XML"))
			XMLSaver<List<ObjectSaver>>.Save(GetObjectsToSave(), fileNameXML);

		if (GUI.Button(loadXMLRect, "LOAD XML")) {
			List<ObjectSaver> toLoad = XMLSaver<List<ObjectSaver>>.Load(fileNameXML);

			if (toLoad == null) {
				Debug.Log("No XML File Found");
				return;
			}

			CreateObjectsFromList(toLoad);
		}

		if (!created)
			return;

		go.transform.position = Random.insideUnitSphere * 5;
		go.GetComponent<Renderer>().material.color = new Color(Random.insideUnitSphere.x, Random.insideUnitSphere.y, Random.insideUnitSphere.z);
		objects.Add(go);
	}
}


[System.Serializable]
public class ObjectSaver {
	//===========================================SAVE AND LOAD======================================
	public string objectType;
	public Color color;
	public Vector3 position;
	public string objectName;

	public ObjectSaver() { }

	public ObjectSaver(GameObject obj, PrimitiveType objectType) {
		switch (objectType) {
			case PrimitiveType.Capsule:
				this.objectType = "Capsule";
				break;
			case PrimitiveType.Cube:
				this.objectType = "Cube";
				break;
			case PrimitiveType.Cylinder:
				this.objectType = "Cylinder";
				break;
		}

		color = obj.GetComponent<Renderer>().material.color;
		position = obj.transform.position;
		objectName = obj.name;
	}

	public PrimitiveType GetObjectType() {
		switch (objectType) {
			case "Capsule":
				return PrimitiveType.Capsule;
			case "Cube":
				return PrimitiveType.Cube;
			case "Cylinder":
				return PrimitiveType.Cylinder;
		}

		return default(PrimitiveType);
	}
}