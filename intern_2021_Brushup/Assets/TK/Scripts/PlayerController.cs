using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TK
{
    [RequireComponent(typeof(Rigidbody))]
    public class PlayerController : MonoBehaviour
    {
        private Rigidbody _rigidbody;
        
        //速度入力受け取り用
        private float _inputSpeedZ;

        //速度
        [SerializeField]
        private float _speedScale;

        //収容人数
        [SerializeField]
        public int _humanCount { get; set; }

        [SerializeField]
        public int _power { get; set; }
        // Start is called before the first frame update
        private void Start()
        {
            _rigidbody = GetComponent<Rigidbody>();
        }

        // Update is called once per frame
        private void Update()
        {
            //入力
            if (Input.GetKeyDown(KeyCode.W))
            {
                _inputSpeedZ = 1.0f;
            }
            if (Input.GetKeyDown(KeyCode.U))
            {
                _power++;
            }
            if (Input.GetKeyDown(KeyCode.I))
            {
                _humanCount++;
            }
        }

        private void FixedUpdate()
        {
            _rigidbody.AddForce(0, 0, _inputSpeedZ);
        }

        public void Test()
        {
            Debug.Log("test");
        }

    }
}