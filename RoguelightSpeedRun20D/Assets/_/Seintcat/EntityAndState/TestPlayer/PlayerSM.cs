using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerSM : StateManager
{
    public static readonly float moveSpeed = 1f;
    public static readonly float runSpeed = 1f;

    [SerializeField]
    private GameObject weaponHanger;
    [SerializeField]
    private List<GameObject> weaponModels;

    private List<GameObject> weaponInstance;
    private GameObject weaponModelNow;

    private Weapon _weaponNow;
    public Weapon weaponNow
    {
        set 
        { 
            _weaponNow = value; 
        }
    }
    private Armor _armorNow;
    public Armor armorNow
    {
        set
        {
            _armorNow = value;
        }
    }
    private Shoes _shoesNow;
    public Shoes shoesNow
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
    }

    public override void Interrupt(string stateName)
    {
        
    }
}
