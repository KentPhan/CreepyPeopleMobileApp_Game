using ExitGames.Client.Photon;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;

namespace Assets.Scripts.Mobile.Managers
{
    public enum PhotonEventCodes
    {
        PLAYER_TRANSFORM = 0,
        FLASH_LIGHT_TOGGLE = 1,
        FLASH_LIGHT_POWER = 2,
        INVENTORY_STATUS = 3,
        GAME_STATE = 4,
        ENEMY_POSITIONS = 5
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

        public override void OnPlayerEnteredRoom(Player newPlayer)
        {
            // Put logic for disconnecting or disconnecting
            base.OnPlayerEnteredRoom(newPlayer);
        }

        public override void OnPlayerLeftRoom(Player otherPlayer)
        {
            // Put logic for disconnecting or disconnecting
            base.OnPlayerLeftRoom(otherPlayer);
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
                case (byte)PhotonEventCodes.PLAYER_TRANSFORM:
                    {
                        OnReceiveTransform(photonEvent);
                        break;
                    }
                case (byte)PhotonEventCodes.FLASH_LIGHT_POWER:
                    {
                        OnReceivePhonePower(photonEvent);
                        break;
                    }
                case (byte)PhotonEventCodes.INVENTORY_STATUS:
                    {
                        OnReceiveInventoryStatus(photonEvent);
                        break;
                    }
                case (byte)PhotonEventCodes.GAME_STATE:
                    {
                        OnReceiveGameState(photonEvent);
                        break;
                    }
            }
        }

        public void OnReceiveTransform(EventData i_photonEvent)
        {
            object[] l_data = (object[])i_photonEvent.CustomData;
            Vector3 l_dataPosition = (Vector3)l_data[0];
            Quaternion l_dataRotation = (Quaternion)l_data[1];
            MobileCanvasManager.Instance.SetTransformText(
                $"( {l_dataPosition.x} , {l_dataPosition.y} , {l_dataPosition.z} )");

            MobileGameManager.Instance.UpdatePlayerTransform(l_dataPosition, l_dataRotation);
        }

        public void OnReceivePhonePower(EventData i_photonEvent)
        {
            object[] l_data = (object[])i_photonEvent.CustomData;
            float l_dataRatio = (float)l_data[0];
            MobileCanvasManager.Instance.UpdatePowerBar(l_dataRatio);
        }

        public void OnReceiveInventoryStatus(EventData i_photonEvent)
        {
            // TODO Incorporate more items
            object[] l_data = (object[])i_photonEvent.CustomData;
            bool l_dataHasKey = (bool)l_data[0];
            MobileCanvasManager.Instance.UpdateInventory(l_dataHasKey);
        }

        public void OnReceiveGameState(EventData i_photonEvent)
        {
            object[] l_data = (object[])i_photonEvent.CustomData;
            GameStates l_dataState = (GameStates)l_data[0];
            MobileGameManager.Instance.UpdateGameState(l_dataState);
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
