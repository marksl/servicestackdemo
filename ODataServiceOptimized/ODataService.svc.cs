using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Objects;
using System.Data.Services;
using System.Data.Services.Common;
using System.Linq;

using DataServicesJSONP;
using Service;

namespace ODataService
{
    [JSONPSupportBehavior]
    public class ODataService : DataService<servicestackEntities>, IServiceProvider,  IExpandProvider 
    {
        public static void InitializeService(DataServiceConfiguration config)
        {
            config.SetEntitySetAccessRule("Contacts", EntitySetRights.AllRead);
            config.SetEntitySetAccessRule("ContactGroups", EntitySetRights.AllRead);
            config.SetEntitySetAccessRule("Users", EntitySetRights.AllRead);
            
            config.DataServiceBehavior.MaxProtocolVersion = DataServiceProtocolVersion.V2;
        }

        public IEnumerable ApplyExpansions(IQueryable queryable, ICollection<ExpandSegmentCollection> expandPaths)
        {
            if (queryable == null) throw new ArgumentNullException("queryable");

            dynamic query = queryable;
            return query.Execute(MergeOption.AppendOnly);
        }

        public object GetService(Type serviceType)
        {
            if (serviceType == null) throw new ArgumentNullException("serviceType");

            return (serviceType == typeof(IExpandProvider) ? this : null);
        }

    }
}
