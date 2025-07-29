
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileMap : MonoBehaviour
{
    private int mapSize = 7;
    private int tileSize = 1;




    public List<MobCreator> creators;


    private void Start()
    {
        FillMap();
    }
   
 
    private GameObject CheckTile(Vector2 startPoint, Vector2 checkDirection)
    {
            Ray2D ray = new Ray2D(startPoint, checkDirection);

            RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction);

            if (hit.collider == null)
            {
                Debug.DrawRay(ray.origin, ray.direction * 20, Color.green, 0.05f);
                return null;
                
            }
            else
            {

                Debug.DrawLine(ray.origin, hit.point, Color.red, 0.05f);
                Debug.DrawRay(hit.point, ray.direction * 20f, Color.green, 0.05f);
               
            return hit.collider.gameObject;
            }
            
    }
    private void FillMap()
    {
        StartCoroutine(go());

    }

          
            
    
    IEnumerator go()
    {
        for (int y = 0; y < mapSize; y++)
        {
            for (int x = 0; x < mapSize; x++)
            {
                List<MobCreator> correctList = new List<MobCreator>();
                foreach (MobCreator creators in creators)
                {
                    correctList.Add(creators);
                }
                GameObject leftGO = CheckTile(new Vector2(x - tileSize, y), Vector2.left);
                if (leftGO != null)
                {
                    Mob leftMob = leftGO.GetComponent<Mob>();
                    correctList.Remove(leftMob.ÑreatorType);
                    
                }
                GameObject downGO = CheckTile(new Vector2(x, y - tileSize), Vector2.down);
                if (downGO != null)
                {
                    Mob downMob = downGO.GetComponent<Mob>();
                    correctList.Remove(downMob.ÑreatorType);

                }


                correctList[Random.RandomRange(0,correctList.Count)].CreateMob(new Vector2(x, y));
                
                yield return new WaitForSeconds(0.05f);
            }
        }
    }
}
   
   

