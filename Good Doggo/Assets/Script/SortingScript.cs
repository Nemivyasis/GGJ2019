using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SortingScript : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {
        if (gameObject.tag.Equals("Pit"))
        {
            transform.Translate(new Vector3(0, 0, 0.15f));
        }
        else
        {
            transform.Translate(new Vector3(0, 0, transform.position.y * 0.01f));
        }
    }
}
