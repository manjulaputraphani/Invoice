using System;

namespace SriDurgaHariHaraBackend.Api.Attributes
{
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class, AllowMultiple = false)]
    public class ValidateRequestAttribute : Attribute
    {
        public Type DtoType { get; }
        public ValidateRequestAttribute(Type dtoType) => DtoType = dtoType;
    }
}