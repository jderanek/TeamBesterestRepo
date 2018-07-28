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
            for (int x = 0; x < 10; x++)
            {
                for (int y = 0; y < 10; y++)
                {
                    if (gameManager.roomList[x, y] != null)
                    {
                        GameObject room = gameManager.roomList[x, y];
                        if (room.GetComponent<BaseRoom>() != null || gameManager.selectedObjects.Contains(room))
                        {
                            //gameManager.roomCount++;
                            if (y < 9)
                            {
                                if (gameManager.roomList[x, y + 1] == null)
                                {
                                    constructionIconHolder = Instantiate(constructionIcon, new Vector3(x, y + 1, 0f), Quaternion.identity);
                                    gameManager.roomList[x, y + 1] = constructionIconHolder;
                                }
                            }

                            if (y >= 1)
                            {
                                if (gameManager.roomList[x, y - 1] == null)
                                {
                                    constructionIconHolder = Instantiate(constructionIcon, new Vector3(x, y - 1, 0f), Quaternion.identity);
                                    gameManager.roomList[x, y - 1] = constructionIconHolder;
                                }
                            }

                            if (x < 9)
                            {
                                if (gameManager.roomList[x + 1, y] == null)
                                {
                                    constructionIconHolder = Instantiate(constructionIcon, new Vector3(x + 1, y, 0f), Quaternion.identity);
                                    gameManager.roomList[x + 1, y] = constructionIconHolder;
                                }
                            }

                            if (x >= 1)
                            {
                                if (gameManager.roomList[x - 1, y] == null)
                                {
                                    constructionIconHolder = Instantiate(constructionIcon, new Vector3(x - 1, y, 0f), Quaternion.identity);
                                    gameManager.roomList[x - 1, y] = constructionIconHolder;
                                }
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
                        if (gameManager.selectedObjects.Count == 0 || gameManager.selectedObjects[0].CompareTag("Construction Icon"))
                        {
                            gameManager.selectedObjects.Add(icon);
                            gameManager.roomsToBuild.Add(icon);
                            icon.GetComponent<SpriteRenderer>().sprite = selectedConstructionIcon;
                            StartConstruction();
                        }                        
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
		//Makes sure there's no room at that point already
		if (gameManager.roomList [(int)placeToBuild.transform.position.x, (int)placeToBuild.transform.position.y].GetComponent<BaseRoom> () != null)
			return;
		
        GameObject newRoom = Instantiate(room, new Vector3(placeToBuild.transform.position.x, placeToBuild.transform.position.y, 0f), Quaternion.identity);
        newRoom.GetComponent<BaseRoom>().myX = (int)newRoom.transform.position.x;
        newRoom.GetComponent<BaseRoom>().myY = (int)newRoom.transform.position.y;
        newRoom.GetComponent<BaseRoom>().Initialize();
        gameManager.roomList[(int)newRoom.transform.position.x, (int)newRoom.transform.position.y] = newRoom;
		gameManager.roomCount++;
        gameManager.CurrencyChanged(-100);
        Destroy(placeToBuild);
        StartConstruction();
    }
}