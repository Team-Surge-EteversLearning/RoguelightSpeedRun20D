using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestDB : MonoBehaviour
{
    public static TestDB instance;
    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
        DontDestroyOnLoad(gameObject);
    }
    public IconSet iconSet;

    public delegate void DBInitialize();
    public static DBInitialize dbi; 
}
