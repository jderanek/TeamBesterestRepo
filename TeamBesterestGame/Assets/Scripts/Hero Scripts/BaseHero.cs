using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//Base class to store heroes max and current health, damage and money drop
public abstract class BaseHero : MonoBehaviour {

	//Private variables for hero stats
	private int maxHealth;
	public int curHealth;
	public int damage;
	private int value;
	private int armor;
	public int capacity;
	public int holding = 0;
	private int threat;
	private string type;
	private BaseRoom curRoom;
	BaseParty currentParty;
	GameManager gameManager;
    UIManager uiManager;
    Text damageText;

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
    
	public void ManualAssignStats(int hp, int dmg, int arm, int val, int cap, int thr) {
		gameManager = GameObject.FindGameObjectWithTag ("GameController").GetComponent<GameManager> ();
        uiManager = gameManager.GetComponent<UIManager>();
		curRoom = gameManager.spawnRoom.GetComponent<BaseRoom> ();

		this.maxHealth = hp;
        this.maxHealth *= gameManager.healthWeight;
        this.curHealth = this.maxHealth;

		this.damage = dmg;
        this.damage *= gameManager.attackWeight;

		this.armor = arm;
        this.armor *= gameManager.defenseWeight;

        this.value = val;
        this.capacity = cap;
		this.threat = thr;

        damageText = this.gameObject.GetComponentInChildren<Text>();
        damageText.text = this.curHealth + "hp";
    }
    

	///<summary>
	///Assigns all stats to this hero by getting information from the spreadsheet
	///</summary>
	/// <param name="nm">type</param>
	public void AssignStats(string nm) {
		gameManager = GameObject.FindGameObjectWithTag ("GameController").GetComponent<GameManager> ();
		curRoom = gameManager.spawnRoom.GetComponent<BaseRoom> ();
		this.type = nm;
		this.maxHealth = int.Parse(gameManager.heroStats.data [type] ["Health"]);
        this.maxHealth *= gameManager.healthWeight;
        this.curHealth = this.maxHealth;

		this.damage = int.Parse(gameManager.heroStats.data [type] ["Base Attack"]);
        this.damage *= gameManager.attackWeight;

		this.armor = int.Parse(gameManager.heroStats.data [type] ["Defense"]);
        this.armor *= gameManager.defenseWeight;

		this.threat = int.Parse(gameManager.heroStats.data [type] ["Threat"]);
		this.value = int.Parse(gameManager.heroStats.data [type] ["Gold Drop Value"]);
		this.capacity = int.Parse(gameManager.heroStats.data [type] ["Carry Capacity"]) * 100;

        damageText = this.gameObject.GetComponentInChildren<Text>();
        damageText.text = this.curHealth + "hp";
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
	public BaseRoom getRoom() {
		return this.curRoom;
	}
	public bool isInCombat() {
		return inCombat;
	}
	public void setParty(BaseParty party) {
		this.currentParty = party;
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
        this.damageText.text = this.curHealth + "hp";
		if (this.curHealth <= 0)
			this.Death ();
	}
	public void Heal(int heal) {
		this.curHealth = Mathf.Clamp (this.curHealth + heal, 0, this.maxHealth);
	}

	//Function to move this hero from one room to another
	public void MoveTo(BaseRoom room) {
		if (curRoom != null) {
			curRoom.heroesInRoom.Remove (this.gameObject);
		
			if (curRoom.heroesInRoom.Count == 0) {
				curRoom.heroInRoom = false;
			}
		}

		this.curRoom = room;
		curRoom.heroesInRoom.Add (this.gameObject);
		curRoom.heroInRoom = true;
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
				gameManager.CurrencyChanged(this.value);
			}
		}
		currentParty.partyMembers.Remove (this);
		Destroy(this.gameObject);
	}

	//Removes this hero without giving gold, meant for heroes who left dungeon
	public void Remove() {
		curRoom.heroesInRoom.Remove(gameObject);
		if (curRoom.heroesInRoom.Count == 0)
		{
			curRoom.heroInRoom = false;
		}

		Destroy(this.gameObject);
	}

	//Default attack function that hits the monster in the room with the highest threat value
	//Some classes override this function for different attack methods
	public virtual void Attack() {
		BaseMonster monScript;
		BaseMonster highThreat = null;
		int threat = -1;
		foreach (GameObject mon in curRoom.roomMembers) {
			if (mon == null)
				continue;
			
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

		if (!curRoom.monsterInRoom && curRoom.gameObject.CompareTag("Boss Room") && gameManager.currentCurrency > 0)
		{
			holding += 100;
			gameManager.currentCurrency -= 100;
			uiManager.UpdateCurrency();
			/*if (holding == capacity)
			{
				Destroy(gameObject);
			}*/
		}

		else if (!curRoom.monsterInRoom && curRoom.currentGold > 0) //If there isn't a monster in the room with the hero and there is gold to be looted
		{
			holding += 100;
			curRoom.currentGold -= 100;
			curRoom.UpdateCoins();
			/*if (holding == capacity)
			{
				Destroy(gameObject);
			}*/
		}
		//Should no longer be needed
		/*
		else if (!curRoom.monsterInRoom && curRoom.neighborRooms.Count != 0) //If there isn't a monster in the room with the hero and if the room has neighbor rooms
		{
			curRoom = curRoom.neighborRooms[0].GetComponent<BaseRoom>();

			curRoom.heroesInRoom.Remove(this.gameObject);

			if (curRoom.heroesInRoom.Count == 0)
			{
				curRoom.heroInRoom = false;
			}

			curRoom = curRoom.gameObject.GetComponent<BaseRoom>();
			curRoom.heroInRoom = true;
			curRoom.heroesInRoom.Add(this.gameObject);
			//curRoom.SortHeroes(); //somethings wrong here

			transform.position = curRoom.gameObject.transform.position;
		}*/
	}

	void OnMouseOver()
	{
		if (Input.GetMouseButtonDown(0) && !inCombat)
        {
			GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>().SelectObject(this.gameObject);
		}
	}
}
