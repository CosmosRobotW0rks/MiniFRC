using MiniFRC_FMS.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
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

    internal static class ModulesMain
    {
        public static void InitModules()
        {
            Assembly asm = Assembly.GetExecutingAssembly();

            List<Type> moduleTypes = asm.GetTypes()
                .Where(
                    x =>
                        x.IsClass
                        && x.IsAbstract
                        && x.IsSealed
                        && x.Namespace.StartsWith("MiniFRC_FMS.Modules")
                        && x.Name.ToLower().EndsWith("module")
                        && x.IsNested == false
                        && x.Name != nameof(ModulesMain)
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

                MethodInfo? initmethod = moduleType.GetMethod("Initialize");
                if (initmethod == null || !initmethod.IsStatic || initmethod.ReturnType != typeof(bool))
                {
                    Logger.Log(
                        LogLevel.ERROR,
                        $"The module {moduleType.Name} does not contain a proper Initialize method (1)"
                    );
                    continue;
                }
                bool? suc = null;

                try
                {
                    suc = (bool?)initmethod.Invoke(null, null);
                }
                catch (Exception ex)
                {
                    Logger.Log(LogLevel.ERROR, $"The module {moduleType.Name} failed to initialize (ex: {ex.Message})");
                    continue;
                }

                if (suc == null)
                {
                    Logger.Log(LogLevel.ERROR, $"The module {moduleType.Name} does not contain a proper Initialize method (2)");
                    continue;
                }

                if (!suc.Value)
                {
                    Logger.Log(LogLevel.ERROR, $"The module {moduleType.Name} failed to initialize");
                }
                else
                {
                    Logger.Log(LogLevel.INFO, $"Initialized {moduleType.Name}");
                    sucCount++;
                }
            }

            Logger.Log(LogLevel.INFO, $"Initialized {sucCount}/{moduleTypes.Count} modules");
        }


    }
}
