using UnityEngine;
using System.Collections;

public class Player2Script : MonoBehaviour {
	
	
	bool jumping1 = false;
	bool jumping2 = false;
	float distanceFromGround;
	int playerMode = 0;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {


		


		//left and right movement
		
		if (  Input.GetKey(KeyCode.A) ) {
			transform.position = Vector3.MoveTowards(transform.position, transform.position - new Vector3(4f,0,0), Time.deltaTime * 4);
			transform.localScale = new Vector3(2,2,1);
			
		}
		else if ( Input.GetKey(KeyCode.D) ) {
			transform.position = Vector3.MoveTowards(transform.position, transform.position + new Vector3(4f,0,0), Time.deltaTime * 4);
			transform.localScale = new Vector3(-2,2,1);
		}
		
		//controls for jumping
		if (Input.GetKeyDown(KeyCode.W) && !jumping2) {
			gameObject.rigidbody2D.AddForce(new Vector2(0,256.0f));
				if (jumping1) {
					jumping2 = true;
				}
			jumping1 = true;
			
			}


			if (Input.GetMouseButtonDown(0)) {
			
			
			
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

		Debug.Log(distanceFromGround);
		
	}


}