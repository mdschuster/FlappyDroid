using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(BoxCollider))]
public class PlayerController : MonoBehaviour
{
    Rigidbody rb;
    BoxCollider bc;
    
    [SerializeField]
    GameObject deathEffect;

    [SerializeField]
    private float jumpForce=5f;

    void Awake(){
        rb=GetComponent<Rigidbody>();
        if(rb==null) Debug.LogError("Player rigidbody not found");
        bc=GetComponent<BoxCollider>();
        if(bc==null) Debug.LogError("Player box collider not found");
        if(deathEffect==null) Debug.LogError("No Death Effecct Found");
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space)){
            jump();
        }        

    }

    
    private void jump(){
        if(GameManager.gameState==GameManager.State.GAMEOVER){
            return;
        }
        Vector3 f = new Vector3(0f,jumpForce,0f);
        rb.AddForce(f,ForceMode.VelocityChange);
    }

    private void OnCollisionEnter(Collision other) {
        death();
        Debug.Log("hit");
    }

    private void death(){
        GameManager.instance().gameOver();
        Vector3 offset = new Vector3(3.4f,0f,1.13f);
        GameObject go = Instantiate(deathEffect, this.transform.position+offset,Quaternion.identity);
        go.GetComponent<ParticleSystem>().Play();
    }

}
