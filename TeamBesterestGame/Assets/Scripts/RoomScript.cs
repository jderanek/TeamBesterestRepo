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

	public GameObject northButton;
	public GameObject southButton;
	public GameObject eastButton;
	public GameObject westButton;


	public int roomThreat;

    // Use this for initialization
    void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();
		//UpdateNeighbors ();
		//print(gameManager.roomList);
		//print (myX);
		//gameManager.roomList [myX, myY] = gameObject;
    }

    // Update is called once per frame
    void Update()
    {
		northButton.SetActive(gameManager.inConstructionMode);
		southButton.SetActive(gameManager.inConstructionMode);
		westButton.SetActive(gameManager.inConstructionMode);
		eastButton.SetActive(gameManager.inConstructionMode);
		
    }

	public void Initialize() {

		/*
		if (northRoom != null)
			neighborRooms.Add(northRoom);
		if (southRoom != null)
			neighborRooms.Add(southRoom);
		if (eastRoom != null)
			neighborRooms.Add(eastRoom);
		if (westRoom != null)
			neighborRooms.Add(westRoom);
		*/
		gameManager.roomList[myX, myY] = gameObject;
		UpdateNeighbors();
		//SortSorroundingRooms();
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
                //heldObject.GetComponent<MonsterScript>().myList = roomMembers;
                heldObject.GetComponent<MonsterScript>().myRoom = this.gameObject;
				foreach (GameObject neighbor in neighborRooms) {
					neighbor.GetComponent<RoomScript>().SortNeighbors();
				}
                //print(roomMembers[0].GetComponent<MonsterScript>().monsterName);
            }
        }
    }

    public void UpdateNeighbors()
    {
		if (myY <= 9) {
			if (gameManager.roomList[myX, myY + 1] != null) {
				northRoom = gameManager.roomList[myX, myY + 1];
				northRoom.GetComponent<RoomScript>().southRoom = gameObject;
				northRoom.GetComponent<RoomScript>().neighborRooms.Add (gameObject);
				neighborRooms.Add (northRoom);
			}
		}

		if (myY >= 1) {
			if (gameManager.roomList[myX, myY - 1] != null) {
				southRoom = gameManager.roomList[myX, myY - 1];
				southRoom.GetComponent<RoomScript>().northRoom = gameObject;
				southRoom.GetComponent<RoomScript> ().neighborRooms.Add (gameObject);
				neighborRooms.Add (southRoom);
			}
		}

		if (myX <= 9) {
			if (gameManager.roomList[myX + 1, myY] != null) {
				eastRoom = gameManager.roomList[myX + 1, myY];
				eastRoom.GetComponent<RoomScript>().westRoom = gameObject;
				eastRoom.GetComponent<RoomScript> ().neighborRooms.Add (gameObject);
				neighborRooms.Add (eastRoom);
			}
		}

		if (myX >= 1) {
			if (gameManager.roomList[myX - 1, myY] != null)
				{
				westRoom = gameManager.roomList[myX - 1, myY];
				westRoom.GetComponent<RoomScript>().eastRoom = gameObject;
				westRoom.GetComponent<RoomScript> ().neighborRooms.Add (gameObject);
				neighborRooms.Add (westRoom);
			}
		}
		SortNeighbors ();
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
				else {//if (x.GetComponent<RoomScript>().roomThreat == y.GetComponent<RoomScript>().roomThreat) {
					var rand = UnityEngine.Random.Range(0, 2);
					if (rand == 0) {
						print (rand);
						return -1;
					} else {
						print(rand);
						return 1;
					}
				}
				//return x.GetComponent<RoomScript> ().roomThreat.CompareTo (y.GetComponent<RoomScript> ().roomThreat);
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
