using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Assets.Scripts.Attacks
{
    public class AttackEffectLoader
    {
        public List<string> GetAttackMethods()
        {
            var methods = typeof(AttackModifiers).GetMethods(BindingFlags.NonPublic | BindingFlags.Instance);

            List<string> methodNames = methods.Select(method => method.Name).ToList();

            methodNames.Remove("Finalize");
            methodNames.Remove("MemberwiseClone");
            methodNames.Remove("obj_address");
            methodNames.Add("None");

            return methodNames.OrderBy(name => name == "None").ToList();
        }
    }
}
