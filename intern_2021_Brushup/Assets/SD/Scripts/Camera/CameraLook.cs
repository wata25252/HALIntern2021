/*-------------------------------------------------------
 * 
 *      [CameraLook.cs]
 *      カメラが見る位置
 *      Author : 出合翔太
 * 
 -------------------------------------------------------*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SD
{
    public class CameraLook : MonoBehaviour
    {
        // プレイヤーの設定
        [Header("プレイヤーのオブジェクト")]
        [SerializeField] private GameObject _player;

        [Header("カメラの補正(大きいほど補正が小さくなる) 1～10")]
        [SerializeField, Range(1.0f,10.0f)] private float _forwardScolar = 10.0f;

        private Transform _playerTransform;
        private Vector3 _forward;
        private Vector3 _right;
        private Vector3 _position;

        private float _LookHeight;  // 高さ

        // Start is called before the first frame update
        void Start()
        {
            // 位置を初期化
            _playerTransform = _player.transform;
            _forward = Vector3.forward;
            _LookHeight = 1.0f;
            this.transform.position = new Vector3(0.0f, 3.0f + _LookHeight, 0.0f);     
        }

        // Update is called once per frame
        void Update()
        {
            
        }

        private void FixedUpdate()
        {
            // プレイヤーのnullチェック
            var nullCheck = _player?.activeInHierarchy; 
            
            // 位置を更新
            _position = _playerTransform.position; // プレイヤーの位置
            this.transform.position = new Vector3(_position.x, _position.y + _LookHeight, _playerTransform.position.z);

            // 進む方向のベクトル
            _forward = _player.GetComponent<TM.PlayerController>().DirectionTravel;
            _forward.Normalize(); // 正規化

            // 左右の入力値を取得
            float inputRight = _player.GetComponent<TM.PlayerController>().HorizontalInput;
            
            // 横方向のベクトルを求める
            // 入力値をかけて、右か左か決める
            _right = this.transform.right * inputRight;
            _right.Normalize(); // 正規化
            
            Debug.DrawLine(this.transform.position, this.transform.position+ _right * 100.0f, Color.cyan);

            // カメラを向けるベクトルを求める
            var dirLook = (_forward * _forwardScolar) + _right;

            Debug.DrawLine(this.transform.position, this.transform.position + dirLook * 100.0f, Color.cyan);

            // このオブジェクトの前ベクトルが進む方向を向くように回転させる
            Quaternion quaternion = Quaternion.identity;
            // 前ベクトルが更新されたら、回転する
            if (dirLook!= Vector3.zero)
            {
                quaternion = Quaternion.LookRotation(dirLook);
            }
            // 線形補間を使った回転
            this.transform.rotation = Quaternion.Lerp(this.transform.rotation, quaternion, 1.0f);
        }
    }
}
