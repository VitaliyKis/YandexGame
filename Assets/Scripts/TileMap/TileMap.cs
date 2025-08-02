
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileMap : MonoBehaviour
{
    public int mapSize = 7;
    public int tileSize = 1;

    public List<MobCreator> creators;
    public TileSpawner spawner;
    public StateContext stateContext;
    public Mob[,] mobs;

    private List<GameObject> neighboursList;
    private void Awake()
    {
        EventBus.PickMobEvent += PickMob;
    }
    private void OnDestroy()
    {
        EventBus.PickMobEvent -= PickMob;
    }
    private void Start()
    {
        mobs = new Mob[mapSize, mapSize];
        FillMap();
        StartStateMachine();
    }

    private void StartStateMachine()
    {
        
        stateContext = new StateContext(new IddleState(stateContext));
        stateContext.tileMap = this;
    }
    private void PickMob(Mob pickedMob)
    {
        if (stateContext.GetState() is PickedMobState)
        {
            for (int i = 0; i < neighboursList.Count; i++)
            {
                if (pickedMob.gameObject == neighboursList[i])
                {
                    PickedMobState currentState = (PickedMobState)stateContext.GetState();
                    currentState.ExitPickedState();
                    stateContext.SwitchState(new IddleState(stateContext));

                    //дописать свап плиток

                    ShowAllTiles();
                }
            }
            
        }
        else
        {
            PickedMobState currentState = new PickedMobState(stateContext);
            stateContext.SwitchState(currentState);
            neighboursList = currentState.EnterPickedState(pickedMob);
            HideOtherTiles(neighboursList);
        }
    }
    private void HideOtherTiles(List<GameObject> neighbours)
    {
        for (int y = 0; y < mapSize; y++)
        {
            for (int x = 0; x < mapSize; x++)
            {
                mobs[x,y].gameObject.SetActive(false); 
            }
        }
        foreach (GameObject mob in neighbours)
        {
            if (mob != null)
            {
                mob.SetActive(true);
            }
        }
    }
    private void ShowAllTiles()
    {
        for (int y = 0; y < mapSize; y++)
        {
            for (int x = 0; x < mapSize; x++)
            {

                mobs[x,y].gameObject.SetActive(true); 
            }
        }
    }

    public GameObject CheckTile(Vector2 startPoint, Vector2 checkDirection)
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
        spawner.Spawn();

    }
    




}
   
   

