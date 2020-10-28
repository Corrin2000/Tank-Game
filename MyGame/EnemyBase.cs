using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Engine;  

namespace MyGame
{
    class EnemyBase : GameObject
    {
        Random rand = new Random();
        int randomspawnY;
        int randomspawnX;
        bool dead = false;
        int stimer = 60;
        public static int hitTotal = 2;
        public int hit = 2;
        public EnemyBase()
            : base("EnemyBase", 64, 64, "EnemyBase.png")
        {  
            GameObject tank = new Tank();
            randomspawnY = rand.Next(-512, 7680);
            randomspawnX = rand.Next(-500, 500);
            Position.X = randomspawnX;
            Position.Y = randomspawnY;
            hit = hitTotal;
            CollisionData.CollisionEnabled = true;
            CollisionData.SetCollisionData(32);
            ZOrder = -1;
        }
        
        public override void Update()
        {
            base.Update();
            --stimer;
            if (stimer <= 0)
            {                
                stimer = 45;
                ObjectManager.AddGameObject(new shootBullet("left", "", Position));
                ObjectManager.AddGameObject(new shootBullet("right", "", Position));
                ObjectManager.AddGameObject(new shootBullet("", "up", Position));
                ObjectManager.AddGameObject(new shootBullet("", "down", Position));
                ObjectManager.AddGameObject(new shootBullet("left", "up", Position));
                ObjectManager.AddGameObject(new shootBullet("right", "up", Position));
                ObjectManager.AddGameObject(new shootBullet("left", "down", Position));
                ObjectManager.AddGameObject(new shootBullet("right", "down", Position));   
            }
            if (hit == hitTotal/2)
            {
                SetModulationColor(0, 0, 0, 0.5f);
            }
            GameObject tank = ObjectManager.GetObjectByName("TankBase");
            if (hit == 0)
            {
                if (dead == false)
                {
                    (tank as Tank).score += 4;
                }
                dead = true;
                IsDead = true;
            }
            if (!IsInViewport() && (tank as Tank).Phealth <= 0)
            {
                IsDead = true;
            }
        }
        public override void CollisionReaction(CollisionInfo collisionInfo_)
        {
            base.CollisionReaction(collisionInfo_);
            if (collisionInfo_.collidedWithGameObject.Name == "TankShell")
            {
                --hit;
            }
        }
    }
}
