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
    private float jumpForce=5f;

    void Awake(){
        rb=GetComponent<Rigidbody>();
        if(rb==null) Debug.LogError("Player rigidbody not found");
        bc=GetComponent<BoxCollider>();
        if(bc==null) Debug.LogError("Player box collider not found");
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
        Vector3 f = new Vector3(0f,jumpForce,0f);
        rb.AddForce(f,ForceMode.VelocityChange);
    }

}
