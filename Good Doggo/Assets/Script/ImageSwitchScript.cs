using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImageSwitchScript : MonoBehaviour {

    public Sprite image1;
    public Sprite image2;
    public SpriteRenderer render;
    bool im1current;
    float time = 0.0f;

	// Use this for initialization
	void Start () {
        im1current = true;
        render = gameObject.GetComponent<SpriteRenderer>();
	}
	
	// Update is called once per frame
	void Update () {
        time += Time.deltaTime;
        if(time >= 1.0f)
        {
            time = 0.0f;
            changeImage();
        }
	}

    public void changeImage()
    {
        if (im1current)
        {
            im1current = false;
            render.sprite = image2;
        }
        else
        {
            im1current = true;
            render.sprite = image1;
        }
    }
}
