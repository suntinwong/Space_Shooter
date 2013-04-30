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
        public float speed;

        //Constructor
        public Bullet(Texture2D newtexture) {
            texture = newtexture;
            isVisible = false;
            speed = Defualt.Default.BulletDefualtSpeed;
        }

        //draw method
        public void Draw(SpriteBatch spritebatch){
            if(isVisible) spritebatch.Draw(texture, position, Color.White);
        }

    }
}
