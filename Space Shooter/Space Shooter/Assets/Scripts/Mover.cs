using UnityEngine;
using System.Collections;

public class Mover : MonoBehaviour
{
	public float speed;
	
	void Start ()
	{
		GetComponent<Rigidbody>().velocity = transform.forward * speed;
	}

	void Update()
	{
		transform.position = new Vector3 ( 
      		transform.position.x,
		    0.0f,
		    transform.position.z
        );
	}
}