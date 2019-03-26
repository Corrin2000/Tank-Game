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
    class Bullet : GameObject
    {
        public int speed = 8;
        public Bullet(PointF position)
            : base("Bullet", 14, 14, "Bullet.png")
        {
            Position = position;
            CollisionData.CollisionEnabled = true;
            CollisionData.SetCollisionData(4);
        }
        public override void Update()
        {
            base.Update();            
            if (!IsInViewport())
            {
                IsDead = true;
            }            
        }
        public override void CollisionReaction(CollisionInfo collisionInfo_)
        {
            base.CollisionReaction(collisionInfo_);
            if (collisionInfo_.collidedWithGameObject.Name == "TankBase")
            {
                IsDead = true;
            }
        }
    }
}
