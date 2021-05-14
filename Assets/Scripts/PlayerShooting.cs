using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MLAPI;
using MLAPI.NetworkedVar;
using MLAPI.Messaging;

public class PlayerShooting : NetworkedBehaviour
{
    public ParticleSystem bulletParticleSystem;
    private ParticleSystem.EmissionModule em;

    NetworkedVarBool shooting = new NetworkedVarBool(new NetworkedVarSettings { WritePermission = NetworkedVarPermission.OwnerOnly }, false);
    // bool shooting = false;
    float fireRate = 10f;
    float shootTimer = 0f;

    void Start()
    {
        em = bulletParticleSystem.emission;
    }


    void Update()
    {
        if (IsLocalPlayer)
        {
            shooting.Value = Input.GetMouseButton(0); // inefficent way of doing it
            shootTimer += Time.deltaTime;
            if (shooting.Value && shootTimer >= 1f / fireRate)
            {
                shootTimer = 0f;
                //call our method
                InvokeServerRpc(ShootServerRpc);
            }


        }
        //Hard coded, if shooting then set to 10f else set to 0f
        em.rateOverTime = shooting.Value ? fireRate : 0f;
    }

    [ServerRPC]
    void ShootServerRpc()
    {
        Ray ray = new Ray(bulletParticleSystem.transform.position, bulletParticleSystem.transform.forward);
        if (Physics.Raycast(ray, out RaycastHit hit, 100f))
        {
            //when we hit smt
            var player = hit.collider.GetComponent<PlayerHealth>();
            if (player != null)
            {
                player.TakeDamage(10f);


            }
        }
    }
}
