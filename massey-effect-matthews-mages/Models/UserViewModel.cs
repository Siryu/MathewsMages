using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace massey_effect_matthews_mages.Models
{
	public class UserViewModel
	{
		public User User { get; set; }
		[Range(0, double.MaxValue, ErrorMessage="Must be greater than 0")]
		public int Attack { get; set; }
		//[Range(0, double.MaxValue, ErrorMessage = "Must be greater than 0")]
		//public int AttackRange { get; set; }
		//[Range(0, double.MaxValue, ErrorMessage = "Must be greater than 0")]
		//public int AttackSpeed { get; set; }
		[Range(0, double.MaxValue, ErrorMessage = "Must be greater than 0")]
		public int CurrentHealth { get; set; }
		[Range(0, double.MaxValue, ErrorMessage = "Must be greater than 0")]
		public int Defense { get; set; }
		[Range(0, double.MaxValue, ErrorMessage = "Must be greater than 0")]
		public int MonstersKilled { get; set; }
		//[Range(0, double.MaxValue, ErrorMessage = "Must be greater than 0")]
		//public int MoveSpeed { get; set; }
		[Range(0, double.MaxValue, ErrorMessage = "Must be greater than 0")]
		public int RoomsTraveled { get; set; }
		[Range(0, double.MaxValue, ErrorMessage = "Must be greater than 0")]
		public int HighestAttackAchieved { get; set; }
		[Range(0, double.MaxValue, ErrorMessage = "Must be greater than 0")]
		public int HighestDefenseAchieved { get; set; }


		public UserViewModel()
		{

		}

		public UserViewModel(User user)
		{
			this.User = user;
			this.Attack = user.Attack;
			//this.AttackRange = user.AttackRange;
			//this.AttackSpeed = user.AttackSpeed;
			this.CurrentHealth = user.CurrentHealth;
			this.Defense = user.Defense;
			this.MonstersKilled = user.HighMonstersKilled;
			//this.MoveSpeed = user.MoveSpeed;
			this.RoomsTraveled = user.HighRoomsTraveled;
			this.HighestAttackAchieved = user.HighAttack;
			this.HighestDefenseAchieved = user.HighDefense;
		}
	}
}