using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameStats : MonoBehaviour {

	public Text score;
	public Text win;

	private float count;

	// Use this for initialization
	void Start () {
		win.text = " ";
		count = 0;
		setScore ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void setScore(){
		score.text = "Score: " + count.ToString ();
	}

	void OnTriggerEnter(Collider other){
		if (other.gameObject.CompareTag ("Orb")) {
			Destroy (other.gameObject);
			count = count + 1;
			setScore ();
		}
		if (count >= 1) {
			win.text = "WIN!";
		}
	}
}
