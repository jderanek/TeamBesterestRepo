using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Claustrophobic : BaseTrait
{
    public Claustrophobic(List<PersonalityTags.Tag> tags) : base(tags) { }

    public override void OnDeath(BaseMonster monster) { }

    public override void OnSpawn() { }

    public override void OnKill(BaseHero killed) { }

    public override void OnHeroDeath(BaseHero dead) { }

    public override int OnAttacked(int dmg, BaseMonster attacked, BaseHero attacker = null)
    {
        return dmg * (1 + attacked.getCurRoom().heroesInRoom.Count);
    }
}