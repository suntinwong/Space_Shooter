using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace _2Dshootertutorial {


    //Creates a scrolling background with the specified texture.
    //makes two textures, one on top of the other to create an illusion of continous scrolling
    public class Starfield {

        public Texture2D texture;
        public Vector2 bgPosition1,bgPosition2; //bg#1,bg#2 positions
        public float speed;


        //Constructor
        public Starfield() {
            texture = null;
            bgPosition1 = new Vector2(0, 0);
            bgPosition2 = new Vector2(0, -1920);
            speed = 1f;
        }

        //load content
        public void LoadContent(ContentManager content) {
            texture = content.Load<Texture2D>("Artwork/space");
            bgPosition2 = new Vector2(0, -1 * texture.Height);
        }

        //draw method
        public void Draw(SpriteBatch spritebatch) {
            spritebatch.Draw(texture, bgPosition1, Color.White);
            spritebatch.Draw(texture, bgPosition2, Color.White);
        }

        //update mehtod
        public void Update(GameTime gametime) {

            //move backgrounds
            bgPosition1.Y += speed;
            bgPosition2.Y += speed;

            //scrolling illusion, reset coordinates when needed
            if (bgPosition1.Y >= texture.Height) {
                bgPosition1.Y = 0;
                bgPosition2.Y = -1 * texture.Height;
            }
        }




    }
}
