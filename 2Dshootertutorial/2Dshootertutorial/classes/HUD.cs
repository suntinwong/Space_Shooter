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
        Texture2D hp_empty, hp_greenbar,border;
        string scoreText, healthText;
        Vector2 scorePos, healthPos;
        Color textColor;
        int healthpercent;


        public HUD() {
            scoreText = "Score: ";
            healthText = "Health: ";
            scorePos = new Vector2(Defualt.Default._W - 150, 50);
            healthPos = new Vector2(Defualt.Default._W - 150, 85);
            textColor = Color.White;
            healthpercent = 100;
        }
        
        //load method
        public void LoadContent(ContentManager content) {
            spritefont = content.Load<SpriteFont>("SpriteFonts/MyFont1");
            hp_empty = content.Load<Texture2D>("Artwork/hp_empty"); 
            hp_greenbar = content.Load<Texture2D>("Artwork/hp_greenbar");
            border = content.Load<Texture2D>("Artwork/blackborder");
        }

        //Draw Method
        public void Draw(SpriteBatch spritebatch) {
            spritebatch.Draw(border, new Vector2(0 - border.Width,0), Color.White);
            spritebatch.Draw(border, new Vector2(Defualt.Default._W, 0), Color.White);
            spritebatch.Draw(hp_empty, healthPos, Color.White);
            spritebatch.DrawString(spritefont, scoreText, scorePos, textColor, 0f, new Vector2(0, 0), 1f, SpriteEffects.None, 0f);
            spritebatch.Draw(hp_empty, healthPos, Color.White);
            spritebatch.Draw(hp_greenbar, new Vector2(healthPos.X+40,healthPos.Y+10), new Rectangle(0,0, healthpercent,hp_greenbar.Height), Color.White);
        }

        //Game over method
        public void Draw_gameOver(SpriteBatch spritebatch,int newscore) {

            //Draw first line of text: Game over
            string text = "Game Over";
            Vector2 pos = new Vector2( ((Defualt.Default._W)/2)-25, Defualt.Default._H / 2);
            spritebatch.DrawString(spritefont, text, pos, Color.White, 0f, new Vector2(0, 0), 1f, SpriteEffects.None, 0f);

            //Draw second line of text: Your final score
            string text2 = "Your Final Score: " + newscore;
            Vector2 pos2 = new Vector2( ((Defualt.Default._W)/2)-75, (Defualt.Default._H / 2) +30);
            spritebatch.DrawString(spritefont, text2, pos2, Color.White, 0f, new Vector2(0, 0), 1f, SpriteEffects.None, 0f);
        }


        //Update Method
        public void Update(int newscore, int newhealth) {
            if (newhealth < 0) newhealth = 0;
            scoreText = "Score: " + newscore;
            healthText = "Health: " + newhealth + "%";
            healthpercent = newhealth;
        }


    }
}
