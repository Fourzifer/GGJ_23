using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OneWayPlatform : MonoBehaviour
{
    private PlatformEffector2D effector;
    public float waitTime;

    // Start is called before the first frame update
    void Start()
    {
        effector = GetComponent<PlatformEffector2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetButtonUp("Jump"))
        {
            waitTime = 0.3f;
        }

        if (Input.GetAxisRaw("Vertical") < 0)
        {
            print(waitTime);
            if (waitTime <= 0)
            {
                effector.rotationalOffset = 180f;
                waitTime = 0.3f;
            }

            else
            {
                waitTime -= Time.deltaTime;
            }
        }

        if(Input.GetButtonDown("Jump"))
        {
            effector.rotationalOffset = 0;
        }
    }
}