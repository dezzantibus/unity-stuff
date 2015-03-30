using UnityEngine;
using System.Collections;

public class GravityScript : MonoBehaviour {

	public float density;
	
	public float radius;
	
	public float mass;

	public GravityScript[] celestialBodies;
	
	public Rigidbody rb;

	private double bigG;

	public int isFixed;

	public string velocity;

	public string InitialPosition;

	public string InitialVelocity;

	// Use this for initialization
	void Start () 
	{
		bigG = 3;
		radius = transform.localScale.x / 2;
		mass = Mathf.Pow( radius, 3 ) * Mathf.PI * 4 / 3;
		celestialBodies = GameObject.FindObjectsOfType<GravityScript>();
		rb = GetComponent<Rigidbody> ();
		rb.mass = mass;
		PositionRandomly();
	}
	
	// Update is called once per frame
	void Update () 
	{
		foreach (GravityScript Body in celestialBodies)
		{
			if( Body.name != name )
			{
				calculateAttraction( Body );
			}
		}

		float maxDistance = 200;

		if (transform.position.x > maxDistance || transform.position.x < -maxDistance) 
		{
			PositionRandomly();
		}
		
		if (transform.position.y > maxDistance || transform.position.y < -maxDistance) 
		{
			PositionRandomly();
		}
		
		if (transform.position.z > maxDistance || transform.position.z < -maxDistance) 
		{
			PositionRandomly();
		}

		velocity = rb.velocity.magnitude.ToString();

	}

	void calculateAttraction( GravityScript Body )
	{

		//rigidbody.AddForce((planet.position - transform.position).normalized * acceleration);

		if (isFixed == 0) {
			float distance_x = Body.transform.position.x - transform.position.x;
			float distance_y = Body.transform.position.y - transform.position.y;
			float distance_z = Body.transform.position.z - transform.position.z;

			float distance = Mathf.Sqrt (Mathf.Pow (distance_x, 2) + Mathf.Pow (distance_y, 2));
			distance = Mathf.Sqrt (Mathf.Pow (distance, 2) + Mathf.Pow (distance_z, 2));

			double force = bigG * mass * Body.mass / Mathf.Pow (distance, 2);
			// force = force / mass;

			float force_x = (float)force * distance_x / distance; 
			float force_y = (float)force * distance_y / distance; 
			float force_z = (float)force * distance_z / distance; 

			rb.AddForce (new Vector3 (force_x, force_y, force_z));
		}
	}

	void PositionRandomly()
	{

		if ( isFixed != 1) 
		{

			rb.velocity = Vector3.zero;
			rb.angularVelocity = Vector3.zero;

			float distance = 40;
			float force = 6;

			float position_x = 0;
			float position_y = 0;
			float position_z = 0;

			while( position_x < 20 && position_x > -20 )
			{
				position_x = (Random.value * distance * 2) - distance;
			}
			
//			while( position_y < 20 && position_y > -20 )
//			{
//				position_y = (Random.value * distance * 2) - distance;
//			}
			
			while( position_z < 20 && position_z > -20 )
			{
				position_z = (Random.value * distance * 2) - distance;
			}
			
			transform.position = new Vector3( position_x, position_y, position_z );
			InitialPosition = position_x + " - " + position_y + " - " + position_z;

			float velocity_x = (Random.value * force * 2) - force;
			float velocity_y = 0;//(Random.value * force * 2) - force;
			float velocity_z = (Random.value * force * 2) - force;

			rb.velocity = new Vector3 (velocity_x, velocity_y, velocity_z);
			InitialVelocity = velocity_x + " - " + velocity_y + " - " + velocity_z;

		}
	}

	void OnTriggerEnter( Collider Body )
	{
		//if (Body.gameObject.isFixed == 1) {
			PositionRandomly ();
		//} 
//		else if( isFixed != 1 )  
//		{
//			if( Body.radius < radius )
//			{
//				AbsorbMass( Body.gameObject );
//			}
//			else if ( Body.gameObject.rb.velocity < rb.velocity )
//			{
//				AbsorbMass( Body.gameObject );
//			}
//		}
	}

//	void AbsorbMass( GameObject Body )
//	{
//		mass += Body.mass;
//	}

}
