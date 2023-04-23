using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    // in Tile
    public int x;
    public int y;
    bool life = false;
    public bool thislife = false;
    public List<Tile> neighbors;
    public Color live;
    public Color dead;

    void Start()
    {
        //will run after all tiles have been generated
        neighbors = new List<Tile>();
        if (x > 0)
            neighbors.Add(TileGenerator.tileGrid[x - 1, y]);
        if (x < TileGenerator.width - 1)
            neighbors.Add(TileGenerator.tileGrid[x + 1, y]);
        if (y > 0)
        {
            neighbors.Add(TileGenerator.tileGrid[x, y - 1]);
            if (x > 0)
                neighbors.Add(TileGenerator.tileGrid[x - 1, y - 1]);
            if (x < TileGenerator.width - 1)
                neighbors.Add(TileGenerator.tileGrid[x + 1, y - 1]);
        }
        if (y < TileGenerator.width - 1)
        {
            neighbors.Add(TileGenerator.tileGrid[x, y + 1]);
            if (x > 0)
                neighbors.Add(TileGenerator.tileGrid[x - 1, y + 1]);
            if (x < TileGenerator.width - 1)
                neighbors.Add(TileGenerator.tileGrid[x + 1, y + 1]);
        }
        //and so on for all other neighbors
    }

    public void transformed()
    {
        gameObject.transform.localPosition = new Vector2(x, y);
    }

    public void cycle()
    {
        int population = 0;
        foreach (Tile neighbor in neighbors)
        {
            if (neighbor.life)
                population++;
        }
        if (!life && population == 3)
            thislife = true;
        else if (life && population >= 2 && population <= 3)
            thislife = true;
        else
            thislife = false;
    }

    public void updateLife()
    {
        SpriteRenderer sprite = GetComponent<SpriteRenderer>();
        life = thislife;
        if (life)
            sprite.color = Random.ColorHSV(0f, 1f, 0.5f, 1f, 0.5f, 1f);
        else
            sprite.color = dead;
    }

    public void giveLife()
    {
        thislife = true;
        updateLife();
    }

    void OnMouseOver()
    {
        giveLife();
    }
}
