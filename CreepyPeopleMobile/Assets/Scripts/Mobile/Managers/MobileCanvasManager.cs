using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Mobile.Managers
{
    public class MobileCanvasManager : MonoBehaviour
    {
        public static MobileCanvasManager Instance;

        [SerializeField] private Text ConnectionText;
        [SerializeField] private Text TransformText;

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

        public void SetConnectionStatusText(string i_text)
        {
            ConnectionText.text = i_text;
        }

        public void SetTransformText(string i_text)
        {
            TransformText.text = i_text;
        }
    }
}
