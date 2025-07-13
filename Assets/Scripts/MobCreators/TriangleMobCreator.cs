using System.Collections;
using System.Collections.Generic;
using UnityEditor.iOS.Xcode;
using UnityEngine;

public class TriangleMobCreator : MobCreator
{
    public TriangleMob triangleMobPrefab;
    public override Mob CreateMob(Vector2 mobPlace)
    {
        Debug.Log("Создан треугольник");
        return InitMob(mobPlace);
    }

    protected override Mob InitMob(Vector2 mobPlace)
    {
        TriangleMob mob = Instantiate(triangleMobPrefab,mobPlace,Quaternion.identity);
        mob.Сreator = this;
        Debug.Log(mob.Сreator);
        Debug.Log("------------------------");
        return mob;
    }
}
