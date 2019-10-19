/**
 * 路由配置
 */
export default [
  {
    path: '/',
    component: '../layouts/BasicLayout',
    routes: [
      { path: '/', component: '../pages/index' }
    ]
  }
]

// [
//   {
//     path: '/',
//     component: '../layouts/index',
//     routes: [
//       { path: '/', component: '../pages/index' }
//     ]
//   }
// ]
