using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Super class for all traits, contains base modifiers for morale and stress
//As well as functions to apply trait effects to the monster
public abstract class TraitBase {

	//Name variable to get from child classes
	private string name;

	public void setName(string newName) {
		this.name = newName;
	}
	public string getName() {
		return this.name;
	}

	//Abstract function to apply base effects
	public abstract void ApplyBase(MonsterScript monster);

	//Abstract function to apply effects during end of work 'day'
	public abstract void ApplyDayEffects(MonsterScript monster);

	//Abstract function to apply effects during end of work 'week'
	public abstract void ApplyWeekEffects(MonsterScript monster);
}
