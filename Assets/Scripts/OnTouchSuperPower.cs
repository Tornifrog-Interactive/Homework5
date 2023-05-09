using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class OnTouchSuperPower : MonoBehaviour
{
    public string playerTag = "Player";
    public TileBase[] tilesToAdd = null;

    private void OnTriggerEnter2D(Collider2D col)
    {
        var objectInstance = col.gameObject;
        if (objectInstance.CompareTag(playerTag))
        {
            objectInstance.GetComponent<KeyboardMoverByTile>().allowedTiles.AddAllowedTile(tilesToAdd);
            Destroy(gameObject);
        }
    }
}