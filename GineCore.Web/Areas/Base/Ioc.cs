using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GineCore.Web.Controllers.Base
{
    public class Ioc
    {
        public static T GetService<T>() 
        {
            IWebHost host = Program.BuildWebHost(null);
            IServiceScope scope = host.Services.CreateScope();
            return scope.ServiceProvider.GetService<T>();
        }
    }
}
