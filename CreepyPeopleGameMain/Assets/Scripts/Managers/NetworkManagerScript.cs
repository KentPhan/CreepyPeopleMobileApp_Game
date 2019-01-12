using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using UnityEngine.UI;

namespace Assets
{
    public class NetworkManagerScript : MonoBehaviourPunCallbacks
    {
        public string versionName = "0.1";

        public GameObject startScreen, connectedScreen, disconnectedScreen;
        public Text connectionsCount;


        private void OnGUI()
        {
            connectionsCount.text = PhotonNetwork.CountOfPlayers.ToString();
        }

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        public void ConnectToNetwork()
        {
            PhotonNetwork.ConnectUsingSettings();


            Debug.Log("Connecting to Network...");
        }


        public override void OnConnectedToMaster()
        {
            PhotonNetwork.JoinLobby(TypedLobby.Default);

            Debug.Log("Connected to Master");
        }



        public override void OnJoinedLobby()
        {
            startScreen.SetActive(false);
            connectedScreen.SetActive(true);

            Debug.Log("Joined Lobby");
        }

        private void OnFailedToConnectToPhoton()
        {
            if (startScreen.activeSelf)
                startScreen.SetActive(false);

            if (connectedScreen.activeSelf)
                connectedScreen.SetActive(false);

            disconnectedScreen.SetActive(true);

            Debug.Log("Disconnnected from Network...");
        }
    }
}
