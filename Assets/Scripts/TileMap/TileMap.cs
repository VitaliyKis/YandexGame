using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileMap : MonoBehaviour
{
    private Tile[,] map;
    private int size = 7;
    private void Start()
    { 
        map = new Tile[size,size];
    }

}
