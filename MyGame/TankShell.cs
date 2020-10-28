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
    class TankShell : GameObject
    {
        public bool isdead = false;        
        bool playing = false;       
        public TankShell(PointF position, int rotation)
            : base("TankShell", 24, 20, "Shell.png", 3, 1, 3, 0.3f)
        {
            GameObject gun = ObjectManager.GetObjectByName("TankGun");
            Position = position;
            ZOrder = -1;            
            Rotation = rotation;
            CollisionData.CollisionEnabled = true;
            CollisionData.SetCollisionData(4);
            //SoundManager.AddSoundEffect("shoot", "TankShot.mp3");
            //SoundManager.PlaySoundEffect("shoot");
        }
        public override void Update()
        {
            GameObject tank = ObjectManager.GetObjectByName("TankBase");

            if ((tank as Tank).power == "Spike Ball")
            {
                if ((tank as Tank).powerLevel == 1)
                {
                    Scale.X = 1;
                    Scale.Y = 1;
                    CollisionData.SetCollisionData(10);
                }
                else if ((tank as Tank).powerLevel == 2)
                {
                    Scale.X = 2;
                    Scale.Y = 2;
                    CollisionData.SetCollisionData(20);
                }
                else if ((tank as Tank).powerLevel > 2)
                {
                    Scale.X = 3;
                    Scale.Y = 3;
                    CollisionData.SetCollisionData(30);
                }
            }
            base.Update();
            if (Rotation == 0)
            {
                Position.Y+=12;
                Rotation = 0;
            }
            if (Rotation == 180)
            {
                Position.Y -= 12;
                Rotation = 180;
            }
            if (Rotation == 90)
            {
                Position.X -= 12;
                Rotation = 90;
            }
            if (Rotation == 270)
            {
                Position.X += 12;
                Rotation = 270;
            }
            if (Rotation == 315)
            {
                Position.Y += 10;
                Position.X += 10;
                Rotation = 315;
            }
            if (Rotation == 45)
            {
                Position.Y += 10;
                Position.X -= 10;
                Rotation = 45;
            }
            if (Rotation == 225)
            {
                Position.X += 10;
                Position.Y -= 10;
                Rotation = 225;
            }
            if (Rotation == 135)
            {
                Position.X -= 10;
                Position.Y -= 10;
                Rotation = 135;
            }
            if (Rotation == 22)
            {
                Position.X -= 9;
                Position.Y += 4;
                Rotation = 22;
            }
            if (Rotation == 67)
            {
                Position.X -= 4;
                Position.Y += 9;
                Rotation = 67;
            }
            if (Rotation == 293)
            {
                Position.X += 9;
                Position.Y += 4;
                Rotation = 293;
            }
            if (Rotation == 337)
            {
                Position.X += 4;
                Position.Y += 9;
                Rotation = 337;
            }
            if (Rotation == 113)
            {
                Position.Y -= 4;
                Position.X -= 9;
                Rotation = 113;
            }
            if (Rotation == 157)
            {
                Position.Y -= 9;
                Position.X -= 4;
                Rotation = 157;
            }
            if (Rotation == 247)
            {
                Position.X += 9;
                Position.Y -= 4;
                Rotation = 247;
            }
            if (Rotation == 203)
            {
                Position.X += 4;
                Position.Y -= 9;
                Rotation = 203;
            }
            if (Rotation == 292)
            {
                Position.X += 9;
                Position.Y += 4;
                Rotation = 292;
            }            
            if ((tank as Tank).power == "Spike Ball"&& playing == false)
            {
                AnimationData.GoToAndPlay(1);
                playing = true;
            }
            if (AnimationData.CurrentFrame == 0 && (tank as Tank).power == "Spike Ball")
            {
                AnimationData.GoToAndPlay(1);
                playing = true;
            }
            if ((tank as Tank).power != "Spike Ball")
            {
                AnimationData.GoToFrame(0);
                playing = false;
            }            
            if (isdead == true)
            {
                IsDead = true;
            }
            if (!IsInViewport())
            {
                IsDead = true;
            }
        }
        public override void CollisionReaction(CollisionInfo collisionInfo_)
        {
            GameObject tank = ObjectManager.GetObjectByName("TankBase");
            base.CollisionReaction(collisionInfo_);
            if (collisionInfo_.collidedWithGameObject.Name == "ShootingEnemy" || collisionInfo_.collidedWithGameObject.Name == "HomingEnemy" || collisionInfo_.collidedWithGameObject.Name == "EnemyBase" || collisionInfo_.collidedWithGameObject.Name == "mine")
            {
                if((tank as Tank).power == "Spike Ball")
                {
                    IsDead = false;
                    isdead = false;
                }
                else
                {
                    IsDead = true;
                }
            }
        }
    }
}
