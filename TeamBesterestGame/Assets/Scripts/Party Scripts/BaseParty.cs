using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseParty {

	BaseHero[] partyMembers;
	RoomScript curRoom;

	//Getter functions for variables
	public BaseHero[] getPartyMembers() {
		return this.partyMembers;
	}
	public RoomScript getRoom() {
		return this.curRoom;
	}

	//Calls all party members attack function in the current room
	public void AttackPhase() {
		foreach (BaseHero hero in partyMembers)
			hero.Attack ();
	}

	//Moves all party members to the given room
	public void MoveTo(RoomScript room) {
		this.curRoom = room;
		foreach (BaseHero hero in partyMembers)
			hero.MoveTo (curRoom);
	}
}
