using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Main.Managers
{
    public class MainCanvasManager : MonoBehaviour
    {
        public static MainCanvasManager Instance;

        public Text ConnectionText;

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
    }
}
