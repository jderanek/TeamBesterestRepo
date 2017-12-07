using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorScript : MonoBehaviour {

    public bool inConstructionMode;
	public bool doorClosed = true;

	public GameObject door;
    public string myName;
    public RoomScript myRoomScript;

	// Use this for initialization
	void Start () {
        myName = gameObject.name;
        myRoomScript = gameObject.GetComponentInParent<RoomScript>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        
    }

    private void OnMouseOver()
    {
		if (Input.GetMouseButtonDown (1))
		{
			doorClosed = !doorClosed;
			door.gameObject.SetActive(doorClosed);
            if (myName == "NorthDoorButton")
            {
                myRoomScript.northDoor = !myRoomScript.northDoor;
            }
            else if (myName == "SouthDoorButton")
            {
                myRoomScript.southDoor = !myRoomScript.southDoor;
            }
           else if (myName == "EastDoorButton")
            {
                myRoomScript.eastDoor = !myRoomScript.eastDoor;
            }
            else if (myName == "WestDoorButton")
            {
                myRoomScript.westDoor = !myRoomScript.westDoor;
            }
            myRoomScript.UpdateNeighbors();

        } 
	
    }

}
