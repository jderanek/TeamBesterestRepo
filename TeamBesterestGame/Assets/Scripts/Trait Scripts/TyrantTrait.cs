using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TyrantTrait : TraitBase {

	//Empty function
	public override void ApplyBase(MonsterScript monster) {
		this.setName ("Tyrant");
	}

	//Empty function
	public override void ApplyDayEffects(MonsterScript monster) {
	}

	//Finds all monsters in the room 2 sizes smaller than itself
	public override void ApplyWeekEffects(MonsterScript monster) {
		RoomScript room = monster.myRoom.GetComponent<RoomScript> ();
		List<GameObject> smallMonsters = new List<GameObject> ();

		foreach (GameObject mon in room.roomMembers) {
			MonsterScript monScript = mon.GetComponent<MonsterScript> ();
			if (monScript != null && monScript != monster) {
				if (monster.size - monScript.size >= 2)
					smallMonsters.Add (mon);
			}
		}

		//Eats random monster if there is at least one
		if (smallMonsters.Count > 1) {
			int toEat = Random.Range (0, smallMonsters.Count - 1);
			MonsterScript smallScript = smallMonsters [toEat].GetComponent<MonsterScript> ();
			if (smallScript != null)
				smallScript.TakeDamage (smallScript.currentHealth);
		}
	}
}

