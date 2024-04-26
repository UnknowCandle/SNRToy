using System.Collections;
using System.Collections.Generic;
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
        }
        else
        {
            Destroy(gameObject);
        }
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
