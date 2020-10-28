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
    class powerup : GameObject
    {
        Random rand = new Random();
        int type;
        public powerup(string Name, int Type)
            : base(Name, 68, 68, "powerups.png", 3, 1, 3, 20f)
        {
            CollisionData.CollisionEnabled = true;
            CollisionData.SetCollisionData(33, 33);
            ZOrder = 4;
            Position.Y = rand.Next(-512, 7680);
            Position.X = rand.Next(-500, 500);           
            type = Type;
            AnimationData.GoToFrame((uint)type);
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
