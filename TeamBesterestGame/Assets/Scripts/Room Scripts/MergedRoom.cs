using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MergedRoom : BaseRoom {

	public List<BaseRoom> rooms = new List<BaseRoom>();

	//Start function that doesn't call Initialize, because it's merged not created
	//Instead, sets gameManager
	void Start() {
		this.SetManager ();
	}

	override public void RoomEffect(BaseMonster monster){
	}

	public override void RemoveRoomEffect(BaseMonster monster){
	}

	//Combines two basic rooms into a single merged room
	public static MergedRoom MergeBase (BaseRoom room1, BaseRoom room2) {
		var roomObject = new GameObject ();
		MergedRoom merged = roomObject.AddComponent<MergedRoom> () as MergedRoom;
		merged.SetManager ();
		merged.myX = room1.myX;
		merged.myY = room2.myY;

		merged.rooms.Add (room1);
		merged.rooms.Add (room2);

		room1.master = merged;
		room2.master = merged;

		//Replaces previous rooms with this room in the list
		merged.gameManager.roomList [room1.myX, room1.myY] = roomObject;
		merged.gameManager.roomList [room2.myX, room2.myY] = roomObject;

		//Adds the stats of the merged rooms together
		merged.size = room1.size + room2.size;
		merged.roomThreat = room1.roomThreat + room2.roomThreat;
		merged.goldCapacity = room1.goldCapacity + room2.goldCapacity;
		merged.currentGold = room1.currentGold + room2.currentGold;

		//Moves all monsters into the new room and removes them from old
		merged.roomMembers.AddRange (room1.roomMembers);
		room1.roomMembers.Clear ();
		merged.roomMembers.AddRange (room2.roomMembers);
		room2.roomMembers.Clear ();

		//Moves all heroes into the merged room and removes them from the old
		merged.heroesInRoom.AddRange (room1.heroesInRoom);
		room1.heroesInRoom.Clear ();
		merged.heroesInRoom.AddRange (room2.heroesInRoom);
		room2.heroesInRoom.Clear ();

		//Assigns bools from monster and hero count
		if (merged.heroesInRoom.Count > 0)
			merged.heroInRoom = true;

		if (merged.roomMembers.Count > 0)
			merged.monsterInRoom = true;

		BaseRoom toRemove;

		//Removes rooms from previous adjacent rooms and clears them to prevent mix up
		//They should be automatically refilled when reactivated
		foreach (GameObject room in room1.adjacentRooms) {
			toRemove = room.GetComponent<BaseRoom> ();
			toRemove.adjacentRooms.Remove (room1.gameObject);
		}
		room1.adjacentRooms.Clear ();
		room1.neighborRooms.Clear ();

		foreach (GameObject room in room2.adjacentRooms) {
			toRemove = room.GetComponent<BaseRoom> ();
			toRemove.adjacentRooms.Remove (room2.gameObject);
		}
		room2.adjacentRooms.Clear ();
		room2.neighborRooms.Clear ();

		return merged;
	}

	//Merges together a MergedRoom and a BaseRoom
	public static MergedRoom MergeMixed (MergedRoom merged, BaseRoom toMerge) {
		merged.rooms.Add (toMerge);

		//Replaces previous rooms with this room in the list
		merged.gameManager.roomList [toMerge.myX, toMerge.myY] = merged.gameObject;

		//Adds the stats of the merged rooms together
		merged.size += toMerge.size;
		merged.roomThreat += toMerge.roomThreat;
		merged.goldCapacity += toMerge.goldCapacity;
		merged.currentGold += toMerge.currentGold;

		toMerge.master = merged;

		//Moves all monsters into the new room and removes them from old
		merged.roomMembers.AddRange (toMerge.roomMembers);
		toMerge.roomMembers.Clear ();

		//Moves all heroes into the merged room and removes them from the old
		merged.heroesInRoom.AddRange (toMerge.heroesInRoom);
		toMerge.heroesInRoom.Clear ();

		//Assigns bools from monster and hero count
		if (merged.heroesInRoom.Count > 0)
			merged.heroInRoom = true;

		if (merged.roomMembers.Count > 0)
			merged.monsterInRoom = true;

		BaseRoom toRemove;

		//Removes rooms from previous adjacent rooms and clears them to prevent mix up
		//They should be automatically refilled when reactivated
		foreach (GameObject room in toMerge.adjacentRooms) {
			toRemove = room.GetComponent<BaseRoom> ();
			toRemove.adjacentRooms.Remove (toMerge.gameObject);
		}
		toMerge.adjacentRooms.Clear ();
		toMerge.neighborRooms.Clear ();

		return merged;
	}

	//Merges together two MergedRooms
	public static MergedRoom MergeMerged (MergedRoom merged, MergedRoom toMerge) {
		//Adds all rooms from toMerge, and clears it to prepare for deletion
		//Also replaces them in the roomList
		foreach (BaseRoom room in toMerge.rooms) {
			merged.rooms.Add (room);
			merged.gameManager.roomList [room.myX, room.myY] = merged.gameObject;
			room.master = merged;
		}
		toMerge.rooms.Clear ();

		//Adds the stats of the merged rooms together
		merged.size += toMerge.size;
		merged.roomThreat += toMerge.roomThreat;
		merged.goldCapacity += toMerge.goldCapacity;
		merged.currentGold += toMerge.currentGold;

		//Moves all monsters into the new room and removes them from old
		merged.roomMembers.AddRange (toMerge.roomMembers);
		toMerge.roomMembers.Clear ();

		//Moves all heroes into the merged room and removes them from the old
		merged.heroesInRoom.AddRange (toMerge.heroesInRoom);
		toMerge.heroesInRoom.Clear ();

		//Assigns bools from monster and hero count
		if (merged.heroesInRoom.Count > 0)
			merged.heroInRoom = true;

		if (merged.roomMembers.Count > 0)
			merged.monsterInRoom = true;

		BaseRoom toRemove;

		//Removes rooms from previous adjacent rooms and clears them to prevent mix up
		//They should be automatically refilled when reactivated
		foreach (GameObject room in toMerge.adjacentRooms) {
			toRemove = room.GetComponent<BaseRoom> ();
			toRemove.adjacentRooms.Remove (toMerge.gameObject);
		}
		toMerge.adjacentRooms.Clear ();
		toMerge.neighborRooms.Clear ();

		GameObject.Destroy (toMerge.gameObject);

		return merged;
	}

	//Takes two rooms and merges them based on type
	public static MergedRoom MergeStart(BaseRoom room1, BaseRoom room2) {
		if (room1 is MergedRoom && room2 is MergedRoom)
			return MergeMerged (room1 as MergedRoom, room2 as MergedRoom);
		else if (room1 is MergedRoom)
			return MergeMixed (room1 as MergedRoom, room2);
		else if (room2 is MergedRoom)
			return MergeMixed (room2 as MergedRoom, room1);
		else
			return MergeBase (room1, room2);
	}

	//Takes a list of rooms and merges them all
	public static void MergeRooms(List<GameObject> rooms) {
		if (rooms.Count < 2)
			return;
		else if (rooms.Count > 4)
			return;
		
		MergedRoom final = MergeStart (rooms [0].GetComponent<BaseRoom> (), rooms [1].GetComponent<BaseRoom> ());

		for (int i = 2; i < rooms.Count; i++)
			final = MergeStart (final, rooms [i].GetComponent<BaseRoom> ());
	}

	//Spreads the gold from all rooms evenly
	public void UpdateGold() {
		int gold = this.currentGold;
		//Assigns gold evenly to the rooms, and changes their graphics
		foreach (BaseRoom room in rooms) {
			if (gold > 300) {
				room.currentGold = 300;
				gold -= 300;
				room.UpdateCoins ();
			} else {
				room.currentGold = gold;
				gold = 0;
				room.UpdateCoins ();
			}
		}
	}

	//Updates the visuals of all the monsters in the room
	public override void UpdateMonsters() {
		int curIndex = 0;
		int maxIndex = this.rooms.Count - 1;
		int curMon = 0;

		foreach (GameObject monster in roomMembers) {
			monster.transform.position = new Vector3(
				rooms[curIndex].gameObject.transform.position.x + .25f,
				rooms[curIndex].gameObject.transform.position.y + .25f - (curMon * .25f),
				0
			);

			curMon += 1;

			if (curMon > 2) {
				curIndex += 1;
				curMon = 0;
			}
			if (curIndex > maxIndex)
				curIndex = 0;
		}
	}

	//Updates the visuals of all the monsters in the room
	public override void UpdateHeroes() {
		int curIndex = 0;
		int maxIndex = this.rooms.Count - 1;
		int curHero = 0;

		foreach (GameObject hero in this.heroesInRoom) {
			hero.GetComponent<BaseHero>().targetPos = new Vector3(
				rooms[curIndex].gameObject.transform.position.x - .25f,
				rooms[curIndex].gameObject.transform.position.y + .25f - (curHero * .25f),
				0
			);

			curHero += 1;

			if (curHero > 2) {
				curIndex += 1;
				curHero = 0;
			}
			if (curIndex > maxIndex)
				curIndex = 0;
		}
	}
}
