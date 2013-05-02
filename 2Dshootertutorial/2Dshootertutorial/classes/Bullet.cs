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
    public class Bullet {
        public Rectangle boundingBox;
        public Texture2D texture;
        public Vector2 position, origin;
        public bool isVisible;
        public float speedY,speedX,rotation;

        //Constructor
        public Bullet(Texture2D newtexture, float newspeed = 10) {
            texture = newtexture;
            isVisible = true;
            speedY = newspeed;
            speedX = 0;
            rotation = 0;
        }

        //draw method
        public void Draw(SpriteBatch spritebatch){
            if(isVisible) 
                spritebatch.Draw(texture, position, null, Color.White, rotation, new Vector2(0, 0), 1f, SpriteEffects.None, 0f);
        }

    }
}
