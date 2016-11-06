using UnityEngine;
using System.Collections;

public class SetTextureDoor : MonoBehaviour {


	private Transform[] children;
	private GameObject doortextures;
	private Camera camera;

	// Use this for initialization
	void Start () {

	}

	void Awake(){
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
