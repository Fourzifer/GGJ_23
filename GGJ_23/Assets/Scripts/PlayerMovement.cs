using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private float horizontalAxisValues;
    private bool faceRight = true;

    public float maxAngle;
    public float horizontalSpeed;
    public float verticalSpeed;
    public float height;
    public float fallSpeedMult;

    public float airSpeedDamping = 0.2f;
    public float airSpeedRatio = 0.5f;

    public Rigidbody2D rigidBody;

    private bool allowJump = false;

    [SerializeField] int count = 0;

    //[HideInInspector]
    public bool UsingGrapplingHook = false;

    // Update is called once per frame
    void Update()
    {
        //flipping
        horizontalAxisValues = Input.GetAxisRaw("Horizontal");
        if (faceRight == true && horizontalAxisValues < 0) //turn left
        {
            flipDirection();
        }
        else if (faceRight == false && horizontalAxisValues > 0) //turn right
        {
            flipDirection();
        }


        if (UsingGrapplingHook == false)
        {
            //jumping
            if (Input.GetButtonDown("Jump"))
            {
                if (count < 2)
                {
                    allowJump = true;
                    count++;
                }
                else
                {
                    allowJump = false;
                }

                if (allowJump == true)
                {
                    // allows to jump a certain height,
                    // * with gravity scale and increased gavity on object in game gives iniial faster speed for better looking jump
                    rigidBody.velocity = new Vector2(rigidBody.velocity.x, Mathf.Sqrt(-2 * Physics2D.gravity.y * rigidBody.gravityScale * height));
                }
            }

            //check if going down
            if (rigidBody.velocity.y < 0)
            {
                rigidBody.velocity += Vector2.up * Physics2D.gravity.y * (fallSpeedMult - 1) * Time.deltaTime;
            }
        }
    }

    private void FixedUpdate()
    {
        if (UsingGrapplingHook)
        {
            // Only apply force when we are pressing the button.
            if (Mathf.Abs(horizontalAxisValues) > 0)
            {
                rigidBody.AddForce(new Vector2(horizontalAxisValues * horizontalSpeed, 0), ForceMode2D.Force);
            }
        }
        else
        {
            if (count > 0)
            {
                var s = horizontalAxisValues * horizontalSpeed * airSpeedRatio + Mathf.Lerp(0, rigidBody.velocity.x, 1 - airSpeedDamping);
                s = Mathf.Clamp(s, -horizontalSpeed, horizontalSpeed);
                rigidBody.velocity = new Vector2(s, rigidBody.velocity.y);
            }
            else
            {
                rigidBody.velocity = new Vector2(horizontalAxisValues * horizontalSpeed, rigidBody.velocity.y);
            }
        }
    }

    private void flipDirection()
    {
        faceRight = !faceRight;
        Vector3 changeDirection = transform.localScale;
        changeDirection.x = changeDirection.x*(-1);
        transform.localScale = changeDirection;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        for (int i = 0; i < collision.contactCount; i++)
        {
            Vector2 normal = collision.GetContact(i).normal;

            if(Vector2.Angle(Vector2.up, normal) < maxAngle) //set max angle that is allowed to be jumped on
            {
                if (Mathf.Abs(rigidBody.velocity.y) < 0.01)
                {
                    count = 0;
                }
            }
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (Mathf.Abs(rigidBody.velocity.y) < 0.01)
        {
            count = 0;
        }
    }
}
