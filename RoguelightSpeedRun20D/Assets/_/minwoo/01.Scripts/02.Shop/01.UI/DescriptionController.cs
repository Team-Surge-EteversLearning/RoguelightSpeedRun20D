using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DescriptionController : MonoBehaviour
{ 

[SerializeField] TMP_Text descriptionTxt;

    public static Action<string> onDescription;
    public static Action onDescriptionComplete;


    void Start()
    {
        onDescription = UpdateDescriptionText;
        onDescriptionComplete = endDescription;
        gameObject.SetActive(false);
    }

    void UpdateDescriptionText(string text)
    {
        if (!gameObject.activeSelf)
        {
            gameObject.SetActive(true);
        }
        descriptionTxt.text = text;
    }
    void endDescription()
    {
        print("!!!!!");
        gameObject.SetActive(false);
        descriptionTxt.text = "";
    }
}
