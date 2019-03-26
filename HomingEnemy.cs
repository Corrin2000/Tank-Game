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
    class HomingEnemy : GameObject
    {
        static Random rand = new Random();
        bool dead = false;
        public static int healthTotal = 1;
        public int health = 1;
        public HomingEnemy()
            : base("HomingEnemy", 20, 40, "Missile.png", 2, 1, 2, 0.1f)
        {
            CollisionData.CollisionEnabled = true;
            CollisionData.SetCollisionData(10);
            AnimationData.Play();
            
            GameObject tank = ObjectManager.GetObjectByName("TankBase");
            PposX = rand.Next(-1, 1);
            PposY = rand.Next(-1000, 1000);
            Position.Y = tank.Position.Y + PposY;
            health = healthTotal;
            if (PposX <= 0)
            {
                Position.X = -600;
            }
            if (PposX >= 0)
            {
                Position.X = 600;
            }
            //SoundManager.AddSoundEffect("rocket", "Rocket.mp3");
            //SoundManager.PlaySoundEffect("rocket");
        }
        float PposX;
        float PposY;
        float speed = 6f;
        public override void Update()
        {
            base.Update();
            GameObject tank = ObjectManager.GetObjectByName("TankBase");
            if (health <= 0)
            {
                GameObject EMissle = new MissileExplosion(Position);
                ObjectManager.AddGameObject(EMissle);
                if (dead == false)
                {
                    (tank as Tank).score += 2;
                }
                dead = true;
                IsDead = true;
            }
            PointF vector = new PointF(tank.Position.X - Position.X, tank.Position.Y - Position.Y);
            float length = vector.X * vector.X + vector.Y * vector.Y;
            length = (float)Math.Sqrt(length);
            if (length != 0)
            {
                vector.X /= length;
                vector.Y /= length;
            }
                Position.X += vector.X * speed;
                Position.Y += vector.Y * speed;
                float angleRadians = (float)Math.Acos(vector.X);
                float angleDegrees = angleRadians * 180 / (float)Math.PI;
                if (vector.Y <= 0)
                {
                    angleDegrees = -angleDegrees;
                }
                Rotation = angleDegrees - 90;
        }
        public override void CollisionReaction(CollisionInfo collisionInfo_)
        {
            base.CollisionReaction(collisionInfo_);
            GameObject tank = ObjectManager.GetObjectByName("TankBase");
            if (collisionInfo_.collidedWithGameObject.Name == "TankBase")
            {
                ObjectManager.AddGameObject(new MissileExplosion(Position));
                SoundManager.StopSoundEffect("rocket");
                IsDead=true;
            }
            else if (collisionInfo_.collidedWithGameObject.Name == "TankShell")
            {
                health--;

            }
        }
    }
}
