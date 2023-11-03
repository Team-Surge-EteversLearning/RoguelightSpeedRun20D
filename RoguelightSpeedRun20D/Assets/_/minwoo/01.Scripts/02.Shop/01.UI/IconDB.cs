using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IconDB : MonoBehaviour
{
    public static IconDB instance;
    private void Awake()
    {
        instance = this;
    }
    public IconSet iconSet;
}
