using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhoulSM : StateManager
{
    public bool foundPlayer { get; private set; }

    public Collider headIdle;
    public Collider headWalk;

    public override void MakeState()
    {
        State state;
        allStates = new Dictionary<string, State>();

        state = new GhoulState_Idle();
        allStates.Add(state.stateName, state);

        state = new GhoulState_Move();
        allStates.Add(state.stateName, state);

        mainState = allStates["Idle"];
    }

    private void Awake()
    {
        ManagerStart();
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
        if(stateName == "EyeEnter")
        {
            foundPlayer = true;
        }
        else if(stateName == "EyeExit")
        {
            foundPlayer = false;
        }
    }
}
