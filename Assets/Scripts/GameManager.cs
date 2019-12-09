using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    [SerializeField]
    private GameObject pipePrefab;

    [SerializeField]
    private float spawnRange=3f;

    [SerializeField]
    private float timeBetweenSpawn = 3;
    private float spawnCooldown;
    private float spawnTime=0;


    // Start is called before the first frame update
    void Start()
    {
        if(pipePrefab==null) Debug.LogError("PipePrefab not found!");
    } 

    // Update is called once per frame
    void Update()
    {
        if(spawnTime<=0){
            spawnTime=timeBetweenSpawn;
            //random y
            float ySpawn=Random.Range(-spawnRange,spawnRange);
            float xSpawn=10f;
            //actually do the spawn
            Instantiate(pipePrefab,new Vector3(xSpawn,ySpawn,0f), Quaternion.identity);

        } else {
            spawnTime-=Time.deltaTime;
        }
    }

    private void spawnPipe(){

    }
}
