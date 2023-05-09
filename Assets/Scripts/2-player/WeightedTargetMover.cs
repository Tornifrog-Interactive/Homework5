using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Tilemaps;

/**
 * This component moves its object towards a given target position.
 */
public class WeightedTargetMover: MonoBehaviour {
    [SerializeField] Tilemap tilemap = null;
    [SerializeField] WeightedAllowedTiles allowedTiles = null;

    [Tooltip("The speed by which the object moves towards the target, in meters (=grid units) per second")]
    [SerializeField] float speed = 2f;

    [Tooltip("The target position in world coordinates")]
    [SerializeField] Vector3 targetInWorld;

    [Tooltip("The target position in grid coordinates")]
    [SerializeField] Vector3Int targetInGrid;

    protected bool atTarget;  // This property is set to "true" whenever the object has already found the target.

    public void SetTarget(Vector3 newTarget) {
        if (targetInWorld != newTarget) {
            targetInWorld = newTarget;
            targetInGrid = tilemap.WorldToCell(targetInWorld);
            atTarget = false;
        }
    }

    public Vector3 GetTarget() {
        return targetInWorld;
    }

    private TilemapWeightedGraph tilemapGraph = null;
    private float timeBetweenSteps;

    protected virtual void Start() {
        Dictionary<TileBase, double> allowedTilesWithWeights = new Dictionary<TileBase, double>();
        for (int i = 0; i < allowedTiles.GetAllowedTiles().Count; i++)
        {
            allowedTilesWithWeights[allowedTiles.GetAllowedTiles()[i]] = allowedTiles.GetWeightsPerTile()[i];
        }
        tilemapGraph = new TilemapWeightedGraph(tilemap, allowedTilesWithWeights);
        timeBetweenSteps = 1 / speed;
        StartCoroutine(MoveTowardsTheTarget());
    }

    IEnumerator MoveTowardsTheTarget() {
        for(;;) {
            yield return new WaitForSeconds(timeBetweenSteps);
            if (enabled && !atTarget)
                MakeOneStepTowardsTheTarget();
        }
    }

    private void MakeOneStepTowardsTheTarget() {
        Vector3Int startNode = tilemap.WorldToCell(transform.position);
        Vector3Int endNode = targetInGrid;
        List<Vector3Int> shortestPath = Dijkstra<Vector3Int>.ShortestPath(tilemapGraph, startNode, endNode).ToList();
        Debug.Log("shortestPath = " + string.Join(" , ",shortestPath));
        if (shortestPath.Count >= 2) { // shortestPath contains both source and target.
            Vector3Int nextNode = shortestPath[1];
            transform.position = tilemap.GetCellCenterWorld(nextNode);
        } else {
            atTarget = true;
        }
    }
}
