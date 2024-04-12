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
    // Start is called before the first frame update
    void Start()
    {
        _fadeInId = Animator.StringToHash(KWord.LoadFadeIn);
        _fadeOutId = Animator.StringToHash(KWord.LoadFadeOut);
        _animator = GetComponent<Animator>();
        
        if(_fadeOutOnStrat){
            FadeOut();
        }
        
    }

    public void FadeIn(){
        _animator.Play(_fadeInId);
    }

    public void FadeOut(){
        _animator.Play(_fadeOutId);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
