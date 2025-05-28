using System.Collections.Generic;
using System.Linq;
using UnityEngine;

class MapObjectPool : MonoBehaviour
{
    public static MapObjectPool Instance { get { Init(); return s_instance; } }
    private static MapObjectPool s_instance;

    class Pool
    {
        public GameObject Original { get; private set; }

        private GameObject root;
        private readonly Queue<GameObject> poolQueue = new Queue<GameObject>();

        public void Create(GameObject _origin, int _count = 20)
        {
            Original = _origin;
            root = new GameObject($"{Original.name}_root");
            root.transform.parent = Instance.transform;
            for (int i=0;i< _count; ++i)
            {
                GameObject obj = Instantiate(Original);
                obj.name = Original.name;
                Enqueue(obj);
            }
        }

        public void Enqueue(GameObject _obj)
        {
            if (_obj == null)
            {
                return;
            }

            _obj.transform.parent = root.transform;
            _obj.SetActive(false);

            poolQueue.Enqueue(_obj);
        }
        public GameObject Dequeue()
        {
            GameObject obj;
            if (poolQueue.Count > 0) {
                obj = poolQueue.Dequeue();
            }
            else
            {
              obj = Instantiate(Original);
                obj.name = Original.name;
            }

            obj.SetActive(true);
            return obj;
        }
    }

    [SerializeField]
    private List<GameObject> preSelectedprefabs;
    private readonly Dictionary<string, Pool> poolDict = new Dictionary<string, Pool>();

    private void Awake()
    {
        foreach (GameObject prefab in preSelectedprefabs)
        {
            Create(prefab);
        }
    }

    private static void Init()
    {
        if (s_instance == null)
        {
            GameObject obj = GameObject.Find("@MapObjectPool");
            if (null == obj)
            {
                obj = new GameObject { name = "@MapObjectPool" };
                s_instance = obj.AddComponent<MapObjectPool>();
            }
            else
            {
                s_instance = obj.GetComponent<MapObjectPool>();
            }
        }
    }

    private void Create(GameObject _origin, int _count = 20)
    {
        if (_origin != null && !poolDict.ContainsKey(_origin.name))
        {
            Pool pool = new Pool();
            pool.Create(_origin, _count);
            poolDict[_origin.name] = pool;
        }
    }

    public void Enqueue(GameObject _obj)
    {
        if (_obj != null && !poolDict.ContainsKey(_obj.name))
        {
            Destroy(_obj);
            return;
        }

        poolDict[_obj.name].Enqueue(_obj);
    }

    public GameObject Dequeue(string _name)
    {
        if (!poolDict.ContainsKey(_name))
        {
            GameObject prefab = Resources.Load<GameObject>("Prefabs/" + _name);
            if(prefab != null)
            {
                Debug.LogWarning("MapObjectPool : Prefab Load Error");
                return null;
            }

            Create(prefab);
        }

        return poolDict[_name].Dequeue();
    }

    public GameObject Dequeue(GameObject _origin)
    {
        if (_origin == null)
        {
            return null;
        }

        if (!poolDict.ContainsKey(_origin.name))
        {
            Create(_origin);
        }

        return poolDict[_origin.name].Dequeue();
    }

    public GameObject SelectRandom()
    {
        if (poolDict.Count == 0)
        {
            return null;
        }

        return poolDict.ElementAt(Random.Range(0, poolDict.Count)).Value.Dequeue();
    }
}
