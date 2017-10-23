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
	public List<GameObject> monsters;
	private GameObject currentMonster;
	private MonsterScript currentMonsterScript;
	public bool monsterInRange;
    
    // Use this for initialization
    void Start ()
    {
		monsterInRange = false;
	}

    // Update is called once per frame
    void Update()
    {
        //movement script
		transform.position = Vector3.MoveTowards(transform.position, monsterPosition.position, movementSpeed * Time.deltaTime);

		if (monsterInRange) {
			Attack();
		}

    }

    
    void OnTriggerEnter2D(Collider2D other)
    {
		if (other.CompareTag ("Monster")) 
		{
			Debug.Log ("Monster Hit");
			currentMonsterScript = other.gameObject.GetComponent<MonsterScript>();
			currentMonster = other.gameObject;
			movementSpeed = 0;
			monsterInRange = true;
		}
    }

	public void Attack()
	{
		currentMonster.GetComponent<MonsterScript>().TakeDamage(damage);
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
		currentMonsterScript.heroInRange = false; 
        Destroy(this);
    }
}
