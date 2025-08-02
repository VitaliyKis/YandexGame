using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickedMobState : State
{
    public override StateContext context { get; set; }

    public List<GameObject> EnterPickedState(Mob pickedMob)
    {
        List<GameObject> result = new List<GameObject>();
        GameObject downGO = context.tileMap.CheckTile(new Vector2(pickedMob.transform.position.x, pickedMob.transform.position.y - context.tileMap.tileSize), Vector2.down);
        GameObject upGO = context.tileMap.CheckTile(new Vector2(pickedMob.transform.position.x, pickedMob.transform.position.y + context.tileMap.tileSize), Vector2.up);
        GameObject leftGO = context.tileMap.CheckTile(new Vector2(pickedMob.transform.position.x - context.tileMap.tileSize, pickedMob.transform.position.y), Vector2.left);
        GameObject rightG = context.tileMap.CheckTile(new Vector2(pickedMob.transform.position.x + context.tileMap.tileSize, pickedMob.transform.position.y), Vector2.right);
        result.Add(downGO);
        result.Add(upGO);
        result.Add(leftGO);
        result.Add(rightG);
        Debug.Log("Picked State Enter");
        return result;

    }
    public void ExitPickedState()
    {
        Debug.Log("Picked State Exit");
    }
    public PickedMobState(StateContext context)
    {
        this.context = context; 
    }
}
