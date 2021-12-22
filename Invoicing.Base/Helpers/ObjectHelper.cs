using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace Invoicing.Base.Helpers
{
    public static class ObjectHelper
    {
        public static ICollection<Tuple<object, object>> NonePrimitiveProperties<T>(T oldObj, T newObj)
        {

            if (oldObj == null && newObj == null)
                return null;

            var prims = new List<Tuple<object, object>>();
            Type objType;

            if (oldObj == null)
                objType = newObj.GetType();
            else
                objType = oldObj.GetType();

            FieldInfo[] fi = objType.GetFields(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public);

            foreach (var field in fi)
            {
                if (!typeof(IEnumerable).IsAssignableFrom(field.FieldType) || field.FieldType == typeof(string))
                {
                    if (!(field.FieldType.IsPrimitive || field.FieldType == typeof(decimal) || field.FieldType == typeof(string)))
                    {
                        object o1 = null, o2 = null;

                        if (oldObj != null)
                            o1 = field.GetValue(oldObj);
                        if (newObj != null)
                            o2 = field.GetValue(newObj);

                        var tuple = new Tuple<object, object>(o1, o2);
                        prims.Add(tuple);
                    }
                }
            }
            return prims;

        }

        public static ICollection<PropertyDifferences> DetailedCompare<T>(T oldObj, T newObj)
        {
            var diff = new List<PropertyDifferences>();
            Type objType;

            if (oldObj == null)
                objType = newObj.GetType();
            else
                objType = oldObj.GetType();

            FieldInfo[] fi = objType.GetFields(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public);

            foreach (var field in fi)
            {
                if (field.FieldType.IsPrimitive || field.FieldType == typeof(decimal) || field.FieldType == typeof(string))
                {
                    var prop = new PropertyDifferences
                    {
                        Property = field.Name
                    };

                    if (oldObj != null)
                        prop.OldVal = field.GetValue(oldObj);

                    if (newObj != null)
                        prop.NewVal = field.GetValue(newObj);

                    if (!Equals(prop.OldVal, prop.NewVal))
                        diff.Add(prop);
                }
            }
            return diff;
        }

    }
}
