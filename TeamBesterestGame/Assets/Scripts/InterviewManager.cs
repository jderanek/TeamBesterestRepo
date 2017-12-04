using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InterviewManager : MonoBehaviour {

    public GameObject gameManager;
    public GameObject resume;
    public GameObject hiringButton;

    public GameObject monsterInstance;

    public GameObject interviewQuestions;
    public GameObject questionOne;
    public GameObject questionTwo;
    public GameObject questionThree;
    public GameObject interviewResponse;

    public Text responseText;

    // Use this for initialization
    void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameController");
    }

    // Update is called once per frame
    void Update()
    {
        if (gameManager.GetComponent<GameManager>().interviewing)
        {
            monsterInstance = hiringButton.GetComponent<HiringUIScript>().monsterInstance;
        }
        else
        {
            monsterInstance = null;
        }
    }

    public void Question1Response()
    {
        interviewQuestions.SetActive(false);
        interviewResponse.SetActive(true);
        responseText.text = "I have " + monsterInstance.GetComponent<MonsterScript>().startingHealth + " health.";
    }

    public void Question2Response()
    {
        interviewQuestions.SetActive(false);
        interviewResponse.SetActive(true);
        responseText.text = "My damage is " + monsterInstance.GetComponent<MonsterScript>().attackDamage;
    }

    public void Question3Response()
    {
        interviewQuestions.SetActive(false);
        interviewResponse.SetActive(true);
        responseText.text = "I'm " + monsterInstance.GetComponent<MonsterScript>().trait1 + " and " + monsterInstance.GetComponent<MonsterScript>().trait2;
    }

    public void InterviewResponse()
    {
        interviewResponse.SetActive(false);
        interviewQuestions.SetActive(true);
    }

    public void ExitInterview()
    {
        gameManager.GetComponent<GameManager>().interviewing = false;
    }

}
