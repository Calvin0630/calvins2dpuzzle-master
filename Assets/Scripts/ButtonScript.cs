using UnityEngine;
using System.Collections;

public class ButtonScript : MonoBehaviour {
    Collider2D col;
    Vector2 topOfButton;

	// Use this for initialization
	void Start () {
        topOfButton = new Vector2(transform.position.x, transform.position.y + transform.lossyScale.y/2 +.01f);
	}
	
	// Update is called once per frame
	void Update () {

        col = Physics2D.OverlapCircle(topOfButton, transform.lossyScale.x/2 - .2f, 1 << 10);



        if (col != null && isWithinRange(.5f, col.gameObject.transform.position.y - col.bounds.size.y / 2, topOfButton.y))
        {
            //do something
            Debug.Log("nnnnnnnnnnnnnnnnnn");
            

        }
	}

    //methods

    //checks if number x is within plus or minus the range of number y
    public bool isWithinRange(float range, float x, float y)
    {
        bool isInside = false;
        if (x >= (y - range) && x <= (y + range))
        {
            isInside = true;        
        }
        return isInside;
    }
    
    
}
