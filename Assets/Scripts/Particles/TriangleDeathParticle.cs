using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriangleDeathParticle : MonoBehaviour
{
    public void PlayParticle()
    {
        this.GetComponent<ParticleSystem>().Play();
    }
}
