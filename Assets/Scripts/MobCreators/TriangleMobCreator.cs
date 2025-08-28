using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.iOS.Xcode;
using UnityEngine;

public class TriangleMobCreator : MobCreator
{

    public TriangleMob triangleMobPrefab;
    public override Mob CreateMob(Vector2 mobPlace)
    {
        return InitMob(mobPlace);
    }

    protected override Mob InitMob(Vector2 mobPlace)
    {
        TriangleMob mob = Instantiate(triangleMobPrefab,mobPlace,Quaternion.identity);
        mob.ÑreatorType = this;
        mob.IsBlocked = true;
        mob.particleColor = Color.red;

        return mob;
    }
}
