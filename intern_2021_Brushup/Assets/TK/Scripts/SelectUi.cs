using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

namespace TK
{
    public class SelectUi : MonoBehaviour
    {
        [SerializeField]
        private bool isSelected = false;

        private void Start()
        {
            if (!isSelected)
            {
                isSelected = false;
                var target = GetComponent<Text>();
                target.DOColor(Color.gray, 0.5f);
            }
            else
            {
                isSelected = true;
                var target = GetComponent<Text>();
                target.DOColor(Color.white, 0.5f);
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

    }
}


