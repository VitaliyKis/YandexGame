
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileMap : MonoBehaviour
{
    private int mapSize = 7;
    private int tileSize = 1;




    public List<Mob> mobs;
    public List<MobCreator> creators;

    public TriangleMobCreator triangleMobCreator;

    private void Start()
    {
        triangleMobCreator.CreateMob(Vector2.zero);
    }
 
    private GameObject CheckTile(Vector2 startPoint, Vector2 checkDirection)
    {
            Ray2D ray = new Ray2D(startPoint, checkDirection);

            RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction);

            if (hit.collider == null)
            {
                Debug.DrawRay(ray.origin, ray.direction * 20, Color.green, 10000f);
                return null;
                
            }
            else
            {

                Debug.DrawLine(ray.origin, hit.point, Color.red, 10000f);
                Debug.DrawRay(hit.point, ray.direction * 20f, Color.green, 10000f);
                
                return hit.collider.gameObject;
            }
            
    }
    private void FillMap()
    {
        
       



               StartCoroutine(go());


                //List<Mob> correctNeighbours = mobs;

                ////GameObject downNeighbour = CheckTile(new Vector2(x,y - tileSize), Vector2.down); // Проверяем низ
                ////if (downNeighbour != null)
                ////{
                ////    string downMobName = downNeighbour.GetComponent<Mob>().GetType().Name;

                ////    for (int i = 0; i < correctNeighbours.Count; i++)
                ////    {
                ////        if (correctNeighbours[i].GetType().Name == downMobName)
                ////        {
                ////            correctNeighbours.RemoveAt(i);
                ////        }
                ////    }


                ////}


                //Debug.Log(mobs.Count);
                //Debug.Log("--------------------------------------");
                //GameObject leftNeighbour = CheckTile(new Vector2(x - tileSize, y), Vector2.left); // Проверяем лево
                //if (leftNeighbour != null)
                //{
                //    string leftMobName = leftNeighbour.GetComponent<Mob>().GetType().Name;

                //    for (int i = 0; i < correctNeighbours.Count; i++)
                //    {
                //        if (correctNeighbours[i].GetType().Name == leftMobName)
                //        {
                //            correctNeighbours.RemoveAt(i);
                //        }
                //    }
                //    Debug.Log(mobs.Count);

                //}

                //int index = UnityEngine.Random.Range(0, correctNeighbours.Count - 1);

                //Instantiate(correctNeighbours[index], new Vector2(x, y), Quaternion.identity);


                //Debug.Log("NEXT");


            
        
    }

          
            
    
    IEnumerator go()
    {
        for (int y = 0; y < mapSize; y++)
        {
            for (int x = 0; x < mapSize; x++)
            {
                GameObject leftGO = CheckTile(new Vector2(x - tileSize, y), Vector2.left);
                if (leftGO != null)
                {
                    Mob leftMob = leftGO.GetComponent<Mob>();
                    
                }
                GameObject downGO = CheckTile(new Vector2(x, y - tileSize), Vector2.down);
                
                yield return new WaitForSeconds(0.1f);
            }
        }
    }
}
   
   

