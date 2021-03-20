using System.ComponentModel.DataAnnotations;

namespace Planner.Api.Model
{
    /// <summary>
    /// 
    /// </summary>
    public class UpdateFinanceStatementRequest
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
        public System.Guid Id { get; set; }

        /// <summary>
        /// expense title
        /// </summary>
        [Required]
        public string Title { get; set; }
    }
}
