using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeleteAndShiftMobsState : State
{
    public override StateContext context { get; set; }
    private bool[,] mobsToDelete;

    public override void EnterState()
    {
        Debug.Log("DeleteAndShiftMobs State Enter");
        DeleteMatchMobs(mobsToDelete);
        ShiftMobs();
        context.SwitchState(new SpawnNewMobsState(context));
    }
    public void DeleteMatchMobs(bool[,] mobsToDestroy)
    {
        for (int x = 0; x < context.tileMap.mapSize; x++)
        {
            for (int y = 0; y < context.tileMap.mapSize; y++)
            {
                if (mobsToDestroy[x, y] == true)
                {
                    ObjectDestroyer.DestroyObject(context.tileMap.mobs[x, y].gameObject);
                    context.tileMap.mobs[x, y] = null;
                    


                }
            }

        }
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
                        context.tileMap.mobs[x, y + offset].transform.position = new Vector3(x, y, 0f);
                        context.tileMap.mobs[x, y] = context.tileMap.mobs[x, y + offset];
                        context.tileMap.mobs[x, y + offset] = null;
                    }


                }
            }
        }
    }

    public override void ExitState()
    {
        Debug.Log("DeleteAndShiftMobs State Exit");
    }

    public DeleteAndShiftMobsState(StateContext context, bool[,] mobsToDelete)
    {
        this.context = context;
        this.mobsToDelete = mobsToDelete;
    }
}
