using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Mob : MonoBehaviour
{
    public abstract MobCreator �reatorType { get;  set; }
    public abstract string MobType {  get; set; }

    public ParticleSystem particleSystem;

    public bool IsBlocked;
    public Color particleColor;

    
    private void OnMouseDown()
    {
        if (IsBlocked == false)
        {
            EventBus.PickMobEvent(this);
        }
        

    }
}
