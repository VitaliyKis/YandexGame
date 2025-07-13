using System.Collections;
using System.Collections.Generic;
using UnityEditor.iOS.Xcode;
using UnityEngine;

public class TriangleMobCreator : MobCreator
{
    public TriangleMob triangleMobPrefab;
    public override Mob CreateMob(Vector2 mobPlace)
    {
        Debug.Log("������ �����������");
        return InitMob(mobPlace);
    }

    protected override Mob InitMob(Vector2 mobPlace)
    {
        TriangleMob mob = Instantiate(triangleMobPrefab,mobPlace,Quaternion.identity);
        mob.�reator = this;
        Debug.Log(mob.�reator);
        Debug.Log("------------------------");
        return mob;
    }
}
