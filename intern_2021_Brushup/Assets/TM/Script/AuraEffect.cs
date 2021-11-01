using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TM
{
    public class AuraEffect : MonoBehaviour
    {

        [Header("タグ・レイヤー設定")]
        [SerializeField] private GameObject _target;  // 追跡するオブジェクト
        [SerializeField] private Vector3 _offset; // 地面のレイヤー番号
        [SerializeField] private SD.TitleStateManager _titleStateManager; // 階級マネージャー
        [SerializeField] private List<Color> _auraForRanks; // 階級に対する色
        [SerializeField] private int _lapseFrame; // オーラ消滅までのフレーム
        [SerializeField] private AnimationCurve _blurCurve_; // ブラーの遷移曲線
        private int _lapseFrameCnt; // オーラ消滅までのカウント
        private Material _mat; // オーラマテリアル
        private int _prevTitleRank;

        private void Start()
        {
            var renderer = GetComponentInChildren<Renderer>();
            _mat = renderer.material;
            _prevTitleRank = -1;
        }

        private void FixedUpdate()
        {
            int titleRank = _titleStateManager.TitleRank;
            if (titleRank != _prevTitleRank)
            {
                if (_auraForRanks.Count > titleRank)
                {
                    var auraColor = _auraForRanks[titleRank];
                    _mat.SetColor("_Color", auraColor);
                    _lapseFrameCnt = _lapseFrame;
                }
                _prevTitleRank = titleRank;
            }

            _lapseFrameCnt = System.Math.Max(_lapseFrameCnt - 1, 0);
            float t = _lapseFrameCnt / (float)_lapseFrame;
            _mat.SetFloat("_Blur", _blurCurve_.Evaluate(t));

            if (_target)
            {
                var targetPos = _target.transform.position;

                transform.position = targetPos + _offset;

                //// 追尾対象の下方向にレイを飛ばして接地点に地震を移動
                //RaycastHit hit;
                //if (Physics.Raycast(targetPos, Vector3.down, out hit, Mathf.Infinity, 1 << _groundLayer))
                //{
                //    transform.position = hit.point;
                //}
            }
            else
            {
                transform.position = new Vector3(99999.0f, 99999.0f, 99999.0f);
            }
        }
    }
}