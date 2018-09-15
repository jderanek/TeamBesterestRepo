using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cliquey : BaseTrait
{
    public Cliquey() : base() { }
    public Cliquey(List<PersonalityTags.Tag> tags) : base(tags) { }

    public override void OnDeath(BaseMonster monster) { }

    public override void OnSpawn() { }

    public override void OnKill(BaseHero killed) { }

    public override void OnHeroDeath(BaseHero dead) { }

    public override int OnAttack(int dmg, BaseHero attacked, BaseMonster attacker = null)
    {
        if (attacked.getRoom().monsterSize == attacked.getRoom().size)
            return dmg * 3;
        return dmg;
    }
}