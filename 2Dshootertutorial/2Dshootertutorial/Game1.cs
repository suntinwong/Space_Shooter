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

        //Create objects
        Player p = new Player(); //make player object
        Starfield sf = new Starfield(); //make starfield background object
        List<Asteroid> asteroids = new List<Asteroid>(); //make asteroid list
        List<Explosion> explosions = new List<Explosion>(); //make explosion list
        List<Enemy> enemies = new List<Enemy>(); // make the enemy list
        HUD hud = new HUD(); //make hud
        SoundManager sm = new SoundManager(); //sound manager 
       
        //Game varibles
        int gamestate = 1;
        bool gameoverflag = false;

        public Game1() {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            //Set some basic stuff for my game
            graphics.IsFullScreen = false; //set it to full screen no
            graphics.PreferredBackBufferWidth = Defualt.Default._W + 175; //set the screen dimension width
            graphics.PreferredBackBufferHeight = Defualt.Default._H; //set the screen dimension height
            this.Window.Title = "MySpaceShooter"; //set window title
        }

        
        //init funciton
        protected override void Initialize() {
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

            //When in playing game state
            if (gamestate == 1) {
                sf.Update(gameTime);                            //update starfield
                if(p.isVisible) p.Update(gameTime);             //update player
                UpdateAsteroids(gameTime);                      //update asteroids
                UpdateExplosions(gameTime);                     //update explosions
                UpdateEnemies(gameTime);                        //update enemies
                UpdateCollisions(gameTime);                     //update all collisions
                hud.Update(p.score, p.health);                  //update the hud
            }

            //Game wait between game state and gameover state
            if (gameoverflag) if (gameTime.TotalGameTime.Seconds % 8 == 0) gamestate = 2;

            base.Update(gameTime);
        }

       //Draw Method : do all the drawings to the screen
        protected override void Draw(GameTime gameTime) {
            GraphicsDevice.Clear(Color.Black);
            spriteBatch.Begin();

            //When in playing game state
            if (gamestate == 1) { 
                sf.Draw(spriteBatch);                                   //ALWAYS draw starfield first
                foreach (Asteroid a in asteroids) a.Draw(spriteBatch);  //draw asteroids
                foreach (Enemy e in enemies) e.Draw(spriteBatch);       //draw enemies
                p.Draw(spriteBatch);                                    //draw player
                foreach (Explosion e in explosions) e.Draw(spriteBatch); //draw explosions
                hud.Draw(spriteBatch);                                  //draw hud
            }

           //Endgame state
            else if (gamestate == 2) { 
                hud.Draw_gameOver(spriteBatch, p.score);        //draw gameover screen
            }


            spriteBatch.End();
            base.Draw(gameTime);
        }

        //Update astroids helper function
        private void UpdateAsteroids(GameTime gameTime) {
            int randX = random.Next(0, Defualt.Default._W);
            int randY = random.Next(-1 * Defualt.Default._H, -50);

            //Add more asteroids if needed
            if (asteroids.Count() < Defualt.Default.AsteroidMax)
                asteroids.Add(new Asteroid(Content, random.Next(0, Defualt.Default._W - 25), random.Next(Defualt.Default._H * -2, -50)));

            //Remove or update all asteroids
            for (int i = 0; i < asteroids.Count(); i++) {
                if (!asteroids[i].isVisible) asteroids.RemoveAt(i); //Remove astroids if not visible
                else asteroids[i].Update(gameTime); //update all asteroids
            }
        }

        //Update collisions helper function  - Handle all collisions
        private void UpdateCollisions(GameTime gameTime){
            
            //Do all player lazer collisions
            for (int i = 0; i < p.bullets.Count(); i++) {

                //Check asteroids
                for (int j = 0; j < asteroids.Count(); j++) {
                    if(p.bullets[i].boundingBox.Intersects(asteroids[j].boundingBox)){
                        asteroids[j].health -= p.laserDamage;
                        p.bullets[i].isVisible = false;
                        if(asteroids[j].health <= 0){ //when its killed
                            explosions.Add(new Explosion(Content, asteroids[j].position,20f,1f));
                            sm.explodeSound.Play();
                            asteroids[j].isVisible = false;
                            p.score += 1;
                        } 
                        else explosions.Add(new Explosion(Content, p.bullets[i].position, 20f, .33f));
                    }
                }

                //Check enemy ships
                for(int j = 0; j < enemies.Count(); j++){
                    if(p.bullets[i].boundingBox.Intersects(enemies[j].boundingBox)){
                        enemies[j].health -= p.laserDamage;
                        p.bullets[i].isVisible = false;
                        if (enemies[j].health <= 0) { //when its killed
                            explosions.Add(new Explosion(Content, new Vector2( enemies[j].position.X +  enemies[j].texture.Width/2, enemies[j].position.Y +  enemies[j].texture.Height/2) , 20f, 1f));
                            sm.explodeSound.Play();
                            enemies[j].isVisible = false;
                            p.score += enemies[j].score;
                        } 
                        else explosions.Add(new Explosion(Content, p.bullets[i].position, 20f, .33f));
                    }
                }
            }

            //Check if player collides with enemy lasers or enemy ship
            for (int i = 0; i < enemies.Count(); i++) {
                for (int j = 0; j < enemies[i].bullets.Count(); j++) { //check lasers
                    if (p.boundingBox.Intersects(enemies[i].bullets[j].boundingBox)) {
                        enemies[i].bullets[j].isVisible = false;
                        p.health -= (int)enemies[i].bulletdamage;
                        explosions.Add(new Explosion(Content, enemies[i].bullets[j].position, 20f, .33f));
                        sm.explodeSound.Play();
                    }
                }
                if (p.boundingBox.Intersects(enemies[i].boundingBox)) {  //check actual ship
                    if (p.health + 100 > enemies[i].health) { 
                        enemies[i].isVisible = false;
                        explosions.Add(new Explosion(Content, enemies[i].position, 20f, 1f));
                        sm.explodeSound.Play();
                    } 
                    p.health -= enemies[i].health;
                }
            }

            //check if player collides with asteroids
            for (int i = 0; i < asteroids.Count(); i++) {
                if (p.boundingBox.Intersects(asteroids[i].boundingBox)) {
                    p.health -= asteroids[i].damage;
                    asteroids[i].isVisible = false;
                    explosions.Add(new Explosion(Content, asteroids[i].position, 20f, 1f));
                    sm.explodeSound.Play();
                }
            }

           if (p.health <= 0 && !gameoverflag) GameOverState();

        }

        //update enemies helper funciton
        private void UpdateEnemies(GameTime gameTime) {

            //Add more enemies if needed
            if (enemies.Count() < Defualt.Default.EnemyMax) {
               
                enemies.Add(new Enemy(Content,RandomShipType(), random.Next(0, Defualt.Default._W-30), random.Next(-2 * Defualt.Default._H, -250)));
            }

            //Remove or update all enemies
            for (int i = 0; i < enemies.Count(); i++) {
                if (enemies[i].position.Y >= Defualt.Default._H + 400 ) enemies.RemoveAt(i); //Remove astroids if not visible
                else enemies[i].Update(gameTime); //update all asteroids
            }

        }

        //update explisions helper function
        private void UpdateExplosions(GameTime gametime) {
            for (int i = 0; i < explosions.Count(); i++) {
                if (!explosions[i].isVisible) explosions.RemoveAt(i);
                else explosions[i].Update(gametime);
            }
        }

        //Player has died, pre-gameover state
        private void GameOverState() {

            explosions.Add(new Explosion(Content, p.position, 40f, 3f));
            MediaPlayer.Stop();
            p.kill_player();
            gameoverflag = true;

        }

        //get a random shiptype number
        private int RandomShipType() {
            int shiptype = 0;
            int rand = random.Next(0, 100);
            if (rand > 80) shiptype = 1;
            return shiptype;
        }
    }
}
