using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using System.Linq;
using System.Text.RegularExpressions;


public class PlayerController : MonoBehaviour {


	public GameObject Snake;

	private List<GameObject> tailBlocks;

	private List<GameObject> Teleports;

	public float speed;
	public float torque;

	public Text score;
	public Text win;
	public float count;

	private bool keypress = true;

	private bool gameover = false;

	private Rigidbody rb;

	private float gravity = 9.8f;

	public Quaternion rotate;

	private float direction;

	private float minAngle = 0.0f;
	private float maxAngle = 90.0f;

	private float angle = 0.0f;

	private Quaternion startingRotation;

	private bool straight = true;

	private Transform position;

	TailController newTailcontrol;

	public int activeState;


	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody>();

		count = 0;
		setScore ();
		win.text = " ";
		startingRotation = this.transform.rotation;

		Teleports = new List<GameObject> (GameObject.FindGameObjectsWithTag ("Teleport"));

		Teleports.Sort ((x, y) => string.Compare(x.name, y.name));

		tailBlocks = new List<GameObject> (GameObject.FindGameObjectsWithTag ("SnakeTail"));

		tailBlocks.Sort ((x, y) => string.Compare(x.name, y.name));

		Vector3 offsetfirst = tailBlocks [0].transform.position - transform.position;
		offsetfirst = offsetfirst.normalized * transform.localScale.y;  
		tailBlocks [0].transform.position = offsetfirst + transform.position;
		tailBlocks [0].GetComponent<TailController> ().prev = this.gameObject;
		tailBlocks [0].GetComponent<TailController> ().next = tailBlocks [1];

		for (int i = 1; i < tailBlocks.Count; i++) {
			Vector3 offsets = tailBlocks [i].transform.position - tailBlocks [i-1].transform.position;
			offsets = offsets.normalized * tailBlocks [i].transform.localScale.y;  
			tailBlocks [i].transform.position = offsets + tailBlocks [i-1].transform.position;

			tailBlocks [i].GetComponent<TailController> ().prev = tailBlocks [i - 1];
			if (i != tailBlocks.Count-1) {
				tailBlocks [i].GetComponent<TailController> ().next = tailBlocks [i + 1];
			} else {
				tailBlocks [i].GetComponent<TailController> ().next = null;
			}
		}

		foreach (var item in tailBlocks) {
			Debug.Log (item);
		}

		foreach (var port in Teleports) {
			Debug.Log (port);
		}

	
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
		switch (Input.inputString) {
		case "p":
		case "P":
			for (int i = 0; i < tailBlocks.Count; i++) {
				Debug.Log (i);
				Debug.Log (tailBlocks [i]);
				Debug.Log (tailBlocks [i].GetComponent<TailController> ().prev);
				Debug.Log (tailBlocks [i].GetComponent<TailController> ().next);
				Debug.Log (tailBlocks [i].GetComponent<TailController> ().activeState);
			}
			break;

		case "v":
			Debug.Log ((Quaternion.Inverse(GetRotation (transform.forward, transform.up)) * transform.rotation).eulerAngles);
			break;
		default:
			//Debug.Log(String.Format("Invalid Input String: {0}",int.Parse(Input.inputString)));
			break;
		}
	}

	void LateUpdate(){

	}

	void FixedUpdate (){

		rb.AddForce(-transform.up * gravity);
		
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
		
		/*Vector3 offsetfirst = tailBlocks [0].transform.position - transform.position;
		offsetfirst = offsetfirst.normalized * transform.localScale.y;  
		tailBlocks [0].transform.position = offsetfirst + transform.position;

		for (int i = 1; i < tailBlocks.Count; i++) {
		Vector3 offsets = tailBlocks [i].transform.position - tailBlocks [i-1].transform.position;
		offsets = offsets.normalized * tailBlocks [i].transform.localScale.y;  
		tailBlocks [i].transform.position = offsets + tailBlocks [i-1].transform.position;
		}*/


		/*Vector3 offsets =  transform.position - player.transform.position;
		offsets = offset.normalized * transform.localScale.y;  
		transform.position = offset + player.transform.position;*/

		
		if (keypress) {

			if (Input.GetKeyDown (KeyCode.RightArrow)) {
				keypress = false;
				angle += 90;
			Debug.Log("Turn Right");
			Debug.Log (GetRotation (transform.forward, transform.right).eulerAngles);
			Debug.Log ((GetRotation (transform.forward, -transform.right).eulerAngles));
			Debug.Log("End Turn Right");
				StopAllCoroutines ();
				//StartCoroutine (Rotate (angle));
				StartCoroutine (Rotate(GetRotation (transform.forward, transform.right)));
			}

			if (Input.GetKeyDown (KeyCode.LeftArrow)) {
				keypress = false;
				angle -= 90;
			Debug.Log("Turn left");
			Debug.Log (GetRotation (transform.forward, transform.right).eulerAngles);
			Debug.Log ((GetRotation (transform.forward, -transform.right).eulerAngles));
			Debug.Log("End Turn left");
				StopAllCoroutines ();
				//StartCoroutine (Rotate (angle));
				StartCoroutine (Rotate(GetRotation (transform.forward, -transform.right)));
			}
		}
	}

	//IEnumerator Rotate(float rotationAmount){
	IEnumerator Rotate(Quaternion rotation){
		//Quaternion finalRotation = Quaternion.Euler( 0, rotationAmount, 0 ) * startingRotation;
		Quaternion finalRotation = rotation *  this.transform.rotation;

		while(this.transform.rotation != finalRotation){
			this.transform.rotation = Quaternion.Slerp(this.transform.rotation, finalRotation, Time.deltaTime * speed);
			yield return 0;
		}

		this.transform.rotation = finalRotation;

		keypress = true;

		Debug.Log ("Final rotation");
		Debug.Log (finalRotation.eulerAngles);
		Debug.Log (startingRotation.eulerAngles);
		Debug.Log (transform.rotation.eulerAngles);
		tailBlocks [0].GetComponent<TailController> ().activeState = 4;
		position = this.transform;
		StopAllCoroutines ();
		StartCoroutine ("Movement");
	}

	/*IEnumerator Movement(){

		while (true) {
			rb.MovePosition (transform.position + transform.forward * speed * Time.deltaTime);
			yield return 0;
		}
	}*/

	
	IEnumerator Movement(){

		while (true) {
			rb.MovePosition (transform.position + transform.forward * Time.deltaTime);
			
			
		/*Vector3 offsetfirst = tailBlocks [0].transform.position - transform.position;
		offsetfirst = offsetfirst.normalized * transform.localScale.y;  
		tailBlocks [0].transform.position = offsetfirst + transform.position;

		for (int i = 1; i < tailBlocks.Count; i++) {
			Vector3 offsets = tailBlocks [i].transform.position - tailBlocks [i-1].transform.position;
			offsets = offsets.normalized * tailBlocks [i].transform.localScale.y;  
			tailBlocks [i].transform.position = offsets + tailBlocks [i-1].transform.position;
		}*/

			yield return 0;
		}
	}

	void jumpTailfoward(Vector3 vector){
		if (Vector3.Dot (transform.forward, vector) ==  0) {
			Debug.Log ("Right Angle");
			
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

		switch (other.gameObject.tag) {
		case "Orb1":
			Destroy (other.gameObject);
			count = count + 1;
			setScore ();

			Debug.Log ("Ate Orb1 Create Snake");

			AddTail ();
			break;
		case "Orb2":
			Destroy (other.gameObject);
			count = count + 2;
			setScore ();

			Debug.Log ("Ate Orb2 Create Snake");

			
			AddTail ();
			break;
		case "Orb3":
			Destroy (other.gameObject);
			count = count + 3;
			setScore ();

			Debug.Log ("Ate Orb3 Create Snake");

			AddTail ();
			break;
		case "HitGravity":
			Debug.Log ("hitgravity");
		Debug.Log (Quaternion.Inverse(GetRotation (transform.forward, transform.up)));
		Debug.Log ((Vector3.Cross(transform.rotation.eulerAngles.normalized, transform.forward) * 90) + this.transform.rotation.eulerAngles);
		Debug.Log (GetRotation (transform.rotation.eulerAngles.normalized, -transform.forward).eulerAngles);
		Debug.Log ((Quaternion.Inverse(GetRotation (transform.forward, transform.up)) * this.transform.rotation).eulerAngles);
		this.transform.rotation = Quaternion.Euler((GetRotation (-transform.forward, transform.up).eulerAngles + this.transform.rotation.eulerAngles));
			tailBlocks [0].GetComponent<TailController> ().activeState = 4;
			Debug.Log ("End hitgravity");
			break;
		case "FallGravity":
			this.transform.rotation = GetRotation (transform.forward, -transform.up);
			tailBlocks [0].GetComponent<TailController> ().activeState = 4;
			break;
		case "Teleport":
			
			GameObject tp;

			if (int.Parse (Regex.Replace (other.gameObject.name, "[^0-9]", "")) % 2 == 0) {
				tp = Teleports [Teleports.IndexOf(other.gameObject) + 1];
			} else {
				tp = Teleports [Teleports.IndexOf(other.gameObject) - 1];
			}
		

			string numbers = Regex.Replace (other.gameObject.name, "[^0-9]", "");
			int num = int.Parse (numbers) + 1;

			string name = "Teleport" + num;

			//Transform temp = Teleport ("Teleport" + (int.Parse (Regex.Replace (this.gameObject.name, "[^0-9]", "")) + 1));

			this.transform.position = tp.transform.position;
			this.transform.rotation = tp.transform.rotation;

			tailBlocks [0].GetComponent<TailController> ().activeState = 4;
			break;
		default:
			break;
		}


		/*if (other.gameObject.CompareTag ("Orb1")) {
			Destroy (other.gameObject);
			count = count + 1;
			setScore ();

			Debug.Log ("Ate Orb Create Snake");

			AddTail ();
		}*/

		if (count > 4) {
			win.text = "WIN!";
			//StopAllCoroutines ();
		}

	}
	

	Quaternion GetRotation(Vector3 vec1, Vector3 vec2){
		//Vector3 Crossvector = Vector3.Cross(vec1,vec2);
		return	Quaternion.FromToRotation (vec1,vec2);
	}

	
	Transform Teleport(string name){
		GameObject door = GameObject.Find(name);
		return door.transform;
	}


	void AddTail(){
		GameObject Last = tailBlocks.Last();

		Vector3 newposition = Last.transform.position + (-Last.transform.forward);
		GameObject newTail = (GameObject)Instantiate (Snake, newposition, Quaternion.identity);
		newTail.GetComponent<TailController> ().prev = Last;
		newTail.GetComponent<TailController> ().next = null;
		tailBlocks [tailBlocks.Count - 1].GetComponent<TailController> ().next = newTail;
		tailBlocks.Add(newTail);
	}
	
	void setScore(){
		score.text = "Score: " + count.ToString ();
	}
}
