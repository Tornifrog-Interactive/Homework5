using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Tilemaps;

/**
 * This component just keeps a list of allowed tiles.
 * Such a list is used both for pathfinding and for movement.
 */
public class AllowedTiles : MonoBehaviour  {
    [SerializeField] TileBase[] allowedTiles = null;

    public bool Contain(TileBase tile) {
        return allowedTiles.Contains(tile);
    }

    public void AddAllowedTile(IEnumerable<TileBase> tiles)
    {
        allowedTiles = allowedTiles.Union(tiles).ToArray();
    }

    public TileBase[] Get() { return allowedTiles;  }
}
