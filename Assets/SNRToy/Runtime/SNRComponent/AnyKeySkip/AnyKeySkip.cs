using System.Collections;
using System.Collections.Generic;
using SNRDicExtend;
using SNRInputManager;
using SNRLogHelper;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Video;




public class AnyKeySkip : MonoBehaviour
{

    public enum PhaseType
    {
        Unknown,
        Ani,
        Video,
    }

    public class PhaseData
    {
        public Object PhaseObj { get; set; } = null;
        public PhaseType PType { get; set; } = PhaseType.Unknown;

        #region Video
        public VideoClip VdoClip { get; set; } = null;
        public string VdoFilePath { get; set; } = null;

        #endregion

        #region Animation
        public int AniEndFrame { get; set; } = 0;
        public string AniName { get; set; } = null;

        #endregion

        public bool StopSound { get; set; } = true;
    }



    public DelRtnVoid2P<int, string> PlayAniEndCallback { get; set; } = null;
    public DelRtnVoid2P<int, string> PlayVideoEndCallback { get; set; } = null;
    public DelRtnVoid1P<int> AllPhaseDoneCallback { get; set; } = null;

    public int _maxPhase = 1;//当前一共有几个过场 需跳过几次
    public int _curPhaseIdx = 0;//当前处于第几的个过场

    public Dictionary<int, PhaseData> _phaseDic = new Dictionary<int, PhaseData>();

    public void StartWithData(int startPhase, int maxPhase, Dictionary<int, PhaseData> dicData)
    {
        _maxPhase = maxPhase;
        _phaseDic.Clear();
        _phaseDic.UpdateFromDic<int, PhaseData>(dicData);
        InitVideoCallBack();

        //do phase here
        DoPhase(startPhase);
    }

    void InitVideoCallBack()
    {
        List<VideoPlayer> list = new List<VideoPlayer>();
        foreach (var kyp in _phaseDic)
        {
            PhaseData data = _phaseDic[kyp.Key];
            if (data.PType == PhaseType.Video && !list.Contains((VideoPlayer)data.PhaseObj))
            {
                VideoPlayer player = data.PhaseObj as VideoPlayer;
                player.loopPointReached += PlayVideoEnd;

                list.Add(player);
            }
        }

    }

    void DoPhase(int pPhaseIdx)
    {
        bool beyondMax = pPhaseIdx >= _maxPhase;
        if (!beyondMax && _phaseDic.ContainsKey(pPhaseIdx))
        {
            ResetCanvasAlpha();
            _curPhaseIdx = pPhaseIdx;

            PhaseData data = _phaseDic[pPhaseIdx];
            switch (data.PType)
            {
                case PhaseType.Ani:
                    {
                        GetCurPhaseAni()?.Play(data.AniName);
                        StartCoroutine(WaitForAnimationEnd());
                    }
                    break;

                case PhaseType.Video:
                    {
                        VideoPlayer player = data.PhaseObj as VideoPlayer;
                        if (data.VdoClip != null)
                        {
                            player.clip = data.VdoClip;
                        }
                        else if (data.VdoFilePath != null)
                        {
                            player.url = data.VdoFilePath;
                        }

                        player.Play();
                    }
                    break;
            }
        }
        else
        {
            if (pPhaseIdx == _maxPhase)
            {
                AllPhaseDone();
            }
            else
            {
                SLog.Err($"not have the phase idx {pPhaseIdx}");
            }



        }
    }

    void AllPhaseDone()
    {
        SLog.Log("all phase done");
        if (AllPhaseDoneCallback != null)
        {
            AllPhaseDoneCallback(_curPhaseIdx);
        }
    }

    Animator GetCurPhaseAni()
    {
        PhaseData data = _phaseDic[_curPhaseIdx];
        if (data.PType != PhaseType.Ani)
        {
            return null;
        }
        Animator ani = data.PhaseObj as Animator;

        return ani;
    }

    IEnumerator WaitForAnimationEnd()
    {
        yield return null;
        Animator ani = GetCurPhaseAni();
        //1 animation play over
        while (ani.GetCurrentAnimatorStateInfo(0).normalizedTime < 1)
        {
            yield return null;
        }

        string aniName = ani.GetCurrentAnimatorClipInfo(0)[0].clip.name;
        SLog.Log($"checked {aniName} completed");
        PlayAnimationEnd(aniName);
    }


    void PlayAnimationEnd(string pName)
    {
        SLog.Log("ani play end " + pName);
        if (PlayAniEndCallback != null)
        {
            PlayAniEndCallback(_curPhaseIdx, pName);
        }

        DoPhase(_curPhaseIdx + 1);
    }

    void PlayVideoEnd(VideoPlayer pPlayer)
    {
        string vdoName = pPlayer.clip.name;
        SLog.Log("vdo play end " + vdoName);

        if (PlayVideoEndCallback != null)
        {
            PlayVideoEndCallback(_curPhaseIdx, vdoName);
        }

        DoPhase(_curPhaseIdx + 1);
    }



    void ShowTip(float pInterval = 0.1f)
    {
        CanvasGroup cg = GetRootCanvasGroup();
        if (cg.alpha > 0)
        {
            SLog.Log("already showing tip");
            return;
        }

        StartCoroutine(ShowTipSub(pInterval));
    }

    IEnumerator ShowTipSub(float pInterval)
    {
        CanvasGroup cg = GetRootCanvasGroup();
        //SLog.Log("cur alpha " + cg.alpha);
        while (cg.alpha < 1.0f)
        {
            cg.alpha += pInterval;
            //SLog.Log("increase alpha " + cg.alpha);
            yield return new WaitForSeconds(0.07f);
        }
    }


    bool IsTipShowComplete()
    {
        CanvasGroup cg = GetRootCanvasGroup();
        return cg.alpha >= 1.0f;
    }

    bool IsInPhase()
    {
        return _curPhaseIdx >= 0;
    }

    TextMeshProUGUI GetContinueLb()
    {
        TextMeshProUGUI lb = transform.Find("canvasRoot").Find("continue").GetComponent<TextMeshProUGUI>();

        return lb;
    }

    CanvasGroup GetRootCanvasGroup()
    {
        CanvasGroup cg = transform.Find("canvasRoot").GetComponent<CanvasGroup>();

        return cg;
    }


    void AnyKeyPressed(InputAction.CallbackContext obj)
    {
        SLog.Log($"got action name {obj.action.name}");
        if (IsInPhase())
        {
            if (IsTipShowComplete())
            {//skip
                TryToDoSkip();
            }
            else
            {//show tip
                ShowTip();
            }
        }
        else
        {
            SLog.Log($"not active, cur phase is {_curPhaseIdx}");
        }
    }

    void ResetCanvasAlpha()
    {
        CanvasGroup cg = GetRootCanvasGroup();
        cg.alpha = 0;
    }

    void TryToDoSkip()
    {
        if (_curPhaseIdx <= _maxPhase && _phaseDic.ContainsKey(_curPhaseIdx))
        {
            ResetCanvasAlpha();
            PhaseData pData = _phaseDic[_curPhaseIdx];
            if (pData.StopSound)
            {
                SoundManager.Instance?.StopMusic();
            }

            switch (pData.PType)
            {
                case PhaseType.Ani:
                    {
                        Animator ani = pData.PhaseObj as Animator;
                        ani.Play(pData.AniName, 0, pData.AniEndFrame);
                    }
                    break;

                case PhaseType.Video:
                    {
                        VideoPlayer vdoPlayer = pData.PhaseObj as VideoPlayer;
                        vdoPlayer.frame = (long)vdoPlayer.frameCount;
                    }
                    break;

                default:
                    {
                        SLog.Err($"sth wrong happen {pData.PType} curPhase {_curPhaseIdx}");
                    }
                    break;
            }

        }
    }

    void Awake()
    {
        InputManager.EnableSkipCutscene(AnyKeyPressed);
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {


    }

    void OnDisable()
    {
        InputManager.DisableSkipCutscene(AnyKeyPressed);
    }
}
