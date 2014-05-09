using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebExpress.Core.TypeExtensions
{
    public static class EnumExtenstion
    {
        public static ValueText[] GetValueTexts<T>()
        {
            var items = Enum.GetValues(typeof(T));
            var result = new List<ValueText>();
            foreach (var item in items)
            {
                var vt = new ValueText { Value = (int)item, Text = item.ToString() };
                result.Add(vt);
            }
            return result.ToArray();
        }
    }
}
