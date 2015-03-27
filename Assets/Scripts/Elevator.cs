using UnityEngine;
using System.Collections;

public class Elevator : MonoBehaviour {

	public Transform elevator;
	public Transform downPosition;
	public Transform upPosition;
	GameObject player;
	Vector3 directionUp;
	Vector3 directionDown;
	bool isDown;
	bool isUp;


	void Start()
	{
		Vector3 directionDown = Vector3.down;
		Vector3 directionUp = Vector3.up;
		print ("Started");
		player = GameObject.FindGameObjectWithTag ("Player");
		//StartCoroutine (MoveUp (transform, down, up, 0f));
		//StartCoroutine (MoveDown (transform, up, down, 0f));
	}
	
	void Update () 
	{
		gameObject.transform.Translate (directionUp * Time.smoothDeltaTime);
		if(isDown)
		{
			//StartCoroutine(MoveUp (elevator.transform, down, up, 5));
		}

		if(isUp)
		{

		}

		if(isDown == true && isUp == false && Input.GetKeyDown ("t"))
		{
	//		StartCoroutine(CanMoveUp());
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
		print ("Got here");

		yield return null;
		//yield return StartCoroutine(MoveUp (elevator.transform, down, up, 5));
	}
	
		/*if(isDown == false && isUp == true && Input.GetKeyDown ("t"))
		{
			yield return StartCoroutine(MoveDown (transform, up, down, 1.0f));
		}
*/

	IEnumerator MoveUp(Transform thisTransform, Vector3 startPos, Vector3 endPos, float time)
	{
		isDown = false;
		isUp = true;
		print ("moving locations");
		float i = 0.0f;
		float rate = 1.0f / time;
		while(i < 1.0f) 
		{
			i += Time.deltaTime * rate;
			thisTransform.position = Vector3.Lerp (startPos, endPos, i);
			yield return null;
		}
	}

	/*IEnumerator MoveDown(Transform thisTransform, Vector3 startPos, Vector3 endPos, float time)
	{
		isDown = true;
		isUp = false;
		float i = 0.0f;
		float rate = 1.0f / time;
		while(i < 1.0f) 
		{
			i += Time.deltaTime * rate;
			thisTransform.position = Vector3.Lerp (startPos, endPos, i);
			//yield return StartCoroutine(MoveUp (elevator.transform, down, up, 5));
		}
	}*/

}
