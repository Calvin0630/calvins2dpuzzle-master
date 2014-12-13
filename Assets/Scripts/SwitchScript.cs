using UnityEngine;
using System.Collections;

public class SwitchScript : MonoBehaviour {

	int timer = 0;
	Collider2D col;
	float affectedRadius = 1;
	public static Sprite off = Resources.Load ("switchOff", typeof(Sprite)) as Sprite;
	public static Sprite on = Resources.Load ("switchOn", typeof(Sprite)) as Sprite;
	

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		timer += 1;

		col = Physics2D.OverlapCircle (transform.position, affectedRadius, 1 << 8);
		//Debug.Log (col.gameObject.name);

		if (col != null) {
			if (col.gameObject.name == "ballOfLight(Clone)" ) {
				gameObject.GetComponent<SpriteRenderer> ().sprite = on;
				timer=0;
			}
		}
		else if (timer > 10){
			gameObject.GetComponent<SpriteRenderer> ().sprite = off;
		}
	}
}
