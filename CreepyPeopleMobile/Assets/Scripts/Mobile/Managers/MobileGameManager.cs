using System;
using UnityEngine;

namespace Assets.Scripts.Mobile.Managers
{
    public enum GameStates
    {
        START = 0,
        PLAY = 1,
        GAMEOVER = 2
    }

    public class MobileGameManager : MonoBehaviour
    {
        public static MobileGameManager Instance;


        private bool FlashLightOn = false;

        [SerializeField]
        private GameObject PlayerRepresentationPrefab;

        private GameStates m_CurrentGameState;

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

        }

        // Update is called once per frame
        void Update()
        {

        }

        public GameObject GetPlayerPrefab()
        {
            return PlayerRepresentationPrefab;
        }

        public void UpdatePlayerTransform(Vector3 i_Position, Quaternion i_Rotation)
        {
            PlayerRepresentationPrefab.transform.position = i_Position;
            PlayerRepresentationPrefab.transform.rotation = i_Rotation;
        }

        public void ToggleFlashLight()
        {
            if (FlashLightOn)
            {
                FlashLightOn = false;
            }
            else
            {
                FlashLightOn = true;
            }
        }

        public void UpdateGameState(GameStates i_State)
        {
            m_CurrentGameState = i_State;
            switch (i_State)
            {
                case GameStates.START:
                    MobileCanvasManager.Instance.SwitchToNoPower();
                    break;
                case GameStates.PLAY:
                    MobileCanvasManager.Instance.SwitchToHome();
                    break;
                case GameStates.GAMEOVER:
                    MobileCanvasManager.Instance.SwitchToNoPower();
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(i_State), i_State, null);
            }
        }
    }
}
