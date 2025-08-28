using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobDeathParticle : MonoBehaviour
{
    public ParticleSystem particleSystem;
    public ParticleSystem PlayParticle(Color color,Vector3 explosionPosition)
    {
        particleSystem.startColor = color;
        this.gameObject.transform.position = explosionPosition;
        particleSystem.Play();
        return particleSystem;
        
    }
}
