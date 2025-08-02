using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class State
{
    public abstract StateContext context { get; set; }
    public virtual void EnterState()
    {
    }
    public virtual void ExitState()
    {

    }


}
