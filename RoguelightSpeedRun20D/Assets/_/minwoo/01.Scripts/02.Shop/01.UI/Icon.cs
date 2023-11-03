using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
[Serializable]
public class Icon
{
    [SerializeField]string name;
    [SerializeField]Sprite icon;

    public string Name { get => name; set => name = value; }
    public Sprite IconSprite { get => icon; set => icon = value; }
}
