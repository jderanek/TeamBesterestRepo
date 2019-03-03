using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Yarn.Unity;

//integer that manages the dialogue voice that is called based on the character
public class SoundManager : MonoBehaviour
{

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

    public bool MusicPlaying = false;

    private float volume;

    private void Awake()
    {
        if (MusicPlaying == false)
        {
            AkSoundEngine.PostEvent("Background_Music", goblins[goblinToTalk]);
            MusicPlaying = true;ss
        }
        GetGoblinObjects();
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
        AkSoundEngine.PostEvent(goblinVoiceName[goblinToTalk], goblins[goblinToTalk]);
    }

    public static void StopDialogue()
    {
        AkSoundEngine.StopAll(goblins[goblinToTalk]);
    }

    public static void GetGoblinObjects()
    {
        goblins = new GameObject[7];

        goblins[0] = GameObject.Find("Goblenn");
        goblins[1] = GameObject.Find("Geoff");
        goblins[2] = GameObject.Find("Jeff");
        goblins[3] = GameObject.Find("Gabbin");
        goblins[4] = GameObject.Find("Nilbog");
        goblins[5] = GameObject.Find("Boss");
        goblins[6] = GameObject.Find("Voiceless");

        goblinVoiceName = new string[7];

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
                GoblennDialogueMarker.SetActive(true);
                GeoffDialogueMarker.SetActive(false);
                JeffDialogueMarker.SetActive(false);
                GabbinDialogueMarker.SetActive(false);
                NilbogDialogueMarker.SetActive(false);
                break;
            case "Geoff":
                goblinToTalk = 1;
                GoblennDialogueMarker.SetActive(false);
                GeoffDialogueMarker.SetActive(true);
                JeffDialogueMarker.SetActive(false);
                GabbinDialogueMarker.SetActive(false);
                NilbogDialogueMarker.SetActive(false);
                break;
            case "Jeff":
                goblinToTalk = 2;
                GoblennDialogueMarker.SetActive(false);
                GeoffDialogueMarker.SetActive(false);
                JeffDialogueMarker.SetActive(true);
                GabbinDialogueMarker.SetActive(false);
                NilbogDialogueMarker.SetActive(false);
                break;
            case "Gabbin":
                goblinToTalk = 3;
                GoblennDialogueMarker.SetActive(false);
                GeoffDialogueMarker.SetActive(false);
                GabbinDialogueMarker.SetActive(true);
                JeffDialogueMarker.SetActive(false);
                NilbogDialogueMarker.SetActive(false);
                break;
            case "Nilbog":
                goblinToTalk = 4;
                GoblennDialogueMarker.SetActive(false);
                GeoffDialogueMarker.SetActive(false);
                NilbogDialogueMarker.SetActive(true);
                JeffDialogueMarker.SetActive(false);
                GabbinDialogueMarker.SetActive(false);
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

