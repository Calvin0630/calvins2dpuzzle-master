using UnityEngine;
using System.Collections;

public class SwitchScript : MonoBehaviour {


	Collider2D col;
	float affectedRadius = 1;
	//public static Sprite off = Resources.Load ("Environment/switchOff", typeof(Sprite)) as Sprite;
	public static Sprite on;// = Resources.Load ("Environment/switchOn", typeof(Sprite)) as Sprite;
	bool triggered = false;
	

	// Use this for initialization
	void Start () {
		on = Resources.Load ("Environment/switchOn", typeof(Sprite)) as Sprite;
	}
	
	// Update is called once per frame
	void Update () {

		col = Physics2D.OverlapCircle (transform.position, affectedRadius, 1 << 8);
		//Debug.Log (col.gameObject.name);

		if (col != null && !triggered) {
			if (col.gameObject.name == "ballOfLight(Clone)" ) {
				gameObject.GetComponent<SpriteRenderer> ().sprite = on;
				GameObject clone = (GameObject)Instantiate ((GameObject)Resources.Load ("Environment/door"), new Vector3(20f,-8.5f,0f), Quaternion.identity);
				triggered = true;
			}
		}

	}
}
