using UnityEngine;
using System.Collections;

public class Rotater : MonoBehaviour {

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		transform.Rotate (new Vector3 (15, 35, 65) * Time.deltaTime);
	}
}
