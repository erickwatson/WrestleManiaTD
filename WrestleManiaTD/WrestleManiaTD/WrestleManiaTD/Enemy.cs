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
        GameManager game = null;
        Vector2 velocity = Vector2.Zero;
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
        public Enemy(GameManager game)
        {
            this.game = game;
            velocity = Vector2.Zero;
        }
        public void Load(ContentManager content)
        {
            //AnimatedTexture animation = new AnimatedTexture(Vector2.Zero, 0, 1, 1);
            //animation.Load(content, "enemy", 4, 5);

            //sprite.Add(animation, 16, 0);
        }
    }
}
