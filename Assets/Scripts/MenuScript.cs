using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MLAPI;
using UnityEngine.UI;
using MLAPI.Transports.UNET;
using System;

namespace ChiliPixel
{
    public class MenuScript : MonoBehaviour
    {
        public GameObject menuPanel;
        public InputField inputField;

        public void Start()
        {
            NetworkingManager.Singleton.ConnectionApprovalCallback += Approvalcheck;
        }

        private void Approvalcheck(byte[] connectionData, ulong clientId, NetworkingManager.ConnectionApprovedDelegate callback)
        {
            bool approve = false;
            // if connection is correct the approve
            string password = System.Text.Encoding.ASCII.GetString(connectionData);
            if (password == "mygame")
            {
                //if the password is mygame, it will approve and join the game
                approve = true;
            }
            Debug.Log($"Approval: {approve}");
            callback(true, null, approve, new Vector3(0, 10, 0), Quaternion.identity);
        }

        public void Host()
        {
            NetworkingManager.Singleton.StartHost();
            menuPanel.SetActive(false);
        }

        public void Join()
        {
            //When Join is clicked
            if (inputField.text.Length <= 0)
            {
                NetworkingManager.Singleton.GetComponent<UnetTransport>().ConnectAddress = "127.0.0.1";

            }
            else
            {
                NetworkingManager.Singleton.GetComponent<UnetTransport>().ConnectAddress = inputField.text;
            }
            NetworkingManager.Singleton.NetworkConfig.ConnectionData = System.Text.Encoding.ASCII.GetBytes("mygame");
            NetworkingManager.Singleton.StartClient();
            menuPanel.SetActive(false);
        }
    }
}


