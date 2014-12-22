using UnityEngine;
using System.Collections;

public class BlackholeScript : MonoBehaviour {

	Vector2 lightToBlackhole;
	Vector2 forceOnLight;
	Vector2 resultVelocity;
	Collider2D[] col; 
	float blackholeStrength = .6f;
	float radiusOfAffectedArea = 20;
	float speedOfLight;   // Player2Script.speedOfLight;

	// Use this for initialization
	void Start () {
		speedOfLight = Player2Script.speedOfLight;
	}
	
	// Update is called once per frame
	void Update () {
		col = Physics2D.OverlapCircleAll(transform.position, radiusOfAffectedArea,1 << 8);

		for (int i=0; i<col.Length; i++) {
			lightToBlackhole = getVectorFromAToB (col[i].gameObject.transform.position, transform.position);
			forceOnLight = ( blackholeStrength * (radiusOfAffectedArea/(getMagnitude(lightToBlackhole)*getMagnitude(lightToBlackhole)) * lightToBlackhole));
			resultVelocity = new Vector2( col[i].gameObject.rigidbody2D.velocity.x + forceOnLight.x , col[i].gameObject.rigidbody2D.velocity.y + forceOnLight.y );
			resultVelocity = setMagnitude(speedOfLight, resultVelocity);
			col[i].gameObject.rigidbody2D.velocity = resultVelocity;
		}

	}




	// methods

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

	public float getMagnitude(Vector2 vector) {
		float magnitude;
		magnitude = Mathf.Sqrt(vector.x*vector.x + vector.y*vector.y);
		return magnitude;
	}

	public Vector2 setMagnitude(float magnitude, Vector2 vector) {
		float ratio;
		ratio = Mathf.Sqrt (magnitude * magnitude / (vector.x * vector.x + vector.y * vector.y));
		Vector2 result = new Vector2 (vector.x * ratio, vector.y * ratio);
		return result;
	}
}
