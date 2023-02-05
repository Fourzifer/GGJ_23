using Freya;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hook : MonoBehaviour
{
    GameObject[] HookPoints;

    LineRenderer Line;

    bool swinging = false;

    Rigidbody2D Rigidbody2D;
    HingeJoint2D Hinge;

    PlayerMovement Player;

    GameObject GrabbedPoint;

    // Start is called before the first frame update
    void Start()
    {
        Player = GetComponent<PlayerMovement>();
        Rigidbody2D = GetComponent<Rigidbody2D>();
        
        HookPoints = GameObject.FindGameObjectsWithTag("Hook Point");

        // FIXME: Should the line renderer be on the same gameobject?
        // probably not
        Line = gameObject.AddComponent<LineRenderer>();
        Line.enabled = false;

        Hinge = gameObject.AddComponent<HingeJoint2D>();
        Hinge.enableCollision = true;
        Hinge.autoConfigureConnectedAnchor = false;
        Hinge.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        GameObject closestHookPoint = FindClosestHookPoint(transform.position);

        // FIXME: Highlight the closest hook point!
        if (Input.GetButtonDown("Hook"))
        {
            if (swinging)
            {
                ReleaseHook();
                swinging = false;
            }
            else
            {
                GrabHook(closestHookPoint);
                swinging = true;
            }
        }

        if (Input.GetButtonDown("Jump"))
        {
            if (swinging)
            {
                ReleaseHook();
                swinging = false;
            }
        }

        if (swinging)
        {
            Line.SetPosition(1, transform.position);
        }
    }

    void GrabHook(GameObject hookPoint)
    {
        Line.SetPosition(0, hookPoint.transform.position);
        Line.SetPosition(1, transform.position);
        Line.enabled = true;

        swinging = true;

        // Don't freeze rotation.
        Rigidbody2D.constraints &= ~RigidbodyConstraints2D.FreezeRotation;

        var direction = hookPoint.transform.position - transform.position;

        Rigidbody2D.SetRotation(Vector2.SignedAngle(Vector2.up, direction));

        float dist = Vector2.Distance(transform.position, hookPoint.transform.position);

        Hinge.anchor = new Vector2(0, dist);
        Hinge.connectedAnchor = hookPoint.transform.position;
        Hinge.enabled = true;

        Player.UsingGrapplingHook = true;

        GrabbedPoint = hookPoint;
    }

    void ReleaseHook()
    {
        Line.enabled = false;

        swinging = true;

        Rigidbody2D.SetRotation(0);

        // Freeze rotation.
        Rigidbody2D.constraints |= RigidbodyConstraints2D.FreezeRotation;

        Hinge.enabled = false;

        Player.UsingGrapplingHook = false;

        GrabbedPoint = null;
    }

    GameObject FindClosestHookPoint(Vector2 position)
    {
        float closestDistanceSqr = float.PositiveInfinity;
        GameObject closestPoint = null;

        foreach (var point in HookPoints)
        {
            float distSqr = Vector2.SqrMagnitude(position - (Vector2)point.transform.position);
            if (distSqr < closestDistanceSqr)
            {
                closestDistanceSqr = distSqr;
                closestPoint = point;
            }
        }

        return closestPoint;
    }
    
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (swinging)
        {
            for (int i = 0; i < collision.contactCount; i++)
            {
                Vector2 normal = collision.GetContact(i).normal;

                if (Vector2.Angle(Vector2.up, normal) < 75)
                {
                    ReleaseHook();
                    swinging = false;
                }
            }
        }
    }
}
