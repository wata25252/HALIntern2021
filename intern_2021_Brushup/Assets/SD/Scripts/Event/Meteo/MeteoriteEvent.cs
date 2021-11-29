/*--------------------------------------------------------
 * 
 *      [Meteorite.cs]
 *      ゲームイベント：隕石
 *      Author : 出合翔太 
 * 
 --------------------------------------------------------*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SD
{
    public class MeteoriteEvent : EventBase
    {
        private GameObject _meteoSpawn;
        private GameObject _ui;
        private bool _isSpawn;

        // 開始
        public override void Begin()
        {
            _ui = GameObject.FindGameObjectWithTag("UI_Meteorite");           
            _meteoSpawn = GameObject.FindGameObjectWithTag("Event_Meteo");
            
            _isSpawn = false;
            _nowTime = 0;

            // サウンド再生
            SE _se = GameObject.FindGameObjectWithTag("Manager_SEManager").GetComponent<SE>();
            _se.Play(0);
        }

        public override void Tick(GameEvent gameEvent)
        {
            _nowTime += Time.deltaTime;
            // スポーンをしていなければ、スポーン
            if(!_isSpawn)
            {                
                _meteoSpawn.GetComponent<MeteoSpawn>().Spawn();
                _isSpawn = true;
                
            }
            // ３秒間、UIを点滅させる
            if (_nowTime <= 3)
            {
                _ui.GetComponent<EventUi>().Blink();
            }
            else // 3秒たったら、消す
            {
                _ui.GetComponent<EventUi>().End();
            }
            // 5秒間たったら、イベントを変える
            if (_nowTime > 5)
            {
                _nowTime = 0;             
                gameEvent.ChangeEvent(new EventNone());
            }
        }

        public override void End(GameEvent gameEvent)
        {
            _ui.GetComponent<EventUi>().End();
            gameEvent.ChangeEvent(new EventNone());            
        }

    }
}