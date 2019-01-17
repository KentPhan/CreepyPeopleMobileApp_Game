using ExitGames.Client.Photon;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;

namespace Assets.Scripts.Mobile.Managers
{
    public enum PhotonEventCodes
    {
        MOVE_POSITION = 0,
        FLASH_LIGHT_TOGGLE = 1,
        FLASH_LIGHT_POWER = 2,
        INVENTORY_STATUS = 3
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

        #region -= Events =-

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
                        OnReceivePosition(photonEvent);
                        break;
                    }
                case (byte)PhotonEventCodes.FLASH_LIGHT_POWER:
                    {
                        OnReceivePhonePower(photonEvent);
                        break;
                    }
            }
        }

        public void OnReceivePosition(EventData i_photonEvent)
        {
            object[] l_data = (object[])i_photonEvent.CustomData;
            Vector3 l_dataPosition = (Vector3)l_data[0];
            MobileCanvasManager.Instance.SetTransformText(
                $"( {l_dataPosition.x} , {l_dataPosition.y} , {l_dataPosition.z} )");
        }

        public void OnReceivePhonePower(EventData i_photonEvent)
        {
            object[] l_data = (object[])i_photonEvent.CustomData;
            float l_dataRatio = (float)l_data[0];
        }

        #endregion

        #region -= RaiseEvents =-

        public void ToggleFlashLight(bool i_newState)
        {
            object[] l_content = new object[] { i_newState };
            RaiseEventOptions l_eventOptions = new RaiseEventOptions() { Receivers = ReceiverGroup.All };
            SendOptions l_sendOptions = new SendOptions() { Reliability = true };
            PhotonNetwork.RaiseEvent((byte)PhotonEventCodes.FLASH_LIGHT_TOGGLE, l_content, l_eventOptions, l_sendOptions);
        }

        #endregion
    }
}
