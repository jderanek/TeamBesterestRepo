using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CemetaryRoom : BaseRoom {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    override public void RoomEffect(BaseMonster monster)
    {
        if (String.Equals(monster.getArchetype(), "Undead"))
        {
            monster.setCurDamage(monster.getBaseDamage() + 3);
        }
    }

    public override void RemoveRoomEffect(BaseMonster monster)
    {
        monster.setCurDamage(monster.getBaseDamage());
    }
}
