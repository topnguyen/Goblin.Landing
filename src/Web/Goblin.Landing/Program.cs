using System.Collections.Generic;
using System.Threading.Tasks;
using Goblin.Identity.Share;
using Goblin.Identity.Share.Models.RoleModels;
using Microsoft.AspNetCore.Hosting;

namespace Goblin.Landing
{
    public static class Program
    {
        public static async Task Main(string[] args)
        {
            await Goblin.Core.Web.Setup.ProgramHelper.Main(args, webHostBuilder =>
                {
                    webHostBuilder.UseStartup<Startup>();
                }, scope =>
                {
                    // Register Role and Permission
                    
                    var roleModel = new GoblinIdentityUpsertRoleModel
                    {
                        Name = "Admin",
                        Permissions = new List<string>
                        {
                            "Member Manager",
                            "Blog Manager",
                            "Work Manager",
                            "Tech Manager"
                        }
                    };

                    var upsertRoleTask = GoblinIdentityHelper.UpsertRoleAsync(roleModel);

                    upsertRoleTask.Wait();

                    var upsertRoleResult = upsertRoleTask.Result;
                }
            );
        }
    }
}