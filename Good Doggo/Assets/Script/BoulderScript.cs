using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoulderScript : MonoBehaviour {
    public bool CanPush(Vector3 direction)
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position + direction, 0.2f);
        if (colliders.Length == 0) return true;
        if (colliders[0].tag.Equals("Pit")) return true;
        return false;
    }

    public void Push(Vector3 direction)
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position + direction, 0.2f);
        if (colliders.Length != 0 && colliders[0].tag.Equals("Pit"))
        {
            transform.SetPositionAndRotation(new Vector3(-30, 0, 0), new Quaternion(0f, 0f, 0f, 0f));
            colliders[0].transform.SetPositionAndRotation(new Vector3(-30, 0, 0), new Quaternion(0f, 0f, 0f, 0f));
        }
        else
        {
            transform.Translate(direction);
        }
    }
}
