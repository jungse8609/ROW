using System.Collections.Generic;
using UnityEngine;

public class MapTile : MonoBehaviour
{
    public Vector2Int Coordinate { 
        get => coordinate;
        set
        {
            coordinate = value;
            Relocation();
        }
    }

    private Vector2Int coordinate = Vector2Int.zero;
    private GameObject plane;
    private List<GameObject> mapObjects = new List<GameObject>();      // 환경 오브젝트 보관용

    private const int PositionYBias = -2;
    private const int GenerateRandomMin = 3;
    private const int GenerateRandomMax = 7;
    private const float CollisionRadius = 2f;
    private const int MaxOverlapChecks = 2;

    private void Awake()
    {
        GameObject planePrefab = Resources.Load("Prefabs/Plane") as GameObject;
        Debug.Assert(planePrefab != null, "MapTile : Plane prefab not found");

        plane = Instantiate(planePrefab, transform);    // Instantiate는 null을 반환하지 않음 -> 기존 Log 지움
        plane.transform.localPosition = new Vector3(0, -1f, 0);
    }

    public void Relocation()
    {
        transform.position = MapController.CoordinateToPosition(coordinate);
        GenerateMapObjects();
    }

    private void GenerateMapObjects()
    {
        foreach(GameObject obj in mapObjects)
        {
            Destroy(obj);
        }
        mapObjects.Clear();

        int randomMapObjectCount = Random.Range(GenerateRandomMin, GenerateRandomMax);

        for(int i=0; i< randomMapObjectCount; ++i)
        {
            for(int escapeCount = 0; escapeCount < 10; ++i)
            {
                float positionX = Random.Range(-MapController.PlaneSize / 2, MapController.PlaneSize / 2 + 1);
                float positionZ = Random.Range(-MapController.PlaneSize / 2, MapController.PlaneSize / 2 + 1);
                
                Vector3 worldPosition = new Vector3(positionX, 0, positionZ) + transform.position;
                if (!IsPositionOccupied(worldPosition)) // if find _position
                {
                    GameObject obj = Instantiate(SelectPrefabs(), transform);
                    obj.transform.localPosition = new Vector3(positionX, PositionYBias, positionZ);
                    obj.transform.rotation = RandomRotation();

                    mapObjects.Add(obj);
                    break;
                }
            }
        }
    }
    private bool IsPositionOccupied(Vector3 _position)
    {
        // 해당 위치에 충돌체가 있는지 체크
        Physics.SyncTransforms(); // 콜라이더들 동기화
        Collider[] results = new Collider[MaxOverlapChecks];    // 2개면 충분
        int hits = Physics.OverlapSphereNonAlloc(_position, CollisionRadius, results);
        return hits > 1; // 충돌체가 있으면 true 반환 // Plane과는 항상 충돌하기때문에 1
    }
    private GameObject SelectPrefabs()  // prefab 결정해서 전달
    {
        int rand = UnityEngine.Random.Range(0, 5); // 0 ~ 4

        string path = "Prefabs/";
        switch(rand)
        {
            case 0:
                path += "Cabin";
                break;
            case 1:
                path += "Rock4";
                break;
            case 2:
                path += "Shrub";
                break;
            case 3:
                path += "Tree";
                break;
            case 4:
                path += "Tree_Chopped";
                break;
            default:
                break;
        }

        GameObject prefab = Resources.Load<GameObject>(path);
        Debug.Assert(prefab != null, "MapTile : MapObject Resourece Load error");
      
        return prefab;
    }
    private Quaternion RandomRotation() // y축 90의 배수 회전 값 랜덤 선택
    {
        // 90도, 180도, 270도, 360도 중 랜덤 선택
        float[] angles = { 90f, 180f, 270f, 360f };
        return Quaternion.Euler(0, angles[Random.Range(0, angles.Length)], 0);
    }
}
