using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarriorHero : BaseHero {

	bool boosted = false;

	//Assigns all initial stats
	void Start () {
		//this.AssignStats (20, 2, 10, 2, 200, 6);
		this.AssignStats("Warrior");
	}

	public override void TakeDamage(int dmg, BaseMonster attacker = null) {
		base.TakeDamage (dmg);
		if (this.getHealth () <= this.getMaxHealth () / 2 && !boosted) {
			this.Heal (1);
			this.setArmor (this.getArmor () + 1);
			this.boosted = true;
		}
	}
}