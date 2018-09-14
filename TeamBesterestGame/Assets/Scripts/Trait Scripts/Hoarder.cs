using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Heroes this monster kills don't give gold
public class Hoarder : BaseTrait {
    public Hoarder(List<PersonalityTags.Tag> tags) : base(tags) { }

    public override void OnDeath(BaseMonster monster) {}

    public override void OnSpawn() {}

    public override void OnKill(BaseHero killed) {
        killed.setValue(0);
    }

    public override void OnHeroDeath(BaseHero dead) {}
}
