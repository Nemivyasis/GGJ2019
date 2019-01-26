using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoggoScript : MonoBehaviour {

    public enum direction
    {
        right,
        left,
        down,
        up
    }

    public float updateTime = .5f;
    float sinceLastUpdate;

    public direction startingForward = direction.right;
    direction forward;

	// Use this for initialization
	void Start () {
        sinceLastUpdate = 0;
	}
	
	// Update is called once per frame
	void Update () {
        sinceLastUpdate += Time.deltaTime;

        if(sinceLastUpdate >= updateTime)
        {
            sinceLastUpdate = 0;

            Act();
        }
	}

    private int GetWallAtLocation(Vector3 location)
    {
        Collider[] colliders = Physics.OverlapSphere(location, 0.2f);
        if (colliders.Length == 0) return 0;
        GameObject found = colliders[0].gameObject;
        if (found.tag.Equals("Rock")) return 1;
        return 0;

    }

    private void Act()
    {
        Vector3 forwardLoc = transform.position + GetDirection(forward);
        int whatsAhead = GetWallAtLocation(forwardLoc);
        if (whatsAhead == 0) Move();
        else if (whatsAhead == 1) forward = TurnRight(forward);
    }

    private void Move()
    {
        transform.Translate(GetDirection(forward));
    }

    private Vector3 GetDirection(direction d)
    {
        switch (d)
        {
            case direction.right:
                return new Vector3(1, 0, 0);
            case direction.left:
                return new Vector3(-1, 0, 0);
            case direction.down:
                return new Vector3(0, -1, 0);
            default:
                return new Vector3(0, 1, 0);
        }
    }

    private direction TurnRight(direction current)
    {
        switch(current)
        {
            case direction.right:
                return direction.down;
            case direction.left:
                return direction.up;
            case direction.down:
                return direction.left;
            default:
                return direction.right;
        }
    }

    private direction TurnLeft(direction current)
    {
        switch (current)
        {
            case direction.right:
                return direction.up;
            case direction.left:
                return direction.down;
            case direction.down:
                return direction.right;
            default:
                return direction.left;
        }
    }

    private direction TurnAround(direction current)
    {
        switch (current)
        {
            case direction.right:
                return direction.left;
            case direction.left:
                return direction.right;
            case direction.down:
                return direction.up;
            default:
                return direction.down;
        }
    }
}
