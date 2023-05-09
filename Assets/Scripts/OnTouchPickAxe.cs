using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnTouchPickAxe : MonoBehaviour
{
    public string playerTag = "Player";

    private void OnTriggerEnter2D(Collider2D col)
    {
        var objectInstance = col.gameObject;
        if (objectInstance.CompareTag(playerTag))
        {
            objectInstance.GetComponent<KeyboardMoverByTile>().turnStoneToGrass.canTurnStoneToGrass = true;
            Destroy(gameObject);
        }
    }
}
