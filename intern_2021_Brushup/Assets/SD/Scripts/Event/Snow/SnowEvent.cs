/*-------------------------------------------------------
 * 
 *      [Snow.cs]
 *      ゲームイベント：大雪
 * 
 -------------------------------------------------------*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SD
{
    public class SnowEvent : EventBase
    {
        private GameObject _playerCollider; // プレイヤー
        private GameObject _ui;
        //　
        private SnowSpawn _spawn;
        private float _timeLimit;

        // スポーンした雪のパーティクル
        private GameObject _snowEffect;

        public override void Begin()
        {
            _spawn = GameObject.FindGameObjectWithTag("Event_Snow").GetComponent<SnowSpawn>(); ;
            // エフェクト発生
            _snowEffect = _spawn.Spawn();
            
            // イベント発生Ui
            _ui = GameObject.FindGameObjectWithTag("UI_Snow");            

            _playerCollider = GameObject.FindGameObjectWithTag("Player_Collider");
            // 物理マテリアルの切り替える
            _playerCollider.GetComponent<PhysicMatList>().Change(2);

            _nowTime = 0;
            _timeLimit = Random.Range(5, 20);

            // サウンド再生
            SE _se = GameObject.FindGameObjectWithTag("Manager_SEManager").GetComponent<SE>();
            _se.Play(0);
        }

        public override void Tick(GameEvent gameEvent)
        {            
            _nowTime += Time.deltaTime;
            
            // ３秒間、点滅
            if (_nowTime <= 3)
            {
                _ui.GetComponent<EventUi>().Blink();
            }
            else
            {
                _ui.GetComponent<EventUi>().End();
            }
            // イベント終了
            if (_nowTime > _timeLimit)
            {
                _nowTime = 0;
                this.End(gameEvent);
            }            
        }

        public override void End(GameEvent gameEvent)
        {
            // パーティクルエフェクトを削除
            Destroy(_snowEffect);
            _ui.GetComponent<EventUi>().End();
            gameEvent.ChangeEvent(new EventNone()); // イベントを変更
            _nowTime = 0;
        }
    }
}
