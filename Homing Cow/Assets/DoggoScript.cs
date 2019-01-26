using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoggoScript : MonoBehaviour {

    public enum directions
    {
        right,
        left,
        down,
        up
    }
    public enum AI
    {
        forwardRight
    }

    public float updateTime = .5f;
    float sinceLastUpdate;

    AI aiChoice;
    public directions startingForward = directions.right;
    directions forward;

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

            switch (aiChoice)
            {
                case AI.forwardRight:
                    ForwardRight();
                    break;
                default:
                    break;
            }
        }
	}

    private void ForwardRight()
    {
        switch (forward)
        {
            case directions.right:
                transform.Translate(new Vector3(1, 0, 0));
                break;
            case directions.left:
                transform.Translate(new Vector3(-1, 0, 0));
                break;
            case directions.down:
                transform.Translate(new Vector3(0, -1, 0));
                break;
            case directions.up:
                transform.Translate(new Vector3(0, 1, 0));
                break;
            default:
                break;
        }
    }
}
