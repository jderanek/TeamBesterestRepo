using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Recovers 25% health after combat
public class HeavySleeper : BaseTrait
{
    public HeavySleeper(List<PersonalityTags.Tag> tags) : base(tags) { }

    public override void OnDeath(BaseMonster monster) { }

    public override void OnAttack(BaseHero attacked) { }

    public override void OnSpawn() { }

    public override void OnKill(BaseHero killed) { }

    public override void OnHeroDeath(BaseHero dead) { }

    public override void OnCombatEnd(BaseMonster monster)
    {
        monster.Heal((int)(monster.getMaxHealth() * .25));
    }
}
