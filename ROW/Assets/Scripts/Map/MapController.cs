using System.Collections.Generic;
using Unity.AI.Navigation;
using UnityEngine;

public class MapController : MonoBehaviour
{
    [SerializeField]
    private GameObject debugPlane;
    [SerializeField]
    private GameObject player;

    private Vector2Int playerCoordinate = Vector2Int.zero;  // 플레이어의 이전 좌표
    private List<MapTile> maps = new List<MapTile>();
    private NavMeshSurface navMeshSurface;

    public const int ListArraySize = 2;
    public const int PlaneSize = 20;

    void Start()
    {
        navMeshSurface = GetComponent<NavMeshSurface>();
        Destroy(debugPlane);

        for (int x = -ListArraySize; x <= ListArraySize; ++x)
        {
            for (int y = -ListArraySize; y <= ListArraySize; ++y)
            {
                GameObject obj = new GameObject("MapTile");
                obj.transform.parent = transform;

                MapTile mapTile = obj.AddComponent<MapTile>();
                mapTile.Coordinate = new Vector2Int(x, y);

                maps.Add(mapTile);
            }
        }

        UpdateNavMesh();
    }

    void Update()
    {
        UpdatePlayerCoordinate();
    }

    private void UpdatePlayerCoordinate()
    {
        Vector3 position = player.transform.position;
        Vector2Int newCoordinate = new Vector2Int((int)(position.x / PlaneSize), (int)(position.z / PlaneSize));

        if (playerCoordinate != newCoordinate)
        {
            playerCoordinate = newCoordinate;
            UpdateMaps();
        }
    }

    private void UpdateMaps()
    {
        foreach (MapTile mapTile in maps)
        {
            int coordX = (int)(mapTile.Coordinate.x - playerCoordinate.x);
            int coordY = (int)(mapTile.Coordinate.y - playerCoordinate.y);

            if (Mathf.Abs(coordX) > ListArraySize)
            {
                mapTile.Coordinate = new Vector2Int(
                    playerCoordinate.x + (coordX > 0 ? -ListArraySize : ListArraySize),
                    mapTile.Coordinate.y);
            }
            if (Mathf.Abs(coordY) > ListArraySize)
            {
                mapTile.Coordinate = new Vector2Int(
                    mapTile.Coordinate.x,
                    playerCoordinate.y + (coordY > 0 ? -ListArraySize : ListArraySize));
            }
        }

        UpdateNavMesh();
    }

    private void UpdateNavMesh()
    {
        navMeshSurface.BuildNavMesh();
    }

    public static Vector3 CoordinateToPosition(Vector2Int _coordinate)
    {
        return new Vector3(_coordinate.x * PlaneSize, 0f, _coordinate.y * PlaneSize);
    }
}

