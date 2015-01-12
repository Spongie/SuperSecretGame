using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CV_clone;

namespace TheSuperTrueRealCV
{
    interface IEnemyAI
    {
        void Activate(Moving_Entity target);
        void Update();
        void Disable();
    }
}
