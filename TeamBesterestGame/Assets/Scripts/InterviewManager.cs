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

    public GameObject revealedTraitText;

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
        selfcenteredQuestions, speechQuestions, conditionQuestions, appearanceQuestions, negativeQuestions };
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

    public int q11;
    int q12;
    public int q21;
    int q22;
    public int q31;
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
   
    public void StatQuestion()
    {
        switch (monsterInstance.GetComponent<BaseMonster>().cNum)
        {
            case 1: //gabbin
                responseText.text = "Oh, I like to stay hidden. I don't fight that much myself. I prefer to cheer on my coworkers from the sidelines.";
                break;
            case 2: //goblenn
                responseText.text = "I used to run for the Goblin Tech track team, so I can be pretty fast! It's kept me out of trouble when things get dire around here, let me tell you! And that's been a bit of a problem of late! Heard management picked up someone to help deal with that, though. What a sucker.";
                break;
            case 3: //nilbog
                responseText.text = "People might look at me and see weakness. They will change their mind once the feel my fist on their face...";
                break;
            case 4: //geoff
                responseText.text = "I kill things, and kill them hard. I tend to also be quite popular with the end of a hero's sword.";
                break;
            case 5: //jeff
                responseText.text = "Just point me at the enemy, and watch me work. You can't go wrong with me";
                break;
        }
    }
    public void PersonalityQuestion()
    {
        switch (monsterInstance.GetComponent<BaseMonster>().cNum)
        {
            case 1: //gabbin
                responseText.text = "I just love to talk to everyone! Also my favorite pasttime is cheering everyone else up!";
                break;
            case 2: //goblenn
                responseText.text = "I tend to stay pretty low-profile around here. If someone wants to take the spotlight when humans show up, they can be my guest! As long as I'm not the center of attention... And there are clearly marked escape routes...";
                break;
            case 3: //nilbog
                responseText.text = "I dont need the help of anyone here, they will just get in my way...";
                break;
            case 4: //geoff
                responseText.text = "Fighting is what I do best. I tend to get a little heated at times so don't expect me to get along with everyone";
                break;
            case 5: //jeff
                responseText.text = "I have been training my whole life. I live for battle! But I can sometimes get overzealous and bring some of the bloodlust off the battlefield. ";
                break;
        }
    }
    public void CoworkerQuestion1()
        {
            switch (monsterInstance.GetComponent<BaseMonster>().cNum)
            {
                case 1: //gabbin
                    responseText.text = "Eh, Geoff seems kinda violent. That doesn't really match my happy go lucky attitude to be honest. We don't talk all that much.";
                    break;
                case 2: //goblenn
                    responseText.text = "I've heard Geoff is one of the harder hitters around here.  Loves to fight, and I mean lives for it! I have heard they've got some beef with Jeff though. I definitely don't want to be around if that goes bad!  Judging by the looks they give each other during breaks...";
                    break;
                case 3: //nilbog
                    responseText.text = "Geoff's not my type. Too often do I want to punch them over punching a hero";
                    break;
                case 4: //geoff
                    responseText.text = "Gabbin? Seems like a decent guy. He will annoyingly talk your ear off though sometimes.";
                    break;
                case 5: //jeff
                    responseText.text = "For the most part, Gabbin seems like a goody-two shoes. As long as he stays out of my way, we won't have any issues.";
                    break;
            }
    }
    public void CoworkerQuestion2()
    {
        switch (monsterInstance.GetComponent<BaseMonster>().cNum)
        {
            case 1: //gabbin
                responseText.text = "Seems like Jeff can take a hit, but easy to anger. They don't when I try and start a conversation. They also get angry when Geoff talks to them.";
                break;
            case 2: //goblenn
                responseText.text = "Jeff scares me. Someone who loves fighting that much has to have some sort of anger problem. I know I wouldn't want to be the one on the receiving end of that! They're probably handy to have around when the humans show up, though...";
                break;
            case 3: //nilbog
                responseText.text = "Jeff is all out showboating. I cant work properly when someone is just flailing around.";
                break;
            case 4: //geoff
                responseText.text = "Goblenn needs to goblin up and stop whining about everything. He always needs someone to bail him out of a jam.";
                break;
            case 5: //jeff
                responseText.text = "I think Goblenn is in dire need of a wake up call. He can't just run away all the time when things get rough.";
                break;
        }
    }
    public void CoworkerQuestion3()
    {
        switch (monsterInstance.GetComponent<BaseMonster>().cNum)
        {
            case 1: //gabbin
                responseText.text = "Seems like Goblenn doesn't like to be alone. They also run away from a fight if they ARE alone. I would love to talk to[Coward] but I don't see them around all that often.";
                break;
            case 2: //goblenn
                responseText.text = "Oh, Gabbin? They're a treat to work with, but I don't know how good they are in a fight themself. They try to amp me up for work, but I usually prefer to place myself in the back of a fight anyway...";
                break;
            case 3: //nilbog
                responseText.text = "Why do we need Goblenn? If they want to run away from a fight, let them... I wont need 'em.";
                break;
            case 4: //geoff
                responseText.text = "That Nilbog dude is kinda weird. Everytime I try talking to him, he just clams up. Better to just live and let live I guess.";
                break;
            case 5: //jeff
                responseText.text = "I always see Nilbog skulking around and never talking during meetings. Doesn't seem the type to have many friends";
                break;
        }
    }
    public void CoworkerQuestion4()
    {
        switch (monsterInstance.GetComponent<BaseMonster>().cNum)
        {
            case 1: //gabbin
                responseText.text = "Nilbog kinda creeps me out. They never want to talk. It honestly brings down my whole mood.";
                break;
            case 2: //goblenn
                responseText.text = "Nilbog tends to give most everyone the cold shoulder. The idea of fighting without any friends is inconceivable, but they seem to prefer it that way! What a weirdo!";
                break;
            case 3: //nilbog
                responseText.text = "Gabbin seems like a nice guy. But seems like they're not cut out for this line of work. I'll be all you need.";
                break;
            case 4: //geoff
                responseText.text = "Jeff is the worst man. 1 He has the same name as me. 2. He spells it wrong 3. I'm pretty sure he stole my sweetroll at lunch!";
                break;
            case 5: //jeff
                responseText.text = "You had to bring up that guy huh? Geoff is a hotheaded idiot who apparently can't even spell his own name! If you talk to him after me, tell him the sweetroll was delicious.";
                break;
        }
    }

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

    }
    
    public void ExitInterview() //exits interview and hides the interview UI
    {
        gameManager.GetComponent<GameManager>().interviewCanvas.SetActive(false);
        gameManager.GetComponent<GameManager>().interviewMenu.SetActive(true);
        SoundManager.StopDialogue();

        interviewResponse.SetActive(false);
        responseText.text = "Hello";
        gameManager.GetComponent<GameManager>().interviewing = false;
    }
}
