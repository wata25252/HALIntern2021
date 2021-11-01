using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SD
{
    public class TitleState3rd : TitleStateBase
    {
        public override void Begin()
        {
            _titleName = "県立公園級";
            _peopleRequiredNextTitle = 15000;
        }
        public override void Tick(TitleStateManager mgr, GameObject player)
        {
            if(IsTitkeGoesUp(player))
            {
                mgr.ChangeTitle(new TitleState4th());
            }
        }
    }
}


