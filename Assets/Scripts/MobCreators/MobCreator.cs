using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MobCreator : MonoBehaviour
{
    public abstract Mob CreateMob(Vector2 mobPlace);
    protected abstract Mob InitMob(Vector2 mobPlace);
}
