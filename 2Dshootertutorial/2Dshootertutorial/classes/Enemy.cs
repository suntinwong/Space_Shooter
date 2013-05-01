using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace _2Dshootertutorial {
    public class Enemy {

        //Actual player ship stuff
        public Texture2D texture;
        public Vector2 position;
        public float speed; //ship's movement speed
        public int shiptype; //type of enemy ship
        public Rectangle boundingBox;
        public bool isVisible;

        //Bullet stuff
        public Texture2D bulletTexture;
        public float bulletDelay, firerate, bulletvelocity, bulletdamage;
        public List<Bullet> bullets;

        //Other stuff
        public int health,score;
        Random random = new Random();
        public float randX, randY;

        //Constructor
        public Enemy(ContentManager Content,int type,float posx, float posy) {

            //set basic ship parameters
            shiptype = type;
            isVisible = true;
            bullets = new List<Bullet>();
            bulletDelay = 0;
            position = new Vector2(posx, posy);

            //Defualt type 0, medium fighter type
            if (type == 0) {
                texture = Content.Load<Texture2D>("Artwork/enemyship0");
                bulletTexture = Content.Load<Texture2D>("Artwork/enemybullet0");
                health = 50; speed = 2.5f; bulletvelocity = 5f; firerate = 80; bulletdamage = 30; score = 5;
            } 
            
            //Type 1, large fighter type
            else if (type == 1) {
                texture = Content.Load<Texture2D>("Artwork/enemyship1");
                bulletTexture = Content.Load<Texture2D>("Artwork/enemybullet0");
                health = 75; speed = 1.25f; bulletvelocity = 5f; firerate = 120; bulletdamage = 30; score = 10;
            }
        }

       
        //Draw method
        public void Draw(SpriteBatch spritebatch) {

            //Only draw if the ship is visible
            if (isVisible) {

                //Draw the actual ship
                spritebatch.Draw(texture, position, Color.White);

                //Draw all bullets associated with the ship
                for (int i = 0; i < bullets.Count(); i++) 
                    bullets[i].Draw(spritebatch);
            }
        }

        //update method
        public void Update(GameTime gametime) {

            //set bounding box for collision
            boundingBox = new Rectangle((int)position.X, (int)position.Y, texture.Width, texture.Height);
            
            //update movement
            position.Y += speed;
            if (position.Y >= Defualt.Default._H + 100) isVisible = false;

            //only shoot bullets if they are on the screen
            if (position.Y > 0) shoot_bullets();

            //update all bullets
            update_bullets();
        }

        //shoot bullets helper function
        private void shoot_bullets() {

             //shoot only if the bullet delay is set - Determine bullet texture, and bullet position
            if (bulletDelay >= firerate) {

                //Defualt, medium fighter type - one laser from center
                if (shiptype == 0) {
                    Bullet b = new Bullet(bulletTexture,bulletvelocity);
                    b.position = new Vector2(position.X + texture.Width / 2 - b.texture.Width / 2, position.Y + texture.Height / 2 - b.texture.Height / 2);
                    b.isVisible = true;
                    bulletDelay = 0;
                    if (bullets.Count() < 20) bullets.Add(b); //add bullet
                }

                //Large fighter type - three lasers fanned out
                else if (shiptype == 1) {
                    bulletDelay = 0;
                    Bullet b1 = new Bullet(bulletTexture,bulletvelocity);
                    b1.position = new Vector2(position.X + texture.Width / 2 - b1.texture.Width / 2, position.Y + texture.Height / 2 - b1.texture.Height / 2);
                    Bullet b2 = new Bullet(bulletTexture, bulletvelocity);
                    b2.position = new Vector2(position.X - b1.texture.Width / 2, position.Y + texture.Height / 2 - b1.texture.Height / 2);
                    b2.rotation = 45; b2.speedX = -1 * bulletvelocity / 4; 
                    Bullet b3 = new Bullet(bulletTexture, bulletvelocity);
                    b3.position = new Vector2(position.X + texture.Width - b1.texture.Width / 2, position.Y + texture.Height / 2 - b1.texture.Height / 2);
                    b3.rotation = -45; b3.speedX = bulletvelocity / 4; 
                    bullets.Add(b1); //add bullet
                    bullets.Add(b2);
                    bullets.Add(b3);
                }      
            }
        }


        //update bullets help function
        public void update_bullets() {

            //move all bullets that are owned by the ship
            for (int i = 0; i < bullets.Count(); i++) {
                bullets[i].position.Y += bullets[i].speedY; //move bullet
                bullets[i].position.X += bullets[i].speedX;
                bullets[i].boundingBox = new Rectangle((int)bullets[i].position.X, (int)bullets[i].position.Y, bulletTexture.Width, bulletTexture.Height);
                if (bullets[i].position.Y <= 0 || !bullets[i].isVisible) {
                    bullets[i].isVisible = false;
                    bullets.RemoveAt(i); //remove bullet if it is not visible
                }
            }
            bulletDelay++;
        }

    }
}
