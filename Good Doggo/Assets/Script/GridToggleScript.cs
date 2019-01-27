using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridToggleScript : MonoBehaviour {
    // Update is called once per frame
	void Update () {
        if (Input.GetKeyDown("g"))
        {
            if (transform.position.z == 1) transform.Translate(new Vector3(0, 0, 2));
            else transform.Translate(new Vector3(0, 0, -2));
        }
	}
}
