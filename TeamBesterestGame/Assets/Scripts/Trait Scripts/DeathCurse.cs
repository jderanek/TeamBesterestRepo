using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Kills all heroes in the monsters room when the monster dies
public class DeathCurse : BaseTrait {

    public DeathCurse() : base() { }
	public DeathCurse(List<PersonalityTags.Tag> tags) : base(tags) { }

    public override void OnDeath(BaseMonster monster)
    {
        BaseRoom room = monster.getCurRoom();
        if (room.heroInRoom)
        {
            while (room.heroesInRoom.Count > 0)
            {
                room.heroesInRoom[0].GetComponent<BaseHero>().Death();
            }
        }
    }

    public override void OnSpawn() {}

    public override void OnKill(BaseHero killed) {}

    public override void OnHeroDeath(BaseHero dead) {}
}
