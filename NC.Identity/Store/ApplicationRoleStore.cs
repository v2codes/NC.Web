using Microsoft.AspNetCore.Identity;
using NC.Identity.Models;
using NC.Model.EntityModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace NC.Identity.Store
{
    /// <summary>
    /// 自定义 RoleStore
    /// </summary>
    public class ApplicationRoleStore : IRoleStore<SysRole>
    {
        private bool _disposed;
        private readonly ApplicationUserDbContext _db;
        public ApplicationRoleStore(ApplicationUserDbContext dbContext)
        {
            _db = dbContext;
        }

        /// <summary>
        /// 创建角色
        /// </summary>
        /// <param name="role"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<IdentityResult> CreateAsync(SysRole role, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            ThrowIfDisposed();

            if (role == null)
            {
                throw new ArgumentNullException(nameof(role));
            }
            await _db.AddAsync(role);
            await _db.SaveChangesAsync();
            return IdentityResult.Success;
        }

        /// <summary>
        /// 删除角色
        /// </summary>
        /// <param name="role"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<IdentityResult> DeleteAsync(SysRole role, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            ThrowIfDisposed();

            if (role == null)
            {
                throw new ArgumentNullException(nameof(role));
            }
            if (role.IsSystem == true)
            {
                var error = new IdentityError() { Description = $"系统级角色“{role.Name}”禁止删除" };
                return IdentityResult.Failed(error);
            }

            _db.Remove(role);
            await _db.SaveChangesAsync();
            return IdentityResult.Success;
        }

        /// <summary>
        /// 根据 ID 查找角色
        /// </summary>
        /// <param name="roleId"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public Task<SysRole> FindByIdAsync(string roleId, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            ThrowIfDisposed();

            var id = ConvertIdFromString(roleId);
            var role = _db.Roles.Find(roleId);
            return Task.FromResult(role);
        }

        /// <summary>
        /// 根据角色名称查找角色
        /// </summary>
        /// <param name="normalizedRoleName"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public Task<SysRole> FindByNameAsync(string normalizedRoleName, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            ThrowIfDisposed();

            var role = _db.Roles.FirstOrDefault(p => p.NormalizedName == normalizedRoleName);

            return Task.FromResult(role);
        }

        /// <summary>
        /// 获取角色ID
        /// </summary>
        /// <param name="role"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public Task<string> GetRoleIdAsync(SysRole role, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            ThrowIfDisposed();

            if (role == null)
            {
                throw new ArgumentNullException(nameof(role));
            }
            return Task.FromResult(role.Id.ToString());
        }

        /// <summary>
        /// 获取角色名称
        /// </summary>
        /// <param name="role"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public Task<string> GetRoleNameAsync(SysRole role, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            ThrowIfDisposed();

            if (role == null)
            {
                throw new ArgumentNullException(nameof(role));
            }
            return Task.FromResult(role.Name);
        }

        /// <summary>
        /// 获取规范化用户名
        /// </summary>
        /// <param name="role"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public Task<string> GetNormalizedRoleNameAsync(SysRole role, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            ThrowIfDisposed();

            if (role == null)
            {
                throw new ArgumentNullException(nameof(role));
            }
            return Task.FromResult(role.Name);
        }

        /// <summary>
        /// 设置规范化用户名
        /// </summary>
        /// <param name="role"></param>
        /// <param name="normalizedName"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public Task SetNormalizedRoleNameAsync(SysRole role, string normalizedName, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            ThrowIfDisposed();

            if (role == null)
            {
                throw new ArgumentNullException(nameof(role));
            }
            role.NormalizedName = normalizedName;
            return Task.FromResult<object>(null);
        }

        /// <summary>
        /// 设置角色名称
        /// </summary>
        /// <param name="role"></param>
        /// <param name="roleName"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public Task SetRoleNameAsync(SysRole role, string roleName, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            ThrowIfDisposed();

            if (role == null)
            {
                throw new ArgumentNullException(nameof(role));
            }
            role.Name = roleName;
            return Task.FromResult<object>(null);
        }

        /// <summary>
        /// 更新角色
        /// </summary>
        /// <param name="role"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<IdentityResult> UpdateAsync(SysRole role, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            ThrowIfDisposed();

            if (role == null)
            {
                throw new ArgumentNullException(nameof(role));
            }
            _db.Update(role);
            await _db.SaveChangesAsync();
            return IdentityResult.Success;
        }


        #region Other Functions

        /// <summary>
        /// Converts the provided <paramref name="id"/> to a strongly typed key object.
        /// </summary>
        /// <param name="id">The id to convert.</param>
        /// <returns>An instance of <typeparamref name="Guid"/> representing the provided <paramref name="id"/>.</returns>
        public virtual Guid ConvertIdFromString(string id)
        {
            if (id == null)
            {
                return default(Guid);
            }
            return (Guid)TypeDescriptor.GetConverter(typeof(Guid)).ConvertFromInvariantString(id);
        }

        /// <summary>
        /// Converts the provided <paramref name="id"/> to its string representation.
        /// </summary>
        /// <param name="id">The id to convert.</param>
        /// <returns>An <see cref="string"/> representation of the provided <paramref name="id"/>.</returns>
        public virtual string ConvertIdToString(Guid id)
        {
            if (id.Equals(default(Guid)))
            {
                return null;
            }
            return id.ToString();
        }

        /// <summary>
        /// 释放
        /// </summary>
        public void Dispose()
        {
            _disposed = true;
        }

        /// <summary>
        /// 如果已释放，则抛出异常
        /// </summary>
        protected void ThrowIfDisposed()
        {
            if (_disposed)
            {
                throw new ObjectDisposedException(GetType().Name);
            }
        }
        #endregion
    }
}
