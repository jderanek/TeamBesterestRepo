using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseRoom : MonoBehaviour {

	//Private variables
	//Damage type here, is it a string???
	float efficieny;
	int size;
	int cost;
	int slots;

	///<summary>
	///Assigns all stats to this room, to be used in place of super.
	///</summary>
	/// <param name="sz">Room Size</param>
	/// <param name="ct">Room Cost</param>
	/// <param name="sl">Treasure Slots</param>
	void AssignStats(int sz, int ct, int sl) {
		this.size = sz;
		this.cost = ct;
		this.slots = sl;
	}

	//Getters
	public int getSize() {
		return this.size;
	}
	public int getCost() {
		return this.cost;
	}
	public int getTreasureSlots() {
		return  this.slots;
	}
}
