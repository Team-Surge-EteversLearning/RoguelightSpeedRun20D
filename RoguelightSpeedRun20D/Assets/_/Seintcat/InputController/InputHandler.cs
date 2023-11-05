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

    [SerializeField]
    private InputActionAsset actionAsset;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        actionAsset.Enable();
        actionAsset.FindActionMap("Fighting").Enable();
        actionAsset.FindActionMap("Fighting").FindAction("Move").performed += OnMove;
        actionAsset.FindActionMap("Fighting").FindAction("Move").canceled += OnMove;
        actionAsset.FindActionMap("Fighting").FindAction("Running").started += OnRunning;
        actionAsset.FindActionMap("Fighting").FindAction("Running").canceled += OnRunning;
        actionAsset.FindActionMap("Fighting").FindAction("Attack").started += OnAttack;
        actionAsset.FindActionMap("Fighting").FindAction("Attack").canceled += OnAttack;
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

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
        else
            attack = false;
    }
}
