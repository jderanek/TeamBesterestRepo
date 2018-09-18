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
    public GameObject interviewResponse;

	/*public int q31;
	public int q32;
	public int q41;
	public int q42;

	public Text q3Text;
	public Text q4Text;*/

    //arraylists for stat questions.
    /*public string[][] statQuestions = {hpQuestions, attackQuestions, defenseQuestions};

	//arraylists for specific stat questions
	public static string[] hpQuestions = {"Are you able to take a lot of punishment?", "Can you last long in a fight?", 
		"Can you take what you dish out?", "After a big fight, are you still able to stand?", "Can you outlast a tough hero?"};
	public static string[] attackQuestions = {"Are you good in a fight?", "Do you have any combat experience?", 
		"After a big fight, are you still able to stand?", "How much battle training do you have?", "Can you hold your own in a fight?"};
    public static string[] defenseQuestions = {"How good are you at protecting yourself?", "Do big hits shake you up?", "Are you any good at blocking attacks?",
    "Do you have a sturdy defense?", "Are you good at mitigating damage?"};
	public static string[] nerveQuestions = {"Do you become uneasy in stressful situations?", "If a strong hero showed up, how would you respond", 
		"Do you buck under pressure?", "Can you handle stressful situations?", "Do you lose your nerve easily?"};
	public static string[] threatQuestions = {"Has anyone ever said you have an imposing presence?", "If you speak up do people pay attention to you?", 
		"Do heroes seem to prioritize you over others?", "Do you seem to draw everyone's focus?", "Has anyone ever called you the center of attention?"};
	//public string[] wageQuestions;
    */

    public string[][] tagQuestions = { hierarchyQuestions, aidQuestions, rudeQuestions, workethicQuestions, physicalQuestions,
        selfcenteredQuestions, speechQuestions, conditionQuestions, appearanceQuestions, appearanceQuestions, negativeQuestions };
    public static string[] hierarchyQuestions = { "Are you someone who likes to take charge?", "Do you prefer to let others take the lead?", "Are you good at following orders?" };
    public static string[] aidQuestions = { "Could I expect you to aid others willingly when they ask?", "Are you an amicable person?", "While you work, do you try to always lend a hand?" };
    public static string[] rudeQuestions = { "Do you sometimes tend to rub people the wrong way?", "Have people told you that always bring down the mood of the room?", "Would someone say you have a scary presence?" };
    public static string[] workethicQuestions = { "Can I count on you to put 100% effort into your job?", "Can you keep yourself on-task during stressful situations?", "Are you the type of person who can work for extended periods of time?" };
    public static string[] physicalQuestions = { "Tell me about the toughest brawl you have been in.", "Can you hold your own in combat?", "Do you enjoy fighting?" };
    public static string[] selfcenteredQuestions = { "Do others tend to see you as a vain person?", "Can you work well with new people?", "Have previous coworkers said that you are full of yourself?" };
    public static string[] speechQuestions = { "Do you like to meet new people?", "Have others said you are a chatterbox?", "Are you adept at mingling with your coworkers?" };
    public static string[] conditionQuestions = { "How would you describe your physique?", "Tell me about your medical history.", "Are there any physical conditions I need to know about?" };
    public static string[] appearanceQuestions = { "Tell me how others say you act like.", "How do you see yourself?", "Do you have any health problems I should know about?" };
    public static string[] negativeQuestions = { "Would your actions cause any disruption in the workplace?", "Will there be any fights or trouble if you are hired?", "Do you have any history of workplace disputes?" };

    int q11;
    int q12;
    int q21;
    int q22;
    int q31;
    int q32;

    public Text q1Text;
    public Text q2Text;
    public Text q3Text;

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
            monsterInstance = gameManager.GetComponent<GameManager>().monsterInstance;//takes the gameManagers monsterInstance for questioning
                                                                                        //might wanna use selectedObject for consistency - Nate
        }
        else
        {
            monsterInstance = null;
        }
    }

    public void Question1()
    {
        gameManager.GetComponent<GameManager>().PassTime(1);
        UpdateQuestions();
    }

    public void Question2()
    {
        gameManager.GetComponent<GameManager>().PassTime(1);
        UpdateQuestions();
    }

    public void Question3()
    {
        gameManager.GetComponent<GameManager>().PassTime(1);
        UpdateQuestions();
    }

    //old interview questions
    /*
	//personality question
    public void Question1Response()
    {
        // interviewQuestions.SetActive(false);
        //interviewResponse.SetActive(true);
        if (monsterInstance.GetComponent<BaseMonster>().getTraitName() == "Cowardly")
		{
			responseText.text = "I wouldn't say I would run inta fire or nothing... I just like to not be the only one fighting.";
		}
		else if(monsterInstance.GetComponent<BaseMonster>().getTraitName() == "Guardian")
		{
			responseText.text = "Someone has to look out for the little guys, and that someone is me.";
		}
		else if(monsterInstance.GetComponent<BaseMonster>().getTraitName() == "Tyrant")
		{
			responseText.text = "It's live or die around here. If someone gets crushed, it's no skin off my back.";
		}
		else if(monsterInstance.GetComponent<BaseMonster>().getTraitName() == "Reckless")
		{
			responseText.text = "I love to fight! No bones aboout it!";
		}
		else if(monsterInstance.GetComponent<BaseMonster>().getTraitName() == "Wary")
		{
			responseText.text = "I'm not scared! And that rattling isn't my bones either!";
		}
		else if(monsterInstance.GetComponent<BaseMonster>().getTraitName() == "Prideful")
		{
			responseText.text = "I'm a bone-a-fide top fighter! Just watch me!";
		}
		else if(monsterInstance.GetComponent<BaseMonster>().getTraitName() == "Workaholic")
		{
			responseText.text = "As you can see, I have already worked myself to the bone.";
		}
		else if(monsterInstance.GetComponent<BaseMonster>().getTraitName() == "Slacker")
		{
			responseText.text = "Tibia honest, I do love to nap every now and then.";
		}
		else if(monsterInstance.GetComponent<BaseMonster>().getTraitName() == "Fancy")
		{
			responseText.text = "Does this tie match my hair? Wait, don't answer that.";
		}
		else if(monsterInstance.GetComponent<BaseMonster>().getTraitName() == "Gross")
		{
			responseText.text = "I'm kinda dirty, but if you hire me I promise to wash my bones at least once a... year";
		}
		else if(monsterInstance.GetComponent<BaseMonster>().getTraitName() == "Flirty")
		{
			responseText.text = "I consider myself more of a lover than a fighter, and you know with me the Bone-Zone is always open.";
		}
		gameManager.GetComponent<GameManager>().PassTime(1);
    }
    
	//work ethic question
    public void Question2Response()
    {
		if(monsterInstance.GetComponent<BaseMonster>().getWorkEthic() == -1)
		{
			responseText.text = "I guess I'm a bit of a slacker.";
		}
		else if(monsterInstance.GetComponent<BaseMonster>().getWorkEthic() == 0)
		{
			responseText.text = "I like the feeling I get after a hard day's work.";
		}
		else if(monsterInstance.GetComponent<BaseMonster>().getWorkEthic() == 1)
		{
			responseText.text = "I can't live without my work. So please hire me...";
		}
		gameManager.GetComponent<GameManager>().PassTime(1);
    }
    

	//Stat question
    public void Question3Response()
    {
		if (q31 == 0)//hp responses
		{
			if(monsterInstance.GetComponent<BaseMonster>().healthTier == -1)
			{
				responseText.text = "I can take a beating before I start to crack.";
			}
			else if(monsterInstance.GetComponent<BaseMonster>().healthTier == 0)
			{
				responseText.text = "My bones are normal for the most part.";
			}
			else if(monsterInstance.GetComponent<BaseMonster>().healthTier == 1)
			{
				responseText.text = "Uh... I would prefer not risking a fracture...";
			}
		}
		else if (q31 == 1)//attack responses
		{
			if(monsterInstance.GetComponent<BaseMonster>().attackTier == -1)
			{
				responseText.text = "I used to get into scuffles every now and then, I came out on top most of the time.";
			}
			else if(monsterInstance.GetComponent<BaseMonster>().attackTier == 0)

            {
				responseText.text = "Ehh... I am average at best...";
			}
            else if (monsterInstance.GetComponent<BaseMonster>().attackTier == 1)
            {
				responseText.text = "Combat isn't really my thing, but I'll give it a shot.";
			}
		}
		else if (q31 == 2)//defense responses
		{
			if (monsterInstance.GetComponent<BaseMonster>().defenseTier == -1)
            {
				responseText.text = "I don't break that easily that's for sure.";
			}
			else if (monsterInstance.GetComponent<BaseMonster>().defenseTier == 0)
            {
				responseText.text = "I can hold my own, that's about it.";
			}
			else if (monsterInstance.GetComponent<BaseMonster>().defenseTier == 1)
            {
				responseText.text = "These old bones have seen better days...";
			}
		}
	
		gameManager.GetComponent<GameManager>().PassTime(1);
		UpdateQuestions();
    }

	//stat question
	public void Question4Response()
	{
        if (q41 == 0)//hp responses
        {
            if (monsterInstance.GetComponent<BaseMonster>().healthTier == -1)
            {
                responseText.text = "I can take a beating before I start to crack.";
            }
            else if (monsterInstance.GetComponent<BaseMonster>().healthTier == 0)
            {
                responseText.text = "My bones are normal for the most part.";
            }
            else if (monsterInstance.GetComponent<BaseMonster>().healthTier == 1)
            {
                responseText.text = "Uh... I would prefer not risking a fracture...";
            }
        }
        else if (q41 == 1)//attack responses
        {
            if (monsterInstance.GetComponent<BaseMonster>().attackTier == -1)
            {
                responseText.text = "I used to get into scuffles every now and then, I came out on top most of the time.";
            }
            else if (monsterInstance.GetComponent<BaseMonster>().attackTier == 0)

            {
                responseText.text = "Ehh... I am average at best...";
            }
            else if (monsterInstance.GetComponent<BaseMonster>().attackTier == 1)
            {
                responseText.text = "Combat isn't really my thing, but I'll give it a shot.";
            }
        }
        else if (q41 == 2)//defense responses
        {
            if (monsterInstance.GetComponent<BaseMonster>().defenseTier == -1)
            {
                responseText.text = "I don't break that easily that's for sure.";
            }
            else if (monsterInstance.GetComponent<BaseMonster>().defenseTier == 0)
            {
                responseText.text = "I can hold my own, that's about it.";
            }
            else if (monsterInstance.GetComponent<BaseMonster>().defenseTier == 1)
            {
                responseText.text = "These old bones have seen better days...";
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

        if (q31 == q41)
        {
            UpdateQuestions(q41);
        }

		q3Text.text = statQuestions[q31][q32];
		q4Text.text = statQuestions[q41][q42];
	}*/

    //replaces text on the stat questions
    public void UpdateQuestions()
    {
        q11 = Random.Range(0, tagQuestions.Length);
        q12 = Random.Range(0, 2);
        q21 = Random.Range(0, tagQuestions.Length);
        q22 = Random.Range(0, 2);
        q31 = Random.Range(0, tagQuestions.Length);
        q32 = Random.Range(0, 2);

        if (q11 == q21 && q12 == q22) // if q2 is q1
        {
            UpdateQuestions(2);
        }
        else if (q11 == q31 && q12 == q32) //if q3 is q1
        {
            UpdateQuestions(3);
        }
        else if (q21 == q31 && q22 == q32) // if q3 is q2
        {
            UpdateQuestions(3);
        }

        q1Text.text = tagQuestions[q11][q12];
        q2Text.text = tagQuestions[q21][q22];
        q3Text.text = tagQuestions[q31][q32];
    }

    public void UpdateQuestions(int q) //helper function so the questions don't match
    {

        if (q == 2) //resets q2
        {
            q21 = Random.Range(0, tagQuestions.Length);
            q22 = Random.Range(0, 2);
        }

        else if (q == 3) //resets q3
        {
            q31 = Random.Range(0, tagQuestions.Length);
            q32 = Random.Range(0, 2);
        }

        if (q11 == q21 && q12 == q22) // if q2 is q1
        {
            UpdateQuestions(2);
        }
        else if (q11 == q31 && q12 == q32) //if q3 is q1
        {
            UpdateQuestions(3);
        }
        else if (q21 == q31 && q22 == q32) // if q3 is q2
        {
            UpdateQuestions(3);
        }

        /*
        if (q31 == q41)
        {
            UpdateQuestions(q41);
        }

        q41 = Random.Range(0, 4);
        q42 = Random.Range(0, 4);
        */
    }

    public void ExitInterview() //exits interview and hides the interview UI
    {
        gameManager.GetComponent<GameManager>().monsterInstance = null;
        gameManager.GetComponent<GameManager>().interviewCanvas.SetActive(false);

        interviewResponse.SetActive(false);
        responseText.text = "Hello";
        gameManager.GetComponent<GameManager>().interviewing = false;
    }

    public void LeaveInterview() //called when you leave an interview with the red X button
    {
        gameManager.GetComponent<GameManager>().monsterInstance = null;

        if (gameManager.GetComponent<GameManager>().interviewing && monsterInstance != null)
        {
            //monsterInstance.GetComponent<BaseMonster>().setApplicationLife(1);
            gameManager.GetComponent<GameManager>().applicationsList.Remove(monsterInstance);
            gameManager.GetComponent<UIManager>().UpdateApplications();
            Destroy(monsterInstance);
            monsterInstance = null;
        }
    }

}
