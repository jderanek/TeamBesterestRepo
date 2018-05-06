using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SniperHero : BaseHero {

	//Assigns all initial stats
	void Start () {
		//this.AssignStats (18, 8, 25, 0, 300, 4);
		this.AssignStats("Arbalest");
	}
}