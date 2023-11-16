using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MyTestTxt : MonoBehaviour
{
    [SerializeField] TMP_Text text;
    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    private void Update()
    {
        text.text = PlayerStatsManager.HpMax.ToString();
    }

}
