using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Yarn.Unity;

//integer that manages the dialogue voice that is called based on the character
public class SoundManager : MonoBehaviour
{
    public GameManager gameManager;
    public static int goblinToTalk = 0;

    //for speech bubble calling
    public GameObject GoblennDialogueMarker;
    public GameObject GabbinDialogueMarker;
    public GameObject GeoffDialogueMarker;
    public GameObject JeffDialogueMarker;
    public GameObject NilbogDialogueMarker;

    //List of all goblin objects
    static GameObject[] goblins;
    static string[] goblinVoiceName;

    public static bool MusicPlaying = false;

    private float volume;

    private void Awake()
    {
        //set rtpc values to 100 here!
        AkSoundEngine.SetRTPCValue("Master_Volume", 100);
        AkSoundEngine.SetRTPCValue("Voice_Volume", 100);
        AkSoundEngine.SetRTPCValue("Sound_Effects", 100);
        AkSoundEngine.SetRTPCValue("Music_Volume", 100);
        gameManager = FindObjectOfType<GameManager>().GetComponent<GameManager>();
        GetGoblinObjects();
    }

    private void Start()
    {
        if (MusicPlaying == false)
        {
            AkSoundEngine.PostEvent("BackGround_Music", gameObject);
            MusicPlaying = true;
        }
    }

    public void ChangeMasterVolume(GameObject scrollBar)
    {
        print(scrollBar.GetComponent<Scrollbar>().value);
        AkSoundEngine.SetRTPCValue("Master_Volume", scrollBar.GetComponent<Scrollbar>().value * 100); //multiply by 100 because the float passed counts as a percent
        return;
    }

    public void ChangeVoiceVolume(GameObject scrollBar)
    {
        AkSoundEngine.SetRTPCValue("Voice_Volume", scrollBar.GetComponent<Scrollbar>().value * 100); //multiply by 100 because the float passed counts as a percent
        return;
    }

    public void ChangeSFXVolume(GameObject scrollBar)
    {
        AkSoundEngine.SetRTPCValue("Sound_Effects", scrollBar.GetComponent<Scrollbar>().value * 100); //multiply by 100 because the float passed counts as a percent
        return;
    }

    public void ChangeMusicVolume(GameObject scrollBar)
    {
        AkSoundEngine.SetRTPCValue("Music_Volume", scrollBar.GetComponent<Scrollbar>().value * 100); //multiply by 100 because the float passed counts as a percent
        return;
    }

    public static void SetSoundBank()
    {
        InterviewUI.voicePlaying = true;
        AkSoundEngine.PostEvent(goblinVoiceName[goblinToTalk], goblins[goblinToTalk]);
    }

    public static void StopDialogue()
    {
        AkSoundEngine.StopAll(goblins[goblinToTalk]);
    }

    public static void GetGoblinObjects()
    {
        goblins = new GameObject[8];

        goblins[0] = GameObject.Find("Goblenn");
        goblins[1] = GameObject.Find("Geoff");
        goblins[2] = GameObject.Find("Jeff");
        goblins[3] = GameObject.Find("Gabbin");
        goblins[4] = GameObject.Find("Nilbog");
        goblins[5] = GameObject.Find("Boss");
        goblins[6] = GameObject.Find("Voiceless");
        goblins[7] = GameObject.Find("Gordon");

        goblinVoiceName = new string[8];

        goblinVoiceName[0] = "Goblenn_Voice";
        goblinVoiceName[1] = "Geoff_Voice";
        goblinVoiceName[2] = "JefF_Voice";
        goblinVoiceName[3] = "Gabbin_Voice";
        goblinVoiceName[4] = "Nilbog_Voice";
        goblinVoiceName[5] = "Boss_Voice";
        goblinVoiceName[6] = "Voiceless";
        goblinVoiceName[7] = "Gordon_Voice";
    }

    [YarnCommand("ChangeSpeaker")]
    public void ChangeSpeaker(string newSpeaker)
    {
        switch (newSpeaker)
        {
            case "Goblenn":
                goblinToTalk = 0;
                gameManager.SetCurrentSpeaker(1);
                break;
            case "Geoff":
                goblinToTalk = 1;
                gameManager.SetCurrentSpeaker(4);
                break;
            case "Jeff":
                goblinToTalk = 2;
                gameManager.SetCurrentSpeaker(5);
                break;
            case "Gabbin":
                goblinToTalk = 3;
                gameManager.SetCurrentSpeaker(2);
                break;
            case "Nilbog":
                goblinToTalk = 4;
                gameManager.SetCurrentSpeaker(3);
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

