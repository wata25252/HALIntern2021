/*-------------------------------------------------------
 * 
 *      [EarthQuake.cs]
 *      ゲームイベント：地震
 *      Author : 出合翔太
 * 
 ------------------------------------------------------*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SD
{
    public class EarthQuakeEvent : EventBase
    {
        private GameObject _playerCollider; // プレイヤー
        private GameObject _ui; // UI
        private Sheke _camera;  // 
        private float _timeLimit;

        public override void Begin()
        {
            _camera = GameObject.FindGameObjectWithTag("Event_Quake").GetComponent<Sheke>();

            _ui = GameObject.FindGameObjectWithTag("UI_EarthQuake");
            
            _playerCollider = GameObject.FindGameObjectWithTag("Player_Collider");
            // 物理マテリアルの切り替える
            _playerCollider.GetComponent<PhysicMatList>().Change(1);

            _nowTime = 0;
            // イベントの時間
            _timeLimit = Random.Range(5, 20);

            // サウンド再生
            SE _se = GameObject.FindGameObjectWithTag("Manager_SEManager").GetComponent<SE>();
            _se.Play(0);            
        }

        public override void Tick(GameEvent gameEvent)
        {
            _nowTime += Time.deltaTime;

            // カメラを揺らす
            _camera.CameraSheke();

            // ３秒間点滅
            if (_nowTime <= 3)
            {
                _ui.GetComponent<EventUi>().Blink();
            }
            else
            {
                _ui.GetComponent<EventUi>().End();
            }
            // イベントの終了
            if (_nowTime > _timeLimit)
            {
                this.End(gameEvent);
            }            
        }

        public override void End(GameEvent gameEvent)
        {
            _ui.GetComponent<EventUi>().End();
            gameEvent.ChangeEvent(new EventNone()); // イベントを変更
            _nowTime = 0;
        }

    }
}
