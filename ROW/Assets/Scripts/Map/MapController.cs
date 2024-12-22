using NUnit.Framework.Constraints;
using System.Collections.Generic;
using UnityEditor.VersionControl;
using UnityEngine;

public class MapController : MonoBehaviour
{
    private List<MapTile> m_Maps = new List<MapTile>();

    [SerializeField]
    public GameObject m_DebugPlane;

    [SerializeField]
    public GameObject m_Player = null;
    private Vector2 m_PlayerCoordinate = Vector2.zero;  // ���� �÷��̾� Coordinate

    static public int iListArraySize = 2;

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
    }

    // Update is called once per frame
    void Update()
    {
        CalcuatePlayerCoordinate();
    }

    private void CalcuatePlayerCoordinate() {
        Vector3 vPosition = m_Player.GetComponent<Transform>().position;
        // Plane ũ�� : 20x20
        Vector2 vNewPlayerCoordinate = new Vector2((int)(vPosition.x/20), (int)(vPosition.z/20));
        
        if (m_PlayerCoordinate != vNewPlayerCoordinate)
        {
            m_PlayerCoordinate = vNewPlayerCoordinate;
            UpdateMaps();   // �÷��̾� ��ġ�� �ٸ��� ��ü
        }
    }

    private void UpdateMaps()   // Coordinate ������� Plane ��ġ �̵�
    {
        foreach (MapTile mapTile in m_Maps)
        {
            // �ݴ����� Plane�� �� �ݴ������� �Űܾ���
            int CoordX = (int)(mapTile.m_Coordinate.x - m_PlayerCoordinate.x);
            int CoordY = (int)(mapTile.m_Coordinate.y - m_PlayerCoordinate.y);
            if (Mathf.Abs(CoordX) >= (iListArraySize + 1) || Mathf.Abs(CoordY) >= (iListArraySize + 1))
            {
                mapTile.Relocation(CoordX, CoordY);

            }
        }

    }
}
