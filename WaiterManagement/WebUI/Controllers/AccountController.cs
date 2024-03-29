﻿using System.Web.Mvc;
using System.Web.Security;
using WebUI.Infrastructure.Abstract;
using WebUI.Models;

namespace WebUI.Controllers
{
	public class AccountController : Controller
	{
		#region Dependencies

		readonly IAuthProvider _authProvider;
		private readonly IClientDataAccess _clientDataAccess;

		#endregion

		#region Controllers

		public AccountController(IAuthProvider auth, IClientDataAccess clientDataAccess)
		{
			_authProvider = auth;
			_clientDataAccess = clientDataAccess;
		}

		#endregion

		#region Actions

		public ViewResult Login()
		{
			return View();
		}

		[HttpPost]
		public ActionResult Login(LoginViewModel model, string returnUrl)
		{
			if (ModelState.IsValid)
			{
				if (_authProvider.Authenticate(model.UserName, model.Password))
				{
					return Redirect(returnUrl ?? Url.Action("Index", "Cart"));
				}

				ModelState.AddModelError("", "Incorrect username or password");
				return View();
			}
			else
			{
				return View();
			}
		}

		public PartialViewResult Summary()
		{
			return PartialView(_authProvider);
		}

		public ActionResult Logout(string returnUrl)
		{
			FormsAuthentication.SignOut();

			return Redirect(returnUrl);
		}

		public ViewResult Orders()
		{
			return View(_clientDataAccess.GetOrders(_authProvider.GetClientId()));
		}

		#endregion
	}
}