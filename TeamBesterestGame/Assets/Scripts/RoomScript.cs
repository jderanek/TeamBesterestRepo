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

    public bool northDoor; //public to be accessed by door script, may replace with trigger events later
    public bool southDoor; //public to assign reference in editor
    public bool eastDoor; //public to assign reference in editor
    public bool westDoor; //public to assign reference in editor

    public GameObject northButton; //public to assign reference in editor
    public GameObject southButton; //public to assign reference in editor
    public GameObject eastButton; //public to assign reference in editor
    public GameObject westButton; //public to assign reference in editor

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
        UpdateNeighbors();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

	public void Initialize() {
		
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
                northRoom = gameManager.roomList[myX, myY + 1];
                if (northDoor && gameManager.roomList[myX, myY + 1].GetComponent<RoomScript>().southDoor)
                {
                    northRoom.GetComponent<RoomScript>().southRoom = gameObject;
                    northRoom.GetComponent<RoomScript>().neighborRooms.Add(gameObject);
                    neighborRooms.Add(northRoom);
                }
                else northRoom.GetComponent<RoomScript>().neighborRooms.Remove(gameObject);
            }
        }

		if (myY >= 1) {
            if (gameManager.roomList[myX, myY - 1] != null)
            {
                southRoom = gameManager.roomList[myX, myY - 1];
                if (southDoor && gameManager.roomList[myX, myY - 1].GetComponent<RoomScript>().northDoor)
                {
                    southRoom.GetComponent<RoomScript>().northRoom = gameObject;                    
                    southRoom.GetComponent<RoomScript>().neighborRooms.Add(gameObject);
                    neighborRooms.Add(southRoom);
                }
                else southRoom.GetComponent<RoomScript>().neighborRooms.Remove(gameObject);
            }
        }

		if (myX <= 9) {
            if (gameManager.roomList[myX + 1, myY] != null)
            {
                eastRoom = gameManager.roomList[myX + 1, myY];
                if (eastDoor && gameManager.roomList[myX + 1, myY].GetComponent<RoomScript>().westDoor)
                {                    
                    eastRoom.GetComponent<RoomScript>().westRoom = gameObject;
                    eastRoom.GetComponent<RoomScript>().neighborRooms.Add(gameObject);
                    neighborRooms.Add(eastRoom);
                }
                else eastRoom.GetComponent<RoomScript>().neighborRooms.Remove(gameObject);
            }
        }

		if (myX >= 1) {
            if (gameManager.roomList[myX - 1, myY] != null)
            {
                westRoom = gameManager.roomList[myX - 1, myY];
                if (westDoor && gameManager.roomList[myX - 1, myY].GetComponent<RoomScript>().eastDoor)
                {
                    westRoom.GetComponent<RoomScript>().eastRoom = gameObject;
                    westRoom.GetComponent<RoomScript>().neighborRooms.Add(gameObject);
                    neighborRooms.Add(westRoom);
                }
                else westRoom.GetComponent<RoomScript>().neighborRooms.Remove(gameObject);
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
            if (x.GetComponent<MonsterScript>().threatValue > y.GetComponent<MonsterScript>().threatValue)
            {
                return -1;
            }

            else if (x.GetComponent<MonsterScript>().threatValue < y.GetComponent<MonsterScript>().threatValue)
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
				if (x.GetComponent<HeroScript>().threatValue > y.GetComponent<HeroScript>().threatValue)
				{
					return -1;
				}

				else if (x.GetComponent<HeroScript>().threatValue < y.GetComponent<HeroScript>().threatValue)
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
                monster.GetComponent<MonsterScript>().inCombat = true;
            }
            else
            {
                monster.GetComponent<MonsterScript>().inCombat = false;
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
}
