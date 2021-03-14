using System;
using System.ComponentModel.DataAnnotations;

namespace Planner.Api.Model
{
    /// <summary>
    /// 
    /// </summary>
    public class AmountRecordModel
    {
        /// <summary>
        /// amount record id if exists
        /// </summary>
        public Guid? Id { get; set; } = null;

        /// <summary>
        /// option description
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// amount
        /// </summary>
        [Required]
        public decimal Amount { get; set; }
    }
}
