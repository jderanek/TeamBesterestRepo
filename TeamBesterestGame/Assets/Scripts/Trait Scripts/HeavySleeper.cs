using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Recovers 25% health after combat
public class HeavySleeper : BaseTrait
{
    public HeavySleeper() : base() { traitName = "Heavy Sleeper"; }
    public HeavySleeper(List<PersonalityTags.Tag> tags) : base(tags) { traitName = "Heavy Sleeper"; }

    public override void OnDeath(BaseMonster monster) { }

    public override void OnSpawn() { }

    public override void OnKill(BaseHero killed) { }

    public override void OnHeroDeath(BaseHero dead) { }

    public override void OnCombatEnd(BaseMonster monster)
    {
        monster.Heal((int)(monster.getMaxHealth() * .25));
    }
}
