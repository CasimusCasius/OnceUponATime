using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game.Stats
{
    public interface IModifierProvider
    {
        IEnumerable<float> GetAdditiveModifier(Stat stat);

    }
}
