using System.Data.Services;
using System.Data.Services.Common;
using DataServicesJSONP;
using Service;

namespace ODataService
{
    [JSONPSupportBehavior]
    public class ODataService : DataService<servicestackEntities>
    {
        public static void InitializeService(DataServiceConfiguration config)
        {
            config.SetEntitySetAccessRule("Contacts", EntitySetRights.AllRead);
            config.SetEntitySetAccessRule("ContactGroups", EntitySetRights.AllRead);
            config.SetEntitySetAccessRule("Users", EntitySetRights.AllRead);
            
            config.DataServiceBehavior.MaxProtocolVersion = DataServiceProtocolVersion.V2;
        }
    }
}
