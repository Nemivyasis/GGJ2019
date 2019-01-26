using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
    public bool running;
    private bool onTreat;

    public Sprite doggoRight;
    public Sprite doggoLeft;
    public Sprite doggoDown;
    public Sprite doggoUp;

    public direction startingForward = direction.right;
    direction forward;

	// Use this for initialization
	void Start () {
        sinceLastUpdate = 0;
	}
	
	// Update is called once per frame
	void Update () {
        if (running)
        {
            sinceLastUpdate += Time.deltaTime;

            if (sinceLastUpdate >= updateTime)
            {
                sinceLastUpdate = 0;

                Act();
            }
        }
        else
        {
            if (Input.GetKeyDown("s"))
            {
                running = true;
            }
        }
	}

    private GameObject GetObjectAtLocation(Vector3 location)
    {
        Collider[] colliders = Physics.OverlapSphere(location, 0.2f);
        if (colliders.Length == 0) return null;
        return colliders[0].gameObject;

    }

    private void Act()
    {
        Vector3 forwardLoc = transform.position + GetDirection(forward);
        if(onTreat) forwardLoc += GetDirection(forward);
        GameObject whatsAhead = GetObjectAtLocation(forwardLoc);
        if (whatsAhead == null) Move();
        else if (whatsAhead.tag.Equals("Tree"))
        {
            TurnRight();
        }
        else if (whatsAhead.tag.Equals("Treat"))
        {
            Move();
            onTreat = true;
            Collider[] colliders = Physics.OverlapSphere(transform.position, 0.2f);
            if (colliders.Length != 0)
            {
                GameObject treat = colliders[0].gameObject;
                treat.GetComponent<TreatScript>().GetEaten();
            }

        }
        else if (whatsAhead.tag.Equals("Pit"))
        {
            ResetAll();
            running = false;
        }
        else if (whatsAhead.tag.Equals("Boulder"))
        {
            BoulderScript bs = whatsAhead.GetComponent<BoulderScript>();
            if (bs.CanPush(GetDirection(forward)))
            {
                bs.Push(GetDirection(forward));
                Move();
            }
            else
            {
                TurnRight();
            }
        }

        else if (whatsAhead.tag.Equals("Goal"))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }

    private void TurnRight()
    {
        if (onTreat)
        {
            ResetAll();
        }
        else
        {
            forward = RightTurn(forward);
            UpdateDirection();
        }
    }

    private void Move()
    {
        if (onTreat)
        {
            
            transform.Translate(GetDirection(forward) * 2);
            onTreat = false;
        }
        else
        {
            transform.Translate(GetDirection(forward));
        }
    }

    public void ResetAll()
    {
        GameObject[] objects = UnityEngine.Object.FindObjectsOfType<GameObject>();
        foreach(GameObject o in objects)
        {
            if (o.activeInHierarchy)
            {
                ResetScript rs = o.GetComponent<ResetScript>();
                if (rs != null) rs.Reset();
            }
        }
        forward = startingForward;
        UpdateDirection();
        onTreat = false;
        running = false;
    }

    public void UpdateDirection()
    {
        SpriteRenderer sr = (SpriteRenderer)gameObject.GetComponent<SpriteRenderer>();
        switch (forward)
        {

            case direction.right:
                sr.sprite = doggoRight;
                break;
            case direction.left:
                sr.sprite = doggoLeft;
                break;
            case direction.down:
                sr.sprite = doggoDown;
                break;
            default:
                sr.sprite = doggoUp;
                break;
        }
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

    private direction RightTurn(direction current)
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

    private direction LeftTurn(direction current)
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

    private direction AboutTurn(direction current)
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
