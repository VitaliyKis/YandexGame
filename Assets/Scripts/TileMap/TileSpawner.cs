using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileSpawner : MonoBehaviour
{
    public TileMap tileMap;
    public void Spawn()
    {
        for (int y = 0; y < tileMap.mapSize; y++)
        {
            for (int x = 0; x < tileMap.mapSize; x++)
            {
                List<MobCreator> correctList = new List<MobCreator>();
                foreach (MobCreator creators in tileMap.creators)
                {
                    correctList.Add(creators);
                }
                GameObject leftGO = tileMap.CheckTile(new Vector2(x - tileMap.tileSize, y), Vector2.left);
                if (leftGO != null)
                {
                    Mob leftMob = leftGO.GetComponent<Mob>();
                    correctList.Remove(leftMob.ÑreatorType);

                }
                GameObject downGO = tileMap.CheckTile(new Vector2(x, y - tileMap.tileSize), Vector2.down);
                if (downGO != null)
                {
                    Mob downMob = downGO.GetComponent<Mob>();
                    correctList.Remove(downMob.ÑreatorType);

                }


                tileMap.mobs[x,y] = correctList[Random.RandomRange(0, correctList.Count)].CreateMob(new Vector2(x, y));

            }
        }
    }
}
