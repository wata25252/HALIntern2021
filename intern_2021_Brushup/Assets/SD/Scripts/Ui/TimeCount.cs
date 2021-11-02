/*-------------------------------------------------------
 * 
 *      [TimeCount.cs]
 *      時間の管理
 *      Author : 出合翔太
 * 
 --------------------------------------------------------*/ 
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace SD
{
    public class TimeCount : MonoBehaviour
    {
        [SerializeField] private GameObject _gameObject;
        [SerializeField] private Text _text;
        private Image _imageBG;
        private TK.GameManager _gameManager;
        private float _moveTime;
        private float _angle;

        // Start is called before the first frame update
        void Start()
        {
            _gameManager = GameObject.Find("GameManager").GetComponent<TK.GameManager>();
            _imageBG = this.GetComponent<Image>();
            _imageBG.color = Color.green;
        }

        // Update is called once per frame
        void Update()
        {
            _moveTime = _gameManager._timeCount;
            if (_moveTime > 60)
            {
                _imageBG.color = Color.blue;
            }
            else if (_moveTime < 60 && _moveTime > 20)
            {
                _imageBG.color = Color.green;
            }
            else if (_moveTime < 20 && _moveTime > 10)
            {
                _imageBG.color = Color.yellow;
            }
            else if (_moveTime < 10)
            {
                _imageBG.color = Color.red;
            }
            _angle = _moveTime * 360 / 60;
            _gameObject.transform.localEulerAngles = new Vector3(0, 0, _angle);

            _text.text = _gameManager._timeCount.ToString("F1");
        }
    }
}
