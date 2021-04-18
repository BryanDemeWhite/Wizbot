using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using Microsoft.Extensions.DependencyInjection;

namespace WizBot.Modules
{
    //int b = 1;
    public class Thing : ModuleBase<SocketCommandContext>
    {
       // static int FireBall = 1;
      //  static int FireBallCooldown; //PEROSNAL
        //  static int BurnOrNot = 1;
        // static int block;
    //    static int SlashCooldown; //PEROSNAL
       
      //  static int hpPot = 1;
     //   static int quickAttack = 1;
     //   static int qos = 1;
        ////////////////////^^^^

        // static double Armor = randy = rand.NextDouble
        public static Random rand = new Random();
        public MonsterHealth myMonster = new Thing.MonsterHealth();
     

        public static int BaseHP = rand.Next(1,100);
        public static int CurrentHP = BaseHP;
        public class MonsterHealth
        {
            
           
            static int travel = 1;
            static int harder = Everyone.DifficultyUp * 10;
            static int a = harder+90;
            static int Loot = 0;
            static int PlayerEXP = 0;

            static double level = 1;




            /// //////////////////////////////////////////////////
         
         
            public int GetHealth()
            {
                return CurrentHP;
            }
            public int GetfullHealth()
            {
                return BaseHP;
            }

            
            /// ///////////////////////////////////////////////////
            public void Respawn()
            {
       BaseHP = rand.Next(1,a);
               CurrentHP = BaseHP;
            }
            public int Travely()
            {
            
                return travel;
            }

            public void Randomizor()
            {
                travel = rand.Next(1, 100);
                Loot = rand.Next(-30, 20);
                PlayerEXP = rand.Next(-20, 10);
            }
            public double LevelFinder() //for monster
            {
                level = BaseHP * .1;
                int value = (int)level;
                int rounded = (int)level;
                return rounded + 1;
            }
            ///////////////////////////////////////////    
            public void LootDo() // for monster
            {
                Randomizor();
                Loot = Loot + GetfullHealth();
                if (Loot <= 0)
                {
                    Loot = 1;
                }

            }
            public int LootReturn() // for monster
            {
                return Loot;
            }
            ///////////////////////////////////////////    
            public void PlayerEXPDo() // for monster
            {
               
                PlayerEXP = PlayerEXP + GetfullHealth();
                if (PlayerEXP <= 0)
                {
                    PlayerEXP = 1;
                }

            }
            public int PlayerEXPReturn() // for monster
            {
                return PlayerEXP;
            }



        }
        public class HunterHealth
        {
            private static int FireBall = 1;
          //  private static int FireBallCooldown;
            public static int HunterPoisonCooldown;//PEROSNAL
            private static double fix = 1;                            //  static int BurnOrNot = 1;
            private static int hpPot = 1;
            private static int quickAttack = 1;
            private static int qos = 1;                         // static int block;
            private static double Armor = 1;
            private static double ReducedAttack = 1;
            private static double Percent = 1;
            private static int SlashCooldown; //PEROSNAL
            private static int Counter = 1;
            private static int Burn = 1;
            private static int Arrow = 0;
            private static int ArrowCooldown = 0; //PEROSNAL
            private static int Slash = 0;
            private static int AgileCooldown = 0;

            public static int hunterpoison = 0;
            private static int leveldamamp = HunterOnly.HunterDamagePoint;
            
            private static int YourBaseHP = 500 - 35 + ((Everyone.HunterLevel * 35)* HunterOnly.HunterHPPoint);
            public static double HunterCurrentHP = YourBaseHP;
            private static int c = 1;
            private static double ArmorBonus = HunterOnly.HunterArmorPoint * .025;
           private static double helpyyy = 1 - ArmorBonus;
            
            private static double newcounter = Counter * helpyyy;
            // private static int DamageBonus = HunterOnly.HunterDamagePoint;
            /// ////////////////////////////////////////////////////////
            public void Randomizor()
            {
                hpPot = rand.Next(100, 150);
                Counter = rand.Next(-(BaseHP / 2), (BaseHP / 2));
                Counter = (BaseHP / 2) - Counter;
                Armor = rand.NextDouble();
                Slash = rand.Next(3- c, 7 - c) + leveldamamp;
                Arrow = rand.Next(1- c, 10 - c) + leveldamamp;
                quickAttack = rand.Next(1- c, 10 - c) + leveldamamp;
                qos = rand.Next(1, 10);
                hunterpoison = rand.Next(3- c, 7 - c) + leveldamamp;

            }
            public double GetYourHealth() //perosnal
            {
                int value = (int)HunterCurrentHP;
                int rounded = (int)HunterCurrentHP;
                return rounded;
            }
            public double GetYourFullHealth() //persoanl
            {
                YourBaseHP = 500 - 35 + ( HunterOnly.HunterHPPoint * 35);
                return YourBaseHP;
            }
            public void QuickAttack()
            {
               
                Randomizor();
                CurrentHP = CurrentHP - quickAttack;
             

            }
            public int QuickOrSlow()
            {

                return qos;
            }
         

            public void PoisonDo()
            {
                Randomizor();
             
                CurrentHP = CurrentHP - hunterpoison;
                HunterPoisonCooldown = 5;
            }
            public void PoisonCool()
            {
                HunterPoisonCooldown--;
                if (HunterPoisonCooldown < 0)
                {
                    HunterPoisonCooldown = 0;
                }
            }
            public int QuickAttackReturn()
            {
                return quickAttack;

            }
            ////////////////////////////////////////////////////////
            public void AllCoolDown()
            {
                ArrowAttackCool();
                SlashAttackCool();
                PoisonCool();
                AgileCool();
                //  FireBallCool();

            }
            public void HealthPotion()
            {
                Randomizor();
                HunterCurrentHP = HunterCurrentHP + hpPot;
                if (HunterCurrentHP > YourBaseHP)
                {
                    HunterCurrentHP = YourBaseHP;
                }

            }
            public int HealthPotionReturn()
            {
                return hpPot;

            }
            public void SlashAttackDo()
            {
                Randomizor();
                CurrentHP = CurrentHP - Slash;
                SlashCooldown = 1;
                //////////////////////////////////////////////////COOL DOWN REDUCTIONS******
                PoisonCool();
                //FireBallCool();
                AgileCool();
                //////////////////////////////////////////////////////////////////////////

            }
            public void SlashAttackCool()
            {
                SlashCooldown--;
                if (SlashCooldown < 0)
                {
                    SlashCooldown = 0;
                }
            }
            public void AgileDo()
            {
                AgileCooldown=10;
              
            }
            public void AgileCool()
            {
                AgileCooldown--;
                if (AgileCooldown < 0)
                {
                    AgileCooldown = 0;
                }
            }
            public int AgileCoolReturn()
            {
                return AgileCooldown;
            }
            public int SlashAttackCoolReturn()
            {
                return SlashCooldown;
            }
            public int SlashAttackReturn()
            {
                return Slash;
            }
            ////////////////////////////////////
            public void ArrowAttackDo()
            {
                Randomizor();
                CurrentHP = CurrentHP - Arrow;
                ArrowCooldown = 1;
                //////////////////////////////////////////////////COOL DOWN REDUCTIONS******
                PoisonCool();
                //FireBallCool();
                AgileCool();
                //////////////////////////////////////////////////////////////////////////

            }
            public void ArrowAttackCool()
            {
                ArrowCooldown--;
                if (ArrowCooldown < 0)
                {
                    ArrowCooldown = 0;
                }
            }
            public int ArrowAttackCoolReturn()
            {
                return ArrowCooldown;
            }
            public int ArrowAttackReturn()
            {
                return Arrow;
            }
            ////////////////////////////////////
            ////////////////////////////////////////////////////////
            public void FireBallDo() //attack
            {
                
               // MonsterHealth.Randomizor();
                Randomizor();
                CurrentHP = CurrentHP - 100;
                //FireBallCooldown = 0;
                //////////////////////////////////////////////////COOL DOWN REDUCTIONS****** //HIGHLY PERSONAL YOUR COOLDOWN
              //  SlashAttackCool();
            //    ArrowAttackCool();
                //////////////////////////////////////////////////////////////////////////

            }
      //      public void FireBallCool() //PEROSNAL
        //    {
         //       FireBallCooldown--;
         //       if (FireBallCooldown < 0)
         //       {
        //            FireBallCooldown = 0;
       //         }
       //     }
      //      public int FireBallCoolReturn() //attack
     //       {
     //           return FireBallCooldown;
       //     }
            public int FireBallReturn() //attack
            {
                return FireBall;
            }

            public int Burny() // just an attack
            {

                return Burn;
            }
            public void FireBallBurnDo() //just an attack
            {
                
                CurrentHP = CurrentHP - Burn;


            }
            ////////////////////////////////////
            public void CounterAttackDo() //personal
            {
                Randomizor();
                newcounter = Counter * helpyyy;
                HunterCurrentHP = HunterCurrentHP - newcounter;

            }
            public int CounterAttackReturn() //personal
            {

                int value = (int)newcounter;
                int rounded = (int)newcounter;
                return rounded;
            }

            /// ///////////////////////////////////////////////////
            public double BlockandRestDo()  //personal
            {
                Randomizor();
               
                ReducedAttack = Armor * Counter;

                HunterCurrentHP = HunterCurrentHP - ReducedAttack;
                AllCoolDown();
                return 0;

            }
            public double BlockedAttackReturn() // personal
            {
                fix = ReducedAttack;
                int value = (int)fix;
                int rounded = (int)fix;
                return rounded;


            }
            public double BlockedPercentReturn() // personal
            {
                Percent = Counter / ReducedAttack;
                Percent = 100 / Percent;
                Percent = 100 - Percent;




                int value = (int)Percent;
                int rounded = (int)Percent;

                return rounded;


            }

        }
        public class MageHealth
        {
            public static int MagePoisonCooldown;
            private static int FireBall = 1;
            public static int FireBallCooldown; //PEROSNAL
            private static double fix = 1;                            //  static int BurnOrNot = 1;
            private static int hpPot = 1;
            private static double Armor = 1;
            private static double Percent = 1;
            public static int ArcaneCooldown; //PEROSNAL
            private static int Counter = 1;
            private static int Burn = 1;
            private static int Wack = 0;
            private static int WackBonus = 0;
            private static int WackBonusOrNot = 0;
            public static int WackCooldown = 0; //PEROSNAL
            private static int Arcane = 0;
           // private static int YourBaseHP = 500;
            private static int HealFactor = 1;
            public static int magepoison = 0;

            private static double ReducedAttack = 0;
            
            private static int YourBaseHP = 500 - 25 + (Everyone.MageLevel * 25) * MageOnly.MageHPPoint;
            public static double MageCurrentHP = YourBaseHP;
            private static int c = 1;
            private static int leveldamamp = MageOnly.MageDamagePoint;




            private static double ArmorBonus = MageOnly.MageArmorPoint * .015;
            private static double helpyyy = 1 - ArmorBonus;

            public static double newcounterm = Counter * helpyyy;



            /// ////////////////////////////////////////////////////////
            public void Randomizor()
            {
                //   travel = rand.Next(1, 100);
                hpPot = rand.Next(100, 150);
                Counter = rand.Next(-(BaseHP / 2), (BaseHP / 2));
                Counter = (BaseHP / 2) - Counter;

                Burn = rand.Next(1- c, 10 - c) + leveldamamp;
                Armor = rand.Next(25 , 75 );
                Arcane = rand.Next(3 - c, 6 - c) + leveldamamp;
                Wack = rand.Next(1 - c, 5 - c) + leveldamamp;
                WackBonusOrNot = rand.Next(1 , 10);
                WackBonus = rand.Next(5 - c, 10 - c) + leveldamamp;
                FireBall = rand.Next(15 - c, 25 - c) + leveldamamp;
                HealFactor = rand.Next(5 - c, 25 - c) + leveldamamp;//may get too strong
                magepoison = rand.Next(3 - c, 7 - c) + leveldamamp;
                //     Loot = rand.Next(1, 10);
                //   quickAttack = rand.Next(1, 10);
                //   qos = rand.Next(1, 10);

            }
            public void PoisonDo()
            {
                Randomizor();

                CurrentHP = CurrentHP - magepoison;
                MagePoisonCooldown = 5;
            }
            public void PoisonCool()
            {
                MagePoisonCooldown--;
                if (MagePoisonCooldown < 0)
                {
                    MagePoisonCooldown = 0;
                }
            }
            public double GetYourHealth() //perosnal
            {
                int value = (int)MageCurrentHP;
                int rounded = (int)MageCurrentHP;
                return rounded;
            }
            public double GetYourFullHealth() //persoanl
            {
                return YourBaseHP;
            }
          
            ////////////////////////////////////////////////////////
            public void AllCoolDown()
            {
                WackAttackCool();
                ArcaneAttackCool();
                FireBallCool();

            }
            public void HealthPotion()
            {
                Randomizor();
                MageCurrentHP = MageCurrentHP + hpPot;
                if (MageCurrentHP > YourBaseHP)
                {
                    MageCurrentHP = YourBaseHP;
                }

            }
            public int HealthPotionReturn()
            {
                return hpPot;

            }
            public void ArcaneAttackDo()
            {
                Randomizor();
                CurrentHP = CurrentHP - Arcane;
                ArcaneCooldown = 4;
                //////////////////////////////////////////////////COOL DOWN REDUCTIONS*****
                FireBallCool();
                WackAttackCool();
                //////////////////////////////////////////////////////////////////////////

            }
            public void ArcaneAttackCool()
            {
                ArcaneCooldown--;
                if (ArcaneCooldown < 0)
                {
                    ArcaneCooldown = 0;
                }
            }
            public int ArcaneAttackCoolReturn()
            {
                return ArcaneCooldown;
            }
            public int ArcaneAttackReturn()
            {
                return Arcane;
            }
            ////////////////////////////////////
            public void WackAttackDo()
            {
                Randomizor();
                CurrentHP = CurrentHP - Wack;
                WackCooldown = 1;
                //////////////////////////////////////////////////COOL DOWN REDUCTIONS******
                FireBallCool();
                ArcaneAttackCool();
                //////////////////////////////////////////////////////////////////////////

            }
            public void WackAttackCool()
            {
                WackCooldown--;
                if (WackCooldown < 0)
                {
                    WackCooldown = 0;
                }
            }
            public int WackAttackCoolReturn()
            {
                return WackCooldown;
            }
            public int WackAttackReturn()
            {
                return Wack;
            }
            public int WackBonusReturn()
            {
                return WackBonus;
            }
            public void WackBonusDo()
            {
                WackBonus = rand.Next(2, 5);
                CurrentHP = CurrentHP - WackBonus;
             

            }
            public int WackBonusOrNotReturn()
            {
                return WackBonusOrNot;
            }
            ////////////////////////////////////
            ////////////////////////////////////////////////////////
            public void FireBallDo() //attack
            {

                // MonsterHealth.Randomizor();
                Randomizor();
                CurrentHP = CurrentHP - FireBall;
                FireBallCooldown = 6;
                //////////////////////////////////////////////////COOL DOWN REDUCTIONS****** //HIGHLY PERSONAL YOUR COOLDOWN
                ArcaneAttackCool();
                WackAttackCool();
                //////////////////////////////////////////////////////////////////////////

            }
            public void FireBallCool() //PEROSNAL
            {
                FireBallCooldown--;
                if (FireBallCooldown < 0)
                {
                    FireBallCooldown = 0;
                }
            }
            public int FireBallCoolReturn() //attack
            {
                return FireBallCooldown;
            }
            public int FireBallReturn() //attack
            {
                return FireBall;
            }

            public int Burny() // just an attack
            {

                return Burn;
            }
            public void FireBallBurnDo() //just an attack
            {

                CurrentHP = CurrentHP - Burn;


            }
            ////////////////////////////////////
            public void CounterAttackDo() //personal
            {
                Randomizor();
                newcounterm = Counter * helpyyy;
                MageCurrentHP = MageCurrentHP - newcounterm;

            }
            public int CounterAttackReturn() //personal
            {
                int value = (int)newcounterm;
                int rounded = (int)newcounterm;
                return rounded;
            }
            public double BlockandRestDo()  //personal
            {
                Randomizor();
                Armor = Armor * .01;
                ReducedAttack = Armor * Counter;

                MageCurrentHP = MageCurrentHP - ReducedAttack;
                AllCoolDown();
                return 0;

            }
            public void forceheal()  //personal
            {


                MageCurrentHP += HealFactor;


            }
            public int forcehealreturn()  //personal
            {


                return HealFactor;


            }
            public double BlockedAttackReturn() // personal
            {
                fix = ReducedAttack;
                int value = (int)fix;
                int rounded = (int)fix;
                return rounded;


            }
            public double BlockedPercentReturn() // personal
            {
                Percent = Counter / ReducedAttack;
                Percent = 100 / Percent;
                Percent = 100 - Percent;




                int value = (int)Percent;
                int rounded = (int)Percent;

                return rounded;


            }


            /// ///////////////////////////////////////////////////


        }
    }
}
