using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseEntity : MonoBehaviour {
    int speed;
    int baseSpeed;

    public void SetSpeed(int spd)
    {
        this.speed = spd;
    }
    public int GetSpeed()
    {
        return this.speed;
    }
    public void SetBaseSpeed(int spd)
    {
        this.baseSpeed = spd;
    }
    public int GetBaseSpeed()
    {
        return baseSpeed;
    }

    public virtual void Attack() { }
}
