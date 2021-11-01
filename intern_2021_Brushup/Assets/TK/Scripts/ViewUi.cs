using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

namespace TK
{
    public class ViewUi : MonoBehaviour
    {

        private bool _isViewing = false;

        [SerializeField]
        private string _message;

        public void ShowWindow()
        {

                var target = GetComponent<Text>();
                target.DOText(_message, 1.0f);

            
        }

        public void SetMessage(string s)
        {
            _message = s;
        }
    }
}
