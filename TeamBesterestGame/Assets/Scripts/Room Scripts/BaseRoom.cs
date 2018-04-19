using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseRoom : MonoBehaviour {

	//Private variables
	//Damage type here, is it a string???
	float efficieny;
	int size;
	int cost;
	int slots;
	int threat = 0;

	private GameManager gameManager;

	//Values copied directly from old BaseRoom
	public List<BaseRoom> neighborRooms = new List<BaseRoom>(); //public to assign reference in editor

	public List<BaseMonster> roomMembers = new List<BaseMonster>(); //public to be accessed by hero/monster

	public List<BaseHero> heroesInRoom = new List<BaseHero>();

	public int myX; //public to assign reference in editor
	public int myY; //public to assign reference in editor
	private GameObject heldObject;
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

	public int goldCapacity = 300; //public so it can be tested in editor
	public int currentGold; //public so it can be tested in editor
	public GameObject coin1; //public to assign reference in editor
	public GameObject coin2; //public to assign reference in editor
	public GameObject coin3; //public to assign reference in editor
	public Sprite emptyCoin; //public to assign reference in editor
	public Sprite filledCoin; //public to assign reference in editor

	///<summary>
	///Assigns all stats to this room, to be used in place of super.
	///</summary>
	/// <param name="sz">Room Size</param>
	/// <param name="ct">Room Cost</param>
	/// <param name="sl">Treasure Slots</param>
	void AssignStats(int sz, int ct, int sl) {
		this.size = sz;
		this.cost = ct;
		this.slots = sl;
	}

	//Getters
	public int getSize() {
		return this.size;
	}
	public int getCost() {
		return this.cost;
	}
	public int getTreasureSlots() {
		return  this.slots;
	}

	//Group of all functions copied from BaseRoom
	// Use this for initialization
	void Start()
	{
		gameManager = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();
		gameManager.roomList[myX, myY] = gameObject;
		//print(gameObject + " " + myX + ", " + myY);
		//print(gameManager.roomList[myX, myY]);
		UpdateNeighbors();
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
		heldObject = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>().heldObject;
		if (heldObject != null)
		{
			if (heldObject.CompareTag("Monster") && Input.GetMouseButtonDown(1))
			{
				roomMembers.Add(heldObject.GetComponent<BaseMonster>());
				threat += heldObject.GetComponent<MonsterScript> ().threatValue;
				monsterInRoom = true;
				heldObject.GetComponent<MonsterScript>().myRoom = this.gameObject;
				foreach (BaseRoom neighbor in neighborRooms) {
					neighbor.GetComponent<BaseRoom>().SortNeighbors();
				}
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
					if (!northDoorBool && !gameManager.roomList[myX, myY + 1].GetComponent<BaseRoom>().southDoorBool)
					{
						northRoom.GetComponent<BaseRoom>().southRoom = gameObject;
						northRoom.GetComponent<BaseRoom>().neighborRooms.Add(gameObject.GetComponent<BaseRoom>());
						neighborRooms.Add(northRoom.GetComponent<BaseRoom>());
					}
					else northRoom.GetComponent<BaseRoom>().neighborRooms.Remove(gameObject.GetComponent<BaseRoom>());
				}
			}
		}

		if (myY >= 1) {
			if (gameManager.roomList[myX, myY - 1] != null)
			{
				if (gameManager.roomList[myX, myY - 1].CompareTag("Room") || gameManager.roomList[myX, myY - 1].CompareTag("Spawn Room") || gameManager.roomList[myX, myY - 1].CompareTag("Boss Room"))
				{
					southRoom = gameManager.roomList[myX, myY - 1];
					if (!southDoorBool && !gameManager.roomList[myX, myY - 1].GetComponent<BaseRoom>().northDoorBool)
					{
						southRoom.GetComponent<BaseRoom>().northRoom = gameObject;
						southRoom.GetComponent<BaseRoom>().neighborRooms.Add(gameObject.GetComponent<BaseRoom>());
						neighborRooms.Add(southRoom.GetComponent<BaseRoom>());
					}
					else southRoom.GetComponent<BaseRoom>().neighborRooms.Remove(gameObject.GetComponent<BaseRoom>());
				}    
			}
		}

		if (myX < 9) {
			if (gameManager.roomList[myX + 1, myY] != null)
			{
				if (gameManager.roomList[myX + 1, myY].CompareTag("Room") || gameManager.roomList[myX + 1, myY].CompareTag("Spawn Room") || gameManager.roomList[myX + 1, myY].CompareTag("Boss Room"))
				{
					eastRoom = gameManager.roomList[myX + 1, myY];
					if (!eastDoorBool && !gameManager.roomList[myX + 1, myY].GetComponent<BaseRoom>().westDoorBool)
					{
						eastRoom.GetComponent<BaseRoom>().westRoom = gameObject;
						eastRoom.GetComponent<BaseRoom>().neighborRooms.Add(gameObject.GetComponent<BaseRoom>());
						neighborRooms.Add(eastRoom.GetComponent<BaseRoom>());
					}
					else eastRoom.GetComponent<BaseRoom>().neighborRooms.Remove(gameObject.GetComponent<BaseRoom>());
				}                 
			}
		}

		if (myX >= 1) {
			if (gameManager.roomList[myX - 1, myY] != null)
			{
				if (gameManager.roomList[myX - 1, myY].CompareTag("Room") || gameManager.roomList[myX - 1, myY].CompareTag("Spawn Room") || gameManager.roomList[myX - 1, myY].CompareTag("Boss Room"))
				{
					westRoom = gameManager.roomList[myX - 1, myY];
					if (!westDoorBool && !gameManager.roomList[myX - 1, myY].GetComponent<BaseRoom>().eastDoorBool)
					{
						westRoom.GetComponent<BaseRoom>().eastRoom = gameObject;
						westRoom.GetComponent<BaseRoom>().neighborRooms.Add(gameObject.GetComponent<BaseRoom>());
						neighborRooms.Add(westRoom.GetComponent<BaseRoom>());
					}
					else westRoom.GetComponent<BaseRoom>().neighborRooms.Remove(gameObject.GetComponent<BaseRoom>());
				}
			}
		}

		SortNeighbors();
	}

	public void SortNeighbors() {
		if (neighborRooms.Count >= 2) {
			neighborRooms.Sort (delegate(BaseRoom x, BaseRoom y) {
				if (x.threat > y.threat) {
					return -1;
				}
				else if (x.threat < y.threat) {
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
			northRoom.GetComponent<BaseRoom>().southRoom = null;
		}

		if (southRoom != null)
		{
			southRoom.GetComponent<BaseRoom>().northRoom = null;
		}

		if (eastRoom != null)
		{
			eastRoom.GetComponent<BaseRoom>().westRoom = null;
		}

		if (westRoom != null)
		{
			westRoom.GetComponent<BaseRoom>().eastRoom = null;
		}

		northRoom = null;
		southRoom = null;
		eastRoom = null;
		westRoom = null;

		neighborRooms.Clear();
	}

	public void SortMembers()
	{
		roomMembers.Sort(delegate (BaseMonster x, BaseMonster y)
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
		roomMembers.Sort(delegate (BaseMonster x, BaseMonster y)
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
		foreach (BaseMonster monster in roomMembers)
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
					northRoom.GetComponent<BaseRoom>().southDoorBool = northDoorBool;
					northRoom.GetComponent<BaseRoom>().southDoor.GetComponent<SpriteRenderer>().enabled = northDoorBool;
				}

			}
			else if (door == "South")
			{
				southDoorBool = !southDoorBool;
				southDoor.GetComponent<SpriteRenderer>().enabled = southDoorBool;
				if (southRoom != null)
				{
					southRoom.GetComponent<BaseRoom>().northDoorBool = southDoorBool;
					southRoom.GetComponent<BaseRoom>().northDoor.GetComponent<SpriteRenderer>().enabled = southDoorBool;
				}
			}
			else if (door == "East")
			{
				eastDoorBool = !eastDoorBool;
				eastDoor.GetComponent<SpriteRenderer>().enabled = eastDoorBool;
				if (eastDoor != null)
				{
					eastRoom.GetComponent<BaseRoom>().westDoorBool = eastDoorBool;
					eastRoom.GetComponent<BaseRoom>().westDoor.GetComponent<SpriteRenderer>().enabled = eastDoorBool;
				}
			}
			else if (door == "West")
			{
				westDoorBool = !westDoorBool;
				westDoor.GetComponent<SpriteRenderer>().enabled = westDoorBool;
				if (westRoom != null)
				{
					westRoom.GetComponent<BaseRoom>().eastDoorBool = westDoorBool;
					westRoom.GetComponent<BaseRoom>().eastDoor.GetComponent<SpriteRenderer>().enabled = westDoorBool;
				}
			}
		}
	}
}
