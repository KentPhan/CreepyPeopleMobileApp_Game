using UnityEngine;

namespace Assets.Scripts.Mobile.Managers
{
    public class MobileGameManager : MonoBehaviour
    {
        public static MobileGameManager Instance;


        private bool FlashLightOn = false;

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
            return null;
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
