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
    public class EarthQuake : EventBase
    {
        private GameObject _playerCollider; // プレイヤー
        private GameObject _ui;
        private Sheke _camera;
        private float _timeLimit;

        public override void Begin()
        {
            _camera = GameObject.Find("Quake").GetComponent<Sheke>();

            _ui = GameObject.Find("EarthQuake");
            
            _playerCollider = GameObject.Find("Player/Collider");
            // 物理マテリアルの切り替える
            _playerCollider.GetComponent<PhysicMatList>().Change(1);

            _nowTime = 0;
            // イベントの時間
            _timeLimit = Random.Range(5, 20);

            // サウンド再生
            SE _se = GameObject.Find("SEManager").GetComponent<SE>();
            _se.Play(0);            
        }

        public override void Tick(GameEvent gameEvent)
        {
            _nowTime += Time.deltaTime;
            _camera.CameraSheke();
            if (_nowTime <= 3)
            {
                _ui.GetComponent<EventUi>().Blink();
            }
            else
            {
                _ui.GetComponent<EventUi>().End();
            }
            if (_nowTime > _timeLimit)
            {
                this.End(gameEvent);
            }            
        }

        public override void End(GameEvent gameEvent)
        {
            _ui.GetComponent<EventUi>().End();
            gameEvent.ChangeEvent(new EventNone()); // イベント終了
            _nowTime = 0;
        }

    }
}
