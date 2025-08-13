using System.Collections.Generic;
using UnityEngine;

public class TileMap : MonoBehaviour
{
    public int mapSize;
    public int tileSize = 1;

    public List<MobCreator> creators;
    public TileSpawner spawner;
    public StateContext stateContext;
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
        
        stateContext = new StateContext(new IddleState(stateContext));
        stateContext.tileMap = this;
    }
    private void PickMob(Mob pickedMob)
    {

        if (stateContext.GetState() is PickedMobState)
        {
            //Проверка на тот же слот
            if (pickedMob.gameObject == selectedMob.gameObject)
            {
                PickedMobState currentState = (PickedMobState)stateContext.GetState();
                currentState.ExitPickedState();
                stateContext.SwitchState(new IddleState(stateContext));
                ShowAllTiles();

            }
            //Проверка на соседей
            for (int i = 0; i < neighboursList.Count; i++)
            {
                if (pickedMob.gameObject == neighboursList[i])
                {

                    PickedMobState currentState = (PickedMobState)stateContext.GetState();
                    currentState.ExitPickedState();
                    stateContext.SwitchState(new IddleState(stateContext));

                    ReplaceGameObjects(selectedMob,pickedMob);
                    bool IsCombosDeleted;
                    do
                    {
                        IsCombosDeleted = DeleteMatch();
                        if (IsCombosDeleted)
                        {
                            ShiftTiles();
                            FillEmptyPlaces();
                        }
                    }
                    while (IsCombosDeleted == true);
                    ShowAllTiles();
                    break;
                }
                
            }

        
        }
        
        else
        {
            selectedMob = pickedMob;
            PickedMobState currentState = new PickedMobState(stateContext);
            stateContext.SwitchState(currentState);
            neighboursList = currentState.EnterPickedState(pickedMob);
            HideOtherTiles(neighboursList);
        }
    }
    public void FillEmptyPlaces()
    {
        
        int y = mapSize - 1;
        for (int x = 0; x < mapSize; x++)
        {
            if (mobs[x, mapSize - 1] == null)
            {
           
                int offsetY = 0;
                while (y - offsetY >= 0 && mobs[x, y - offsetY] == null)
                {
                    offsetY++;
                    
                }
                if (y - offsetY >= 0)
                {
                    offsetY = offsetY - 1;
                    while (offsetY >= 0)
                    {
                        mobs[x, y - offsetY] = creators[Random.Range(0, creators.Count - 1)].CreateMob(new Vector2(x, y - offsetY));
                        offsetY--;
                    }
                }
            }
        }
    }
    public bool DeleteMatch()
    {
        bool IsFinded = false;
        bool[,] toRemove = new bool[mapSize,mapSize]; // без инициализации каждая переменная равна false

        for (int x = 0; x < mapSize; x++)
        {
            for (int y = 0; y < mapSize; y++)
            {
               
              
               int runLenX = 1;
                
                while (x + runLenX < mapSize && mobs[x + runLenX, y].MobType == mobs[x, y].MobType)//совпадение по оси x
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

                while (y + runLenY < mapSize && mobs[x, y + runLenY].MobType == mobs[x, y].MobType)//совпадение по оси y
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
            return false;
        }
        else
        {
            RemoveMatchTiles(toRemove);
            return true;
        }
    }

    public void RemoveMatchTiles(bool[,] matchTiles)
    {
        for (int x = 0; x < mapSize; x++)
        {
           for (int y = 0; y < mapSize; y++)
            {
                if (matchTiles[x, y] == true)
                {
                    if (mobs[x,y] != null) // Потом удалить, так как клетки будут заполнятся
                    {
                        Destroy(mobs[x, y].gameObject);
                        mobs[x,y] = null;  
                    }
                    
                    
                }
            }

        }
    }



    public void ShiftTiles()
    {

        for (int x = 0; x < mapSize; x++)
        {
            for (int y = 0; y < mapSize ; y++)
            {
                if (mobs[x, y] == null)
                {
                  
                    int offset = 1;
                    while (offset + y < mapSize && mobs[x, offset + y] == null)
                    {
                        offset++;
                    }
                    if (y + offset < mapSize)
                    {
                        mobs[x, y + offset].transform.position = new Vector3(x, y, 0f);
                        mobs[x, y] = mobs[x, y + offset];
                        mobs[x, y + offset] = null;
                    }

                    
                }
            }
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
   
   

