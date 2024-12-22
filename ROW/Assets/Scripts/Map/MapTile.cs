using UnityEngine;

public class MapTile
{
    public Vector2 m_Coordinate { 
        get { return m_vCoordinate; } 
        set { m_vCoordinate = value; } 
    }

    private Vector2 m_vCoordinate = Vector2.zero;
    private GameObject m_Plane = null;


    public MapTile(Vector2 _Coordinate)
    {
        m_vCoordinate = _Coordinate;
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

        m_Plane.GetComponent<Transform>().position = CoordinateToPosition();
        
    }

    private Vector3 CoordinateToPosition()
    {
        return new Vector3(m_vCoordinate.x * 20, -0.9f, m_vCoordinate.y * 20);
    }
}
