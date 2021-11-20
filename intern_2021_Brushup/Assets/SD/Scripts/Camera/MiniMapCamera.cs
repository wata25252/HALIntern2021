/*-------------------------------------------------------
 * 
 *      [MiniMapCamera.cs]
 *      ミニマップ用のカメラの制御
 *      Author : deaishota
 * 
 -------------------------------------------------------*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace SD
{
    public class MiniMapCamera : MonoBehaviour
    {
        private GameObject _player;
        private Vector3 _playerPosition;
        const float _offsetHeight = 40.0f;

        // Start is called before the first frame update
        void Start()
        {
            _player = GameObject.FindWithTag("Player");
        }

        // Update is called once per frame
        void Update()
        {
            
        }

        
        private void FixedUpdate()
        {
            // カメラの位置をプレイヤーの位置に合わせる
            _playerPosition = _player.transform.position;

            // y座標を補正する
            _playerPosition.y += _offsetHeight;

            // 位置を更新
            this.gameObject.transform.position = _playerPosition;
        }

    }
}