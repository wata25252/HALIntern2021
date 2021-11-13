/*-------------------------------------------------------
 * 
 *      [GameCamera.cs]
 *      ゲーム終了したときのカメラ
 *      Author : 出合翔太
 * 
 --------------------------------------------------------*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

namespace SD
{
    public class GameCamera : MonoBehaviour
    {
        private TK.GameManager _gameManager;
        private GameObject _playerCameralook;
        // Start is called before the first frame update
        void Start()
        {
            _gameManager = GameObject.Find("GameManager").GetComponent<TK.GameManager>();
            _playerCameralook = GameObject.Find("Player/CameraLookPosition");
        }

        // Update is called once per frame
        void Update()
        {

        }

        private void FixedUpdate()
        {
            // ゲームが終了したら
            if(_gameManager.IsGameEnd())
            {                
                // 非アクティブ
                this.gameObject.SetActive(false);
            }
        }
    }
}
