using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Engine;
using System.Windows.Forms;
using System.Drawing;

namespace MyGame
{
    class Mine : GameObject
    {
        bool dead = false;
        int spawnTimer = 2;
        int beep = 60;
        Random rand = new Random();
        public Mine()
            : base("mine", 40, 20, "landMine.png", 2, 1, 2, 0.5f)
        {
            CollisionData.CollisionEnabled = true;
            CollisionData.SetCollisionData(40, 20);
            Position.Y = rand.Next(-512, 7680);//-512, 7680
            Position.X = rand.Next(-475, 475);            
            AnimationData.Play();
            //Position.Y = 100;
            //SoundManager.AddSoundEffect("beep", "Beep.mp3");
        }
        public override void Update()
        {
            base.Update();
            spawnTimer--;
            beep--;
            if(beep==0)
            {
                beep=60;
                if(IsInViewport())
                {
                    //SoundManager.PlaySoundEffect("beep");
                }
            }

        }
        public override void CollisionReaction(CollisionInfo collisionInfo_)
        {
            GameObject Tank = ObjectManager.GetObjectByName("TankBase");
            base.CollisionReaction(collisionInfo_);
            if (collisionInfo_.collidedWithGameObject.Name == "TankBase")
            {
                if (spawnTimer < 0)
                {
                    ObjectManager.AddGameObject(new mExplosion(Position));
                }
                IsDead = true;
                
            }
            else if (collisionInfo_.collidedWithGameObject.Name == "TankShell")
            {
                if (dead == false)
                {
                    (Tank as Tank).score += 1;
                    ObjectManager.AddGameObject(new mExplosion(Position));
                }
                dead = true;
                IsDead = true;
            }            
        }
    }
}
