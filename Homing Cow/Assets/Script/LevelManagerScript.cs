﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManagerScript : MonoBehaviour {

    public int maxTrees;
    public int maxTreats;
    public int maxBoulders;
    int treeCount;
    int treatCount;
    int boulderCount;

    int currentSelected = 0;

    public GameObject treePrefab;
    public GameObject treatPrefab;
    public GameObject boulderPrefab;

    public Sprite treeSprite;
    public Sprite treatSprite;
    public Sprite boulderSprite;

    SpriteRenderer render;

    List<GameObject> placedObjects = new List<GameObject>();

    private void Start()
    {
        treeCount = maxTrees;
        treatCount = maxTreats;
        boulderCount = maxBoulders;
        render = gameObject.GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        if (currentSelected > 0)
        {
            if(mousePos.x >= 7.5 && render.enabled)
            {
                render.enabled = false;
            }
            if(mousePos.x < 7.5 && !render.enabled && currentSelected > 0)
            {
                render.enabled = true;
            }
            transform.position = new Vector3(Mathf.Floor(mousePos.x + .5f), Mathf.Floor(mousePos.y + .5f), -5);
        }

        if (Input.GetKeyDown("1"))
        {
            currentSelected = 1;
            render.enabled = true;
            render.sprite = treeSprite;
        }
        if (Input.GetKeyDown("2"))
        {
            currentSelected = 2;
            render.enabled = true;
            render.sprite = treatSprite;
        }
        if (Input.GetKeyDown("3"))
        {
            currentSelected = 3;
            render.enabled = true;
            render.sprite = boulderSprite;
        }
        if (Input.GetKeyDown("`"))
        {
            currentSelected = -1;
            render.enabled = false;
        }

        if (Input.GetMouseButtonDown(0))
        {
            if (mousePos.x >= 7.5)
            {

            }
            else
            {
                if (currentSelected == -1)
                {
                    DeleteElement(mousePos);
                }
                else if (currentSelected > 0)
                {
                    PlaceElement(mousePos);
                }
            }
        }

        if (Input.GetMouseButtonDown(1))
        {
            DeleteElement(mousePos);
        }
    }

    void PlaceElement(Vector3 pos)
    {
        Vector3 roundedPos = new Vector3(Mathf.Round(pos.x), Mathf.Round(pos.y), 0f);
        Collider[] colliders = Physics.OverlapSphere(roundedPos, 0.2f);
        if (colliders.Length == 0) {
            GameObject p = null;
            switch (currentSelected)
            {
                case 1:
                    if (treeCount > 0)
                    {
                        treeCount--;
                        p = Instantiate(treePrefab);
                    }
                    break;

                case 2:
                    if (treatCount > 0)
                    {
                        treatCount--;
                        p = Instantiate(treatPrefab);
                    }
                    break;

                case 3:
                    if (boulderCount > 0)
                    {
                        boulderCount--;
                        p = Instantiate(boulderPrefab);
                    }
                    break;

                default:
                    break;

            }
            if (p != null)
            {
                p.transform.position = roundedPos;
                currentSelected = 0;
                placedObjects.Add(p);
                render.enabled = false;
            }
        }
        
    }

    void DeleteElement(Vector3 pos)
    {
        Vector3 roundedPos = new Vector3(Mathf.Round(pos.x), Mathf.Round(pos.y), 0f);
        Collider[] colliders = Physics.OverlapSphere(roundedPos, 0.2f);
        if (colliders.Length != 0)
        {
            GameObject g = colliders[0].gameObject;
            bool found = false;
            foreach (GameObject check in placedObjects)
            {
                if (g == check)
                {
                    if (g.tag.Equals("Tree"))
                    {
                        treeCount++;
                    }
                    else if (g.tag.Equals("Treat"))
                    {
                        treatCount++;
                    }
                    else if (g.tag.Equals("Boulder"))
                    {
                        boulderCount++;
                    }
                    Destroy(g);
                    found = true;
                }
            }
            if (found) placedObjects.Remove(g);
        }
    }

    void DeleteAll()
    {
        foreach (GameObject g in placedObjects)
        {
            if (g.tag.Equals("Tree"))
            {
                treeCount++;
            }
            else if (g.tag.Equals("Treat"))
            {
                treatCount++;
            }
            else if (g.tag.Equals("Boulder"))
            {
                boulderCount++;
            }
            Destroy(g);
        }

        placedObjects = new List<GameObject>();
    }
}
