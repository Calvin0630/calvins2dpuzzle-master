using UnityEngine;
using System.Collections;

public class PlayerScript : MonoBehaviour {
	
	
	bool jumping1 = false;
	bool jumping2 = false;
	float movementSpeed = 4;
	float initialMovementSpeed;
	float maxRunSpeed = 7;
	float distanceFromGround;
	int orbCount = 0;
	GameObject[] blackholes = new GameObject[5];
	float jumpStrength = 9;


	// Use this for initialization
	void Start () {
		initialMovementSpeed = movementSpeed;
	}
	
	// Update is called once per frame
	void Update () {

		//left and right movement
		if (Input.GetKey (KeyCode.A)) {
			gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(-movementSpeed , GetComponent<Rigidbody2D>().velocity.y);
			transform.localScale = new Vector3 (-7, 7, 1);
		} 
		else if (Input.GetKey (KeyCode.D)) {
			gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(movementSpeed , GetComponent<Rigidbody2D>().velocity.y);
			transform.localScale = new Vector3 (7, 7, 1);
		}
        else { gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(0, gameObject.GetComponent<Rigidbody2D>().velocity.y); }


        // controls for sprinting
		if (Input.GetKey (KeyCode.LeftShift) && movementSpeed < maxRunSpeed) {
			movementSpeed+=.2f;
		} 
		else if (gameObject.GetComponent<Rigidbody2D>().velocity.x == 0 || !Input.GetKey (KeyCode.LeftShift)) { 
			movementSpeed = initialMovementSpeed;
		}



		//controls for jumping
		if (Input.GetKeyDown (KeyCode.W) && !jumping2) {
			//gameObject.rigidbody2D.AddForce (new Vector2 (0, 256.0f));
			gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(gameObject.GetComponent<Rigidbody2D>().velocity.x, jumpStrength);
			if (jumping1) {
				jumping2 = true;
			}
			jumping1 = true;

		}


		

		//instantiates black holes on mouse click
		if (Input.GetMouseButtonDown(0) && orbCount < 5) {
			GameObject prefab = (GameObject)Resources.Load ("Player/sphere");
			GameObject clone = (GameObject)Instantiate (prefab, getWorldMouseCoordinates(), Quaternion.identity);
			blackholes[orbCount] = clone;
			orbCount++;


					
		}

		//control to delete black hole
		if (Input.GetMouseButtonDown(1) && orbCount > 0) {

			for (int i=blackholes.Length-1;i>=0;i--) {
				if (blackholes[i] != null) {
					Destroy(blackholes[i]);
					break;
				}
			}
			orbCount--;
		}


	}
	
	void FixedUpdate(){
		//logic for double jumping
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

	public Vector2 addVectors(Vector2 a, Vector2 b) {
		Vector2 result = new Vector2 (a.x + b.x, a.y + b.y);
		return result;
	}

	public Vector3 getWorldMouseCoordinates() {
		Vector3 mouse = new Vector3 (Input.mousePosition.x, Input.mousePosition.y, 0);
		Vector3 mousePos = Camera.main.ScreenToViewportPoint (mouse);
		Vector3 mousePos1 = new Vector3 (2f * (mousePos.x - .5f), 2f * (mousePos.y - .5f));
		Vector3 mouseUnit = new Vector3 (Camera.main.aspect * Camera.main.orthographicSize * mousePos1.x, Camera.main.orthographicSize * mousePos1.y);
		return mouseUnit;
	}
	public Vector2 distanceFromPlayer(Vector2 position) {
		float differenceY = position.y - transform.position.y;
		float differenceX = position.x - transform.position.x;
		Vector2 distance = new Vector2 (differenceX, differenceY);
		return distance;
	}

	public Vector2 getVectorFromAToB (Vector2 A, Vector2 B) {
		Vector2 result = new Vector2 ((B.x - A.x), (B.y - A.y));
		return result;
	}

	public Vector2 getDistance(Vector2 vector, Vector2 secondVector) {
		float differenceY = Mathf.Abs(vector.y - secondVector.y);
		float differenceX = Mathf.Abs(vector.x - secondVector.x);
		Vector2 distance = new Vector2 (differenceX, differenceY);
		return distance;  
	}
}