using UnityEngine;
using System.Collections;

public class Rotator : MonoBehaviour 
{

	public float xStep;
	
	public float yStep;
	
	public float zStep;
	
	// Update is called once per frame
	void Update () 
	{
		transform.Rotate (new Vector3 (xStep, yStep, zStep) * Time.deltaTime);
	}
}
