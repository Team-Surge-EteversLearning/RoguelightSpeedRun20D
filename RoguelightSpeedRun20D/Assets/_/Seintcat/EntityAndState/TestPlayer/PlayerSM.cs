using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerSM : StateManager
{
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

        nowState = allStates["Idle"];
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
