using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TM
{
    [RequireComponent(typeof(Player))]
    public class PlayerController : MonoBehaviour
    {
        //追加分
        private GameObject _gameManager;
        private SD.PlayerCollision _collision; 
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
        
        // 進む方向
        private Vector3 _direction_travel;

        // 左右の軸入力値
        public float HorizontalInput { get { return _horizontalAxisInput; } }

        // 進む方向
        public Vector3 DirectionTravel { get { return _direction_travel; } }


        private void Start()
        {
            _gameManager = GameObject.Find("GameManager");
            _collision = GetComponent<SD.PlayerCollision>();
            _player = GetComponent<Player>();
            _rb = GetComponent<Rigidbody>();
            
            // 最初に力を加える
            _rb.AddTorque(transform.right * PitchTorque * 100.0f);
        }

        private void Update()
        {

            if (_gameManager.GetComponent<TK.GameManager>().IsGameEnd()!=true)
            {
                // 入力取得
                _verticalAxisInput = Mathf.Max(Input.GetAxis(_verticalAxisInputName), 0.01f);
                _horizontalAxisInput = Input.GetAxis(_horizontalAxisInputName);
            }
            else
            {
                _verticalAxisInput = 0.0f;
                _horizontalAxisInput = 0.4f;
                if (!_collision.IsDead())//タイムアップ時の処理
                {
                    _collision.ShowResult();
                    _rb.drag = 0.0f;
                    _rb.mass = 1000.0f;
                    _rb.angularDrag = 0.0f;
                    _rb.AddTorque(new Vector3(0, 0, 1000));
                }
            }
        }

        private void FixedUpdate()
        {
            // 前回転
            _rb.AddTorque(transform.right * PitchTorque * _verticalAxisInput * 100.0f);

            // ターン
            var projectUp = Vector3.ProjectOnPlane(Vector3.up, transform.right);
            _rb.AddTorque(projectUp * YawTorque * _horizontalAxisInput);         

            // 倒れる力
            var vel = _rb.velocity;
            _rb.AddTorque(vel * RollTorque * _horizontalAxisInput);
    
            // 進む方向を求める
            _direction_travel = Vector3.ProjectOnPlane(vel, Vector3.up);
            Debug.DrawLine(transform.position, transform.position + _direction_travel * 100.0f, Color.magenta);
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
    }
}