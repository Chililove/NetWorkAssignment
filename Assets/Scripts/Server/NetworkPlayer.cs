using MLAPI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Server
{
    public class NetworkPlayer: NetworkedBehaviour
    {
        private NetworkPlayerState playerState;
        
        public override void NetworkStart()
        {
            if(!IsServer)
            {
                enabled = false;
            } else
            {
                playerState = GetComponent<NetworkPlayerState>();
            }
        }
    }
   

}
