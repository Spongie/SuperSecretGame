using Assets.Scripts.Attacks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Assets.Scripts.Utility
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
