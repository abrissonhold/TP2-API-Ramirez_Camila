using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class ApproverRole
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public ICollection<ApprovalRule> ApprovalRules { get; set; } = new List<ApprovalRule>();
        public ICollection<ProjectApprovalStep> ProjectApprovalSteps { get; set; } = new List<ProjectApprovalStep>();
        public ICollection<User> Users { get; set; } = new List<User>();
    }
}
