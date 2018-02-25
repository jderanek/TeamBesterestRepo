using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Base class to store heroes max and current health, damage and money drop
public abstract class BaseHero {

	//Private variables for hero stats
	private int maxHealth;
	private int curHealth;
	private int damage;
	private int value;
	private int armor;
	private int capacity;
	private int holding = 0;
	private int threat;

	//Function to assign values to script from child class
	private void AssignStats(int hp, int dmg, int val, int arm, int cap, int thr) {
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
	}
	public void Heal(int heal) {
		this.curHealth = Mathf.Clamp (this.curHealth + heal, 0, this.maxHealth);
	}

	//Abstract attack function to allow for different attack types by class
	public abstract void Attack(RoomScript room);
}
