using UnityEngine;
using System.Collections;

public class OrbCreation : MonoBehaviour {


	int orbNum;
	public int count;

	public GameObject Orb1;
	public GameObject Orb2;
	public GameObject Orb3;

	private GameObject orb;

	private int reinitiate;

	public bool gone;



	// Use this for initialization
	void Start () {
		orbNum = Random.Range (1, 3);
		count = 0;
		reinitiate = Random.Range (205, 220);
		gone = false;
		switch (orbNum) {
		case 1:
			orb = (GameObject)Instantiate (Orb1, this.transform.position ,this.transform.rotation);
			orb.transform.parent = gameObject.transform;
			break;
		case 2:
			orb = (GameObject)Instantiate (Orb2, this.transform.position ,this.transform.rotation);
			orb.transform.parent = gameObject.transform;
			break;
		case 3:
			orb = (GameObject)Instantiate (Orb3, this.transform.position ,this.transform.rotation);
			orb.transform.parent = gameObject.transform;
			break;
		}
	}
	
	void Update () {

			if (gone) {
				count++;
				if (count % reinitiate == 0) {
				gone = false;
				count = 1;
				Debug.Log ("Create Orb");
				switch (orbNum) {
				case 1:
					orb = (GameObject)Instantiate (Orb1, this.transform.position ,this.transform.rotation);
					orb.transform.parent = gameObject.transform;
					break;
				case 2:
					orb = (GameObject)Instantiate (Orb2, this.transform.position ,this.transform.rotation);
					orb.transform.parent = gameObject.transform;
					break;
				case 3:
					orb = (GameObject)Instantiate (Orb3, this.transform.position ,this.transform.rotation);
					orb.transform.parent = gameObject.transform;
					break;
				}
			}
		}
		
	}
}
