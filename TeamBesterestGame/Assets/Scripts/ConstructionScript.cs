using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ConstructionScript : MonoBehaviour
{

    public GameManager gameManager; //public to be assigned in editor //can be looked up with tags or something later

    public GameObject constructionIcon; //public to be assigned in editor

    public GameObject room; //public to be assigned in editor

    // Use this for initialization
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void StartConstruction()
    {
        GameObject constructionIconHolder;
        if (gameManager.inConstructionMode)
        {
            foreach (GameObject room in gameManager.roomList)
            {
                if (room != null)
                {
                    if (room.CompareTag("Room") || room.CompareTag("Spawn Room") || room.CompareTag("Boss Room"))
                    {
                        int myX = room.GetComponent<RoomScript>().myX;
                        int myY = room.GetComponent<RoomScript>().myY;
                        if (myY < 9)
                        {
                            if (gameManager.roomList[myX, myY + 1] == null)
                            {
                                constructionIconHolder = Instantiate(constructionIcon, new Vector3(myX, myY + 1, 0f), Quaternion.identity);
                                gameManager.roomList[myX, myY + 1] = constructionIconHolder;

                                EventTrigger trigger = constructionIconHolder.GetComponent<EventTrigger>();
                                EventTrigger.Entry entry = new EventTrigger.Entry();
                                entry.eventID = EventTriggerType.PointerDown;
                                entry.callback.AddListener((eventData) => { SpawnNewRoom(gameManager.roomList[myX, myY + 1]); });
                                trigger.triggers.Add(entry);
                            }
                        }

                        if (myY >= 1)
                        {
                            if (gameManager.roomList[myX, myY - 1] == null)
                            {
                                constructionIconHolder = Instantiate(constructionIcon, new Vector3(myX, myY - 1, 0f), Quaternion.identity);
                                gameManager.roomList[myX, myY - 1] = constructionIconHolder;

                                EventTrigger trigger = constructionIconHolder.GetComponent<EventTrigger>();
                                EventTrigger.Entry entry = new EventTrigger.Entry();
                                entry.eventID = EventTriggerType.PointerDown;
                                entry.callback.AddListener((eventData) => { SpawnNewRoom(gameManager.roomList[myX, myY - 1]); });
                                trigger.triggers.Add(entry);
                            }
                        }

                        if (myX < 9)
                        {
                            if (gameManager.roomList[myX + 1, myY] == null)
                            {
                                constructionIconHolder = Instantiate(constructionIcon, new Vector3(myX + 1, myY, 0f), Quaternion.identity);
                                gameManager.roomList[myX + 1, myY] = constructionIconHolder;

                                EventTrigger trigger = constructionIconHolder.GetComponent<EventTrigger>();
                                EventTrigger.Entry entry = new EventTrigger.Entry();
                                entry.eventID = EventTriggerType.PointerDown;
                                entry.callback.AddListener((eventData) => { SpawnNewRoom(gameManager.roomList[myX + 1, myY]); });
                                trigger.triggers.Add(entry);
                            }
                        }

                        if (myX >= 1)
                        {
                            if (gameManager.roomList[myX - 1, myY] == null)
                            {
                                constructionIconHolder = Instantiate(constructionIcon, new Vector3(myX - 1, myY, 0f), Quaternion.identity);
                                gameManager.roomList[myX - 1, myY] = constructionIconHolder;

                                EventTrigger trigger = constructionIconHolder.GetComponent<EventTrigger>();
                                EventTrigger.Entry entry = new EventTrigger.Entry();
                                entry.eventID = EventTriggerType.PointerDown;
                                entry.callback.AddListener((eventData) => { SpawnNewRoom(gameManager.roomList[myX - 1, myY]); });
                                trigger.triggers.Add(entry);
                            }
                        }
                    }
                }
            }
        }
        else
        {
            foreach (GameObject constructionIcon in gameManager.roomList)
            {
                if (constructionIcon != null)
                {
                    if (constructionIcon.CompareTag("Construction Icon"))
                    {
                        Destroy(constructionIcon);
                    }
                }
            }
        }
    }

    public void SpawnNewRoom(GameObject placeToBuild)
    {
        GameObject newRoom = Instantiate(room, new Vector3(placeToBuild.transform.position.x, placeToBuild.transform.position.y, 0f), Quaternion.identity);
        newRoom.GetComponent<RoomScript>().myX = (int)newRoom.transform.position.x;
        newRoom.GetComponent<RoomScript>().myY = (int)newRoom.transform.position.y;
        gameManager.roomList[(int)newRoom.transform.position.x, (int)newRoom.transform.position.y] = newRoom;
        Destroy(placeToBuild);
        StartConstruction();
    }
}