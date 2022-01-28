using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneSpawnObjects : MonoBehaviour
{
    private float nextSpawnTimeBig, nextSpawnTimeMedium, nextSpawnTimeSmall;
    private string[] meteoritesNames = new string[] { "BigMeteorite", "MediumMeteorite", "SmallMeteorite" };
    private string[] laserNames = new string[] { "LaserItem", "LaserItemRed" };

    [SerializeField] private float addToSpawnTime;
    public float chanceSpawnRare = 0.1f;

    // Start is called before the first frame update
    void Start()
    {
        nextSpawnTimeBig = Time.time + addToSpawnTime;
        //PermanentFunctions.instance.OnIncreaseSpeed += IncreaseRateOfSpawnMeteorites;

        //nextSpawnTimeMedium = Time.time + 3f;
        //nextSpawnTimeSmall = Time.time + 1f;

    }

    // Update is called once per frame
    void Update()
    {
        SpawnMeteorite(nextSpawnTimeBig);
    }

    private void SpawnMeteorite(float spawnTime)
    {
        if (Time.time > spawnTime)
        {
            string typeObject = string.Empty;

            if (Random.Range(0f, 2f) > chanceSpawnRare)
            {
                typeObject = meteoritesNames[Random.Range(0, meteoritesNames.Length)];
            }
            else
            {
                typeObject = laserNames[Random.Range(0, laserNames.Length)];
            }

            GameObject obj = ObjectsPooler.current.GetPooledObject(typeObject);

            if (obj == null)
            {
                return;
            }
            obj.transform.position = this.transform.position;
            obj.transform.rotation = this.transform.rotation;
            obj.SetActive(true);

            nextSpawnTimeBig += addToSpawnTime;
        }
    }

    private void IncreaseRateOfSpawnMeteorites()
    {
        if (addToSpawnTime >= 0)
        {
            addToSpawnTime -= 0.2f;
        }

    }
}
