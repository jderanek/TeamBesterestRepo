using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Super class for all traits, contains base modifiers for morale and stress
//As well as functions to apply trait effects to the monster
public abstract class TraitBase {
	//Base function to apply morale and stress modifiers
	public abstract void ApplyBase(MonsterScript monster);

	//Abstract function to apply effects during end of work 'day'
	public abstract void ApplyDayEffects(MonsterScript monster);

	//Abstract function to apply effects during end of work 'week'
	public abstract void ApplyWeekEffects(MonsterScript monster);
}
