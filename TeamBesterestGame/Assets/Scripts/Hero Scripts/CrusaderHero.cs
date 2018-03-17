using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrusaderHero : BaseHero {

	//Assigns all initial stats
	void Start () {
		this.AssignStats (30, 3, 20, 3, 200, 6);
	}
}
