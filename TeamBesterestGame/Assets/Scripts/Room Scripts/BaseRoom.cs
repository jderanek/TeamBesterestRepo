using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseRoom : MonoBehaviour {

	//Private variables
	//Damage type here, is it a string???
	float efficieny;
	int size;
	int cost;

	///<summary>
	///Assigns all stats to this room, to be used in place of super.
	///</summary>
	/// <param name="sz">Room Size</param>
	/// <param name="ct">Room Cost</param>
	void AssignStats(int sz, int ct) {
		this.size = sz;
		this.cost = ct;
	}

	//Getters
	public int getSize() {
		return this.size;
	}
	public int getCost() {
		return this.cost;
	}
}
