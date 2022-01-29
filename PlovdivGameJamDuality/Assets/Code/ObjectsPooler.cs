using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectsPooler : MonoBehaviour
{
    internal static ObjectsPooler current;
    [SerializeField] internal List<GameObject> pooledPlatformsArray;
    [SerializeField] internal GameObject pooledGroundObject;
    [SerializeField] private int pooledAmount;
    [SerializeField] private int pooledAmountGround;
    [SerializeField] private bool willGrow, willGrowGround;

    internal List<GameObject> pooledObjects, pooledGroundObjects;
    [SerializeField] internal GameObject parentInstantiateObject;

    private void Awake()
    {
        current = this;
    }

    void Start()
    {
        pooledObjects = new List<GameObject>();
        pooledGroundObjects = new List<GameObject>();

        for (int i = 0; i < pooledAmount; i++)
        {
            //GameObject obj = Instantiate(pooledObjectsArray[Random.Range(0, pooledObjectsArray.Count)]);
            GameObject obj_1 = Instantiate(pooledPlatformsArray[0]);
            obj_1.transform.parent = parentInstantiateObject.transform;
            GameObject obj_2 = Instantiate(pooledPlatformsArray[1]);
            obj_2.transform.parent = parentInstantiateObject.transform;
            GameObject obj_3 = Instantiate(pooledPlatformsArray[2]);
            obj_3.transform.parent = parentInstantiateObject.transform;
            GameObject obj_4 = Instantiate(pooledPlatformsArray[3]);
            obj_4.transform.parent = parentInstantiateObject.transform;

            obj_1.SetActive(false);
            obj_2.SetActive(false);
            obj_3.SetActive(false);
            obj_4.SetActive(false);

            pooledObjects.Add(obj_1);
            pooledObjects.Add(obj_2);
            pooledObjects.Add(obj_3);
            pooledObjects.Add(obj_4);
        }

        for (int i = 0; i < pooledAmountGround; i++)
        {
            Debug.Log("pooled ?");
            GameObject groundObj = Instantiate(pooledGroundObject);
            groundObj.transform.parent = parentInstantiateObject.transform;
            groundObj.SetActive(false);
            pooledGroundObjects.Add(groundObj);
        }
    }

    internal GameObject GetPooledObject(string typeObject)
    {
        for (int i = 0; i < pooledObjects.Count; i++)
        {
            string[] prefabsFullName = pooledObjects[i].name.Split(new char[] { '_' }, System.StringSplitOptions.RemoveEmptyEntries);
            string name = prefabsFullName[0];

            if (name == typeObject && !pooledObjects[i].activeInHierarchy)
            {
                return pooledObjects[i];
            }
        }

        if (willGrow == true)
        {
            //GameObject obj = Instantiate(pooledObjectsArray[Random.Range(0, pooledObjectsArray.Count)]);
            for (int i = 0; i < pooledPlatformsArray.Count; i++)
            {
                if (typeObject == pooledPlatformsArray[i].name)
                {
                    GameObject obj = Instantiate(pooledPlatformsArray[i]);
                    obj.transform.parent = parentInstantiateObject.transform;
                    pooledObjects.Add(obj);
                    return obj;
                }
            }
        }

        return null;
    }

    internal GameObject GetPooledGroundObjects(string typeObject)
    {
        for (int i = 0; i < pooledGroundObjects.Count; i++)
        {
            string name = pooledGroundObject.name.Substring(0, 1);

            if (name == typeObject && !pooledGroundObjects[i].activeInHierarchy)
            {
                return pooledGroundObjects[i];
            }
        }

        if (willGrowGround == true)
        {
            GameObject laserObj = Instantiate(pooledGroundObject);
            laserObj.transform.parent = parentInstantiateObject.transform;
            pooledGroundObjects.Add(laserObj);
            return laserObj;
        }

        return null;
    }
}
