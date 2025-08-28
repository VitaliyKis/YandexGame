using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HexagonMobCreator : MobCreator
{
    public HexagonMob hexagonMobPrefab;
    public override Mob CreateMob(Vector2 mobPlace)
    {
        return InitMob(mobPlace);
    }

    protected override Mob InitMob(Vector2 mobPlace)
    {
        HexagonMob mob = Instantiate(hexagonMobPrefab, mobPlace, Quaternion.identity);
        mob.ÑreatorType = this;
        mob.IsBlocked = true;
        mob.particleColor = Color.yellow;

        return mob;
    }
}

