using System.Collections.Generic;
using UnityEngine;

public class DotPool : MonoBehaviour
{
    #region public variables
    public GameObject DotPrefab;
    public int amountToPool;
    #endregion

    #region private variables
    private List<GameObject> pooledObjects = new List<GameObject>();
    #endregion

    #region Unity methods
    private void Start()
    {
        for (int i = 0; i < amountToPool; i++)
        {
            var newObject = Instantiate(DotPrefab, transform);
            newObject.SetActive(false);
            pooledObjects.Add(newObject);
        }
    }
    #endregion

    #region public methods
    public GameObject GetPooledObject()
    {
        foreach (var pooledObject in pooledObjects)
        {
            if (!pooledObject.activeInHierarchy)
            {
                return pooledObject;
            }
        }

        return null;
    }

    public void ReturnAllObjectsToPool()
    {
        foreach (var pooledObject in pooledObjects)
        {
            if (pooledObject.activeInHierarchy)
            {
                pooledObject.transform.parent = transform;
                pooledObject.SetActive(false);
            }
        }
    }
    #endregion
}
