using UnityEngine;
using System.Collections;

public class ButtonScript : MonoBehaviour {
	//Vector2 topOfButton = new Vector2(transform.position.x, transform.position.y +.5f);


	// Use this for initialization
	void Start () {
		//so this is here
	}
	
	// Update is called once per frame
	void Update () {
		RaycastHit2D hit = Physics2D.Raycast (new Vector2(transform.position.x, transform.position.y + 1), Vector2.up);
	    if (hit.collider != null) {
			float distance = Mathf.Abs(hit.point.y-transform.position.y);
			if (distance <= 1.1f) {

			}
		}
	}
}
