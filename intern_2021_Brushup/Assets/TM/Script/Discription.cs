using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

namespace TM
{
    public class Discription : MonoBehaviour
    {
        [SerializeField] private List<Sprite> _discriptionSprites;  // ゲーム説明スプライト
        [SerializeField] private KeyCode _enterKey; // 決定キー
        [SerializeField] private Image _image; // 説明書きを表示する画像コンポーネント
        [SerializeField] private TitleUIController _titleUIController; // タイトルUIコンポーネント
        [SerializeField] private int _enterDeferredFrame;   // 決定を押してまた押せるまでのフレーム
        private bool _isEntered = false;    // 決定キーが押された
        private int _enterDeferredFrameCnt = 0;   // 決定遅延のフレームカウンタ
        private int _displayingIndex = 0;   // 表示するスプライト番号
        private bool _showDiscription = false;  // 説明を表示する
        public bool ShowDiscription
        {
            get => _showDiscription;
            set
            {
                _showDiscription = value;
                _displayingIndex = value
                                 ? _displayingIndex
                                 : 0;
                if (_image)
                {
                    _image.gameObject.SetActive(value);
                }

                if(_titleUIController)
                {
                    _titleUIController.AcceptInput = !value;
                }
            }
        }

        private void Start()
        {
            ShowDiscription = false;
        }

        private void Update()
        {
            if (Input.GetKeyDown(_enterKey))
            {
                _isEntered = true;
            }
        }

        private void FixedUpdate()
        {
            // 決定キーを押すと次の画面へ
            if (ShowDiscription && _isEntered)
            {
                _isEntered = false;

                if (_displayingIndex < _discriptionSprites.Count)
                {
                    _image.sprite = _discriptionSprites[_displayingIndex++];
                }
                else
                {
                    ShowDiscription = false;
                }
            }
        }
    }
}