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
            texture = Content.Load<Texture2D>("explosion3");
            position = newposition;
            timer = 0f;
            interval = newinterval;
            currentFrame = 1;
            spriteWidth = 128; //width of each frame
            spriteHeight = 128; //height of each frame
            numframes = 17;
            isVisible = true;
            scale = newscale;

        }

        //LoadContent
        public void LoadContent(ContentManager content) {
           
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
            if (isVisible)
                spritebatch.Draw(texture, position, sourceRect, Color.White, 0f, origin,scale,SpriteEffects.None,0);

        }


    }
}
