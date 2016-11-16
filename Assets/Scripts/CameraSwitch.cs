using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using System.Linq;
using System.Text.RegularExpressions;

public class CameraSwitch : MonoBehaviour {

	private Camera[] cams;

	private int curCamera;

	// Use this for initialization
	void Start () {
		cams = Camera.allCameras;
		cams = removePortalCams (cams);


		for (int i = 0; i < cams.Length; i++) {
			if (cams [i].CompareTag ("MainCamera")) {
				cams [i].enabled = true;
				cams [i].gameObject.SetActive (true);
			} else {
				cams [i].enabled = false;
				cams [i].gameObject.SetActive (false);
			}
		}

		/*for (int i = 0; i < cams.Length; i++) {
			//Debug.Log (cams[i].tag);
		}*/

		curCamera = 0;
	}

	Camera[] removePortalCams(Camera[] cams){
		List<Camera> camera = new List<Camera> (cams);
		for (int i = camera.Count-1; i > 0; i--) {
			if (cams [i].name == "PortalCamera") {
				camera.Remove(camera[i]);
			}
		}
		/*
		Debug.Log ("After");
		for (int i = 0; i < camera.Count; i++) {
			Debug.Log (camera[i].name);
		}
		Debug.Log ("done");
		*/
		return camera.ToArray();
	}
	
	// Update is called once per frame
	void Update () {
		
		/*switch (Input.inputString) {
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
		}*/

		switch (Input.inputString) {
		case "v":
			switchCamera (0);
			break;
		case "b":
			switchCamera (1);
			break;
		case "n":
			prevCamera ();
			break;
		case "m":
			nextCamera ();
			break;
		default:
			//Debug.Log(String.Format("Invalid Input String: {0}",int.Parse(Input.inputString)));
			break;
		}
	
	}

	private void prevCamera(){
		int num = Mathf.Abs (curCamera-1) % cams.Count ();
		if (cams[num] != null) {
			cams[curCamera].enabled = false;
			cams[curCamera].gameObject.SetActive(false);
		} else {
			cams[num].enabled = true;
			cams[num].gameObject.SetActive(true);
		}
		curCamera = num;
	}

	private void nextCamera(){
		int num = Mathf.Abs (curCamera+1) % cams.Count ();
		if (cams[num] != null) {
			cams[curCamera].enabled = false;
			cams[curCamera].gameObject.SetActive(false);
		} else {
			cams[num].enabled = true;
			cams[num].gameObject.SetActive(true);
		}
		curCamera = num;
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
