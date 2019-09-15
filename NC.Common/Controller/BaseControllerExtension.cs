using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using NC.Common.Response;

namespace NC.Common.Controller
{
    /// <summary>
    /// 控制器基类扩展方法
    /// </summary>
    public static class BaseControllerExtension
    {
        /// <summary>
        /// 返回 JsonResult
        /// </summary>
        private static JsonResult GetResult(object obj)
        {
            return new JsonResult(obj);
        }

        /// <summary>
        /// 操作成功
        /// </summary>
        /// <typeparam name="T">操作结果类型</typeparam>
        /// <param name="controller"></param>
        /// <param name="data">响应数据</param>
        /// <returns>返回操作状态</returns>
        public static JsonResult Success(this BaseController controller)
        {
            return GetResult(new ApiResponse());
        }

        /// <summary>
        /// 操作成功
        /// </summary>
        /// <typeparam name="T">操作结果类型</typeparam>
        /// <param name="controller"></param>
        /// <param name="data">响应数据</param>
        /// <returns>返回操作状态、字符串类型操作结果</returns>
        public static JsonResult Success(this BaseController controller, string data)
        {
            return GetResult(new ApiResponse<string>(data));
        }

        /// <summary>
        /// 操作成功
        /// </summary>
        /// <typeparam name="T">操作结果类型</typeparam>
        /// <param name="controller"></param>
        /// <param name="data">响应数据</param>
        /// <returns>返回操作状态以及操作结果</returns>
        public static JsonResult Success<T>(this BaseController controller, T data)
        {
            return GetResult(new ApiResponse<T>(data));
        }

        /// <summary>
        /// 操作失败
        /// </summary>
        /// <param name="errorMessage">异常信息</param>
        /// <returns>返回操作异常状态</returns>
        public static JsonResult Fail(this BaseController controller)
        {
            return GetResult(new ApiResponse());
        }

        /// <summary>
        /// 操作失败
        /// </summary>
        /// <param name="errorMessage">异常信息</param>
        /// <returns>返回异常信息</returns>
        public static JsonResult Fail(this BaseController controller, string errorMessage)
        {
            return GetResult(new ApiResponse(errorMessage));
        }

        /// <summary>
        /// 操作失败
        /// </summary>
        /// <param name="errorMessage">异常信息</param>
        /// <returns>返回异常描述以及堆栈信息</returns>
        public static JsonResult Fail(this BaseController controller, Exception ex)
        {
            return GetResult(new ApiResponse(ex));
        }
    }
}
