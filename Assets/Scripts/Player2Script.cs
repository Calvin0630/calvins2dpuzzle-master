using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Player2Script : MonoBehaviour {
	
	
	bool jumping1 = false;
	bool jumping2 = false;
	float distanceFromGround;
	float sprinting = 1;
	public static float lightDelay = 5;
	float lightTimer = lightDelay;
	public static List<GameObject> lightList = new List<GameObject>();
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {



		if (Input.GetButton ("LJoystickButton")) {
			sprinting = 2f;
		} else {
			sprinting = 1;		
		}

		transform.position = Vector3.MoveTowards(transform.position, transform.position + new Vector3(4f,0,0) * sprinting * Input.GetAxis("LX"), Time.deltaTime * 4);
			

		
		//controls for jumping
		if ( Input.GetButtonDown("A") && !jumping2) {
			gameObject.rigidbody2D.AddForce(new Vector2(0,256.0f));
				if (jumping1) {
					jumping2 = true;
				}
			jumping1 = true;
			
			}




		//controls for shooting light
		if (Input.GetButton ("RB") && ((Input.GetAxis ("RX") + Input.GetAxis ("RY") > 0) || (Input.GetAxis ("RX") + Input.GetAxis ("RY") < 0)) && lightTimer > lightDelay ) {
			GameObject prefab = (GameObject)Resources.Load ("ballOfLight");
			GameObject light = (GameObject)Instantiate (prefab, transform.position, Quaternion.identity);
			Vector2 direction = new Vector2(Input.GetAxis ("RX"), Input.GetAxis ("RY"));
			light.rigidbody2D.AddForce (setMagnitude(.5f,direction));
			lightList.Add (light);
			lightTimer = 0;
		}

		//checks if light is off the screen. If so it despawns them.
		if (lightList.Count != 0) {
			for (int i=0;i < lightList.Count;i++) {
				if (Mathf.Abs(lightList[i].transform.position.y)> Camera.main.orthographicSize || Mathf.Abs(lightList[i].transform.position.x)> Camera.main.orthographicSize * Camera.main.aspect) {
					Destroy(lightList[i]);
					lightList.RemoveAt(i);
				}
			}
		}
	}
	
	void FixedUpdate(){

		lightTimer += 1;

		Vector2 feet = new Vector2 (transform.position.x, transform.position.y-1.8f);
		RaycastHit2D hit = Physics2D.Raycast (feet, -Vector2.up);
		if (hit.collider != null)
		{
			distanceFromGround = Mathf.Abs(hit.point.y - transform.position.y);
		}

		if (distanceFromGround <= 1.9f && jumping2 ){
			jumping1 = false;
			jumping2 = false;
		}


	}

	public float getMagnitude(Vector2 vector) {
		float magnitude;
		magnitude = Mathf.Sqrt(vector.x*vector.x + vector.y*vector.y);
		return magnitude;
	}

	public Vector2 setMagnitude(float magnitude, Vector2 vector) {
		float ratio;
		ratio = Mathf.Sqrt(magnitude*magnitude/(vector.x*vector.x + vector.y*vector.y));
		Vector2 result = new Vector2(vector.x*ratio , vector.y*ratio);
		return result;
	}

	public Vector3 getWorldMouseCoordinates() {
		Vector3 mouse = new Vector3 (Input.mousePosition.x, Input.mousePosition.y, 0);
		Vector3 mousePos = Camera.main.ScreenToViewportPoint (mouse);
		Vector3 mousePos1 = new Vector3 (2f * (mousePos.x - .5f), 2f * (mousePos.y - .5f));
		Vector3 mouseUnit = new Vector3 (Camera.main.aspect * Camera.main.orthographicSize * mousePos1.x, Camera.main.orthographicSize * mousePos1.y);
		return mouseUnit;
	}

}