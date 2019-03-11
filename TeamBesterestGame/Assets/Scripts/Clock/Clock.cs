using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clock : MonoBehaviour {

    float time = 0f;
    bool running = false;

    public void Awake()
    {
        gameObject.SetActive(false);
    }

    public void Run()
    {
        running = true;
        gameObject.SetActive(true);
    }

	// Update is called once per frame
	void Update () {
        if (!running)
            return;

        float newScale = (-4f * Mathf.Pow(time, 2)) + (7.7f * time);
        time += Time.deltaTime;

        gameObject.transform.localScale = new Vector2(newScale, newScale);

        if (time >= 2f)
        {
            running = false;
            gameObject.SetActive(false);
        }
	}
}
