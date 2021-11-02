/*-------------------------------------------------------
 * 
 *      [GameEvent.cs]
 *      ゲームイベント用
 *      Author : 出合翔太
 * 
 -------------------------------------------------------*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SD
{
    public class GameEvent : MonoBehaviour
    {        
        private EventBase _event;
        private TK.GameManager _manager;
      
        // Start is called before the first frame update
        void Start()
        {
            _manager = GetComponent<TK.GameManager>();
            _event = new EventNone();
        }

        // Update is called once per frame
        void Update()
        {
            
        }

        private void FixedUpdate()
        {
            // ゲーム中
            if(!_manager.IsGameEnd())
            {
                _event.Tick(this); // イベントの更新処理
            }
            else　// ゲーム終了なら、イベントを強制終了
            {
                _event.End(this);
            }
        }

        // イベントの変更
        public void ChangeEvent(EventBase nextevent)
        {
            _event = nextevent;           
            _event.Begin();
        }
    }
}
