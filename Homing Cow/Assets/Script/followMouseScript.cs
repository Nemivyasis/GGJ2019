using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class followScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.position = new Vector3(Mathf.Floor(mousePos.x + .5f), Mathf.Floor(mousePos.y + .5f), -5);
	}
}
