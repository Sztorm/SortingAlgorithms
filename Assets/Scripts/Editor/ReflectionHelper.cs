using System;
using System.Reflection;

namespace SortingAlgorithms.Editor
{
    public static class ReflectionHelper
    {
        public static Action<TObject> CreateMethodDelegate<TObject>(
            string name, BindingFlags bindingAttr)
        {
            Type objectType = typeof(TObject);
            MethodInfo method = objectType.GetMethod(name, bindingAttr);

            var methodDelegate = (Action<TObject>)Delegate
                .CreateDelegate(typeof(Action<TObject>), method);

            return methodDelegate;
        }

        public static Action<TObject, TArg1> CreateMethodDelegate<TObject, TArg1>(
            string name, BindingFlags bindingAttr)
        {
            Type objectType = typeof(TObject);
            MethodInfo method = objectType.GetMethod(name, bindingAttr);

            var methodDelegate = (Action<TObject, TArg1>)Delegate
                .CreateDelegate(typeof(Action<TObject, TArg1>), method);

            return methodDelegate;
        }

        public static Action<TObject, TArg1, TArg2> CreateMethodDelegate<TObject, TArg1, TArg2>(
            string name, BindingFlags bindingAttr)
        {
            Type objectType = typeof(TObject);
            MethodInfo method = objectType.GetMethod(name, bindingAttr);

            var methodDelegate = (Action<TObject, TArg1, TArg2>)Delegate
                .CreateDelegate(typeof(Action<TObject, TArg1, TArg2>), method);

            return methodDelegate;
        }
    }
}
