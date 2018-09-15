using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Increases gold gain by 2x
public class MoneyMonger : BaseTrait
{
    public MoneyMonger() : base() { }
    public MoneyMonger(List<PersonalityTags.Tag> tags) : base(tags) { }

    public override void OnDeath(BaseMonster monster) { }

    public override void OnSpawn() { }

    public override void OnKill(BaseHero killed)
    {
        killed.setValue(killed.getValue() * 2);
    }

    public override void OnHeroDeath(BaseHero dead) { }
}
