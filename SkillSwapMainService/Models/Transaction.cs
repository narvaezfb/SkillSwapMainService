using System;
namespace SkillSwapMainService.Models
{
	public class Transaction
	{
		public int TransactionID { get; set; }

		public int OffererUserID { get; set; }

		public int ReceiverUserID { get; set; }

		public int OfferedSkillID { get; set; }

		public int ReceivedSkillID { get; set; }

		public DateTime TransactionDate { get; set; }

		public int Duration { get; set; }

		public bool isComplete { get; set; }

		public Skill OfferedSkill { get; set; }

        public Skill ReceivedSkill { get; set; }

    }
}

