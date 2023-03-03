using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UnityProject.Cookscape { 
    public class PhotonManager : MonoBehaviourPunCallbacks
    {

        readonly string gameVersion = "0.1";
        string userId = "kennen";

        Dictionary<string, GameObject> roomDictionary = new Dictionary<string, GameObject>();
    
        public GameObject roomPrefab = null;
        public Transform scrollContent;

        private void Awake()
        {
            PhotonNetwork.AutomaticallySyncScene = true;
            PhotonNetwork.GameVersion = gameVersion;
            PhotonNetwork.ConnectUsingSettings();
        }

        // Start is called before the first frame update
        void Start()
        {
            Debug.Log("00. start photonManager...");

            userId = PlayerPrefs.GetString("USER_ID", $"USER_{Random.Range(0,100)}");
            PhotonNetwork.NickName = userId;
        }

        public override void OnConnectedToMaster()
        {
            Debug.Log("01. success... connected to master");
            PhotonNetwork.JoinLobby();
        }

        public override void OnJoinedLobby()
        {
            Debug.Log("02. joined the Lobby...");
        }

        // Update is called once per frame
        void Update()
        {
        
        }
    }
}
