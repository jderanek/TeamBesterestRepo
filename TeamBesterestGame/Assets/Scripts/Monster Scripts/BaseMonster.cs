using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class BaseMonster : MonoBehaviour {

	//Private variables for this monsters stats
	string name;
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
	bool inCombat;
	bool hasFought;
	GameObject curRoom;
	Text damageText;

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
		this.name = nm;
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
	public int getArmor() {
		return this.armor;
	}
	public float getMorale() {
		return this.morale;
	}
	public float getStress() {
		return this.stress;
	}
	public float getStressGain() {
		return this.stressGain;
	}
	public float getStressLoss() {
		return this.vacationStressLoss;
	}
	public int getInfamyGain() {
		return this.infamyGain;
	}
	public bool isInCombat() {
		return this.inCombat;
	}
	public bool getHasFought() {
		return this.hasFought;
	}
	public string getName() {
		return this.name;
	}
	public int getWorkEthic() {
		return this.workEthic;
	}
	public int getSize() {
		return this.size;
	}

	//Function to make monster lose health
	public void Damage(int dam) {
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
			highThreat.Damage(this.getCurDamage());
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
