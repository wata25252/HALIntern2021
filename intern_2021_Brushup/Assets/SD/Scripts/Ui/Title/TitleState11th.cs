using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SD
{
    public class TitleState11th : TitleStateBase
    {
        public override void Begin()
        {
            _titleName = "ディーパラダイス級";
            _peopleRequiredNextTitle = 55000;
        }
        public override void Tick(TitleStateManager mgr, GameObject player)
        {
            if (IsTitkeGoesUp(player))
            {
                mgr.ChangeTitle(new TitleState12th());
            }
        }
    }
}
