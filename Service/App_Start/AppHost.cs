using System.Configuration;
using Funq;
using Service.App_Start;
using Service.Service;
using ServiceStack.OrmLite;
using ServiceStack.OrmLite.SqlServer;
using ServiceStack.Text;
using ServiceStack.WebHost.Endpoints;
using WebActivator;

[assembly: PreApplicationStartMethod(typeof (AppHost), "Start")]

namespace Service.App_Start
{
    public class AppHost : AppHostBase
    {
        public AppHost() : base("StarterTemplate ASP.NET Host", typeof (UserService).Assembly)
        {
        }

        public override void Configure(Container container)
        {
            //Set JSON web services to return idiomatic JSON camelCase properties
            JsConfig.EmitCamelCaseNames = true;

            //Configure User Defined REST Paths
            Routes.Add<UserRequest>("/user/{Id}");

            string connectionString = ConfigurationManager.ConnectionStrings["AppDb"].ConnectionString;

            container.Register<IDbConnectionFactory>(c =>
                                                     new OrmLiteConnectionFactory(connectionString,
                                                                                  SqlServerOrmLiteDialectProvider.
                                                                                      Instance));
        }

        public static void Start()
        {
            new AppHost().Init();
        }
    }
}