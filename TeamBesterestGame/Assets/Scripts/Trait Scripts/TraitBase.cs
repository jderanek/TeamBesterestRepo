using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Super class for all traits, contains base modifiers for morale and stress
//As well as functions to apply trait effects to the monster
public abstract class TraitBase {

	//Base modifers to the monster
	private int moraleMod;
	private int stressMod;

	//Basic getters and setters for morale and stress modifiers
	public int getMorale() {
		return this.moraleMod;
	}
	public int getStress() {
		return this.stressMod;
	}
	public void setMorale(int m) {
		this.moraleMod = m;
	}
	public void setStess(int s) {
		this.stressMod = s;
	}

	//Base function to apply morale and stress modifiers
	public void ApplyBase(MonsterScript monster) {
		monster.morale += moraleMod;
		monster.stress += stressMod;
	}

	//Abstract function to apply effects during end of work 'day'
	public abstract void ApplyDayEffects(MonsterScript monster);

	//Abstract function to apply effects during end of work 'week'
	public abstract void ApplyWeekEffects(MonsterScript monster);
}
