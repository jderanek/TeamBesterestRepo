using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterScript : MonoBehaviour {


    public int[] StartingHealth;
    public int CurrentHealth;

    public int[] PossibleDamage;
    public int AttackDamage;

    public GameObject[] PossibleTraits;
    public GameObject Trait1;
    public GameObject Trait2;


    void Awake ()
    {
        CurrentHealth = StartingHealth[Random.Range(0, StartingHealth.Length)];
        AttackDamage = PossibleDamage[Random.Range(0, PossibleDamage.Length)]; 
        int Trait1Index = Random.Range(0, PossibleTraits.Length);
        int Trait2Index = Random.Range(0, PossibleTraits.Length);
        Trait1 = PossibleTraits[Trait1Index];
        Trait2 = PossibleTraits[Trait2Index];
	}
	
	void Update ()
    {
		
	}

	void TakingDamage(int damageTaken)
	{
		CurrentHealth -= damageTaken;
	}
}
