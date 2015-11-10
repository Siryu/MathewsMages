using massey_effect_matthews_mages.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace massey_effect_matthews_mages.Membership
{
	public class LogUserStore : IUserStore<LogUser, int>, 
		IUserPasswordStore<LogUser, int>, IUserLockoutStore<LogUser, int>,
		IUserTwoFactorStore<LogUser, int>, IUserRoleStore<LogUser, int>
	{
		private TempDBLogUsers Context;

		public LogUserStore(DbContext context)
		{
			Context = (TempDBLogUsers)context;
		}

		public Task CreateAsync(LogUser user)
		{
			
			Role role = Context.Roles.Where(r => r.Name == "User").FirstOrDefault();
			user.Roles.Add(role);
			Context.LogUsers.Add(user);
			
			return Context.SaveChangesAsync();
		}

		public Task DeleteAsync(LogUser user)
		{
			Context.LogUsers.Remove(user);
			return Context.SaveChangesAsync();
		}

		public Task<LogUser> FindByIdAsync(int userId)
		{
			return Context.LogUsers.Where(u => u.Id == userId).FirstOrDefaultAsync();
		}

		public Task<LogUser> FindByNameAsync(string userName)
		{
			return Context.LogUsers.Where(u => u.UserName == userName).FirstOrDefaultAsync();
		}

		public Task UpdateAsync(LogUser user)
		{
			Context.LogUsers.Attach(user);
			Context.Entry(user).State = EntityState.Modified;
			return Context.SaveChangesAsync();
		}

		public void Dispose()
		{
			Context.Dispose();
		}

		public Task<string> GetPasswordHashAsync(LogUser user)
		{
			return Task.FromResult(user.Password);
		}

		public Task<bool> HasPasswordAsync(LogUser user)
		{
			return Task.FromResult(user.Password != null);
		}

		public Task SetPasswordHashAsync(LogUser user, string passwordHash)
		{
			user.Password = passwordHash;
			return Task.FromResult(0);
		}

		public Task<int> GetAccessFailedCountAsync(LogUser user)
		{
			return Task.FromResult(0);
		}

		public Task<bool> GetLockoutEnabledAsync(LogUser user)
		{
			return Task.FromResult(false);
		}

		public Task<DateTimeOffset> GetLockoutEndDateAsync(LogUser user)
		{
			throw new NotImplementedException();
		}

		public Task<int> IncrementAccessFailedCountAsync(LogUser user)
		{
			return Task.FromResult(0);
		}

		public Task ResetAccessFailedCountAsync(LogUser user)
		{
			return Task.FromResult(0);
		}

		public Task SetLockoutEnabledAsync(LogUser user, bool enabled)
		{
			throw new NotImplementedException();
		}

		public Task SetLockoutEndDateAsync(LogUser user, DateTimeOffset lockoutEnd)
		{
			throw new NotImplementedException();
		}

		public Task<bool> GetTwoFactorEnabledAsync(LogUser user)
		{
			return Task.FromResult(false);
		}

		public Task SetTwoFactorEnabledAsync(LogUser user, bool enabled)
		{
			throw new NotImplementedException();
		}

		public Task AddToRoleAsync(LogUser user, string roleName)
		{
			Role role = user.Roles.Where(r => r.Name == roleName).FirstOrDefault();
			user.Roles.Add(role);
			return Context.SaveChangesAsync();
		}

		public Task<IList<string>> GetRolesAsync(LogUser user)
		{
			IList<string> roleList = new List<string>();
			foreach (Role r in user.Roles)
				roleList.Add(r.Name);
			return Task.FromResult(roleList);
		}

		public Task<bool> IsInRoleAsync(LogUser user, string roleName)
		{
			bool isInRole = user.Roles.Where(r => r.Name == roleName).Any();
			return Task.FromResult(isInRole);
		}

		public Task RemoveFromRoleAsync(LogUser user, string roleName)
		{
			throw new NotImplementedException();
		}


		public Task<LogUser> FindByIdAsync(string userId)
		{
			int id = 0;
			Int32.TryParse(userId, out id);
			return this.FindByIdAsync(id);
		}
	}
}