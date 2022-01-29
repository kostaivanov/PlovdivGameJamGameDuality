using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneSpawnObjects : MonoBehaviour
{
    private float nextSpawnTime;
    private string[] platformNames = new string[] { "1", "2", "3", "4" };
    private string groundName = "5";
    [SerializeField] private float addToSpawnTime;
    public float chanceSpawnRare = 0.1f;

    // Start is called before the first frame update
    void Start()
    {
        nextSpawnTime = Time.time + addToSpawnTime;
        //PermanentFunctions.instance.OnIncreaseSpeed += IncreaseRateOfSpawnMeteorites;

        //nextSpawnTimeMedium = Time.time + 3f;
        //nextSpawnTimeSmall = Time.time + 1f;
    }

    // Update is called once per frame
    void Update()
    {
        if (this.gameObject.name.EndsWith("4") == true)
        {
            SpawnGround(nextSpawnTime);
        }
        else
        {
            SpawnMeteorite(nextSpawnTime);
        }
    }

    private void SpawnMeteorite(float spawnTime)
    {
        if (Time.time > spawnTime)
        {
            string typeObject = string.Empty;

            if (Random.Range(0f, 10f) > chanceSpawnRare)
            {
                typeObject = platformNames[Random.Range(0, platformNames.Length)];
            }
            //else
            //{
            //    typeObject = laserNames[Random.Range(0, laserNames.Length)];
            //}

            GameObject obj = ObjectsPooler.current.GetPooledObject(typeObject);

            if (obj == null)
            {
                return;
            }
            obj.transform.position = this.transform.position;
            obj.transform.rotation = this.transform.rotation;
            obj.SetActive(true);

            nextSpawnTime += addToSpawnTime;
        }
    }

    private void SpawnGround(float spawnTime)
    {
        if (Time.time > spawnTime)
        {
            string typeObject = string.Empty;
           
            typeObject = groundName;
            
            GameObject obj = ObjectsPooler.current.GetPooledObject(typeObject);

            if (obj == null)
            {
                return;
            }
            obj.transform.position = this.transform.position;
            obj.transform.rotation = this.transform.rotation;
            obj.SetActive(true);

            nextSpawnTime += addToSpawnTime;
        }
    }

    //private void IncreaseRateOfSpawnMeteorites()
    //{
    //    if (addToSpawnTime >= 0)
    //    {
    //        addToSpawnTime -= 0.2f;
    //    }

    //}
}
