using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace _2Dshootertutorial {

    //Main
    public class Player {

        //Actual player ship stuff
        public Texture2D texture;
        public Vector2 position;
        public int speed; //ship's movement speed
        public Rectangle boundingBox;
        public bool isVisible;

        //Bullet stuff
        public Texture2D bulletTexture;
        public float bulletDelay,bulletspeed,firerate;
        public List<Bullet> bullets;

        //other
        public int health,score,laserDamage;
        SoundManager sm;

        //Defualt Constructor
        public Player(){

            //set important properties
            health = 100;           //player's hitpoints
            speed = 5;              //Move speed
            laserDamage = 25;       //per laser hit damage
            bulletspeed = 15f;      //speed of the laser
            firerate = 10f;          //fire rate of lasers

            //set other stuff
            texture = null;
            position = new Vector2(300, 600);
            isVisible = true;
            bullets = new List<Bullet>();
            bulletDelay = firerate;
            score = 0;
            sm = new SoundManager();
        }

        //load content
        public void LoadContent(ContentManager Content){
            texture = Content.Load<Texture2D>("Artwork/player");
            bulletTexture = Content.Load<Texture2D>("Artwork/playerbullet");
            sm.LoadContent(Content);
        }

        //draw funciton
        public void Draw(SpriteBatch spriteBatch){
            if(isVisible) spriteBatch.Draw(texture, position, Color.White); //draw players
            foreach (Bullet i in bullets) i.Draw(spriteBatch); //draw bullets
        }

        //update function
        public void Update(GameTime gameTime) {

            //update boundingBox
            boundingBox = new Rectangle((int)position.X, (int)position.Y, texture.Width-10, texture.Height-16);

            bool moveLeft, moveRight, moveUp, moveDown,firebullets;

            //get the keyboard state input
            if (!Defualt.Default.UsingKinect) {
                KeyboardState keystate = Keyboard.GetState();
                if (keystate.IsKeyDown(Keys.W)) moveUp = true; else moveUp = false;
                if (keystate.IsKeyDown(Keys.A)) moveLeft = true; else moveLeft = false;
                if (keystate.IsKeyDown(Keys.S)) moveDown = true; else moveDown = false;
                if (keystate.IsKeyDown(Keys.D)) moveRight = true; else moveRight = false;
                if (keystate.IsKeyDown(Keys.Space)) firebullets = true; else firebullets = false;
            }
            
            //Getting kinect input
            else {
                moveUp = false;
                moveLeft = false;
                moveRight = false;
                moveDown = false;
                firebullets = false;
            }

            //Move ship if applicable
            if (moveUp) position.Y -= speed;
            if (moveLeft) position.X -= speed;
            if (moveDown) position.Y += speed;
            if (moveRight) position.X += speed;
            if (firebullets) shoot_bullet();

            //keep player in playing screen if applicable
            if (position.X <= 0) position.X = 0;
            if (position.X >= Defualt.Default._W - texture.Width) position.X = Defualt.Default._W - texture.Width;
            if (position.Y <= 0) position.Y = 0;
            if (position.Y >= Defualt.Default._H - texture.Height) position.Y = Defualt.Default._H - texture.Height;
           
            //update bullet
            update_bullets();

        }

        //Shoot method, used to set starting position of our bullets
        public void shoot_bullet() {

            //shoot only if the bullet delay is set. particle originates in center of player
            if (bulletDelay >= firerate) {
                Bullet b = new Bullet(bulletTexture,bulletspeed);
                b.position = new Vector2(position.X + texture.Width / 2 - b.texture.Width / 2, position.Y + texture.Height / 2 - b.texture.Height / 2);
                b.isVisible = true;
                if (bullets.Count() < 20) bullets.Add(b);
                bulletDelay = 0;
                sm.playerShootSound.Play();
            }
        }

        //update bullets
        public void update_bullets() {

             for(int i = 0; i < bullets.Count(); i++) {
                bullets[i].position.Y -= bullets[i].speedY; //move bullet
                bullets[i].boundingBox = new Rectangle((int)bullets[i].position.X, (int)bullets[i].position.Y, bulletTexture.Width, bulletTexture.Height);
                if (bullets[i].position.Y <= 0 || !bullets[i].isVisible) {
                    bullets[i].isVisible = false;
                    bullets.RemoveAt(i); //remove bullet if it is not visible
                }

            }
             bulletDelay++;
        }

        //Destroy the player ship
        public void kill_player() {
            boundingBox = new Rectangle(-1000, 0, 0, 0);
            isVisible = false;
            bullets.RemoveRange(0, bullets.Count());
        }

    }
}
