using MLAPI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Server
{
    public class NetworkPlayerShooting: NetworkedBehaviour
    {
        private NetworkPlayerState playerState;

        public ParticleSystem bulletParticleSystem;
        private ParticleSystem.EmissionModule em;
        bool shoot;
        // bool shooting = false;
        float fireRate = 10f;
        float shootTimer = 0f;


        void Start()
        {
            em = bulletParticleSystem.emission;
        }

        public override void NetworkStart()
        {
            if (!IsServer)
            {
                enabled = false;
            }
            else
            {
                playerState = GetComponent<NetworkPlayerState>();
                playerState.RecievedShootingEvent += PlayerState_RecievedShootingEvent;
            }
        }

        private void PlayerState_RecievedShootingEvent(bool shooting)
        {
            shootTimer += Time.deltaTime;
            if (shooting && shootTimer >= 1f / fireRate)
            {         
                shootTimer = 0f;
                //call our method
                shoot = true;
            }
            //Hard coded, if shooting then set to 10f else set to 0f
            em.rateOverTime = shooting ? fireRate : 0f;

        }

        private void OnDestroy()
        {
            if(playerState)
            {
                playerState.RecievedShootingEvent -= PlayerState_RecievedShootingEvent;
            }
        }

        private void Update()
        {
            if (!IsServer) { return; }

            if(shoot)
            {
                shoot = false;
                Ray ray = new Ray(bulletParticleSystem.transform.position, bulletParticleSystem.transform.forward);
                if (Physics.Raycast(ray, out RaycastHit hit, 100f))
                {
                    //when we hit smt
                    var player = hit.collider.GetComponent<NetworkPlayerState>();
                    if (player != null)
                    {
                        player.health.Value -= 10f;
                    }
                }
            }
        }
    }
}
