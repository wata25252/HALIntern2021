/*-------------------------------------------------------
 * 
 *      [TitleStateBase.cs]
 *      称号のステートベース
 *      Author : 出合翔太
 * 
 -------------------------------------------------------*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SD
{
    public abstract class TitleStateBase : MonoBehaviour
    {
        protected string _titleName;    // 称号の名前
        protected uint _peopleRequiredNextTitle;  // 次の称号になるのに必要な人数

        // はじめ
        public abstract void Begin();

        // 更新
        public abstract void Tick(TitleStateManager mgr, GameObject player);

        // 称号の名前を返す
        public string GetTitle()
        {
            return _titleName;
        }
        
        // 称号が上がるかどうかを返す
        protected bool IsTitkeGoesUp(GameObject player)
        {
            if (player.GetComponent<TM.Player>().CrewCount > _peopleRequiredNextTitle)
            {
                return true;
            }
            return false;
        }
    }
}