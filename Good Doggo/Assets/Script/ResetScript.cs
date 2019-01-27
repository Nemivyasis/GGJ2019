using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetScript : MonoBehaviour {

    Vector3 original;

    void Start()
    {
        original = transform.position;
    }

    public void Reset()
    {
        transform.SetPositionAndRotation(original, new Quaternion(0f, 0f, 0f, 0f));
    }
}
