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
    class Gun : GameObject
    {        
        GameObject Tank;
        int counter = 0;
        int timer = 15;
        public Gun(GameObject tank)
            : base("TankGun", 128, 128, "TankGun.png")
        {
            Tank = tank;           
            ZOrder = 2;
        }
        public override void Update()
        {
            base.Update();
            GameObject TankShell = ObjectManager.GetObjectByName("TankShell");
            timer--;
            Name = "TankGun" + counter;
            ObjectManager.RemoveAllObjectsByName("TankGun");

            GameObject tank = ObjectManager.GetObjectByName("TankBase");
            counter++;

            Position.Y = tank.Position.Y;
            Position.X = tank.Position.X;
            if ((tank as Tank).power != "Spike Ball")
            {
                Scale.X = 1;
                Scale.Y = 1;
                CollisionData.SetCollisionData(4);
            }
            if ((tank as Tank).Phealth > 0)
            {                
                if (InputManager.IsPressed(Keys.Left) && (tank as Tank).power == "Machine Gun")
                {
                    if (timer <= 0)
                    {
                        ObjectManager.AddGameObject(new TankShell(Position, 90));
                        if ((tank as Tank).powerLevel == 3)
                        {
                            timer = 3;
                        }
                        else
                        {
                            timer = 5;
                        }
                        (tank as Tank).powerTotal--;
                    }
                    Rotation = 90;
                }
                else if (InputManager.IsPressed(Keys.Right) && (tank as Tank).power == "Machine Gun")
                {
                    if (timer <= 0)
                    {
                        ObjectManager.AddGameObject(new TankShell(Position, 270));
                        if ((tank as Tank).powerLevel == 3)
                        {
                            timer = 3;
                        }
                        else
                        {
                            timer = 5;
                        }
                        (tank as Tank).powerTotal--;
                    }
                    Rotation = 270;
                }
                else if (InputManager.IsPressed(Keys.Up) && (tank as Tank).power == "Machine Gun")
                {
                    if (timer <= 0)
                    {
                        ObjectManager.AddGameObject(new TankShell(Position, 0));
                        if ((tank as Tank).powerLevel == 3)
                        {
                            timer = 3;
                        }
                        else
                        {
                            timer = 5;
                        }
                        (tank as Tank).powerTotal--;
                    }
                    Rotation = 0;
                }
                else if (InputManager.IsPressed(Keys.Down) && (tank as Tank).power == "Machine Gun")
                {
                    if (timer <= 0)
                    {
                        ObjectManager.AddGameObject(new TankShell(Position, 180));
                        if ((tank as Tank).powerLevel == 3)
                        {
                            timer = 3;
                        }
                        else
                        {
                            timer = 5;
                        }
                        (tank as Tank).powerTotal--;
                    }
                    Rotation = 180;
                }
                else if (InputManager.IsTriggered(Keys.Left))
                {
                    if (timer <= 0)
                    {
                        if ((tank as Tank).power == "Spread Shot")
                        {
                            if ((tank as Tank).powerLevel == 1)
                            {
                                ObjectManager.AddGameObject(new TankShell(Position, 22));
                                ObjectManager.AddGameObject(new TankShell(Position, 113));
                            }
                            if ((tank as Tank).powerLevel == 2)
                            {
                                ObjectManager.AddGameObject(new TankShell(Position, 45));
                                ObjectManager.AddGameObject(new TankShell(Position, 90));
                                ObjectManager.AddGameObject(new TankShell(Position, 135));
                            }
                            if ((tank as Tank).powerLevel > 2)
                            {
                                ObjectManager.AddGameObject(new TankShell(Position, 45));
                                ObjectManager.AddGameObject(new TankShell(Position, 90));
                                ObjectManager.AddGameObject(new TankShell(Position, 135));
                                ObjectManager.AddGameObject(new TankShell(Position, 113));
                                ObjectManager.AddGameObject(new TankShell(Position, 22));
                            }
                            (tank as Tank).powerTotal--;
                        }
                        else if ((tank as Tank).power == "Spike Ball")
                        {                            
                            ObjectManager.AddGameObject(new TankShell(Position, 90));
                            (tank as Tank).powerTotal--;
                        }
                        else
                        {
                            ObjectManager.AddGameObject(new TankShell(Position, 90));
                        }
                        timer = 15;
                    }                
                    Rotation = 90;
                    
                }
                else if (InputManager.IsTriggered(Keys.Right))
                {
                    if (timer <= 0)
                    {
                        if ((tank as Tank).power == "Spread Shot")
                        {
                            if ((tank as Tank).powerLevel == 1)
                            {
                                ObjectManager.AddGameObject(new TankShell(Position, 292));
                                ObjectManager.AddGameObject(new TankShell(Position, 247));
                            }
                            if ((tank as Tank).powerLevel == 2)
                            {
                                ObjectManager.AddGameObject(new TankShell(Position, 315));
                                ObjectManager.AddGameObject(new TankShell(Position, 270));
                                ObjectManager.AddGameObject(new TankShell(Position, 225));
                            }
                            if ((tank as Tank).powerLevel > 2)
                            {
                                ObjectManager.AddGameObject(new TankShell(Position, 315));
                                ObjectManager.AddGameObject(new TankShell(Position, 270));
                                ObjectManager.AddGameObject(new TankShell(Position, 225));
                                ObjectManager.AddGameObject(new TankShell(Position, 247));
                                ObjectManager.AddGameObject(new TankShell(Position, 292));
                            }
                            (tank as Tank).powerTotal--;
                        }
                        else if ((tank as Tank).power == "Spike Ball")
                        {                            
                            ObjectManager.AddGameObject(new TankShell(Position, 270));
                            (tank as Tank).powerTotal--;
                        }
                        else
                        {
                            ObjectManager.AddGameObject(new TankShell(Position, 270));
                        }
                        timer = 15;
                    }                
                    Rotation = 270;
                }
                else if (InputManager.IsTriggered(Keys.Up))
                {
                    if (timer <= 0)
                    {
                        if ((tank as Tank).power == "Spread Shot")
                        {
                            if ((tank as Tank).powerLevel == 1)
                            {
                                ObjectManager.AddGameObject(new TankShell(Position, 337));
                                ObjectManager.AddGameObject(new TankShell(Position, 67));
                            }
                            if ((tank as Tank).powerLevel == 2)
                            {
                                ObjectManager.AddGameObject(new TankShell(Position, 315));
                                ObjectManager.AddGameObject(new TankShell(Position, 0));
                                ObjectManager.AddGameObject(new TankShell(Position, 45));
                            }
                            if ((tank as Tank).powerLevel > 2)
                            {
                                ObjectManager.AddGameObject(new TankShell(Position, 315));
                                ObjectManager.AddGameObject(new TankShell(Position, 0));
                                ObjectManager.AddGameObject(new TankShell(Position, 45));
                                ObjectManager.AddGameObject(new TankShell(Position, 337));
                                ObjectManager.AddGameObject(new TankShell(Position, 67));
                            }
                            (tank as Tank).powerTotal--;
                        }
                        else if ((tank as Tank).power == "Spike Ball")
                        {
                            ObjectManager.AddGameObject(new TankShell(Position, 0));
                            (tank as Tank).powerTotal--;
                        }
                        else
                        {
                            ObjectManager.AddGameObject(new TankShell(Position, 0));
                        }
                        timer = 15;
                    }                
                    Rotation = 0;
                }
                else if (InputManager.IsTriggered(Keys.Down))
                {
                    if (timer <= 0)
                    {
                        if ((tank as Tank).power == "Spread Shot")
                        {
                            if ((tank as Tank).powerLevel == 1)
                            {
                                ObjectManager.AddGameObject(new TankShell(Position, 203));
                                ObjectManager.AddGameObject(new TankShell(Position, 157));
                            }
                            if ((tank as Tank).powerLevel == 2)
                            {
                                ObjectManager.AddGameObject(new TankShell(Position, 225));
                                ObjectManager.AddGameObject(new TankShell(Position, 180));
                                ObjectManager.AddGameObject(new TankShell(Position, 135));
                            }
                            if ((tank as Tank).powerLevel > 2)
                            {
                                ObjectManager.AddGameObject(new TankShell(Position, 225));
                                ObjectManager.AddGameObject(new TankShell(Position, 180));
                                ObjectManager.AddGameObject(new TankShell(Position, 135));
                                ObjectManager.AddGameObject(new TankShell(Position, 203));
                                ObjectManager.AddGameObject(new TankShell(Position, 157));
                            }
                            (tank as Tank).powerTotal--;
                        }
                        else if ((tank as Tank).power == "Spike Ball")
                        {
                            ObjectManager.AddGameObject(new TankShell(Position, 180));
                            (tank as Tank).powerTotal--;
                        }
                        else
                        {
                            ObjectManager.AddGameObject(new TankShell(Position, 180));
                        }
                        timer = 15;
                    }                    
                    Rotation = 180;
                }
            }
            counter++;
        }
    }        
}
