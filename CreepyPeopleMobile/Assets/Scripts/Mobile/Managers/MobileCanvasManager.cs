using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Mobile.Managers
{
    public enum PhoneStates
    {
        HOME,
        MAP,
        NO_POWER
    }

    public class MobileCanvasManager : MonoBehaviour
    {
        public static MobileCanvasManager Instance;

        // Screens
        [SerializeField] private RectTransform HomeScreen;
        [SerializeField] private RectTransform MappAppScreen;
        [SerializeField] private RectTransform NoPowerScreen;

        // Debug Texts
        [SerializeField] private TextMeshProUGUI ConnectionText;
        [SerializeField] private TextMeshProUGUI TransformText;

        // Buttons
        [SerializeField] private Toggle FlashLightToggle;
        [SerializeField] private Button MapButton;
        [SerializeField] private Button HomeButton;

        // Power Bar
        [SerializeField] private RectTransform PowerInnerBar;
        private float m_PowerBarMaxWidth;

        // Inventory
        [SerializeField] private RectTransform InventorySection;

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
                if (m_CurrentPhoneState != PhoneStates.NO_POWER)
                    SwitchToHome(HomeButton);
            });

            // Get Power Bar Max Width based upon how it is set
            m_PowerBarMaxWidth = PowerInnerBar.rect.width;

            // Set initial inventory
            InventorySection.GetChild(0).gameObject.SetActive(false);

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

        public void UpdatePowerBar(float i_Ratio)
        {
            Rect l_currentRect = PowerInnerBar.rect;
            float l_newWidth = i_Ratio * m_PowerBarMaxWidth;

            if (m_CurrentPhoneState != PhoneStates.NO_POWER)
            {
                if (l_newWidth <= 0)
                {
                    SwitchToNoPower();
                    l_newWidth = 0;
                }
                PowerInnerBar.sizeDelta = new Vector2(l_newWidth, l_currentRect.height);
            }
            else
            {
                if (l_newWidth > 0)
                {
                    SwitchToHome(HomeButton);
                }
            }



        }

        /// <summary>
        /// Updates the inventory. TODO Very basic right now. Only one item
        /// </summary>
        /// <param name="i_HasKey">if set to <c>true</c> [i has key].</param>
        public void UpdateInventory(bool i_HasKey)
        {
            if (i_HasKey)
                InventorySection.GetChild(0).gameObject.SetActive(true);
            else
                InventorySection.GetChild(0).gameObject.SetActive(false);
        }

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
                NoPowerScreen.gameObject.SetActive(false);
                MappAppScreen.gameObject.SetActive(true);
                HomeScreen.gameObject.SetActive(false);
            }
        }

        private void SwitchToHome(Button i_Button)
        {
            m_CurrentPhoneState = PhoneStates.HOME;
            NoPowerScreen.gameObject.SetActive(false);
            MappAppScreen.gameObject.SetActive(false);
            HomeScreen.gameObject.SetActive(true);
        }

        private void SwitchToNoPower()
        {
            m_CurrentPhoneState = PhoneStates.NO_POWER;
            NoPowerScreen.gameObject.SetActive(true);
            MappAppScreen.gameObject.SetActive(false);
            HomeScreen.gameObject.SetActive(false);
        }

        #endregion
    }
}
