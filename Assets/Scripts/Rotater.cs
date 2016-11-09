using UnityEngine;
using System.Collections;

public class Rotater : MonoBehaviour {
	Quaternion newRotation;
	Quaternion finalRotation;

	private float speed;

	private bool turn;

	private float s;

	// Use this for initialization
	void Start () {
		speed = 0.1f;

		switch (this.gameObject.tag) {
		case "Orb1":
			speed = 1f;
			break;
		case "Orb2":
			speed = 25f;
			break;
		case "Orb3":
			speed = 18f;
			break;
		default:
			Debug.Log (this.gameObject.tag);
			break;
		}

		//turn = false;

		//finalRotation = getRotation() *  this.transform.rotation;

	}

	// Update is called once per frame
	void Update () {
		/*s = Time.smoothDeltaTime * speed;
		this.transform.rotation *= getRotation ();
		/*this.transform.rotation = Quaternion.Slerp (this.transform.rotation, finalRotation, s);
		if(this.transform.rotation==finalRotation){
			
			Debug.Log ("hello");
			Debug.Log (gameObject);
	 		finalRotation = getRotation() *  this.transform.rotation;
		}*/

		switch (this.gameObject.tag) {
		case "Orb1":
			transform.Rotate (new Vector3 (15, 30, 65) * Time.deltaTime);
			break;
		case "Orb2":
			transform.Rotate (new Vector3 (25, 60, 85) * Time.deltaTime);
			break;
		case "Orb3":
			transform.Rotate (new Vector3 (35, 90, 145) * Time.deltaTime);
			break;
		default:
			Debug.Log (this.gameObject.tag);
			break;
		}

	}

	void turning(Quaternion rotation){

		Quaternion finalRotation = rotation *  this.transform.rotation;

		while(this.transform.rotation != finalRotation){
			this.transform.rotation = Quaternion.Slerp(this.transform.rotation, finalRotation, Time.deltaTime * 5f);
		}
		this.transform.rotation = finalRotation;

	}

	Quaternion getRotation(){
		switch (this.gameObject.tag) {
		case "Orb1":
			speed = 0.1f;
			newRotation = Quaternion.Euler (new Vector3 (Random.value * 3f, Random.value * 3f, Random.value * 3f));
			break;
		case "Orb2":
			speed = 0.2f;
			newRotation = Quaternion.Euler (new Vector3 (Random.value * 3f, Random.value * 3f, Random.value * 3f));
			break;
		case "Orb3":
			speed = 0.3f;
			newRotation = Quaternion.Euler (new Vector3 (Random.value * 3f, Random.value * 3f, Random.value * 3f));
			break;
		default:
			Debug.Log (this.gameObject.tag);
			break;
		}

		return newRotation;
	}

	/*
	IEnumerator MyCoroutine (Transform target)
	{
		while(Vector3.Distance(transform.position, target.position) > 0.05f)
		{
			transform.position = Vector3.Lerp(transform.position, target.position, smoothing * Time.deltaTime);

			yield return null;
		}

		print("Reached the target.");

		yield return new WaitForSeconds(3f);

		print("MyCoroutine is now finished.");
	}
	*/

	IEnumerator Rotation(Quaternion rotation){
		//Quaternion finalRotation = Quaternion.Euler( 0, rotationAmount, 0 ) * startingRotation;
		Quaternion finalRotation = rotation *  this.transform.rotation;

		while(this.transform.rotation != finalRotation){
			this.transform.rotation = Quaternion.Slerp(this.transform.rotation, finalRotation, Time.deltaTime * 5f);
			yield return 0;
		}

		turn = true;
		this.transform.rotation = finalRotation;
	}

}
