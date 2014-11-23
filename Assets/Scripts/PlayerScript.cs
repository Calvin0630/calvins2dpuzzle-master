using UnityEngine;
using System.Collections;

public class PlayerScript : MonoBehaviour {
	
	
	bool jumping1 = false;
	bool jumping2 = false;
	float distanceFromGround;
	Vector3 mousePos;
	int orbCount = 0;

	//float cameraSize = Camera.main.orthographicSize;
	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {



		//contains all player controls




						//left and right movement
		if (Input.GetKey (KeyCode.LeftArrow)) {
			transform.position = Vector3.MoveTowards (transform.position, transform.position - new Vector3 (4f, 0, 0), Time.deltaTime * 4);
			transform.localScale = new Vector3 (-7, 7, 1);
		
		} else if (Input.GetKey (KeyCode.RightArrow)) {
			transform.position = Vector3.MoveTowards (transform.position, transform.position + new Vector3 (4f, 0, 0), Time.deltaTime * 4);
			transform.localScale = new Vector3 (7, 7, 1);
		}
		
		//controls for jumping
		if (Input.GetKeyDown (KeyCode.UpArrow) && !jumping2) {
			gameObject.rigidbody2D.AddForce (new Vector2 (0, 256.0f));
			if (jumping1) {
				jumping2 = true;
			}
			jumping1 = true;

		}
		

		//instantiates black holes on mouse click
		if (Input.GetMouseButtonDown(0) && orbCount <= 5) {
			GameObject prefab = (GameObject)Resources.Load ("Sphere");
			GameObject clone = (GameObject)Instantiate (prefab, getWorldMouseCoordinates(), Quaternion.identity);
			orbCount++;
					
		}


		//control to delete black hole
		if (Input.GetMouseButtonDown(1) && orbCount > 0) {

		}
		Debug.Log (Camera.main.aspect);


	}
	
	void FixedUpdate(){
		Vector2 feet = new Vector2 (transform.position.x, transform.position.y-1.6f);
		RaycastHit2D hit = Physics2D.Raycast (feet, -Vector2.up);
		if (hit.collider != null)
		{
			distanceFromGround = Mathf.Abs(hit.point.y - transform.position.y);
		}
		
		if (distanceFromGround <= 1.7f && jumping2 ){
			jumping1 = false;
			jumping2 = false;
		}

	}
	
	//methods
	public Vector3 getWorldMouseCoordinates() {
		Vector3 mouse = new Vector3 (Input.mousePosition.x, Input.mousePosition.y, 0);
		Vector3 mousePos = Camera.main.ScreenToViewportPoint (mouse);
		Vector3 mousePos1 = new Vector3 (2f * (mousePos.x - .5f), 2f * (mousePos.y - .5f));
		Vector3 mouseUnit = new Vector3 (Camera.main.aspect * Camera.main.orthographicSize * mousePos1.x, Camera.main.orthographicSize * mousePos1.y);
		return mouseUnit;
	}
}