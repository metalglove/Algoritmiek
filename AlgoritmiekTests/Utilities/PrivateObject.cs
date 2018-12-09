using System;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;

namespace AlgoritmiekTests.Utilities
{
    /// <summary>
    /// Represents an encapsulated object with the intent to access private members.
    /// </summary>
    /// <typeparam name="TObjectType">The object type to access private members for.</typeparam>
    [ExcludeFromCodeCoverage]
    public class PrivateObject<TObjectType> where TObjectType : class
    {
        /// <summary>
        /// The value from the private member.
        /// </summary>
        public dynamic Value { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="PrivateObject{TObjectType}"/> class.
        /// </summary>
        /// <param name="obj">The object to access private members for.</param>
        /// <param name="name">The name of the private member.</param>
        /// <param name="privateType">The type of the private member to access.</param>
        public PrivateObject(ref TObjectType obj, string name, PrivateType privateType)
        {
            Type objType = typeof(TObjectType);
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
                    objType.InvokeMember(name, BindingFlags.InvokeMethod | BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance, null, obj, null);
                    break;
                case PrivateType.MethodWithReturnValue:
                    Value = objType.InvokeMember(name, BindingFlags.InvokeMethod | BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance, null, obj, null);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(privateType), privateType, null);
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PrivateObject{TObjectType}"/> class.
        /// </summary>
        /// <param name="obj">The object to access private members for.</param>
        /// <param name="name">The name of the private member.</param>
        /// <param name="privateType">The type of the private member to access.</param>
        /// <param name="args">The arguments for the Method</param>
        /// <exception cref="MemberAccessException"></exception>
        public PrivateObject(ref TObjectType obj, string name, PrivateType privateType, object[] args)
        {
            Type objType = typeof(TObjectType);
            switch (privateType)
            {
                case PrivateType.Field:
                    throw new MemberAccessException("Fields do not take arguments, use the constructor without arguments");
                case PrivateType.Property:
                    throw new MemberAccessException("Properties do not take arguments, use the constructor without arguments");
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
