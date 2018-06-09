using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Base class to store heroes max and current health, damage and money drop
public abstract class BaseHero : MonoBehaviour {

	//Private variables for hero stats
	private int maxHealth;
	private int curHealth;
	private int damage;
	private int value;
	private int armor;
	private int capacity;
	private int holding = 0;
	private int threat;
	private string type;
	private RoomScript curRoom;
	BaseParty currentParty;
	GameManager gameManager;

	//Variables not set by assignment
	private bool inCombat;

	///<summary>
	///Assigns all stats to this hero, to be used in place of super.
	///</summary>
	/// <param name="hp">Total Health</param>
	/// <param name="dmg">Base Damage</param>
	/// <param name="val">Money earned on death</param>
	/// <param name="arm">Base Armor</param>
	/// <param name="cap"Max holding money>
	/// <param name="thr">Threat</param>
	public void AssignStats(int hp, int dmg, int val, int arm, int cap, int thr) {
		gameManager = GameObject.FindGameObjectWithTag ("GameController").GetComponent<GameManager> ();
		curRoom = gameManager.spawnRoom.GetComponent<RoomScript> ();
		this.maxHealth = hp;
		this.curHealth = hp;
		this.damage = dmg;
		this.value = val;
		this.armor = arm;
		this.capacity = cap;
		this.threat = thr;
	}

	///<summary>
	///Assigns all stats to this hero by getting information from the spreadsheet
	///</summary>
	/// <param name="nm">type</param>
	public void AssignStats(string nm) {
		gameManager = GameObject.FindGameObjectWithTag ("GameController").GetComponent<GameManager> ();
		curRoom = gameManager.spawnRoom.GetComponent<RoomScript> ();
		this.type = nm;
		this.maxHealth = int.Parse(gameManager.heroStats.data [type] ["Health"]);
		this.curHealth = int.Parse(gameManager.heroStats.data [type] ["Health"]);
		this.damage = int.Parse(gameManager.heroStats.data [type] ["Base Attack"]);
		this.armor = int.Parse(gameManager.heroStats.data [type] ["Defense"]);
		this.threat = int.Parse(gameManager.heroStats.data [type] ["Threat Level"]);
		this.value = int.Parse(gameManager.heroStats.data [type] ["Kill Value"]);
		this.capacity = int.Parse(gameManager.heroStats.data [type] ["Carry Capacity"]) * 100;
	}

	//Getter functions for damage, current health and currency value
	public int getHealth() {
		return this.curHealth;
	}
	public int getMaxHealth() {
		return this.maxHealth;
	}
	public int getDamage() {
		return this.damage;
	}
	public int getValue() {
		return this.value;
	}
	public int getThreat() {
		return this.threat;
	}
	public int getArmor() {
		return this.armor;
	}
	public int getCapacity() {
		return this.capacity;
	}
	public int getHolding() {
		return this.holding;
	}
	public RoomScript getRoom() {
		return this.curRoom;
	}
	public bool isInCombat() {
		return inCombat;
	}

	//Setter functions for damage and value
	public void setDamage(int dmg) {
		this.damage = dmg;
	}
	public void setValue(int val) {
		this.value = val;
	}

	//Setter function to gain money
	public void Grab(int mon) {
		this.holding = Mathf.Clamp (this.holding + mon, 0, this.capacity);
	}

	//Damage and Heal functions to restore or reduce hero health
	public void TakeDamage(int dmg) {
		this.curHealth -= Mathf.Clamp (dmg - armor, 0, this.maxHealth);
		if (this.curHealth <= 0)
			this.Death ();
	}
	public void Heal(int heal) {
		this.curHealth = Mathf.Clamp (this.curHealth + heal, 0, this.maxHealth);
	}

	//Function to move this hero from one room to another
	public void MoveTo(RoomScript room) {
		curRoom.heroesInRoom.Remove(this.gameObject);
		if (curRoom.heroesInRoom.Count == 0)
		{
			curRoom.heroInRoom = false;
		}
		this.curRoom = room;
		this.gameObject.transform.position = room.gameObject.transform.position;
	}

	//Function to kill this hero
	public void Death() {
		curRoom.heroesInRoom.Remove(gameObject);
		if (curRoom.heroesInRoom.Count == 0)
		{
			curRoom.heroInRoom = false;
		}

		GameManager gameManager;
		GameObject gameMangerObject = GameObject.FindWithTag ("GameController");
		if (gameMangerObject != null) {
			gameManager = gameMangerObject.GetComponent <GameManager> ();

			gameManager.GetComponent<GameManager> ().IncreaseInfamyXP (this.threat);
			if (gameManager.currentCurrency < gameManager.maximumCurrency) {
				gameManager.GoldGainedOnDeath (this.value);
			}
		}
		//Destroy(this.gameObject);
	}

	//Default attack function that hits the monster in the room with the highest threat value
	//Some classes override this function for different attack methods
	public virtual void Attack() {
		BaseMonster monScript;
		BaseMonster highThreat = null;
		int threat = -1;
		foreach (GameObject mon in curRoom.roomMembers) {
            monScript = mon.GetComponent<BaseMonster>();
           
			if (monScript != null) {
				if (monScript.getThreat() > threat) {
					highThreat = monScript;
					threat = monScript.getThreat();
				}
			}
            
		}

		//Makes mosnter take damage
		if (highThreat != null)
			highThreat.TakeDamage(this.getDamage());
	}

	//Copied from HeroScript
	public void CheckCurrentRoom() {
		curRoom.SortNeighbors();

		if (!curRoom.monsterInRoom && curRoom.gameObject.CompareTag("Boss Room") && gameManager.currentCurrency > 0)
		{
			holding += 100;
			gameManager.currentCurrency -= 100;
			gameManager.UpdateCurrency();
			if (holding == capacity)
			{
				Destroy(gameObject);
			}
		}

		else if (!curRoom.monsterInRoom && curRoom.currentGold > 0) //If there isn't a monster in the room with the hero and there is gold to be looted
		{
			holding += 100;
			curRoom.currentGold -= 100;
			curRoom.UpdateCoins();
			if (holding == capacity)
			{
				Destroy(gameObject);
			}
		}
		else if (!curRoom.monsterInRoom && curRoom.neighborRooms.Count != 0) //If there isn't a monster in the room with the hero and if the room has neighbor rooms
		{
			curRoom = curRoom.neighborRooms[0].GetComponent<RoomScript>();

			curRoom.heroesInRoom.Remove(this.gameObject);

			if (curRoom.heroesInRoom.Count == 0)
			{
				curRoom.heroInRoom = false;
			}

			curRoom = curRoom.gameObject.GetComponent<RoomScript>();
			curRoom.heroInRoom = true;
			curRoom.heroesInRoom.Add(this.gameObject);
			//curRoom.SortHeroes(); //somethings wrong here

			transform.position = curRoom.gameObject.transform.position;
		}
	}

	void OnMouseOver()
	{
		if (Input.GetMouseButtonDown(0) && !inCombat)
        {
			GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>().SelectObject(this.gameObject);
		}
	}
}
