/**
 * 当前权限
 */
let CURRENT: string | string[] = 'NULL';
type CurrentAuthorityType = string | string[] | (() => typeof CURRENT);

//!TODO ts语法不明 ~~o(>_<)o ~~
/**
 * 渲染授权？
 * @param Authorized
 */
const renderAuthorize = <T>(Authorized: T): ((currentAuthority: CurrentAuthorityType) => T) => (
  currentAuthority: CurrentAuthorityType
): T => {
  if (currentAuthority) {
    if (typeof currentAuthority === 'function') {
      CURRENT = currentAuthority();
    }
    if (Object.prototype.toString.call(currentAuthority) === '[object String]' || Array.isArray(currentAuthority)) {
      CURRENT = currentAuthority as string[];
    }
  } else {
    CURRENT = 'NULL';
  }
  return Authorized;
}

export { CURRENT };
export default <T>(Authorized: T) => renderAuthorize<T>(Authorized);

// const func = <T>(param: T): ((currentAuthority: CurrentAuthorityType) => T) => (
//   curr: CurrentAuthorityType
// ): T => {
//   return param;
// }


// const funcT =<T>(param:T):T => {
//   const result:Partial<T>={};
//   return <T>result;
// }

// funcT<string>('leo');
// funcT<number>(123);
