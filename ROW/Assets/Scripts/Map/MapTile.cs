using System;
using System.Collections;
using System.Collections.Generic;

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


    private List<GameObject> m_Envirionments = new List<GameObject>();      // 환경 오브젝트 보관용
    private bool[,] m_arrEnvironmentLocation = new bool[10, 10];

    public MapTile(Vector2 _Coordinate)
    {
        m_vCoordinate = _Coordinate;

        // 충돌 검사 문제인지 Terrain부터 이동하면 생성이 안됨
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

    private Vector3 CoordinateToPosition()
    {
        return new Vector3(m_vCoordinate.x * 20, -1f, m_vCoordinate.y * 20);
    }

    private void GenerateObjects()
    {
        // 1. 위치 초기화, 기존 오브젝트 전체 삭제
        for(int i=0; i<10; ++i)
        {
            for(int j=0; j<10; ++j)
            {
                m_arrEnvironmentLocation[i, j] = false;
            }
        }

        foreach(GameObject obj in m_Envirionments)
        {
            MonoBehaviour.Destroy(obj);

        }
        m_Envirionments.Clear();

        // 2. 생성 개수를 랜덤으로 지정
        int iEnvironmentCount = UnityEngine.Random.Range(3, 11);    // 3~10

        for(int i=0; i<iEnvironmentCount; ++i)
        {

            while(true)
            {
                float PositionX = UnityEngine.Random.Range(-10f, 11f);    // -10 ~ 10
                float PositionZ = UnityEngine.Random.Range(-10f, 11f);    // -10 ~ 10

                Vector3 vRandomPosition = new Vector3(PositionX, 1, PositionZ) + CoordinateToPosition();

                if(!IsPositionOccupied(vRandomPosition))
                {
                    vRandomPosition.y = -2;
                    GameObject obj = MonoBehaviour.Instantiate(SelectPrefabs(), vRandomPosition, RandomRotation());
                    if(null == obj)
                    {
                        Debug.Log("MapTile : Environment Object Instantiate error");

#if UNITY_EDITOR
                        UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
                    }

                    m_Envirionments.Add(obj);
                    break;
                }
            }
        }
    }

    private bool IsPositionOccupied(Vector3 Position)
    {
        // 해당 위치에 충돌체가 있는지 체크
        Collider[] colliders = Physics.OverlapSphere(Position, 2f);
        return colliders.Length > 0; // 충돌체가 있으면 true 반환
    }

    private GameObject SelectPrefabs()  // Prefab 결정해서 전달
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
    private Quaternion RandomRotation()     // y축 90의 배수 회전 값 랜덤 선택
    {
        // 90도, 180도, 270도, 360도 중 랜덤 선택
        float[] PossibleRotations = { 90f, 180f, 270f, 360f };
        int RandomIndex = UnityEngine.Random.Range(0, PossibleRotations.Length);
        return Quaternion.Euler(0, PossibleRotations[RandomIndex], 0);
    }
}
