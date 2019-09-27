import { extend } from 'umi-request';
import { notification } from 'antd';

/**
 * 请求状态描述
 */
const codeMessages = [
  {
    Code: 200,
    Message: '服务器成功返回请求的数据。'
  },
  {
    Code: 201,
    Message: '新建或修改数据成功。'
  },
  {
    Code: 202,
    Message: '一个请求已经进入后台排队（异步任务）。'
  },
  {
    Code: 204,
    Message: '删除数据成功。'
  },
  {
    Code: 400,
    Message: '发出的请求有错误，服务器没有进行新建或修改数据的操作。'
  },
  {
    Code: 401,
    Message: '用户没有权限（令牌、用户名、密码错误）。'
  },
  {
    Code: 403,
    Message: '用户得到授权，但是访问是被禁止的。'
  },
  {
    Code: 404,
    Message: '发出的请求针对的是不存在的记录，服务器没有进行操作。'
  },
  {
    Code: 406,
    Message: '请求的格式不可得。'
  },
  {
    Code: 410,
    Message: '请求的资源被永久删除，且不会再得到的。'
  },
  {
    Code: 422,
    Message: '当创建一个对象时，发生一个验证错误。'
  },
  {
    Code: 500,
    Message: '服务器发生错误，请检查服务器。'
  },
  {
    Code: 502,
    Message: '网关错误。'
  },
  {
    Code: 503,
    Message: '服务不可用，服务器暂时过载或维护。'
  },
  {
    Code: 504,
    Message: '网关超时。'
  },
];

/**
 * 异常处理程序
 */
const errorHandler = (error: { response: Response }): Response => {
  const { response } = error;
  if (response && response.status) {
    const msg = codeMessages.find(v => v.Code === response.status);
    const errorText = msg ? msg.Message : response.statusText;
    const { status, url } = response;

    notification.error({
      message: `请求错误 ${status}: ${url}`,
      description: errorText,
    });
  } else if (!response) {
    notification.error({
      description: '您的网络发生异常，无法连接服务器',
      message: '网络异常',
    });
  }

  return response;
}

/**
 * 配置request请求时的默认参数
 */
const request = extend({
  errorHandler,           // 默认异常处理
  credentials: 'include', // 默认请求是否带上cookie
})
