using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace UnityProject.Cookscape { 
    public class PhotonManager : MonoBehaviourPunCallbacks
    {
        // version
        private readonly string version = "1.0f";

        // User Id
        private string userId = "Hyeok";

        public Text StatusText;

        // Awake is called before start script
        private void Awake()
        {
            // Auto scene loading for same room users
            PhotonNetwork.AutomaticallySyncScene = true;

            // Permission for same version users
            PhotonNetwork.GameVersion = version;

            // Set user id
            PhotonNetwork.NickName = userId;

            // Set communictaion count with photoncloud server, default : 30/s
            Debug.Log(PhotonNetwork.SendRate);

            // Join server
            PhotonNetwork.ConnectUsingSettings();

            //Screen.SetResolution(960, 540, false);
        }

        // [callback] called when join photon server
        public override void OnConnectedToMaster()
        {
            Debug.Log("Connected to Master!");

            // Connected lobby : true
            Debug.Log($"PhotonNetwork.InLobby = {PhotonNetwork.InLobby}");

            // Join lobby
            PhotonNetwork.JoinLobby();
        }

        // [callback] called when join lobby
        public override void OnJoinedLobby()
        {
            // Connected lobby : true
            Debug.Log($"PhotonNetwork.InLobby = {PhotonNetwork.InLobby}");

            RoomOptions ro = new()
            {
                MaxPlayers = 5,      // maximum user
                IsOpen = true,       // open or close room
                IsVisible = true    // public or private room
            };

            PhotonNetwork.JoinOrCreateRoom("My Room", ro, null);
        }

        // [callback] called when created room
        public override void OnCreatedRoom()
        {
            Debug.Log("Created Room");
            Debug.Log($"Room Name : {PhotonNetwork.CurrentRoom.Name}");
        }

        // [callback] called when joined room
        public override void OnJoinedRoom()
        {
            Debug.Log("Joined Room");
            Debug.Log($"Room Name : {PhotonNetwork.CurrentRoom.Name}");
            Debug.Log($"Player Count : {PhotonNetwork.CurrentRoom.PlayerCount}");

            // User info in room
            foreach(var player in PhotonNetwork.CurrentRoom.Players)
            {
                // Nickname & user origin number
                Debug.Log($"{player.Value.NickName}, {player.Value.ActorNumber }");
            }

            // Save character information in array
            Transform[] points = GameObject.Find("SpwanPointGroup").GetComponentsInChildren<Transform>();
            int idx = Random.Range(1, points.Length);

            // create character
            PhotonNetwork.Instantiate("Prefabs/CarrotShef", points[idx].position, points[idx].rotation, 0);
        }

        // [callback] called when failed join room
        public override void OnJoinRoomFailed(short returnCode, string message)
        {
            Debug.Log($"Join Room Failed {returnCode} : {message}");
        }

        // Start is called before the first frame update
        private void Start()
        {
            
        }


        // update is called once per frame
        private void Update()
        {

        }

        //readonly string gameVersion = "0.1";
        //string userId = "kennen";

        //Dictionary<string, GameObject> roomDictionary = new Dictionary<string, GameObject>();

        //public GameObject roomPrefab = null;
        //public Transform scrollContent;

        //private void Awake()
        //{
        //    PhotonNetwork.AutomaticallySyncScene = true;
        //    PhotonNetwork.GameVersion = gameVersion;
        //    PhotonNetwork.ConnectUsingSettings();
        //}

        //// Start is called before the first frame update
        //void Start()
        //{
        //    Debug.Log("00. start photonManager...");

        //    userId = PlayerPrefs.GetString("USER_ID", $"USER_{Random.Range(0, 100)}");
        //    PhotonNetwork.NickName = userId;
        //}

        //public override void OnConnectedToMaster()
        //{
        //    Debug.Log("01. success... connected to master");
        //    PhotonNetwork.JoinLobby();
        //}

        //public override void OnJoinedLobby()
        //{
        //    Debug.Log("02. joined the Lobby...");
        //}

        //// Update is called once per frame
        //void Update()
        //{

        //}
    }
}
