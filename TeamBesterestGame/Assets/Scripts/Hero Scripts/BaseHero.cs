using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Base class to store heroes max and current health, damage and money drop
public abstract class BaseHero : MonoBehaviour {

	//Private variables for health, damage and value
	private int maxHealth;
	private int curHealth;
	private int damage;
	private int value;

	//Function to assign values to script from child class
	private void AssignStats(int hp, int dmg, int val) {
		this.maxHealth = hp;
		this.curHealth = hp;
		this.damage = dmg;
		this.value = val;
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

	//Setter functions for damage and value
	public void setDamage(int dmg) {
		this.damage = dmg;
	}
	public void setValue(int val) {
		this.value = val;
	}

	//Damage and Heal functions to restore or reduce hero health
	public void Damage(int dmg) {
		this.curHealth -= dmg;
		if (this.curHealth <= 0)
			Death ();
	}
	public void Heal(int heal) {
		this.curHealth += heal;
	}

	//Abstract death function to be called on health becoming 0
	public abstract void Death();
}
