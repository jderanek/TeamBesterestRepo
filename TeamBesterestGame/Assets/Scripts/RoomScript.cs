using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SurroundingRoomSorter : IComparer<GameObject>
{
    public int Compare(GameObject x, GameObject y)
    {
        if (x.GetComponent<RoomScript>().roomValue == y.GetComponent<RoomScript>().roomValue)
        {
            return Mathf.RoundToInt(Random.Range(0f, 1f));
        }
        if (x.GetComponent<RoomScript>().roomValue > y.GetComponent<RoomScript>().roomValue) 
        {
            return 0;
        }
        else return 1;
    }
}


public class RoomScript : MonoBehaviour
{
    public GameManager gameManager;

    public List<GameObject> roomMembers = new List<GameObject>();
    private GameObject heldObject;
    public int myX;
    public int myY;

    public List<GameObject> heroesInRoom = new List<GameObject>();

    public GameObject northRoom;
    public GameObject southRoom;
    public GameObject eastRoom;
    public GameObject westRoom;

    public List<GameObject> surroundingRooms = new List<GameObject>();

    public int roomValue;

    // Use this for initialization
    void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();
        UpdateNeighbors();
        surroundingRooms.Add(northRoom);
        surroundingRooms.Add(southRoom);
        surroundingRooms.Add(eastRoom);
        surroundingRooms.Add(westRoom);
        SortSorroundingRooms();
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
                //heldObject.GetComponent<MonsterScript>().myList = roomMembers;
                heldObject.GetComponent<MonsterScript>().myRoom = this.gameObject;
                print(roomMembers[0].GetComponent<MonsterScript>().monsterName);
            }
        }
    }

    public void UpdateNeighbors()
    {
        if (gameManager.roomList[myX, myY + 1] != null)
        {
            northRoom = gameManager.roomList[myX, myY + 1];
            northRoom.GetComponent<RoomScript>().southRoom = gameObject;
        }

        if (gameManager.roomList[myX, myY - 1] != null)
        {
            southRoom = gameManager.roomList[myX, myY - 1];
            southRoom.GetComponent<RoomScript>().northRoom = gameObject;
        }

        if (gameManager.roomList[myX + 1, myY] != null)
        {
            eastRoom = gameManager.roomList[myX + 1, myY];
            eastRoom.GetComponent<RoomScript>().westRoom = gameObject;
        }

        if (gameManager.roomList[myX - 1, myY] != null)
        {
            westRoom = gameManager.roomList[myX - 1, myY];
            westRoom.GetComponent<RoomScript>().eastRoom = gameObject;
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

    public void SortSorroundingRooms()
    {
        SurroundingRoomSorter roomSorter = new SurroundingRoomSorter();
        surroundingRooms.Sort(roomSorter);
    }

    public void AddHeroToRoom(GameObject newHero)
    {
        heroesInRoom.Add(newHero);
    }
    public void RemoveHeroFromRoom(GameObject oldHero)
    {
        heroesInRoom.Remove(oldHero);
    }
}
