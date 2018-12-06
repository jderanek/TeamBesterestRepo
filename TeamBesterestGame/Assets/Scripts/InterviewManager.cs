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

    /*public void Question1()
    {
        GetResponse(q11);
        monsterInstance.GetComponent<BaseMonster>().RevealTraits();
        gameManager.GetComponent<GameManager>().PassTime(1);
        UpdateTraitText();
        UpdateQuestions();
    }

    public void Question2()
    {
        GetResponse(q21);
        monsterInstance.GetComponent<BaseMonster>().RevealTraits();
        gameManager.GetComponent<GameManager>().PassTime(1);
        UpdateTraitText();
        UpdateQuestions();
    }

    public void Question3()
    {
        GetResponse(q31);
        monsterInstance.GetComponent<BaseMonster>().RevealTraits();
        gameManager.GetComponent<GameManager>().PassTime(1);
        UpdateTraitText();
        UpdateQuestions();
    }*/

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

    public void GetResponse(int q)
    {
        switch (q)
        {
            case 0://hierarchy group
                if (monsterInstance.GetComponent<BaseMonster>().tags.Contains(PersonalityTags.Tag.FOLLOWER) && !monsterInstance.GetComponent<BaseMonster>().revealedTags.Contains(PersonalityTags.Tag.FOLLOWER))
                {
                    monsterInstance.GetComponent<BaseMonster>().revealedTags.Add(PersonalityTags.Tag.FOLLOWER);
                    responseText.text = "Others can take the reins, too much responsibility for me";
                }
                else if (monsterInstance.GetComponent<BaseMonster>().tags.Contains(PersonalityTags.Tag.COWARDLY) && !monsterInstance.GetComponent<BaseMonster>().revealedTags.Contains(PersonalityTags.Tag.COWARDLY))
                {
                    monsterInstance.GetComponent<BaseMonster>().revealedTags.Add(PersonalityTags.Tag.COWARDLY);
                    responseText.text = "They say discretion is the better part of valor ok?";
                }
                else
                {
                    responseText.text = "All that leader stuff isn’t for me";
                }
                break;
            case 1://aid group
                if (monsterInstance.GetComponent<BaseMonster>().tags.Contains(PersonalityTags.Tag.HELPFUL) && !monsterInstance.GetComponent<BaseMonster>().revealedTags.Contains(PersonalityTags.Tag.HELPFUL))
                {
                    monsterInstance.GetComponent<BaseMonster>().revealedTags.Add(PersonalityTags.Tag.HELPFUL);
                    responseText.text = "I am always willing to lend a helping hand!";
                }
                else if (monsterInstance.GetComponent<BaseMonster>().tags.Contains(PersonalityTags.Tag.FRIENDLY) && !monsterInstance.GetComponent<BaseMonster>().revealedTags.Contains(PersonalityTags.Tag.FRIENDLY))
                {
                    monsterInstance.GetComponent<BaseMonster>().revealedTags.Add(PersonalityTags.Tag.FRIENDLY);
                    responseText.text = "Yes, it is not hard for me to get along with new people";
                }
                else if (monsterInstance.GetComponent<BaseMonster>().tags.Contains(PersonalityTags.Tag.POSITIVE) && !monsterInstance.GetComponent<BaseMonster>().revealedTags.Contains(PersonalityTags.Tag.POSITIVE))
                {
                    monsterInstance.GetComponent<BaseMonster>().revealedTags.Add(PersonalityTags.Tag.POSITIVE);
                    responseText.text = "I always look on the bright side of things!";
                }
                else
                {
                    responseText.text = "I have to watch my own back, don’t have time to worry about others";
                }
                break;
            case 2://rude group
                if (monsterInstance.GetComponent<BaseMonster>().tags.Contains(PersonalityTags.Tag.MEAN) && !monsterInstance.GetComponent<BaseMonster>().revealedTags.Contains(PersonalityTags.Tag.MEAN))
                {
                    monsterInstance.GetComponent<BaseMonster>().revealedTags.Add(PersonalityTags.Tag.MEAN);
                    responseText.text = "I just speak my mind, what is wrong with that?!";
                }
                else if (monsterInstance.GetComponent<BaseMonster>().tags.Contains(PersonalityTags.Tag.ANTISOCIAL) && !monsterInstance.GetComponent<BaseMonster>().revealedTags.Contains(PersonalityTags.Tag.ANTISOCIAL))
                {
                    monsterInstance.GetComponent<BaseMonster>().revealedTags.Add(PersonalityTags.Tag.ANTISOCIAL);
                    responseText.text = "I just prefer to keep people at a distance, I have my reasons.";
                }
                else if (monsterInstance.GetComponent<BaseMonster>().tags.Contains(PersonalityTags.Tag.THREATENING) && !monsterInstance.GetComponent<BaseMonster>().revealedTags.Contains(PersonalityTags.Tag.THREATENING))
                {
                    monsterInstance.GetComponent<BaseMonster>().revealedTags.Add(PersonalityTags.Tag.THREATENING);
                    responseText.text = "I don’t even need to fight heroes, they cower in fear!";
                }
                else if (monsterInstance.GetComponent<BaseMonster>().tags.Contains(PersonalityTags.Tag.NEGATIVE) && !monsterInstance.GetComponent<BaseMonster>().revealedTags.Contains(PersonalityTags.Tag.NEGATIVE))
                {
                    monsterInstance.GetComponent<BaseMonster>().revealedTags.Add(PersonalityTags.Tag.NEGATIVE);
                    responseText.text = "I am just being real with my chances, ok?";
                }
                else
                {
                    responseText.text = "I don’t think I give off that impression, at least I hope not…";
                }
                break;
            case 3://work-ethic group
                if (monsterInstance.GetComponent<BaseMonster>().tags.Contains(PersonalityTags.Tag.DILIGENT) && !monsterInstance.GetComponent<BaseMonster>().revealedTags.Contains(PersonalityTags.Tag.DILIGENT))
                {
                    monsterInstance.GetComponent<BaseMonster>().revealedTags.Add(PersonalityTags.Tag.DILIGENT);
                    responseText.text = "Yes, that is not a problem for me";
                }
                else if (monsterInstance.GetComponent<BaseMonster>().tags.Contains(PersonalityTags.Tag.HARDWORKING) && !monsterInstance.GetComponent<BaseMonster>().revealedTags.Contains(PersonalityTags.Tag.HARDWORKING))
                {
                    monsterInstance.GetComponent<BaseMonster>().revealedTags.Add(PersonalityTags.Tag.HARDWORKING);
                    responseText.text = "I love to work and do my best!";
                }
                else if (monsterInstance.GetComponent<BaseMonster>().tags.Contains(PersonalityTags.Tag.MOTIVATED) && !monsterInstance.GetComponent<BaseMonster>().revealedTags.Contains(PersonalityTags.Tag.MOTIVATED))
                {
                    monsterInstance.GetComponent<BaseMonster>().revealedTags.Add(PersonalityTags.Tag.MOTIVATED);
                    responseText.text = "I stay focused on the mission, at all times.";
                }
                else
                {
                    responseText.text = "I am just here for the paycheck, so don’t expect any miracles";
                }
                break;
            case 4://physical group
                if (monsterInstance.GetComponent<BaseMonster>().tags.Contains(PersonalityTags.Tag.AGGRESSIVE) && !monsterInstance.GetComponent<BaseMonster>().revealedTags.Contains(PersonalityTags.Tag.AGGRESSIVE))
                {
                    monsterInstance.GetComponent<BaseMonster>().revealedTags.Add(PersonalityTags.Tag.AGGRESSIVE);
                    responseText.text = "I take what I see, no time for hesitation";
                }
                else if (monsterInstance.GetComponent<BaseMonster>().tags.Contains(PersonalityTags.Tag.STRONG) && !monsterInstance.GetComponent<BaseMonster>().revealedTags.Contains(PersonalityTags.Tag.STRONG))
                {
                    monsterInstance.GetComponent<BaseMonster>().revealedTags.Add(PersonalityTags.Tag.STRONG);
                    responseText.text = "I can knock out heroes wearing even the strongest armor!";
                }
                else if (monsterInstance.GetComponent<BaseMonster>().tags.Contains(PersonalityTags.Tag.VIOLENT) && !monsterInstance.GetComponent<BaseMonster>().revealedTags.Contains(PersonalityTags.Tag.VIOLENT))
                {
                    monsterInstance.GetComponent<BaseMonster>().revealedTags.Add(PersonalityTags.Tag.VIOLENT);
                    responseText.text = "I prefer to break people, especially puny heroes!";
                }
                else if (monsterInstance.GetComponent<BaseMonster>().tags.Contains(PersonalityTags.Tag.ENDURING) && !monsterInstance.GetComponent<BaseMonster>().revealedTags.Contains(PersonalityTags.Tag.ENDURING))
                {
                    monsterInstance.GetComponent<BaseMonster>().revealedTags.Add(PersonalityTags.Tag.ENDURING);
                    responseText.text = "You can always count on me in a tough brawl!";
                }
                else
                {
                    responseText.text = "Getting down and dirty isn’t really my forte.";
                }
                break;
            case 5://self-centered group
                if (monsterInstance.GetComponent<BaseMonster>().tags.Contains(PersonalityTags.Tag.PROUD) && !monsterInstance.GetComponent<BaseMonster>().revealedTags.Contains(PersonalityTags.Tag.PROUD))
                {
                    monsterInstance.GetComponent<BaseMonster>().revealedTags.Add(PersonalityTags.Tag.PROUD);
                    responseText.text = "I know what I am and what I can do, what is wrong with being proud of that?";
                }
                else if (monsterInstance.GetComponent<BaseMonster>().tags.Contains(PersonalityTags.Tag.EGOCENTRIC) && !monsterInstance.GetComponent<BaseMonster>().revealedTags.Contains(PersonalityTags.Tag.EGOCENTRIC))
                {
                    monsterInstance.GetComponent<BaseMonster>().revealedTags.Add(PersonalityTags.Tag.EGOCENTRIC);
                    responseText.text = "I mean who wouldn’t? Have you seen my looks?";
                }
                else if (monsterInstance.GetComponent<BaseMonster>().tags.Contains(PersonalityTags.Tag.INDEPENDENT) && !monsterInstance.GetComponent<BaseMonster>().revealedTags.Contains(PersonalityTags.Tag.INDEPENDENT))
                {
                    monsterInstance.GetComponent<BaseMonster>().revealedTags.Add(PersonalityTags.Tag.INDEPENDENT);
                    responseText.text = "I prefer to go solo if possible.";
                }
                else
                {
                    responseText.text = "I don’t really see myself as someone special";
                }
                break;
            case 6://speech group
                if (monsterInstance.GetComponent<BaseMonster>().tags.Contains(PersonalityTags.Tag.SOCIAL) && !monsterInstance.GetComponent<BaseMonster>().revealedTags.Contains(PersonalityTags.Tag.SOCIAL))
                {
                    monsterInstance.GetComponent<BaseMonster>().revealedTags.Add(PersonalityTags.Tag.SOCIAL);
                    responseText.text = "Conversation is one of my strong suits, not sure how it helps fighting heroes though.";
                }
                else if (monsterInstance.GetComponent<BaseMonster>().tags.Contains(PersonalityTags.Tag.TALKATIVE) && !monsterInstance.GetComponent<BaseMonster>().revealedTags.Contains(PersonalityTags.Tag.TALKATIVE))
                {
                    monsterInstance.GetComponent<BaseMonster>().revealedTags.Add(PersonalityTags.Tag.TALKATIVE);
                    responseText.text = "I just love gossip, I want to know every juicy detail!";
                }
                else
                {
                    responseText.text = "I tend to focus on my work rather than chatting";
                }
                break;
            case 7://condition group
                if (monsterInstance.GetComponent<BaseMonster>().tags.Contains(PersonalityTags.Tag.SICKLY) && !monsterInstance.GetComponent<BaseMonster>().revealedTags.Contains(PersonalityTags.Tag.SICKLY))
                {
                    monsterInstance.GetComponent<BaseMonster>().revealedTags.Add(PersonalityTags.Tag.SICKLY);
                    responseText.text = "Days? How about weeks?";
                }
                else if (monsterInstance.GetComponent<BaseMonster>().tags.Contains(PersonalityTags.Tag.WEAK) && !monsterInstance.GetComponent<BaseMonster>().revealedTags.Contains(PersonalityTags.Tag.WEAK))
                {
                    monsterInstance.GetComponent<BaseMonster>().revealedTags.Add(PersonalityTags.Tag.WEAK);
                    responseText.text = "Y-yeah… maybe I have applied for the wrong position...";
                }
                else if (monsterInstance.GetComponent<BaseMonster>().tags.Contains(PersonalityTags.Tag.BURLY) && !monsterInstance.GetComponent<BaseMonster>().revealedTags.Contains(PersonalityTags.Tag.BURLY))
                {
                    monsterInstance.GetComponent<BaseMonster>().revealedTags.Add(PersonalityTags.Tag.BURLY);
                    responseText.text = "My endurance is much higher than average.";
                }
                else
                {
                    responseText.text = "I feel fine for the most part";
                }
                break;
            case 8://appearance group
                if (monsterInstance.GetComponent<BaseMonster>().tags.Contains(PersonalityTags.Tag.SPOOKY) && !monsterInstance.GetComponent<BaseMonster>().revealedTags.Contains(PersonalityTags.Tag.SPOOKY))
                {
                    monsterInstance.GetComponent<BaseMonster>().revealedTags.Add(PersonalityTags.Tag.SPOOKY);
                    responseText.text = "Yes, most heroes shake in their boots when face to face with me!";
                }
                else if (monsterInstance.GetComponent<BaseMonster>().tags.Contains(PersonalityTags.Tag.GROSS) && !monsterInstance.GetComponent<BaseMonster>().revealedTags.Contains(PersonalityTags.Tag.GROSS))
                {
                    monsterInstance.GetComponent<BaseMonster>().revealedTags.Add(PersonalityTags.Tag.GROSS);
                    responseText.text = "Listen, I call it my natural musk.";
                }
                else if (monsterInstance.GetComponent<BaseMonster>().tags.Contains(PersonalityTags.Tag.OLDFASHIONED) && !monsterInstance.GetComponent<BaseMonster>().revealedTags.Contains(PersonalityTags.Tag.OLDFASHIONED))
                {
                    monsterInstance.GetComponent<BaseMonster>().revealedTags.Add(PersonalityTags.Tag.OLDFASHIONED);
                    responseText.text = "I guess that wrinkle cream isn’t working at all…";
                }
                else
                {
                    responseText.text = "I think most would say I don’t really stand out much";
                }
                break;
            case 9://negative group
                if (monsterInstance.GetComponent<BaseMonster>().tags.Contains(PersonalityTags.Tag.LAZY) && !monsterInstance.GetComponent<BaseMonster>().revealedTags.Contains(PersonalityTags.Tag.LAZY))
                {
                    monsterInstance.GetComponent<BaseMonster>().revealedTags.Add(PersonalityTags.Tag.LAZY);
                    responseText.text = "Listen, I call it my beauty sleep.";
                }
                else if (monsterInstance.GetComponent<BaseMonster>().tags.Contains(PersonalityTags.Tag.ANGRY) && !monsterInstance.GetComponent<BaseMonster>().revealedTags.Contains(PersonalityTags.Tag.ANGRY))
                {
                    monsterInstance.GetComponent<BaseMonster>().revealedTags.Add(PersonalityTags.Tag.ANGRY);
                    responseText.text = "What?! Who said that?!";
                }
                else if (monsterInstance.GetComponent<BaseMonster>().tags.Contains(PersonalityTags.Tag.GREEDY) && !monsterInstance.GetComponent<BaseMonster>().revealedTags.Contains(PersonalityTags.Tag.GREEDY))
                {
                    monsterInstance.GetComponent<BaseMonster>().revealedTags.Add(PersonalityTags.Tag.GREEDY);
                    responseText.text = "I prefer the term FRUGAL thank you.";
                }
                else if (monsterInstance.GetComponent<BaseMonster>().tags.Contains(PersonalityTags.Tag.WILD) && !monsterInstance.GetComponent<BaseMonster>().revealedTags.Contains(PersonalityTags.Tag.WILD))
                {
                    monsterInstance.GetComponent<BaseMonster>().revealedTags.Add(PersonalityTags.Tag.WILD);
                    responseText.text = "I am just GO GO GO ALL THE TIME!";
                }
                else
                {
                    responseText.text = "Don’t worry about me. I don’t like instigating any brawls.";
                }
                break;
        }
        Debug.Log("Tags for: " + monsterInstance.name);
        foreach (PersonalityTags.Tag tag in monsterInstance.GetComponent<BaseMonster>().revealedTags)
            Debug.Log(tag.ToString());
        Debug.Log("Revealed Traits: ");
        foreach (string trait in monsterInstance.GetComponent<BaseMonster>().revealedTraits)
            Debug.Log(trait);
    }

    public void UpdateTraitText()
    {
        if (monsterInstance.GetComponent<BaseMonster>().revealedTraits.Count == 3 && monsterInstance != null)
        {
            revealedTraitText.transform.GetChild(0).GetComponent<Text>().text = monsterInstance.GetComponent<BaseMonster>().revealedTraits[0];
            revealedTraitText.transform.GetChild(1).GetComponent<Text>().text = monsterInstance.GetComponent<BaseMonster>().revealedTraits[1];
            revealedTraitText.transform.GetChild(2).GetComponent<Text>().text = monsterInstance.GetComponent<BaseMonster>().revealedTraits[2];
        }
        else if (monsterInstance.GetComponent<BaseMonster>().revealedTraits.Count == 2 && monsterInstance != null)
        {
            revealedTraitText.transform.GetChild(0).GetComponent<Text>().text = monsterInstance.GetComponent<BaseMonster>().revealedTraits[0];
            revealedTraitText.transform.GetChild(1).GetComponent<Text>().text = monsterInstance.GetComponent<BaseMonster>().revealedTraits[1];
        }
        else if (monsterInstance.GetComponent<BaseMonster>().revealedTraits.Count == 1 && monsterInstance != null)
        {
            revealedTraitText.transform.GetChild(0).GetComponent<Text>().text = monsterInstance.GetComponent<BaseMonster>().revealedTraits[0];
        }
        else
        {
            revealedTraitText.transform.GetChild(0).GetComponent<Text>().text = "???";
            revealedTraitText.transform.GetChild(1).GetComponent<Text>().text = "???";
            revealedTraitText.transform.GetChild(2).GetComponent<Text>().text = "???";
        }
    }

    public void ExitInterview() //exits interview and hides the interview UI
    {
        gameManager.GetComponent<GameManager>().monsterInstance = null;
        gameManager.GetComponent<GameManager>().interviewCanvas.SetActive(false);
        gameManager.GetComponent<GameManager>().interviewMenu.SetActive(true);
        SoundManager.StopDialogue();

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
            //Destroy(monsterInstance);
            gameManager.GetComponent<GameManager>().interviewMenu.SetActive(true);
            monsterInstance = null;
            gameManager.GetComponent<GameManager>().interviewing = false;
            //UpdateTraitText();
        }
    }

}
