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

	private CameraController mainCamera;
	private CameraController fpCamera;

	public float speed;
	public float torque;

	public Text score;
	public Text win;
	public float count;

	private bool keypress = true;

	private bool gameover = false;

	private Rigidbody rb;

	private float gravity = 2f;

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

	private GameBaord board;

	private Transform startposition;


	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody>();

		startposition = this.transform;

		/*
		count = 0;
		setScore ();
		win.text = "";
		*/
		speed = 2;
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

		mainCamera = GameObject.Find ("MainCamera").GetComponent<CameraController>();
		mainCamera.gameObject.SetActive (true);

		fpCamera = GameObject.Find ("FPCamera").GetComponent<CameraController>();
		fpCamera.gameObject.SetActive (false);
	
		StopAllCoroutines ();
		StartCoroutine ("Movement");

		/*for (int i = 0; i < tailBlocks.Count; i++) {
			Debug.Log ("Intainsiate");
			Vector3 newposition = transform.position + (-transform.forward) * i;
			tailBlocks[i]=Instantiate (prefab, newposition, Quaternion.identity, transform.transform);
		}*/
	}

	void restart(){
		this.transform.rotation = startposition.rotation;
		this.transform.position = startposition.position;
		foreach (var item in tailBlocks) {
			Debug.Log (item);
		}
		for (int i = 0; i < tailBlocks.Count-1; i++) {
			tailBlocks [i].GetComponent<TailController> ().transform.rotation = startposition.rotation;
			tailBlocks [i].GetComponent<TailController> ().transform.position = new Vector3(startposition.position.x,startposition.position.y,startposition.position.z - (i+1));
		}
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

		case "o":
			Debug.Log ((this.transform.rotation).eulerAngles);
			Debug.Log ((this.transform.rotation * GetRotation (transform.forward, transform.up)).eulerAngles);
			Debug.Log ((GetRotation (transform.right, transform.forward) * this.transform.rotation).eulerAngles);
			Debug.Log ((GetRotation (Vector3.up, -transform.forward) * this.transform.rotation).eulerAngles);
			Debug.Log ((GetRotation (Vector3.up, -transform.forward) * this.transform.rotation));
			break;
		default:
			//Debug.Log(String.Format("Invalid Input String: {0}",int.Parse(Input.inputString)));
			break;
		}
	}

	void OnDrawGizmos(){
		
		Color color;
		color = Color.green;
		// local up
		DrawHelperAtCenter(this.transform.up, color, 4f);

		color.g -= 0.5f;
		// global up
		//DrawHelperAtCenter(Vector3.up, color, 3f);

		color = Color.blue;
		// local forward
		DrawHelperAtCenter(this.transform.forward, color, 4f);

		color.b -= 0.5f;
		// global forward
		//DrawHelperAtCenter(Vector3.forward, color, 3f);

		color = Color.red;
		// local right
		DrawHelperAtCenter(this.transform.right, color, 4f);

		color.r -= 0.5f;
		// global right
		//DrawHelperAtCenter(Vector3.right, color, 3f);
	}

	private void DrawHelperAtCenter(Vector3 direction, Color color, float scale){
		Gizmos.color = color;
		Vector3 destination = transform.position + direction * scale;
		Gizmos.DrawLine(transform.position, destination);
	}

	void LateUpdate(){

	}

	void FixedUpdate (){

		rb.AddForce(-transform.up * gravity);

		
		if (keypress) {

			if (Input.GetKeyDown (KeyCode.D)) {
				keypress = false;
			Debug.Log("Turn Right");
			Debug.Log (GetRotation (transform.forward, transform.right).eulerAngles);
			Debug.Log ((GetRotation (transform.forward, -transform.right).eulerAngles));
			Debug.Log("End Turn Right");
				StopAllCoroutines ();
				//StartCoroutine (Rotate (angle));
				StopCoroutine("Movement");
				StartCoroutine (Rotate(GetRotation (transform.forward, transform.right)));
			}

			if (Input.GetKeyDown (KeyCode.A)) {
				keypress = false;
			Debug.Log("Turn left");
			Debug.Log (GetRotation (transform.forward, transform.right).eulerAngles);
			Debug.Log ((GetRotation (transform.forward, -transform.right).eulerAngles));
			Debug.Log("End Turn left");
				StopAllCoroutines ();
				StopCoroutine("Movement");
				//StartCoroutine (Rotate (angle));
				StartCoroutine (Rotate(GetRotation (this.transform.forward, -this.transform.right)));
			}
		}
	}

	//IEnumerator Rotate(float rotationAmount){
	IEnumerator Rotate(Quaternion rotation){
		//Quaternion finalRotation = Quaternion.Euler( 0, rotationAmount, 0 ) * startingRotation;
		Debug.Log("ROTATION");
		Quaternion finalRotation = rotation * this.transform.rotation;
		Debug.Log(finalRotation.eulerAngles);
		Debug.Log(this.transform.rotation.eulerAngles);

		while(this.transform.rotation != finalRotation){
			this.transform.rotation = Quaternion.Slerp(this.transform.rotation, finalRotation, Time.deltaTime);
			//Debug.Log (this.transform.rotation.eulerAngles);

			//mainCamera.rotationX = mainCamera.transform.rotation.eulerAngles.y + this.transform.localRotation.eulerAngles.y;
			//mainCamera.rotationY = mainCamera.transform.rotation.eulerAngles.x + this.transform.localRotation.eulerAngles.x;
			//fpCamera.rotationX = fpCamera.transform.rotation.eulerAngles.y + this.transform.rotation.eulerAngles.y;
			//fpCamera.rotationY = fpCamera.transform.rotation.eulerAngles.x + this.transform.rotation.eulerAngles.x;
			yield return 0;
		}

		this.transform.rotation = finalRotation;

	//mainCamera.transform.rotation = this.transform.rotation;
	//mainCamera.rotationX = 0;
	//mainCamera.rotationY = 0;

	//mainCamera.rotationX = this.transform.rotation.eulerAngles.y;
	//mainCamera.rotationY = this.transform.rotation.eulerAngles.x;
	//fpCamera.rotationX = this.transform.rotation.eulerAngles.y;
	//fpCamera.rotationY = this.transform.rotation.eulerAngles.x;

		keypress = true;

		Debug.Log ("Final rotation");
		Debug.Log (finalRotation.eulerAngles);
		Debug.Log (startingRotation.eulerAngles);
		Debug.Log (transform.rotation.eulerAngles);
		tailBlocks [0].GetComponent<TailController> ().activeState = 4;


		StopAllCoroutines ();
		StartCoroutine ("Movement");
	}

	
	IEnumerator Movement(){

		while (true) {
			rb.MovePosition (transform.position + transform.forward * speed * Time.deltaTime);
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

		switch (other.gameObject.tag) {
		/*case "Orb1":
			other.GetComponentInParent<OrbCreation> ().gone = true;
			other.GetComponentInParent<OrbCreation> ().count = 1;
			Destroy (other.gameObject);
			count = count + 1;
			setScore ();

			Debug.Log ("Ate Orb1 Create Snake");

			AddTail ();
			break;
		case "Orb2":
			other.GetComponentInParent<OrbCreation> ().gone = true;
			other.GetComponentInParent<OrbCreation> ().count = 1;
			Destroy (other.gameObject);
			count = count + 2;
			setScore ();

			Debug.Log ("Ate Orb2 Create Snake");

			
			AddTail ();
			break;
		case "Orb3":
			other.GetComponentInParent<OrbCreation> ().gone = true;
			other.GetComponentInParent<OrbCreation> ().count = 1;
			Destroy (other.gameObject);
			count = count + 3;
			setScore ();

			Debug.Log ("Ate Orb3 Create Snake");

			AddTail ();
			break;*/
		case "HitGravity":
			Debug.Log ("Hitgravity");

			Debug.Log (this.transform.rotation.eulerAngles);
			this.transform.rotation = GetRotation (transform.up, -transform.forward) * this.transform.rotation;
			Debug.Log (this.transform.rotation.eulerAngles);
			tailBlocks [0].GetComponent<TailController> ().activeState = 4;
	
		mainCamera.startquaternion = this.transform.rotation;
		mainCamera.rotationX = 0;
		mainCamera.rotationY = 0;

		fpCamera.startquaternion = this.transform.rotation;
		fpCamera.rotationX = 0;
		fpCamera.rotationY = 0;

			Debug.Log ("End hitgravity");
			break;
		case "FallGravity":
		Debug.Log ("fallgravity");
			this.transform.rotation = GetRotation (transform.up, transform.forward) * this.transform.rotation;
		tailBlocks [0].GetComponent<TailController> ().activeState = 4;
	
		mainCamera.startquaternion = this.transform.rotation;
		mainCamera.rotationX = 0;
		mainCamera.rotationY = 0;

		fpCamera.startquaternion = this.transform.rotation;
		fpCamera.rotationX = 0;
		fpCamera.rotationY = 0;

		Debug.Log ("fallgravity");
			break;
		case "Teleport":
			Debug.Log ("teleport");
			GameObject tp;

			if (int.Parse (Regex.Replace (other.gameObject.name, "[^0-9]", "")) % 2 == 0) {
				tp = Teleports [Teleports.IndexOf (other.gameObject) + 1];
			} else {
				tp = Teleports [Teleports.IndexOf (other.gameObject) - 1];
			}

			string numbers = Regex.Replace (other.gameObject.name, "[^0-9]", "");
			int num = int.Parse (numbers) + 1;

			string name = "Teleport" + num;

			//Transform temp = Teleport ("Teleport" + (int.Parse (Regex.Replace (this.gameObject.name, "[^0-9]", "")) + 1));

			this.transform.position = tp.transform.position + tp.transform.forward;
			this.transform.rotation = tp.transform.rotation;

			mainCamera.startquaternion = this.transform.rotation;
			mainCamera.rotationX = 0;
			mainCamera.rotationY = 0;

			fpCamera.startquaternion = this.transform.rotation;
			fpCamera.rotationX = 0;
			fpCamera.rotationY = 0;

			tailBlocks [0].GetComponent<TailController> ().activeState = 4;
			break;
		case "Collision":
			//win.text = "LOSE!";
			StopAllCoroutines ();
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
		/*
		if (count > 4) {
			win.text = "WIN!";
			//StopAllCoroutines ();
		}*/

	}
	

	Quaternion GetRotation(Vector3 vec1, Vector3 vec2){
		return	Quaternion.FromToRotation (vec1,vec2);
	}

	
	Transform Teleport(string name){
		GameObject door = GameObject.Find(name);
		return door.transform;
	}


	public void AddTail(){
		GameObject Last = tailBlocks.Last();

		Vector3 newposition = Last.transform.position + (-Last.transform.forward);
		GameObject newTail = (GameObject)Instantiate (Snake, newposition,Last.transform.rotation);
		newTail.GetComponent<TailController> ().prev = Last;
		newTail.GetComponent<TailController> ().next = null;
		tailBlocks [tailBlocks.Count - 1].GetComponent<TailController> ().next = newTail;
		tailBlocks.Add(newTail);
	}
	
	void setScore(){
		score.text = "Score: " + count.ToString ();
	}
}
