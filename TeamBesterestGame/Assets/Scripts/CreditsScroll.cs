using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreditsScroll : MonoBehaviour {

    float speed = 1f;
    RectTransform rectTrans;
    float endX = 0f;
    float endY = -1000f;

    void Start()
    {
        rectTrans = gameObject.GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void Update () {
        Vector2 newPos = Vector2.MoveTowards(rectTrans.anchoredPosition, new Vector2(endX, endY), speed);
        rectTrans.anchoredPosition = newPos;
    }
}
