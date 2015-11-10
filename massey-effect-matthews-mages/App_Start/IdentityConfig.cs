﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security;
using massey_effect_matthews_mages.Models;

namespace massey_effect_matthews_mages
{
	// Configure the application user manager used in this application. UserManager is defined in ASP.NET Identity and is used by the application.
	//public class ApplicationUserManager : UserManager<LogUser>
	//{
	//	public ApplicationUserManager(IUserStore<LogUser> store)
	//		: base(store)
	//	{
	//	}

	////	public static ApplicationUserManager Create(IdentityFactoryOptions<ApplicationUserManager> options, IOwinContext context) 
	////	{
	////		var manager = new ApplicationUserManager(new UserStore<LogUser>(context.Get<ApplicationDbContext>()));
	////		// Configure validation logic for usernames
	////		manager.UserValidator = new UserValidator<LogUser>(manager)
	////		{
	////			AllowOnlyAlphanumericUserNames = false,
	////			RequireUniqueEmail = true
	////		};

	////		// Configure validation logic for passwords
	////		manager.PasswordValidator = new PasswordValidator
	////		{
	////			RequiredLength = 6,
	////			RequireNonLetterOrDigit = true,
	////			RequireDigit = true,
	////			RequireLowercase = true,
	////			RequireUppercase = true,
	////		};

	////		// Configure user lockout defaults
	////		manager.UserLockoutEnabledByDefault = true;
	////		manager.DefaultAccountLockoutTimeSpan = TimeSpan.FromMinutes(5);
	////		manager.MaxFailedAccessAttemptsBeforeLockout = 5;

	////		// Register two factor authentication providers. This application uses Phone and Emails as a step of receiving a code for verifying the user
	////		// You can write your own provider and plug it in here.
	////		manager.RegisterTwoFactorProvider("Phone Code", new PhoneNumberTokenProvider<LogUser>
	////		{
	////			MessageFormat = "Your security code is {0}"
	////		});
	////		manager.RegisterTwoFactorProvider("Email Code", new EmailTokenProvider<LogUser>
	////		{
	////			Subject = "Security Code",
	////			BodyFormat = "Your security code is {0}"
	////		});
	////		manager.EmailService = new EmailService();
	////		manager.SmsService = new SmsService();
	////		var dataProtectionProvider = options.DataProtectionProvider;
	////		if (dataProtectionProvider != null)
	////		{
	////			manager.UserTokenProvider = 
	////				new DataProtectorTokenProvider<LogUser>(dataProtectionProvider.Create("ASP.NET Identity"));
	////		}
	////		return manager;
	////	}
	//}

	//// Configure the application sign-in manager which is used in this application.
	//public class ApplicationSignInManager : SignInManager<LogUser, string>
	//{
	//	public ApplicationSignInManager(ApplicationUserManager userManager, IAuthenticationManager authenticationManager)
	//		: base(userManager, authenticationManager)
	//	{
	//	}

	//	public override Task<ClaimsIdentity> CreateUserIdentityAsync(LogUser user)
	//	{
	//		return user.GenerateUserIdentityAsync((ApplicationUserManager)UserManager);
	//	}

	//	public static ApplicationSignInManager Create(IdentityFactoryOptions<ApplicationSignInManager> options, IOwinContext context)
	//	{
	//		return new ApplicationSignInManager(context.GetUserManager<ApplicationUserManager>(), context.Authentication);
	//	}
	//}
}
