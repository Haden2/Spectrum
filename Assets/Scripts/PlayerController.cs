using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerController : MonoBehaviour {

	public Elevator elevator;
	public Inventory inventory;
	public bool elevatorDoor;
	public bool inElevator;

	void Start()
	{
		elevator = GameObject.FindGameObjectWithTag ("Elevator").GetComponent<Elevator> ();
		inventory = GetComponent<Inventory> ();
	}

	void OnTriggerEnter (Collider other)
	{
		if(other.gameObject.name == "Elevator")
		{
			elevatorDoor = true;
		}
		if(elevatorDoor && inventory.activeElevatorKey)
		{
			print ("Engage the Doors motherfucker");
			elevator.canActivate = true;
		}
	}
	void OnTriggerExit(Collider other)
	{
		if(other.gameObject.name == "Elevator")
		{
		elevatorDoor = false;
		}
	}

	void Update()
	{
		if(elevatorDoor && inventory.activeElevatorKey && elevator.canActivate == true && Input.GetKeyDown("e"))
		{
			elevator.closeDoor = false;
			elevator.openDoor = true;
		}
	}
}
