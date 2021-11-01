using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace TM
{
    public class TitleOperationDiscription : MonoBehaviour
    {
        [SerializeField] private TitleUIController _titleUIController; // タイトルUIコンポーネント
        [SerializeField] private Image _titleOperation;      // タイトル操作方法画像コンポーネント

        void Start()
        {
            if(!_titleUIController)
            {
                Destroy(gameObject);  
            }

            _titleOperation.gameObject.SetActive(false);
        }

        void Update()
        {
            if(!_titleUIController.IsSkyView)
            {
                _titleOperation.gameObject.SetActive(true);

                // もう必要ないのでコンポーネント自身を消す
                Destroy(this);
            }
        }
    }
}