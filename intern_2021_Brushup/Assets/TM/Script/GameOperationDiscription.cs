using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace TM
{
    public class GameOperationDiscription : MonoBehaviour
    {
        [SerializeField] private TK.GameManager _gameManager; // ゲームマネージャーコンポーネント
        [SerializeField] private Image _gameOperation;      // ゲーム操作方法画像コンポーネント
        [SerializeField] private Image _resultOperation;    // リザルト操作方法画像コンポーネント

        private void Start()
        {
            if(!_gameManager)
            {
                Destroy(gameObject);
            }

            _gameOperation.gameObject.SetActive(true);
            _resultOperation.gameObject.SetActive(false);
        }

        private void Update()
        {
            if(_gameManager.IsGameEnd())
            {
                _gameOperation.gameObject.SetActive(false);
                _resultOperation.gameObject.SetActive(true);

                // もう必要ないのでコンポーネント自身を消す
                Destroy(this);
            }
        }
    }
}