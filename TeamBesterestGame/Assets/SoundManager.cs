using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;

//integer that manages the dialogue voice that is called based on the character
public class SoundManager : MonoBehaviour
{

    public static int goblinToTalk = 0;

    //List of all goblin objects
    static GameObject[] goblins;
    static string[] goblinVoiceName;

    private void Awake()
    {
        GetGoblinObjects();
    }

    public static void SetSoundBank()
    {
        AkSoundEngine.PostEvent(goblinVoiceName[goblinToTalk], goblins[goblinToTalk]);
    }

    public static void StopDialogue()
    {
        AkSoundEngine.StopAll(goblins[goblinToTalk]);
    }

    public static void GetGoblinObjects()
    {
        goblins = new GameObject[5];

        goblins[0] = GameObject.Find("Goblenn");
        goblins[1] = GameObject.Find("Geoff");
        goblins[2] = GameObject.Find("Jeff");
        goblins[3] = GameObject.Find("Gabbin");
        goblins[4] = GameObject.Find("Nilbog");
        goblins[5] = GameObject.Find("Boss");
        goblins[6] = GameObject.Find("Voiceless");

        goblinVoiceName = new string[5];

        goblinVoiceName[0] = "Goblenn_Voice";
        goblinVoiceName[1] = "Geoff_Voice";
        goblinVoiceName[2] = "JefF_Voice";
        goblinVoiceName[3] = "Gabbin_Voice";
        goblinVoiceName[4] = "Nilbog_Voice";
        goblinVoiceName[5] = "Boss_Voice";
        goblinVoiceName[6] = "Voiceless";
    }

    [YarnCommand("ChangeSpeaker")]
    public void ChangeSpeaker(string newSpeaker)
    {
        switch (newSpeaker)
        {
            case "Goblenn":
                goblinToTalk = 0;
                break;
            case "Geoff":
                goblinToTalk = 1;
                break;
            case "Jeff":
                goblinToTalk = 2;
                break;
            case "Gabbin":
                goblinToTalk = 3;
                break;
            case "Nilbog":
                goblinToTalk = 4;
                break;
            case "Boss":
                goblinToTalk = 5;
                break;
            case "Voiceless":
                goblinToTalk = 6;
                break;
            default:
                break;
        }

        SetSoundBank();
    }
}

