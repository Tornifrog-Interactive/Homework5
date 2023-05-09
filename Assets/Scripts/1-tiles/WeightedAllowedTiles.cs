using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Tilemaps;

/**
 * This component just keeps a list of allowed tiles.
 * Such a list is used both for pathfinding and for movement.
 */
public class WeightedAllowedTiles : MonoBehaviour
{
    [SerializeField] List<TileBase> allowedTiles;
    [SerializeField] List<double> weightsPerTile;

    public bool Contain(TileBase tile) {
        return allowedTiles.Contains(tile);
    }

    public void AddAllowedTile(List<TileBase> tiles, List<double> weights)
    {
        allowedTiles = allowedTiles.Union(tiles).ToList();
        weightsPerTile = weightsPerTile.Union(weights).ToList();
    }

    public List<TileBase> GetAllowedTiles() { return allowedTiles;  }
    public List<double> GetWeightsPerTile() { return weightsPerTile;  }
}
