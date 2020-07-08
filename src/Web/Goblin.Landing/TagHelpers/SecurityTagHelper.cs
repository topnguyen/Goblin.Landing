using System;
using System.Collections.Generic;
using System.Linq;
using Goblin.Identity.Share.Models.UserModels;
using Goblin.Landing.Core;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Goblin.Landing.TagHelpers
{
 public enum SecurityMode
    {
        Hide,
        Disable,
        Readonly
    }

    [HtmlTargetElement("input")]
    [HtmlTargetElement("a")]
    [HtmlTargetElement("textarea")]
    [HtmlTargetElement("select")]
    [HtmlTargetElement("button")]
    [HtmlTargetElement("div")]
    [HtmlTargetElement("span")]
    [HtmlTargetElement("label")]
    [HtmlTargetElement("li")]
    [HtmlTargetElement("ul")]
    [HtmlTargetElement("script")]
    [HtmlTargetElement("style")]
    public class SecurityTagHelper : TagHelper
    {
        private const string SecurityModeAttributeName = "security-mode";

        private const string PermissionsAttributeName = "security-permission";

        [HtmlAttributeName(SecurityModeAttributeName)]
        public SecurityMode Mode { set; get; }

        [HtmlAttributeName(PermissionsAttributeName)]
        public IEnumerable<string> Permissions { set; get; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            var loggedInUser = LoggedInUser<GoblinIdentityUserModel>.Current?.Data;
            
            if (Permissions?.Any() == true && loggedInUser?.Permissions.Any(x => Permissions.Contains(x)) != true)
            {
                switch (Mode)
                {
                    case SecurityMode.Hide:
                        {
                            output.SuppressOutput();

                            return;
                        }
                    case SecurityMode.Disable:
                        {
                            var attribute = new TagHelperAttribute("disabled", "disabled");

                            output.Attributes.Add(attribute);

                            break;
                        }
                    case SecurityMode.Readonly:
                        {
                            var attribute = new TagHelperAttribute("readonly", "readonly");

                            output.Attributes.Add(attribute);

                            break;
                        }
                    default:
                        {
                            throw new ArgumentOutOfRangeException();
                        }
                }
            }

            base.Process(context, output);
        }
    }
}