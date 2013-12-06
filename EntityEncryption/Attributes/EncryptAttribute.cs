using System;

namespace EntityEncryption.Attributes
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class EncryptAttribute : Attribute { }
}