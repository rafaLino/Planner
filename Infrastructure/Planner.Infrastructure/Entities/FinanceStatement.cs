using System;

namespace Planner.Infrastructure.Entities
{
    public class FinanceStatement
    {
        public Guid Id { get; set; }
        public string Title { get; set; }

        public DateTime ReferenceDate { get; set; }

        public Guid AccountId { get; set; }
    }
}
