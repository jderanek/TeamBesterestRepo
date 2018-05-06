using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MageHero : BaseHero {

	//Assigns all initial stats
	void Start () {
		//this.AssignStats (12, 6, 15, 0, 200, 4);
		this.AssignStats("Wizard");
	}

	//Calls Base attack function 3 times to allow Mage to attack three enemies per room
	public override void Attack() {
		base.Attack ();
		base.Attack ();
		base.Attack ();
	}
}
