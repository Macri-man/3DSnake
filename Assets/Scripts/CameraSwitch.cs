using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using System.Linq;
using System.Text.RegularExpressions;

public class CameraSwitch : MonoBehaviour {

	private Camera[] cams;

	private int curCamera;

	private Camera fpcamera;
	private Camera maincamera;


	// Use this for initialization
	void Start () {
		cams = Camera.allCameras;
		cams = removePortalCams (cams);

		/*for (int i = 0; i < cams.Length; i++) {
			if (cams [i].CompareTag ("MainCamera")) {
				maincamera = cams [i];
			}else if(cams [i].CompareTag ("FPCamera")){
				fpcamera = cams [i];
			}else {
				cams [i].gameObject.SetActive (false);
			}
		}*/

		for (int i = 0; i < cams.Length; i++) {
			cams [i].gameObject.SetActive (false);
		}

		/*for (int i = 0; i < cams.Length; i++) {
			//Debug.Log (cams[i].tag);
		}*/

		curCamera = 1;
	}

	Camera[] removePortalCams(Camera[] cams){
		List<Camera> camera = new List<Camera> (cams);
		for (int i = camera.Count-1; i >= 0; i--) {
			if (cams [i].name == "PortalCamera") {
				camera.Remove(camera[i]);
			}else if(cams [i].name == "MainCamera"){
				maincamera = camera[i];
				maincamera.gameObject.SetActive (true);
				maincamera.enabled = true;
				camera.Remove(camera[i]);
			}else if(cams [i].name == "FPCamera"){
				fpcamera = camera[i];
				fpcamera.gameObject.SetActive (true);
				fpcamera.enabled = true;
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
			cams [curCamera].gameObject.SetActive (false);
			fpcamera.gameObject.SetActive (false);
			maincamera.gameObject.SetActive (true);
			break;
		case "b":
			cams [curCamera].gameObject.SetActive (false);
			maincamera.gameObject.SetActive (false);
			fpcamera.gameObject.SetActive (true);
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
		//Debug.Log ("PrevCamera");
		int num = 0;
		if (curCamera - 1 < 0) {
			num = cams.Count () - 1; 
		} else {
			num = curCamera - 1;
		}
		//Debug.Log (num);
		//Debug.Log (cams.Count ()-1);
		if (cams[num] != null) {
			cams[curCamera].gameObject.SetActive(false);
			cams[num].gameObject.SetActive(true);
		}
		curCamera = num;
		//Debug.Log ("close PrevCamera");

	}

	private void nextCamera(){
		//Debug.Log ("NextCamera");
		int num = 0;
		if (curCamera >= cams.Count()-1) {
			num = 0; 
		}else {
			num = curCamera + 1;
		}
		//Debug.Log (num);
		//Debug.Log (cams.Count ()-1);
		if (cams[num] != null) {
			cams[curCamera].gameObject.SetActive(false);
			cams[num].gameObject.SetActive(true);
		}
		curCamera = num;
		//Debug.Log ("Close NextCamera");

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
