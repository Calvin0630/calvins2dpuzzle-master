using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Player2Script : MonoBehaviour {
	
	
	bool jumping1 = false;
	bool jumping2 = false;
	float jumpStrength = 9;
	float distanceFromGround;
	float walkSpeed = 4;
	float runningSpeed = 4;
	float initialRunningSpeed;
	float maxRunSpeed = 6;
	public static float speedOfLight = 50;
	public static float lightDelay = 5;
	float lightTimer = lightDelay;
	public static List<GameObject> lightList = new List<GameObject>();
	Collider2D col;

	// Use this for initialization
	void Start () {
		initialRunningSpeed = runningSpeed;
	}
	
	// Update is called once per frame
	void Update () {



		if (Input.GetButton ("Y")) {
			gameObject.rigidbody2D.velocity = new Vector2 (runningSpeed * Input.GetAxis ("LX"), gameObject.rigidbody2D.velocity.y);
			if (runningSpeed<maxRunSpeed) {
				runningSpeed+=.2f;
			}
		} 
		else if (gameObject.rigidbody2D.velocity.x == 0 || !Input.GetButton ("Y")) { 
			runningSpeed = initialRunningSpeed;
		}
		
		transform.position = Vector3.MoveTowards(transform.position, transform.position + new Vector3(4f,0,0) * Input.GetAxis("LX"), Time.deltaTime * 4);

		//gameObject.rigidbody2D.velocity = new Vector2(4 * Input.GetAxis("LX"), gameObject.rigidbody2D.velocity.y);


		//controls for jumping
		if ( Input.GetButtonDown("B") && !jumping2) {
			gameObject.rigidbody2D.velocity = new Vector2(gameObject.rigidbody2D.velocity.x, jumpStrength);
				if (jumping1) {
					jumping2 = true;
				}
			jumping1 = true;
			
		}




		//controls for shooting light
		if (Input.GetButton ("RB") && ((Input.GetAxis ("RX") + Input.GetAxis ("RY") > 0) || (Input.GetAxis ("RX") + Input.GetAxis ("RY") < 0)) && lightTimer > lightDelay ) {
			GameObject prefab = (GameObject)Resources.Load ("Player/ballOfLight");
			GameObject light = (GameObject)Instantiate (prefab, transform.position, Quaternion.identity);
			Vector2 direction = new Vector2(Input.GetAxis ("RX"), Input.GetAxis ("RY"));
			light.rigidbody2D.velocity = setMagnitude(speedOfLight,direction);
			lightList.Add (light);
			lightTimer = 0;
		}



		if (lightList.Count != 0) {
			for (int i=0;i < lightList.Count;i++) {

				//checks if light is off the screen. If so it despawns them.
				if (Mathf.Abs(lightList[i].transform.position.y)> Camera.main.orthographicSize || Mathf.Abs(lightList[i].transform.position.x)> Camera.main.orthographicSize * Camera.main.aspect) {
					Destroy(lightList[i]);
					lightList.RemoveAt(i);
					continue;
				}
				//check if the light is colliding with a wall. if so it despawns it.
				col = Physics2D.OverlapCircle (lightList[i].transform.position, .5f, 1 << 9);
				if (col != null) {
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