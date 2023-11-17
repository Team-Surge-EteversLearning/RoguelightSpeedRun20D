using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
[CreateAssetMenu(fileName = "iconSet_", menuName = "icon")]
public class IconSet : ScriptableObject
{
    [SerializeField] Sprite defaultImg;
    [SerializeField] List<Icon> icons = new List<Icon>();

    public Sprite GetIcon(string name)
    {
        foreach (var icon in icons)
        {
            if (name == icon.Name)
                return icon.IconSprite;
        }
        return defaultImg;
    }
}
