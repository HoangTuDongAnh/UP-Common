using System;
using UnityEngine;

namespace HoangTuDongAnh.UP.Common.Attributes
{
    /// <summary>
    /// Show field if a bool member is true.
    /// </summary>
    [AttributeUsage(AttributeTargets.Field)]
    public sealed class ShowIfAttribute : PropertyAttribute
    {
        public string ConditionMember { get; }

        public ShowIfAttribute(string conditionMember)
        {
            ConditionMember = conditionMember;
        }
    }
}