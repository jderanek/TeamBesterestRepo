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
    public GameObject interviewResponse;

	public int q31;
	public int q32;
	public int q41;
	public int q42;

	public Text q3Text;
	public Text q4Text;

	//arraylists for stat questions.
	public string[][] statQuestions = {hpQuestions, attackQuestions, nerveQuestions, threatQuestions}; //wageQuestions};
	//public string[][] questionFour = {hpQuestions, attackQuestions, nerveQuestions, threatQuestions}; //wageQuestions};

	//arraylists for specific stat questions
	public static string[] hpQuestions = {"Are you able to take a lot of punishment?", "Can you last long in a fight?", 
		"Can you take what you dish out?", "After a big fight, are you still able to stand?", "Can you outlast a tough hero?"};
	public static string[] attackQuestions = {"Are you good in a fight?", "Do you have any combat experience?", 
		"After a big fight, are you still able to stand?", "How much battle training do you have?", "Can you hold your own in a fight?"};
	public static string[] nerveQuestions = {"Do you become uneasy in stressful situations?", "If a strong hero showed up, how would you respond", 
		"Do you buck under pressure?", "Can you handle stressful situations?", "Do you lose your nerve easily?"};
	public static string[] threatQuestions = {"Has anyone ever said you have an imposing presence?", "If you speak up do people pay attention to you?", 
		"Do heroes seem to prioritize you over others?", "Do you seem to draw everyone's focus?", "Has anyone ever called you the center of attention?"};
	//public string[] wageQuestions;

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
       // interviewQuestions.SetActive(false);
        //interviewResponse.SetActive(true);
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
			responseText.text = "It's live or die around here. If someone gets crushed, it's no skin off my back.";
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
			responseText.text = "I'm a bone-a-fide top fighter! Just watch me!";
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
			responseText.text = "I'm kinda dirty, but if you hire me I promise to wash my bones at least once a... year";
		}
		else if(monsterInstance.GetComponent<MonsterScript>().traitName == "Flirty")
		{
			responseText.text = "I consider myself more of a lover than a fighter, and you know with me the Bone-Zone is always open.";
		}
		gameManager.GetComponent<GameManager>().PassTime(1);
    }

	//work ethic question
    public void Question2Response()
    {
       // interviewQuestions.SetActive(false);
        //interviewResponse.SetActive(true);
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
		gameManager.GetComponent<GameManager>().PassTime(1);
    }

	//Stat question
    public void Question3Response()
    {
       // interviewQuestions.SetActive(false);
        //interviewResponse.SetActive(true);
		if (q31 == 0)//hp responses
		{
			if(monsterInstance.GetComponent<MonsterScript>().startingHealth > monsterInstance.GetComponent<MonsterScript>().averageHealth)
			{
				responseText.text = "I can take a beating before I start to crack.";
			}
			else if(monsterInstance.GetComponent<MonsterScript>().startingHealth == monsterInstance.GetComponent<MonsterScript>().averageHealth)
			{
				responseText.text = "My bones are normal for the most part.";
			}
			else if(monsterInstance.GetComponent<MonsterScript>().startingHealth < monsterInstance.GetComponent<MonsterScript>().averageHealth)
			{
				responseText.text = "Uh... I would prefer not risking a fracture...";
			}
		}
		else if (q31 == 1)//attack responses
		{
			if(monsterInstance.GetComponent<MonsterScript>().attackDamage > monsterInstance.GetComponent<MonsterScript>().averageDamage)
			{
				responseText.text = "I used to get into scuffles every now and then, I came out on top most of the time.";
			}
			else if(monsterInstance.GetComponent<MonsterScript>().attackDamage == monsterInstance.GetComponent<MonsterScript>().averageDamage)
			{
				responseText.text = "Ehh... I am average at best...";
			}
			else if(monsterInstance.GetComponent<MonsterScript>().attackDamage < monsterInstance.GetComponent<MonsterScript>().averageDamage)
			{
				responseText.text = "Combat isn't really my thing, but I'll give it a shot.";
			}
		}
		else if (q31 == 2)//nerve responses
		{
			if(monsterInstance.GetComponent<MonsterScript>().curNerve > monsterInstance.GetComponent<MonsterScript>().baseNerve)
			{
				responseText.text = "I'm not scared of anything.";
			}
			else if(monsterInstance.GetComponent<MonsterScript>().curNerve == monsterInstance.GetComponent<MonsterScript>().baseNerve)
			{
				responseText.text = "I don't really scare easy, but I'm not extremely courageous.";
			}
			else if(monsterInstance.GetComponent<MonsterScript>().curNerve < monsterInstance.GetComponent<MonsterScript>().baseNerve)
			{
				responseText.text = "EEP! Sorry, I got scared looking at your shadow...";
			}
		}
		else if (q31 == 3)//threat responses
		{
			if(monsterInstance.GetComponent<MonsterScript>().threatValue > 4)
			{
				responseText.text = "Give me the job, or i will hurt you.";
			}
			else if(monsterInstance.GetComponent<MonsterScript>().threatValue == 4)
			{
				responseText.text = "Heroes will attack me unless some bigger monster is nearby.";
			}
			else if(monsterInstance.GetComponent<MonsterScript>().threatValue < 4)
			{
				responseText.text = "People don't really notice me all that much. In fact, I'm surprised you notice me right now.";
			}
		}
		gameManager.GetComponent<GameManager>().PassTime(1);
		UpdateQuestions();
    }

	//stat question
	public void Question4Response()
	{
		//interviewQuestions.SetActive(false);
		//interviewResponse.SetActive(true);
		if (q41 == 0)//hp responses
		{
			if(monsterInstance.GetComponent<MonsterScript>().startingHealth > monsterInstance.GetComponent<MonsterScript>().averageHealth)
			{
				responseText.text = "I can take a beating before I start to crack.";
			}
			else if(monsterInstance.GetComponent<MonsterScript>().startingHealth == monsterInstance.GetComponent<MonsterScript>().averageHealth)
			{
				responseText.text = "My bones are normal for the most part.";
			}
			else if(monsterInstance.GetComponent<MonsterScript>().startingHealth < monsterInstance.GetComponent<MonsterScript>().averageHealth)
			{
				responseText.text = "Uh... I would prefer not risking a fracture...";
			}
		}
		else if (q41 == 1)//attack responses
		{
			if(monsterInstance.GetComponent<MonsterScript>().attackDamage > monsterInstance.GetComponent<MonsterScript>().averageDamage)
			{
				responseText.text = "I used to get into scuffles every now and then, I came out on top most of the time.";
			}
			else if(monsterInstance.GetComponent<MonsterScript>().attackDamage == monsterInstance.GetComponent<MonsterScript>().averageDamage)
			{
				responseText.text = "Ehh... I am average at best...";
			}
			else if(monsterInstance.GetComponent<MonsterScript>().attackDamage < monsterInstance.GetComponent<MonsterScript>().averageDamage)
			{
				responseText.text = "Combat isn't really my thing, but I'll give it a shot.";
			}
		}
		else if (q41 == 2)//nerve responses
		{
			if(monsterInstance.GetComponent<MonsterScript>().curNerve > monsterInstance.GetComponent<MonsterScript>().baseNerve)
			{
				responseText.text = "I'm not scared of anything.";
			}
			else if(monsterInstance.GetComponent<MonsterScript>().curNerve == monsterInstance.GetComponent<MonsterScript>().baseNerve)
			{
				responseText.text = "I don't really scare easy, but I'm not extremely courageous.";
			}
			else if(monsterInstance.GetComponent<MonsterScript>().curNerve < monsterInstance.GetComponent<MonsterScript>().baseNerve)
			{
				responseText.text = "EEP! Sorry, I got scared looking at your shadow...";
			}
		}
		else if (q41 == 3)//threat responses
		{
			if(monsterInstance.GetComponent<MonsterScript>().threatValue > 4)
			{
				responseText.text = "Give me the job, or i will hurt you.";
			}
			else if(monsterInstance.GetComponent<MonsterScript>().threatValue == 4)
			{
				responseText.text = "Heroes will attack me unless some bigger monster is nearby.";
			}
			else if(monsterInstance.GetComponent<MonsterScript>().threatValue < 4)
			{
				responseText.text = "People don't really notice me all that much. In fact, I'm surprised you notice me right now.";
			}
		}
		gameManager.GetComponent<GameManager>().PassTime(1);
		UpdateQuestions();
	}

	//archetype question
	public void Question5Response()
	{
		//interviewQuestions.SetActive(false);
		//interviewResponse.SetActive(true);
		responseText.text = "Archetype question: TBD";
		gameManager.GetComponent<GameManager>().PassTime(1);
	}

	//replaces text on the stat questions
	public void UpdateQuestions()
	{
		q31 = Random.Range(0, statQuestions.Length);
		q32 = Random.Range(0, 4);
		q41 = Random.Range(0, statQuestions.Length);
		q42 = Random.Range(0, 4);

		q3Text.text = statQuestions[q31][q32];
		q4Text.text = statQuestions[q41][q42];
	}

    public void InterviewResponse()
    {
        //interviewResponse.SetActive(false);
        //interviewQuestions.SetActive(true);
    }

    public void ExitInterview() //exits interview and hides the interview UI
    {
		//gameManager.GetComponent<GameManager>().interviewButtons.SetActive(false);
        gameManager.GetComponent<GameManager>().Q1.SetActive(false);
        gameManager.GetComponent<GameManager>().Q2.SetActive(false);
        gameManager.GetComponent<GameManager>().Q3.SetActive(false);
        gameManager.GetComponent<GameManager>().Q4.SetActive(false);
        gameManager.GetComponent<GameManager>().Q5.SetActive(false);
        gameManager.GetComponent<GameManager>().interviewResponse.SetActive(false);
		//gameManager.GetComponent<GameManager>().interviewBackground.SetActive(false);
        gameManager.GetComponent<GameManager>().interviewImage.SetActive(false);
        gameManager.GetComponent<GameManager>().interviewExit.SetActive(false);
		gameManager.GetComponent<GameManager>().constructionButton.SetActive(true);
		gameManager.GetComponent<GameManager>().applicationsButton.SetActive(true);
        //gameManager.GetComponent<GameManager>().contextMenu.SetActive(true);
        interviewResponse.SetActive(false);

		/*if (gameManager.GetComponent<GameManager>().resume != null)
		{
			gameManager.GetComponent<GameManager>().resume.SetActive(true);
		}
        gameManager.GetComponent<GameManager>().interviewing = false;*/
    }

}
