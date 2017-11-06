using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomScript : MonoBehaviour
{

    public List<GameObject> roomMembers = new List<GameObject>();
    private GameObject heldObject;

    public List<GameObject> heroesInRoom = new List<GameObject>();

    public GameObject northRoom;
    public GameObject southRoom;
    public GameObject eastRoom;
    public GameObject westRoom;

    // Use this for initialization
    void Start()
    {

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
}
