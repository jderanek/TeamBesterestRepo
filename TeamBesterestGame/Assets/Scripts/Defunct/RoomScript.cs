using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class RoomScript : MonoBehaviour
{
    private GameManager gameManager;

    public List<GameObject> neighborRooms = new List<GameObject>(); //public to assign reference in editor

    public List<GameObject> roomMembers = new List<GameObject>(); //public to be accessed by hero/monster
    private GameObject heldObject;
    public int myX; //public to assign reference in editor
    public int myY; //public to assign reference in editor

    public List<GameObject> heroesInRoom = new List<GameObject>();

    public bool monsterInRoom = false; //public to be accessed by hero/monster
    public bool heroInRoom = false; //public to be accessed by hero/monster

    public GameObject northRoom; //public to assign reference in editor, should be unnecesary in the future
    public GameObject southRoom; //public to assign reference in editor
    public GameObject eastRoom; //public to assign reference in editor
    public GameObject westRoom; //public to assign reference in editor

    public GameObject northDoor; //public to be accessed by door script, may replace with trigger events later
    public GameObject southDoor; //public to assign reference in editor
    public GameObject eastDoor; //public to assign reference in editor
    public GameObject westDoor; //public to assign reference in editor

    public bool northDoorBool; //public to be accessed by other scripts
    public bool southDoorBool; //public to be accessed by other scripts
    public bool eastDoorBool; //public to be accessed by other scripts
    public bool westDoorBool; //public to be accessed by other scripts

    /*
    public GameObject northButton; //public to assign reference in editor
    public GameObject southButton; //public to assign reference in editor
    public GameObject eastButton; //public to assign reference in editor
    public GameObject westButton; //public to assign reference in editor
    */

    public int roomThreat; //public to be accessed by heroes

    public int goldCapacity = 300; //public so it can be tested in editor
    public int currentGold; //public so it can be tested in editor
    public GameObject coin1; //public to assign reference in editor
    public GameObject coin2; //public to assign reference in editor
    public GameObject coin3; //public to assign reference in editor
    public Sprite emptyCoin; //public to assign reference in editor
    public Sprite filledCoin; //public to assign reference in editor

    // Use this for initialization
    void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();
        gameManager.roomList[myX, myY] = gameObject;
        //print(gameObject + " " + myX + ", " + myY);
        //print(gameManager.roomList[myX, myY]);
        UpdateNeighbors();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //gonna get rid of this soon
	public void Initialize() {
		
	}

    public void ActivateButtons(bool inConstructionMode)
    {
        /*
        northButton.SetActive(inConstructionMode);
        southButton.SetActive(inConstructionMode);
        westButton.SetActive(inConstructionMode);
        eastButton.SetActive(inConstructionMode);
        */
    }

    void OnMouseOver()
    {
        if (gameManager.selectedObject != null)
        {
            if (Input.GetMouseButtonDown(1))
            {
				//gameManager.selectedObject.GetComponent<BaseMonster> ().getCurRoom ().roomMembers.Remove (gameManager.selectedObject);
				gameManager.selectedObject.transform.position = new Vector3 (
					gameObject.transform.position.x + UnityEngine.Random.Range (-0.25f, 0.25f),
					gameObject.transform.position.y + UnityEngine.Random.Range (-0.25f, 0.25f),
					0
                    );
                gameManager.AddToDepartment(gameManager.selectedObject, gameManager.dungeonList);
                gameManager.UpdateMonsters();
                gameManager.UpdateDepartments();
				roomMembers.Add (gameManager.selectedObject);
				roomThreat += gameManager.selectedObject.GetComponent<BaseMonster> ().getThreat ();
				monsterInRoom = true;
				gameManager.selectedObject.GetComponent<BaseMonster> ().setCurRoom (this.gameObject);
				foreach (GameObject neighbor in neighborRooms) {
					neighbor.GetComponent<RoomScript> ().SortNeighbors ();
				}
                gameManager.selectedObject = null;
            }
        }
    }

    public void UpdateNeighbors()
    {
        neighborRooms.Clear();

		if (myY < 9) {
            if (gameManager.roomList[myX, myY + 1] != null)
            {
                if (gameManager.roomList[myX, myY + 1].CompareTag("Room") || gameManager.roomList[myX, myY + 1].CompareTag("Spawn Room") || gameManager.roomList[myX , myY + 1].CompareTag("Boss Room"))
                {
                    northRoom = gameManager.roomList[myX, myY + 1];
                    if (!northDoorBool && !gameManager.roomList[myX, myY + 1].GetComponent<RoomScript>().southDoorBool)
                    {
                        northRoom.GetComponent<RoomScript>().southRoom = gameObject;
                        northRoom.GetComponent<RoomScript>().neighborRooms.Add(gameObject);
                        neighborRooms.Add(northRoom);
                    }
                    else northRoom.GetComponent<RoomScript>().neighborRooms.Remove(gameObject);
                }
            }
        }

		if (myY >= 1) {
            if (gameManager.roomList[myX, myY - 1] != null)
            {
                if (gameManager.roomList[myX, myY - 1].CompareTag("Room") || gameManager.roomList[myX, myY - 1].CompareTag("Spawn Room") || gameManager.roomList[myX, myY - 1].CompareTag("Boss Room"))
                {
                    southRoom = gameManager.roomList[myX, myY - 1];
                    if (!southDoorBool && !gameManager.roomList[myX, myY - 1].GetComponent<RoomScript>().northDoorBool)
                    {
                        southRoom.GetComponent<RoomScript>().northRoom = gameObject;
                        southRoom.GetComponent<RoomScript>().neighborRooms.Add(gameObject);
                        neighborRooms.Add(southRoom);
                    }
                    else southRoom.GetComponent<RoomScript>().neighborRooms.Remove(gameObject);
                }    
            }
        }

		if (myX < 9) {
            if (gameManager.roomList[myX + 1, myY] != null)
            {
                if (gameManager.roomList[myX + 1, myY].CompareTag("Room") || gameManager.roomList[myX + 1, myY].CompareTag("Spawn Room") || gameManager.roomList[myX + 1, myY].CompareTag("Boss Room"))
                {
                    eastRoom = gameManager.roomList[myX + 1, myY];
                    if (!eastDoorBool && !gameManager.roomList[myX + 1, myY].GetComponent<RoomScript>().westDoorBool)
                    {
                        eastRoom.GetComponent<RoomScript>().westRoom = gameObject;
                        eastRoom.GetComponent<RoomScript>().neighborRooms.Add(gameObject);
                        neighborRooms.Add(eastRoom);
                    }
                    else eastRoom.GetComponent<RoomScript>().neighborRooms.Remove(gameObject);
                }                 
            }
        }

		if (myX >= 1) {
            if (gameManager.roomList[myX - 1, myY] != null)
            {
                if (gameManager.roomList[myX - 1, myY].CompareTag("Room") || gameManager.roomList[myX - 1, myY].CompareTag("Spawn Room") || gameManager.roomList[myX - 1, myY].CompareTag("Boss Room"))
                {
                    westRoom = gameManager.roomList[myX - 1, myY];
                    if (!westDoorBool && !gameManager.roomList[myX - 1, myY].GetComponent<RoomScript>().eastDoorBool)
                    {
                        westRoom.GetComponent<RoomScript>().eastRoom = gameObject;
                        westRoom.GetComponent<RoomScript>().neighborRooms.Add(gameObject);
                        neighborRooms.Add(westRoom);
                    }
                    else westRoom.GetComponent<RoomScript>().neighborRooms.Remove(gameObject);
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

		neighborRooms.Clear();
    }

    public void SortMembers()
    {
        roomMembers.Sort(delegate (GameObject x, GameObject y)
        {
            if (x.GetComponent<BaseMonster>().getThreat() > y.GetComponent<BaseMonster>().getThreat())
            {
                return -1;
            }

            else if (x.GetComponent<BaseMonster>().getThreat() < y.GetComponent<BaseMonster>().getThreat())
            {
                return 1;
            }

            else
            {
                if (UnityEngine.Random.Range(0, 2) == 0)
                {
                    return -1;
                }
                else
                {
                    return 1;
                }
            }
        });
    }

	public void SortHeroes()
	{
		roomMembers.Sort(delegate (GameObject x, GameObject y)
			{
				if (x.GetComponent<BaseHero>().getThreat() > y.GetComponent<BaseHero>().getThreat())
				{
					return -1;
				}

				else if (x.GetComponent<BaseHero>().getThreat() < y.GetComponent<BaseHero>().getThreat())
				{
					return 1;
				}

				else
				{
					if (UnityEngine.Random.Range(0, 2) == 0)
					{
						return -1;
					}
					else
					{
						return 1;
					}
				}
			});
	}

    //make this more efficent later by only calling it on necesary rooms rather than all
    public void RoomMemeberHandler()
    {
        foreach (GameObject monster in roomMembers)
        {
            if (heroesInRoom.Count != 0)
            {
				monster.GetComponent<BaseMonster> ().setInCombat (true);
            }
            else
            {
				monster.GetComponent<BaseMonster> ().setInCombat (false);
            }
        }
    }

    public void AddGoldToRoom(int coinClicked)
    {
        if (coinClicked == 1)
        {
            if (currentGold == 100)
            {
                gameManager.currentCurrency += currentGold;
                currentGold = 0;
                gameManager.UpdateCurrency();
            }
            else if (currentGold > 100)
            {
                gameManager.currentCurrency += currentGold - 100;
                currentGold = 100;
                gameManager.UpdateCurrency();
            }
            else if (currentGold < 100 && gameManager.currentCurrency >= 100)
            {
                gameManager.currentCurrency -= 100;
                currentGold = 100;
                gameManager.UpdateCurrency();
            }

        }
        else if (coinClicked == 2)
        {
            if (currentGold == 200)
            {
                gameManager.currentCurrency += currentGold - 100;
                currentGold = 100;
                gameManager.UpdateCurrency();
            }
            else if (currentGold > 200)
            {
                gameManager.currentCurrency += currentGold - 200;
                currentGold = 200;
                gameManager.UpdateCurrency();
            }
            else if (currentGold < 200 && gameManager.currentCurrency >= (200 - currentGold))
            {
                gameManager.currentCurrency -= 200 - currentGold;
                currentGold = 200;
                gameManager.UpdateCurrency();
            }
        }
        else if (coinClicked == 3)
        {
            if (currentGold >= 300)
            {
                gameManager.currentCurrency += currentGold - 200;
                currentGold = 200;
                gameManager.UpdateCurrency();
            }
            else if (currentGold < 300 && gameManager.currentCurrency >= (300 - currentGold))
            {
                gameManager.currentCurrency -= 300 - currentGold;
                currentGold = 300;
                gameManager.UpdateCurrency();
            }
        }
        UpdateCoins();
    }

    public void UpdateCoins()
    {
        switch(currentGold)
        {
            case 0:
                coin1.GetComponent<SpriteRenderer>().sprite = emptyCoin;
                coin2.GetComponent<SpriteRenderer>().sprite = emptyCoin;
                coin3.GetComponent<SpriteRenderer>().sprite = emptyCoin;
                break;
            case 100:
                coin1.GetComponent<SpriteRenderer>().sprite = filledCoin;
                coin2.GetComponent<SpriteRenderer>().sprite = emptyCoin;
                coin3.GetComponent<SpriteRenderer>().sprite = emptyCoin;
                break;
            case 200:
                coin1.GetComponent<SpriteRenderer>().sprite = filledCoin;
                coin2.GetComponent<SpriteRenderer>().sprite = filledCoin;
                coin3.GetComponent<SpriteRenderer>().sprite = emptyCoin;
                break;
            case 300:
                coin1.GetComponent<SpriteRenderer>().sprite = filledCoin;
                coin2.GetComponent<SpriteRenderer>().sprite = filledCoin;
                coin3.GetComponent<SpriteRenderer>().sprite = filledCoin;
                break;
            default:
                Debug.Log("You shouldn't be seeing this message");
                break;
        }
    }

    public void OperateDoors(string door)
    {
        if (gameManager.inConstructionMode)
        {
            if (door == "North")
            {
                northDoorBool = !northDoorBool;
                northDoor.GetComponent<SpriteRenderer>().enabled = northDoorBool;
                if (northRoom != null)
                {
                    northRoom.GetComponent<RoomScript>().southDoorBool = northDoorBool;
                    northRoom.GetComponent<RoomScript>().southDoor.GetComponent<SpriteRenderer>().enabled = northDoorBool;
                }

            }
            else if (door == "South")
            {
                southDoorBool = !southDoorBool;
                southDoor.GetComponent<SpriteRenderer>().enabled = southDoorBool;
                if (southRoom != null)
                {
                    southRoom.GetComponent<RoomScript>().northDoorBool = southDoorBool;
                    southRoom.GetComponent<RoomScript>().northDoor.GetComponent<SpriteRenderer>().enabled = southDoorBool;
                }
            }
            else if (door == "East")
            {
                eastDoorBool = !eastDoorBool;
                eastDoor.GetComponent<SpriteRenderer>().enabled = eastDoorBool;
                if (eastDoor != null)
                {
                    eastRoom.GetComponent<RoomScript>().westDoorBool = eastDoorBool;
                    eastRoom.GetComponent<RoomScript>().westDoor.GetComponent<SpriteRenderer>().enabled = eastDoorBool;
                }
            }
            else if (door == "West")
            {
                westDoorBool = !westDoorBool;
                westDoor.GetComponent<SpriteRenderer>().enabled = westDoorBool;
                if (westRoom != null)
                {
                    westRoom.GetComponent<RoomScript>().eastDoorBool = westDoorBool;
                    westRoom.GetComponent<RoomScript>().eastDoor.GetComponent<SpriteRenderer>().enabled = westDoorBool;
                }
            }
        }
    }
}
