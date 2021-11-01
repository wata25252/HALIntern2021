/*-------------------------------------------------------
 * 
 *      [TitleStateFirst.cs]
 *      最初の称号
 *      Author : 出合翔太
 * 
 -------------------------------------------------------*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SD
{
    public class TitleState1st : TitleStateBase
    {
        public override void Begin()
        {
            _titleName = "近所の公園級";
            _peopleRequiredNextTitle = 5000;
		}

        public override void Tick(TitleStateManager mgr, GameObject player)
        {
            if (IsTitkeGoesUp(player))
            {
                mgr.ChangeTitle(new TitleState2nd());
            }
        }

    }
}
