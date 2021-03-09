using System;

namespace Planner.Infrastructure.Entities
{
    public class FinanceStatement : Entity
    {
        public string Title { get; set; }

        public DateTime ReferenceDate { get; set; }

        public string AccountId { get; set; }
    }
}
