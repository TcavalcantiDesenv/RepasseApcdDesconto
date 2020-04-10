using System.Collections.Generic;

namespace PlatinDashboard.Domain.Entities
{
    public class UserStore
    {
        public int UserStoreId { get; set; }
        public string UserId { get; set; }
        public int StoreId { get; set; }
        public virtual User User { get; set; }
        public virtual Store Store { get; set; }
    }
}
