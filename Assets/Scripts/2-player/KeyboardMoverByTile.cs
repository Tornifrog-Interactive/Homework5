using UnityEngine;
using UnityEngine.Tilemaps;

/**
 * This component allows the player to move by clicking the arrow keys,
 * but only if the new position is on an allowed tile.
 */
public class KeyboardMoverByTile: KeyboardMover {
    [SerializeField] Tilemap tilemap = null;
    public AllowedTiles allowedTiles = null;
    public TileBase stoneTile = null;
    public TurnStoneToGrass turnStoneToGrass = null;

    private TileBase TileOnPosition(Vector3 worldPosition) {
        Vector3Int cellPosition = tilemap.WorldToCell(worldPosition);
        return tilemap.GetTile(cellPosition);
    }

    void Update()  {
        Vector3 newPosition = NewPosition();
        TileBase tileOnNewPosition = TileOnPosition(newPosition);
        if (tileOnNewPosition == stoneTile && turnStoneToGrass.canTurnStoneToGrass)
        {
            turnStoneToGrass.turnStoneToGrass(tilemap.WorldToCell(newPosition), tilemap);
        }
        if (allowedTiles.Contain(tileOnNewPosition)) {
            transform.position = newPosition;
        } else {
            Debug.Log("You cannot walk on " + tileOnNewPosition + "!");
        }
    }
}
