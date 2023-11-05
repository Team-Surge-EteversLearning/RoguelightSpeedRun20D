using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerSM : StateManager
{
    public static readonly float moveSpeed = 0.2f;
    public static readonly float runSpeed = 0.35f;

    [SerializeField]
    private GameObject weaponHanger;
    [SerializeField]
    private List<GameObject> weaponModels;

    private List<GameObject> weaponInstance;
    private GameObject weaponModelNow;

    private static Weapon _weaponNow;
    public static Weapon weaponNow
    {
        set 
        { 
            _weaponNow = value;
            
        }
    }
    private static Armor _armorNow;
    public static Armor armorNow
    {
        set
        {
            _armorNow = value;
        }
    }
    private static Shoes _shoesNow;
    public static Shoes shoesNow
    {
        set
        {
            _shoesNow = value;
        }
    }

    private void Awake()
    {
        ManagerStart();
    }
    public override void MakeState()
    {
        State state;
        allStates = new Dictionary<string, State>();

        state = new PlayerState_Idle();
        allStates.Add(state.stateName, state);
        state = new PlayerState_Move();
        allStates.Add(state.stateName, state);

        mainState = allStates["Idle"];
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        ManagerUpdate();

        if (InputHandler.attack)
        {
            gameObject.GetComponent<Animator>().Play("Attack", 1);
        }
    }

    public override void Interrupt(string stateName)
    {
        
    }
}
