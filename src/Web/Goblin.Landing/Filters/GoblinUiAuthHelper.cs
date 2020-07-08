using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Goblin.Core.Constants;
using Goblin.Core.Utils;
using Goblin.Identity.Share;
using Goblin.Identity.Share.Models.UserModels;
using Goblin.Landing.Core;
using Goblin.Landing.Models.Attributes;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Goblin.Landing.Filters
{
    public static class GoblinUiAuthHelper
    {
        public static bool IsAuthenticated(FilterContext context)
        {
            if (!(context.ActionDescriptor is ControllerActionDescriptor controllerActionDescriptor))
            {
                return true;
            }

            // If allow anonymous => the user/request is authenticated
            
            if (IsActionAllowAnonymous(controllerActionDescriptor))
            {
                return true;
            }

            return LoggedInUser<GoblinIdentityUserModel>.Current != null;
        }

        public static void BindLoggedInUser(HttpContext httpContext)
        {
            var accessToken = CookieHelper.GetShare<string>(httpContext, GoblinCookieKeys.AccessToken);

            if (string.IsNullOrWhiteSpace(accessToken))
            {
                return;
            }

            try
            {
                var userModelTask = GoblinIdentityHelper.GetProfileByAccessTokenAsync(accessToken);

                userModelTask.Wait();

                var userModel = userModelTask.Result;

                LoggedInUser<GoblinIdentityUserModel>.Current = new LoggedInUser<GoblinIdentityUserModel>(userModel);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }
        
         /// <summary>
        ///     Check user is authorization. Please call <see cref="IsAuthenticated" /> to check
        ///     authenticated before call this method.
        /// </summary>
        /// <param name="context">  </param>
        /// <returns>  </returns>
        public static bool IsAuthorized(FilterContext context)
        {
            if (!(context.ActionDescriptor is ControllerActionDescriptor controllerActionDescriptor))
            {
                return true;
            }

            // If allow anonymous => the user/request is authorized
            
            if (IsActionAllowAnonymous(controllerActionDescriptor))
            {
                return true;
            }

            var listAuthorizeAttribute = new List<AuthAttribute>();

            var actionAttributes = controllerActionDescriptor.MethodInfo.GetCustomAttributes<AuthAttribute>(false).ToList();

            if (!actionAttributes.Any())
            {
                actionAttributes = controllerActionDescriptor.MethodInfo.GetCustomAttributes<AuthAttribute>(true).ToList();
            }

            if (actionAttributes.Any()) listAuthorizeAttribute.AddRange(actionAttributes);

            // If Action combine Controller authorize or Action not have any Authorize Attribute,
            // then add allow role of controller to the list allow role.
            
            var isCombineAuthorize = controllerActionDescriptor.MethodInfo.GetCustomAttributes<CombineAuthAttribute>(false).Any();
            
            var isCombineAuthorizeByBase = controllerActionDescriptor.MethodInfo.GetCustomAttributes<CombineAuthAttribute>(true).Any();

            if (!isCombineAuthorize && isCombineAuthorizeByBase)
            {
                isCombineAuthorize = true;
            }

            if (isCombineAuthorize || !listAuthorizeAttribute.Any())
            {
                var controllerAttributes = controllerActionDescriptor.ControllerTypeInfo.GetCustomAttributes<AuthAttribute>(false).ToList();

                if (!controllerAttributes.Any())
                {
                    controllerAttributes = controllerActionDescriptor.ControllerTypeInfo.GetCustomAttributes<AuthAttribute>(true).ToList();
                }

                if (controllerAttributes.Any())
                {
                    listAuthorizeAttribute.AddRange(controllerAttributes);
                }
            }

            // If list attribute or list allow role don't have any thing => User is authorized
            
            if (!listAuthorizeAttribute.Any() || listAuthorizeAttribute.SelectMany(x => x.Permissions).Any() != true)
            {
                return true;
            }

            // Apply rule AND conditional for list attribute, OR conditional for role into an attribute

            // Only check attribute have role
            
            listAuthorizeAttribute = listAuthorizeAttribute.Where(x => x.Permissions?.Any() == true).ToList();

            var listRequirePermission = listAuthorizeAttribute.SelectMany(x => x.Permissions).ToList();

            var loggedInUserPermissions = LoggedInUser<GoblinIdentityUserModel>.Current.Data.Permissions;

            var isAuthorized = listRequirePermission.Any(x => loggedInUserPermissions?.Contains(x) == true);
                
            return isAuthorized;
        }
        
        public static bool IsActionAllowAnonymous(ControllerActionDescriptor controllerActionDescriptor)
        {
            // If action have any AllowAnonymousAttribute => Allow Anonymous
            
            var isActionAllowAnonymous = controllerActionDescriptor.MethodInfo.GetCustomAttributes<AllowAnonymousAttribute>(false).Any();

            if (!isActionAllowAnonymous)
            {
                isActionAllowAnonymous = controllerActionDescriptor.MethodInfo.GetCustomAttributes<AllowAnonymousAttribute>(true).Any();
            }

            if (isActionAllowAnonymous)
            {
                return true;
            }

            var isActionHaveAnyPermission = controllerActionDescriptor.MethodInfo.GetCustomAttributes<AuthAttribute>(true).Any();

            var isCombineAuthorize = controllerActionDescriptor.MethodInfo.GetCustomAttributes<CombineAuthAttribute>(true).Any();

            if (!isCombineAuthorize && isActionHaveAnyPermission)
            {
                return false;
            }

            var isControllerAllowAnonymous = controllerActionDescriptor.ControllerTypeInfo.GetCustomAttributes<AllowAnonymousAttribute>(true).Any();

            if (isControllerAllowAnonymous)
            {
                return true;
            }

            if (!isActionHaveAnyPermission)
            {
                var isControllerHaveAnyRole = controllerActionDescriptor.ControllerTypeInfo.GetCustomAttributes<AuthAttribute>(true).Any();

                return !isControllerHaveAnyRole;
            }

            return false;
        }
    }
}