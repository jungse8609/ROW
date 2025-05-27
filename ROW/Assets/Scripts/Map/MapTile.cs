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
    private List<GameObject> mapObjects = new List<GameObject>();      // ȯ�� ������Ʈ ������

    private const int PositionYBias = -2;
    private const int GenerateRandomMin = 3;
    private const int GenerateRandomMax = 7;
    private const float CollisionRadius = 2f;
    private const int MaxOverlapChecks = 2;

    private void Awake()
    {
        GameObject planePrefab = Resources.Load("Prefabs/Plane") as GameObject;
        Debug.Assert(planePrefab != null, "MapTile : Plane prefab not found");

        plane = Instantiate(planePrefab, transform);    // Instantiate�� null�� ��ȯ���� ���� -> ���� Log ����
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
        // �ش� ��ġ�� �浹ü�� �ִ��� üũ
        Physics.SyncTransforms(); // �ݶ��̴��� ����ȭ
        Collider[] results = new Collider[MaxOverlapChecks];    // 2���� ���
        int hits = Physics.OverlapSphereNonAlloc(_position, CollisionRadius, results);
        return hits > 1; // �浹ü�� ������ true ��ȯ // Plane���� �׻� �浹�ϱ⶧���� 1
    }
    private GameObject SelectPrefabs()  // prefab �����ؼ� ����
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
    private Quaternion RandomRotation() // y�� 90�� ��� ȸ�� �� ���� ����
    {
        // 90��, 180��, 270��, 360�� �� ���� ����
        float[] angles = { 90f, 180f, 270f, 360f };
        return Quaternion.Euler(0, angles[Random.Range(0, angles.Length)], 0);
    }
}
