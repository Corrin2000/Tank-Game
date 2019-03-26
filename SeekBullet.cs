using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Engine;
using System.Drawing;
using System.Windows.Forms;

namespace MyGame
{
    class SeekBullet : Bullet
    {
        bool set;
        PointF vector;
        int delay = 15;
        public SeekBullet(PointF position)
            : base(position)
        {
            GameObject tank = ObjectManager.GetObjectByName("TankBase");
            vector = new PointF(tank.Position.X - Position.X, tank.Position.Y - Position.Y);
            //SoundManager.AddSoundEffect("shot", "shoot.mp3");
            //SoundManager.PlaySoundEffect("shot");
        }
        public override void Update()
        {
            base.Update();
            delay--;
            if (!IsInViewport() && delay == 0)
            {
                SoundManager.StopSoundEffect("shot");
            }
            
            
            if (set == false)
            {
                float length = vector.X * vector.X + vector.Y * vector.Y;
                length = (float)Math.Sqrt(length);
                if (length != 0)
                {
                    vector.X /= length;
                    vector.Y /= length;
                }
                set = true;
            }
            Position.X += vector.X * speed;
            Position.Y += vector.Y * speed;
            //Position.X += 5;
        }
    }
}
