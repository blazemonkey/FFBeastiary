using System;
using System.Collections.Generic;
using System.Text;

namespace FFBestiary.Extensions
{
    public static class IEnumerable 
    {
        public static void ForEach<T>(this IEnumerable<T> list, Action<T> action)
        {
            foreach (var l in list)
            {
                action.Invoke(l);
            }
        }
    }
}
