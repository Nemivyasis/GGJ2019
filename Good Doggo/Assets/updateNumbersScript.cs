using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class updateNumbersScript : MonoBehaviour {

    public Sprite Zero;
    public Sprite One;
    public Sprite Two;
    public Sprite Three;
    public Sprite Four;
    public Sprite Five;

    LevelManagerScript manager;
	// Use this for initialization
	void Start () {
        manager = GameObject.FindGameObjectWithTag("LevelManager").GetComponent<LevelManagerScript>();
        updateCount();
	}
	
	// Update is called once per frame
	void Update () {
	}

    public void updateCount()
    {
        SetSprite(transform.GetChild(0).GetComponent<SpriteRenderer>(), manager.treeCount);
        SetSprite(transform.GetChild(1).GetComponent<SpriteRenderer>(), manager.treatCount);
        SetSprite(transform.GetChild(2).GetComponent<SpriteRenderer>(), manager.boulderCount);

    }

    private void SetSprite(SpriteRenderer render, int i)
    {
        switch (i)
        {
            case 0:
                render.sprite = Zero;
                break;
            case 1:
                render.sprite = One;
                break;
            case 2:
                render.sprite = Two;
                break;
            case 3:
                render.sprite = Three;
                break;
            case 4:
                render.sprite = Four;
                break;
            case 5:
                render.sprite = Five;
                break;
            default:
                break;
        }
    }
}
