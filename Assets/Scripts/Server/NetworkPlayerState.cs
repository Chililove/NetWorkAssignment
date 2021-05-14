using MLAPI;
using MLAPI.Messaging;
using MLAPI.NetworkedVar;
using System;
using UnityEngine;

namespace Assets.Scripts.Server
{
    public class NetworkPlayerState: NetworkedBehaviour
    {
        public event Action<bool> RecievedShootingEvent;
        public NetworkedVarFloat health = new NetworkedVarFloat(100f);
        // bool shooting = false;
        
        [ServerRPC]
        public void ShootingServerRPC()
        {
            RecievedShootingEvent.Invoke(true);
        }
    }
}
