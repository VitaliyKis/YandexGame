using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiamondDeathParticle : MonoBehaviour
{
    public void PlayParticle()
    {
        this.GetComponent<ParticleSystem>().Play();
    }
}
