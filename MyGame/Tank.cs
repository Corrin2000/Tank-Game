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
    class Tank : GameObject
    {
        private int _score=0;
        int timer;
        int maxHealth = 3;
        //bool first = true;
        int counter = 0;
        public string power;
        bool upframe = false;
        int tankspeed = 4;
        public int Phealth = 3;

        double powerUsedPercent = 0;        
        public int powerTotal;
        int spikeTotal = 25;
        int spreadTotal = 25;
        int machineTotal = 100;
        public int powerLevel;

        bool done;
        public int score
        {
            get { return _score; }
            set { _score = value; }
        }

        public Tank()
            : base("TankBase", 128, 128, "TankBottom.png", 3, 1, 3, .05f)
        {
            CollisionData.CollisionEnabled = true;
            CollisionData.SetCollisionData(32);
            ZOrder = 1;            
            AnimationData.GoToFrame(0);
            //power = "Spike Ball";
            //powerTotal = 200;
            powerLevel = 1;
            //power = "Spread Shot";
            //power = "Machine Gun";       
            //SoundManager.AddSoundEffect("move", "tank-move.mp3");
            //SoundManager.PlaySoundEffect("move");
        }
        public override void Update()
        {
            if (power == "Machine Gun" && powerLevel == 2 && done==false)
            {
                machineTotal = 200;
                powerTotal = 200;
                powerUsedPercent = 100 * (((double)powerTotal / machineTotal));
                done = true;
            }
            if (power != "Machine Gun")
            {
                done = false;
                machineTotal = 100;
            }
            if (InputManager.IsTriggered(Keys.K))
            {
                powerLevel++;
            }
            if (power=="Machine Gun")
            {
                powerUsedPercent = 100*(((double)powerTotal / machineTotal));                
            }
            if (power == "Spread Shot")
            {
                powerUsedPercent = 100*((((double)powerTotal / spreadTotal)));
            }
            if (power == "Spike Ball")
            {
                powerUsedPercent = 100*((((double)powerTotal / spikeTotal)));
            }
            if (powerTotal <= 0)
            {
                power = "";
            }           
            ObjectManager.AddGameObject(new Gun(this));            
            base.Update();
            --timer;            
            TextManager.RemoveTextObjectByName("power"+(counter-1));
            TextManager.AddText("power"+counter, "Current Power Is: " + power, FontTypes.Arial32);
            TextObject Power = TextManager.GetTextObjectByName("power"+counter);
            Power.Position = new PointF(-500, 350 + Camera.Position.Y);
            Power.SetColor(1,0,1);
            if (power != "")
            {
                TextManager.RemoveTextObjectByName("powerRemain" + (counter - 1));
                TextManager.AddText("powerRemain" + counter, "Power Percent Remaining: " + (powerUsedPercent), FontTypes.Arial32);
                TextObject PowerRemain = TextManager.GetTextObjectByName("powerRemain" + counter);
                PowerRemain.Position = new PointF(-500, 300 + Camera.Position.Y);
                PowerRemain.SetColor(1, 0, 1);
            }
            else
            {
                TextManager.RemoveTextObjectByName("powerRemain" + counter);
                TextManager.RemoveTextObjectByName("powerRemain" + (counter+1));
                TextManager.RemoveTextObjectByName("powerRemain" + (counter - 1));
            }
            counter++;
            if (upframe == true)
            {
                AnimationData.Play();
            }
            else
            {
                AnimationData.Stop();
            }
            upframe = false;
            //move tank up
            //SoundManager.PauseSoundEffect("move");
            if (Phealth > 0)
            {
                /*if (first == true)
                {
                    SoundManager.PlaySoundEffect("move");
                    first = false;
                }
                SoundManager.UnpauseSoundEffect("move");
                */

                //movement
                if (InputManager.IsPressed(Keys.W) && Position.Y <= 7234)
                {
                    upframe = true;
                    Position.Y += tankspeed;
                    Rotation = 0;
                }
                if (InputManager.IsPressed(Keys.S) && Position.Y >= -320)
                {
                    upframe = true;
                    Position.Y -= tankspeed;
                    Rotation = 180;
                }
                if (InputManager.IsPressed(Keys.D) && Position.X <= 450)
                {
                    upframe = true;
                    Position.X += tankspeed;
                    Rotation = 270;
                }
                if (InputManager.IsPressed(Keys.A) && Position.X >= -450)
                {
                    upframe = true;
                    Position.X -= tankspeed;
                    Rotation = 90;
                }
                if (InputManager.IsPressed(Keys.D) && InputManager.IsPressed(Keys.W))
                {
                    upframe = true;
                    Rotation = 315;
                }
                if (InputManager.IsPressed(Keys.A) && InputManager.IsPressed(Keys.W))
                {
                    upframe = true;
                    Rotation = 45;
                }
                if (InputManager.IsPressed(Keys.S) && InputManager.IsPressed(Keys.A))
                {
                    upframe = true;
                    Rotation = 135;
                }
                if (InputManager.IsPressed(Keys.S) && InputManager.IsPressed(Keys.D))
                {
                    upframe = true;
                    Rotation = 225;
                }
            }
            if (Phealth != 0)
            {
                if (Position.Y >= 0 && Position.Y < 6912)
                {
                    Camera.Position = new PointF(0, Position.Y);
                    GameObject health = ObjectManager.GetObjectByName("Health");
                    TextObject Score = TextManager.GetTextObjectByName("Score");
                    Score.Position.Y = Camera.Position.Y + 378;
                    health.Position.Y = Camera.Position.Y + 368;
                }
            }

            //hurt colors
            if (timer <= 60 && timer >= 0)
            {
                SetModulationColor(1, 0, 0, 0.5f);
            }
            else
            {
                SetModulationColor(0, 0, 0, 0f);
            }

            //No extra health
            if (Phealth > maxHealth)
            {
                Phealth=maxHealth;
            }
       }
        public override void CollisionReaction(CollisionInfo collisionInfo_)
        {
            base.CollisionReaction(collisionInfo_);
            if (collisionInfo_.collidedWithGameObject.Name == "Bullet" || collisionInfo_.collidedWithGameObject.Name == "HomingEnemy" || collisionInfo_.collidedWithGameObject.Name == "mine")
            {
                if (timer <= 0)
                {
                    --Phealth;
                    timer = 60;
                }
            }
            if (collisionInfo_.collidedWithGameObject.Name == "Heart")
            {
                ++Phealth;
            }
            if (collisionInfo_.collidedWithGameObject.Name == "spreadShot")
            {
                if (power == "Spread Shot")
                {
                    powerLevel++;
                }
                else
                {
                    powerLevel = 1;
                }
                power="Spread Shot";
                powerTotal = spreadTotal;                
            }
            if (collisionInfo_.collidedWithGameObject.Name == "spikeBall")
            {
                if (power == "Spike Ball")
                {
                    powerLevel++;
                }
                else
                {
                    powerLevel = 1;
                }
                power="Spike Ball";
                powerTotal = spikeTotal;             
            }
            if (collisionInfo_.collidedWithGameObject.Name == "machineGun")
            {
                if (power == "Machine Gun")
                {
                    powerLevel++;
                }
                else
                {
                    powerLevel = 1;
                }
                power="Machine Gun";
                powerTotal = machineTotal;
            }
        }
            
    }
}
