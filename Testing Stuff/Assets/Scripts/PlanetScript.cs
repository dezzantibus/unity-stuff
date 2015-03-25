using UnityEngine;
using System.Collections;

public class PlanetScript : MonoBehaviour {

	public float width;

	public float height;

	public float tilt;

	public float angle;

	public float speed;

	// Update is called once per frame
	void Update () 
	{
		float z = width * Mathf.Sin ( angle * Mathf.PI / 180 );
		float x = width * Mathf.Cos ( angle * Mathf.PI / 180 ) * ( width/height );
		float y = x * tilt / 100;

		transform.position = new Vector3 ( x, y, z );

		angle += speed;// * Time.deltaTime;

		angle = angle % 360;

	}

//	float toRadians()
//	{
//		return angle * Mathf.PI / 180;
//	}

}
