using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TyrantTrait : TraitBase {

	//Empty function
	public override void ApplyBase(BaseMonster monster) {
		this.setName ("Tyrant");
	}

	//Empty function
	public override void ApplyDayEffects(BaseMonster monster) {
	}

	//Finds all monsters in the room 2 sizes smaller than itself
	public override void ApplyWeekEffects(BaseMonster monster) {
		BaseRoom room = monster.getCurRoom ();
		List<GameObject> smallMonsters = new List<GameObject> ();

		foreach (GameObject mon in room.roomMembers) {
			BaseMonster monScript = mon.GetComponent<BaseMonster> ();
			if (monScript != null && monScript != monster) {
				if (monster.getSize() - monScript.getSize() >= 2)
					smallMonsters.Add (mon);
			}
		}

		//Eats random monster if there is at least one
		if (smallMonsters.Count > 1) {
			int toEat = Random.Range (0, smallMonsters.Count - 1);
			BaseMonster smallScript = smallMonsters [toEat].GetComponent<BaseMonster> ();
			if (smallScript != null)
				smallScript.TakeDamage (smallScript.getCurHealth ());
		}
	}
}

