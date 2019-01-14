using ExitGames.Client.Photon;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;

namespace Assets.Scripts.Mobile.Managers
{
    public enum PhotonEventCodes
    {
        MOVE_POSITION = 0,
        FLASH_LIGHT = 1
    }

    public class MobileNetworkManager : MonoBehaviourPunCallbacks, IOnEventCallback
    {
        private byte maxPlayersPerRoom = 2;

        public string versionName = "0.1";

        public static MobileNetworkManager Instance;

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
            // Only for testing
            MobileCanvasManager.Instance.SetConnectionStatusText(PhotonNetwork.NetworkClientState.ToString());
        }

        #region -= ConnectionToRoom =-

        public void ConnectToNetwork()
        {
            PhotonNetwork.ConnectUsingSettings();
            Debug.Log("Connecting to Network...");
        }


        public override void OnConnectedToMaster()
        {
            PhotonNetwork.JoinOrCreateRoom("One", null, null);

            Debug.Log("Connected to Master");
        }

        public override void OnJoinedLobby()
        {
            Debug.Log("Joined Lobby");
        }

        public override void OnJoinedRoom()
        {
            Debug.Log("Joined Room");
        }

        private void OnFailedToConnectToPhoton()
        {
            Debug.Log("Disconnected from Network...");
        }

        #endregion

        #region -= Event Shit =-

        public override void OnEnable()
        {
            PhotonNetwork.AddCallbackTarget(this);
        }

        public override void OnDisable()
        {
            PhotonNetwork.RemoveCallbackTarget(this);
        }

        public void OnEvent(EventData photonEvent)
        {
            switch (photonEvent.Code)
            {
                case (byte)PhotonEventCodes.MOVE_POSITION:
                    {
                        object[] l_data = (object[])photonEvent.CustomData;
                        Vector3 l_dataPosition = (Vector3)l_data[0];
                        MobileCanvasManager.Instance.SetTransformText(
                            $"( {l_dataPosition.x} , {l_dataPosition.y} , {l_dataPosition.z} )");
                        break;
                    }
            }
        }

        #endregion
    }
}
