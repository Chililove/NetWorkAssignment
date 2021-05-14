using Assets.Scripts.Server;
using MLAPI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Client
{
    public class ClientInputSender: NetworkedBehaviour
    {
        private NetworkPlayerState playerState;
        
       // float fireRate = 10f;
      // float shootTimer = 0f;

        void Start()
        {
            playerState = GetComponent<NetworkPlayerState>();
        }

        void Update()
        {
            if (!IsClient || !IsOwner)
            {
                return;
            }
            if(Input.GetMouseButton(0))
            {
                playerState.ShootingServerRPC();
            }

        }
    }
}
