/*-------------------------------------------------------
 * 
 *      [ResultCameraLookPosition.cs]
 *      リザルトカメラが追尾する位置
 *      Author : 出合翔太
 * 
 -------------------------------------------------------*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SD
{
    public class ResultCameraLookPosition : MonoBehaviour
    {
        private TK.GameManager _gm;
        private GameObject _player;
        private float _Height;
        // Start is called before the first frame update
        void Start()
        {
            _gm = GameObject.Find("GameManager").GetComponent<TK.GameManager>();
            _player = GameObject.Find("Player/CameraLookPosition");
            _Height = 6.5f;
        }

        // Update is called once per frame
        void Update()
        {
            
        }

        private void FixedUpdate()
        {         
            // ゲームが終了したら、回転する
            if (_gm.IsGameEnd())
            {
                this.gameObject.transform.Rotate(new Vector3(0.0f, 0.05f, 0.0f));
            }
            else
            {
                // 位置をプレイヤーに合わせる
                Vector3 _playerPosition = _player.transform.position;
                this.gameObject.transform.position = new Vector3(_playerPosition.x, _Height, _playerPosition.z);
                this.gameObject.transform.rotation = _player.transform.rotation;
            }
        }
    }
}
