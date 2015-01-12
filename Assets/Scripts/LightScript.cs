/**using UnityEngine;
using System.Collections;

public class LightScript : MonoBehaviour {

	Collision col;

	void OnCollisionEnter (Collision col) {
		if (col.GameObject.name == "tile") {
			Destroy (gameObject);
		}
	}

}
*/

using UnityEngine;
using System.Collections;

public class LightScript : MonoBehaviour
{
	void OnCollisionEnter (Collision col)
	{
		if(col.gameObject.layer == LayerMask.NameToLayer("Environment"))
		{
			Destroy(col.gameObject);
			Debug.Log ("It worked");
		}
	}
}