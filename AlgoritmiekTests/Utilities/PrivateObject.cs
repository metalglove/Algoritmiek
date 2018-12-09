using System;
using System.Reflection;

namespace AlgoritmiekTests.Utilities
{
    public class PrivateObject<TObjectType> where TObjectType : class
    {
        public dynamic Value { get; private set; }

        public PrivateObject(ref TObjectType obj, string name, PrivateType privateType)
        {
            Type objType = typeof(TObjectType);
            PrivateTypeSwitch(obj, name, privateType, null, objType);
        }

        public PrivateObject(ref TObjectType obj, string name, PrivateType privateType, object[] args)
        {
            Type objType = typeof(TObjectType);
            PrivateTypeSwitch(obj, name, privateType, args, objType);
        }

        private void PrivateTypeSwitch(TObjectType obj, string name, PrivateType privateType, object[] args, Type objType)
        {
            switch (privateType)
            {
                case PrivateType.Field:
                    FieldInfo fieldInfo = objType.GetField(name, BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public);
                    Value = fieldInfo.GetValue(obj);
                    break;
                case PrivateType.Property:
                    PropertyInfo propertyInfo = objType.GetProperty(name, BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public);
                    Value = propertyInfo.GetValue(obj);
                    break;
                case PrivateType.Method:
                    objType.InvokeMember(name, BindingFlags.InvokeMethod | BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance, null, obj, args);
                    break;
                case PrivateType.MethodWithReturnValue:
                    Value = objType.InvokeMember(name, BindingFlags.InvokeMethod | BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance, null, obj, args);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(privateType), privateType, null);
            }
        }
    }
}
