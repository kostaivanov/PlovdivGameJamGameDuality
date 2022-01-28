using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectsPooler : MonoBehaviour
{
    internal static ObjectsPooler current;
    [SerializeField] internal List<GameObject> pooledObjectsArray;
    [SerializeField] private int pooledAmount;
    [SerializeField] private bool willGrow;

    internal List<GameObject> pooledObjects;
    [SerializeField] internal GameObject parentInstantiateObject;

    private void Awake()
    {
        current = this;
    }

    void Start()
    {
        pooledObjects = new List<GameObject>();

        for (int i = 0; i < pooledAmount; i++)
        {
            //GameObject obj = Instantiate(pooledObjectsArray[Random.Range(0, pooledObjectsArray.Count)]);
            GameObject obj_1 = Instantiate(pooledObjectsArray[0]);
            obj_1.transform.parent = parentInstantiateObject.transform;
            GameObject obj_2 = Instantiate(pooledObjectsArray[1]);
            obj_2.transform.parent = parentInstantiateObject.transform;
            GameObject obj_3 = Instantiate(pooledObjectsArray[2]);
            obj_3.transform.parent = parentInstantiateObject.transform;
            GameObject obj_4 = Instantiate(pooledObjectsArray[3]);
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
            for (int i = 0; i < pooledObjectsArray.Count; i++)
            {
                if (typeObject == pooledObjectsArray[i].name)
                {
                    GameObject obj = Instantiate(pooledObjectsArray[i]);
                    obj.transform.parent = parentInstantiateObject.transform;
                    pooledObjects.Add(obj);
                    return obj;
                }
            }
        }

        return null;
    }
}
