using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Gets a massive damage and armor bonus when at 25% health
public class Procrastinator : BaseTrait
{
    bool isBoosted = false;
    public Procrastinator() : base() { this.traitName = "Procrastinator"; }
    public Procrastinator(List<PersonalityTags.Tag> tags) : base(tags) { this.traitName = "Procrastinator"; }

    public override void OnDeath(BaseMonster monster) { }

    public override void OnSpawn() { }

    public override void OnKill(BaseHero killed) { }

    public override void OnHeroDeath(BaseHero dead) { }

    public override void OnTimePass(BaseMonster monster)
    {
        if (!isBoosted && monster.getCurHealth() < (monster.getMaxHealth()/4))
        {
            monster.setCurDamage(monster.getCurDamage() * 5);
            monster.setDefense(monster.getDefense() * 5);
        } else if (isBoosted)
        {
            monster.setCurDamage(monster.getCurDamage() / 5);
            monster.setDefense(monster.getDefense() / 5);
        }
    }
}
