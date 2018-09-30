using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Has a growing chance to instakill low health heroes
public class Opportunist : BaseTrait
{
    public Opportunist() : base() { traitName = "Oppurtunist"; }
    public Opportunist(List<PersonalityTags.Tag> tags) : base(tags) { traitName = "Oppurtunist"; }

    public override void OnDeath(BaseMonster monster) { }

    public override int OnAttack(int dmg, BaseHero attacked, BaseMonster attacker) {
        float healthRatio = attacked.getMaxHealth() / attacked.curHealth;
        if (healthRatio <= .5)
        {
            if (Random.Range(0, 1) >= (.1 + ((.5 - healthRatio) * 2)))
                attacked.Death();
        }

        return dmg;
    }

    public override void OnSpawn() { }

    public override void OnKill(BaseHero killed) { }

    public override void OnHeroDeath(BaseHero dead) { }
}
