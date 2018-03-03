using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//this sript manages monster responses
public class InterviewManager : MonoBehaviour {

    public GameObject gameManager;
    //public GameObject resume;
    //public GameObject hiringButton;

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
			//fetches monster to interview
			monsterInstance = gameManager.GetComponent<GameManager>().currentResumes[gameManager.GetComponent<GameManager>().activeResume].GetComponent<ResumeScript>().monster; //takes the gameManagers monsterInstance for questioning
        }
        else
        {
            monsterInstance = null;
        }
    }

	//personality question
    public void Question1Response()
    {
        interviewQuestions.SetActive(false);
        interviewResponse.SetActive(true);
		if(monsterInstance.GetComponent<MonsterScript>().traitName == "Cowardly")
		{
			responseText.text = "I wouldn't say I would run inta fire or nothing... I just like to not be the only one fighting.";
		}
		else if(monsterInstance.GetComponent<MonsterScript>().traitName == "Guardian")
		{
			responseText.text = "Someone has to look out for the little guys, and that someone is me.";
		}
		else if(monsterInstance.GetComponent<MonsterScript>().traitName == "Tyrant")
		{
			responseText.text = "It's live or die around here. If someone gets crushed, it's n skin off my back.";
		}
		else if(monsterInstance.GetComponent<MonsterScript>().traitName == "Reckless")
		{
			responseText.text = "I love to fight! No bones aboout it!";
		}
		else if(monsterInstance.GetComponent<MonsterScript>().traitName == "Wary")
		{
			responseText.text = "I'm not scared! And that rattling isn't my bones either!";
		}
		else if(monsterInstance.GetComponent<MonsterScript>().traitName == "Prideful")
		{
			responseText.text = "I'm a bona fide top fighter! Just watch me!";
		}
		else if(monsterInstance.GetComponent<MonsterScript>().traitName == "Workaholic")
		{
			responseText.text = "As you can see, I have already worked myself to the bone.";
		}
		else if(monsterInstance.GetComponent<MonsterScript>().traitName == "Slacker")
		{
			responseText.text = "Tibia honest, I do love to nap every now and then.";
		}
		else if(monsterInstance.GetComponent<MonsterScript>().traitName == "Fancy")
		{
			responseText.text = "Does this tie match my hair? Wait, don't answer that.";
		}
		else if(monsterInstance.GetComponent<MonsterScript>().traitName == "Gross")
		{
			responseText.text = "I promise I will wash my bones at least once a... year";
		}
		else if(monsterInstance.GetComponent<MonsterScript>().traitName == "Flirty")
		{
			responseText.text = "I consider myself more of a lover than a fighter, and you know with me the Bone-Zone is always open.";
		}
    }

	//work ethic question
    public void Question2Response()
    {
        interviewQuestions.SetActive(false);
        interviewResponse.SetActive(true);
		if(monsterInstance.GetComponent<MonsterScript>().workEthic == -1)
		{
			responseText.text = "I guess I'm a bit of a slacker.";
		}
		else if(monsterInstance.GetComponent<MonsterScript>().workEthic == 0)
		{
			responseText.text = "I like the feeling I get after a hard day's work.";
		}
		else if(monsterInstance.GetComponent<MonsterScript>().workEthic == 1)
		{
			responseText.text = "I can't live without my work. So please hire me...";
		}
    }

	//Stat question
    public void Question3Response()
    {
        interviewQuestions.SetActive(false);
        interviewResponse.SetActive(true);
		responseText.text = "I hate fire! Fire can do some serious damage to me!"; //+ monsterInstance.GetComponent<MonsterScript>().trait1 + " and " + monsterInstance.GetComponent<MonsterScript>().trait2;
    }

	//stat question
	public void Question4Response()
	{
		interviewQuestions.SetActive(false);
		interviewResponse.SetActive(true);
		responseText.text = "I enjoy walking around town and killing humans.";
	}

	//archetype question
	public void Question5Response()
	{
		interviewQuestions.SetActive(false);
		interviewResponse.SetActive(true);
		responseText.text = "I drink a lot of milk! It's pretty good stuff.";
	}

    public void InterviewResponse()
    {
        interviewResponse.SetActive(false);
        interviewQuestions.SetActive(true);
    }

    public void ExitInterview() //exits interview and hides the interview UI
    {
		gameManager.GetComponent<GameManager>().interviewButtons.SetActive(false);
		gameManager.GetComponent<GameManager>().interviewImage.SetActive(false);
		gameManager.GetComponent<GameManager>().interviewBackground.SetActive(false);
		gameManager.GetComponent<GameManager>().interviewExit.SetActive(false);
		gameManager.GetComponent<GameManager>().constructionButton.SetActive(true);
		gameManager.GetComponent<GameManager>().applicationsButton.SetActive(true);

		/*if (gameManager.GetComponent<GameManager>().resume != null)
		{
			gameManager.GetComponent<GameManager>().resume.SetActive(true);
		}
        gameManager.GetComponent<GameManager>().interviewing = false;*/
    }

}
