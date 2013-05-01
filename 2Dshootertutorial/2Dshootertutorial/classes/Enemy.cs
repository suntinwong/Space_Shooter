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
        public bool isColliding, isVisible;

        //Bullet stuff
        public Texture2D bulletTexture;
        public float bulletDelay;
        public List<Bullet> bullets;

        //Other stuff
        public int health;

        //Constructor
        public Enemy(int type) {


        }

        //Load method
        public void LoadContent(ContentManager Content) {

        }

        //Draw method
        public void Draw(SpriteBatch spritebatch) {


        }

        //update method
        public void update() {

        }

       


    }
}
