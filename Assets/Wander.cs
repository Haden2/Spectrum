using UnityEngine;
using System.Collections;

/// <summary>
/// Creates wandering behaviour for a CharacterController.
/// </summary>
public class Wander : MonoBehaviour
{
	public float speed = 1;
	public float directionChangeInterval = 1;
	public float maxHeadingChange = 10;
	
	float heading;
	Vector3 targetRotation;
	
	void Awake ()
	{
		// Set random initial rotation
		heading = Random.Range(0, 360);
		transform.eulerAngles = new Vector3(0, heading, 0);
		
		StartCoroutine(NewHeading());
	}
	
	void Update ()
	{
		transform.eulerAngles = Vector3.Slerp(transform.eulerAngles, targetRotation, Time.deltaTime * directionChangeInterval);
		var forward = transform.TransformDirection(Vector3.forward);
		transform.TransformDirection (Vector3.forward * speed);	
	}
	
	/// <summary>
	/// Repeatedly calculates a new direction to move towards.
	/// Use this instead of MonoBehaviour.InvokeRepeating so that the interval can be changed at runtime.
	/// </summary>
	IEnumerator NewHeading ()
	{
		while (true) {
			NewHeadingRoutine();
			yield return new WaitForSeconds(directionChangeInterval);
		}
	}
	
	/// <summary>
	/// Calculates a new direction to move towards.
	/// </summary>
	void NewHeadingRoutine ()
	{

		targetRotation = new Vector3(0, heading, 0);
	}
}
