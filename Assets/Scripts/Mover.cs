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

public class Mover : MonoBehaviour
{
    /// <summary>
    /// X position when the pipe will despawn
    /// </summary>
    private float despawnPosition = -20;

    private Transform parent;

    /// <summary>
    /// Start is called on the frame when a script is enabled just before
    /// any of the Update methods is called the first time.
    /// </summary>
    void Start()
    {
        if (this.transform.parent != null)
        {
            parent = this.transform.parent;
        }
        else
        {
            parent = this.transform;
        }
    }

    /// <summary>
    /// Update is called once per frame
    /// </summary>
    void Update()
    {
        if (parent.position.x < despawnPosition)
        {
            Destroy(parent.gameObject);
        }

    }

    /// <summary>
    /// Physics update. Contains code for moving the pipe.
    /// </summary>
    private void FixedUpdate()
    {
        if (GameManager.instance().moving == false) return;
        //pipe object should move in one direction at a constant speed
        //TODO: Should probably use translate here
        Vector3 pos = parent.position;
        pos.x -= 0.1f;
        parent.position = pos;
    }
}
