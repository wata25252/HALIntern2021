using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SD
{
    public class TitleState9th : TitleStateBase
    {
        public override void Begin()
        {
            _titleName = "富士級";
            _peopleRequiredNextTitle = 45000;
        }
        public override void Tick(TitleStateManager mgr, GameObject player)
        {
            if (IsTitkeGoesUp(player))
            {
                mgr.ChangeTitle(new TitleState10th());
            }
        }
    }
}
