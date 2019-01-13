using Photon.Pun;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Main.Managers
{
    public class MainNetworkManager : MonoBehaviourPunCallbacks
    {
        private byte maxPlayersPerRoom = 2;

        public string versionName = "0.1";
        public Text connectionsCount;

        public static MainNetworkManager Instance;

        private void Awake()
        {
            if (Instance == null)
                Instance = this;

            else if (Instance != this)
                Destroy(gameObject);

            DontDestroyOnLoad(gameObject);
        }

        // Start is called before the first frame update
        void Start()
        {
            ConnectToNetwork();
        }

        // Update is called once per frame
        void Update()
        {
            if (PhotonNetwork.InRoom)
            {

            }
        }

        public void ConnectToNetwork()
        {
            PhotonNetwork.ConnectUsingSettings();
            Debug.Log("Connecting to Network...");
        }


        public override void OnConnectedToMaster()
        {
            PhotonNetwork.CreateRoom("One");
            MainCanvasManager.Instance.SetConnectionStatusText("Connected");
            Debug.Log("Connected to Master");
        }



        public override void OnJoinedLobby()
        {
            Debug.Log("Joined Lobby");
        }

        private void OnFailedToConnectToPhoton()
        {
            MainCanvasManager.Instance.SetConnectionStatusText("Disconnected");
            Debug.Log("Disconnnected from Network...");
        }
    }
}
