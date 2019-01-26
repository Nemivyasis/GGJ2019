using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour {

    public enum levelObjects
    {
        dog,
        rock
    }
    levelObjects[,] grid;
    public int gridWidth = 10;
    public int gridHeight = 10;
    public Vector2 leftCorner = new Vector2(5f, -5f);
	// Use this for initialization
	void Start () {
        grid = new levelObjects[100,100];

        GameObject level = GameObject.FindGameObjectWithTag("Level");

        int objectCount = level.transform.childCount;

        for (int i = 0; i < objectCount; i++)
        {
            GameObject obj = level.transform.GetChild(i).gameObject;

            Vector2 pos = new Vector2(obj.transform.position.x - leftCorner.x, obj.transform.position.y - leftCorner.y);

            if(pos.x < 0 || pos.y < 0 || pos.x > gridWidth - 1 || pos.x > gridHeight - 1)
            {
                continue;
            }
            switch (obj.tag)
            {
                case "Dog":
                    grid[(int)pos.x, (int)pos.y] = levelObjects.dog;
                    break;
               case "Rock":
                    grid[(int)pos.x, (int)pos.y] = levelObjects.rock;
                    break;
                default:
                    break;
            }
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
