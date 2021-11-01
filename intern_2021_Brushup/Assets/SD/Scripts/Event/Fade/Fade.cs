/*-------------------------------------------------------
 * 
 *      [Fade.cs]
 *      Uiのフェードをする
 *      Author : 出合翔太
 * 
 -------------------------------------------------------*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace SD
{
    public class Fade : MonoBehaviour
    {
        private FadeStateBase _State;
        // Start is called before the first frame update
        void Start()
        {
            Text text = GetComponent<Text>();
            text.color = Color.clear;
            _State = new FadeStateIn();
        }

        // Update is called once per frame
        void Update()
        {

        }

        private void FixedUpdate()
        {
            _State.Execute(this);
        }

        public void ChangeState(FadeStateBase state)
        {
            _State = state;
        }
    }
}
