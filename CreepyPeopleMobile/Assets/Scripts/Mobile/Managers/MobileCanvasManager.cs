using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Mobile.Managers
{
    public class MobileCanvasManager : MonoBehaviour
    {
        public static MobileCanvasManager Instance;

        [SerializeField] private TextMeshProUGUI ConnectionText;
        [SerializeField] private TextMeshProUGUI TransformText;
        [SerializeField] private Toggle FlashLightToggle;

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
            FlashLightToggle.onValueChanged.AddListener(delegate
            {
                OnFlashLightToggle(FlashLightToggle);
            });
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

        public void OnFlashLightToggle(Toggle i_change)
        {
            MobileNetworkManager.Instance.ToggleFlashLight(i_change.isOn);

            ColorBlock l_ColorBlock = i_change.colors;
            l_ColorBlock.normalColor = i_change.isOn ? Color.grey : Color.white;
            l_ColorBlock.highlightedColor = i_change.isOn ? Color.grey : Color.white;
            i_change.colors = l_ColorBlock;
        }
    }
}
