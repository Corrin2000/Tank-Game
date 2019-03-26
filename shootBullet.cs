using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
using Engine;

namespace MyGame
{
    class shootBullet : Bullet
    {      
        string lr;
        string ud;
        //int delay = 30;
        public shootBullet( string LR, string UD, PointF position)
            : base(position)
        {
            lr = LR;
            ud = UD;
            //SoundManager.AddSoundEffect("fire", "shoot.mp3");
            if (IsInViewport())
            {
                //SoundManager.PlaySoundEffect("fire");
            }
        }
        public override void Update()
        {
            base.Update();
            if (lr == "left")
            {
                Position.X -= speed;
            }
            else if (lr == "right")
            {
                Position.X += speed;
            }
            if (ud == "down")
            {
                Position.Y -= speed;
            }
            else if (ud == "up")
            {
                Position.Y += speed;
            }            
        }
    }
}
