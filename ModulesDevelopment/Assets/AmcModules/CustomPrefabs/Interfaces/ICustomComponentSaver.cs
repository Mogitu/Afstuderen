using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AmcCustomPrefab
{
    public interface ICustomComponentSaver
    {
        void Save(string scriptText, AmcComponent component = null);
    }
}