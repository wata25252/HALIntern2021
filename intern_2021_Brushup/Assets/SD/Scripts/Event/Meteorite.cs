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
    public class Meteorite : EventBase
    {
        private GameObject _player;
        private GameObject _meteoSpawn;
        private GameObject _ui;
        private bool _isSpawn;

        public override void Begin()
        {
            _ui = GameObject.Find("Meteorite");           
            _player = GameObject.Find("Player");
            _meteoSpawn = GameObject.Find("MeteoSpawn");
            
            _isSpawn = false;
            _nowTime = 0;

            // サウンド再生
            SE _se = GameObject.Find("SEManager").GetComponent<SE>();
            _se.Play(0);
        }

        public override void Tick(GameEvent gameEvent)
        {
            _nowTime += Time.deltaTime;
            if(!_isSpawn)
            {
                _meteoSpawn.GetComponent<MeteoSpawn>().Spawn();
                _isSpawn = true;
                
            }
            if (_nowTime <= 3)
            {
                _ui.GetComponent<EventUi>().Blink();
            }
            else
            {
                _ui.GetComponent<EventUi>().End();
            }
            if (_nowTime > 5)
            {
                _nowTime = 3;             
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