using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CowardlyTrait : TraitBase {

	//Gains morale if room threat is high, loses it if it is low
	public override void ApplyDayEffects(MonsterScript monster) {
		int threat = monster.myRoom.GetComponent<RoomScript> ().roomThreat;

		switch (threat) {
		case 1:
			monster.morale = Mathf.Clamp (monster.morale - .50f, 0, 100);
			break;
		case 2:
			monster.morale = Mathf.Clamp (monster.morale - .25f, 0, 100);
			break;
		case 3:
			monster.morale = Mathf.Clamp (monster.morale - .10f, 0, 100);
			break;
		case 4:
			monster.morale = Mathf.Clamp (monster.morale, 0, 100);
			break;
		case 5:
			monster.morale = Mathf.Clamp (monster.morale + .10f, 0, 100);
			break;
		case 6:
			monster.morale = Mathf.Clamp (monster.morale + .25f, 0, 100);
			break;
		default:
			monster.morale = Mathf.Clamp (monster.morale + .50f, 0, 100);
			break;
		}
	}

	//Empty function
	public override void ApplyWeekEffects(MonsterScript monster) {
		return;
	}
}
