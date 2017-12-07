using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class RoomScript : MonoBehaviour
{
    public GameManager gameManager;

	public List<GameObject> neighborRooms = new List<GameObject>();

    public List<GameObject> roomMembers = new List<GameObject>();
    private GameObject heldObject;
    public int myX;
    public int myY;

    public List<GameObject> heroesInRoom = new List<GameObject>();

    public bool monsterInRoom = false;

    public GameObject northRoom;
    public GameObject southRoom;
    public GameObject eastRoom;
    public GameObject westRoom;

    public bool northDoor;
    public bool southDoor;
    public bool eastDoor;
    public bool westDoor;

    public GameObject northButton;
	public GameObject southButton;
	public GameObject eastButton;
	public GameObject westButton;


	public int roomThreat;

    // Use this for initialization
    void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

	public void Initialize() {
		gameManager.roomList[myX, myY] = gameObject;
		UpdateNeighbors();
	}

    public void ActivateButtons(bool inConstructionMode)
    {
        northButton.SetActive(inConstructionMode);
        southButton.SetActive(inConstructionMode);
        westButton.SetActive(inConstructionMode);
        eastButton.SetActive(inConstructionMode);
    }

    void OnMouseOver()
    {
        heldObject = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>().heldObject;
        if (heldObject != null)
        {
            if (heldObject.CompareTag("Monster") && Input.GetMouseButtonDown(1))
            {
                roomMembers.Add(heldObject);
				roomThreat += heldObject.GetComponent<MonsterScript> ().threatValue;
                monsterInRoom = true;
                heldObject.GetComponent<MonsterScript>().myRoom = this.gameObject;
				foreach (GameObject neighbor in neighborRooms) {
					neighbor.GetComponent<RoomScript>().SortNeighbors();
				}
            }
        }
    }

    public void UpdateNeighbors()
    {
        neighborRooms.Clear();

		if (myY <= 9) {
            if (gameManager.roomList[myX, myY + 1] != null)
            {
                if (northDoor && gameManager.roomList[myX, myY + 1].GetComponent<RoomScript>().southDoor)
                {
                    northRoom = gameManager.roomList[myX, myY + 1];
                    northRoom.GetComponent<RoomScript>().southRoom = gameObject;
                    northRoom.GetComponent<RoomScript>().neighborRooms.Add(gameObject);
                    neighborRooms.Add(northRoom);
                }
            }
        }

		if (myY >= 1) {
            if (gameManager.roomList[myX, myY - 1] != null)
            {
                if (southDoor && gameManager.roomList[myX, myY - 1].GetComponent<RoomScript>().northDoor)
                {
                    southRoom = gameManager.roomList[myX, myY - 1];
                    southRoom.GetComponent<RoomScript>().northRoom = gameObject;
                    southRoom.GetComponent<RoomScript>().neighborRooms.Add(gameObject);
                    neighborRooms.Add(southRoom);
                }
            }
        }

		if (myX <= 9) {
            if (gameManager.roomList[myX + 1, myY] != null)
            {
                if (eastDoor && gameManager.roomList[myX + 1, myY].GetComponent<RoomScript>().westDoor)
                {
                    eastRoom = gameManager.roomList[myX + 1, myY];
                    eastRoom.GetComponent<RoomScript>().westRoom = gameObject;
                    eastRoom.GetComponent<RoomScript>().neighborRooms.Add(gameObject);
                    neighborRooms.Add(eastRoom);
                }
            }
        }

		if (myX >= 1) {
            if (gameManager.roomList[myX - 1, myY] != null)
            {
                if (westDoor && gameManager.roomList[myX - 1, myY].GetComponent<RoomScript>().eastDoor)
                {
                    westRoom = gameManager.roomList[myX - 1, myY];
                    westRoom.GetComponent<RoomScript>().eastRoom = gameObject;
                    westRoom.GetComponent<RoomScript>().neighborRooms.Add(gameObject);
                    neighborRooms.Add(westRoom);
                }
            }
        }

		SortNeighbors();
    }

	public void SortNeighbors() {
		if (neighborRooms.Count >= 2) {
			neighborRooms.Sort (delegate(GameObject x, GameObject y) {
				if (x.GetComponent<RoomScript>().roomThreat > y.GetComponent<RoomScript>().roomThreat) {
					return -1;
				}
				else if (x.GetComponent<RoomScript>().roomThreat < y.GetComponent<RoomScript>().roomThreat) {
					return 1;
				}
				else {
					if (UnityEngine.Random.Range(0, 2) == 0) {
						return -1;
					} else {
						return 1;
					}
				}
			});
		}
	}

    public void ClearNeighbors()
    {
        if (northRoom != null)
        {
            northRoom.GetComponent<RoomScript>().southRoom = null;
        }

        if (southRoom != null)
        {
            southRoom.GetComponent<RoomScript>().northRoom = null;
        }

        if (eastRoom != null)
        {
            eastRoom.GetComponent<RoomScript>().westRoom = null;
        }

        if (westRoom != null)
        {
            westRoom.GetComponent<RoomScript>().eastRoom = null;
        }

        northRoom = null;
        southRoom = null;
        eastRoom = null;
        westRoom = null;

		neighborRooms.Clear ();
    }
}
