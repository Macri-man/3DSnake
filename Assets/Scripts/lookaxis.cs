using UnityEngine;
using System.Collections;

public class lookaxis : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}


	void OnDrawGizmos()
	{
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

		Gizmos.DrawCube (transform.position,new Vector3(this.transform.localScale.x,this.transform.localScale.y,this.transform.localScale.z));

	}

	private void DrawHelperAtCenter(Vector3 direction, Color color, float scale){
		Gizmos.color = color;
		Vector3 destination = transform.position + direction * scale;
		Gizmos.DrawLine(transform.position, destination);
	}

}
