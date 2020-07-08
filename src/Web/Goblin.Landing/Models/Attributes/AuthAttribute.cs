using System;

namespace Goblin.Landing.Models.Attributes
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true)]
    public class AuthAttribute : Attribute
    {
        public AuthAttribute(params string[] permission)
        {
            Permissions = permission;
        }

        public string[] Permissions { get; }
    }
}