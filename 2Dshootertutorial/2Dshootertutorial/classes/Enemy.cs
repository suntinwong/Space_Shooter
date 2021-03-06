﻿using System;
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
        public float speedX,speedY; //ship's movement speed
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
            bulletDelay = 150;
            speedX = 0; speedY = 0;
            position = new Vector2(posx, posy);
            

            //Type 4, small fast fighter
            if (type == 4) {
                Random random = new Random();
                position = new Vector2(posx, random.Next(-200, -50));
                speedX = (float)(random.Next(-12, 12)) / 20f;
                texture = Content.Load<Texture2D>("Artwork/enemyship4");
                bulletTexture = Content.Load<Texture2D>("Artwork/enemybullet0");
                health = 25; speedY = 4.5f; bulletvelocity = 999f; firerate = 999; bulletdamage = 12; score = 4;
            }

            //Type 0, medium fighter type
            else if (type == 0) {

                Random random = new Random();
                position = new Vector2(posx, random.Next(-200, -50));
                speedX = (float)(random.Next(-10, 10)) / 20f;
                texture = Content.Load<Texture2D>("Artwork/enemyship0");
                bulletTexture = Content.Load<Texture2D>("Artwork/enemybullet0");
                health = 50; speedY = 2.25f; bulletvelocity = 4.75f; firerate = 150; bulletdamage = 12; score = 5;

            }

            //Type 1, large fighter type
            else if (type == 1) {
                texture = Content.Load<Texture2D>("Artwork/enemyship1");
                bulletTexture = Content.Load<Texture2D>("Artwork/enemybullet0");
                health = 100; speedY = 1.25f; bulletvelocity = 4.75f; firerate = 115; bulletdamage = 12; score = 15;
            }

            //Type 2, large fighter type#2
            else if (type == 2) {
                texture = Content.Load<Texture2D>("Artwork/enemyship2");
                bulletTexture = Content.Load<Texture2D>("Artwork/enemybullet1");
                health = 100; speedY = 1.25f; bulletvelocity = 3f; firerate = 110; bulletdamage = 20; score = 15;
            }

            //Type 3, large destroyer
            else if (type == 3) {
                Random random = new Random();
                texture = Content.Load<Texture2D>("Artwork/enemyship3");
                bulletTexture = Content.Load<Texture2D>("Artwork/enemybullet2");
                position = new Vector2(-1 * texture.Width - 10, random.Next(0, 200));
                health = 400; speedX = .25f; bulletvelocity = 7.5f; firerate = 125; bulletdamage = 30; score = 30;
            }

            //Type 5, large destroyer #2
            else if (type == 5) {
                Random random = new Random();
                texture = Content.Load <Texture2D>("Artwork/enemyship5");
                bulletTexture = Content.Load<Texture2D>("Artwork/enemybullet0");
                position = new Vector2(Defualt.Default._W + 10, random.Next(0, 200));
                health = 400; speedX = -.25f; bulletvelocity = 4.75f; firerate = 125; bulletdamage = 30; score = 30;

            }
        }

       
        //Draw method
        public void Draw(SpriteBatch spritebatch) {

            //Only draw if the ship is visible
            if (isVisible) spritebatch.Draw(texture, position, Color.White);

            //Draw all bullets associated with the ship
            for (int i = 0; i < bullets.Count(); i++)  bullets[i].Draw(spritebatch);
            
        }

        //update method
        public void Update(GameTime gametime,Vector2 playerpos) {

            //set bounding box for collision
            if (isVisible) boundingBox = new Rectangle((int)position.X, (int)position.Y, texture.Width, texture.Height);
            else boundingBox = new Rectangle(-900, 0, 1, 1);
            
            //update movement
            position.Y += speedY;
            position.X += speedX;
            if (position.Y >= Defualt.Default._H + 400 || position.X > Defualt.Default._W + 25 || position.X + texture.Width < -25)
                isVisible = false;

            //only shoot bullets if they are on the screen
            if (position.Y > 0 && isVisible) shoot_bullets(playerpos);

            //update all bullets
            update_bullets();
        }

        //shoot bullets helper function
        private void shoot_bullets(Vector2 playerpos) {

             //shoot only if the bullet delay is set - Determine bullet texture, and bullet position
            if (bulletDelay >= firerate) {
                bulletDelay = 0;


                //small fighter type. does not shoot lasers
                if (shiptype == 4) {


                }

                //Defualt, medium fighter type - one laser from center
                else if (shiptype == 0) {
                    Bullet b = new Bullet(bulletTexture, bulletvelocity);
                    b.position = new Vector2(position.X + texture.Width / 2 - b.texture.Width / 2, position.Y + texture.Height / 2 - b.texture.Height / 2);
                    bullets.Add(b); //add bullet
                }

                //Large fighter type - three lasers fanned out
                else if (shiptype == 1) {
                    Bullet b1 = new Bullet(bulletTexture, bulletvelocity);
                    b1.position = new Vector2(position.X + texture.Width / 2 - b1.texture.Width / 2, position.Y + texture.Height / 2 - b1.texture.Height / 2);
                    Bullet b2 = new Bullet(bulletTexture, bulletvelocity);
                    b2.position = new Vector2(position.X - b1.texture.Width / 2, position.Y + texture.Height / 2 - b1.texture.Height / 2);
                    b2.rotation = 45; b2.speedX = -1 * bulletvelocity / 4; b2.speedY = bulletvelocity * .75f;
                    Bullet b3 = new Bullet(bulletTexture, bulletvelocity);
                    b3.position = new Vector2(position.X + texture.Width - b1.texture.Width / 2, position.Y + texture.Height / 2 - b1.texture.Height / 2);
                    b3.rotation = -45; b3.speedX = bulletvelocity / 4; b3.speedY = bulletvelocity * .75f;
                    bullets.Add(b1); //add bullet
                    bullets.Add(b2);
                    bullets.Add(b3);
                }

                //Large fighter type#2 - laser in general direction of player
                else if (shiptype == 2) {
                    Bullet b = new Bullet(bulletTexture, bulletvelocity);
                    b.position = new Vector2(position.X + texture.Width / 2 - b.texture.Width / 2, position.Y + texture.Height / 2 - b.texture.Height / 2);
                    b.speedX = (float)(Math.Atan2((playerpos.X - b.position.X), (playerpos.Y - b.position.Y)));
                    bullets.Add(b); //add bullet
                }

                //cruiser - big fire ball to player's location
                else if (shiptype == 3) {
                    Bullet b1 = new Bullet(bulletTexture, bulletvelocity);
                    b1.position = new Vector2(position.X + texture.Width / 2f - b1.texture.Width / 2, position.Y + texture.Height - b1.texture.Height / 2);
                    b1.speedX = (playerpos.X - b1.position.X) / (bulletvelocity * 15);
                    b1.speedY = (playerpos.Y - b1.position.Y) / (bulletvelocity * 15);
                    b1.rotation = (float)(Math.Atan2((b1.position.X - playerpos.X), (playerpos.Y - b1.position.Y)));
                    bullets.Add(b1); //add bullet
                }

                //Cruiser - 8 downward dumb bullets
                else if (shiptype == 5) {
                    for (int i = 0; i < 6; i++) {
                        Bullet b = new Bullet(bulletTexture, bulletvelocity);
                        b.position = new Vector2(position.X  + ((i* .16f)* texture.Width) - b.texture.Width / 2, position.Y + texture.Height / 2 - b.texture.Height / 2);
                        bullets.Add(b); //add bullet
                    }
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
