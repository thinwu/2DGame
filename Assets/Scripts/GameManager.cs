using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance { get; private set; }
    public GameObject[] Ammos;
    public GameObject[] Health;
    // Start is called before the first frame update
    void Awake()
    {
        instance = this;
    }
    private IEnumerator _spawnInSec(GameObject o, Vector2 position, float delay)
    {
        yield return new WaitForSeconds(delay);
        GameObject.Instantiate(o, position, Quaternion.identity);
    }
    public void SpawnInSec(GameObject o, Vector2 position, float delay)
    {
        StartCoroutine(_spawnInSec(o, position, delay));
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
