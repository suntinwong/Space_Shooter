using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;


namespace _2Dshootertutorial {
    public class Player {

        public Texture2D texture;
        public Vector2 position;
        public int speed;

        //Collision varibles
        public Rectangle boundingBox;
        public bool isColliding;

        //Defualt Constructor
        public Player(){

            //set some varibles
            texture = null;
            position = new Vector2(300, 600);
            speed = 10;
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

        }

    }
}
