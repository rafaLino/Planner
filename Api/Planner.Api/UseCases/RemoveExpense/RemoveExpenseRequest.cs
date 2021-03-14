﻿using System.ComponentModel.DataAnnotations;

namespace Planner.Api.UseCases.RemoveExpense
{
    /// <summary>
    /// 
    /// </summary>
    public class RemoveExpenseRequest
    {
        /// <summary>
        /// account id
        /// </summary>
        [Required]
        public System.Guid AccountId { get; set; }

        /// <summary>
        /// expense id
        /// </summary>
        [Required]
        public System.Guid ExpenseId { get; set; }
    }
}
