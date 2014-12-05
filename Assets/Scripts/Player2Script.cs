using UnityEngine;
using System.Collections;

public class Player2Script : MonoBehaviour {
	
	
	bool jumping1 = false;
	bool jumping2 = false;
	float distanceFromGround;
	float sprinting = 1;
	public static float lightDelay = 150;
	float lightTimer = lightDelay;
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



		lightTimer += Time.time;
		Debug.Log (lightTimer);
		if (Input.GetButton ("RB") && ((Input.GetAxis ("RX") + Input.GetAxis ("RY") > 0) || (Input.GetAxis ("RX") + Input.GetAxis ("RY") < 0)) && lightTimer > lightDelay ) {
			GameObject prefab = (GameObject)Resources.Load ("ballOfLight");
			GameObject light = (GameObject)Instantiate (prefab, transform.position, Quaternion.identity);
			light.rigidbody2D.AddForce (new Vector2(Input.GetAxis ("RX"), Input.GetAxis ("RY")));
			lightTimer = 0;
		}


	}
	
	void FixedUpdate(){
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

	public Vector2 setMagnitude(float magnitude, Vector2 vector) {
		Vector2 result = new Vector2(0,0);
		return result;
	}

}