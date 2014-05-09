using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Art.Website.Models
{
    public abstract class TranslatorBase<T, TResult> : ITranslator<T, TResult>
    {
        public IList<TResult> Translate(IList<T> froms)
        {
            var tos = new List<TResult>();
            foreach (T from in froms)
            {
                var to = this.Translate(from);
                tos.Add(to);
            }
            return tos;
        }

        public abstract TResult Translate(T from);


        public abstract T Translate(TResult from);
    }
}
