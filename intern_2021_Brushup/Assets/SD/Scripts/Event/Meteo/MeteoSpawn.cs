/*-------------------------------------------------------
 * 
 *      [MeteoSpawn.cs]
 *      隕石のスポーン
 *      Author : 出合翔太
 * 
 --------------------------------------------------------*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SD
{
    public class MeteoSpawn : MonoBehaviour
    {
        [Header("隕石")]
        [SerializeField] private GameObject _meteo;
      
        [Header("隕石の移動スピード")] 
        [SerializeField, Range(100.0f, 500.0f)]
        private float _speed = 200.0f;

        // ターゲットとなるオブジェクト
        private GameObject _player;
        private Vector3 _targetPosition;
        private Vector3 _targetPrePosition;
        private Vector3 _targetPrePosition2;

        // Start is called before the first frame update
        void Start()
        {
            _player = GameObject.Find("Player/CameraLookPosition");
        }

        // Update is called once per frame
        void Update()
        {

        }

        private void FixedUpdate()
        {
            // プレイヤーのNullチェック
            var playerNullCheck = _player?.activeSelf;

            // プレイヤーの方向を見る
            //this.transform.LookAt(_player.transform);

            // プレイヤーの位置を予測する
            _targetPosition = CirclePrediction(transform.position, _player.transform.position, _targetPrePosition, _targetPrePosition2, _speed);
            _targetPrePosition2 = _targetPrePosition;
            _targetPrePosition = _player.transform.position;
        }

        public void Spawn()
        {
            // インスタンスの生成
            GameObject meteo = Instantiate(_meteo, this.transform);

            // 力を加えて、飛ばす 
            meteo.GetComponent<Rigidbody>().AddForce((_targetPosition - this.transform.position).normalized * _speed, ForceMode.VelocityChange);
        }

        #region 予測射撃用のヘルパー関数
        //線形予測射撃
        private Vector3 LinePrediction(Vector3 shotPosition, Vector3 targetPosition, Vector3 targetPrePosition, float bulletSpeed)
        {
            //Unityの物理はm/sなのでm/flameにする
            bulletSpeed = bulletSpeed * Time.deltaTime;
            Vector3 v3_Mv = targetPosition - targetPrePosition;
            Vector3 v3_Pos = targetPosition - shotPosition;

            float A = Vector3.SqrMagnitude(v3_Mv) - bulletSpeed * bulletSpeed;
            float B = Vector3.Dot(v3_Pos, v3_Mv);
            float C = Vector3.SqrMagnitude(v3_Pos);

            //0割禁止
            if (A == 0 && B == 0) return targetPosition;
            if (A == 0) return targetPosition + v3_Mv * (-C / B / 2);

            //虚数解はどうせ当たらないので絶対値で無視した
            float D = Mathf.Sqrt(Mathf.Abs(B * B - A * C));
            return targetPosition + v3_Mv * PlusMin((-B - D) / A, (-B + D) / A);
        }

        //プラスの最小値を返す(両方マイナスなら0)
        private float PlusMin(float a, float b)
        {
            if (a < 0 && b < 0) return 0;
            if (a < 0) return b;
            if (b < 0) return a;
            return a < b ? a : b;
        }

        // 円形予測射撃
        private Vector3 CirclePrediction(Vector3 shotPosition, Vector3 targetPosition, Vector3 targetPrePosition, Vector3 targetPrePosition2, float bulletSpeed)
        {
            //3点の角度変化が小さい場合は線形予測に切り替え
            if (Mathf.Abs(Vector3.Angle(targetPosition - targetPrePosition, targetPrePosition - targetPrePosition2)) < 0.03)
            {
                return LinePrediction(shotPosition, targetPosition, targetPrePosition, bulletSpeed);
            }

            //Unityの物理はm/sなのでm/flameにする
            bulletSpeed = bulletSpeed * Time.deltaTime;

            //１、3点から円の中心点を出す
            Vector3 CenterPosition = Circumcenter(targetPosition, targetPrePosition, targetPrePosition2);

            //２、中心点から見た1フレームの角速度と軸を出す
            Vector3 axis = Vector3.Cross(targetPrePosition - CenterPosition, targetPosition - CenterPosition);
            float angle = Vector3.Angle(targetPrePosition - CenterPosition, targetPosition - CenterPosition);

            //３、現在位置で弾の到達時間を出す
            float PredictionFlame = Vector3.Distance(targetPosition, shotPosition) / bulletSpeed;

            //４、到達時間分を移動した予測位置で再計算して到達時間を補正する。
            for (int i = 0; i < 3; ++i)
            {
                PredictionFlame = Vector3.Distance(RotateToPosition(targetPosition, CenterPosition, axis, angle * PredictionFlame), shotPosition) / bulletSpeed;
            }

            return RotateToPosition(targetPosition, CenterPosition, axis, angle * PredictionFlame);
        }

        //三角形の頂点三点の位置から外心の位置を返す
        private Vector3 Circumcenter(Vector3 posA, Vector3 posB, Vector3 posC)
        {
            //三辺の長さの二乗を出す
            float edgeA = Vector3.SqrMagnitude(posB - posC);
            float edgeB = Vector3.SqrMagnitude(posC - posA);
            float edgeC = Vector3.SqrMagnitude(posA - posB);

            //重心座標系で計算する
            float a = edgeA * (-edgeA + edgeB + edgeC);
            float b = edgeB * (edgeA - edgeB + edgeC);
            float c = edgeC * (edgeA + edgeB - edgeC);

            if (a + b + c == 0) return (posA + posB + posC) / 3;//0割り禁止
            return (posA * a + posB * b + posC * c) / (a + b + c);
        }

        //目標位置をセンター位置で軸と角度で回転させた位置を返す
        private Vector3 RotateToPosition(Vector3 v3_target, Vector3 v3_center, Vector3 v3_axis, float f_angle)
        {
            return Quaternion.AngleAxis(f_angle, v3_axis) * (v3_target - v3_center) + v3_center;
        }
        #endregion
    }
}
