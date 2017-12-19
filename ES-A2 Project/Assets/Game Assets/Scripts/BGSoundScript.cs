using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGSoundScript : MonoBehaviour {
    public static BGSoundScript music;
    // Use this for initialization
    void Start () {
		
	}



    void Awake()
    {
        if (music == null)
        {
            music = this;
            GameObject.DontDestroyOnLoad(this.gameObject);
        }
    }
    //Play Gobal End

    // Update is called once per frame
    void Update () {
		
	}
}
