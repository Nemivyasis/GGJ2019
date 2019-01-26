using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreatScript : MonoBehaviour {
    public Vector3 original;

    public void GetEaten()
    {
        transform.SetPositionAndRotation(new Vector3(-30, 0, 0), new Quaternion(0f, 0f, 0f, 0f));
    }

    public void Reset()
    {
        transform.SetPositionAndRotation(original, new Quaternion(0f, 0f, 0f, 0f));
    }
}
