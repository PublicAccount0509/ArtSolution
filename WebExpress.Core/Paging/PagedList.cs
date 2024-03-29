﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebExpress.Core.Paging;

namespace WebExpress.Core
{
    /// <summary>
    /// Paged list
    /// </summary>
    /// <typeparam name="T">T</typeparam>
    [Serializable]
    public sealed class PagedList<T> : List<T>
    {
        public PagingResult PagingResult { get; set; }

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="source">source</param>
        /// <param name="pageIndex">Page index</param>
        /// <param name="pageSize">Page size</param>
        public PagedList(IQueryable<T> source, int pageIndex, int pageSize)
        {
            PagingResult = new PagingResult(pageIndex, pageSize);

            PagingResult.TotalCount = source.Count();

            var skipCount = pageIndex * pageSize;
            var data = source.Skip(skipCount).Take(pageSize).ToList();
            this.AddRange(data);
        }


    }
}
