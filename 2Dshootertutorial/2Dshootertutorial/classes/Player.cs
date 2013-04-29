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
        public Texture2D texture;
        public Vector2 position;
        public int speed; //ship's movement speed

        //Collision varibles
        public Rectangle boundingBox;
        public bool isColliding;

        //Defualt Constructor
        public Player(){

            //set some varibles
            texture = null;
            position = new Vector2(300, 600);
            speed = Defualt.Default.PlayerDefualtSpeed;
            isColliding = false;
        }

        //load content
        public void LoadContent(ContentManager Content){
            texture = Content.Load<Texture2D>("ship");
        }

        //draw funciton
        public void Draw(SpriteBatch spriteBatch){
            spriteBatch.Draw(texture, position, Color.White);
        }

        //update function
        public void Update(GameTime gameTime) {

            bool moveLeft, moveRight, moveUp, moveDown;

            //get the keyboard state input
            if (!Defualt.Default.UsingKinect) {
                KeyboardState keystate = Keyboard.GetState();
                if (keystate.IsKeyDown(Keys.W)) moveUp = true; else moveUp = false;
                if (keystate.IsKeyDown(Keys.A)) moveLeft = true; else moveLeft = false;
                if (keystate.IsKeyDown(Keys.S)) moveDown = true; else moveDown = false;
                if (keystate.IsKeyDown(Keys.D)) moveRight = true; else moveRight = false;
            }
            
            //Getting kinect input
            else {
                moveUp = false;
                moveLeft = false;
                moveRight = false;
                moveDown = false;
            }

            //Move ship if applicable
            if (moveUp) position.Y -= speed;
            if (moveLeft) position.X -= speed;
            if (moveDown) position.Y += speed;
            if (moveRight) position.X += speed;

            //keep player in playing screen if applicable
            if (position.X <= 0) position.X = 0;
            if (position.X >= Defualt.Default._W - texture.Width) position.X = Defualt.Default._W - texture.Width;
            if (position.Y <= 0) position.Y = 0;
            if (position.Y >= Defualt.Default._H - texture.Height) position.Y = Defualt.Default._H - texture.Height;
           
        }

    }
}
