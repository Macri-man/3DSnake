using UnityEngine;
using System.Collections;

public class CameraSwitch : MonoBehaviour {

	private Camera[] cams;

	// Use this for initialization
	void Start () {
		cams = Camera.allCameras;
		for (int i = 0; i < cams.Length; i++) {
			if (cams[i].CompareTag("MainCamera")) {
				cams[i].enabled = true;
				cams[i].gameObject.SetActive(true);
			} else {
				cams[i].enabled = false;
				cams[i].gameObject.SetActive(false);
			}
		}
		for (int i = 0; i < cams.Length; i++) {
			Debug.Log (cams[i].tag);
		}
	}
	
	// Update is called once per frame
	void Update () {

		switch (Input.inputString) {
		case "1":
		case "2":
		case "3":
		case "4":
		case "5":
		case "6":
		case "7":
		case "8":
			switchCamera ((int.Parse(Input.inputString)-1));
			break;
		default:
			//Debug.Log(String.Format("Invalid Input String: {0}",int.Parse(Input.inputString)));
			break;
		}
	
	}

	private void switchCamera(int keynum){
		for (int i = 0; i < cams.Length; i++) {
			if (cams[i] != null && keynum != i) {
				cams[i].enabled = false;
				cams[i].gameObject.SetActive(false);
			} else {
				cams[i].enabled = true;
				cams[i].gameObject.SetActive(true);
			}
		}
	}
}
