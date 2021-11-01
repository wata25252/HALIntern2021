using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TM
{
    public class PlayerTumblingDecision : MonoBehaviour
    {
        [SerializeField, Range(0, 1)] private float _tumblingAngle;  // 転倒判定となる角度
        [SerializeField] private uint _tumblingDecisionFrame;    // 転倒判定になるまでの時間
        [SerializeField] private bool _isTumbled = false;
        private uint frameCnt_ = 0;

        public bool IsTumbled => _isTumbled;

        private void FixedUpdate()
        {
            if (Mathf.Abs(Vector3.Dot(transform.right, Vector3.up)) >= _tumblingAngle)
            {
                if(frameCnt_++ >= _tumblingDecisionFrame)
                {
                    _isTumbled = true;
                    Debug.Log("倒れた");
                }
            }
            else
            {
                frameCnt_ = 0;
            }
        }
    }
}