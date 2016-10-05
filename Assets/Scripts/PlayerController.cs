using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour {


	public GameObject prefab;

	//private List<Object> tailBlocks = new List<Object>(3);

	public float speed;
	public float torque;

	public Text score;
	public Text win;
	public float count;

	private bool gameover = false;

	private Rigidbody rb;

	public Quaternion rotate;

	private float direction;

	private float minAngle = 0.0f;
	private float maxAngle = 90.0f;

	private float angle = 0.0f;

	private Quaternion startingRotation;

	private bool straight = true;

	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody>();
		count = 0;
		setScore ();
		win.text = " ";
		startingRotation = this.transform.rotation;

		StopAllCoroutines ();
		StartCoroutine ("Movement");

		/*for (int i = 0; i < tailBlocks.Count; i++) {
			Debug.Log ("Intainsiate");
			Vector3 newposition = transform.position + (-transform.forward) * i;
			tailBlocks[i]=Instantiate (prefab, newposition, Quaternion.identity, transform.transform);
		}*/
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void LateUpdate(){

	}

	void FixedUpdate (){
		
		/*Debug.Log (transform.rotation);
		Debug.Log (Mathf.Round(transform.rotation.x) % 90);
		Debug.Log (Mathf.Round(transform.rotation.x) % 90 != 0);
	
		Debug.Log (!(transform.rotation.x % 90 != 0 && transform.rotation.y % 90 != 0 && transform.rotation.z % 90 != 0));
		if (!(transform.rotation.x % 90 != 0 && transform.rotation.y % 90 != 0 && transform.rotation.z % 90 != 0) || direction != 0) {


			Quaternion startQuat = transform.rotation;
			Quaternion endQuat = startQuat * Quaternion.Euler (0f, 90f * direction, 0f);

			float angles = Mathf.LerpAngle (startQuat.y, endQuat.y, Time.time * speed);

			Quaternion rotating = Quaternion.Euler (0, angles, 0);

			Quaternion rot = Quaternion.Slerp (startQuat, endQuat, Time.time * speed);

			rb.MoveRotation (rot);
		} else {
			Debug.Log (direction);
			rb.MovePosition (transform.position + transform.forward * Time.deltaTime);
		}*/

		direction = Input.GetAxis ("Horizontal");

		Quaternion startQuat = transform.rotation;
		Quaternion endQuat = startQuat * Quaternion.Euler (0f, 90f * direction, 0f);

		float angles = Mathf.LerpAngle (startQuat.y, endQuat.y, Time.time * speed);

		Quaternion rotating = Quaternion.Euler (0, angles, 0);

		Quaternion rot = Quaternion.Slerp (startQuat, endQuat, Time.deltaTime);
		//rb.MoveRotation (rot);

		/*if(straight){
			rb.MovePosition (transform.position + transform.forward * Time.deltaTime);
		}*/

		/*if( Input.GetKeyDown( KeyCode.RightArrow ) ){
			angle += 90;
			StopAllCoroutines();
			StartCoroutine(Rotate(angle));
		}

		//go to -90 degrees with left arrow
		if( Input.GetKeyDown( KeyCode.LeftArrow ) ){
			angle -= 90;
			StopAllCoroutines();
			StartCoroutine(Rotate(angle));
		}*/


		if( Input.GetKeyDown( KeyCode.RightArrow ) ){
			straight = false;
			angle += 90;
			StopAllCoroutines ();
			StartCoroutine (Rotate (angle));
		}

		if (Input.GetKeyDown (KeyCode.LeftArrow)) {
			straight = false;
			angle -= 90;
			StopAllCoroutines ();
			StartCoroutine (Rotate (angle));
		}
	}

	IEnumerator Rotate(float rotationAmount){
		Quaternion finalRotation = Quaternion.Euler( 0, rotationAmount, 0 ) * startingRotation;

		while(this.transform.rotation != finalRotation){
			this.transform.rotation = Quaternion.Slerp(this.transform.rotation, finalRotation, Time.deltaTime * speed);
			yield return 0;
		}
		StopAllCoroutines ();
		StartCoroutine ("Movement");

		//straight = true;
	}

	IEnumerator Movement(){

		while(this.gameover != true){
			rb.MovePosition (transform.position + transform.forward * Time.deltaTime);
			yield return 0;
		}
	}

	void OnCollisionEnter(Collision collision){
		foreach(ContactPoint contact  in collision.contacts){
			Debug.DrawRay (contact.point, contact.normal, Color.white);
		}

		//Debug.Log ("Enter Called");

	}

	void OnCollisionStay(Collision collision){
		foreach(ContactPoint contact  in collision.contacts){
			Debug.DrawRay (contact.point, contact.normal, Color.blue);
		}

		//Debug.Log ("Stay Called");
	}

	void OnCollisionExit(Collision collision){
		foreach(ContactPoint contact  in collision.contacts){
			Debug.DrawRay (contact.point, contact.normal, Color.green);
		}

		//Debug.Log ("Exit Called");
	}

	void OnTriggerEnter(Collider other){
		if (other.gameObject.CompareTag ("Orb1")) {
			Destroy (other.gameObject);
			count = count + 1;
			setScore ();

			//Vector3 newposition = transform.position + (-transform.forward) * 2;

			//tailBlocks.Add(Instantiate (prefab, newposition, Quaternion.identity));
		}

		if (count > 2) {
			win.text = "WIN!";
		}

	}

	void setScore(){
		score.text = "Score: " + count.ToString ();
	}
}
