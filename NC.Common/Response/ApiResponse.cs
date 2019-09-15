using System;
using System.Net;
using Newtonsoft.Json;

namespace NC.Common.Response
{

    /// <summary>
    /// 后台返回JSON信息包装模型（string data）
    /// </summary>
    public class ApiResponse
    {
        /// <summary>
        /// Http 请求状态，用于异常处理(System.Net.HttpStatusCode)
        /// </summary>
        public int Code { get; set; }
        /// <summary>
        /// 错误消息
        /// </summary>
        public string Message { get; set; }
        /// <summary>
        /// String 数据
        /// </summary>
        protected string Data { get; set; }

        /// <summary>
        /// 请求失败
        /// </summary>
        public ApiResponse()
        {
            Message = "SUCCESS";
            Code = (int)HttpStatusCode.OK;
        }

        /// <summary>
        /// 初始化模型，请求失败
        /// </summary>
        public ApiResponse(Exception ex)
        {
            Message = ex.Message;
            Data = ex.StackTrace;
            Code = (int)HttpStatusCode.BadRequest;
        }

        /// <summary>
        /// 请求失败
        /// </summary>
        /// <param name="errorMessage">异常消息</param>
        public ApiResponse(string errorMessage)
        {
            Message = errorMessage;
            Code = (int)HttpStatusCode.BadRequest;
        }

        /// <summary>
        /// 序列化当前对象
        /// </summary>
        /// <returns></returns>
        public string ToJson()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
    /// <summary>
    /// 后台返回JSON信息包装泛型模型
    /// </summary>
    /// <typeparam name="T">返回数据类型</typeparam>
    public class ApiResponse<T> : ApiResponse
    {
        /// <summary>
        /// 初始化泛型模型，请求成功
        /// </summary>
        /// <param name="t">要返回的类型值</param>
        public ApiResponse(T t)
        {
            Data = t;
            Code = (int)HttpStatusCode.OK;
        }

        /// <summary>
        /// 返回的数据类型
        /// </summary>
        public new T Data { get; set; }

        /// <summary>
        /// 泛型类型JsonResponse不提供ToJson方法！
        /// </summary>
        /// <returns></returns>
        public new string ToJson()
        {
            return "泛型类型JsonResponse<T>不提供ToJson方法！";
        }
    }

}
