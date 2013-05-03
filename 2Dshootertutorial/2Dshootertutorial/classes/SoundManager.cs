using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Audio;

namespace _2Dshootertutorial {
    public class SoundManager {
        public SoundEffect playerShootSound;
        public SoundEffect explodeSound;
        public SoundEffect playerHit;
        public Song bgm1;

        //Constructor
        public SoundManager() {
            playerShootSound = null;
            explodeSound = null;
            playerHit = null;
            bgm1 = null;
        }

        //Load method
        public void LoadContent(ContentManager content) {
            playerShootSound = content.Load<SoundEffect>("Sounds/playershoot");
            explodeSound = content.Load<SoundEffect>("Sounds/explode");
            playerHit = content.Load<SoundEffect>("Sounds/playerhit");
            bgm1 = content.Load<Song>("Sounds/bgm1");
        }

    }
}
