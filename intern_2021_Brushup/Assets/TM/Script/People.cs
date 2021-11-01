using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

namespace TM
{

    public class People : MonoBehaviour
    {
        private TK.GameManager _gm;
        [Header("タグ設定")]
        [SerializeField] private string _playerTag; // プレイヤーのタグ名

        [Header("見た目")]
        [SerializeField] private List<GameObject> _humanModels;  // 人のモデル

        [Header("吸収パラメータ")]
        [SerializeField] private float _absorptionDuration; // 吸収されるまでの時間
        [SerializeField] private float _outwardBulge; // 吸収経路の外側への膨らみ係数

        private GameObject _player;

        private void Start()
        {
            _gm = GameObject.Find("GameManager").GetComponent<TK.GameManager>();

            var player = GameObject.FindGameObjectWithTag(_playerTag);

            if (player == null)
            {
                Destroy(gameObject);
                return;
            }

            _player = player;

            // ランダムな向きにランダムな人のモデルを子として生成
            var model = _humanModels[Random.Range(0, _humanModels.Count)];
            var rot = Quaternion.Euler(0, 0, Random.Range(-180f, 180f));
            Instantiate(model, transform.position, rot, transform);

            var pp = _player.transform.position;
            transform.DOLocalJump(_player.transform.position, _outwardBulge, 1, _absorptionDuration)
                     .OnComplete(() => Destroy(gameObject));
        }

        private void Update()
        {
        }

        private void OnDestroy()
        {
            if (_player != null)
            {
                var pComp = _player.GetComponent<Player>();
                if (pComp == null)
                {
                    return;
                }
                if (_gm != null)
                {
                    if (!_gm.IsGameEnd())
                    {
                        ++pComp.CrewCount;
                    }
                }
                
            }
        }
    }
}