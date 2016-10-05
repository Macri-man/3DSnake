using UnityEngine;
using System.Collections;

public class TailController : MonoBehaviour {

	public GameObject player;

	private Vector3 offset;

	// Use this for initialization
	void Start () {
		player =  GameObject.FindWithTag("Player");
	}
	
	// Update is called once per frame
	void Update () {
		transform.position = player.transform.position;
	}
}
