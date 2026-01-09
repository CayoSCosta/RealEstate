using System.Xml;

namespace Imobi.MVC.ViewModels.Validator.Base
{
    [AttributeUsage(AttributeTargets.Property)]
    public class ValidateAsAttribute : Attribute
    {
        public ValidationType Type { get; }
        public ValidateAsAttribute(ValidationType type) => Type = type;
    }
}
