using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pipe : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void FixedUpdate() {
        //pipe object should move in one direction at a constant speed
        //TODO: Should probably use translate here
        Vector3 pos = this.transform.position;
        pos.x-=0.1f;
        this.transform.position=pos;
    }
}
