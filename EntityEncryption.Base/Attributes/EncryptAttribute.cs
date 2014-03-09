using System;

namespace EntityEncryption.Base.Attributes
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class EncryptAttribute : Attribute { }
}