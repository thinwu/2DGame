using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D), typeof(DamageControl))]
public class SpawnPoint : MonoBehaviour
{
    public float spawnInteval = 1f;
    public GameObject[] spawnObject;
    public int totalSpawnNumber;
    float timeToSpawn = 0f;
    // Start is called before the first frame update
    void Awake()
    {
        gameObject.GetComponent<BoxCollider2D>().isTrigger = true;
    }

    // Update is called once per frame
    void Update()
    {
        timeToSpawn += Time.deltaTime;
        if(timeToSpawn >= spawnInteval && totalSpawnNumber>0)
        {
            timeToSpawn = 0f;
            --totalSpawnNumber;
            int typeIndex = Random.Range(0, spawnObject.Length);
            GameManager.instance.SpawnInSec(spawnObject[typeIndex], gameObject.transform.position,0);
        }
        
    }
}
