using System;
using System.Collections;
using System.Collections.Generic;
using SNRKWordDefine;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class LoadingLayer : MonoBehaviour
{
    [SerializeField] private bool _fadeOutOnStrat;
    private Animator _animator;

    private int _fadeInId;
    private int _fadeOutId;

    public Action BeforeFadeInAni { get; set; } = null;
    public Action BeforeFadeOutAni { get; set; } = null;


    public static LoadingLayer GetCurLoadingLayer()
    {
        GameObject obj = GameObject.Find("LoadingLayer");
        LoadingLayer ret = null;
        if (obj)
        {
            ret = obj.transform.Find("RootCanvas").GetComponent<LoadingLayer>();
        }

        return ret;
    }

    // Start is called before the first frame update
    void Start()
    {
        _fadeInId = Animator.StringToHash(KWord.LoadFadeIn);
        _fadeOutId = Animator.StringToHash(KWord.LoadFadeOut);
        _animator = GetComponent<Animator>();

        if (_fadeOutOnStrat)
        {
            FadeOut();
        }

    }

    public virtual void FadeIn()
    {
        if (BeforeFadeInAni != null)
        {
            BeforeFadeInAni();
        }
        _animator.Play(_fadeInId);
    }

    public virtual void FadeOut()
    {
        if (BeforeFadeOutAni != null)
        {
            BeforeFadeOutAni();
        }
        _animator.Play(_fadeOutId);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
