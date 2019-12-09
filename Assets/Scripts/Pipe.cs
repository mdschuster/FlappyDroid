using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pipe : MonoBehaviour
{

    private float despawnPosition=-20;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(this.transform.position.x< despawnPosition){
            Destroy(this.gameObject);
        }

    }

    private void FixedUpdate() {
        if(GameManager.gameState == GameManager.State.GAMEOVER){
            return;
        }
        //pipe object should move in one direction at a constant speed
        //TODO: Should probably use translate here
        Vector3 pos = this.transform.position;
        pos.x-=0.1f;
        this.transform.position=pos;
    }
}
