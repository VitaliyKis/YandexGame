using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleMob : Mob
{

    public override MobCreator �reatorType { get; set; }
    public override string MobType { get; set; }
    public CircleMob()
    {
        MobType = "CircleMob";
    }
}
