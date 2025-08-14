using System.Collections.Generic;
using UnityEngine;

public class TileMap : MonoBehaviour
{
    public int mapSize;
    public int tileSize = 1;

    public List<MobCreator> creators;
    public TileSpawner spawner;
    public StateContext context;
    public Mob[,] mobs;

    private List<GameObject> neighboursList;
    private Mob selectedMob;
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
        
        context = new StateContext(new IddleState(context));
        context.tileMap = this;
    }
    private void PickMob(Mob pickedMob)
    {

        if (context.GetState() is PickedMobState)
        {
            //Проверка на тот же слот
            if (pickedMob.gameObject == selectedMob.gameObject)
            {
                PickedMobState currentState = (PickedMobState)context.GetState();
                currentState.ExitPickedState();
                context.SwitchState(new IddleState(context));
                ShowAllTiles();

            }
            //Проверка на соседей
            for (int i = 0; i < neighboursList.Count; i++)
            {
                if (pickedMob.gameObject == neighboursList[i])
                {

                    PickedMobState currentState = (PickedMobState)context.GetState();
                    currentState.ExitPickedState();
                    ReplaceGameObjects(selectedMob,pickedMob);
                    context.SwitchState(new SearchingMatchState(context));
                    ShowAllTiles();
                    break;
                }
                
            }

        
        }
        
        else
        {
            selectedMob = pickedMob;
            PickedMobState currentState = new PickedMobState(context);
            context.SwitchState(currentState);
            neighboursList = currentState.EnterPickedState(pickedMob);
            HideOtherTiles(neighboursList);
        }
    }
    
    private void ReplaceGameObjects(Mob firstMob,Mob secondMob)
    {

        mobs[(int)firstMob.gameObject.transform.position.x, (int)firstMob.gameObject.transform.position.y] = secondMob;
        mobs[(int)secondMob.gameObject.transform.position.x, (int)secondMob.gameObject.transform.position.y] = firstMob;
      



        Vector3 firstPos = firstMob.gameObject.transform.position;

        firstMob.gameObject.transform.position = secondMob.gameObject.transform.position;
        secondMob.gameObject.transform.position = firstPos;

    }
    private void HideOtherTiles(List<GameObject> neighbours)
    {
        for (int y = 0; y < mapSize; y++)
        {
            for (int x = 0; x < mapSize; x++)
            {
                if (mobs[x, y] != null)
                {

                SpriteRenderer spriteRenderer = mobs[x, y].gameObject.GetComponent<SpriteRenderer>();
                spriteRenderer.material.color = new Color(spriteRenderer.material.color.r, spriteRenderer.material.color.g, spriteRenderer.material.color.b, 0.5f);
                
                }
            }
        }
        foreach (GameObject mob in neighbours)
        {
            if (mob != null)
            {
                SpriteRenderer spriteRenderer = mob.gameObject.GetComponent<SpriteRenderer>();
                spriteRenderer.material.color = new Color(spriteRenderer.material.color.r, spriteRenderer.material.color.g, spriteRenderer.material.color.b, 1f);

            }
        }
        SpriteRenderer spriteRendere = selectedMob.gameObject.GetComponent<SpriteRenderer>();
        spriteRendere.material.color = new Color(spriteRendere.material.color.r, spriteRendere.material.color.g, spriteRendere.material.color.b, 1f);



    }
    private void ShowAllTiles()
    {
        for (int y = 0; y < mapSize; y++)
        {
            for (int x = 0; x < mapSize; x++)
            {
                if (mobs[x, y] != null)
                {
                    SpriteRenderer spriteRenderer = mobs[x, y].gameObject.GetComponent<SpriteRenderer>();
                    spriteRenderer.material.color = new Color(spriteRenderer.material.color.r, spriteRenderer.material.color.g, spriteRenderer.material.color.b, 1f);
                }
            }
        }
    }

    public GameObject CheckTile(Vector2 startPoint, Vector2 checkDirection)
    {
            Ray2D ray = new Ray2D(startPoint, checkDirection);

            RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction);

            if (hit.collider == null)
            {
                //Debug.DrawRay(ray.origin, ray.direction * 20, Color.green, 1f);
                return null;
                
            }
            else
            {

                //Debug.DrawLine(ray.origin, hit.point, Color.red, 1f);
                //Debug.DrawRay(hit.point, ray.direction * 20f, Color.green, 1f);
               
            return hit.collider.gameObject;
            }
            
    }
    private void FillMap()
    {
        spawner.Spawn();

    }
    




}
   
   

