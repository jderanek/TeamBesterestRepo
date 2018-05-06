using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RogueHero : BaseHero {

	//Assigns all initial stats
	void Start () {
		//this.AssignStats (15, 4, 15, 1, 300, 2);
		this.AssignStats("Rogue");
	}
}