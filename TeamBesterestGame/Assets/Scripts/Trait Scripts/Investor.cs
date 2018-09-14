using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Gradually increases gold gain by 10% per dead hero. Caps at 3X gold
public class Investor : BaseTrait
{
    float goldMult = 1f;

    public Investor(List<PersonalityTags.Tag> tags) : base(tags) { }

    public override void OnDeath(BaseMonster monster) { }

    public override void OnAttack(BaseHero attacked) { }

    public override void OnSpawn() { }

    public override void OnKill(BaseHero killed)
    {
        killed.setValue((int)(killed.getValue() * goldMult));
    }

    public override void OnHeroDeath(BaseHero dead) {
        this.goldMult = Mathf.Min(goldMult + .1f, 3f);
    }
}
