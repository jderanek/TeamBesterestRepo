using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasePortrait : MonoBehaviour
{
    SpriteRenderer sprite;
    // Use this for initialization
    void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
        StartCoroutine(Shake(20f, 50f, 5f));
    }

    // Update is called once per frame
    void Update()
    {

    }

    IEnumerator Move(float distance, float speed)
    {
        Vector3 targetPosition = new Vector3(0f, this.transform.position.x - distance, 0f);
        while (Vector3.Distance(transform.position, targetPosition) > 0.05f)
        {
            transform.position = Vector3.Lerp(transform.position, targetPosition, speed * Time.deltaTime);

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
