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
            if(!_manager.IsGameEnd())
            {
                _event.Tick(this);                
            }
            else
            {
                _event.End(this);
            }
        }

        public void ChangeEvent(EventBase nextevent)
        {
            _event = nextevent;           
            _event.Begin();
        }
    }
}
