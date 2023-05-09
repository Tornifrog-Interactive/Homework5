using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TurnStoneToGrass : MonoBehaviour
{

    public bool canTurnStoneToGrass = false;

    public TileBase grassTile = null;
    
    public void turnStoneToGrass(Vector3Int pos, Tilemap tilemap)
    {
        tilemap.SetTile(pos, grassTile);
    }
}
