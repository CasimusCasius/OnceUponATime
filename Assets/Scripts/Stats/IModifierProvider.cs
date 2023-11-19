using System.Collections.Generic;

namespace Game.Stats
{
    public interface IModifierProvider
    {
        IEnumerable<float> GetAdditiveModifiers(EStat stat);
        IEnumerable<float> GetPercentageModifiers(EStat stat);
    }
}
