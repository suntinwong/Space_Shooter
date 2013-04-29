using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;


namespace _2Dshootertutorial {
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Random random = new Random(); //seed random number
        Player p = new Player(); //make player object
        Starfield sf = new Starfield(); //make starfield background object
        List<Asteroid> asteroids = new List<Asteroid>(); //make asteroid list


        public Game1() {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            //Set some basic stuff for my game
            graphics.IsFullScreen = false; //set it to full screen no
            graphics.PreferredBackBufferWidth = Defualt.Default._W; //set the screen dimension width
            graphics.PreferredBackBufferHeight = Defualt.Default._H; //set the screen dimension height
            this.Window.Title = "MySpaceShooter"; //set window title
        }

        
        //init funciton
        protected override void Initialize() {
            // TODO: Add your initialization logic here

            base.Initialize();
        }

       
        //load content
        protected override void LoadContent() {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            p.LoadContent(Content); //load the player
            sf.LoadContent(Content); //load the starfield

        }


       
        //unload
        protected override void UnloadContent() {
            // TODO: Unload any non ContentManager content here
        }


       //update
        protected override void Update(GameTime gameTime) {
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();
            
            sf.Update(gameTime); //update starfield
            p.Update(gameTime); //update player
            UpdateAsteroids(gameTime); //update asteroids
            UpdateCollisions();
            base.Update(gameTime);
        }

       //draw
        protected override void Draw(GameTime gameTime) {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            //Do all the drawings
            spriteBatch.Begin();
            sf.Draw(spriteBatch); //draw starfield first
            foreach (Asteroid a in asteroids) a.Draw(spriteBatch); //draw asteroids
            p.Draw(spriteBatch); //draw player
            spriteBatch.End();

            base.Draw(gameTime);
        }

        //load astroids helper function
        private void UpdateAsteroids(GameTime gameTime) {
            int randX = random.Next(0, Defualt.Default._W);
            int randY = random.Next(-1 * Defualt.Default._H, -50);

            //Add more asteroids if needed
            if (asteroids.Count() < Defualt.Default.AsteroidMax)
                asteroids.Add(new Asteroid(Content.Load<Texture2D>("asteroid"), new Vector2(randX, randY)));

            //Remove or update all asteroids
            for (int i = 0; i < asteroids.Count(); i++) {
                asteroids[i].Update(gameTime); //update all asteroids
                if (!asteroids[i].isVisible) asteroids.RemoveAt(i); //Remove astroids if not visible
            }

         
        }

        //Checks collisions
        private void UpdateCollisions() {

            //Check to see if player or bullets collides with asteroids
            for (int i = 0; i < asteroids.Count(); i++) {

                if (p.boundingBox.Intersects(asteroids[i].boundingBox)) 
                    asteroids[i].isVisible = false;

                for (int j = 0; j < p.bullets.Count(); j++) {
                    if (p.bullets[j].boundingBox.Intersects(asteroids[i].boundingBox)) {
                        p.bullets[j].isVisible = false;
                        asteroids[i].isVisible = false;
                    }
                }
            }
        }

    }
}
