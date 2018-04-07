using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(The_Ace_of_Spades_Pizza.Startup))]
namespace The_Ace_of_Spades_Pizza
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
