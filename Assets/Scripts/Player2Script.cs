using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Player2Script : MonoBehaviour {
	
	
	bool jumping1 = false;
	bool jumping2 = false;
	float jumpStrength = 9;
	float distanceFromGround;
	float movementSpeed = 4;
	float initialMovementSpeed;
	float maxRunSpeed = 6;
	public static float speedOfLight = 1000;
	public static float lightDelay = 0;
	float lightTimer = lightDelay;
	int prevLightCount;
	public static List<GameObject> lightList = new List<GameObject>();
	Collider2D col;

	// Use this for initialization
	void Start () {
		initialMovementSpeed = movementSpeed; 


		LineRenderer test = gameObject.AddComponent<LineRenderer>();
		test.SetColors (Color.yellow, Color.yellow);
		test.material.color = Color.yellow;
		test.SetWidth (.25f,.25f);



	}



	// Update is called once per frame
	void Update () {

		//LineRenderer test = GetComponent<LineRenderer>();

		if (Input.GetButton ("LB")) {
			gameObject.rigidbody2D.velocity = new Vector2 (movementSpeed * Input.GetAxis ("LX"), gameObject.rigidbody2D.velocity.y);
			if (movementSpeed<maxRunSpeed) {
				movementSpeed+=.2f;
			}
		} 
		else if (gameObject.rigidbody2D.velocity.x == 0 || !Input.GetButton ("Y")) { 
			movementSpeed = initialMovementSpeed;
		}
		
		//transform.position = Vector3.MoveTowards(transform.position, transform.position + new Vector3(4f,0,0) * Input.GetAxis("LX"), Time.deltaTime * 4);

		gameObject.rigidbody2D.velocity = new Vector2(movementSpeed * Input.GetAxis("LX"), gameObject.rigidbody2D.velocity.y);


		//controls for jumping
		if ( Input.GetButtonDown("A") && !jumping2) {
			gameObject.rigidbody2D.velocity = new Vector2(gameObject.rigidbody2D.velocity.x, jumpStrength);
				if (jumping1) {
					jumping2 = true;
				}
			jumping1 = true;
			
		}





		//despawns light
		LineRenderer test1 = GetComponent<LineRenderer>();
		test1.SetVertexCount (lightList.Count);
		
		
		prevLightCount = lightList.Count;
		
		if (lightList.Count != 0) {
			
			for (int i=0;i < lightList.Count;i++) {
				
				test1.SetPosition(i, lightList[i].transform.position);
				
				//checks if light is off the screen. If so it despawns them.
				if (Mathf.Abs(lightList[i].transform.position.y)> Camera.main.orthographicSize || Mathf.Abs(lightList[i].transform.position.x)> Camera.main.orthographicSize * Camera.main.aspect) {
					Destroy(lightList[i]);
					lightList.RemoveAt(i);
					continue;
				}
				//check if the light is colliding with a wall. if so it despawns it.
				col = Physics2D.OverlapCircle (lightList[i].transform.position, .2f, 1 << 9);
				if (col != null) {
					Destroy(lightList[i]);
					lightList.RemoveAt(i);
					continue;
					
				}
				
				
			}
			
		}




		//controls for shooting light
		if (Input.GetButton ("RB") && ((Input.GetAxis ("RX") + Input.GetAxis ("RY") > 0) || (Input.GetAxis ("RX") + Input.GetAxis ("RY") < 0)) && lightTimer > lightDelay ) {
			GameObject prefab = (GameObject)Resources.Load ("Player/ballOfLight");
			GameObject light = (GameObject)Instantiate (prefab,transform.position, Quaternion.identity);
			Vector2 direction = new Vector2(Input.GetAxis ("RX"), Input.GetAxis ("RY"));
			direction = setMagnitude(speedOfLight,direction);
			light.rigidbody2D.velocity = direction;
			//light.rigidbody2D.AddForce(direction);
			lightList.Add (light);
			lightTimer = 0;


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


	//is called after update
	public void LateUpdate () {

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