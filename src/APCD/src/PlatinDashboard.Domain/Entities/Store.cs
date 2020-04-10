using System.Collections.Generic;

namespace PlatinDashboard.Domain.Entities
{
    public class Store
    {
        public int StoreId { get; set; }
        public string Name { get; set; }
        public int ExternalStoreId { get; set; }
        public int CompanyId { get; set; }
        public virtual Company Company { get; set; }
        public virtual ICollection<UserStore> UsersStores { get; set; }
    }
}
