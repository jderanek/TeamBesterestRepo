using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;

//integer that manages the dialogue voice that is called based on the character
public static class SoundManager
{

    public static int goblinToTalk = 0;

    //List of all goblin objects
    static GameObject[] goblins = GetGoblinObjects();

    public static void SetSoundBank()
    {
        switch (goblinToTalk)
        {
            case 0:
                AkSoundEngine.PostEvent("Goblenn_Voice", goblins[goblinToTalk]); //Goblenn Dialogue voices
                break;
            case 1:
                AkSoundEngine.PostEvent("Geoff_Voice", goblins[goblinToTalk]); //Geoff Dialogue Voice
                break;
            case 2:
                AkSoundEngine.PostEvent("Jeff_Voice", goblins[goblinToTalk]); //Jeff Dialogue Voice
                break;
            case 3:
                AkSoundEngine.PostEvent("Gabbin_Voice", goblins[goblinToTalk]); //Gabbin Dialogue Voice
                break;
            case 4:
                AkSoundEngine.PostEvent("Nilbog_Voice", goblins[goblinToTalk]); // Nilbog Dialogue Voice
                break;
            default:
                Debug.Log("Default case"); //You shouooldnt be heere
                break;
        }
    }

    public static void StopDialogue()
    {
        AkSoundEngine.StopAll(goblins[goblinToTalk]);
    }

    public static GameObject[] GetGoblinObjects()
    {
        GameObject[] output = new GameObject[5];

        output[0] = GameObject.Find("Goblenn");
        output[1] = GameObject.Find("Geoff");
        output[2] = GameObject.Find("Jeff");
        output[3] = GameObject.Find("Gabbin");
        output[4] = GameObject.Find("Nilbog");

        return output;
    }
}

