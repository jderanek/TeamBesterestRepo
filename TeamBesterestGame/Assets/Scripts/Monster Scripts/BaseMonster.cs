using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseMonster : MonoBehaviour {

	//Private variables for this monsters stats
	int curHealth;
	int maxHealth;
	int curDamage;
	int damage;
	TraitBase personality;
	int salary;
	float stress;
	float morale;
	float stressGain;
	float vacationStressLoss;
	int infamyGain;
	int threat;
	int armor;
	bool inCombat;
	bool hasFought;

	///<summary>
	///Assigns all stats to this monster, to be used in place of super.
	/// Defaults stress and morale, as well as gain, loss and infamy
	///</summary>
	/// <param name="hp">Total Health</param>
	/// <param name="dam">Base Damage</param>
	/// <param name="trait">Personality Trait</param>
	/// <param name="sal">Base Salary</param>
	/// <param name="thr">Threat</param>
	/// <param name="arm">Base Armor</param>
	public void AssignStats(int hp, int dam, TraitBase trait, int sal, int thr, int arm) {
		this.maxHealth = hp;
		this.curHealth = maxHealth;
		this.damage = dam;
		this.curDamage = this.damage;
		this.personality = trait;
		this.salary = sal;
		this.threat = thr;
		this.stress = 0f;
		this.morale = .5f;
		this.stressGain = .02f;
		this.vacationStressLoss = .15f;
		this.infamyGain = 1;
		this.armor = arm;
	}

	//Getters for most stats
	public int getCurHealth() {
		return this.curHealth;
	}
	public int getMaxHealth() {
		return this.maxHealth;
	}
	public int getCurDamage() {
		return this.curDamage;
	}
	public int getBaseDamage() {
		return this.damage;
	}
	public TraitBase getTrait() {
		return this.personality;
	}
	public int getSalary() {
		return this.salary;
	}
	public int getThreat() {
		return this.threat;
	}
	public float getMorale() {
		return this.morale;
	}
	public float getStress() {
		return this.stress;
	}
}
