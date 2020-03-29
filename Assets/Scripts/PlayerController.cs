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

/// <summary>
/// PlayerController contains all information for moving the player character object.
/// Must have a Rigidbody and BoxCollider components attached.
/// </summary>
[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(BoxCollider))]
public class PlayerController : MonoBehaviour
{
    /// <summary>
    /// Contains a reference to the Rigidbody.
    /// </summary>
    Rigidbody rb;

    /// <summary>
    /// Contains a reference to the boxCollider.
    /// </summary>
    BoxCollider bc;

    /// <summary>
    /// GameObject prefab with death particle effect. Serialized.
    /// </summary>
    [SerializeField]
    public GameObject deathEffect;

    /// <summary>
    /// Upward force when pressed, Serialized.
    /// </summary>
    [SerializeField]
    private float jumpForce = 5f;

    /// <summary>
    /// Array of particles systems to play when 'jumping'. Serialized.
    /// </summary>
    [SerializeField]
    private ParticleSystem[] upThrust;

    /// <summary>
    /// Runs before Start().
    /// </summary>
    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        if (rb == null) Debug.LogError("Player rigidbody not found");
        bc = GetComponent<BoxCollider>();
        if (bc == null) Debug.LogError("Player box collider not found");
        if (deathEffect == null) Debug.LogError("No Death Effecct Found");
    }

    /// <summary>
    /// Update is called every frame of the game.
    /// </summary>
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            jump();
        }

    }

    /// <summary>
    /// Jump adds the jumpForce in the upward direction and plays each
    /// particle system associated with the upThrust array.
    /// </summary>
    private void jump()
    {
        Vector3 f = new Vector3(0f, jumpForce, 0f);
        rb.AddForce(f, ForceMode.VelocityChange);

        foreach (ParticleSystem ps in upThrust)
        {
            ps.Play();
        }
    }

    /// <summary>
    /// Code to run when two object colliders interact.
    /// </summary>
    /// <param name="other">Other collider involved in the collision.</param>
    private void OnCollisionEnter(Collision other)
    {
        GameManager.instance().dead = true;
        Debug.Log("hit");
    }

    /// <summary>
    /// Code to run when two objects interact via triggers.
    /// </summary>
    /// <param name="other">Other collider involved in the collision.</param>
    private void OnTriggerEnter(Collider other)
    {
        GameManager.instance().dead = true;
        Debug.Log("left game region");
    }
}
