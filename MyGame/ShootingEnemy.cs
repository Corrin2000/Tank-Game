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
    class ShootingEnemy : GameObject
    {
        static Random rand = new Random();        
        bool dead = false;
        public int health = 1;
        public static int healthTotal = 1;
        public ShootingEnemy()
            : base("ShootingEnemy", 64, 64, "ShootingEnemy.png", 2, 1, 2, 0.05f)
        {
           
            CollisionData.CollisionEnabled = true;
            CollisionData.SetCollisionData(16);
            AnimationData.Play();
            GameObject tank = ObjectManager.GetObjectByName("TankBase");
            PposX = rand.Next(-1, 1);
            PposY = rand.Next(-1000, 1000);
            health = healthTotal;
            Position.Y = tank.Position.Y + PposY;
            if (PposX <= 0)
            {
                Position.X = -600;
                Rotation = 270;
            }
            if (PposX >= 0)
            {
                Position.X = 600;
                Rotation = 90;
            }
        }
        float PposX;
        float PposY;
        int stimer = 60;
        float speed = 1.5f;
  
        public override void Update()
        {
            base.Update();
            GameObject tank = ObjectManager.GetObjectByName("TankBase");
            if (health <= 0)
            {
                GameObject HExplosion = new HelicopterExplosion(Position);
                ObjectManager.AddGameObject(HExplosion);
                if (dead == false)
                {
                    (tank as Tank).score += 1;
                }
                dead = true;
                IsDead = true;
            }
            --stimer;
            PointF vector = new PointF(tank.Position.X - Position.X, tank.Position.Y - Position.Y);
            float length = vector.X * vector.X + vector.Y * vector.Y;
            length = (float)Math.Sqrt(length);
            if (length != 0)
            {
                vector.X /= -length;
                vector.Y /= -length;
            }
            if (length <= 200)
            {
                Position.X += vector.X * speed;
                Position.Y += vector.Y * speed;
            }
            if (length >= 202)
            {
                Position.X += vector.X * -speed;
                Position.Y += vector.Y * -speed;
            }
            if (length == 201)
            {
                Position.X += 0;
                Position.Y += 0;
            }
            if (length <= 75)
            {
                speed = 2.5f;
            }
            else
            {
                speed = 1.5f;
            }
            if (stimer <= 0)
            {
                ObjectManager.AddGameObject(new SeekBullet(Position));
                stimer = 90;
            }
            float angleRadians = (float)Math.Acos(vector.X);
            float angleDegrees = angleRadians * 180 / (float)Math.PI;
            if (vector.Y <= 0)
            {
                angleDegrees *= -1;
            }
            Rotation = (angleDegrees - 90) - 180;
        }

        public override void CollisionReaction(CollisionInfo collisionInfo_)
        {
            base.CollisionReaction(collisionInfo_);            
            if (collisionInfo_.collidedWithGameObject.Name == "TankShell")
            {                
                health--;                
            }
        }
    }
}
