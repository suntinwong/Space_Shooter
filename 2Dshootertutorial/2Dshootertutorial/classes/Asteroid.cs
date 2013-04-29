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
        public bool isColliding, isdestroyed;
        public Rectangle boundingBox;

        //constructor
        public Asteroid() {
            texture = null;
            position = new Vector2(300, -100);
            rotationAngle = 0;
            speed = Defualt.Default.AsteroidDefualtSpeed;
            isColliding = false;
            isdestroyed = false;
        }

        //Load content method
        public void LoadContent(ContentManager content){
            texture = content.Load<Texture2D>("asteroid");
            origin.X = texture.Width / 2;
            origin.Y = texture.Height / 2;
        }

        //Draw mehtod
        public void Draw(SpriteBatch spritebatch) {
            if (!isdestroyed) {
                spritebatch.Draw(texture, position, null, Color.White, rotationAngle, origin,1.0f, SpriteEffects.None, 0f);
            }
        }

        //Update Method
        public void Update(GameTime gametime) {
            
            //set bounding box for collision
            boundingBox = new Rectangle((int)position.X, (int)position.Y,(int)texture.Width,(int)texture.Height);

            //update movement
            position.Y += speed;
            if (position.Y >= Defualt.Default._H) position.Y = -100;

            //rotate asteroid
            float elapsed = (float)gametime.ElapsedGameTime.TotalSeconds;
            rotationAngle += elapsed;
            float circle = MathHelper.Pi * 2;
            rotationAngle = rotationAngle % circle;
        }



    }
}
