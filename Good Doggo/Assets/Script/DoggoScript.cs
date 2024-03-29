﻿using System;
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

    public float updateTime = .51f;
    float sinceLastUpdate;
    public bool running;
    private bool onTreat;
    private bool onGoal;
    private bool checkShowing;
    private float checkTime;

    public Sprite doggoRight;
    public Sprite doggoLeft;
    public Sprite doggoDown;
    public Sprite doggoUp;

    public GameObject checkPrefab;

    public direction startingForward = direction.right;
    direction forward;

    Animator anim;

    public SidebarScript sidebar;

	// Use this for initialization
	void Start () {
        sinceLastUpdate = 0;
        anim = gameObject.GetComponent<Animator>();
        SetAnimation();
        sidebar = GameObject.FindGameObjectWithTag("Sidebar").GetComponent<SidebarScript>();
	}
	
	// Update is called once per frame
	void Update () {
        if (MenuScript.beaten && Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene("LevelSelect");
        }
        if (!checkShowing)
        {
            if (running)
            {
                sinceLastUpdate += Time.deltaTime;

                if (sinceLastUpdate >= updateTime)
                {
                    sinceLastUpdate = 0;

                    Act();
                }
            }

            if (Input.GetKeyDown("up"))
            {
                if (updateTime > 0.1f) updateTime -= 0.1f;
            }

            if (Input.GetKeyDown("down"))
            {
                if (updateTime < 1f) updateTime += 0.1f;
            }
        }
        else
        {
            checkTime -= Time.deltaTime;
            if(checkTime < 0.0f)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            }
        }
    }

    private void SetAnimation()
    {
        anim.SetInteger("direction", (int) forward);
    }
    private GameObject GetObjectAtLocation(Vector3 location)
    {
        Collider[] colliders = Physics.OverlapSphere(location, 0.4f);
        if (colliders.Length == 0) return null;
        return colliders[0].gameObject;

    }

    private void Act()
    {
        if (onGoal)
        {
            checkShowing = true;
            checkTime = 3.0f;
            GameObject c = Instantiate(checkPrefab);
            c.transform.position = new Vector3(-0.5f, -4.5f, -1.0f);
        }
        else
        {
            Vector3 forwardLoc = transform.position + GetDirection(forward);
            if (onTreat)
            {
                if (forwardLoc.x > -8 && forwardLoc.x < 7 && forwardLoc.y > -12 && forwardLoc.y < 3) forwardLoc += GetDirection(forward);
                else onTreat = false;
            }
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
                Move();
                onGoal = true;
            }
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
        transform.position = (new Vector3(transform.position.x, transform.position.y, transform.position.y * 0.01f));
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
        sidebar.setPlay();
    }

    public void UpdateDirection()
    {
        SetAnimation();
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

