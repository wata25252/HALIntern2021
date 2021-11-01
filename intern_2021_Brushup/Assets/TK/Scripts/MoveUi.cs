using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

namespace TK{
    public class MoveUi : MonoBehaviour
    {
        private RectTransform _rectTransform;

        private bool _isViewing=false;

        [SerializeField]
        private string _message;

        [SerializeField]
        private bool isSelected=false;
        public void Start()
        {
            _rectTransform = GetComponent<RectTransform>();
            
        }

        public void ShowWindow()
        {
            if (!_isViewing)
            {
                var target = GetComponent<Text>();
                target.DOText(_message, 1.0f);
                _isViewing = true;
            }
        }

        public void SetSelected()
        {
            if (!isSelected)
            {
                isSelected = true;
                var target = GetComponent<Text>();
                target.DOColor(Color.white, 0.5f);
            }
        }

        public void SetUnSelected()
        {
            if (isSelected)
            {
                isSelected = false;
                var target = GetComponent<Text>();
                target.DOColor(Color.gray, 0.5f);
            }
        }
        //public void ExitWindow()
        //{
        //    _rectTransform.anchoredPosition = new Vector3(_rectTransform.localPosition.x, _rectTransform.localPosition.y, _rectTransform.localPosition.z);
        //    _rectTransform.localPosition= new Vector3(_rectTransform.localPosition.x+700, _rectTransform.localPosition.y, _rectTransform.localPosition.z);
        //    _rectTransform.DOLocalMoveX(0f, 1f).SetEase(Ease.OutBounce);
        //}
    }
}
