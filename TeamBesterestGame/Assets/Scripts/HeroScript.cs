using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroScript : MonoBehaviour {

    private int aggro;
    public int damage;
    public int attackSpeed;
    public int heroHp;
    public int movementSpeed;
    public int traits;
    //public transform monsterPosition;

    private GameObject monster;
    
    // Use this for initialization
    void Start ()
    {

	}

    // Update is called once per frame
    void Update()
    {
        ///movement script
        //transform.position = Vector3.moveTowards(transform.position, monsterPosition.position, movementSpeed * time.deltaTime);
    }

    
    /*void OnCollisionEnter(Collider)
    {
        monsterCollide = monster.GetComponents<MonsterScript>();
        movementSpeed =  0;

    }*/

    public void TakeDamage(int damageTaken)
    {
        heroHp -= damageTaken;

        if (heroHp <= 0)
        {
            Death();
        }
    }

    private void Death()
    {
        Destroy(this);
    }
}
