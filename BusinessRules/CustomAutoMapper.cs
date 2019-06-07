using System;
using System.Collections.Generic;
using System.Reflection;

namespace BusinessRules
{
    public class CustomAutoMapper<T, W>
    {
        public static T Map(W w)
        {
            T t = Activator.CreateInstance<T>();
            PropertyInfo[] propriedades = typeof(W).GetProperties();
            foreach (var item in propriedades)
            {
                try
                {
                    object data = item.GetValue(w);
                    typeof(T).GetProperty(item.Name).SetValue(t, data);
                }
                catch
                {
                }
            }
            return t;
        }
        public static IList<T> Map(IList<W> ws)
        {
            List<T> dados = new List<T>();
            foreach (W item in ws)
            {
                dados.Add(Map(item));
            }
            return dados;
        }
    }
}
