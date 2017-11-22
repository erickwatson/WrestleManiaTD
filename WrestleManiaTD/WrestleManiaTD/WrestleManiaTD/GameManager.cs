using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended;
using MonoGame.Extended.Maps.Tiled;
using MonoGame.Extended.ViewportAdapters;using System;using System.Collections.Generic;

namespace WrestleManiaTD
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class GameManager : Game
    {

        public static int tile = 64;
        // abitrary choice for 1m (1 tile = 1 meter)
        public static float meter = tile;
        // very exaggerated gravity (6x)
        public static float gravity = meter * 10f * 10f;
        // max vertical speed (10 tiles/sec horizontal, 15 tiles/sec vertical)
        public static Vector2 maxVelocity = new Vector2(meter * 10, meter * 15);
        // horizontal acceleration - take 1/2 second to reach max velocity
        public static float acceleration = maxVelocity.X * 2;
        // horizontal friction - take 1/6 second to stop from max velocity
        public static float friction = maxVelocity.X * 6;

        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        Player player = new Player();

        Enemy enemy = new Enemy();

        Camera2D camera = null;
        TiledMap map = null;
        TiledTileLayer collisionLayer;
        List<Coord> path;



        public int ScreenWidth
        {
            get
            {
                return graphics.GraphicsDevice.Viewport.Width;
            }
        }

        public int ScreenHeight
        {
            get
            {
                return graphics.GraphicsDevice.Viewport.Height;
            }
        }

        public GameManager()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
                        
            player.Load(Content);
            enemy.Load(Content);
            // Game Canvas is approx 720x480
            path = new List<Coord>
                {
                new Coord() { X = 100, Y = 100 },
                new Coord() { X = 0, Y = 440 },
                new Coord() { X = 20, Y = 400 },
                new Coord() { X = 720, Y = 240 }
                };

            var viewportAdapter = new BoxingViewportAdapter(Window, GraphicsDevice,
                ScreenWidth, ScreenHeight);

            camera = new Camera2D(viewportAdapter);
            camera.Position = new Vector2(0, ScreenHeight);

            map = Content.Load<TiledMap>("Map");
            foreach (TiledTileLayer layer in map.TileLayers)
            {
                if (layer.Name == "Floor")
                    collisionLayer = layer;
            }

        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();


            // TODO: Add your update logic here
            float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
            player.Update(deltaTime);
            enemy.Update(deltaTime);

            Vector2 pos = new Vector2((int)enemy.Position.X, (int)enemy.Position.Y);
            Vector2 goal = new Vector2((int)enemy.Goal.X, (int)enemy.Goal.Y);

            // going to need to put this in a class
            // https://stackoverflow.com/questions/38402480/c-sharp-declare-a-static-list


            double temp = Math.Pow(enemy.Position.X - enemy.Goal.X, 2) + Math.Pow(enemy.Position.Y - enemy.Goal.Y, 2);
            if (temp < (10))
            {
                Coord old = path[0];
                path.RemoveAt(0);
                path.Add(old);
                enemy.Goal = new Vector2(path[0].X, path[0].Y);
            }
            if (pos == goal)
            {
                //if (enemy.Goal == new Vector2(100, 100))
                //{
                //    enemy.Goal = new Vector2(0, 0);
                //}else if (enemy.Goal == new Vector2(0, 0))
                //{
                //    enemy.Goal = new Vector2(200, 200);
                //}else if (enemy.Goal == new Vector2(100, 0))
                //{
                //    enemy.Goal = new Vector2(100, 100);
                //}
                
                //if  (enemy.Goal == new Vector2(path[0].X, path[0].Y))
                //{
                //    path.RemoveAt(0);
                //    enemy.Goal = new Vector2(path[0].X, path[0].Y);
                //}
                //else
                //{
                //    enemy.Goal = new Vector2(0, 0);
                //}


            }


            //Distance between two points, figure out
            //(Math.Pow(x1-x2,2)+Math.Pow(y1-y2,2)) < (d*d);
            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            var transformMatrix = camera.GetViewMatrix();

            //spriteBatch.Begin(transformMatrix: transformMatrix);
            spriteBatch.Begin();
            map.Draw(spriteBatch);

            player.Draw(spriteBatch);
            enemy.Draw(spriteBatch);

                      

            spriteBatch.End();

            base.Draw(gameTime);
        }

        public int PixelToTile(float pixelCoord)
        {
            return (int)Math.Floor(pixelCoord / tile);
        }

        public int TileToPixel(int tileCoord)
        {
            return tileCoord * tileCoord;
        }

        public int CellAtPixelCoord(Vector2 pixelCoords)
        {
            if (pixelCoords.X < 0 ||
                pixelCoords.X > map.WidthInPixels || pixelCoords.Y < 0)
                return 1;

            if (pixelCoords.Y > map.HeightInPixels)
                return 0;
            return CellAtTileCoord(PixelToTile(pixelCoords.X), PixelToTile(pixelCoords.Y));
        }

        public int CellAtTileCoord(int tx, int ty)
        {
            if (tx < 0 || tx >= map.Width || ty < 0)
                return 1;

            if (ty >= map.Height)
                return 0;

            TiledTile tile = collisionLayer.GetTile(tx, ty);
            return tile.Id;
        }
    }
}
