using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterScript : MonoBehaviour
{


    public int[] StartingHealth;
    public int CurrentHealth;

    public int[] PossibleDamage;
    public int AttackDamage;

    public GameObject[] PossibleTraits;
    public GameObject Trait1;
    public GameObject Trait2;

    private GameObject Resume;
    private GameObject MonsterInstance;
    public bool MonsterGrabbed;
    private GameObject ResumeButton;
    private GameObject Hero;

    private bool HeroInRange;

    void Awake()
    {
        CurrentHealth = StartingHealth[Random.Range(0, StartingHealth.Length)];
        AttackDamage = PossibleDamage[Random.Range(0, PossibleDamage.Length)];
        int Trait1Index = Random.Range(0, PossibleTraits.Length);
        int Trait2Index = Random.Range(0, PossibleTraits.Length);
        Trait1 = PossibleTraits[Trait1Index];
        Trait2 = PossibleTraits[Trait2Index];

        Resume = GameObject.FindGameObjectWithTag("Resume");
        MonsterGrabbed = true;
        MonsterInstance = this.gameObject;

        ResumeButton = GameObject.FindGameObjectWithTag("ResumeButton");
        HeroInRange = false;
    }

    void Update()
    {
        Hero = GameObject.FindGameObjectWithTag("Hero");

        //Monster placement start
        if (MonsterInstance != null && MonsterGrabbed == true)
        {
            Vector3 mousePos = Input.mousePosition;
            mousePos.z = transform.position.z - Camera.main.transform.position.z;
            MonsterInstance.transform.position = Camera.main.ScreenToWorldPoint(mousePos);
        }

        if (Input.GetMouseButtonDown(1) && MonsterGrabbed == true)
        {
            MonsterGrabbed = false;
            ResumeButton.GetComponent<HiringUIScript>().ResumeUp = false;
            //Resume.SetActive(true);
        }
        //Monster placement end

        if (HeroInRange == true)
        {
            //Attack();
        }

    }


    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Hero")
        {
            Hero = other.gameObject;
            HeroInRange = true;
        }
    }

    /*public void Attack()
    {
        Hero.GetComponent<HeroScript>().TakeDamage(AttackDamage);

    }*/

    public void TakeDamage(int damageTaken)
    {
        /*heroHp -= damageTaken;
        if (heroHp <= 0)
        {
            Death();
        }*/
    }

    private void Death()
    {
        Destroy(this);
    }
}
