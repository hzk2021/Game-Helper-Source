using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Discord.Game
{
    public enum ObjectType
    {
        LocalPlayer,
        Enemy_ClosestToLocalPlayer,
        Enemy_ClosestToMouse,
        Enemy_LowestHealthPercent,
        Enemy_LowestHealthAbsolute
    }

}
