using System;

namespace Goblin.Landing.Models.Attributes
{
    /// <inheritdoc />
    /// <summary>
    ///     Attribute to mark combine (OR conditional) with higher level AuthorizeAttribute
    /// </summary>
    [AttributeUsage(AttributeTargets.Method)]
    public class CombineAuthAttribute : Attribute
    {
    }
}