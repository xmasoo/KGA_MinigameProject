using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePlatform : PlatformBase
{
    public float speed = 2f;
    public float moveRange = 2f;

    private Vector3 startPos;
    private int direction = 1;

    void OnEnable()
    {
        startPos = transform.position;
    }

    void Update()
    {
        transform.Translate(Vector3.right * speed * direction * Time.deltaTime);

        if (Mathf.Abs(transform.position.x - startPos.x) > moveRange)
        {
            direction *= -1;
        }
    }
}
