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

    public Rigidbody2D rigidBody;

    private bool allowJump = false;

    int count = 0;

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

        
        //jumping

        if(Input.GetButtonDown("Jump"))
        {
            
            if (count<2)
            {
                allowJump = true;
                count++;
            }
            else
            {
                allowJump = false;
            }

            
            if (allowJump==true)
            {
                rigidBody.velocity = new Vector2(rigidBody.velocity.x, Mathf.Sqrt(-2 * Physics2D.gravity.y * rigidBody.gravityScale * height)); //allows to jump a certain height, * with gravity scale and increased rgavity on object in game gives iniial faster speed for better looking jump

            }

        }

        //check if going down
        if (rigidBody.velocity.y<0)
        {
            rigidBody.velocity += Vector2.up*Physics2D.gravity.y*(fallSpeedMult-1)*Time.deltaTime;
        }

        
       
        
    }

    private void FixedUpdate()
    {
        rigidBody.velocity = new Vector2(horizontalAxisValues * horizontalSpeed, rigidBody.velocity.y);
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
                //count = 0;
            }
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (Mathf.Abs(rigidBody.velocity.y) < 0.0001)
        {
            count = 0;
        }
    }
}
