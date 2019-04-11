using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaperAnimation : MonoBehaviour
{

    RectTransform rectTrans;
    float startY = -120;
    float startX = 0f;
    float endY = 200f;
    float endX = -365f;
    float speed = 10f;
    float shrinkSpeed = .015f;

    // Use this for initialization
    void Start()
    {
        rectTrans = gameObject.GetComponent<RectTransform>();
        rectTrans.anchoredPosition = new Vector2(startX, startY);
        this.gameObject.SetActive(false);
    }

    //Starts animation
    public void Run()
    {
        InvokeRepeating("Move", 0f, .025f);
    }

    // Move notepad up into interview position
    void Move()
    {
        Vector2 newPos = Vector2.MoveTowards(rectTrans.anchoredPosition, new Vector2(endX, endY), speed);
        rectTrans.anchoredPosition = newPos;

        Vector3 newScale = new Vector3(this.transform.localScale.x - shrinkSpeed, this.transform.localScale.y - shrinkSpeed, 1);
        this.transform.localScale = newScale;

        if (newPos.x == endX && newPos.y == endY)
        {
            CancelInvoke("Move");
            ResetToStart();
        }
    }

    public void ResetToStart()
    {
        if (rectTrans != null)
            rectTrans.anchoredPosition = new Vector2(startX, startY);

        this.transform.localScale = new Vector3(1, 1, 1);
        this.gameObject.SetActive(false);
    }
}
