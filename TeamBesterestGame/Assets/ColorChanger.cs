﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ColorChanger : MonoBehaviour
{
    public float speed;

    public Color Color1;
    public Color Color2;

    void Start()
    {
        
    }

    private void Update()
    {
        Color glennGreen = new Color(0f, 0.6039f, 0.0431f, 1f);
        Color gabbinGreen = new Color(0.3f, 0.4f, 0.6f, 1f);

        float t = (float)((Mathf.Sin(Time.time * speed) + 1) / 2.0);
        Color c = Color.Lerp(Color1, Color2, t);
        
        GetComponent<Image>().color = c;
    }
}