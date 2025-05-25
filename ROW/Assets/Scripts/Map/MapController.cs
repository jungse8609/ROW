using System.Collections.Generic;
using UnityEngine;

public class MapController : MonoBehaviour
{
    private List<MapTile> m_Maps = new List<MapTile>();

    [SerializeField]
    public GameObject m_DebugPlane;

    [SerializeField]
    public GameObject m_Player = null;
    private Vector2 m_PlayerCoordinate = Vector2.zero;  // 이전 플레이어 Coordinate

    static public int iListArraySize = 2;
    static public int PLANESIZE = 20;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Destroy(m_DebugPlane);

        for(int i=-iListArraySize; i<=iListArraySize; ++i)
        {
            for(int j=-iListArraySize;j<=iListArraySize; ++j)
            {
                MapTile newMapTile = new MapTile(new Vector2(i, j));
                m_Maps.Add(newMapTile);
            }
        }

        m_Maps[0].GenerateNavMesh();
        //foreach (MapTile mapTile in m_Maps)
        //{
        //    mapTile.GenerateNavMesh();
        //}

    }

    void Update()
    {
        CalcuatePlayerCoordinate();
    }

    private void CalcuatePlayerCoordinate() {
        Vector3 vPosition = m_Player.GetComponent<Transform>().position;
        Vector2 vNewPlayerCoordinate = new Vector2((int)(vPosition.x/ PLANESIZE), (int)(vPosition.z/PLANESIZE));
        
        if (m_PlayerCoordinate != vNewPlayerCoordinate)
        {
            m_PlayerCoordinate = vNewPlayerCoordinate;
            UpdateMaps();
        }
    }



    private void UpdateMaps()
    {
        foreach (MapTile mapTile in m_Maps)
        {
            int CoordX = (int)(mapTile.m_Coordinate.x - m_PlayerCoordinate.x);
            int CoordY = (int)(mapTile.m_Coordinate.y - m_PlayerCoordinate.y);
            if (Mathf.Abs(CoordX) >= (iListArraySize + 1) || Mathf.Abs(CoordY) >= (iListArraySize + 1))
            {
                mapTile.Relocation(CoordX, CoordY);

            }
        }
        m_Maps[0].GenerateNavMesh();
    }
}
