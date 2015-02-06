using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerController : MonoBehaviour {

	//public float speed;
	public Text pressE;
	public Color pickupText = new Color(1f, 0f, 0f, 1f);
	
	// Update is called once per frame
/*	void FixedUpdate () 
	{
		float moveHorizontal = Input.GetAxis ("Horizontal");
		float moveVertical = Input.GetAxis ("Vertical");

		Vector3 movement = new Vector3 (moveHorizontal, 0.0f, moveVertical);
		rigidbody.AddForce (movement * speed * Time.deltaTime);

	}
*/
	void OnTriggerEnter(Collider other) 
	{
		if (other.gameObject.tag == "PickUp") 
		{
			pressE.color = pickupText;
			//other.gameObject.SetActive(false);
		}
	}
}
