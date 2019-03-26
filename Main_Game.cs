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
    class Main_Game : State
    {
        static Random rand = new Random();
        public int score=0;
        int FinalScore;
        int spawnspeed = 160;
        int Hspawnspeed = 480;
        int Bspawnspeed = 800;
        int Mspawnspeed = 250;
        int healthspawnspeed = 800;
        int Bspawntimer;
        int Hspawntimer;
        int Mspawntimer;
        int powerTimer=1200;
        int healthBonus = 3600;
        int spawn = 600;
        int timer = 60;
        int spawntimer;
        int spawnhealth;
        void CreatePowerup()
        {
            ObjectManager.RemoveAllObjectsByName("spreadShot");
            ObjectManager.RemoveAllObjectsByName("spikeBall");
            ObjectManager.RemoveAllObjectsByName("machineGun");         
            int type = rand.Next(0, 3);//should be 0 and 3
            if (type == 0)
            {
                name = "spreadShot";
            }
            if (type == 1)
            {
                name = "spikeBall";
            }
            if (type == 2)
            {
                name = "machineGun";
            }            
            GameObject power = new powerup(name, type);            
            ObjectManager.AddGameObject(power);
        }
        public override void Create()
        {
            base.Create();
            spawntimer = rand.Next(30, 60);
            Hspawntimer = rand.Next (1200, 1800);
            Bspawntimer = rand.Next(2400, 3200);
            Mspawntimer = rand.Next(60, 120);
            spawnhealth = rand.Next(2400, 3200);//should be 2400 and 3200
            
            GameObject background = new GameObject("Background", 1024, 7680, "BackgroundLevel.png");
            background.Position.Y = 3456;
            ObjectManager.AddGameObject(background);
            background.ZOrder = -10;

            GameObject Tank = new Tank();
            ObjectManager.AddGameObject(Tank);
            GameObject tankShell = ObjectManager.GetObjectByName("TankShell");

            GameObject Health = new GameObject("Health", 96, 32, "Health.png", 4, 1, 4, 0.1f);            
            Health.Position.Y = 368;
            Health.Position.X = -460;
            ObjectManager.AddGameObject(Health);

            GameObject HomingEnemy = new HomingEnemy();
            GameObject EnemyBase = new EnemyBase();
            GameObject ShootingEnemy = new ShootingEnemy();
            GameObject Heart = new Heart();

            GameObject Shoot = ObjectManager.GetObjectByName("ShootingEnemy");
            GameObject Base = ObjectManager.GetObjectByName("EnemyBase");
            GameObject Homing = ObjectManager.GetObjectByName("HomingEnemy");
            

            ObjectManager.AddGameObject(new ShootingEnemy());
            ObjectManager.AddGameObject(new Mine());
            ObjectManager.AddGameObject(new Mine());
            ObjectManager.AddGameObject(new Mine());
            ObjectManager.AddGameObject(new Mine());

            TextObject Score = TextManager.AddText("Score", "Score: ", FontTypes.ComicSansMS32);
            Score.Position.Y = 368;
            Score.Position.X = 360;
            //Engine.Graphics.DrawCollisionData(true);
        }
        public override void Update()
        {
            //reference objects
            GameObject shootingEnemy = ObjectManager.GetObjectByName("ShootingEnemy");
            GameObject enemyBase = ObjectManager.GetObjectByName("EnemyBase");
            GameObject homingEnemy = ObjectManager.GetObjectByName("HomingEnemy");
            GameObject health = ObjectManager.GetObjectByName("Health");
            GameObject tank = ObjectManager.GetObjectByName("TankBase");
            base.Update();

            //timer decrements
            --healthBonus;
            --spawn;
            --Hspawntimer;
            --Bspawntimer;
            --Mspawntimer;
            --spawntimer;
            --timer;
            --spawnhealth;
            --powerTimer;

            //timer checks
            if (healthBonus <= 0)
            {
                ShootingEnemy.healthTotal *= 2;
                EnemyBase.hitTotal *= 2;
                HomingEnemy.healthTotal *= 2;
                healthBonus = 7200;
            }                       
            if (spawn <= 0)
            {
                spawn = 600;
                spawnspeed -= 5;
                Hspawnspeed -= 5;
                Bspawnspeed -= 5;
                Mspawnspeed -= 5;
                ++healthspawnspeed;
            }
            if (powerTimer <= 0)
            {
                CreatePowerup();
                powerTimer = 1200;
            }
            if (spawnhealth <= 0)
            {                          
                ObjectManager.AddGameObject(new Heart());
                spawnhealth = rand.Next(healthspawnspeed, healthspawnspeed*2);
            }
            if (spawntimer <= 0)
            {
                GameObject ShootingEnemy = new ShootingEnemy();
                ObjectManager.AddGameObject(ShootingEnemy);
                spawntimer = rand.Next(spawnspeed, spawnspeed * 2);
            }
            if (Hspawntimer <= 0)
            {
                GameObject HomingEnemy = new HomingEnemy();
                ObjectManager.AddGameObject(HomingEnemy);
                Hspawntimer = rand.Next(Hspawnspeed, Hspawnspeed * 2);
            }
            if (Mspawntimer <= 0)
            {
                GameObject Mine = new Mine();
                ObjectManager.AddGameObject(Mine);
                Mspawntimer = rand.Next(Mspawnspeed, Mspawnspeed * 2);
            }
            if (Bspawntimer <= 0)
            {
                GameObject EnemyBase = new EnemyBase();
                if (!EnemyBase.IsInViewport())
                {
                    ObjectManager.AddGameObject(EnemyBase);
                }
                Bspawntimer = rand.Next(Bspawnspeed, Bspawnspeed * 3);
            }
            if (timer <= 0)
            {
                timer = 30;
            }

            //check for health
            if ((tank as Tank).Phealth == 3)
            {
                health.AnimationData.GoToFrame(0);
            }
            if ((tank as Tank).Phealth == 2)
            {
                health.AnimationData.GoToFrame(1);
            }
            if ((tank as Tank).Phealth == 1)
            {
                health.AnimationData.GoToFrame(2);
            }
            if ((tank as Tank).Phealth == 0)
            {
                health.AnimationData.GoToFrame(3);
                //death
                GameObject GameOver = new GameObject("GameOver", 1024, 768, "GameOver.png");
                GameOver.Position.Y = Camera.Position.Y - 50;
                GameOver.ZOrder = 20;
                ObjectManager.AddGameObject(GameOver);

                GameObject Restart = new RestartButton();
                Restart.Position.Y = Camera.Position.Y -200;
                ObjectManager.AddGameObject(Restart);

                GameObject cursor = new cursor();
                ObjectManager.AddGameObject(cursor);
                cursor.Position.Y = tank.Position.Y;

                TextObject TotalScore = TextManager.AddText("Score", "Score: ", FontTypes.ComicSansMS64);
                TotalScore.Text = "Final Score: " + FinalScore.ToString();
                TotalScore.Position.X -= 125;
                TotalScore.Position.Y = Camera.Position.Y;
                (tank as Tank).Phealth = -1;
            }
            if ((tank as Tank).Phealth > 0)
            {
                TextObject Score = TextManager.GetTextObjectByName("Score");
                Score.Text = "Score: " + (tank as Tank).score.ToString();
                FinalScore = (tank as Tank).score;
            }
            if (InputManager.IsTriggered(Keys.Escape))
            {
                Game.quit = true;
            }
        }
    }
}
