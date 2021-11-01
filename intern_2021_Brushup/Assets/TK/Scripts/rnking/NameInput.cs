using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//=====================================-
//ランキングの名前を入力する
//==========================================-
namespace TK
{
    public class NameInput : MonoBehaviour
    {
        private InputField _inputField;
        // Start is called before the first frame update
        void Start()
        {
            _inputField = GetComponent<InputField>();
            // 値をリセット
            _inputField.text = "";

            // フォーカス
            _inputField.ActivateInputField();
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}