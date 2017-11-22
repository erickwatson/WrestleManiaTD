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

        public Vector2 Goal = new Vector2(100,100);

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
        public Enemy()//GameManager game)
        {
            //this.game = game;
            velocity = Vector2.Zero;
        }
        public void Load(ContentManager content)
        {
            AnimatedTexture animation = new AnimatedTexture(Vector2.Zero, 0, 1, 1);
            animation.Load(content, "rock_large", 1, 0);

            sprite.Add(animation, 16, 0);
        }

        public void Update(float deltaTime)
        {
            //(Position - Goal).Normalize

            double temp = Math.Pow(Position.X - End.X, 2) + Math.Pow(Position.Y - End.Y, 2);
            if (!(temp < (10)))
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
