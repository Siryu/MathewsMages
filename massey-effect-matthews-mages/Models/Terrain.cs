using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace massey_effect_matthews_mages.Models
{
    public class Terrain
    {
        public string Image { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public int CollisionX { get; set; }
        public int CollisionY { get; set; }
        public int CollisionHeight { get; set; }
        public int CollisionWidth { get; set; }

        public Terrain()
        {

        }

        public Terrain(int xPosition, int yPosition, int width, int height, int collisionX, int collisionY, int collisionWidth, int collisionHeight, string image)
        {
            this.X = xPosition;
            this.Y = yPosition;
            this.Width = width;
            this.Height = height;
            this.CollisionX = collisionX;
            this.CollisionY = collisionY;
            this.CollisionWidth = collisionWidth;
            this.CollisionHeight = collisionHeight;
            this.Image = image;
        }
    }
}