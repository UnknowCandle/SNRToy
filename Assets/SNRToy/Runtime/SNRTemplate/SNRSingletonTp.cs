using System.Collections;
using System.Collections.Generic;
using SNRLogHelper;
using UnityEngine;

public class SNRSingletonTp<T> : SNRBehaviour where T : SNRBehaviour
{
    private static T _instance;
    public static T Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<T>();
            }

            if (_instance == null)
            {
                GameObject obj = new GameObject();
                _instance = obj.AddComponent<T>();
                obj.name = typeof(T).ToString();
                DontDestroyOnLoad(obj);

                string tName = typeof(T).Name;
                switch (tName)
                {
                    case "SoundManager":
                        {
                            obj.AddComponent<AudioController>();
                        }
                        break;

                    default:
                        {

                        }
                        break;

                }


            }

            return _instance;

        }
    }


    protected virtual void Awake()
    {
        if (_instance == null)
        {
            _instance = this as T;
            DontDestroyOnLoad(gameObject);
            SubClassAwakeInit();
        }
        else
        {
            SLog.Warn($"destory superfluous singleton obj of {gameObject.name}");
            Destroy(gameObject);
        }
    }

    /// <summary>
    /// use this not need to implement Awake
    /// </summary>

    public virtual void SubClassAwakeInit()
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
}
