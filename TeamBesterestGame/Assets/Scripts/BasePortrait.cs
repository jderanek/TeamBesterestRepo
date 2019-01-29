using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;

public class BasePortrait : MonoBehaviour
{

    public bool interviewable = true;
    SpriteRenderer sprite;
    // Use this for initialization
    void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    [YarnCommand("Move")]
    public void YarnMove(string distance, string duration)
    {
        StartCoroutine(Move(float.Parse(distance), float.Parse(duration) * 20));
    }

    [YarnCommand("Shake")]
    public void YarnShake(string tilt, string speed, string duration)
    {
        StartCoroutine(Shake(float.Parse(tilt), float.Parse(speed) * 10, float.Parse(duration)));
    }

    [YarnCommand("Bob")]
    public void YarnBob(string duration, string speed, string magnitude)
    {
        StartCoroutine(Bob(float.Parse(duration), float.Parse(speed) * 10, float.Parse(magnitude)));
    }

    [YarnCommand("ChangeOppacity")]
    public void YarnOppacity(string alpha, string speed)
    {
        StartCoroutine(ChangeOppacity(float.Parse(alpha), float.Parse(speed) * 10));
    }

    public IEnumerator Move(float distance, float duration)
    {
        float startTime = Time.time;
        print("boop");
        Vector3 targetPosition = new Vector3(this.transform.position.x - distance, 0f, 0f);
        while (Time.time < startTime + duration) //Vector3.Distance(transform.position, targetPosition) > 0.05f)
        {
            transform.position = Vector3.Lerp(transform.position, targetPosition, ((Time.time - startTime) / duration));

            yield return null;
        }

    }

    IEnumerator Bob(float duration, float speed, float magnitude)
    {
        while (duration > 0.05f)
        {
            Vector3 pos = new Vector3(0f, Mathf.Sin(Time.time * speed) * magnitude, 0f);
            transform.position = Vector3.Lerp(transform.position, pos, speed * Time.deltaTime);
            duration -= Time.deltaTime;

            yield return null;
        }
    }

    IEnumerator Shake(float tilt, float speed, float duration)
    {
        while(duration > 0.05f)
        {
            Quaternion rot = Quaternion.AngleAxis(tilt, Vector3.forward);
            if (this.transform.rotation == rot)
            {
                tilt *= -1;
            }
            transform.rotation = Quaternion.RotateTowards(transform.rotation, rot, speed * Time.deltaTime);

            duration -= Time.deltaTime;

            yield return null;
        }
    }

    IEnumerator ChangeOppacity(float alpha, float speed)
    {
        while(Mathf.Abs(sprite.color.a - alpha) > 0.05f)
        {
            sprite.color = new Color (sprite.color.r, sprite.color.g, sprite.color.b, Mathf.Lerp(sprite.color.a, alpha, speed));

            yield return null;
        }
    }
}
