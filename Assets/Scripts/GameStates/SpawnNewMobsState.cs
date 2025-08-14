using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnNewMobsState : State
{
    public override StateContext context { get; set; }
   

    public override void EnterState()
    {
        Debug.Log("SpawnNewMobs State Enter");
        SpawnNewMobs();
        context.SwitchState(new SearchingMatchState(context));
        
    }
    public void SpawnNewMobs()
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
                    while (offsetY >= 0)
                    {
                        context.tileMap.mobs[x, y - offsetY] = context.tileMap.creators[Random.Range(0, context.tileMap.creators.Count - 1)].CreateMob(new Vector2(x, y - offsetY));
                        offsetY--;
                    }
                }
            }
        }
    }
    

    public override void ExitState()
    {
        Debug.Log("SpawnNewMobs State Exit");
    }

    public SpawnNewMobsState(StateContext context)
    {
        this.context = context;
       
    }
}
