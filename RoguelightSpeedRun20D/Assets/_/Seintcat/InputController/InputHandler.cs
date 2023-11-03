using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputHandler : MonoBehaviour
{
    public static PlayerInput playerInput { get; private set; }
    public static Vector2 move {  get; private set; }

    [SerializeField]
    private InputActionAsset actionAsset;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        actionAsset.Enable();
        actionAsset.FindActionMap("Fighting").Enable();
        actionAsset.FindActionMap("Fighting").FindAction("Move").performed += OnMove;
        actionAsset.FindActionMap("Fighting").FindAction("Move").canceled += OnMove;
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
        //Debug.Log(context.ReadValue<Vector2>());
        move = context.ReadValue<Vector2>();
    }

    public static void Debugs(string str)
    {
        Debug.Log(str);
    }
}
