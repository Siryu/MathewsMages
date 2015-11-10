using massey_effect_matthews_mages.DataAL;
using massey_effect_matthews_mages.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace massey_effect_matthews_mages.Controllers
{
    [Authorize]
    public class GameController : Controller
    {
        static IDAL dal = new DBDataAL();

        public ActionResult GenerateRoom()
        {
            Room[] possibleRooms = new Room[2];

            #region forestRoom

            List<Terrain> terrainObjects = new List<Terrain>();
            terrainObjects.Add(new Terrain(586, 626, 709, 801, 319, 752, 77, 49, @"../Content/GameContent/Images/tree01.png"));
            terrainObjects.Add(new Terrain(2268, 1868, 709, 801, 319, 752, 77, 49, @"../Content/GameContent/Images/tree01.png"));
            terrainObjects.Add(new Terrain(1884, 216, 709, 801, 319, 752, 77, 49, @"../Content/GameContent/Images/tree01.png"));
            //terrainObjects.Add(new Terrain(2268, 1868, 903, 597, @"/content/GameContent/Images/trees02.png"));
            //terrainObjects.Add(new Terrain(1884, 216, 709, 801, @"/content/GameContent/Images/trees01.png"));
            terrainObjects.Add(new Terrain(608, 2096, 406, 332, 0, 192, 406, 140, @"../Content/GameContent/Images/rock01.png"));

            Room forestRoom = new Room(3200, 3200, @"/content/GameContent/Images/map3_v3.jpg", terrainObjects);
            possibleRooms[0] = forestRoom;

            #endregion

            #region forestRoomVariation
            List<Terrain> terrainObjects1 = new List<Terrain>();
            //terrainObjects1.Add(new Terrain(586, 626, 709, 801, 319, 752, 77, 49, @"../Content/GameContent/Images/tree01.png"));
            //terrainObjects1.Add(new Terrain(2268, 1868, 709, 801, 319, 752, 77, 49, @"../Content/GameContent/Images/tree01.png"));
            //terrainObjects1.Add(new Terrain(1884, 216, 709, 801, 319, 752, 77, 49, @"../Content/GameContent/Images/tree01.png"));
            //terrainObjects1.Add(new Terrain(2000, 400, 709, 801, 319, 752, 77, 49, @"../Content/GameContent/Images/tree01.png"));
            //terrainObjects1.Add(new Terrain(2798, 2790, 709, 801, 319, 752, 77, 49, @"../Content/GameContent/Images/tree01.png"));
            //terrainObjects1.Add(new Terrain(608, 2096, 406, 332, 0, 192, 406, 140, @"../Content/GameContent/Images/rock01.png"));
            //terrainObjects1.Add(new Terrain(900, 2296, 406, 332, 0, 192, 406, 140, @"../Content/GameContent/Images/rock01.png"));

            ////Room forestRoom2 = new Room(3200, 3200, @"/content/GameContent/Images/map3_v3.jpg", terrainObjects1);
            //List<Terrain> terrainObjects2 = new List<Terrain>();
            //terrainObjects2.Add(new Terrain(2200, 0, 13605, 1600, 2200, 0, 13605, 1600, @"../Content/GameContent/Images/fireball.png"));
            ////terrainObjects2.Add(new Terrain(5439, 2054, 934, 746, 349, 694, 268, 53, @"../Content/GameContent/Images/red_tree.gif"));
            //terrainObjects2.Add(new Terrain(5439, 2054, 934, 746, 0, 0, 934, 746, @"../Content/GameContent/Images/red_tree.gif"));
            //terrainObjects2.Add(new Terrain(5639, 2054, 934, 746, 0, 0, 934, 746, @"../Content/GameContent/Images/red_tree.gif"));
            //terrainObjects2.Add(new Terrain(5839, 2054, 934, 746, 0, 0, 934, 746, @"../Content/GameContent/Images/red_tree.gif"));
            //terrainObjects2.Add(new Terrain(6039, 2054, 934, 746, 0, 0, 934, 746, @"../Content/GameContent/Images/red_tree.gif"));
            //terrainObjects2.Add(new Terrain(6239, 2054, 934, 746, 0, 0, 934, 746, @"../Content/GameContent/Images/red_tree.gif"));
            //terrainObjects2.Add(new Terrain(6439, 2054, 934, 746, 0, 0, 934, 746, @"../Content/GameContent/Images/red_tree.gif"));
            //terrainObjects2.Add(new Terrain(6639, 2054, 934, 746, 0, 0, 934, 746, @"../Content/GameContent/Images/red_tree.gif"));
            //terrainObjects2.Add(new Terrain(6839, 2054, 934, 746, 0, 0, 934, 746, @"../Content/GameContent/Images/red_tree.gif"));
            //terrainObjects2.Add(new Terrain(7039, 2054, 934, 746, 0, 0, 934, 746, @"../Content/GameContent/Images/red_tree.gif"));
            //terrainObjects2.Add(new Terrain(7239, 2054, 934, 746, 0, 0, 934, 746, @"../Content/GameContent/Images/red_tree.gif"));
            //terrainObjects2.Add(new Terrain(7439, 2054, 934, 746, 0, 0, 934, 746, @"../Content/GameContent/Images/red_tree.gif"));
            //terrainObjects2.Add(new Terrain(7639, 2054, 934, 746, 0, 0, 934, 746, @"../Content/GameContent/Images/red_tree.gif"));
            //terrainObjects2.Add(new Terrain(7839, 2054, 934, 746, 0, 0, 934, 746, @"../Content/GameContent/Images/red_tree.gif"));
            //terrainObjects2.Add(new Terrain(8039, 2054, 934, 746, 0, 0, 934, 746, @"../Content/GameContent/Images/red_tree.gif"));
            //terrainObjects2.Add(new Terrain(8239, 2054, 934, 746, 0, 0, 934, 746, @"../Content/GameContent/Images/red_tree.gif"));
            //terrainObjects2.Add(new Terrain(8439, 2054, 934, 746, 0, 0, 934, 746, @"../Content/GameContent/Images/red_tree.gif"));
            //terrainObjects2.Add(new Terrain(8639, 2054, 934, 746, 0, 0, 934, 746, @"../Content/GameContent/Images/red_tree.gif"));
            //terrainObjects2.Add(new Terrain(8839, 2054, 934, 746, 0, 0, 934, 746, @"../Content/GameContent/Images/red_tree.gif"));
            //terrainObjects2.Add(new Terrain(8839, 2054, 934, 746, 0, 0, 934, 746, @"../Content/GameContent/Images/red_tree.gif"));
            //terrainObjects2.Add(new Terrain(8839, 2054, 934, 746, 0, 0, 934, 746, @"../Content/GameContent/Images/red_tree.gif"));
            //terrainObjects2.Add(new Terrain(8839, 2054, 934, 746, 0, 0, 934, 746, @"../Content/GameContent/Images/red_tree.gif"));
            //terrainObjects2.Add(new Terrain(8839, 2054, 934, 746, 0, 0, 934, 746, @"../Content/GameContent/Images/red_tree.gif"));
            //terrainObjects2.Add(new Terrain(8839, 2054, 934, 746, 0, 0, 934, 746, @"../Content/GameContent/Images/red_tree.gif"));

            //terrainObjects2.Add(new Terrain(9039, 2454, 934, 746, 0, 0, 934, 746, @"../Content/GameContent/Images/red_tree.gif"));
            //terrainObjects2.Add(new Terrain(9439, 2854, 934, 746, 0, 0, 934, 746, @"../Content/GameContent/Images/red_tree.gif"));
            //terrainObjects2.Add(new Terrain(9839, 3254, 934, 746, 0, 0, 934, 746, @"../Content/GameContent/Images/red_tree.gif"));
            //terrainObjects2.Add(new Terrain(12239, 3654, 934, 746, 0, 0, 934, 746, @"../Content/GameContent/Images/red_tree.gif"));
            //terrainObjects2.Add(new Terrain(16639, 4054, 934, 746, 0, 0, 934, 746, @"../Content/GameContent/Images/red_tree.gif"));
            //terrainObjects2.Add(new Terrain(20039, 4454, 934, 746, 0, 0, 934, 746, @"../Content/GameContent/Images/red_tree.gif"));
            //terrainObjects2.Add(new Terrain(24439, 4854, 934, 746, 0, 0, 934, 746, @"../Content/GameContent/Images/red_tree.gif"));
            //terrainObjects2.Add(new Terrain(28039, 5254, 934, 746, 0, 0, 934, 746, @"../Content/GameContent/Images/red_tree.gif"));
            //terrainObjects2.Add(new Terrain(32039, 5654, 934, 746, 0, 0, 934, 746, @"../Content/GameContent/Images/red_tree.gif"));
            #endregion

            #region alt forest room

            //mini room
            List<Terrain> terrainObjects2 = new List<Terrain>();

            terrainObjects2.Add(new Terrain(484, 179, 934, 746, 349, 694, 268, 53, @"../Content/GameContent/Images/green_tree.png"));
            terrainObjects2.Add(new Terrain(1288, 788, 934, 746, 349, 694, 268, 53, @"../Content/GameContent/Images/green_tree.png"));
            terrainObjects2.Add(new Terrain(2228, 44, 934, 746, 349, 694, 268, 53, @"../Content/GameContent/Images/green_tree.png"));
            terrainObjects2.Add(new Terrain(200, 1920, 934, 746, 349, 694, 268, 53, @"../Content/GameContent/Images/red_tree.png"));
            terrainObjects2.Add(new Terrain(948, 2284, 934, 746, 349, 694, 268, 53, @"../Content/GameContent/Images/red_tree.png"));

            terrainObjects2.Add(new Terrain(584, 879, 407, 320, 0, 175, 407, 131, @"../Content/GameContent/Images/wayne_rock.png"));
            terrainObjects2.Add(new Terrain(2088, 2116, 407, 320, 0, 175, 407, 131, @"../Content/GameContent/Images/wayne_rock.png"));
            terrainObjects2.Add(new Terrain(2420, 1920, 407, 320, 0, 175, 407, 131, @"../Content/GameContent/Images/wayne_rock.png"));
            terrainObjects2.Add(new Terrain(2548, 2860, 407, 320, 0, 175, 407, 131, @"../Content/GameContent/Images/wayne_rock.png"));

            Room forestRoom2 = new Room(3200, 3200, @"/content/GameContent/Images/map4_v2.jpg", terrainObjects2);
            possibleRooms[1] = forestRoom2;

            #endregion

            User u = dal.GetUser(User.Identity.Name);
            u.CurrentHealth = 100;
            dal.UpdateUser(u);

            Random rand = new Random();
            Room winner = possibleRooms[rand.Next(possibleRooms.Length)];
            winner.monsters = GenerateMonsters(winner);
            return Json(winner, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetUser()
        {
            User u = dal.GetUser(User.Identity.Name);
            string output = JsonConvert.SerializeObject(u);
            return Json(u, JsonRequestBehavior.AllowGet);
        }

        private List<Monster> GenerateMonsters(Room room2)
        {
            User user = dal.GetUser(User.Identity.Name);

            Random rand = new Random();

            int maxMonsters = rand.Next((user.GetComputedLevel() * 2) - (user.GetComputedLevel() / 2), (user.GetComputedLevel() * 2));

            if (maxMonsters <= 5)
            {
                maxMonsters = 6;
            }

            Monster[] monsters = new Monster[maxMonsters];

            for (int i = 0; i < maxMonsters; i++)
            {
                monsters[i] = new Monster(user.GetComputedLevel(), room2, rand);
            }

            return monsters.ToList();
        }

        public void UpdateRoomTraveledAndMonstersKilled(List<string> list)
        {
            User user = dal.GetUser(User.Identity.Name);
            user.MonstersKilled += int.Parse(list[0]);
            user.RoomsTraveled += int.Parse(list[1]);
            UpdateHighScores(user);
            dal.UpdateUser(user);
        }

        public void UpdateUser(List<string> list)
        {
            User user = dal.GetUser(User.Identity.Name);
            user.Attack += int.Parse(list[0]);
            user.Defense += int.Parse(list[1]);
            //user.MonstersKilled += int.Parse(list[2]);
            UpdateHighScores(user);
            dal.UpdateUser(user);
        }

        //list[0] is true/false to reset user stats, list[1] is number of monsters killed, list[2] is if user is going through portal
        public void ResetPlayerStats(List<string> list)
        {
            User user = dal.GetUser(User.Identity.Name);
            user.MonstersKilled = int.Parse(list[1]);

            UpdateHighScores(user);
            if (list[0].Equals("true"))
            {
                user.Attack = 1;
                user.Defense = 1;
                user.MonstersKilled = 0;
                user.RoomsTraveled = 0;
            }

            dal.UpdateUser(user);
        }

        private void UpdateHighScores(User user)
        {
            if (user.Attack > user.HighAttack)
            {
                user.HighAttack = user.Attack;
            }
            if (user.Defense > user.HighDefense)
            {
                user.HighDefense = user.Defense;
            }
            if (user.RoomsTraveled > user.HighRoomsTraveled)
            {
                user.HighRoomsTraveled = user.RoomsTraveled;
            }
            if (user.MonstersKilled > user.HighMonstersKilled)
            {
                user.HighMonstersKilled = user.MonstersKilled;
            }
        }

        //[Authorize]
        public ActionResult TestGame()
        {
            return View("Game");
        }
    }
}