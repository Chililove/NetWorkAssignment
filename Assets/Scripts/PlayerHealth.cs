using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MLAPI;
using MLAPI.NetworkedVar;
using MLAPI.Messaging;

public class PlayerHealth : NetworkedBehaviour
{

   public NetworkedVarFloat health = new NetworkedVarFloat(100f);
    public float currentHealth;
    MeshRenderer[] renderers;
    CharacterController cc;

    public HealthBar healthBar;


    //running on the server

    private void Start()
    {
        renderers = GetComponentsInChildren<MeshRenderer>();
        cc = GetComponent<CharacterController>();
        currentHealth = health.Value;
        healthBar.SetHealth(health.Value);
        
    }

    private void Update()
    {
        if (IsOwner)
        {
            healthBar.SetHealth(health.Value);

            if (health.Value <= 0)
            {
                // respawn
                health.Value = 100f;
                Vector3 pos = new Vector3(Random.Range(-10, 10), 4, Random.Range(-10, 10));
                InvokeClientRpcOnEveryone(ClientRespawn, pos);
            }
        }

    }

    public void TakeDamage(float damage)
    {
  
            health.Value -= damage;      
          //  healthBar.slider.value = health.Value; // hmmm
           
        //check health
       
    }

    [ClientRPC]
    void ClientRespawn(Vector3 position)
    {

        StartCoroutine(Respawn(position));
    }

    IEnumerator Respawn(Vector3 position)
    {
        foreach (var renderer in renderers)
        {
            renderer.enabled = false;
        }  
        cc.enabled = false;
        // this puts a delay
        transform.position = position;
        yield return new WaitForSeconds(1f);
        cc.enabled = true;
        foreach(var renderer in renderers)
        {
            renderer.enabled = true;
        }
    }
}
