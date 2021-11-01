using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SD
{
    public class TitleState8th : TitleStateBase
    {
        public override void Begin()
        {            
            _titleName = "ナガシマスゲーランド級";
            _peopleRequiredNextTitle = 40000;
        }
        public override void Tick(TitleStateManager mgr, GameObject player)
        {
            if (IsTitkeGoesUp(player))
            {
                mgr.ChangeTitle(new TitleState9th());
            }
        }
    }
}
