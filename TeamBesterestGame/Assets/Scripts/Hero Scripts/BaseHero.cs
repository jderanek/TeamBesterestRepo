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
	private RoomScript curRoom;

	//Function to assign values to script from child class
	public void AssignStats(int hp, int dmg, int val, int arm, int cap, int thr) {
		this.maxHealth = hp;
		this.curHealth = hp;
		this.damage = dmg;
		this.value = val;
		this.armor = arm;
		this.capacity = cap;
		this.threat = thr;
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

	//Setter function to gain money
	public void grab(int mon) {
		this.holding = Mathf.Clamp (this.holding + mon, 0, this.capacity);
	}

	//Setter functions for damage and value
	public void setDamage(int dmg) {
		this.damage = dmg;
	}
	public void setValue(int val) {
		this.value = val;
	}

	//Damage and Heal functions to restore or reduce hero health
	public void Damage(int dmg) {
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
	}

	//Function to kill this hero
	public void Death() {
		curRoom.heroesInRoom.Remove(this.gameObject);
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
		Destroy(this.gameObject);
	}

	//Abstract attack function to allow for different attack types by class
	public abstract void Attack(RoomScript room);
}
