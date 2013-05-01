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
        public int speed; //ship's movement speed
        public int shiptype; //type of enemy ship
        public Rectangle boundingBox;
        public bool isVisible;

        //Bullet stuff
        public Texture2D bulletTexture;
        public float bulletDelay,shootspeed,bulletspeed;
        public List<Bullet> bullets;

        //Other stuff
        public int health;
        Random random = new Random();
        public float randX, randY;

        //Constructor
        public Enemy(int type) {

            //set basic ship parameters
            shiptype = type;
            texture = null;
            isVisible = true;
            bullets = new List<Bullet>();
            bulletDelay = 0;

            //defualt, medium fighter type
            if (type == 0) {
                //Modify things unique to this object
                position = new Vector2(random.Next(0, Defualt.Default._W), random.Next(-1 * Defualt.Default._H, -100)); 
                speed = 3;
                bulletspeed = 10;
                shootspeed = 50;
                health = 50;
            }


        }

        //Load method
        public void LoadContent(ContentManager Content) {
            if (shiptype == 0) Content.Load<Texture2D>("enemyship0");

        }

        //Draw method
        public void Draw(SpriteBatch spritebatch) {
            if (isVisible) spritebatch.Draw(texture, position, Color.White);
        }

        //update method
        public void update() {

            //set bounding box for collision
            boundingBox = new Rectangle((int)(position.X - (texture.Width / 2)), (int)(position.Y - (texture.Height / 2)), texture.Width, texture.Height);

            //update movement
            position.Y += speed;
            if (position.Y >= Defualt.Default._H + 100)
                isVisible = false;

            //only shoot bullets if they are on the screen
            if (position.Y > 0) shoot_bullets();

            //update all bullets
            update_bullets();
        }

        //shoot bullets helper function
        private void shoot_bullets() {
             //shoot only if the bullet delay is set
            if (bulletDelay >= shootspeed) {

                //Determine bullet texture, and bullet position
                if (shiptype == 0) {
                    Bullet b = new Bullet(bulletTexture);
                    b.position = new Vector2(position.X + texture.Width / 2 - b.texture.Width / 2,
                                            position.Y + texture.Height / 2 - b.texture.Height / 2);
                    b.isVisible = true;
                    bulletDelay = 0;
                    if (bullets.Count() < 20) bullets.Add(b); //add bullet
                }      
                       
            }

        }


        //update bullets help function
        public void update_bullets() {

            //move all bullets that are owned by the ship
            for (int i = 0; i < bullets.Count(); i++) {
                bullets[i].position.Y -= bulletspeed; //move bullet
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
