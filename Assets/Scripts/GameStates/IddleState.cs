using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class IddleState : State
{
    public override StateContext context { get; set; }

    public override void EnterState()
    {
        Debug.Log("Iddle State Enter");
    }

    public override void ExitState()
    {
        Debug.Log("Iddle State Exit");
    }

    public IddleState(StateContext context)
    {
        this.context = context; 
    }

}
