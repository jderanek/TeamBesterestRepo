using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ConstructionScript : MonoBehaviour
{

    public GameManager gameManager; //public to be assigned in editor //can be looked up with tags or something later
    public GameObject canvas;

    public GameObject constructionIcon; //public to be assigned in editor
    public Sprite selectedConstructionIcon; //public to be assigned in editor

    public GameObject room; //public to be assigned in editor

    public GameObject confirmationBox;

    // Use this for initialization
    void Awake()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();
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
                    if (room.GetComponent<BaseRoom>() != null || gameManager.selectedObjects.Contains(room))
                    {
                        gameManager.roomCount++;
                        int myX = room.GetComponent<BaseRoom>().myX;
                        int myY = room.GetComponent<BaseRoom>().myY;
                        if (myY < 9)
                        {
                            if (gameManager.roomList[myX, myY + 1] == null)
                            {
                                constructionIconHolder = Instantiate(constructionIcon, new Vector3(myX, myY + 1, 0f), Quaternion.identity);
                                gameManager.roomList[myX, myY + 1] = constructionIconHolder;
                                
                                /*
                                EventTrigger trigger = constructionIconHolder.GetComponent<EventTrigger>();
                                EventTrigger.Entry entry = new EventTrigger.Entry();
                                entry.eventID = EventTriggerType.PointerDown;
                                entry.callback.AddListener((eventData) => 
                                {
                                    gameManager.roomsToBuild.Add(constructionIconHolder);
                                    constructionIconHolder.GetComponent<SpriteRenderer>().sprite = selectedConstructionIcon;
                                    print("a");
                                });
                                trigger.triggers.Add(entry);
                                */
                            }
                        }
                        
                        if (myY >= 1)
                        {
                            if (gameManager.roomList[myX, myY - 1] == null)
                            {
                                constructionIconHolder = Instantiate(constructionIcon, new Vector3(myX, myY - 1, 0f), Quaternion.identity);
                                gameManager.roomList[myX, myY - 1] = constructionIconHolder;

                                /*
                                EventTrigger trigger = constructionIconHolder.GetComponent<EventTrigger>();
                                EventTrigger.Entry entry = new EventTrigger.Entry();
                                entry.eventID = EventTriggerType.PointerDown;
                                entry.callback.AddListener((eventData) => 
                                {
                                    gameManager.roomsToBuild.Add(constructionIconHolder);
                                    constructionIconHolder.GetComponent<SpriteRenderer>().sprite = selectedConstructionIcon;
                                    print("b");
                                });
                                trigger.triggers.Add(entry);
                                */
                            }
                        }

                        if (myX < 9)
                        {
                            if (gameManager.roomList[myX + 1, myY] == null)
                            {
                                constructionIconHolder = Instantiate(constructionIcon, new Vector3(myX + 1, myY, 0f), Quaternion.identity);
                                gameManager.roomList[myX + 1, myY] = constructionIconHolder;
                                
                                /*
                                EventTrigger trigger = constructionIconHolder.GetComponent<EventTrigger>();
                                EventTrigger.Entry entry = new EventTrigger.Entry();
                                entry.eventID = EventTriggerType.PointerDown;
                                entry.callback.AddListener((eventData) => 
                                {
                                    gameManager.roomsToBuild.Add(constructionIconHolder);
                                    constructionIconHolder.GetComponent<SpriteRenderer>().sprite = selectedConstructionIcon;
                                    print("c");
                                });
                                trigger.triggers.Add(entry);
                                */
                            }
                        }

                        if (myX >= 1)
                        {
                            if (gameManager.roomList[myX - 1, myY] == null)
                            {
                                constructionIconHolder = Instantiate(constructionIcon, new Vector3(myX - 1, myY, 0f), Quaternion.identity);
                                gameManager.roomList[myX - 1, myY] = constructionIconHolder;    
                                
                                /*
                                EventTrigger trigger = constructionIconHolder.GetComponent<EventTrigger>();
                                EventTrigger.Entry entry = new EventTrigger.Entry();
                                entry.eventID = EventTriggerType.PointerDown;
                                entry.callback.AddListener((eventData) => 
                                {
                                    gameManager.roomsToBuild.Add(constructionIconHolder);
                                    constructionIconHolder.GetComponent<SpriteRenderer>().sprite = selectedConstructionIcon;
                                    print("d");
                                });
                                trigger.triggers.Add(entry);
                                */
                            }
                        }
                    }
                }
            }
        }
        else
        {
            //ClearConstructionIcons();
        }
        foreach (GameObject icon in gameManager.roomList)
        {
            if (icon != null)
            {
                if (icon.CompareTag("Construction Icon"))
                {
                    EventTrigger trigger = icon.GetComponent<EventTrigger>();
                    EventTrigger.Entry entry = new EventTrigger.Entry();
                    entry.eventID = EventTriggerType.PointerDown;
                    entry.callback.AddListener((eventData) =>
                    {
                        gameManager.roomsToBuild.Add(icon);
                        icon.GetComponent<SpriteRenderer>().sprite = selectedConstructionIcon;
                        StartConstruction();
                    });
                    trigger.triggers.Add(entry);
                }
            }
            
        }
    }

    public void ClearConstructionIcons()
    {
        int i = 0;
        int j = 0;
        foreach (GameObject constructionIcon in gameManager.roomList)
        {            
            if (constructionIcon != null)
            {
                if (constructionIcon.CompareTag("Construction Icon")) //maybe constructionIcon.GetComponent<BaseRoom> == null instead?
                {                    
                    gameManager.roomList[j, i] = null;
                    Destroy(constructionIcon);
                }
            }
            //Someone make this better please
            if (i < 9)
            {
                i++;
            }
            else
            {
                i = 0;
                j++;
            }            
        }
    }

    public void SpawnNewRoom(GameObject placeToBuild)
    {
        GameObject newRoom = Instantiate(room, new Vector3(placeToBuild.transform.position.x, placeToBuild.transform.position.y, 0f), Quaternion.identity);
        newRoom.GetComponent<BaseRoom>().myX = (int)newRoom.transform.position.x;
        newRoom.GetComponent<BaseRoom>().myY = (int)newRoom.transform.position.y;
        gameManager.roomList[(int)newRoom.transform.position.x, (int)newRoom.transform.position.y] = newRoom;
        gameManager.CurrencyChanged(-100);
        Destroy(placeToBuild);
        StartConstruction();
    }
}