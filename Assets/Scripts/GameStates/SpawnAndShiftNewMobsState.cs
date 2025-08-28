using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnAndShiftNewMobsState : State
{
    public override StateContext context { get; set; }
    private StartCoroutineManager startCoroutineManager;
    ParticleObjectPool particlePool;

    private int mobToFall;
    private int mobFelted;


    public override void EnterState()
    {
        Debug.Log("SpawnNewMobs State Enter");
        startCoroutineManager.PlayCurrentCoroutine(SpawnAndShiftNewMobs());
    }
    public IEnumerator SpawnAndShiftNewMobs()
    {
        int y = context.tileMap.mapSize - 1;
        for (int x = 0; x < context.tileMap.mapSize; x++)
        {
            if (context.tileMap.mobs[x, context.tileMap.mapSize - 1] == null)
            {

                int offsetY = 0;
                while (y - offsetY >= 0 && context.tileMap.mobs[x, y - offsetY] == null)
                {
                    offsetY++;

                }
                if (y - offsetY >= 0)
                {
                    offsetY = offsetY - 1;
                    int spawnOffset = 0;
                    while (offsetY >= 0)
                    {
                        Vector3 startPos = new Vector3(x,context.tileMap.mapSize + spawnOffset,0f);
                        context.tileMap.mobs[x, y - offsetY] = context.tileMap.creators[Random.Range(0, context.tileMap.creators.Count - 1)].CreateMob(startPos);
                        mobToFall++;
                        //Animation
                        Vector3 pointPos = new Vector3(x, y - offsetY, 0f);
                        startCoroutineManager.PlayCurrentCoroutine(ShiftMobs(startPos, pointPos, context.tileMap.mobs[x, y - offsetY]));
                        //
                        offsetY--;
                        spawnOffset++;
                    }
                }
            }
        }
        while (mobFelted != mobToFall)
        {
            yield return null;
        }
        yield return new WaitForSeconds(0.1f);
        context.SwitchState(new SearchingMatchState(context, startCoroutineManager,particlePool));
    }
    private IEnumerator ShiftMobs(Vector3 startPos, Vector3 targetPos, Mob mob)
    {
        for (float i = 0f; MathOperations.SquareOperation(i) < 1; i += Time.deltaTime)
        {
            mob.gameObject.transform.position = Vector3.Lerp(startPos, targetPos, MathOperations.SquareOperation(i));
            yield return null;

        }
        mobFelted++;
    }
    public override void ExitState()
    {
        Debug.Log("SpawnNewMobs State Exit");
    }

    public SpawnAndShiftNewMobsState(StateContext context,StartCoroutineManager startCoroutineManager, ParticleObjectPool particlePool)
    {
        this.context = context;
        this.startCoroutineManager = startCoroutineManager;
        this.particlePool = particlePool;
       
    }
}
