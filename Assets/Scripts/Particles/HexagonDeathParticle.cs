using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HexagonDeathParticle : MonoBehaviour
{
    public void PlayParticle()
    {
        this.GetComponent<ParticleSystem>().Play();
    }
}
