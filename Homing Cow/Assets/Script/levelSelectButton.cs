using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class levelSelectButton : MonoBehaviour {

    public int levelNum;
    int levelBuildOffset = 1;

    public void levelSelect()
    {
        SceneManager.LoadScene(levelNum + levelBuildOffset - 1);
    }
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
