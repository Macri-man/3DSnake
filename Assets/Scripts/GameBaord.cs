using UnityEngine;
using System.Collections;
using System.Text.RegularExpressions;
using UnityEngine.UI;
using System.Linq;

public class GameBaord : MonoBehaviour {

	GameObject[] menuObjects;
	GameObject control;

	private Rotater objects;
	private PlayerController player;

	private Text score;
	private Text win;
	private float count;
	private bool gameover;

	// Use this for initialization
	void Start () {


		menuObjects = GameObject.FindGameObjectsWithTag("menuObjects");
		//control = GameObject.FindGameObjectWithTag("controlObjects");

		hideMenu ();
		//hideControls ();


		gameover = false;

		win = GameObject.Find ("Win").GetComponent<Text>();
		score = GameObject.Find ("Score").GetComponent<Text>();

		Debug.Log (win);

		player = GameObject.Find ("Player").GetComponent<PlayerController> ();

		count = 0;
		setScore ();
		win.text = "";
	
	}

	// Update is called once per frame
	void Update () {

		if (gameover) {
			if (Input.GetKeyDown (KeyCode.P)) {
				if (Time.timeScale == 1) {
					Time.timeScale = 0;
					showMenu ();
				} else if (Time.timeScale == 0) {
					Time.timeScale = 1;
					hideMenu ();
				}
			}

			if (Input.GetKeyDown (KeyCode.O)) {
				if (Time.timeScale == 1) {
					Time.timeScale = 0;
					showControls ();
				} else if (Time.timeScale == 0) {
					Time.timeScale = 1;
					hideControls ();
				}
			}
		}
		
	}

	void OnTriggerEnter(Collider other){

		switch (other.gameObject.tag) {
		case "Orb1":
			other.GetComponentInParent<OrbCreation> ().gone = true;
			other.GetComponentInParent<OrbCreation> ().count = 1;
			Destroy (other.gameObject);
			count = count + 1;
			setScore ();

			Debug.Log ("Ate Orb1 Create Snake");

			//gameObject.SendMessage("AddTail");
			player.AddTail ();
			break;
		case "Orb2":
			other.GetComponentInParent<OrbCreation> ().gone = true;
			other.GetComponentInParent<OrbCreation> ().count = 1;
			Destroy (other.gameObject);
			count = count + 2;
			setScore ();

			Debug.Log ("Ate Orb2 Create Snake");

			//gameObject.SendMessage("AddTail");
			player.AddTail ();
			break;
		case "Orb3":
			other.GetComponentInParent<OrbCreation> ().gone = true;
			other.GetComponentInParent<OrbCreation> ().count = 1;
			Destroy (other.gameObject);
			count = count + 3;
			setScore ();

			Debug.Log ("Ate Orb3 Create Snake");

			//gameObject.SendMessage("AddTail");
			player.AddTail ();
			break;
		case "Collision":
			win.text = "LOSE!";
			StopAllCoroutines ();
			Time.timeScale = 0;
			showMenu();
			break;
		default:
			break;
		}

		if (count >= 10) {
			win.text = "WIN!";
			StopAllCoroutines ();
			player.StopAllCoroutines ();
			Time.timeScale = 0;
			showMenu();
		}
	}

	public void hideMenu(){
		foreach(GameObject g in menuObjects){
			g.SetActive(false);
		}
	}

	public void showMenu(){
		foreach(GameObject g in menuObjects){
			g.SetActive(true);
		}
	}

	public void showControls(){
		control.SetActive (true);
	}

	public void hideControls(){
		control.SetActive (false);
	}

	public void Reload(){
		Time.timeScale = 1;
		Application.LoadLevel(Application.loadedLevel);
	}

	void setScore(){
		score.text = "Score: " + count.ToString ();
	}
}
