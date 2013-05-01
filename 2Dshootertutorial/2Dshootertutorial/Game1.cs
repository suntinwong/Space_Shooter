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
        List<Explosion> explosions = new List<Explosion>(); //make explosion list
        HUD hud = new HUD(); //make hud
        SoundManager sm = new SoundManager(); //sound manager 
       

        int state = 1;

        public Game1() {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            //Set some basic stuff for my game
            graphics.IsFullScreen = false; //set it to full screen no
            graphics.PreferredBackBufferWidth = Defualt.Default._W + 200; //set the screen dimension width
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
            hud.LoadContent(Content); //Load hud
            sm.LoadContent(Content); //load sound manager
            MediaPlayer.Play(sm.bgm1); //play background music
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
                if(p.isVisible) p.Update(gameTime); //update player
                UpdateAsteroids(gameTime); //update asteroids
                UpdateExplosions(gameTime); //update explosions
                AsteroidCollisions(); //do collision logic with asteroids
                hud.Update(p.score, p.health); //update the hud
                base.Update(gameTime);
            
        }

       //draw
        protected override void Draw(GameTime gameTime) {
            GraphicsDevice.Clear(Color.Black);

            //Do all the drawings
            spriteBatch.Begin();
            sf.Draw(spriteBatch); //draw starfield first
            foreach (Asteroid a in asteroids) a.Draw(spriteBatch); //draw asteroids
            p.Draw(spriteBatch); //draw player
            foreach (Explosion e in explosions) e.Draw(spriteBatch); //draw explosions
            hud.Draw(spriteBatch); //draw hud
            spriteBatch.End();

            base.Draw(gameTime);
        }

        //update astroids helper function
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

        //update explisions helper function
        private void UpdateExplosions(GameTime gametime) {
            for (int i = 0; i < explosions.Count(); i++) {
                if (!explosions[i].isVisible) explosions.RemoveAt(i);
                else explosions[i].Update(gametime);
            }
        }


        //Checks collisions with asteroids, and act appropaitly
        private void AsteroidCollisions() {

            //Check collisions with asteroids and other objects
            for (int i = 0; i < asteroids.Count(); i++) {

                if (p.boundingBox.Intersects(asteroids[i].boundingBox)) { //collision of asteroid & player
                    explosions.Add(new Explosion(Content.Load<Texture2D>("explosion3"), asteroids[i].position,20f,1f));
                    asteroids[i].isVisible = false;
                    p.health -= 35;
                    if (p.health < 1) GameOverState();
                    sm.explodeSound.Play();
                    
                }

                for (int j = 0; j < p.bullets.Count(); j++) { 
                    if (p.bullets[j].boundingBox.Intersects(asteroids[i].boundingBox)) { //collision of asteroid & bullet
                        explosions.Add(new Explosion(Content.Load<Texture2D>("explosion3"), asteroids[i].position,20f,1f));
                        p.bullets[j].isVisible = false;
                        asteroids[i].isVisible = false;
                        p.score++;
                        sm.explodeSound.Play();
                    }
                }
            }
        }

        //Player has died, game over state
        private void GameOverState() {

            explosions.Add(new Explosion(Content.Load<Texture2D>("explosion3"), p.position, 40f, 3f));
            MediaPlayer.Stop();
            p.kill_player();
            
            //Exit();

        }

    }
}
