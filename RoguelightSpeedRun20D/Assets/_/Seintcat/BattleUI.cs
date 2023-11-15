using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleUI : MonoBehaviour
{
    [SerializeField]
    private Slider hpSlider;
    [SerializeField]
    private Slider mpSlider;
    [SerializeField]
    private Slider stSlider;

    private static float _hpBar = 1f;
    private static float _mpBar = 1f;
    private static float _stBar = 1f;

    public static float hpBar { set { _hpBar = value; } }
    public static float mpBar { set { _mpBar = value; } }
    public static float stBar { set { _stBar = value; } }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        hpSlider.value = Mathf.Lerp(hpSlider.value, _hpBar, Time.deltaTime);
        mpSlider.value = Mathf.Lerp(mpSlider.value, _mpBar, Time.deltaTime);
        stSlider.value = Mathf.Lerp(stSlider.value, _stBar, Time.deltaTime);
    }
}
