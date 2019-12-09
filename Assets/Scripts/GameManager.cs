using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    private static GameManager _instance=null;

    [SerializeField]
    private GameObject pipePrefab;
    [SerializeField]
    private PlayerController player;

    [SerializeField]
    private float spawnRange=3f;

    [SerializeField]
    private float timeBetweenSpawn = 3;
    private float spawnCooldown;
    private float spawnTime=0;


    public enum State
    {
        PLAY,GAMEOVER
    }

    public static State gameState = new State();

    private void Awake() {
        if(_instance==null){
            _instance=this;
        }
    }

    public static GameManager instance(){
        return _instance;
    }



    // Start is called before the first frame update
    void Start()
    {
        if(pipePrefab==null) Debug.LogError("PipePrefab not found!");
    } 

    // Update is called once per frame
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

    private void spawnPipe(){
            //random y
            float ySpawn=Random.Range(-spawnRange,spawnRange);
            float xSpawn=10f;
            //actually do the spawn
            Instantiate(pipePrefab,new Vector3(xSpawn,ySpawn,0f), Quaternion.identity);
    }

    public void gameOver(){
        gameState=State.GAMEOVER;
        Time.timeScale=0.3f;
        player.gameObject.SetActive(false);
    }

    public void reset(){
        gameState=State.PLAY;
        Time.timeScale=1f;
        player.gameObject.SetActive(true);
        player.transform.position=Vector3.zero;
    }


}
