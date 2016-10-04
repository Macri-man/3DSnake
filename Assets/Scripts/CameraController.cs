using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {

	public GameObject player;

	private Vector3 offset;

	public float horizontalSpeed = 2.0F;
	public float verticalSpeed = 2.0F;

	private bool mousedown = false;

	float rotationY = 0.0F;
	float rotationX = 0.0F;

	public float senY = 1000.0F;
	public float senX = 1000.0F;

	public float minY = -45.0f;
	public float maxY = 45.0f;

	public float minX = -360.0F; 
	public float maxX = 360.0F;

	// Use this for initialization
	void Start () {
		offset = transform.position - player.transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void LateUpdate () {
		transform.position = player.transform.position + offset;
		if (Input.GetMouseButton (0) || Input.GetMouseButton (1)) {
			if (gameObject.CompareTag ("FPCamera")) {
				rotationX += Input.GetAxis ("Mouse X") * senX * Time.deltaTime;
				rotationY += Input.GetAxis ("Mouse Y") * senY * Time.deltaTime;
			}
			
			if (gameObject.CompareTag ("MainCamera")) {
				rotationX += Input.GetAxis ("Mouse X") * senX * Time.deltaTime;
				rotationY += Input.GetAxis ("Mouse Y") * senY * Time.deltaTime;
			}
			
		}

		rotationX += Input.GetAxis ("Horizontal") * senX * Time.deltaTime;
		rotationY += Input.GetAxis ("Vertical") * senY * Time.deltaTime;

		rotationY = Mathf.Clamp (rotationY, minY, maxY);
		transform.localEulerAngles = new Vector3 (-rotationY, rotationX, 0);

	}
}
