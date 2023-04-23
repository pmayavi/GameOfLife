using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileGenerator : MonoBehaviour
{
    // imagine this is in TileGenerator.cs
    public static Tile[,] tileGrid;
    public static int width = 100;
    public static int height = 100; //these are static so that Tile can easily access them
    public GameObject tilePrefab;
    public float time;
    public bool auto = false;

    void Awake()
    {
        tileGrid = new Tile[width, height];
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                GameObject newTileGO = Instantiate(tilePrefab);
                Tile newTile = newTileGO.GetComponent<Tile>();
                tileGrid[x, y] = newTile;
                newTile.x = x;
                newTile.y = y;
                newTile.transformed();
            }
        }
        tileGrid[50, 50].giveLife();
        tileGrid[50, 48].giveLife();
        tileGrid[51, 49].giveLife();
        tileGrid[51, 48].giveLife();
        tileGrid[52, 49].giveLife();
        if (auto)
            Invoke("cycle", time);
    }

    void cycle()
    {
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                tileGrid[x, y].cycle();
            }
        }
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                tileGrid[x, y].updateLife();
            }
        }
        if (auto)
            Invoke("cycle", time);
    }

    void OnMouseDown()
    {
        auto = !auto;
        if (auto)
            Invoke("cycle", time);
    }
}
