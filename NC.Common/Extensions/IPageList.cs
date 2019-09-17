using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NC.Common.Extensions
{
    /// <summary>
    /// IPagedList
    /// </summary>
    public interface IPagedList
    {
        /// <summary>
        /// 记录数
        /// </summary>
        int TotalCount { get; set; }
        /// <summary>
        /// 页数
        /// </summary>
        int TotalPages { get; set; }
        /// <summary>
        /// 当前页
        /// </summary>
        int PageIndex { get; set; }
        /// <summary>
        /// 页面大小
        /// </summary>
        int PageSize { get; set; }
        /// <summary>
        /// 是否存在上一页
        /// </summary>
        bool HasPreviousPage { get; }
        /// <summary>
        /// 是否存在下一页
        /// </summary>
        bool HasNextPage { get; }
    }
    /// <summary>
    /// 分页通用类
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class PagedList<T> : List<T>, IPagedList
    {
        /// <summary>
        /// 数据源为IQueryable的范型
        /// </summary>
        /// <param name="source">数据源</param>
        /// <param name="pageIndex">当前页</param>
        /// <param name="pageSize">每页显示多少条记录</param>
        public PagedList(IQueryable<T> source, int pageIndex, int pageSize)
        {
            if (source != null) //判断传过来的实体集是否为空
            {
                int total = source.Count();
                this.TotalCount = total;
                this.TotalPages = total / pageSize;

                if (total % pageSize > 0)
                    TotalPages++;

                this.PageSize = pageSize;
                if (pageIndex > this.TotalPages)
                {
                    pageIndex = this.TotalPages;
                }
                if (pageIndex < 1)
                {
                    pageIndex = 1;
                }
                this.PageIndex = pageIndex;
                this.AddRange(source.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList()); //Skip是跳到第几页，Take返回多少条
            }
            else
            {
                this.TotalCount = 0;
                this.TotalPages = 1;
                this.PageSize = 10;
                this.PageIndex = 0;
            }
        }
        /// <summary>
        /// 数据源为IEnumerable的范型, 本构造函数只用于手动构造分页数据（慎用！！！）
        /// </summary>
        /// <param name="source">数据源</param>
        /// <param name="index">当前页</param>
        /// <param name="pageSize">每页数据条数</param>
        /// <param name="total">总数据条数</param>
        /// <param name="totalPages">总页数</param>
        public PagedList(IEnumerable<T> source, int index, int pageSize, int total, int totalPages)
        {
            if (source != null)
            {
                this.TotalCount = total;
                this.TotalPages = totalPages;

                this.PageSize = pageSize;
                this.PageIndex = index;
                this.AddRange(source.ToList());
            }
            else
            {
                this.TotalCount = 0;
                this.TotalPages = 1;
                this.PageSize = 10;
                this.PageIndex = 0;
            }
        }

        /// <summary>
        /// 总页数
        /// </summary>
        public int TotalPages { get; set; }
        /// <summary>
        /// 总记录数
        /// </summary>
        public int TotalCount { get; set; }
        /// <summary>
        /// 当前页
        /// </summary>
        public int PageIndex { get; set; }
        /// <summary>
        /// 每页显示多少条记录
        /// </summary>
        public int PageSize { get; set; }
        /// <summary>
        /// 是否有上一页
        /// </summary>
        public bool HasPreviousPage { get { return (PageIndex > 1); } }
        /// <summary>
        /// 是否有下一页
        /// </summary>
        public bool HasNextPage { get { return (PageIndex * PageSize) < TotalCount; } }

        /// <summary>
        /// 额外参数
        /// </summary>
        public Dictionary<string, object> ExtensionParameters { get; set; }
    }

    /// <summary>
    /// PagedList扩展类
    /// </summary>
    public static class ExtendPagedList
    {
        /// <summary>
        ///  返回PagedList&lt;T&gt;类型的IQueryable的扩展方法
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="linq"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public static PagedList<T> ToPagedList<T>(this IQueryable<T> linq, int pageIndex, int pageSize)
        {
            return new PagedList<T>(linq, pageIndex, pageSize);
        }
    }
}
