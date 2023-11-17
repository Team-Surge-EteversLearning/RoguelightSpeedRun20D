using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputHandler : MonoBehaviour
{
    public static PlayerInput playerInput { get; private set; }
    public static Vector2 move { get; private set; }
    public static bool running { get; private set; }
    public static bool attack { get; private set; }
    public static bool avoid { get; private set; }
    public static bool defence { get; private set; }
    public static float camRotate { get; private set; }
    public static bool skill1 { get; private set; }
    public static bool skill2 { get; private set; }
    public static bool item1 { get; private set; }
    public static bool item2 { get; private set; }
    public static bool item3 { get; private set; }
    public static bool item4 { get; private set; }
    public static bool info { get; private set; }

    public static float skill1Last;
    public static float skill2Last;
    public static float item1Last;
    public static float item2Last;
    public static float item3Last;
    public static float item4Last;

    [SerializeField]
    private InputActionAsset actionAsset;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        actionAsset.Enable();
        InputActionMap map = actionAsset.FindActionMap("Fighting");
        map.Enable();
        map.FindAction("Move").performed += OnMove;
        map.FindAction("Move").canceled += OnMove;
        map.FindAction("Running").started += OnRunning;
        map.FindAction("Running").canceled += OnRunning;
        map.FindAction("Attack").started += OnAttack;
        map.FindAction("Attack").canceled += OnAttack;
        map.FindAction("Avoid").started += OnAvoid;
        map.FindAction("Avoid").canceled += OnAvoid;
        map.FindAction("Defence").started += OnDefence;
        map.FindAction("Defence").canceled += OnDefence;
        map.FindAction("CameraRotate").performed += OnCameraRotate;
        map.FindAction("CameraRotate").canceled += OnCameraRotate;
        map.FindAction("Skill1").performed += OnSkill1;
        map.FindAction("Skill1").canceled += OnSkill1;
        map.FindAction("Skill2").performed += OnSkill2;
        map.FindAction("Skill2").canceled += OnSkill2;
        map.FindAction("Item1").performed += OnItem1;
        map.FindAction("Item1").canceled += OnItem1;
        map.FindAction("Item2").performed += OnItem2;
        map.FindAction("Item2").canceled += OnItem2;
        map.FindAction("Item3").performed += OnItem3;
        map.FindAction("Item3").canceled += OnItem3;
        map.FindAction("Item4").performed += OnItem4;
        map.FindAction("Item4").canceled += OnItem4;
        map.FindAction("OpenInfo").performed += OnInfo;
        map.FindAction("OpenInfo").canceled += OnInfo;
    }


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void LateUpdate()
    {
        attack = false;
        avoid = false;
        skill1 = false;
        skill2 = false;
        item1 = false;
        item2 = false;
        item3 = false;
        item4 = false;
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        move = context.ReadValue<Vector2>();
    }

    public void OnRunning(InputAction.CallbackContext context)
    {
        if (context.ReadValue<float>() > 0.5f)
            running = true;
        else
            running = false;
    }

    public void OnAttack(InputAction.CallbackContext context)
    {
        if (context.ReadValue<float>() > 0.5f)
            attack = true;
    }

    public void OnAvoid(InputAction.CallbackContext context)
    {
        if (context.ReadValue<float>() > 0.5f)
            avoid = true;
    }

    private void OnDefence(InputAction.CallbackContext context)
    {
        if (context.ReadValue<float>() > 0.5f)
            defence = true;
        else
            defence = false;
    }

    private void OnCameraRotate(InputAction.CallbackContext context)
    {
        camRotate = context.ReadValue<float>();
        //Debug.LogWarning(camRotate);
    }

    public void OnSkill1(InputAction.CallbackContext context)
    {
        if (context.ReadValue<float>() > 0.5f)
            skill1 = true;
    }

    public void OnSkill2(InputAction.CallbackContext context)
    {
        if (context.ReadValue<float>() > 0.5f)
            skill2 = true;
    }

    private void OnItem1(InputAction.CallbackContext context)
    {
        if (context.ReadValue<float>() > 0.5f)
            item1 = true;
    }

    private void OnItem2(InputAction.CallbackContext context)
    {
        if (context.ReadValue<float>() > 0.5f)
            item2 = true;
    }
    private void OnItem3(InputAction.CallbackContext context)
    {
        if (context.ReadValue<float>() > 0.5f)
            item3 = true;
    }
    private void OnItem4(InputAction.CallbackContext context)
    {
        if (context.ReadValue<float>() > 0.5f)
            item4 = true;
    }
    private void OnInfo(InputAction.CallbackContext context)
    {
        if (context.ReadValue<float>() > 0.5f)
            info = true;
        else
            info = false;
    }
}
