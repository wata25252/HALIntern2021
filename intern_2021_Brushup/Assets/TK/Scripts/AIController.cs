using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//======================================
//TM.PlayerControllerをAI用に編集しています。
//======================================
namespace TM
{
    [RequireComponent(typeof(Player))]
    public class AIController : MonoBehaviour
    {
        //周回範囲
        [Header("AI設定")]
        [SerializeField] private float _circumferenceRange = 10.0f;
        //方向
        private float _autoHorizontalPower = 0.0f;
        private bool inArea = true;//行動範囲内かどうか 


        // ステータス
        [Header("ステータス")]
        [SerializeField] private uint _maxCrewCount;    // ステータスに影響する最大の乗員数

        [SerializeField, Range(0, 10)] private float _minSourceTorqueFactor;    // 基底トルクの最小値
        [SerializeField, Range(0, 10)] private float _maxSourceTorqueFactor;    // 基底トルクの最大値

        [SerializeField, Range(0, 100)] private float _pitchTorqueFactor;      // 前進時の回転トルク係数
        [SerializeField, Range(-100, 100)] private float _yawTorqueFactor;     // 旋回時の回転トルク係数
        [SerializeField, Range(-100, 100)] private float _rollTorqueFactor;    // 旋回時の転倒補正回転トルク係数

        [SerializeField] private AnimationCurve _pitchTorqueCurve;  // 前進時の回転トルク係数の変動曲線
        [SerializeField] private AnimationCurve _yawTorqueCurve;    // 旋回時の回転トルク係数の変動曲線
        [SerializeField] private AnimationCurve _rollTorqueCurve;   // 旋回時の転倒補正回転トルク係数の変動曲線

        // 入力
        [Header("入力")]
        [SerializeField] private string _verticalAxisInputName = "Vertical";        // 上下の軸入力の名前
        [SerializeField] private string _horizontalAxisInputName = "Horizontal";    // 左右の軸入力の名前

        private float _verticalAxisInput = 0.0f;    // 上下の軸入力値
        private float _horizontalAxisInput = 0.0f;  // 左右の軸入力値
        private Player _player; // 親コンポーネント
        private Rigidbody _rb;  // 剛体

        // それぞれの軸のトルク係数(0~1)
        private float PitchTorque => _pitchTorqueFactor * CalcTorqueFactor(_pitchTorqueCurve);
        private float YawTorque => _yawTorqueFactor * CalcTorqueFactor(_yawTorqueCurve);
        private float RollTorque => _rollTorqueFactor * CalcTorqueFactor(_rollTorqueCurve);

        public Vector3 _projectForward { get; set; }

        //AI用追加分====================================
        //倒れ検知用
        [SerializeField] private TK.Compass RightCompass;
        [SerializeField] private TK.Compass LeftCompass;
        //============================================


        private void Start()
        {
            _player = GetComponent<Player>();
            _rb = GetComponent<Rigidbody>();
        }

        private void Update()
        {

        }

        private void FixedUpdate()
        {



            // 前回転
            _rb.AddTorque(transform.right * PitchTorque * _verticalAxisInput * 100.0f);

            // ターン
            var projectUp = Vector3.ProjectOnPlane(Vector3.up, transform.right);
            _rb.AddTorque(projectUp * YawTorque * _horizontalAxisInput);
            //Debug.DrawLine(transform.position, transform.position + projectUp * 100.0f, Color.green);   // デバッグ

            // 倒れる力
            var vel = _rb.velocity;
            _projectForward = Vector3.ProjectOnPlane(vel, Vector3.up);
            _rb.AddTorque(vel * RollTorque * _horizontalAxisInput);
            //Debug.DrawLine(transform.position, transform.position + _projectForward * 100.0f, Color.blue);   // デバッグ

            // AI入力部分＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝
            AIInput();


            // ----- これ以降デバッグ用処理 -----

            //Debug .DrawLine(transform.position, transform.position + vel, Color.red);

            //if (Input.GetKey(KeyCode.O) && _player.CrewCount < _maxCrewCount)
            //{
            //    ++_player.CrewCount;
            //}
            //if (Input.GetKey(KeyCode.L) && _player.CrewCount > 0)
            //{
            //    --_player.CrewCount;
            //}
        }

        /// <summary>
        /// トルク係数を求める
        /// </summary>
        /// <param name="curve">変動曲線</param>
        /// <returns>トルク係数</returns>
        private float CalcTorqueFactor(AnimationCurve curve)
        {
            float factor = Mathf.Clamp01((float)_player.CrewCount / _maxCrewCount);
            return curve.Evaluate(factor) * (_maxSourceTorqueFactor - _minSourceTorqueFactor) + _minSourceTorqueFactor;
        }
        private void AIInput()
        {
            _verticalAxisInput = 0.5f;

            //コンパスが触れている＝倒れそうなときは立て直し優先
            if (RightCompass._isHitGround)
            {
                _horizontalAxisInput = -1;
            }
            else if (LeftCompass._isHitGround)
            {
                _horizontalAxisInput = 1;
            }
            //それ以外の時は外に行き過ぎていたら旋回
            else
            {
                //行動範囲外＆外側を向いているとき
                if (transform.position.magnitude >= _circumferenceRange
                    && Vector3.Dot(_projectForward, new Vector3(-transform.position.x, -transform.position.y, -transform.position.z)) < 0)
                {
                    if (inArea)
                    {
                        switch (Random.RandomRange(0,1))
                        {
                            case 0:
                                _autoHorizontalPower = -1.0f;
                                break;
                            case 1:
                                _autoHorizontalPower = 1.0f;

                                break; 
                        }
                    }
                    inArea = false;
                    _horizontalAxisInput = _autoHorizontalPower;
                    _verticalAxisInput = 0.1f;

                    Debug.Log("kabedayo");
                }
                else//行動範囲内の時
                {
                    inArea = true;
                    _horizontalAxisInput = 0.0f;
                }
            }
        }
    }


}


