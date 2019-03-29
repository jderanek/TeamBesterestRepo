using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clock : MonoBehaviour {

    float time = 0f;
    bool running = false;

    public void Awake()
    {
        //gameObject.SetActive(false); 
        //this set the thing to false while it was already false
    }

    public void Run()
    {
        SoundManager.MusicPlaying = false;
        AkSoundEngine.StopAll();
        AkSoundEngine.PostEvent("Rewind_Music", gameObject);
        running = true;
        gameObject.SetActive(true);
        time = 0f;
    }

    // Update is called once per frame
    void Update () {
        if (!running)
            return;

        float newScale = (-5.6f * Mathf.Pow(time, 2)) + (10f * time);
        time += Time.deltaTime;

        gameObject.transform.localScale = new Vector2(newScale, newScale);

        if (time >= 1.767f)
        {
            running = false;
            AkSoundEngine.PostEvent("Background_Music", gameObject);
            gameObject.SetActive(false);
        }
	}
}
