using UnityEngine;
using System.Collections;

public class ButtonScript : MonoBehaviour {
    Collider2D col;
    Vector2 topOfButton;
    Vector2 sizeOfOverlap;
	// Use this for initialization
	void Start () {
        topOfButton = new Vector2(transform.position.x, transform.position.y + transform.lossyScale.y/2 +.01f);
        sizeOfOverlap = new Vector2(transform.lossyScale.x - .5f, .5f);
	}
	
	// Update is called once per frame
	void Update () {

        col = Physics2D.OverlapArea(topOfButton + sizeOfOverlap, topOfButton - sizeOfOverlap, 1 << 10);



        if (col != null )
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
