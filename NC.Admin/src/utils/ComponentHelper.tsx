import React from 'react';

/**
 * 验证是否为 class 组件
 */
export const isComponentClass = (component: React.ComponentClass | React.ReactNode): boolean => {
  if (!component) {
    return false;
  }
  const proto = Object.getPrototypeOf(component);
  if (proto === React.Component || proto === Function.prototype) {
    return true;
  }
  return isComponentClass(proto);
}

/**
 * 判断组件是否已实例化
 * Determine whether the incoming component has been instantiated
 * AuthorizedRoute is already instantiated
 * Authorized  render is already instantiated, children is no instantiated
 * Secured is not instantiated
 * @param target
 */
export const checkIsInstantiation = (target: React.ComponentClass | React.ReactNode) => {
  if (isComponentClass(target)) {
    const Target = target as React.ComponentClass;
    return (props: any) => <Target {...props} />;
  }
  if (React.isValidElement(target)) {
    return (props: any) => React.cloneElement(target, props);
  }
  return () => target as (React.ReactNode & null);
}
