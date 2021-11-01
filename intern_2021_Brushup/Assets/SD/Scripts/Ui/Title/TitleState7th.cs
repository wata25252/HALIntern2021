using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SD
{
    public class TitleState7th : TitleStateBase
    {
        public override void Begin()
        {
            _titleName = "おしうりランド級";
            _peopleRequiredNextTitle = 35000;
        }
        public override void Tick(TitleStateManager mgr, GameObject player)
        {
            if (IsTitkeGoesUp(player))
            {
                mgr.ChangeTitle(new TitleState8th());
            }
        }
    }
}
