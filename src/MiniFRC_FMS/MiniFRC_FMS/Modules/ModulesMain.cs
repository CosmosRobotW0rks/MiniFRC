using MiniFRC_FMS.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace MiniFRC_FMS.Modules
{
    [System.AttributeUsage(AttributeTargets.Class)]
    public class ModuleInitPriority : Attribute
    {
        public int Priority { get; private set; }

        public ModuleInitPriority(int priority)
        {
            Priority = priority;
        }
    }

    internal sealed class ModulesMain
    {
        private List<BaseModule> modules = new List<BaseModule>();

        private ModulesMain()
        {
            Assembly asm = Assembly.GetExecutingAssembly();

            List<Type> moduleTypes = asm.GetTypes()
                .Where(
                    x =>
                        x.IsClass
                        && x.Namespace.StartsWith("MiniFRC_FMS.Modules")
                        && x.IsAssignableTo(typeof(BaseModule))
                        && x != typeof(BaseModule)
                )
                .OrderBy(x =>
                {
                    ModuleInitPriority? priority = x.GetCustomAttribute<ModuleInitPriority>();
                    if (priority == null)
                    {
                        return int.MaxValue;
                    }
                    return priority.Priority;
                })
                .ToList();

            int sucCount = 0;

            foreach (Type moduleType in moduleTypes)
            {
                try
                {
                    BaseModule? module = (BaseModule?)Activator.CreateInstance(moduleType);

                    if (module == null)
                    {
                        Logger.Log(LogLevel.ERROR, $"Failed to initialize module {moduleType.Name} (NULL)");
                        continue;
                    }

                    if (!module.InitModule(this))
                    {
                        Logger.Log(LogLevel.ERROR, $"Failed to initialize module {moduleType.Name}");
                        continue;
                    }

                    Logger.Log($"Initialized module {moduleType.Name}");

                    modules.Add(module);
                    sucCount++;
                }
                catch(Exception ex)
                {
                    Logger.Log(LogLevel.ERROR, $"Failed to initialize module {moduleType.Name} (EX: {ex.Message})");
                }
            }

            Logger.Log(LogLevel.INFO, $"Initialized {sucCount}/{moduleTypes.Count} modules");
        }

        public T? GetModule<T>() where T : BaseModule, new()
        {
            return (T?)modules.Find(x => x.GetType() == typeof(T));
        }



        private static ModulesMain? instance = null;

        public static ModulesMain? Instance
        {
            get
            {
                return instance;
            }
        }

        public static void Init()
        {
            if (instance == null)
            {
                instance = new ModulesMain();
            }
        }

    }
}
