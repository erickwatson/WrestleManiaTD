using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace WrestleManiaTD
{
    class Enemy
    {
        Sprite sprite = new Sprite();
        // keep a reference to the Game object to check for collisions on the map
        //GameManager game = null;
        Vector2 velocity = Vector2.Zero;
        // Game Canvas is approx 720x480

        List<Coord> path;

        public Vector2 Goal = new Vector2(0,240);

        public Vector2 End = new Vector2(720, 240);
        //float pause = 0;
        //bool moveRight = true;
        //static float enemyAcceleration = GameManager.acceleration / 5.0f;
        //static Vector2 enemyMaxVelocity = GameManager.maxVelocity / 5.0f;

        public Vector2 Position
        {
            get { return sprite.position; }
            set { sprite.position = value; }
        }
        //public Rectangle Bounds
        //{
        //    get { return sprite.Bounds; }
        //}
        public Enemy(int x, int y)
        {
            //this.game = game;
            Position = new Vector2(x, y);
            velocity = Vector2.Zero;

            path = new List<Coord>
                {
                new Coord() { X = 0, Y = 240 },
                new Coord() { X = 110, Y = 240 },
                new Coord() { X = 110, Y = 370 },
                new Coord() { X = 250, Y = 370 },
                new Coord() { X = 250, Y = 120 },
                new Coord() { X = 390, Y = 120 },
                new Coord() { X = 390, Y = 370 },
                new Coord() { X = 530, Y = 370 },
                new Coord() { X = 530, Y = 120 },
                new Coord() { X = 670, Y = 120 },
                new Coord() { X = 670, Y = 240 },
                new Coord() { X = 720, Y = 240 }
                };
        }
        public void Load(ContentManager content)
        {
            AnimatedTexture animation = new AnimatedTexture(Vector2.Zero, 0, 1, 1);
            animation.Load(content, "MachoMan", 10, 6);
            
            sprite.Add(animation, 16, 0);
        }

        public void Update(float deltaTime)
        {
            //(Position - Goal).Normalize

            double temp = Math.Pow(Position.X - Goal.X, 2) + Math.Pow(Position.Y - Goal.Y, 2);
            if (temp < (10))
            {
                Coord old = path[0];
                path.RemoveAt(0);
                path.Add(old);
                Goal = new Vector2(path[0].X, path[0].Y);
            }

            temp = Math.Pow(Position.X - End.X, 2) + Math.Pow(Position.Y - End.Y, 2);
            if (!(temp < (5)))
            {
                Vector2 direction = (Goal - Position);
                direction.Normalize();
                Position += direction;
            }

            sprite.Update(deltaTime);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            sprite.Draw(spriteBatch);
        }
    }
}
