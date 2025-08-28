using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
public class StartCoroutineManager : MonoBehaviour
{
    public void PlayCurrentCoroutine(IEnumerator coroutine)
    {
        StartCoroutine(coroutine);
    }
    public void PlayCurrentCoroutine(string coroutineName)
    {
        StartCoroutine(coroutineName);
    }
    
}
