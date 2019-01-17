using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Mobile.Managers
{
    public enum PhoneStates
    {
        HOME,
        MAP
    }

    public class MobileCanvasManager : MonoBehaviour
    {
        public static MobileCanvasManager Instance;

        // Screens
        [SerializeField] private RectTransform HomeScreen;
        [SerializeField] private RectTransform MappAppScreen;

        // Debug Texts
        [SerializeField] private TextMeshProUGUI ConnectionText;
        [SerializeField] private TextMeshProUGUI TransformText;

        // Buttons
        [SerializeField] private Toggle FlashLightToggle;
        [SerializeField] private Button MapButton;
        [SerializeField] private Button HomeButton;

        // Members
        private PhoneStates m_CurrentPhoneState;

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

            // Register events
            FlashLightToggle.onValueChanged.AddListener(delegate
            {
                OnFlashLightToggle(FlashLightToggle);
            });
            MapButton.onClick.AddListener(delegate
            {
                SwitchToMap(MapButton);
            });
            HomeButton.onClick.AddListener(delegate
            {
                SwitchToHome(HomeButton);
            });

            // Set initial state
            SwitchToHome(HomeButton);
        }

        // Update is called once per frame
        void Update()
        {

        }

        public void SetConnectionStatusText(string i_text)
        {
            ConnectionText.text = i_text;
        }

        public void SetTransformText(string i_text)
        {
            TransformText.text = i_text;
        }

        #region -= Events =-

        private void OnFlashLightToggle(Toggle i_Toggle)
        {
            MobileNetworkManager.Instance.ToggleFlashLight(i_Toggle.isOn);

            ColorBlock l_ColorBlock = i_Toggle.colors;
            l_ColorBlock.normalColor = i_Toggle.isOn ? Color.grey : Color.white;
            l_ColorBlock.highlightedColor = i_Toggle.isOn ? Color.grey : Color.white;
            i_Toggle.colors = l_ColorBlock;
        }

        private void SwitchToMap(Button i_Button)
        {
            if (m_CurrentPhoneState != PhoneStates.MAP)
            {
                m_CurrentPhoneState = PhoneStates.MAP;
                MappAppScreen.gameObject.SetActive(true);
                HomeScreen.gameObject.SetActive(false);
            }
        }

        private void SwitchToHome(Button i_Button)
        {
            m_CurrentPhoneState = PhoneStates.HOME;
            MappAppScreen.gameObject.SetActive(false);
            HomeScreen.gameObject.SetActive(true);
        }

        #endregion
    }
}
