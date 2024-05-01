using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniFRC_FMS.Modules
{
    internal abstract class BaseModule
    {
        private ModulesMain? Modules;

        public bool InitModule(ModulesMain modules)
        {
            this.Modules = modules;
            return Init();
        }

        protected abstract bool Init();

        protected T? GetModule<T>() where T : BaseModule, new()
        {
            return Modules?.GetModule<T>();
        }
    }
}
