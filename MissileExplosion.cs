﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
using Engine;

namespace MyGame
{
    class MissileExplosion : GameObject
    {
        public MissileExplosion(PointF position)
            : base("EMissile", 64, 64, "MissileExplosion.png", 4, 1, 4, 0.15f)
        {
            GameObject Missile = ObjectManager.GetObjectByName("HomingEnemy");
            Position.X = position.X;
            Position.Y = position.Y;
            AnimationData.Play();
        }
        public override void Update()
        {
            base.Update();
            if (AnimationData.CurrentFrame == 3)
            {
                IsDead = true;
            }
        }
                //GameObject Explosion = new GameObject("EMissile", 64, 64, "MissileExplosion.png", 4, 1, 4, 0.1f);
                //ObjectManager.AddGameObject(Explosion);
                //AnimationData.Play();
    }
}
