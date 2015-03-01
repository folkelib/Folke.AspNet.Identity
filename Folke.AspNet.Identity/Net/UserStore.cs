using Folke.AspNet.Identity;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Folke.Orm.Identity
{
    public class UserStore<T> : IUserTwoFactorStore<T, string>, IUserLockoutStore<T, string>, IUserEmailStore<T>, IUserPasswordStore<T>, IUserStore<T> where T : IdentityUser, new()
    {
        private readonly FolkeConnection connection;
        private bool _disposed;

        public UserStore(FolkeConnection connection)
        {
            this.connection = connection;
        }

        private void ThrowIfDisposed()
        {
            if (this._disposed)
            {
                throw new ObjectDisposedException(this.GetType().Name);
            }
        }

        public async Task CreateAsync(T user)
        {
            this.ThrowIfDisposed();
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }
            
            using (var transaction = connection.BeginTransaction())
            {
                await connection.SaveAsync(user);
                transaction.Commit();
            }
        }

        public async Task DeleteAsync(T user)
        {
            this.ThrowIfDisposed();
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }
            await connection.DeleteAsync(user);
        }

        public async Task<T> FindByIdAsync(string userId)
        {
            this.ThrowIfDisposed();
            if (userId == null)
            {
                throw new ArgumentNullException("userId");
            }
            return await connection.LoadAsync<T>(userId);
        }

        public async Task<T> FindByNameAsync(string userName)
        {
            this.ThrowIfDisposed();
            if (userName == null)
            {
                throw new ArgumentNullException("userName");
            }            
            return await connection.QueryOver<T>()
                                .Where(x => x.UserName == userName)
                                .SingleOrDefaultAsync();         
        }

        public async Task UpdateAsync(T user)
        {
            this.ThrowIfDisposed();
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }
            await connection.UpdateAsync(user);            
        }

        public void Dispose()
        {
            connection.Dispose();
            _disposed = true;
        }

        public async Task<string> GetPasswordHashAsync(T user)
        {
            this.ThrowIfDisposed();
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }
            return await Task.FromResult<string>(user.PasswordHash);
        }

        public async Task<bool> HasPasswordAsync(T user)
        {
            this.ThrowIfDisposed();
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }
            return await Task.FromResult<bool>(user.PasswordHash != null);
        }

        public async Task SetPasswordHashAsync(T user, string passwordHash)
        {
            this.ThrowIfDisposed();
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }
            if (passwordHash == null)
            {
                throw new ArgumentNullException("passwordHash");
            }
            user.PasswordHash = passwordHash;
            await Task.Delay(0);
        }

        public async Task<T> FindByEmailAsync(string email)
        {
            this.ThrowIfDisposed();
            if (email == null)
            {
                throw new ArgumentNullException("email");
            }
            
            return await connection.QueryOver<T>()
                                .Where(x => x.Email == email)
                                .SingleOrDefaultAsync();            
        }

        public Task<string> GetEmailAsync(T user)
        {
            this.ThrowIfDisposed();
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }
            return Task<T>.FromResult(user.Email);
        }

        public Task<bool> GetEmailConfirmedAsync(T user)
        {
            this.ThrowIfDisposed();
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }
            return Task.FromResult<bool>(user.EmailConfirmed);
        }

        public Task SetEmailAsync(T user, string email)
        {
            this.ThrowIfDisposed();
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }
            user.Email = email;
            return Task.FromResult<int>(0);
        }

        public Task SetEmailConfirmedAsync(T user, bool confirmed)
        {
            this.ThrowIfDisposed();
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }
            user.EmailConfirmed = confirmed;
            return Task.FromResult<int>(0);
        }

        public Task<int> GetAccessFailedCountAsync(T user)
        {
            this.ThrowIfDisposed();
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }
            return Task.FromResult<int>(user.AccessFailedCount);
        }

        public Task<bool> GetLockoutEnabledAsync(T user)
        {
            this.ThrowIfDisposed();
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }
            return Task.FromResult<bool>(user.LockoutEnabled);
        }

        public Task<DateTimeOffset> GetLockoutEndDateAsync(T user)
        {
            this.ThrowIfDisposed();
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }

            DateTimeOffset dateTimeOffset;
            if (user.LockoutEndDateUtc.HasValue)
            {
                DateTime? lockoutEndDateUtc = user.LockoutEndDateUtc;
                dateTimeOffset = new DateTimeOffset(DateTime.SpecifyKind(lockoutEndDateUtc.Value, DateTimeKind.Utc));
            }
            else
            {
                dateTimeOffset = new DateTimeOffset();
            }
            return Task.FromResult<DateTimeOffset>(dateTimeOffset);
        }

        public Task<int> IncrementAccessFailedCountAsync(T user)
        {
            this.ThrowIfDisposed();
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }
            
            user.AccessFailedCount = user.AccessFailedCount + 1;
            return Task.FromResult<int>(0);
        }

        public Task ResetAccessFailedCountAsync(T user)
        {
            this.ThrowIfDisposed();
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }
            user.AccessFailedCount = 0;
            return Task.FromResult<int>(0);
        }

        public Task SetLockoutEnabledAsync(T user, bool enabled)
        {
            this.ThrowIfDisposed();
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }
            user.LockoutEnabled = enabled;
            return Task.FromResult<int>(0);
        }

        public Task SetLockoutEndDateAsync(T user, DateTimeOffset lockoutEnd)
        {
            this.ThrowIfDisposed();
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }

            DateTime? nullable;
            if (lockoutEnd == DateTimeOffset.MinValue)
                nullable = null;
            else
                nullable = new DateTime?(lockoutEnd.UtcDateTime);

            user.LockoutEndDateUtc = nullable;
            return Task.FromResult<int>(0);
        }
        
        public Task<bool> GetTwoFactorEnabledAsync(T user)
        {
            this.ThrowIfDisposed();
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }
            return Task.FromResult<bool>(user.TwoFactorEnabled);
        }

        public Task SetTwoFactorEnabledAsync(T user, bool enabled)
        {
            this.ThrowIfDisposed();
            if (user == null)
                throw new ArgumentNullException("user");
            
            user.TwoFactorEnabled = enabled;
            return Task.FromResult<int>(0);
        }
    }
}
