﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class BaseMonster : MonoBehaviour {

	//Private variables for this monsters stats
	string type;
	string monName;
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
	int workEthic;
	int size;
	int tier;
	bool inCombat;
	bool hasFought;
	bool monsterGrabbed;
	bool heroInRoom;
	GameObject curRoom;
	Text damageText;
	GameManager gameManager;

	//Temp variables from transfer
	private TraitBase[] allTraits = new TraitBase[] {new CowardlyTrait(), new FancyTrait(), new FlirtyTrait(), new GrossTrait(), new GuardianTrait(), new PridefulTrait(), new RecklessTrait(), new SlackerTrait(), new TyrantTrait(), new WaryTrait(), new WorkaholicTrait()};
	string traitName;
	public float baseNerve = .5f; //public to be edited in editor
	public float curNerve = .5f; //public to be edited in editor

	void Awake() {
		monsterGrabbed = true;
		heroInRoom = false;
		this.personality = allTraits [Random.Range (0, allTraits.Length)];
		this.personality.ApplyBase (this);
		this.traitName = this.personality.getName ();
		this.workEthic = Random.Range (-1, 1);
	}

	///<summary>
	///Assigns all stats to this monster, to be used in place of super.
	/// Defaults stress and morale, as well as gain, loss and infamy
	///</summary>
	/// <param name="nm">Name of Monster</param>
	/// <param name="hp">Total Health</param>
	/// <param name="dam">Base Damage</param>
	/// <param name="trait">Personality Trait</param>
	/// <param name="sal">Base Salary</param>
	/// <param name="thr">Threat</param>
	/// <param name="arm">Base Armor</param>
	/// <param name="ethic">Work Ethic</param>
	/// <param name="sz">Monster Size</param>
	public void AssignStats(string nm, int hp, int dam, TraitBase trait, int sal, int thr, int arm, int ethic, int sz) {
		this.monName = nm;
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
		damageText = this.gameObject.GetComponentInChildren<Text>();
		this.workEthic = ethic;
		this.size = sz;
	}

	///<summary>
	///Assigns all stats to this monster from monster stats sheet
	///</summary>
	/// <param name="type">Name of Monster Type</param>
	public void AssignStats(string type) {
		gameManager = GameObject.FindGameObjectWithTag ("GameController").GetComponent<GameManager> ();
		this.type = type;
		curRoom = gameManager.spawnRoom;
		//Temp name choosing
		this.monName = gameManager.monNames.data [(Random.Range (1, 5)).ToString()] [(Random.Range (1, 6)).ToString()];
		this.maxHealth = int.Parse(gameManager.monsters.data [type] ["Health"]);
		this.curHealth = int.Parse(gameManager.monsters.data [type] ["Health"]);
		this.damage = int.Parse(gameManager.monsters.data [type] ["Attack"]);
		this.armor = int.Parse(gameManager.monsters.data [type] ["Defense"]);
		//this.threat = int.Parse(gameManager.monsters.data [type] ["Threat Level"]);
		this.size = int.Parse (gameManager.monsters.data [type] ["Size"]);
		this.tier = int.Parse (gameManager.monsters.data [type] ["Tier"]);
		this.salary = int.Parse (gameManager.monsters.data [type] ["Cost"]);

		this.stress = 0f;
		this.morale = .5f;
		this.stressGain = .02f;
		this.vacationStressLoss = .15f;
		this.infamyGain = 1;
		damageText = this.gameObject.GetComponentInChildren<Text>();

		//Placeholder
		this.threat = 3;
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
	public void addDamage(int d) {
		this.curDamage += d;
	}
	public void setDamage(int d) {
		this.damage = d;
	}
	public void setCurDamage(int d) {
		this.curDamage = d;
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
	public void setThreat(int t) {
		this.threat = t;
	}
	public void addThreat(int t) {
		this.threat += t;
	}
	public int getArmor() {
		return this.armor;
	}
	public float getMorale() {
		return this.morale;
	}
	public void setMorale(float newM) {
		this.morale = newM;
	}
	public float getStress() {
		return this.stress;
	}
	public void setStress(float newS) {
		this.stress = newS;
	}
	public float getStressGain() {
		return this.stressGain;
	}
	public void setStressGain(float newGain) {
		this.stressGain = newGain;
	}
	public float getStressLoss() {
		return this.vacationStressLoss;
	}
	public void setStressLoss(float loss) {
		this.vacationStressLoss = loss;
	}
	public int getInfamyGain() {
		return this.infamyGain;
	}
	public void setInfamyGain(int gain) {
		this.infamyGain = gain;
	}
	public bool isInCombat() {
		return this.inCombat;
	}
	public bool getHasFought() {
		return this.hasFought;
	}
	public string getName() {
		return this.monName;
	}
	public int getWorkEthic() {
		return this.workEthic;
	}
	public int getSize() {
		return this.size;
	}
	public string getTraitName() {
		return this.traitName;
	}
	public RoomScript getCurRoom() {
		return this.curRoom.GetComponent<RoomScript> ();
	}

	//Function to make monster lose health
	public void TakeDamage(int dam) {
		this.curHealth = Mathf.Clamp (this.curHealth - dam, 0, this.maxHealth);
		damageText.text = this.curHealth.ToString ();
		if (curHealth <= 0) {
			this.Death ();
		}
	}

	//Function to make monster gain health
	public void Heal(int heal) {
		this.curHealth = Mathf.Clamp (this.curHealth + heal, 0, this.maxHealth);
	}

	//Default attack function that hits the hero with the highest threat
	public virtual void Attack(RoomScript room) {
		BaseHero heroScript;
		BaseHero highThreat = null;
		int threat = -1;
		foreach (GameObject hero in room.roomMembers) {
			heroScript = hero.GetComponent<BaseHero> ();

			if (heroScript != null) {
				if (heroScript.getThreat() > threat) {
					highThreat = heroScript;
					threat = heroScript.getThreat();
				}
			}
		}

		//Makes mosnter take damage
		if (highThreat != null)
			highThreat.TakeDamage(this.getCurDamage());
	}

	private void Death()
	{
		curRoom.GetComponent<RoomScript>().roomMembers.Remove(this.gameObject);
		if (curRoom.GetComponent<RoomScript>().roomMembers.Count == 0)
		{
			curRoom.GetComponent<RoomScript>().monsterInRoom = false;
		}
		Destroy(this.gameObject);
	}

	//Applies personality effects to the monster, as well as other stat modifiers
	//Called at end of each work day
	public void DayHandler() {
	}
}
