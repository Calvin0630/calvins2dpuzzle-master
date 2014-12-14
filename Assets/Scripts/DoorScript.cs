using UnityEngine;
using System.Collections;

public class DoorScript : MonoBehaviour {

	Collider2D collider1;
	float radius = .1f;
	
	
	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
	
		
		collider1 = Physics2D.OverlapCircle (transform.position, radius, 1 << 10);

		
		if (collider1 != null) {
			Application.LoadLevel(1);
		}
	}
}
