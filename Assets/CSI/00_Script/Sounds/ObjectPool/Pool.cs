using System.Collections.Generic;
using UnityEngine;

public class Pool
{
    private Stack<Ipoolable> _pool;
    private Transform _parentTrm;
    private Ipoolable _poolable;
    private GameObject _prefab;


    public Pool(Ipoolable poolable, Transform parent, int count)
    {
        _pool = new Stack<Ipoolable>(count);
        _parentTrm = parent;
        _poolable = poolable;
        _prefab = poolable.ObjectPrefab;

        for (int i = 0; i < count; i++)
        {
            GameObject gameObj = GameObject.Instantiate(_prefab, _parentTrm);
            gameObj.SetActive(false);
            gameObj.name = _poolable.PoolName;
            Ipoolable item = gameObj.GetComponent<Ipoolable>();
            _pool.Push(item);
        }
    }

    public Ipoolable Pop()
    {
        Ipoolable item = null;

        if (_pool.Count == 0)
        {
            GameObject gameObj = GameObject.Instantiate(_prefab, _parentTrm);
            gameObj.name = _poolable.PoolName;
            item = gameObj.GetComponent<Ipoolable>();
        }
        else
        {
            item = _pool.Pop();
            item.ObjectPrefab.SetActive(true);
        }
        return item;
    }

    public void Push(Ipoolable item)
    {
        item.ObjectPrefab.SetActive(false);
        _pool.Push(item);
    }
}
