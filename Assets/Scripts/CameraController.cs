using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {

	public GameObject player;

	private GameObject Camera;

	private Vector3 offset;

	public Quaternion startquaternion;

	public float horizontalSpeed = 2.0F;
	public float verticalSpeed = 2.0F;

	private bool mousedown = false;

	public float rotationY = 0.0F;
	public float rotationX = 0.0F;

	public float senY = 100.0F;
	public float senX = 100.0F;

	public float minY;
	public float maxY;

	public float minX = -360.0F; 
	public float maxX = 360.0F;

	public float axisRotation;

	Vector3 directionRot;

	// Use this for initialization
	void Start () {
		axisRotation = 1;
		offset = transform.position - player.transform.position;
		startquaternion = this.transform.rotation;
		minY = -45.0f;
		maxY = 45.0f;
	}
	
	// Update is called once per frame
	void Update () {

		switch (Input.inputString) {
		case "T":
		case "t":
			break;
		case "u":
			break;
		default:
			//Debug.Log(String.Format("Invalid Input String: {0}",int.Parse(Input.inputString)));
			break;
		}
		
	}

	void OnDrawGizmos()
	{
		Color color;
		color = Color.green;
		// local up
		DrawHelperAtCenter(this.transform.up, color, 4f);

		color.g -= 0.5f;
		// global up
		//DrawHelperAtCenter(Vector3.up, color, 2f);

		color = Color.blue;
		// local forward
		DrawHelperAtCenter(this.transform.forward, color, 4f);

		color.b -= 0.5f;
		// global forward
		//DrawHelperAtCenter(Vector3.forward, color, 2f);

		color = Color.red;
		// local right
		DrawHelperAtCenter(this.transform.right, color, 4f);

		color.r -= 0.5f;
		// global right
		//DrawHelperAtCenter(Vector3.right, color, 2f);
	}

	private void DrawHelperAtCenter(Vector3 direction, Color color, float scale){
		Gizmos.color = color;
		Vector3 destination = transform.position + direction * scale;
		Gizmos.DrawLine(transform.position, destination);
	}

	void LateUpdate () {
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

		if (gameObject.CompareTag ("FPCamera")) {
			rotationX += Input.GetAxis ("CameraHorizontal") * senX * Time.deltaTime;
			rotationY += Input.GetAxis ("CameraVertical") * senY * Time.deltaTime;

			//rotationY = Mathf.Clamp (rotationY, minY, maxY);
			//transform.localEulerAngles = new Vector3 (-rotationY, rotationX, 0);


			transform.position = player.transform.position + player.transform.forward;

			//transform.rotation = Quaternion.Euler (-rotationY,0,0) * Quaternion.AngleAxis(rotationX, player.transform.up);
			//rotationY = Mathf.Clamp (rotationY, minY, maxY);
			//transform.rotation = Quaternion.AngleAxis(-rotationY, this.transform.right) * Quaternion.AngleAxis(rotationX, player.transform.up);
			transform.rotation =  Quaternion.AngleAxis(-rotationY, this.transform.right) * Quaternion.AngleAxis (rotationX, player.transform.up) *  this.startquaternion;

		} else {

			transform.position = player.transform.position + player.transform.up;

			rotationX += Input.GetAxis ("CameraHorizontal") * senX * Time.deltaTime;
			rotationY += Input.GetAxis ("CameraVertical") * senY * Time.deltaTime;


			//rotationY = Mathf.Clamp (rotationY, minY, maxY);
			//transform.localEulerAngles = getVectorRot(player.transform.up);
			//transform.rotation = Quaternion.AngleAxis(-rotationY, player.transform.right) * Quaternion.AngleAxis(rotationX, player.transform.up);

			transform.rotation =  Quaternion.AngleAxis(-rotationY, this.transform.right) * Quaternion.AngleAxis (rotationX, player.transform.up) *  this.startquaternion;
		}

	}
}
