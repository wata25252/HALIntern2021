using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SD
{
    public class TitleState5th : TitleStateBase
    {
        public override void Begin()
        {
            _titleName = "かたひらパーク級";
            _peopleRequiredNextTitle = 25000;
        }
        public override void Tick(TitleStateManager mgr, GameObject player)
        {
            if (IsTitkeGoesUp(player))
            {
                mgr.ChangeTitle(new TitleState6th());
            }
        }
    }
}
