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
	public bool isMoving;


	void Start()
	{
		isDown = true;
		isUp = false;
		isMoving = false;
		player = GameObject.FindGameObjectWithTag ("Player");
	}
	
	void Update () 
	{
		if(isDown == true && isUp == false)
		{
		}
		if(isDown == false && isUp == true)
		{
		}
		
		if(isMoving)
		{

		}

		if(isDown == true && isUp == false && isMoving == false && Input.GetKeyDown ("t"))
		{
			StartCoroutine(MoveUp (transform, downPosition, upPosition, 5));
		}

		if(isDown == false && isUp == true && isMoving == false && Input.GetKeyDown ("t"))
		{
			StartCoroutine(MoveDown (transform, upPosition, downPosition, 5));
		}
	}

	void OnTriggerEnter (Collider other)
	{
		if(other.gameObject == player)
		{
			StartCoroutine (CanMoveUp());
		}
}

	IEnumerator CanMoveUp()
	{
		if (isMoving == true) 
		{
			yield return new WaitForSeconds (5);
			isDown = true;
			isUp = false;
			isMoving = false;
		}
	}
	IEnumerator CanMoveDown()
	{
		if (isMoving == true) 
		{
			yield return new WaitForSeconds (5);
			isDown = false;
			isUp = true;
			isMoving = false;
		}
	}
	

	IEnumerator MoveUp(Transform thisTransform, Vector3 startPos, Vector3 endPos, float time)
	{
		isMoving = true;
		StartCoroutine(CanMoveDown());
		float i = 0.0f;
		float rate = 1.0f / time;
		while(i < 1.0f) 
		{
			i += Time.deltaTime * rate;
			thisTransform.position = Vector3.Lerp (startPos, endPos, i);
			yield return null;
		}
}

	IEnumerator MoveDown(Transform thisTransform, Vector3 endPos, Vector3 startPos, float time)
	{
		isMoving = true;
		StartCoroutine(CanMoveUp());
		float i = 0.0f;
		float rate = 1.0f / time;
		while(i < 1.0f) 
		{
			i += Time.deltaTime * rate;
			thisTransform.position = Vector3.Lerp (endPos, startPos, i);
			yield return null;
		}
	}

}
