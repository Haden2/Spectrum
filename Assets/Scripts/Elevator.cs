using UnityEngine;
using System.Collections;

public class Elevator : MonoBehaviour {

	public Vector3 elevator;
	public Vector3 downPosition;
	public Vector3 upPosition;
	GameObject player;
	Vector3 directionUp;
	Vector3 directionDown;
	public bool isDown;
	public bool isUp;
	public bool moving;


	void Start()
	{
		isDown = true;
		isUp = false;
		moving = false;
		Vector3 directionDown = Vector3.down;
		Vector3 directionUp = Vector3.up;
		print ("Started");
		//player = GameObject.FindGameObjectWithTag ("Player");
	}
	
	void Update () 
	{
		//gameObject.transform.Translate (directionUp * Time.smoothDeltaTime);
		if(isDown)
		{
			//StartCoroutine(MoveUp (elevator.transform, down, up, 5));
		}

		if(isUp)
		{

		}

		if(isDown == true && isUp == false && moving == false && Input.GetKeyDown ("t"))
		{
			StartCoroutine(MoveUp (transform, downPosition, upPosition, 5));
		}

		if(isDown == false && isUp == true && moving == false && Input.GetKeyDown ("t"))
		{
			StartCoroutine(MoveDown (transform, upPosition, downPosition, 5));
		}
	}

/*	void OnTriggerEnter (Collider other)
	{
		if(other.gameObject == player)
		{
			StartCoroutine (CanMove());
		}
	}
*/
	IEnumerator CanMoveUp()
	{
		moving = true;
		yield return new WaitForSeconds (5);
		isDown = false;
		isUp = true;
		moving = false;
	}
	IEnumerator CanMoveDown()
	{
		moving = true;
		yield return new WaitForSeconds (5);
		isDown = true;
		isUp = false;
		moving = false;
	}
	

	IEnumerator MoveUp(Transform thisTransform, Vector3 startPos, Vector3 endPos, float time)
	{
		print ("moving locations");
		moving = true;
		float i = 0.0f;
		float rate = 1.0f / time;
		while(i < 1.0f) 
		{
			i += Time.deltaTime * rate;
			thisTransform.position = Vector3.Lerp (startPos, endPos, i);
			StartCoroutine(CanMoveUp());
			yield return null;
		}
}

	IEnumerator MoveDown(Transform thisTransform, Vector3 endPos, Vector3 startPos, float time)
	{
		moving = true;
		float i = 0.0f;
		float rate = 1.0f / time;
		while(i < 1.0f) 
		{
			i += Time.deltaTime * rate;
			thisTransform.position = Vector3.Lerp (endPos, startPos, i);
			StartCoroutine(CanMoveDown());
			yield return null;
		}
	}

}
