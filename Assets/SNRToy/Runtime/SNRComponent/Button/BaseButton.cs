using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseButton : SNRBehaviour
{
    private static string s_defClickSoundName = null;

    public string DefClickSoundName
    {
        get { return s_defClickSoundName; }
        set { s_defClickSoundName = value; }
    }


    public bool _playClickSound = true;


    void Awake()
    {

    }

    // Start is called before the first frame update
    void Start()
    {

    }



    // Update is called once per frame
    void Update()
    {

    }


    public virtual void PlayClickSound(string pClickSoundName = null)
    {
        if (_playClickSound)
        {
            string playName = String.IsNullOrEmpty(pClickSoundName) ? s_defClickSoundName : pClickSoundName;
            if (string.IsNullOrEmpty(playName))
            {
                FindObjectOfType<SoundManager>()?.Play(playName);
            }
        }
    }
}
