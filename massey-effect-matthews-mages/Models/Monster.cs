using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace massey_effect_matthews_mages.Models
{
    public class Monster
    {
        public static int DropChancePercentage = 20;
        public string Name { get; set; }
        public int MaxHealth { get; set; }
        public int Attack { get; set; }
        public Item DropItem { get; set; }
        public int MoveSpeed { get; set; }
        public Position StartPosition { get; set; }

        public string Image { get; set; }
        public int AnimationSpeed { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }

        public int CollisionX { get; set; }
        public int CollisionY { get; set; }
        public int CollisionHeight { get; set; }
        public int CollisionWidth { get; set; }

        public Monster()
        {

        }

        public Monster(int characterLevel, Room room, Random rand)
        {
            Name = "Slime";
            MaxHealth = characterLevel + 2;
            Attack = characterLevel + 7;
            MoveSpeed = rand.Next(95, 220);
            AnimationSpeed = 12;
            DropItem = GetDropItem(rand);

            Width = 155;
            Height = 155;
            CollisionWidth = 188;
            CollisionHeight = 52;
            CollisionX = 62;
            CollisionY = 120;

            //boss monster!
            if (characterLevel > 5 && rand.Next(16) == 3)
            {
                Image = @"../Content/GameContent/Images/slime_sprite_green.png";
                MoveSpeed = 220;
                AnimationSpeed = 14;
                Attack = Attack * 2;
                Name = "Boss";
                DropItem = GetBossDropItem(rand);
            }
            else
            {
                if (MoveSpeed < 145)
                {
                    Image = @"../Content/GameContent/Images/slime_sprite4.png";
                    Attack += (int)(Attack * 1.5);
                    AnimationSpeed -= rand.Next(1, 4);
                }
                if (MoveSpeed >= 145)
                {
                    Image = @"../Content/GameContent/Images/slime_sprite_red.png";
                }
                if (MoveSpeed >= 170)
                {
                    AnimationSpeed += rand.Next(1, 4);
                }
            }
            StartPosition = new Position(rand.Next(room.Width - Width), rand.Next(room.Height - Height));
            while (StartPosition.X < 300 || StartPosition.Y < 300)
            {
                StartPosition.X = rand.Next(room.Width - Width);
                StartPosition.Y = rand.Next(room.Height - Height);
            }
        }

        private Item GetDropItem(Random rand)
        {
            Item item = null;
            if (DropChancePercentage / rand.Next(1, 100) >= 1)
            {
                item = new Item();
                item.Image = @"../Content/GameContent/Images/powerup.png";

                if (rand.Next(2) % 2 == 1)
                {
                    item.Attack = rand.Next(1, 3);
                }
                else
                {
                    item.Defense = rand.Next(1, 3);
                    item.Image = @"../Content/GameContent/Images/powerup_defense.png";
                }

            }
            return item;
        }

        private Item GetBossDropItem(Random rand)
        {
                Item item = new Item();
                item.Image = @"../Content/GameContent/Images/powerup_boss.png";

                if (rand.Next(2) % 2 == 1)
                {
                    item.Attack = rand.Next(2, 4);
                    item.Defense = 1;
                }
                else
                {
                    item.Defense = rand.Next(2, 4);
                    item.Attack = 1;
                }

            return item;
        }
    }
}