using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CowardlyTrait : TraitBase {

	//Empty function
	public override void ApplyBase(MonsterScript monster) {
	}

	//Gains morale if room threat is high, loses it if it is low
	public override void ApplyDayEffects(MonsterScript monster) {
		int threat = monster.myRoom.GetComponent<RoomScript> ().roomThreat;

		switch (threat) {
		case 1:
			monster.morale = Mathf.Clamp01 (monster.morale - .50f);
			break;
		case 2:
				monster.morale = Mathf.Clamp01 (monster.morale - .25f);
			break;
		case 3:
			monster.morale = Mathf.Clamp01 (monster.morale - .10f);
			break;
		case 4:
			monster.morale = Mathf.Clamp01 (monster.morale);
			break;
		case 5:
			monster.morale = Mathf.Clamp01 (monster.morale + .10f);
			break;
		case 6:
			monster.morale = Mathf.Clamp01 (monster.morale + .25f);
			break;
		default:
			monster.morale = Mathf.Clamp01 (monster.morale + .50f);
			break;
		}
	}

	//Empty function
	public override void ApplyWeekEffects(MonsterScript monster) {
		return;
	}
}
