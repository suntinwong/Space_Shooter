using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace _2Dshootertutorial{
    public class HUD {

        SpriteFont spritefont;
        string scoreText, healthText;
        Vector2 scorePos, healthPos;
        Color textColor;


        public HUD() {
            scoreText = "Score: ";
            healthText = "Health: ";
            scorePos = new Vector2(Defualt.Default._W + 30, 100);
            healthPos = new Vector2(Defualt.Default._W + 30, 150);
            textColor = Color.White;
        }
        
        //load method
        public void LoadContent(ContentManager content) {
            spritefont = content.Load<SpriteFont>("MyFont1");
        }


        //Draw Method
        public void Draw(SpriteBatch spritebatch) {
            spritebatch.DrawString(spritefont, scoreText, scorePos, textColor, 0f, new Vector2(0, 0), 1f, SpriteEffects.None, 0f);
            spritebatch.DrawString(spritefont, healthText, healthPos, textColor, 0f, new Vector2(0, 0), 1f, SpriteEffects.None, 0f);
            //spriteBatch.DrawString(Content.Load<SpriteFont>("MyFont1"), "Hello World", new Vector2(700, 100), Color.White, 0f, new Vector2(0, 0), 1f, SpriteEffects.None, 0f);

        }


        //Update Method
        public void Update(int newscore, int newhealth) {
            if (newhealth < 0) newhealth = 0;
            scoreText = "Score: " + newscore;
            healthText = "Health: " + newhealth + "%";
        
        }


    }
}
