using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TilemapWeightedGraph : IWeightedGraph<Vector3Int>
{
    private Tilemap tilemap;
    private Dictionary<TileBase, double> allowedTiles;

    public TilemapWeightedGraph(Tilemap tilemap, Dictionary<TileBase, double> allowedTiles)
    {
        this.tilemap = tilemap;
        this.allowedTiles = allowedTiles;
    }

    static Vector3Int[] directions =
    {
        new Vector3Int(-1, 0, 0),
        new Vector3Int(1, 0, 0),
        new Vector3Int(0, -1, 0),
        new Vector3Int(0, 1, 0),
    };

    public IEnumerable<Vector3Int> Neighbors(Vector3Int node)
    {
        foreach (var direction in directions)
        {
            Vector3Int neighborPos = node + direction;
            TileBase neighborTile = tilemap.GetTile(neighborPos);
            if (neighborTile != null && allowedTiles.Keys.Contains(neighborTile))
            {
                yield return neighborPos;
            }
        }
    }

    public double Weight(Vector3Int source, Vector3Int destination)
    {
        TileBase sourceTile = tilemap.GetTile(source);
        TileBase destinationTile = tilemap.GetTile(destination);
    
        // Check if source and destination are adjacent
        foreach (var direction in directions)
        {
            Vector3Int neighborPos = source + direction;
            if (neighborPos == destination)
            {
                TileBase neighborTile = tilemap.GetTile(neighborPos);
                if (allowedTiles.ContainsKey(neighborTile))
                {
                    return allowedTiles[neighborTile];
                }
            }
        }

        // If not adjacent, check if both tiles are allowed
        double weight = double.PositiveInfinity;
        if (allowedTiles.ContainsKey(sourceTile) && allowedTiles.ContainsKey(destinationTile))
        {
            weight = allowedTiles[destinationTile];
        }

        return weight;
    }
}