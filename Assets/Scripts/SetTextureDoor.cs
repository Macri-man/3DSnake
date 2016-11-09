using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using System.Linq;
using System.Text.RegularExpressions;

public class SetTextureDoor : MonoBehaviour {


	private Transform[] children;
	private GameObject doortextures;
	private Camera camera;

	private List<GameObject> Teleports;

	// Use this for initialization
	void Start(){

		GameObject tp;

		Teleports = new List<GameObject> (GameObject.FindGameObjectsWithTag ("Teleport"));

		Teleports.Sort ((x, y) => string.Compare(x.name, y.name));
		

		if (int.Parse (Regex.Replace (gameObject.name, "[^0-9]", "")) % 2 == 0) {
			tp = Teleports [Teleports.IndexOf (gameObject) + 1];
		} else {
			tp = Teleports [Teleports.IndexOf (gameObject) - 1];
		}

		Shader shade = new Shader ();

		RenderTexture texure = new RenderTexture(2048, 2048, 24, RenderTextureFormat.ARGB32);
		texure.Create();

//		Material mat = new Material ().mainTexture = shade;
		//children = transform.GetComponentsInChildren<Transform>();
		doortextures = this.gameObject.transform.GetChild(0).gameObject;
		camera =  this.gameObject.transform.GetChild(1).GetComponent<Camera>();
		/*Debug.Log ("print debug");
		Debug.Log (doortextures);
		Debug.Log (children);
		foreach (var item in children) {
			Debug.Log (item);
		}*/

	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
