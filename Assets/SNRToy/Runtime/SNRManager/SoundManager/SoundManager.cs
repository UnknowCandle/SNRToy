using SNRKWordDefine;
using SNRLogHelper;
using UnityEngine;

public class SoundManager : SNRSingletonTp<SoundManager>
{
    public float DefaultVolume { get; set; } = 0.8f;

    public bool IsMute
    {
        get
        {
            bool mute = PlayerPrefs.GetInt(KWord.IsSoundMute, 1) == 1;

            return mute;
        }
        set
        {
            int storeValue = value ? 1 : 0;
            PlayerPrefs.SetInt(KWord.IsSoundMute, storeValue);
        }
    }


    public float GetGlobalVolum()
    {
        float volume = PlayerPrefs.GetFloat(KWord.GlobalVolume, DefaultVolume);

        return volume;
    }


    //0-1
    public void SetGlobalVolume(float pVolume)
    {
        PlayerPrefs.SetFloat(KWord.GlobalVolume, pVolume);
        AudioController.SetGlobalVolume(pVolume);
    }


    public float GetCategoryVolume(string pName)
    {
        float volume = PlayerPrefs.GetFloat(KWord.SoundCategory + pName, DefaultVolume);

        return volume;
    }
    public void SetCategoryVolume(string pName, float pVolume)
    {
        PlayerPrefs.SetFloat(KWord.SoundCategory + pName, pVolume);
        AudioController.SetCategoryVolume(pName, pVolume);
    }

    public bool StopMusic()
    {
        return AudioController.StopMusic();
    }

    public void PlayMusic(string name)
    {
        if (!IsMute)
        {
            AudioController.PlayMusic(name);
        }
    }


    public bool Stop(string pName = null, float pFadeOutLength = -1)
    {
        if (string.IsNullOrEmpty(pName))
        {
            AudioController.StopAll();
            return true;
        }

        return AudioController.Stop(pName, pFadeOutLength);
    }
    public void Play(string name)
    {
        if (!IsMute)
        {
            AudioController.Play(name);
        }
    }

    public void ButtonClick(string name = "DefBtnClick")
    {
        Play(name);
    }


    private static SoundManager _instance;

    private SoundManager()
    {

    }

    void Start()
    {
        SLog.Log("soundmanager start now");

    }


    public override void SubClassAwakeInit()
    {
        base.SubClassAwakeInit();

        SLog.Log("soundmanager awake now");
        SetGlobalVolume(GetGlobalVolum());

    }


}
