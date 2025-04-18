using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public abstract class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
    private static T instance;

    public static T Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<T>();

                //if(instance == null)
                //{
                //    GameObject gameObject = new GameObject(typeof(T).Name);
                //    instance = gameObject.AddComponent<T>();
                //}
            }
            return instance;
        }
    }

    private void Awake()
    {
        CreateIntance();
    }

    private void CreateIntance()
    {
        if (instance == null)
        {
            instance = this as T;
            if (!ShouldDestroyOnLoad)
            {
                DontDestroyOnLoad(gameObject);
            }
        }

        else
        {
            Destroy(gameObject);
        }
    }

    private void OnDestroy()
    {
        if (instance == null) return;
        instance = null;
    }

    protected virtual bool ShouldDestroyOnLoad { get; }
}
