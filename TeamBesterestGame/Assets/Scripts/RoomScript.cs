using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    // Use this for initialization
    void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();
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
}
