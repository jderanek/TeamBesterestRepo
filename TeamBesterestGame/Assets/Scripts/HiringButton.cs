using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HiringButton : MonoBehaviour
{ 
    public GameObject monsterInstance;

    // Use this for initialization
    void Start()
    {

    }

    void Update()
    {

    }

    private void OnMouseOver()
    {
        monsterInstance = GameObject.FindGameObjectWithTag("ResumeButton").GetComponent<HiringUIScript>().monsterInstance;
        if (Input.GetMouseButtonDown(0))
        {
            GameObject.FindGameObjectWithTag("Resume").SetActive(false);
            GameObject.Find("Hiring Button").GetComponent<HiringUIScript>().resumeUp = false;
            GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>().PickUpObject(monsterInstance);
        }
    }
}
