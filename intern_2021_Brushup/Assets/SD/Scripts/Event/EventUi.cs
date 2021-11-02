/*-------------------------------------------------------
 * 
 *      [EventUi.cs]   
 *      イベントのUI
 *      Author : 出合翔太
 * 
 -------------------------------------------------------*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace SD
{
    public class EventUi : MonoBehaviour
    {
        [SerializeField] private Text _text;
        // デフォルトの設定
        private Color _defaultImage;
        private Color _defaultText;

        private float _time;　// 時間計測

        // Start is called before the first frame update
        void Start()
        {
            // デフォルトのカラー
            _defaultImage = this.gameObject.GetComponent<Image>().color;
            _defaultText = _text.color;
            this.gameObject.GetComponent<Image>().color = new Color(_defaultImage.r, _defaultImage.g, _defaultImage.b, 0.0f);
            _text.color = new Color(_defaultText.r, _defaultText.g, _defaultText.b, 0.0f);
        }

        // Update is called once per frame
        void Update()
        {

        }

        // 終了
        public void End()
        {
            // デフォルトに戻す
            this.gameObject.GetComponent<Image>().color = new Color(_defaultImage.r, _defaultImage.g, _defaultImage.b, 0.0f);
            _text.color = new Color(_defaultText.r, _defaultText.g, _defaultText.b, 0.0f);
        }

        // 点滅
        public void Blink()
        {
            this.gameObject.GetComponent<Image>().color = getAlpha(this.gameObject.GetComponent<Image>().color);
            _text.color = getAlpha(_text.color);
        }

        // a値を上下させる
        private Color getAlpha(Color color)
        {
            _time += Time.deltaTime * 5.0f;
            color.a = Mathf.Sin(_time) * 0.5f + 0.5f;
            return color;
        }
    }
}
