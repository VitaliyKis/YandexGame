using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SearchingMatchState : State
{
    public override StateContext context { get; set; }
    private StartCoroutineManager startCoroutineManager;
    ParticleObjectPool particlePool;

    public override void EnterState()
    {
        context.tileMap.BlockAllTilesToTouch();
        Debug.Log("SearchingMatch State Enter");
        FindMatchMobs();
    }
    public void FindMatchMobs()
    {
        bool IsFinded = false;
        bool[,] toRemove = new bool[context.tileMap.mapSize, context.tileMap.mapSize];

        for (int x = 0; x < context.tileMap.mapSize; x++)
        {
            for (int y = 0; y < context.tileMap.mapSize; y++)
            {


                int runLenX = 1;

                while (x + runLenX < context.tileMap.mapSize && context.tileMap.mobs[x + runLenX, y].MobType == context.tileMap.mobs[x, y].MobType)//совпадение по оси x
                {
                    runLenX++;
                }
                if (runLenX >= 3)
                {
                    for (int k = 0; k < runLenX; k++)
                    {
                        IsFinded = true;
                        toRemove[x + k, y] = true;
                    }
                }
                int runLenY = 1;

                while (y + runLenY < context.tileMap.mapSize && context.tileMap.mobs[x, y + runLenY].MobType == context.tileMap.mobs[x, y].MobType)//совпадение по оси y
                {
                    runLenY++;
                }
                if (runLenY >= 3)
                {
                    for (int k = 0; k < runLenY; k++)
                    {
                        IsFinded = true;
                        toRemove[x, y + k] = true;
                    }
                }

            }
        }
        if (IsFinded == false)
        {
            context.SwitchState(new IddleState(context));
            
        }
        else
        {
            context.SwitchState(new DeleteAndShiftMobsState(context, toRemove,startCoroutineManager,particlePool));


        }
    }

    public override void ExitState()
    {
        Debug.Log("SearchingMatch State State Exit");
    }

    public SearchingMatchState(StateContext context, StartCoroutineManager startCoroutineManager, ParticleObjectPool particlePool)
    {
        this.context = context;
        this.startCoroutineManager = startCoroutineManager;
        this.particlePool = particlePool;
    }
}
