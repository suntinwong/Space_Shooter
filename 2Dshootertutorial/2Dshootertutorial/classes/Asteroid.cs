using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace _2Dshootertutorial {
    public class Asteroid {
        public Texture2D texture;
        public Vector2 position, origin;
        public float rotationAngle;
        public int speed;
        public bool isVisible;
        public Rectangle boundingBox;
        Random random = new Random();
        public float randX, randY;

        //constructor
        public Asteroid(Texture2D newtexture, Vector2 newposition) {
            texture = newtexture;
            position = newposition;
            rotationAngle = 0;
            speed = Defualt.Default.AsteroidDefualtSpeed;
            isVisible = true;
            randX = random.Next(0, Defualt.Default._W);
            randY = random.Next(Defualt.Default._H * -2, -50);
        }

        //Load content method
        public void LoadContent(ContentManager content){

            
           
        }

        //Draw mehtod
        public void Draw(SpriteBatch spritebatch) {
            if (isVisible) {
                spritebatch.Draw(texture, position, null, Color.White, rotationAngle, origin,1.0f, SpriteEffects.None, 0f);
            }
        }

        //Update Method
        public void Update(GameTime gametime) {

            //update origin(for rotation) every frame
            origin.X = texture.Width / 2;
            origin.Y = texture.Height / 2;

            //set bounding box for collision
            boundingBox = new Rectangle((int)(position.X - (texture.Width / 2)), (int)(position.Y - (texture.Height / 2)), texture.Width, texture.Height);

            //update movement
            position.Y += speed;
            if (position.Y >= Defualt.Default._H + 100) {
                isVisible = false;
            }

            //rotate asteroid
            float elapsed = (float)gametime.ElapsedGameTime.TotalSeconds;
            rotationAngle += elapsed;
            float circle = MathHelper.Pi * 2;
            rotationAngle = rotationAngle % circle;
        }
    }
}
