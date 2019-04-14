using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NotepadAnimation : MonoBehaviour {

    RectTransform rectTrans;
    float startY = -300f;
    float endY = 0f;
    float speed = 10f;
    public GameObject gameManager;

	// Use this for initialization
	void Start () {
        rectTrans = gameObject.GetComponent<RectTransform>();
        rectTrans.anchoredPosition = new Vector2(0, startY);
    }

    //Starts animation
    public void Run()
    {
        if (this.gameManager.GetComponent<GameManager>().interviewing == true)
        {
            ResetToStart();
            InvokeRepeating("Move", 0f, .025f);
        }
    }
	
    // Move notepad up into interview position
	void Move()
    {
        Vector2 newPos = Vector2.MoveTowards(rectTrans.anchoredPosition, new Vector2(0, endY), speed);
        rectTrans.anchoredPosition = newPos;

        if (newPos.y == endY)
            CancelInvoke("Move");
    }

    public void ResetToStart()
    {
        if (rectTrans != null)
            rectTrans.anchoredPosition = new Vector2(0, startY);
    }
}
