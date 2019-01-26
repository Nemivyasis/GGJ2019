using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour {

    bool[,] open;
    public int maxX=10;
    public int maxY = 10;
    public Vector2 leftCorner = new Vector2(5.5f, -5.5f);
	// Use this for initialization
	void Start () {
        open = new bool[100,100];
 
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
