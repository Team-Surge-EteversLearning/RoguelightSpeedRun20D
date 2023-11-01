using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface InterfaceGo
{
}


public abstract class ac
{
    public static Dictionary<string, ac> dict = new Dictionary<string, ac>();

    public virtual ac Find(string name)
    {
        return dict[name];
    }
}