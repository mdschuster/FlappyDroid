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
    private static GameManager _instance=null;

    /// <summary>
    /// Prefab GameObject that represents the pipe. Serialized.
    /// </summary>
    [SerializeField]
    private GameObject pipePrefab;

    /// <summary>
    /// Object that contains the PlayerController script. Serialized.
    /// </summary>
    [SerializeField]
    private PlayerController player;

    /// <summary>
    /// Y Rgane that the pipes can spawn over. Serialized.
    /// </summary>
    [SerializeField]
    private float spawnRange=3f;

    /// <summary>
    /// Starting time between pipe spawns. Serialized.
    /// </summary>
    [SerializeField]
    private float timeBetweenSpawn = 3;

    /// <summary>
    /// Timer for spawn. Determines when new spawns will happen.
    /// </summary>
    private float spawnTime=0;

    /// <summary>
    /// Game state.
    /// </summary>
    public enum State
    {
        PLAY,GAMEOVER
    }

    /// <summary>
    /// gameState enum. Contains current game state.
    /// </summary>
    public static State gameState = new State();

    private void Awake() {
        if(_instance==null){
            _instance=this;
        }
    }

    /// <summary>
    /// Global entry point for the GameManager Singleton class.
    /// </summary>
    /// <returns>Singleton instance of the GameManager class</returns>
    public static GameManager instance(){
        return _instance;
    }



    /// <summary>
    /// Start is called before the first frame update
    /// </summary>
    void Start()
    {
        if(pipePrefab==null) Debug.LogError("PipePrefab not found!");
    } 

    /// <summary>
    /// Update is called once per frame
    /// </summary>
    void Update()
    {
        if(gameState==State.GAMEOVER){
            return;
        }

        if(spawnTime<=0){
            spawnTime=timeBetweenSpawn;
            spawnPipe();
        } else {
            spawnTime-=Time.deltaTime;
        }
    }

    /// <summary>
    /// Spawns pipe prefab based on allowed spawn ranges.
    /// </summary>
    private void spawnPipe(){
            //random y
            float ySpawn=Random.Range(-spawnRange,spawnRange);
            float xSpawn=30f;
            //actually do the spawn
            Instantiate(pipePrefab,new Vector3(xSpawn,ySpawn,0f), Quaternion.identity);
    }

    /// <summary>
    /// Code to run when the game over is triggered.
    /// </summary>
    public void gameOver(){
        gameState=State.GAMEOVER;
        Time.timeScale=0.7f;
        player.gameObject.SetActive(false);
    }

    /// <summary>
    /// Code to run when the reset is triggered.
    /// </summary>
    public void reset(){
        gameState=State.PLAY;
        Time.timeScale=1f;
        player.gameObject.SetActive(true);
        player.transform.position=Vector3.zero;
    }


}
