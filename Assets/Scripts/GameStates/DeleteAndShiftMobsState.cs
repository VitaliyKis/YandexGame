using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DeleteAndShiftMobsState : State
{
    public override StateContext context { get; set; }
    private bool[,] mobsToDelete;
    private StartCoroutineManager startCoroutineManager;
    private ParticleObjectPool particlePool;

    public override void EnterState()
    {
        Debug.Log("DeleteAndShiftMobs State Enter");
        DeleteMatchMobs(mobsToDelete);
        ShiftMobs();
        context.SwitchState(new SpawnAndShiftNewMobsState(context, startCoroutineManager, particlePool));
    }
    
    public void DeleteMatchMobs(bool[,] mobsToDestroy)
    {
        for (int x = 0; x < context.tileMap.mapSize; x++)
        {
            for (int y = 0; y < context.tileMap.mapSize; y++)
            {
                if (mobsToDestroy[x, y] == true)
                {
                    DeleteMobAnimation(context.tileMap.mobs[x, y], context.tileMap.mobs[x, y].transform.position);
                    context.tileMap.mobs[x, y] = null;
                    


                }
            }

        }
    }
    private void DeleteMobAnimation(Mob mob,Vector3 explosionPosition)
    {
       ObjectDestroyer.DestroyObject(mob.gameObject);
       particlePool.PlayCurrentParticle(mob.particleColor, explosionPosition);
       
    }
    public void ShiftMobs()
    {
        for (int x = 0; x < context.tileMap.mapSize; x++)
        {
            for (int y = 0; y < context.tileMap.mapSize; y++)
            {
                if (context.tileMap.mobs[x, y] == null)
                {
                    

                    int offset = 1;
                    while (offset + y < context.tileMap.mapSize && context.tileMap.mobs[x, offset + y] == null)
                    {
                        offset++;
                    }
                    if (y + offset < context.tileMap.mapSize)
                    {
                        
                        Vector3 startPos = context.tileMap.mobs[x, y + offset].gameObject.transform.position;
                        Vector3 targetPos = new Vector3(x,y,0f);

                        startCoroutineManager.PlayCurrentCoroutine(ShiftMobs(startPos, targetPos, context.tileMap.mobs[x, y + offset]));
                       
                        
                        context.tileMap.mobs[x, y] = context.tileMap.mobs[x, y + offset];
                        context.tileMap.mobs[x, y + offset] = null;
                    }


                }
            }
        }
        
    }
    private IEnumerator ShiftMobs(Vector3 startPos, Vector3 targetPos, Mob mob)
    {
        for (float i = 0f; MathOperations.SquareOperation(i) < 1; i += Time.deltaTime)
        {

            mob.gameObject.transform.position = Vector3.Lerp(startPos, targetPos, MathOperations.SquareOperation(i));
            yield return null;



        }
       
    }
    public override void ExitState()
    {
        Debug.Log("DeleteAndShiftMobs State Exit");
    }

    public DeleteAndShiftMobsState(StateContext context, bool[,] mobsToDelete, StartCoroutineManager startCoroutineManager, ParticleObjectPool particlePool)
    {
        this.context = context;
        this.mobsToDelete = mobsToDelete;
        this.startCoroutineManager = startCoroutineManager;
        this.particlePool = particlePool;
    }
}
