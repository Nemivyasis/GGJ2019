using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SidebarScript : MonoBehaviour {

    SpriteRenderer render;
    public Sprite play;
    public Sprite pause;

    void Start()
    {
        render = gameObject.GetComponent<SpriteRenderer>();
    }

    public void setPlay(){
        render.sprite = play;
    }

    public void setPause(){
        render.sprite = pause;
    }
}
