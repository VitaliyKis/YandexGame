using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HexagonMob : Mob
{
    public override MobCreator ÑreatorType { get; set; }

    public override string MobType { get; set; }
    public HexagonMob()
    {
        MobType = "HexagonMob";
    }

}
