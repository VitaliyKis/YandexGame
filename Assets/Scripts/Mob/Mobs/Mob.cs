using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Mob : MonoBehaviour
{
   public abstract MobCreator ÑreatorType { get;  set; }
   public abstract string MobType {  get; set; }

    
    private void OnMouseDown()
    {
        EventBus.PickMobEvent(this);

    }
}
