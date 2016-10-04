using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour {

	public float speed = 0.01f;
	public float torque;

	public Text score;
	public Text win;
	public float count;

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
		if(straight){
			rb.MovePosition (transform.position + transform.forward * Time.deltaTime);
		}

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
			StartCoroutine (Rotate (angle));
		}

		if (Input.GetKeyDown (KeyCode.LeftArrow)) {
			straight = false;
			angle -= 90;
			StartCoroutine (Rotate (angle));
		}
	}

	IEnumerator Rotate(float rotationAmount){
		Quaternion finalRotation = Quaternion.Euler( 0, rotationAmount, 0 ) * startingRotation;

		while(this.transform.rotation != finalRotation){
			this.transform.rotation = Quaternion.Lerp(this.transform.rotation, finalRotation, Time.deltaTime*speed);
			yield return 0;
		}
		straight = true;
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
		}
		if (count >= 1) {
			win.text = "WIN!";
		}

		if (other.gameObject.CompareTag ("Ramp")) {
			Vector3 cross = Vector3.Cross (other.transform.forward, transform.forward);
			float dot = Vector3.Dot (cross, transform.forward);
			if(cross != Vector3.zero && dot == 0){
				
			}
		}

	}

	void setScore(){
		score.text = "Score: " + count.ToString ();
	}
}
