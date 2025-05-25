using System;
using System.Collections;
using System.Collections.Generic;

using Unity.AI.Navigation;

using UnityEngine;
using UnityEngine.UIElements;

public class MapTile
{
    public Vector2 m_Coordinate { 
        get { return m_vCoordinate; } 
        set { m_vCoordinate = value; } 
    }

    private Vector2 m_vCoordinate = Vector2.zero;
    private GameObject m_Plane = null;
    private List<GameObject> m_Environments = new List<GameObject>();      // ȯ�� ������Ʈ ������

    public MapTile(Vector2 _Coordinate)
    {
        m_vCoordinate = _Coordinate;    

        // �浹 �˻� �������� Terrain���� �̵��ϸ� ������ �ȵ�
        GenerateObjects();

        GameObject Plane = Resources.Load("Prefabs/Plane") as GameObject;

        if(null == Plane)
        {
            Debug.Log("MapTile : Resources.Load error");
        }

        m_Plane = MonoBehaviour.Instantiate(Plane, CoordinateToPosition(), Quaternion.identity);

        if (null == m_Plane)
        {
            Debug.Log("MapTile : Instantiate error");
        }

    }

    public void Relocation(int CoordX, int CoordY)
    {
        Vector2 Before = new Vector2(m_vCoordinate.x, m_vCoordinate.y);
        if(CoordX >= (MapController.iListArraySize+1))
        {
            m_vCoordinate.x -= (MapController.iListArraySize * 2 + 1);
        }
        else if(CoordX <= -(MapController.iListArraySize + 1))
        {
            m_vCoordinate.x += (MapController.iListArraySize * 2 + 1);
        }

        if(CoordY >= (MapController.iListArraySize + 1))
        {
            m_vCoordinate.y -= (MapController.iListArraySize * 2 + 1);
        }
        else if (CoordY <= -(MapController.iListArraySize + 1))
        {
            m_vCoordinate.y += (MapController.iListArraySize * 2 + 1);
        }

        GenerateObjects();

        m_Plane.GetComponent<Transform>().position = CoordinateToPosition();
    }




    public void GenerateNavMesh()
    {
        NavMeshSurface surface = m_Plane.GetComponent<NavMeshSurface>();
        surface.RemoveData();
        surface.BuildNavMesh();

        //foreach (var Environment in m_Environments)
        //{
        //    surface = Environment.GetComponent<NavMeshSurface>();
        //    surface.RemoveData();
        //    surface.BuildNavMesh();
        //}
    }

    private Vector3 CoordinateToPosition()
    {
        return new Vector3(m_vCoordinate.x * 20, -1f, m_vCoordinate.y * 20);
    }

    static int POSITION_Y_BIAS = -2;
    static int GENERATE_RANDOM_MIN = 3;
    static int GENERATE_RANDOM_MAX = 7;

    private void GenerateObjects()
    {
        foreach(GameObject obj in m_Environments)
        {
            MonoBehaviour.Destroy(obj);
        }
        m_Environments.Clear();

        int RandEnvironmentCount = UnityEngine.Random.Range(GENERATE_RANDOM_MIN, GENERATE_RANDOM_MAX);

        for(int i=0; i< RandEnvironmentCount; ++i)
        {
            bool IsPositionFound = false;
            int EscapeCount = 0;

            while(!IsPositionFound && EscapeCount <= 10)
            {
                ++EscapeCount;

                float PositionX = UnityEngine.Random.Range(-MapController.PLANESIZE, MapController.PLANESIZE + 1);
                float PositionZ = UnityEngine.Random.Range(-MapController.PLANESIZE, MapController.PLANESIZE + 1);

                Vector3 vRandomPosition = new Vector3(PositionX, 1, PositionZ) + CoordinateToPosition();

                IsPositionFound = IsPositionOccupied(vRandomPosition);

                if (!IsPositionFound) // if find Position
                {
                    vRandomPosition.y = POSITION_Y_BIAS;
                    GameObject obj = MonoBehaviour.Instantiate(SelectPrefabs(), vRandomPosition, RandomRotation());
                    m_Environments.Add(obj);
                    break;

                }
            }
        }
    }

    private bool IsPositionOccupied(Vector3 Position)
    {
        // �ش� ��ġ�� �浹ü�� �ִ��� üũ
        Collider[] colliders = Physics.OverlapSphere(Position, 2f);
        return colliders.Length > 0; // �浹ü�� ������ true ��ȯ
    }

    private GameObject SelectPrefabs()  // Prefab �����ؼ� ����
    {
        int iRand = UnityEngine.Random.Range(0, 5); // 0 ~ 4

        string fPath = "Prefabs/";
        switch(iRand)
        {
            case 0:
                fPath += "Cabin";
                break;
            case 1:
                fPath += "Rock4";
                break;
            case 2:
                fPath += "Shrub";
                break;
            case 3:
                fPath += "Tree";
                break;
            case 4:
                fPath += "Tree_Chopped";
                break;
            default:
                break;
        }

        GameObject Prefab = Resources.Load<GameObject>(fPath);
        if (Prefab == null)
        {
            Debug.Log("MapTile : Environment Object Resourece Load error");

#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
        }

        return Prefab;
    }
    private Quaternion RandomRotation()     // y�� 90�� ��� ȸ�� �� ���� ����
    {
        // 90��, 180��, 270��, 360�� �� ���� ����
        float[] PossibleRotations = { 90f, 180f, 270f, 360f };
        int RandomIndex = UnityEngine.Random.Range(0, PossibleRotations.Length);
        return Quaternion.Euler(0, PossibleRotations[RandomIndex], 0);
    }
}
