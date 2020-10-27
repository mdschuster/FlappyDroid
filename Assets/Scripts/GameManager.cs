/*
Copyright 2020 Micah Schuster

Redistribution and use in source and binary forms, with or without modification,
are permitted provided that the following conditions are met:

1. Redistributions of source code must retain the above copyright notice, this
list of conditions and the following disclaimer.

2. Redistributions in binary form must reproduce the above copyright notice,
this list of conditions and the following disclaimer in the documentation and/or
other materials provided with the distribution.

THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS "AS IS" AND
ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE IMPLIED
WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE DISCLAIMED.
IN NO EVENT SHALL THE COPYRIGHT HOLDER OR CONTRIBUTORS BE LIABLE FOR ANY DIRECT,
INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES (INCLUDING,
BUT NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES; LOSS OF USE,
DATA, OR PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND ON ANY THEORY OF
LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT (INCLUDING NEGLIGENCE
OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE OF THIS SOFTWARE, EVEN IF
ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    /// <summary>
    /// Instance of this GameManager object.
    /// </summary>
    private static GameManager _instance = null;

    /// <summary>
    /// Prefab GameObject that represents the pipe. Serialized.
    /// </summary>
    [SerializeField]
    private GameObject pipePrefab;

    [SerializeField]
    private List<GameObject> buildingPrefabList;

    /// <summary>
    /// Object that contains the PlayerController script. Serialized.
    /// </summary>
    [SerializeField]
    public PlayerController player;

    /// <summary>
    /// Y Rgane that the pipes can spawn over. Serialized.
    /// </summary>
    [SerializeField]
    private float spawnRange = 3f;

    /// <summary>
    /// Starting time between pipe spawns. Serialized.
    /// </summary>
    [SerializeField]
    private float timeBetweenSpawn = 3;

    /// <summary>
    /// Timer for spawn. Determines when new spawns will happen.
    /// </summary>
    private float spawnTime = 0;

    private float timeBetweenBuildingSpawn = 2f;
    private float buildingSpawnTime = 0f;

    /// <summary>
    /// Player state information, true for dead, false for alive.
    /// </summary>
    [System.NonSerialized]
    public bool dead = false;

    /// <summary>
    /// State machine variable.
    /// </summary>
    private State state;

    /// <summary>
    /// Static variable that determines if the pipes will update their movement.
    /// </summary>
    [System.NonSerialized]
    public bool moving = true;


    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
        }
    }

    /// <summary>
    /// Global entry point for the GameManager Singleton class.
    /// </summary>
    /// <returns>Singleton instance of the GameManager class</returns>
    public static GameManager instance()
    {
        return _instance;
    }

    /// <summary>
    /// Start is called before the first frame update
    /// </summary>
    void Start()
    {
        if (pipePrefab == null) Debug.LogError("PipePrefab not found!");
        state = new Play();

    }

    /// <summary>
    /// Update is called once per frame
    /// </summary>
    void Update()
    {
        //process the current state of the game every frame.
        state = state.process();
    }

    public void spawnPipePair()
    {
        if (spawnTime <= 0)
        {
            spawnTime = timeBetweenSpawn;
            //random y
            float topYSpawn = Random.Range(-spawnRange, spawnRange);
            float topYAngle = Random.Range(0f, 180f);
            float xSpawn = 30f;
            //actually do the spawn
            Instantiate(pipePrefab, new Vector3(xSpawn, topYSpawn, -2.5f), Quaternion.Euler(0f, topYAngle, 0f));

            float botYSpawn = topYSpawn - 2f - Random.Range(0f, 2f);
            float botYAngle = Random.Range(0f, 180f);


            Instantiate(pipePrefab, new Vector3(xSpawn, botYSpawn, -2.5f), Quaternion.Euler(180f, botYAngle, 0f));

            //also spawn score plane here too!

        }
        else
        {
            spawnTime -= Time.deltaTime;
        }
    }

    public void spawnBuilding()
    {
        if (buildingSpawnTime <= 0)
        {
            buildingSpawnTime = timeBetweenBuildingSpawn;
            //random y
            float ySpawn = -12f - Random.Range(0f, 1f);
            float xSpawn = 30f;

            GameObject chosenBuilding = buildingPrefabList[Random.Range(0, buildingPrefabList.Count)];
            //actually do the spawn
            Instantiate(chosenBuilding, new Vector3(xSpawn, ySpawn, -2.5f), Quaternion.Euler(0f, 90f, 0f));
        }
        else
        {
            buildingSpawnTime -= Time.deltaTime;
        }
    }

    /// <summary>
    /// Code to run when the game over is triggered.
    /// </summary>
    public void gameOver()
    {
        player.gameObject.SetActive(false);
        this.moving = false;
    }

    /// <summary>
    /// Code to run when the reset is triggered.
    /// </summary>
    public void reset()
    {
        this.dead = false;
        this.moving = true;
        Time.timeScale = 1f;
        player.gameObject.SetActive(true);
        player.transform.position = Vector3.zero;
    }

    /// <summary>
    /// Code to run when the player object death is triggered.
    /// </summary>
    public void death()
    {
        GameManager.instance().gameOver();
        Vector3 offset = new Vector3(3.4f, 0f, 1.13f);
        GameObject go = Instantiate(player.deathEffect, player.transform.position, Quaternion.identity);
        go.GetComponent<ParticleSystem>().Play();
    }


}
