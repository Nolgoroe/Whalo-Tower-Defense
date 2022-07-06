using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

[Serializable]
public class Pool
{
    public string tagOfPool;
    public GameObject objectToPoolPrefab;
    public int originalSizeOfPool;
    public Transform poolParent;
}

public class ObjectPoolingManager : MonoBehaviour
{
    public List<Pool> objectPools;

    public Dictionary<string, Queue<GameObject>> poolDict;


    #region public functions
    public GameObject GetFromPool(string _tagOfObject, Vector3 _originPosition, Quaternion _desiredRotation)
    {
        if (!poolDict.ContainsKey(_tagOfObject))
        {
            Debug.LogError("Error in pooling! - tag doesn't exsist!");
            return null;

        }

        GameObject go = null;

        if (poolDict[_tagOfObject].Count > 0)
        {
            go = poolDict[_tagOfObject].Dequeue();
            go.SetActive(true);
            go.transform.position = _originPosition;
            go.transform.rotation = _desiredRotation;
        }
        else
        {
            Pool pool = objectPools.Where(p => p.tagOfPool == _tagOfObject).SingleOrDefault();

            go = Instantiate(pool.objectToPoolPrefab, pool.poolParent);

            AddObjectToPoolstring(_tagOfObject, go, _originPosition, _desiredRotation);
        }


        return go;
    }

    public void AddObjectBackToQueue(string _tagOfObject, GameObject _objectToAddToQueue)
    {
        poolDict[_tagOfObject].Enqueue(_objectToAddToQueue);
        _objectToAddToQueue.SetActive(false);
    }

    #endregion

    #region private functions
    private void Start()
    {
        poolDict = new Dictionary<string, Queue<GameObject>>();

        foreach (Pool pool in objectPools)
        {
            Queue<GameObject> queuePool = new Queue<GameObject>();

            for (int i = 0; i < pool.originalSizeOfPool; i++)
            {
                GameObject go = Instantiate(pool.objectToPoolPrefab, pool.poolParent);
                go.SetActive(false);

                queuePool.Enqueue(go);
            }

            poolDict.Add(pool.tagOfPool, queuePool);
        }
    }
    private void AddObjectToPoolstring(string _tagOfObject, GameObject _objectToAddToQueue, Vector3 _originPosition, Quaternion _desiredRotation)
    {
        _objectToAddToQueue.SetActive(true);
        _objectToAddToQueue.transform.position = _originPosition;
        _objectToAddToQueue.transform.rotation = _desiredRotation;
    }

    #endregion
}
