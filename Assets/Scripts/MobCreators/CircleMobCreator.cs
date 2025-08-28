using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleMobCreator : MobCreator
{
    public CircleMob circleMobPrefab;
    public override Mob CreateMob(Vector2 mobPlace)
    {
        return InitMob(mobPlace);
    }

    protected override Mob InitMob(Vector2 mobPlace)
    {
        CircleMob mob = Instantiate(circleMobPrefab, mobPlace, Quaternion.identity);
        mob.ÑreatorType = this;
        mob.IsBlocked = true;
        mob.particleColor = Color.green;
        return mob;
    }
}
