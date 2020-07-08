using System.Collections.Generic;
using System.Linq;
using Elect.Core.DictionaryUtils;
using Elect.Web.Middlewares.HttpContextMiddleware;
using Goblin.Identity.Share.Models.UserModels;
using Goblin.Landing.Core;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Goblin.Landing.TagHelpers
{
    [HtmlTargetElement(MenuItemTagName, TagStructure = TagStructure.NormalOrSelfClosing)]
    public class PortalMenuItemTagHelper : TagHelper
    {
        public const string MenuItemTagName = "menu-item";

        [HtmlAttributeName("menu-name")]
        public string MenuName { get; set; }

        [HtmlAttributeName("menu-url")]
        public string MenuUrl { get; set; }

        [HtmlAttributeName("menu-permissions")]
        public IEnumerable<string> Permissions { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            var loggedInUser = LoggedInUser<GoblinIdentityUserModel>.Current?.Data;
            
            if (Permissions?.Any() == true && loggedInUser?.Permissions.Any(x => Permissions.Contains(x)) != true)
            {
                output.TagName = string.Empty;
                output.Content.Clear();
                return;
            }

            // Setup Output Tag
            
            output.TagMode = TagMode.StartTagAndEndTag;

            output.TagName = "li";

            output.Attributes.Add("class", "nav-item");

            // Anchor 
            
            var anchor = new TagBuilder("a");
            
            anchor.InnerHtml.Append(MenuName);

            anchor.AddCssClass("nav-link");

            anchor.Attributes.AddOrUpdate("href", MenuUrl);
            
            bool isActive = !string.IsNullOrWhiteSpace(MenuUrl) && HttpContext.Current?.Request.GetDisplayUrl().Contains(MenuUrl) == true;
            anchor.Attributes.AddOrUpdate("aria-selected", isActive ? "true" : "false");
            
            // Add Anchor to Output
            
            output.Content.AppendHtml(anchor);
        }
    }
}