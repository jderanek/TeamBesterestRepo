using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroScript : MonoBehaviour {

    private int aggro;
    public int damage;
    public int attackSpeed;
    public int heroHp;
	public float movementSpeed;
    public int traits;
	public Transform monsterPosition;
	public GameObject[] monsters;
	private MonsterScript currentMonster;
    
    // Use this for initialization
    void Start ()
    {

	}

    // Update is called once per frame
    void Update()
    {
        //movement script
		transform.position = Vector3.MoveTowards(transform.position, monsterPosition.position, movementSpeed);
    }

    
    void OnCollisionEnter(Collider monsterCollide)
    {
		currentMonster = monsterCollide.GetComponent<MonsterScript>();
        movementSpeed =  0;

    }

	//next two functions are what monsters will call to damage the hero
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
