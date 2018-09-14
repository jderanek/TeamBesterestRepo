using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Changes some of monsters stats, such as damage
public abstract class BaseEthic {
	//Different recipient types
	public enum Target {
		Self,
		Others,
		AllStack,
		AllNoStack,
		None
	}

	///<summary>
	/// Returns list of all monsters that are targets of the Ethic
	///</summary>
	/// <param name="mon">BaseMonster to target from</param>
	/// <param name="tar">Type of target to select monsters</param>
	public static List<BaseMonster> GetTargets(BaseMonster mon, Target tar = Target.None, string tag = "") {
		List<BaseMonster> targets = new List<BaseMonster> ();
		BaseRoom room = mon.getCurRoom ();
		BaseMonster monScript;

		switch (tar) {
		case Target.Self:
			targets.Add (mon);
			return targets;
		case Target.Others:
			foreach (GameObject monObject in room.roomMembers) {
				monScript = monObject.GetComponent<BaseMonster> ();
				if (monScript != mon)
					targets.Add (monScript);
			}
			return targets;
		case Target.AllStack:
			foreach (GameObject monObject in room.roomMembers) {
				monScript = monObject.GetComponent<BaseMonster> ();
				targets.Add (monScript);
			}
			return targets;
		case Target.AllNoStack:
			foreach (GameObject monObject in room.roomMembers) {
				monScript = monObject.GetComponent<BaseMonster> ();
				if (!monScript.effects.Contains (tag)) {
					targets.Add (monScript);
					monScript.effects.Add (tag);
				}
			}
			return targets;
		default:
			return targets;
		}
			
	}

	//Applies the effects of this ethic to all targeted monsters
	public abstract void ApplyEthic(BaseMonster monster);
}