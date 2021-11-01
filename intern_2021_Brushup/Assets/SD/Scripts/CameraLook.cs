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

        private Transform _playerTransform;
        private Vector3 _forward;
        private Vector3 _position;

        private float _LookHeight;  // 高さ
        private float _LookForward; // 前方向 

        // Start is called before the first frame update
        void Start()
        {
            _playerTransform = _player.transform;
            _forward = Vector3.forward;
            _LookHeight = 1.0f;
            _LookForward = 6.0f;
            this.transform.position = new Vector3(0.0f, 3.0f + _LookHeight, 0.0f);     
        }

        // Update is called once per frame
        void Update()
        {
            
        }

        private void FixedUpdate()
        {
            var nullCheck = _player?.activeInHierarchy;
            
            _forward = _player.GetComponent<TM.PlayerController>().CameraForward;
            _position = _playerTransform.position; // プレイヤーの位置
            this.transform.position = new Vector3(_position.x, _position.y + _LookHeight, _playerTransform.position.z);

            Quaternion quaternion = Quaternion.identity;
            if (_forward != Vector3.zero)
            {
                quaternion = Quaternion.LookRotation(_forward);
            }
            this.transform.rotation = Quaternion.Lerp(this.transform.rotation, quaternion, 1.0f);
        }
    }
}
