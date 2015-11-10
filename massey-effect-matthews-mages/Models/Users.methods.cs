using massey_effect_matthews_mages.DataAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace massey_effect_matthews_mages.Models
{
    public partial class User
    {
        public int Level { get; set; }

        static IDAL dal = new DBDataAL();

        public User()
        {
            Level = GetComputedLevel();
        }

        public int GetComputedLevel()
        {
            int charLevel = (MonstersKilled / 4) + (RoomsTraveled / 2);            
            return charLevel;
        }
    }
}