using UnityEngine;
using System.Collections;

public class Rotater : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}

	// Update is called once per frame
	void Update () {

		switch (this.gameObject.tag) {
		case "Orb1":
			transform.Rotate (new Vector3 (Random.value * 5f, Random.value * 35f, Random.value * 45f) * Time.deltaTime);
			break;
		case "Orb2":
			transform.Rotate (new Vector3 (Random.value * 15f, Random.value * 65f, Random.value * 65f) * Time.deltaTime);
			break;
		case "Orb3":
			transform.Rotate (new Vector3 (Random.value * 25f, Random.value * 45f, Random.value * 90f) * Time.deltaTime);
			break;
		default:
			Debug.Log (this.gameObject.tag);
			break;
		}
	}
}
