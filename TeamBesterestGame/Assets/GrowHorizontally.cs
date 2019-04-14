using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GrowHorizontally : MonoBehaviour {

    public float speed;
    public float size;
    private float t;

    public bool textMode;

    public string[] words;
    private int lastWord;

    // Use this for initialization
    void Start ()
    {

	}
	
	// Update is called once per frame
	void Update ()
    {


        //float t = (float)((Mathf.Sin(Time.time * speed) + 1) / 2.0);
        t = t + Time.deltaTime * speed;
        if (t > 1.0f)
        {
            t = 0f;
            if (textMode)
            {
                int r = Random.Range(0, words.Length);

                transform.parent.GetComponent<Text>().text = words[r];
            }
        }

        GetComponent<RectTransform>().sizeDelta = new Vector2(size * t, GetComponent<RectTransform>().sizeDelta.y);
    }
}
