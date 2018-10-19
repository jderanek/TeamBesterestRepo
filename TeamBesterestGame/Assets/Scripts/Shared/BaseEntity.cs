using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseEntity : MonoBehaviour {
    int speed;

    public void SetSpeed(int spd)
    {
        this.speed = spd;
    }
    public int GetSpeed()
    {
        return this.speed;
    }

    public virtual void Attack() { }
}
