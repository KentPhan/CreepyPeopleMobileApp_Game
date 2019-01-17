using UnityEngine;

namespace Assets.Scripts.Mobile.Managers
{
    public class MobileGameManager : MonoBehaviour
    {
        public static MobileGameManager Instance;


        private bool FlashLightOn = false;

        private GameObject PlayerRepresentationPrefab;

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
    }
}
