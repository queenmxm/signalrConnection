using Microsoft.Owin;
using Owin;
using signarl114.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
[assembly: OwinStartup(typeof(signarl114.Startup))]
namespace signarl114
{

    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.MapSignalR<MyConnection1>("/mmConnection");         
        }
    }
}