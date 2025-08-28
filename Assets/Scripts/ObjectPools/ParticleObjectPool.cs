using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.ParticleSystem;

public class ParticleObjectPool : MonoBehaviour
{
    private List<MobDeathParticle> mobDeathParticleList = new List<MobDeathParticle>();
    public MobDeathParticle MobDeathParticlePrefab;



    public void Start()
    {
        for (int i = 0; i < 3; i++)
        {
           
           mobDeathParticleList.Add(Instantiate(MobDeathParticlePrefab));
        }
    }
    public ParticleSystem PlayCurrentParticle(Color color, Vector3 explosionPosition)
    {
        Debug.LogWarning("+++++++++++++++++++");
        MobDeathParticle mobDeathParticle = null;
        ParticleSystem particleSystem;
            Debug.LogWarning(mobDeathParticleList.Count);

        foreach (MobDeathParticle particle in mobDeathParticleList)
        {
            mobDeathParticle = particle;
            break;
        }
        if (mobDeathParticle == null)
        {
            Debug.LogWarning("========================");

            mobDeathParticle = Instantiate(MobDeathParticlePrefab);
            particleSystem = mobDeathParticle.PlayParticle(color, explosionPosition);
            StartCoroutine(WaitUntilParticleEnd(mobDeathParticle));
        }
        else
        {
            particleSystem = mobDeathParticle.PlayParticle(color, explosionPosition);
            mobDeathParticleList.Remove(mobDeathParticle);
            StartCoroutine(WaitUntilParticleEnd(mobDeathParticle));
        }
        return particleSystem;
    }
    public void ReturnParticleToList(MobDeathParticle mobDeathParticle)
    {
        mobDeathParticleList.Add(mobDeathParticle);
    }
    private IEnumerator WaitUntilParticleEnd(MobDeathParticle particleSystem)
    {
        
        while (particleSystem.particleSystem.IsAlive() == true)
        {
            yield return null;
        }

        ReturnParticleToList(particleSystem);
    }




}
