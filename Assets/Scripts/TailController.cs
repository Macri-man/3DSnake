using UnityEngine;
using System.Collections;

public class TailController : MonoBehaviour {

	public GameObject prev;
	public GameObject next;


	public int activeState;

	Vector3 offset;

	private Vector3 holdtransform;
	private Quaternion holdrotation;

	enum States
	{
		move=1,
		turn=2,
		jump=3,
		wait=4
	}


	// Use this for initialization
	void Start () {
		activeState = 1;
	}
	
	// Update is called once per frame
	void Update () {

		/*if (prev != null) {
			activeState = prev.GetComponent<TailController> ().activeState;
		}*/

		switch (activeState) {
		case 1:
			offset =  transform.position - prev.transform.position;
			offset = offset.normalized * transform.localScale.y;  
			transform.position = offset + prev.transform.position;
			break;
		case 2:
			//Debug.Log (2);
			//Debug.Log (gameObject);
			//transform.position = prev.transform.position;
			//transform.rotation = prev.transform.rotation;
			//Debug.Log (transform.position);
			//Debug.Log (transform.rotation);
			if (next != null) {
				next.transform.position = this.transform.position;
				next.transform.rotation = this.transform.rotation;
				next.GetComponent<TailController> ().activeState = 2;
			}
			//activeState = prev.GetComponent<TailController> ().activeState;
			activeState = 4;
			break;
		case 3:
			
			if (next != null) {
				next.GetComponent<TailController> ().activeState = 3;
			}
			break;
		case 4:
			//Debug.Log (gameObject);
			holdtransform = prev.transform.position;
			holdrotation = prev.transform.rotation;
			//Debug.Log (Vector3.Magnitude(holdtransform + prev.transform.forward) >= Vector3.Magnitude(prev.transform.position));
			activeState = 5;
			break;
		case 5:
			//Debug.Log (Vector3.Magnitude (holdtransform + prev.transform.forward) );
			//Debug.Log (Vector3.Magnitude (prev.transform.position));
			//Debug.Log ((holdtransform + prev.transform.forward));
			//Debug.Log ((prev.transform.position));
			if ( Vector3.Magnitude((holdtransform + prev.transform.forward)) >= Vector3.Magnitude(prev.transform.position)) {
				
				if (next != null) {
					next.transform.position = this.transform.position;
					next.transform.rotation = this.transform.rotation;
					next.GetComponent<TailController> ().activeState = 4;
				}
				transform.position = holdtransform;
				transform.rotation = holdrotation;
				activeState = 1;
			}
			break;
		default:
			Debug.Log ("Not a Valid State");
			break;
		}

	}

	void OnTriggerEnter(Collider other){
		if (other.gameObject.CompareTag ("Player")) {
			//Debug.Log ("Hit Player");
		}

	}


}
