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


    [Group("M")]
    public class MageOnly : ModuleBase<SocketCommandContext>
    {

        static int Cooldown = 0;

        static int EquipedCooldown = 0;
        static int OwnFireball = 0;
        static int fzfz = 0;
        public static int MageAlive = 0;
        //   private static int ShopInv = 0;

        public static int Potions = 3;
        public static int MageHPPoint = 1;
        public static int MageDamagePoint = 1;
        public static int MageArmorPoint = 1;
        public static int MagePoisonous = 0;
        private static int PotionsPoison = 0;
        private static int PotionsMana = 0;
        private static int CraftingMaterials = 5;


        Random rand = new Random();

        static string[] Creator = new string[] { "Wizard530" };

        static Everyone Role = new Everyone();
        Thing.MonsterHealth myMonster = new Thing.MonsterHealth();
        Thing.HunterHealth myHunter = new Thing.HunterHealth();
        Thing.MageHealth myMage = new Thing.MageHealth();
        // public static HunterOnly LOOTGIVER2 = new HunterOnly();
        EmbedBuilder builder = new EmbedBuilder();
        EmbedBuilder builder2 = new EmbedBuilder();
        EmbedBuilder builder3 = new EmbedBuilder();
        EmbedBuilder builder4 = new EmbedBuilder();
        EmbedBuilder builder5 = new EmbedBuilder();
        EmbedBuilder builder6 = new EmbedBuilder();
        [Command("Stat HP")]
        public async Task buypofewionAsync()
        {
            if (Everyone.MageStatPoint >= 1)
            {
                Everyone.MageStatPoint--;
                MageHPPoint++;
            }

        }
        [Command("Stat Damage")]
        public async Task buypodwfewionAsync()
        {
            if (Everyone.MageStatPoint >= 1)
            {
                Everyone.MageStatPoint--;
                MageDamagePoint++;
            }

        }
        [Command("Stat Armor")]
        public async Task buypowdwdfewionAsync()
        {
            if (Everyone.MageStatPoint >= 1)
            {
                Everyone.MageStatPoint--;
                MageArmorPoint++;
            }

        }
        [Command("inv")]
        public async Task invAsync()
        {
            builder.WithTitle("Mage Inventory:")
                 .AddField("Name:", Role.MageName())
                 .AddField("Health:", myMage.GetYourHealth() + "/" + myMage.GetYourFullHealth())
                   .WithColor(Color.Purple)
                      .AddField("Level:", Everyone.MageLevel)
             .AddField("EXP:", Everyone.MageEXP + "/" + Everyone.NextMageLevel)
                               .AddField("Gold:", Everyone.MageGold)
                              .AddField("Healing Potions:", Potions)
                              .AddField("Crafting Materials:", "0")
                               .AddField("Mage Abilites:", "Wack, MagicMissile, Meditate, Fireball")
                                 .AddField("Stat Points:", Everyone.MageStatPoint)
               .AddField("Points In HP:", MageHPPoint)
                 .AddField("Points In Damage:", MageDamagePoint)
                 .AddField("Points In Armor:", MageArmorPoint);

            await ReplyAsync("", false, builder.Build());

        }

        public async Task MonsterDied()
        {
            Everyone.FightingOrNot = 4;
            builder4.WithTitle("The Monster Has Been Defeated!")
             .AddField("-------------------------------", "Use '!GM Loot' To have all active party members Loot the monsters corpes!")
            .WithThumbnailUrl("https://i.imgur.com/9nMPrXq.png")
.WithColor(Color.Purple);
            await ReplyAsync("", false, builder4.Build());
            Everyone.MonsterDeathCounter++;
            Everyone.Debugger++;
            if (Everyone.MonsterDeathCounter == 25)
            {
                Everyone.DifficultyUp++;
                Everyone.MonsterDeathCounter = 0;
                //.Debugger2 ++;
            }
        }
        public async Task MonsterCounter()
        {
            if (HunterOnly.HunterPoisonous > 0)
            {
                myHunter.PoisonDo();
                HunterOnly.HunterPoisonous--;
                int hunterpoisoncount = 3 - HunterOnly.HunterPoisonous;

                builder3.WithTitle("The Hunters Poison Damages For " + Thing.HunterHealth.hunterpoison + " Damage")
                    .AddField("-------------------------------", hunterpoisoncount + " Out Of 3 Turns Of Poison Used")
                   .AddField("-------------------------------", "Monster Health: " + myMonster.GetHealth() + "/" + myMonster.GetfullHealth())
                   .WithThumbnailUrl("https://i.imgur.com/jD7lR8P.png")
                     .WithColor(Color.Green);

                await ReplyAsync("", false, builder3.Build());

            }
            if (MageOnly.MagePoisonous > 0)
            {
                myMage.PoisonDo();
                MageOnly.MagePoisonous--;
                int magepoisoncount = 3 - MageOnly.MagePoisonous;

                builder2.WithTitle("The Mages Poison Damages For " + Thing.MageHealth.magepoison + " Damage")
                    .AddField("-------------------------------", magepoisoncount + " Out Of 3 Turns Of Poison Used")
                   .AddField("-------------------------------", "Monster Health: " + myMonster.GetHealth() + "/" + myMonster.GetfullHealth())
                   .WithThumbnailUrl("https://i.imgur.com/jD7lR8P.png")
                     .WithColor(Color.Green);

                await ReplyAsync("", false, builder2.Build());

            }
            if (myMonster.GetHealth() >= 1)
            {
                await Task.Delay(10);
                myMage.CounterAttackDo();
                builder5.WithTitle("The Monster Attacks Dealing " + myMage.CounterAttackReturn() + " Damage")
                  //   builder5.WithTitle("The Monster Attacks Dealing " + Thing.MageHealth.newcounterm + " Damage")
              .AddField("-------------------------------", "Your Health: " + myMage.GetYourHealth() + "/" + myMage.GetYourFullHealth())
                .AddField("-------------------------------", Context.User.Mention)
      .WithColor(Color.Red);

                await ReplyAsync("", false, builder5.Build());
            }
            if (myMage.GetYourHealth() <= 0)
            {
                builder6.WithTitle("You Were Killed By The Monster!")
        .AddField("-------------------------------", "Wait For The Wizard Of Oz To Revive You!")
          .AddField("-------------------------------", Context.User.Mention)
.WithColor(Color.Red);

                await ReplyAsync("", false, builder6.Build());
                MageAlive = 1;
            }
        }


        [Command("buy HealingPotion")]
        public async Task buypotionAsync()
        {
            if (MageAlive == 0)
            {

                if (Everyone.MageGold >= 50)
                {
                    if (Role.MageName() == Context.User.Username || Context.User.Username == "Wizard530")
                    {
                        if (Everyone.FightingOrNot == 2)
                        {
                            builder.WithTitle("You Buy A Healing Potion")
                                .AddField("-------------------------------", "-50G")
                                  .AddField("-------------------------------", Context.User.Mention)
                                    .WithColor(Color.Purple)

                                .WithThumbnailUrl("https://i.imgur.com/Ehf7AVG.png");
                            await ReplyAsync("", false, builder.Build());
                            Potions += 1;
                            Everyone.MageGold -= 50;
                        }
                    }
                }
                else
                {

                    builder3.WithTitle("You Do Not Have Enough Money To Buy This Item!")
                          .AddField("-------------------------------", Context.User.Mention);
                    await ReplyAsync("", false, builder3.Build());
                }
            }
            else
            {
                builder3.WithTitle("You Cant Do That While Dead!")
                              .AddField("-------------------------------", Context.User.Mention);
                await ReplyAsync("", false, builder3.Build());
            }
        }
       
        [Command("healingpotion")]
        public async Task healthyAsync()
        {
            if (MageAlive == 0)
            {
                if (Role.MageName() == Context.User.Username || Context.User.Username == "Wizard530")
                {


                    if (Potions >= 1 && myMage.GetYourHealth() < myMage.GetYourFullHealth())
                    {
                        myMage.HealthPotion();
                        builder.WithTitle("You Use 1 Healing Potions... Healing yourself for " + myMage.HealthPotionReturn() + " Health")
                            .AddField("-------------------------------", "Your Health: " + myMage.GetYourHealth() + "/" + myMage.GetYourFullHealth())
                              .AddField("-------------------------------", Context.User.Mention)
                          .WithColor(Color.Green)
                      .WithThumbnailUrl("https://i.imgur.com/Ehf7AVG.png");
                        await ReplyAsync("", false, builder.Build());
                        Potions--;
                    }
                    else if (Potions <= 0 && myMage.GetYourHealth() < myMage.GetYourFullHealth())
                    {
                        builder.WithTitle("You have 0 Healing Potions... Find A Shop To Buy More!")
                          .AddField("-------------------------------", "Your Health: " + myMage.GetYourHealth() + "/" + myMage.GetYourFullHealth())
                            .AddField("-------------------------------", Context.User.Mention)
                        .WithColor(Color.Red)
                    .WithThumbnailUrl("https://i.imgur.com/Ehf7AVG.png");
                        await ReplyAsync("", false, builder.Build());
                        Potions = 0;
                    }
                    else
                    {
                        builder.WithTitle("You are already at full Health")
                         .AddField("-------------------------------", "Your Health: " + myMage.GetYourHealth() + "/" + myMage.GetYourFullHealth())
                           .AddField("-------------------------------", Context.User.Mention)
                       .WithColor(Color.Green)
                   .WithThumbnailUrl("https://i.imgur.com/Ehf7AVG.png");
                        await ReplyAsync("", false, builder.Build());

                    }
                }
                else
                {
                    builder3.WithTitle("You are not in this role")
                          .AddField("-------------------------------", Context.User.Mention);
                    await ReplyAsync("", false, builder3.Build());
                }
            }
            else
            {
                builder3.WithTitle("You Cant Do That While Dead!")
                              .AddField("-------------------------------", Context.User.Mention);
                await ReplyAsync("", false, builder3.Build());
            }
        }

        ///////////////////////////////////


        //COMMAND Attack//////////////////////
        [Command("Wack")]
        public async Task AttackAsync()
        {
            if (MageAlive == 0)
            {

                if (Role.MageName() == Context.User.Username || Context.User.Username == "Wizard530")
                {
                    Everyone.MageNotAFK = 1;
                    if (Everyone.FightingOrNot == 0 && myMage.WackAttackCoolReturn() == 0)
                    {

                        myMage.WackAttackDo();
                        if (myMage.WackBonusOrNotReturn() >= 7)
                        {

                            myMage.WackBonusDo();
                            builder.WithTitle("You Bravely... Slap The Monster With your Staff Dealing " + myMage.WackAttackReturn() + " Damage")

                           .AddField("-------------------------------", "You Get Fancy And Decide Cast A Spell After Wacking The Monster Dealing " + myMage.WackBonusReturn() + " Damage")
                          .AddField("-------------------------------", "Monster Health: " + myMonster.GetHealth() + " / " + myMonster.GetfullHealth())
                            .AddField("-------------------------------", Context.User.Mention)

                  .WithColor(Color.Green)
                       .WithThumbnailUrl("https://i.imgur.com/3wAGG9d.png");
                            await ReplyAsync("", false, builder.Build());
                            if (myMonster.GetHealth() <= 0)
                            {
                                await MonsterDied();
                            }
                            await MonsterCounter();

                        }
                        else if (myMage.WackBonusOrNotReturn() < 7)
                        {
                            builder.WithTitle("You Akwardly Slap The Monster With your Staff Dealing " + myMage.WackAttackReturn() + " Damage")
                                                  .AddField("-------------------------------", "Monster Health: " + myMonster.GetHealth() + " / " + myMonster.GetfullHealth())
                                                    .AddField("-------------------------------", Context.User.Mention)
                                          .WithColor(Color.Green)
                                               .WithThumbnailUrl("https://i.imgur.com/3wAGG9d.png");
                            await ReplyAsync("", false, builder.Build());
                            if (myMonster.GetHealth() <= 0)
                            {
                                await MonsterDied();
                            }
                            await MonsterCounter();

                        }


                    }
                    else if (Everyone.FightingOrNot == 0 && myMage.WackAttackCoolReturn() >= 1)
                    {
                        builder2.WithTitle("Wack is on cooldown for " + myMage.WackAttackCoolReturn() + " turn")
                              .AddField("-------------------------------", Context.User.Mention);
                        await ReplyAsync("", false, builder2.Build());

                    }


                    else
                    {
                        builder3.WithTitle("You cannot do that while out of combat.")
                              .AddField("-------------------------------", Context.User.Mention);
                        await ReplyAsync("", false, builder3.Build());

                    }
                }
                else
                {
                    builder3.WithTitle("You are not in this role");
                    await ReplyAsync("", false, builder3.Build());
                }
            }
            else
            {
                builder3.WithTitle("You Cant Do That While Dead!")
                              .AddField("-------------------------------", Context.User.Mention);
                await ReplyAsync("", false, builder3.Build());
            }
        }
        [Command("MagicMissile")]
        public async Task ArrowAttackAsync()
        {
            if (MageAlive == 0)
            {

                if (Role.MageName() == Context.User.Username || Context.User.Username == "Wizard530")
                {
                    Everyone.MageNotAFK = 1;

                    if (Everyone.FightingOrNot == 0 && myMage.ArcaneAttackCoolReturn() == 0)
                    {
                        myMage.ArcaneAttackDo();
                        int first = myMage.ArcaneAttackReturn();
                        myMage.ArcaneAttackDo();
                        int Second = myMage.ArcaneAttackReturn();
                        myMage.ArcaneAttackDo();
                        int Third = myMage.ArcaneAttackReturn();
                        builder.WithTitle("You Shoot 3 Magic Missles At Your Target")
                             .AddField("-------------------------------", "The 1st Shot Does " + first + " Damage")
                              .AddField("-------------------------------", "The 2nd Shot Does " + Second + " Damage")
                               .AddField("-------------------------------", "The 3rd Shot Does " + Third + " Damage")
                         .AddField("-------------------------------", "Monster Health: " + myMonster.GetHealth() + " / " + myMonster.GetfullHealth())
                           .AddField("-------------------------------", Context.User.Mention)
                 .WithColor(Color.Green)
                      .WithThumbnailUrl("https://i.imgur.com/aWJ0cn0.png");
                        await ReplyAsync("", false, builder.Build());




                        if (myMonster.GetHealth() <= 0)
                        {
                            await MonsterDied();
                        }
                        await MonsterCounter();


                    }
                    else if (Everyone.FightingOrNot == 0 && myMage.ArcaneAttackCoolReturn() >= 1)
                    {
                        builder2.WithTitle("MagicMissile is on cooldown for " + myMage.ArcaneAttackCoolReturn() + " turn")
                              .AddField("-------------------------------", Context.User.Mention);
                        await ReplyAsync("", false, builder2.Build());


                    }

                    else
                    {
                        builder3.WithTitle("You cannot do that while out of combat.")
                              .AddField("-------------------------------", Context.User.Mention);
                        await ReplyAsync("", false, builder3.Build());

                    }
                }
                else
                {
                    builder3.WithTitle("You are not in this role")
                          .AddField("-------------------------------", Context.User.Mention);
                    await ReplyAsync("", false, builder3.Build());
                }
            }
            else
            {
                builder3.WithTitle("You Cant Do That While Dead!")
                              .AddField("-------------------------------", Context.User.Mention);
                await ReplyAsync("", false, builder3.Build());
            }
        }
        //COMMAND Attack//////////////////////
        [Command("Fireball")]
        public async Task FireballAsync()
        {
            if (MageAlive == 0)
            {


                if (Role.MageName() == Context.User.Username || Context.User.Username == "Wizard530")
                {
                    Everyone.MageNotAFK = 1;
                    if (Everyone.FightingOrNot == 0 && myMage.FireBallCoolReturn() == 0)
                    {
                        myMage.FireBallDo(); //does attack and puts attack on cooldown
                        fzfz = rand.Next(1, 10);

                        if (fzfz > 5)
                        {

                            if (myMage.FireBallReturn() >= 20)
                            {
                                myMage.FireBallBurnDo();
                                builder.WithTitle("You Cast A Powerful Fireball Dealing " + myMage.FireBallReturn() + " Damage")

                                .AddField("-------------------------------", "You Burn Your Target For an additional " + myMage.Burny() + " Damage")
                                     .AddField("-------------------------------", "Monster Health: " + myMonster.GetHealth() + " / " + myMonster.GetfullHealth())
                                       .AddField("-------------------------------", Context.User.Mention)
                .WithColor(Color.Green)
                     .WithThumbnailUrl("https://i.imgur.com/RFPgCPK.png");
                                await ReplyAsync("", false, builder.Build());
                                if (myMonster.GetHealth() <= 0)
                                {
                                    await MonsterDied();
                                }
                                await MonsterCounter();

                            }
                            else if (myMage.FireBallReturn() < 20)
                            {

                                builder.WithTitle("You Cast A Powerful Fireball Dealing " + myMage.FireBallReturn() + " Damage")
                                     .AddField("-------------------------------", "Monster Health: " + myMonster.GetHealth() + " / " + myMonster.GetfullHealth())
                                       .AddField("-------------------------------", Context.User.Mention)
                .WithColor(Color.Green)
                     .WithThumbnailUrl("https://i.imgur.com/RFPgCPK.png");
                                await ReplyAsync("", false, builder.Build());
                                if (myMonster.GetHealth() <= 0)
                                {
                                    await MonsterDied();
                                }
                                await MonsterCounter();
                            }
                        }
                        else if (fzfz <= 5)
                        {
                            builder3.WithTitle("You Are Interupted While Casting Fireball...")
                                 .WithColor(Color.Red)
                     .WithThumbnailUrl("https://i.imgur.com/RFPgCPK.png");
                            await ReplyAsync("", false, builder3.Build());

                            await MonsterCounter();

                        }

                    }
                    else if (Everyone.FightingOrNot == 0 && myMage.FireBallCoolReturn() >= 1)
                    {
                        builder3.WithTitle("Fireball is on cooldown for " + myMage.FireBallCoolReturn() + " turn")
                              .AddField("-------------------------------", Context.User.Mention);
                        await ReplyAsync("", false, builder3.Build());


                    }
                    else
                    {
                        builder3.WithTitle("You cannot do that while out of combat.")
                              .AddField("-------------------------------", Context.User.Mention);
                        await ReplyAsync("", false, builder3.Build());

                    }
                }
                else
                {
                    builder3.WithTitle("You do not own this spell!")
                          .AddField("-------------------------------", Context.User.Mention);
                    await ReplyAsync("", false, builder3.Build());

                }
            }
            else
            {
                builder3.WithTitle("You Cant Do That While Dead!")
                              .AddField("-------------------------------", Context.User.Mention);
                await ReplyAsync("", false, builder3.Build());
            }

        }
        [Command("Meditate")]
        public async Task BlockAsync()
        {

            if (MageAlive == 0)
            {

                if (Role.MageName() == Context.User.Username || Context.User.Username == "Wizard530")
                {
                    Everyone.MageNotAFK = 1;
                    if (Everyone.FightingOrNot == 0 && myMage.WackAttackCoolReturn() != 0) //COME HERE WHEN ADDING NEW ATTACKS (not neededd anymore)
                    {
                        myMage.BlockandRestDo();
                        myMage.forceheal();
                        builder.WithTitle("You Conjure a Barriar Around Your Body To Block The Monsters Attack!")
                             .AddField("-------------------------------", "All Cooldowns Reduced By 1!")
                            .AddField("-------------------------------", "You Take " + myMage.BlockedPercentReturn() + " % Less Damage")
                              .AddField("-------------------------------", "You Take Roughly " + myMage.BlockedAttackReturn() + " Damage")
                               .AddField("-------------------------------", "The Barrier Heals Your For " + myMage.forcehealreturn() + " Then Dissipate!")

                                   .AddField("-------------------------------", "Your Health: " + myMage.GetYourHealth() + "/" + myMage.GetYourFullHealth())
                                     .AddField("-------------------------------", Context.User.Mention)
              //  .AddInlineField("-------------------------------", "Debugger: " + myMonster.CounterAttackReturn())
              .WithColor(Color.Blue)
                   .WithThumbnailUrl("https://i.imgur.com/Y8izDYJ.png");
                        await ReplyAsync("", false, builder.Build());
                        if (myMage.GetYourHealth() <= 0)
                        {
                            builder2.WithTitle("You Were Killed By The Monster!")

                    .AddField("-------------------------------", "Wait For The Wizard Of Oz To Revive You!")
                      .AddField("-------------------------------", Context.User.Mention)
            .WithColor(Color.DarkRed);

                            await ReplyAsync("", false, builder2.Build());
                            MageAlive = 1;
                        }


                    }
                    else if (Everyone.FightingOrNot == 0 && myMage.WackAttackCoolReturn() == 0)
                    {
                        builder3.WithTitle("You Are Able To Attack This Turn!")
                              .AddField("-------------------------------", Context.User.Mention);
                        await ReplyAsync("", false, builder3.Build());

                    }

                    else
                    {
                        builder3.WithTitle("You cannot do that while out of combat.")
                              .AddField("-------------------------------", Context.User.Mention);
                        await ReplyAsync("", false, builder3.Build());
                    }

                }
            }
            else
            {
                builder3.WithTitle("You Cant Do That While Dead!")
                              .AddField("-------------------------------", Context.User.Mention);
                await ReplyAsync("", false, builder3.Build());
            }
        }
   /*    [Command("PoisonThrow")]
        public async Task poisonyAsync()
        {
           
            if (Role.MageName() == Context.User.Username || Context.User.Username == "Wizard530")
            {
               if (Everyone.FightingOrNot == 0)
                {
                   Everyone.MageNotAFK = 1;
                  
                       if (Thing.MageHealth.MagePoisonCooldown <= 0)
                        {
                          if (PotionsPoison >= 1)
                            {
                                PotionsPoison--;
                                builder3.WithTitle("Your Throw A Potion Of Poison At The Monster")
                                .WithColor(Color.Green)
                              .WithThumbnailUrl("https://i.imgur.com/jD7lR8P.png")


                                                     .AddField("-------------------------------", Context.User.Mention);
                            await ReplyAsync("", false, builder3.Build());
                            MagePoisonous = 3;
                        }
                        else if (PotionsPoison <= 0)
                        {
                            builder2.WithTitle("You have 0 Potions Poison!")
                                      .AddField("-------------------------------", Context.User.Mention);
                            await ReplyAsync("", false, builder2.Build());

                        }
                    }
                        else if (Thing.MageHealth.MagePoisonCooldown >= 1)
                        {
                            builder2.WithTitle("Poison Throw is on cooldown for " + Thing.MageHealth.MagePoisonCooldown + " turns")
                                   .AddField("-------------------------------", Context.User.Mention);
                            await ReplyAsync("", false, builder2.Build());
                        }
                   
                }
                else
                {
                    builder3.WithTitle("You cannot do that while out of combat.")
                        .AddField("-------------------------------", Context.User.Mention);
                    await ReplyAsync("", false, builder3.Build());
                }
            }
            else
            {
                builder3.WithTitle("You are not in this role")
                       .AddField("-------------------------------", Context.User.Mention);
                await ReplyAsync("", false, builder3.Build());

            }
        } */
    } 
}
