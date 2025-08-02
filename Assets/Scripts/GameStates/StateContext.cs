using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateContext
{
    private State state;
    public TileMap tileMap;
    public void SwitchState(State state)
    {
        this.state.ExitState();
        this.state = state;
        state.EnterState();
       
    }
    public State GetState()
    {
    return this.state;
    }
   
    public StateContext(State state)
    {
        this.state = state;
        state.EnterState();
    }
}

