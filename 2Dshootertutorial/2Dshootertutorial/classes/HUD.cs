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
            scorePos = new Vector2(Defualt.Default._W + 10, 100);
            healthPos = new Vector2(Defualt.Default._W + 10, 150);
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
        }

        //Game over method
        public void Draw_gameOver(SpriteBatch spritebatch,int newscore) {

            //Draw first line of text: Game over
            string text = "Game Over";
            Vector2 pos = new Vector2( ((Defualt.Default._W+200)/2)-50, Defualt.Default._H / 2);
            spritebatch.DrawString(spritefont, text, pos, Color.White, 0f, new Vector2(0, 0), 1f, SpriteEffects.None, 0f);

            //Draw second line of text: Your final score
            string text2 = "Your Final Score: " + newscore;
            Vector2 pos2 = new Vector2( ((Defualt.Default._W+200)/2)-100, (Defualt.Default._H / 2) +30);
            spritebatch.DrawString(spritefont, text2, pos2, Color.White, 0f, new Vector2(0, 0), 1f, SpriteEffects.None, 0f);
        }


        //Update Method
        public void Update(int newscore, int newhealth) {
            if (newhealth < 0) newhealth = 0;
            scoreText = "Score: " + newscore;
            healthText = "Health: " + newhealth + "%";
        }


    }
}
