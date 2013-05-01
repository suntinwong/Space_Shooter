using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace _2Dshootertutorial {
    public class Explosion {
        public Texture2D texture;
        public Vector2 position,origin;
        public float timer, interval,scale;
        public int currentFrame, spriteWidth, spriteHeight,numframes;
        public Rectangle sourceRect;
        public bool isVisible;

        //Constructor
        public Explosion(ContentManager Content,Vector2 newposition,float newinterval, float newscale) {

            //Important properties of the object
            position = newposition; //world space of the sprite
            scale = newscale;       //scale of the sprite
            interval = newinterval; //speed at which we go through the sprites
            
            //Other stuff
            texture = Content.Load<Texture2D>("explosion3");
            timer = 0f;
            isVisible = true;
            currentFrame = 1; //frame number where we start on
            numframes = 17; //total number of frames for the sprite
            spriteWidth = 128; //width of each frame
            spriteHeight = 128; //height of each frame
          
        }

        //Update method
        public void Update(GameTime gametime) {

            //increase timer since last update
            timer += (float)gametime.ElapsedGameTime.TotalMilliseconds;

            //check time, do a update if necessary
            if (timer > interval) {currentFrame++;timer = 0;}
            
            //if last frame kill sprite
            if (currentFrame > numframes) {isVisible = false;currentFrame = 0;}

            //set the current frame
            sourceRect = new Rectangle(currentFrame * spriteWidth, 0, spriteWidth, spriteHeight);

            //set the origin
            origin = new Vector2(sourceRect.Width / 2, sourceRect.Height / 2);
        }

        //Draw method
        public void Draw(SpriteBatch spritebatch) {
            if (isVisible) //Only draw when visible
                spritebatch.Draw(texture, position, sourceRect, Color.White, 0f, origin,scale,SpriteEffects.None,0);
        }
    }
}
