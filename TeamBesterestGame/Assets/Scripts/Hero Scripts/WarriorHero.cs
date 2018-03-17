using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarriorHero : BaseHero {

	//Assigns all initial stats
	void Start () {
		this.AssignStats (20, 2, 10, 2, 200, 6);
	}
}