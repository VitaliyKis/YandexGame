using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectDestroyer : MonoBehaviour
{
    public static void DestroyObject(GameObject mobToDestroy)
    {
        Destroy(mobToDestroy);
    }
}
