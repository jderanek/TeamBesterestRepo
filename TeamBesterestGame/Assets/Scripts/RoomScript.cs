﻿using System.Collections;
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
                print(roomMembers[0].GetComponent<MonsterScript>().monsterName);
            }
        }
    }

    public void UpdateNeighbors()
    {
		//neighborRooms.Clear ();
        if (gameManager.roomList[myX, myY + 1] != null)
        {
            northRoom = gameManager.roomList[myX, myY + 1];
            northRoom.GetComponent<RoomScript>().southRoom = gameObject;
			print ("stuff");
			neighborRooms.Add(northRoom);
        }

        if (gameManager.roomList[myX, myY - 1] != null)
        {
            southRoom = gameManager.roomList[myX, myY - 1];
            southRoom.GetComponent<RoomScript>().northRoom = gameObject;
			neighborRooms.Add(southRoom);
        }

        if (gameManager.roomList[myX + 1, myY] != null)
        {
            eastRoom = gameManager.roomList[myX + 1, myY];
            eastRoom.GetComponent<RoomScript>().westRoom = gameObject;
			neighborRooms.Add(eastRoom);
        }

        if (gameManager.roomList[myX - 1, myY] != null)
        {
            westRoom = gameManager.roomList[myX - 1, myY];
            westRoom.GetComponent<RoomScript>().eastRoom = gameObject;
			neighborRooms.Add(westRoom);
        }


		if (neighborRooms.Count > 2) {
			neighborRooms.Sort (delegate(GameObject x, GameObject y) {
				return x.GetComponent<RoomScript> ().roomThreat.CompareTo (y.GetComponent<RoomScript> ().roomThreat);
			});
		}


    }

    public void ClearNeighbors()
    {
        if (northRoom != null)
        {
            northRoom = gameManager.roomList[myX, myY + 1];
            northRoom.GetComponent<RoomScript>().southRoom = null;
        }

        if (southRoom != null)
        {
            southRoom = gameManager.roomList[myX, myY - 1];
            southRoom.GetComponent<RoomScript>().northRoom = null;
        }

        if (eastRoom != null)
        {
            eastRoom = gameManager.roomList[myX + 1, myY];
            eastRoom.GetComponent<RoomScript>().westRoom = null;
        }

        if (westRoom != null)
        {
            westRoom = gameManager.roomList[myX - 1, myY];
            westRoom.GetComponent<RoomScript>().eastRoom = null;
        }

        northRoom = null;
        southRoom = null;
        eastRoom = null;
        westRoom = null;
    }
}
