/**
 * 路由配置
 */
export default [
  {
    path: '/user',
    component: '../layouts/UserLayout',
    routes: [
      {
        name: 'login',
        path: '/user/login',
        component: './user/login',
      }
    ]
  },
  {
    path: '/',
    component: '../layouts/SecurityLayout',
    routes: [
      {
        path: '/',
        component: '../layouts/BasicLayout',
        authority: ['admin', 'user'],
        routes: [
          {
            path: '/',
            redirect: '/welcome',
          },
          {
            path: '/welcome',
            name: 'welcome',
            icon: 'smile',
            component: './Welcome',
          },
          {
            component: './404',
          },
        ]
      },
      {
        components: './404'
      }
    ]
  },
  {
    components: './404'
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
