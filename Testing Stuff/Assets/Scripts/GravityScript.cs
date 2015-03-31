using UnityEngine;
using System.Collections;

public class GravityScript : MonoBehaviour {

	public float density;
	
	public float radius;
	
	public float volume;
	
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
		volume = Mathf.Pow( radius, 3 ) * Mathf.PI * 4 / 3;
		mass = density * volume;
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
			if( Body != null && Body.name != name )
			{
				calculateAttraction( Body );
			}
		}

		float maxDistance = 200;

		if (transform.position.x > maxDistance || transform.localPosition.x < -maxDistance) 
		{
			PositionRandomly();
		}
		
		if (transform.localPosition.y > maxDistance || transform.localPosition.y < -maxDistance) 
		{
			PositionRandomly();
		}
		
		if (transform.localPosition.z > maxDistance || transform.localPosition.z < -maxDistance) 
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
			float force = 3;

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

			float velocity_x = ( ( Random.value * force * 2 ) - force ) * mass;
			float velocity_y = 0.0f; //( ( Random.value * force * 2 ) - force ) * mass;
			float velocity_z = ( ( Random.value * force * 2 ) - force ) * mass;

			rb.velocity = new Vector3 (velocity_x, velocity_y, velocity_z);
			InitialVelocity = velocity_x + " - " + velocity_y + " - " + velocity_z;

		}
	}

	void OnTriggerEnter( Collider Body )
	{

		if(Body.gameObject.name == "Sun") 
		{
			PositionRandomly ();
		} 
		else if( isFixed != 1 ) 
		{
			if( Body.gameObject.transform.localScale.x < transform.localScale.x )
			{
				AbsorbMass( Body.gameObject.name );
			}
			else if ( Body.gameObject.transform.localScale.x == transform.localScale.x && Body.gameObject.GetComponent<Rigidbody> ().velocity.magnitude < rb.velocity.magnitude )
			{
				AbsorbMass( Body.gameObject.name );
			}
		}
	}

	void AbsorbMass( string name )
	{
		foreach (GravityScript Body in celestialBodies)
		{
			if( Body != null && Body.name == name )
			{

				rb.velocity = ( Body.rb.velocity * Body.mass ) + ( rb.velocity * mass );

				mass += Body.mass;

				rb.velocity = rb.velocity / mass;

				volume = mass / density;

				radius = Mathf.Pow( ( ( volume * 3 ) / ( 4 * Mathf.PI ) ), 1f / 3f );

				float scale = radius * 2;

				transform.localScale = new Vector3( scale, scale, scale );

				rb.mass = mass;

				Destroy( Body.gameObject );

			}
		}
	}

}
