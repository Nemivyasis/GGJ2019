using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class timerScript : MonoBehaviour {

    float timer;
    float switchTime = 1;
    bool fastDog = false;
    Animator anim;
	// Use this for initialization
	void Start () {
        anim = gameObject.GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
        timer += Time.deltaTime;
        if(timer > switchTime)
        {
            timer = 0;
            fastDog = !fastDog;
            anim.SetBool("fast", fastDog);
        }
	}
}
