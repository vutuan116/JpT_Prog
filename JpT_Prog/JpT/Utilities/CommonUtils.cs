using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace JpT.Utilities
{
    public class CommonUtils
    {
        public static T MappingData<T>(object objectSource)
        {
            PropertyInfo[] PropsObjectNew = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);
            PropertyInfo[] PropsObjectOld = objectSource.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance);
            T item = (T)Activator.CreateInstance(typeof(T));
            foreach (PropertyInfo prop in PropsObjectNew)
            {
                string propName = prop.Name;
                PropertyInfo propMatching = null;
                foreach (PropertyInfo propOld in PropsObjectOld)
                {
                    if (propOld.Name.Equals(prop.Name))
                    {
                        propMatching = propOld;
                        break;
                    }
                }
                object value = "";
                if (propMatching != null)
                {
                    if (prop.PropertyType.Name == "Boolean")
                    {
                        value = propMatching.GetValue(objectSource, null) == null ? "False" : propMatching.GetValue(objectSource, null);
                    }
                    else
                    {
                        value = propMatching.GetValue(objectSource, null);
                    }
                    prop.SetValue(item, Convert.ChangeType(value, prop.PropertyType), null);
                }
            }
            return item;
        }

        public static T ConvertObject<T>(T objectNew, Object objectOld)
        {
            PropertyInfo[] PropsObjectNew = objectNew.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance);
            PropertyInfo[] PropsObjectOld = objectOld.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance);
            T item = (T)Activator.CreateInstance(objectNew.GetType());
            foreach (PropertyInfo prop in PropsObjectNew)
            {
                string propName = prop.Name;
                PropertyInfo propMatching = null;
                foreach (PropertyInfo propOld in PropsObjectOld)
                {
                    if (propOld.Name.Equals(prop.Name))
                    {
                        propMatching = propOld;
                        break;
                    }
                }
                if (propMatching != null)
                {
                    object value = propMatching.GetValue(objectOld, null);
                    prop.SetValue(item, Convert.ChangeType(value, prop.PropertyType), null);
                }
            }
            return item;
        }
    }
}
