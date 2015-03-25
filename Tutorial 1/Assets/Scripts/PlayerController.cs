using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour 
{

	public float speed;

	public GUIText countext;
	
	public GUIText wintext;
	
	private int count;

	void Start()
	{
		count = 0;
		setCountText ();
		wintext.text = "";
	}

	void FixedUpdate()
	{
		float moveHorizontal = Input.GetAxis ("Horizontal");
		float moveVertical   = Input.GetAxis ("Vertical");

		Vector3 movement = new Vector3 ( moveHorizontal, 0.0f, moveVertical );

		GetComponent<Rigidbody>().AddForce ( movement * speed * Time.deltaTime);
	}

	void OnTriggerEnter(Collider other) 
	{

		if (other.gameObject.tag == "PickUp") 
		{
			other.gameObject.SetActive (false);
			count++;
			setCountText ();
		} 

	}

	void setCountText()
	{
		countext.text = "count: " + count.ToString();
		if (count == 12) 
		{
			wintext.text = "E TT'HA' VINTO!";
		}
	}
}