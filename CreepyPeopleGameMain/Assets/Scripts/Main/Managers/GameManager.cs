using System;
using UnityEngine;

namespace Assets.Scripts.Main.Managers
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField]
        private GameObject PlayerPrefab;
        [SerializeField]
        private GameObject SpawnPosition;

        public static GameManager Instance;

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
            return PlayerPrefab;
        }

        public Transform GetSpawnPosition()
        {
            return SpawnPosition.transform;
        }
    }
}
