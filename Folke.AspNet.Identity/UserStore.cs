using Folke.Orm;
using Microsoft.AspNet.Identity;
using System;
using System.Threading.Tasks;

namespace Folke.AspNet.Identity
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
            if (_disposed)
            {
                throw new ObjectDisposedException(GetType().Name);
            }
        }

        public async Task CreateAsync(T user)
        {
            ThrowIfDisposed();
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
            ThrowIfDisposed();
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }
            await connection.DeleteAsync(user);
        }

        public async Task<T> FindByIdAsync(string userId)
        {
            ThrowIfDisposed();
            if (userId == null)
            {
                throw new ArgumentNullException("userId");
            }
            return await connection.LoadAsync<T>(userId);
        }

        public async Task<T> FindByNameAsync(string userName)
        {
            ThrowIfDisposed();
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
            ThrowIfDisposed();
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
            ThrowIfDisposed();
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }
            return await Task.FromResult(user.PasswordHash);
        }

        public async Task<bool> HasPasswordAsync(T user)
        {
            ThrowIfDisposed();
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }
            return await Task.FromResult(user.PasswordHash != null);
        }

        public async Task SetPasswordHashAsync(T user, string passwordHash)
        {
            ThrowIfDisposed();
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
            ThrowIfDisposed();
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
            ThrowIfDisposed();
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }
            return Task.FromResult(user.Email);
        }

        public Task<bool> GetEmailConfirmedAsync(T user)
        {
            ThrowIfDisposed();
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }
            return Task.FromResult(user.EmailConfirmed);
        }

        public Task SetEmailAsync(T user, string email)
        {
            ThrowIfDisposed();
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }
            user.Email = email;
            return Task.FromResult(0);
        }

        public Task SetEmailConfirmedAsync(T user, bool confirmed)
        {
            ThrowIfDisposed();
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }
            user.EmailConfirmed = confirmed;
            return Task.FromResult(0);
        }

        public Task<int> GetAccessFailedCountAsync(T user)
        {
            ThrowIfDisposed();
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }
            return Task.FromResult(user.AccessFailedCount);
        }

        public Task<bool> GetLockoutEnabledAsync(T user)
        {
            ThrowIfDisposed();
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }
            return Task.FromResult(user.LockoutEnabled);
        }

        public Task<DateTimeOffset> GetLockoutEndDateAsync(T user)
        {
            ThrowIfDisposed();
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
            return Task.FromResult(dateTimeOffset);
        }

        public Task<int> IncrementAccessFailedCountAsync(T user)
        {
            ThrowIfDisposed();
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }
            
            user.AccessFailedCount = user.AccessFailedCount + 1;
            return Task.FromResult(0);
        }

        public Task ResetAccessFailedCountAsync(T user)
        {
            ThrowIfDisposed();
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }
            user.AccessFailedCount = 0;
            return Task.FromResult(0);
        }

        public Task SetLockoutEnabledAsync(T user, bool enabled)
        {
            ThrowIfDisposed();
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }
            user.LockoutEnabled = enabled;
            return Task.FromResult(0);
        }

        public Task SetLockoutEndDateAsync(T user, DateTimeOffset lockoutEnd)
        {
            ThrowIfDisposed();
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
            return Task.FromResult(0);
        }
        
        public Task<bool> GetTwoFactorEnabledAsync(T user)
        {
            ThrowIfDisposed();
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }
            return Task.FromResult(user.TwoFactorEnabled);
        }

        public Task SetTwoFactorEnabledAsync(T user, bool enabled)
        {
            ThrowIfDisposed();
            if (user == null)
                throw new ArgumentNullException("user");
            
            user.TwoFactorEnabled = enabled;
            return Task.FromResult(0);
        }
    }
}
