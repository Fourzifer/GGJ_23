using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OneWayPlatform : MonoBehaviour
{
    private PlatformEffector2D effector;
    public float waitTime;
    private float timer;

    // Start is called before the first frame update
    void Start()
    {
        effector = GetComponent<PlatformEffector2D>();
        timer = waitTime;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetButtonUp("Jump"))
        {
            timer = waitTime;
        }

        if (Input.GetAxisRaw("Vertical") < 0)
        {
            effector.rotationalOffset = 180f;

            timer = waitTime;
        }

        timer -= Time.deltaTime;
        if (timer <= 0)
        {
            effector.rotationalOffset = 0;
            timer = 0;
        }

        if (Input.GetButtonDown("Jump"))
        {
            effector.rotationalOffset = 0;
        }
    }
}