using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


namespace TK
{
    public class DebugUI : MonoBehaviour
    {
        [SerializeField]
        private GameObject _textResource;

        [SerializeField]
        private Text _timeText;
        [SerializeField]
        private Text _humanCountText;

        [SerializeField]
        private GameObject _player;

        private PlayerController _playerController;

        [SerializeField]
        private GameObject _gameManager;


        private Vector4 _textColor = new Vector4(0.9f,0.9f,0.0f,1.0f);
        // Start is called before the first frame update
        void Start()
        {

            //オブジェクトの取得
            Transform canvas = GameObject.Find("Canvas").transform;
            _playerController = _player.GetComponent<PlayerController>();

        }

        // Update is called once per frame
        private void Update()
        {
            
        }

        private void FixedUpdate()
        {
            var nullCheck = _player?.activeInHierarchy;

            //収容人数表示
            _humanCountText.text = "収容人数 : " + _player.GetComponent<TM.Player>().CrewCount;



            ////時間表示
            _timeText.text = "残り時間　: " + _gameManager.GetComponent<GameManager>()._timeCount.ToString("F1");

        }
    }
}
