using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PersonalityTrait {
    PersonalityTags.Tag tag1;
    PersonalityTags.Tag tag2;
    PersonalityTags.Tag tag3;

    public abstract void ApplyEffect(BaseMonster monster);
}
