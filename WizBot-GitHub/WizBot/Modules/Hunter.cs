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

    [Group("H")]
    public class HunterOnly : ModuleBase<SocketCommandContext>
    {

       private static int QuickOrSlow = 0;
        private static int Cooldown = 0;
        private static int Stamina = 0;
        private static int EquipedWeapon = 0;
        private static int EquipedCooldown = 0;
        private static int OwnFireball = 0;
        public static int HunterAlive = 0;
        public static int HunterPoisonous = 0;
        public static int HunterHPPoint = 1;
        public static int HunterDamagePoint = 1;
        public static int HunterArmorPoint = 1;
        public static int agile = 0;
        private static int CraftingMaterials = 5;
        private static int Potions = 3;
        private static int PotionsPoison = 1;

        Random rand = new Random();

        static string[] Creator = new string[] { "Wizard530" };

        static Everyone Role = new Everyone();
        Thing.MonsterHealth myMonster = new Thing.MonsterHealth();
        Thing.HunterHealth myHunter = new Thing.HunterHealth();
        Thing.MageHealth myMage = new Thing.MageHealth();
        //  public static  MageOnly LOOTGIVER = new MageOnly();
        EmbedBuilder builder = new EmbedBuilder();
        EmbedBuilder builder2 = new EmbedBuilder();
        EmbedBuilder builder3 = new EmbedBuilder();
        EmbedBuilder builder4 = new EmbedBuilder();
        EmbedBuilder builder5 = new EmbedBuilder();
        EmbedBuilder builder6 = new EmbedBuilder();
        EmbedBuilder builder7 = new EmbedBuilder();
        EmbedBuilder builder8 = new EmbedBuilder();
        [Command("Stat HP")]
        public async Task buypofewionAsync()
        {
            if (Everyone.HunterStatPoint >= 1)
            {
                Everyone.HunterStatPoint--;
                HunterHPPoint++;
            }

        }
        [Command("Stat Damage")]
        public async Task buypodwfewionAsync()
        {
            if (Everyone.HunterStatPoint >= 1)
            {
                Everyone.HunterStatPoint--;
                HunterDamagePoint++;
            }

        }
        [Command("Stat Armor")]
        public async Task buypowdwdfewionAsync()
        {
            if (Everyone.HunterStatPoint >= 1)
            {
                Everyone.HunterStatPoint--;
                HunterArmorPoint++;
            }

        }
        [Command("inv")]
        public async Task invAsync()
        {
            builder.WithTitle("Hunter Inventory:")
                 .AddField("Name:", Role.HunterName())
                 .AddField("Health:", myHunter.GetYourHealth() + "/" + myHunter.GetYourFullHealth())
                   .WithColor(Color.Purple)
                      .AddField("Level:", Everyone.HunterLevel)
             .AddField("EXP:", Everyone.HunterEXP + "/" + Everyone.NextHunterLevel)
                               .AddField("Gold:", Everyone.HunterGold)
                              .AddField("Healing Potions:", Potions)
                              .AddField("Crafting Materials:", "0")
                               .AddField("Hunter Abilites:", "Bow, Shoot, Dagger, Stab, PoisonTip")
              .AddField("Stat Points:", Everyone.HunterStatPoint)
               .AddField("Points In HP:", HunterHPPoint)
                 .AddField("Points In Damage:", HunterDamagePoint)
                 .AddField("Points In Armor:", HunterArmorPoint);



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
                // Everyone.Debugger2 ++;
            }
        }

        public async Task MonsterCounter()
        {
            if(agile >=1)
            {
                agile--;
                builder5.WithTitle("You Dodge The Monsters Attack With Your Agility")
                       .WithThumbnailUrl("https://i.imgur.com/7O224Fo.png")
                     .WithColor(Color.Blue);
                await ReplyAsync("", false, builder5.Build());
            }
            if (HunterOnly.HunterPoisonous > 0)
            {
                myHunter.PoisonDo();
                HunterOnly.HunterPoisonous--;
                int hunterpoisoncount = 3 - HunterOnly.HunterPoisonous;

                builder7.WithTitle("The Hunters Poison Damages For " + Thing.HunterHealth.hunterpoison + " Damage")
                    .AddField("-------------------------------", hunterpoisoncount + " Out Of 3 Turns Of Poison Used")
                   .AddField("-------------------------------", "Monster Health: " + myMonster.GetHealth() + "/" + myMonster.GetfullHealth())
                   .WithThumbnailUrl("https://i.imgur.com/jD7lR8P.png")
                     .WithColor(Color.Green);

                await ReplyAsync("", false, builder7.Build());

            }
            else if (agile <=0)
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
                    myHunter.CounterAttackDo();
                    builder8.WithTitle("The Monster Attacks Dealing " + myHunter.CounterAttackReturn() + " Damage")
                  // builder5.WithTitle("The Monster Attacks Dealing " + Thing.HunterHealth. + " Damage")
                  .AddField("-------------------------------", "Your Health: " + myHunter.GetYourHealth() + "/" + myHunter.GetYourFullHealth())
                    .AddField("-------------------------------", Context.User.Mention)
          .WithColor(Color.Red);

                    await ReplyAsync("", false, builder8.Build());
                    if (myHunter.GetYourHealth() <= 0)
                    {
                        builder6.WithTitle("You Were Killed By The Monster!")

                .AddField("-------------------------------", "Wait For The Wizard Of Oz To Revive You!")
                  .AddField("-------------------------------", Context.User.Mention)
        .WithColor(Color.DarkRed);

                        await ReplyAsync("", false, builder6.Build());
                        HunterAlive = 1;
                    }
                }
            }
        }


        [Command("buy HealingPotion")]
        public async Task buypotionAsync()
        {
            if (HunterAlive == 0)
            {
                if (Everyone.HunterGold >= 50)
                {
                    if (Role.HunterName() == Context.User.Username || Context.User.Username == "Wizard530")
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
                            Everyone.HunterGold -= 50;
                        }
                    }
                }
                else
                {

                    builder3.WithTitle("You Do Not Have Enough Money To Buy This Item!");
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

        [Command("buy PoisonPotion")]
        public async Task buypotffionAsync()
        {
            if (HunterAlive == 0)
            {
                if (Everyone.HunterGold >= 50)
                {
                    if (Role.HunterName() == Context.User.Username || Context.User.Username == "Wizard530")
                    {
                        if (Everyone.FightingOrNot == 2 && Everyone.ShopInv ==1)
                        {
                            builder.WithTitle("You Buy A Potion Of Poison")
                                .AddField("-------------------------------", "-50G")
                                  .AddField("-------------------------------", Context.User.Mention)
                                    .WithColor(Color.Purple)

                                .WithThumbnailUrl("https://i.imgur.com/jD7lR8P.png");
                            await ReplyAsync("", false, builder.Build());
                            PotionsPoison += 1;
                            Everyone.HunterGold -= 50;
                        }
                    }
                }
                else
                {

                    builder3.WithTitle("You Do Not Have Enough Money To Buy This Item!");
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
            if (HunterAlive == 0)
            {
                if (Role.HunterName() == Context.User.Username || Context.User.Username == "Wizard530")
                {


                    if (Potions >= 1 && myHunter.GetYourHealth() < myHunter.GetYourFullHealth())
                    {
                        myHunter.HealthPotion();
                        builder.WithTitle("You Use 1 Healing Potions... Healing yourself for " + myHunter.HealthPotionReturn() + " Health")
                            .AddField("-------------------------------", "Your Health: " + myHunter.GetYourHealth() + "/" + myHunter.GetYourFullHealth())
                              .AddField("-------------------------------", Context.User.Mention)
                          .WithColor(Color.Green)
                      .WithThumbnailUrl("https://i.imgur.com/Ehf7AVG.png");
                        await ReplyAsync("", false, builder.Build());
                        Potions--;
                    }
                    else if (Potions <= 0 && myHunter.GetYourHealth() < myHunter.GetYourFullHealth())
                    {
                        builder.WithTitle("You have 0 Healing Potions... Find A Shop To Buy More!")
                          .AddField("-------------------------------", "Your Health: " + myHunter.GetYourHealth() + "/" + myHunter.GetYourFullHealth())
                            .AddField("-------------------------------", Context.User.Mention)
                        .WithColor(Color.Red)
                    .WithThumbnailUrl("https://i.imgur.com/Ehf7AVG.png");
                        await ReplyAsync("", false, builder.Build());
                        Potions = 0;
                    }
                    else
                    {
                        builder.WithTitle("You are already at full Health")
                         .AddField("-------------------------------", "Your Health: " + myHunter.GetYourHealth() + "/" + myHunter.GetYourFullHealth())
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
        [Command("Dagger")]
        public async Task PingAsync()
        {
            if (HunterAlive == 0)
            {

                if (Role.HunterName() == Context.User.Username || Context.User.Username == "Wizard530")
                {
                    Everyone.HunterNotAFK = 1;
                    myMonster.Randomizor();


                    if (Everyone.FightingOrNot == 0 && myHunter.QuickOrSlow() <= 3 && EquipedWeapon != 0)
                    {
                        //  EquipedWeapon = 0;
                        myHunter.AllCoolDown();

                        builder3.WithTitle("You Fail To Equip Your Dagger")
                             .WithColor(Color.Red)
                                         .AddField("-------------------------------", "While Equiping You Weapon The Monster Attacks You Dealing " + myHunter.CounterAttackReturn() + " Damage")
                                        .AddField("-------------------------------", "Your Health: " + myHunter.GetYourHealth() + "/" + myHunter.GetYourFullHealth())
                                          .AddField("-------------------------------", Context.User.Mention)
                                        .WithThumbnailUrl("https://i.imgur.com/xIVTYkQ.png");
                        await ReplyAsync("", false, builder3.Build());
                        await MonsterCounter();
                    }
                    else if (Everyone.FightingOrNot == 0 && myHunter.QuickOrSlow() > 3 && EquipedWeapon != 0 && HunterAlive == 0)
                    {
                        EquipedWeapon = 0;
                        myHunter.AllCoolDown();
                        myHunter.QuickAttack();
                        builder.WithTitle("You Are Quick To Draw Your Weapon")
                            .WithColor(Color.Green)
                            .AddField("-------------------------------", "You Draw Your Weapon And Do A Quick Basic Attack For " + myHunter.QuickAttackReturn() + " Damage")
                        .AddField("-------------------------------", "Monster Health: " + myMonster.GetHealth() + "/" + myMonster.GetfullHealth())
                          .AddField("-------------------------------", Context.User.Mention)
                         .WithThumbnailUrl("https://i.imgur.com/xIVTYkQ.png");

                        await ReplyAsync("", false, builder.Build());
                        if (myMonster.GetHealth() <= 0)
                        {
                            await MonsterDied();
                        }

                    }
                    else if (EquipedWeapon == 0)
                    {
                        builder3.WithTitle("Your Weapon Is Already Equiped!")
                              .AddField("-------------------------------", Context.User.Mention)

                      .WithThumbnailUrl("https://i.imgur.com/xIVTYkQ.png");
                        await ReplyAsync("", false, builder3.Build());

                    }
                    else
                    {
                        builder3.WithTitle("You Equip Your Dagger!")
                              .AddField("-------------------------------", Context.User.Mention)

                  .WithThumbnailUrl("https://i.imgur.com/xIVTYkQ.png");
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
        ////COMMAND SWord
        [Command("bow")]
        public async Task bowAsync()
        {
            if (HunterAlive == 0)
            {

                if (Role.HunterName() == Context.User.Username || Context.User.Username == "Wizard530")
                {
                    Everyone.HunterNotAFK = 1;
                    myMonster.Randomizor();
                    if (Everyone.FightingOrNot == 0 && myHunter.QuickOrSlow() <= 3 && EquipedWeapon != 1)
                    {
                        //  EquipedWeapon = 1;
                        myHunter.AllCoolDown();

                        builder3.WithTitle("You Fail To Equip Your Bow")
                            .WithColor(Color.Red)
                          .AddField("-------------------------------", "While Equiping You Weapon The Monster Attacks You Dealing " + myHunter.CounterAttackReturn() + " Damage")
                         .AddField("-------------------------------", "Your Health: " + myHunter.GetYourHealth() + "/" + myHunter.GetYourFullHealth())
                           .AddField("-------------------------------", Context.User.Mention)
                         .WithThumbnailUrl("https://i.imgur.com/EcDASPk.png");
                        await ReplyAsync("", false, builder3.Build());
                        await MonsterCounter();

                    }
                    else if (Everyone.FightingOrNot == 0 && myHunter.QuickOrSlow() > 3 && EquipedWeapon != 1)
                    {
                        EquipedWeapon = 1;
                        myHunter.AllCoolDown();
                        myHunter.QuickAttack();
                        builder.WithTitle("You Are Quick To Draw Your Weapon")
                            .WithColor(Color.Green)
                            .AddField("-------------------------------", "You Draw Your Weapon And Do A Quick Basic Attack For " + myHunter.QuickAttackReturn() + " Damage")
                        .AddField("-------------------------------", "Monster Health: " + myMonster.GetHealth() + "/" + myMonster.GetfullHealth())
                          .AddField("-------------------------------", Context.User.Mention)
                        .WithThumbnailUrl("https://i.imgur.com/EcDASPk.png");
                        await ReplyAsync("", false, builder.Build());
                        if (myMonster.GetHealth() <= 0)
                        {
                            await MonsterDied();
                        }

                    }
                    else if (EquipedWeapon == 1)
                    {
                        builder3.WithTitle("Your Weapon Is Already Equiped!")
                              .AddField("-------------------------------", Context.User.Mention)

                      .WithThumbnailUrl("https://i.imgur.com/EcDASPk.png");
                        await ReplyAsync("", false, builder3.Build());

                    }
                    else
                    {
                        builder3.WithTitle("You Equip Your Bow!")


                             .WithThumbnailUrl("https://i.imgur.com/EcDASPk.png");
                        await ReplyAsync("", false, builder3.Build());
                        EquipedWeapon = 1;
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
        [Command("Stab")]
        public async Task AttackAsync()
        {
            if (HunterAlive == 0)
            {

                if (Role.HunterName() == Context.User.Username || Context.User.Username == "Wizard530")
                {
                    Everyone.HunterNotAFK = 1;

                    if (Everyone.FightingOrNot == 0 && myHunter.SlashAttackCoolReturn() == 0 && EquipedWeapon == 0)
                    {


                        myHunter.SlashAttackDo(); //puts attack on cooldown
                        builder.WithTitle("You Do A Basic Stab Attack Dealing " + myHunter.SlashAttackReturn() + " Damage")
                           .AddField("-------------------------------", "Monster Health: " + myMonster.GetHealth() + " / " + myMonster.GetfullHealth())
                             .AddField("-------------------------------", Context.User.Mention)
                   .WithColor(Color.Green)
                      //  .WithThumbnailUrl("https://i.imgur.com/IXxts5s.png"); SWORD PNG/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                      .WithThumbnailUrl("https://i.imgur.com/xIVTYkQ.png");
                        await ReplyAsync("", false, builder.Build());

                        if (myMonster.GetHealth() <= 0)
                        {
                            await MonsterDied();
                        }
                        await MonsterCounter();





                    }
                    else if (Everyone.FightingOrNot == 0 && myHunter.SlashAttackCoolReturn() >= 1 && EquipedWeapon == 0)
                    {
                        builder2.WithTitle("Stab is on cooldown for " + myHunter.SlashAttackCoolReturn() + " turn")
                              .AddField("-------------------------------", Context.User.Mention);
                        await ReplyAsync("", false, builder2.Build());

                    }
                    else if (Everyone.FightingOrNot == 0 && myHunter.SlashAttackCoolReturn() <= 0 && EquipedWeapon != 0)
                    {
                        builder2.WithTitle("You Do Not Have Melee Weapon Equiped!")
                         .AddField("-------------------------------", "use !dagger to spend 1 turn taking out your Dagger")
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
        [Command("Shoot")]
        public async Task ArrowAttackAsync()
        {
            if (HunterAlive == 0)
            {


                if (Role.HunterName() == Context.User.Username || Context.User.Username == "Wizard530")
                {
                    Everyone.HunterNotAFK = 1;

                    if (Everyone.FightingOrNot == 0 && myHunter.ArrowAttackCoolReturn() == 0 && EquipedWeapon == 1)
                    {

                        myHunter.ArrowAttackDo(); //puts attack on cooldown
                        builder.WithTitle("You Do A Basic Arrow Shoot Attack Dealing " + myHunter.ArrowAttackReturn() + " Damage")
                         .AddField("-------------------------------", "Monster Health: " + myMonster.GetHealth() + " / " + myMonster.GetfullHealth())
                           .AddField("-------------------------------", Context.User.Mention)
                 .WithColor(Color.Green)
                      .WithThumbnailUrl("https://i.imgur.com/EcDASPk.png");
                        await ReplyAsync("", false, builder.Build());


                        if (myMonster.GetHealth() <= 0)
                        {
                            await MonsterDied();
                        }
                        await MonsterCounter();





                    }
                    else if (Everyone.FightingOrNot == 0 && myHunter.ArrowAttackCoolReturn() >= 1 && EquipedWeapon == 1)
                    {
                        builder2.WithTitle("Shoot is on cooldown for " + myHunter.ArrowAttackCoolReturn() + " turn");
                        await ReplyAsync("", false, builder2.Build());


                    }
                    else if (Everyone.FightingOrNot == 0 && myHunter.ArrowAttackCoolReturn() <= 0 && EquipedWeapon != 1)
                    {

                        builder2.WithTitle("You Do Not Have Ranged Weapon Equiped!")
                        .AddField("-------------------------------", "use !bow to spend 1 turn taking out your Bow")
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
        [Command("cheat")]
        public async Task FireballAsync()
        {

            if (Context.User.Username == "Wizard530")
            {

                if (Everyone.FightingOrNot == 0)
                {
                    Everyone.HunterNotAFK = 1;
                    myHunter.FireBallDo(); //does attack and puts attack on cooldown




                    if (myHunter.FireBallReturn() >= 15)
                    {
                        myHunter.FireBallBurnDo();
                        builder.WithTitle("You Cast A Powerful Fireball Dealing 100 Damage")

                        .AddField("-------------------------------", "You Burn Your Target For an additional " + myHunter.Burny() + " Damage")
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
                    else if (myHunter.FireBallReturn() < 15)
                    {

                        builder.WithTitle("You Cast A Powerful Fireball Dealing 100 Damage")
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
                else if (Everyone.FightingOrNot == 0 )
                {
                    builder3.WithTitle("Fireball is on cooldown for turn")
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
            else if (OwnFireball != 1)
            {
                builder3.WithTitle("You do not own this spell!")
                      .AddField("-------------------------------", Context.User.Mention);
                await ReplyAsync("", false, builder3.Build());

            }
            else
            {
                builder3.WithTitle("YOU WOT MATE!?")
                      .AddField("-------------------------------", Context.User.Mention);
                await ReplyAsync("", false, builder3.Build());
            }


        }
        [Command("PoisonTip")]
        public async Task poisonyAsync()
        {
            if (Role.HunterName() == Context.User.Username || Context.User.Username == "Wizard530")
            {
                if (Everyone.FightingOrNot == 0)
                {
                    Everyone.HunterNotAFK = 1;
                    if (Thing.HunterHealth.HunterPoisonCooldown <= 0)
                    {
                        if(PotionsPoison>=1)
                        {
                            PotionsPoison--;
                        builder3.WithTitle("You lace your weapons with a potient poison increasing your damage")
                            .WithColor(Color.Green)
                          .WithThumbnailUrl("https://i.imgur.com/jD7lR8P.png")


                                                 .AddField("-------------------------------", Context.User.Mention);
                        await ReplyAsync("", false, builder3.Build());
                        HunterPoisonous = 3;
                        }
                        else if (PotionsPoison <= 0)
                        {
                            builder2.WithTitle("You have 0 Turns of Poison remaining!")
                                      .AddField("-------------------------------", Context.User.Mention);
                            await ReplyAsync("", false, builder2.Build());

                        }
                    }
                    else if (Thing.HunterHealth.HunterPoisonCooldown >= 1)
                    {
                        builder2.WithTitle("PoisonTip is on cooldown for " + Thing.HunterHealth.HunterPoisonCooldown + " turns")
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
        }
        [Command("Agile")]
        public async Task AttadwdckAsync()
        {
            if (HunterAlive == 0)
            {

                if (Role.HunterName() == Context.User.Username || Context.User.Username == "Wizard530")
                {
                    Everyone.HunterNotAFK = 1;

                    if (Everyone.FightingOrNot == 0  && myHunter.AgileCoolReturn()<=0)
                    {


                       
                        builder.WithTitle("You Become Highly Agile")
                        
                             .AddField("-------------------------------", Context.User.Mention)
                   .WithColor(Color.Green)
                    
                      .WithThumbnailUrl("https://i.imgur.com/7O224Fo.png");
                        await ReplyAsync("", false, builder.Build());
                        agile = 3;
                        myHunter.AgileDo();
                     





                    }
                    else if (Everyone.FightingOrNot == 0 && myHunter.AgileCoolReturn() >= 1)
                    {
                        builder2.WithTitle("Agile is on cooldown for " + myHunter.AgileCoolReturn() + " turns")
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

        /*      [Command("Block")]
              public async Task BlockAsync()
              {
                  if (HunterAlive == 0)
                  {


                      if (Role.HunterName() == Context.User.Username || Context.User.Username == "Wizard530")
                      {
                          Everyone.HunterNotAFK = 1;
                          if (Everyone.FightingOrNot == 0 && myHunter.SlashAttackCoolReturn() != 0 || myHunter.ArrowAttackCoolReturn() != 0) //COME HERE WHEN ADDING NEW ATTACKS (not neededd anymore)
                          {
                              myHunter.BlockandRestDo();
                              builder.WithTitle("You Block The Monsters Attack!")
                                   .AddField("-------------------------------", "All Cooldowns Reduced By 1!")
                                  .AddField("-------------------------------", "You Take " + myHunter.BlockedPercentReturn() + " % Less Damage")
                                    .AddField("-------------------------------", "You Take Roughly " + myHunter.BlockedAttackReturn() + " Damage")
                                         .AddField("-------------------------------", "Your Health: " + myHunter.GetYourHealth() + "/" + myHunter.GetYourFullHealth())
                                           .AddField("-------------------------------", Context.User.Mention)
                    //  .AddInlineField("-------------------------------", "Debugger: " + myMonster.CounterAttackReturn())
                    .WithColor(Color.Blue)
                         .WithThumbnailUrl("https://i.imgur.com/M9RhXQP.png");
                              await ReplyAsync("", false, builder.Build());
                              if (myHunter.GetYourHealth() <= 0)
                              {
                                  builder2.WithTitle("You Were Killed By The Monster!")

                          .AddInlineField("-------------------------------", "Wait For The Wizard Of Oz To Revive You!")
                            .AddField("-------------------------------", Context.User.Mention)
                  .WithColor(Color.DarkRed);

                                  await ReplyAsync("", false, builder2.Build());
                                  HunterAlive = 1;
                              }


                          }
                          else if (Everyone.FightingOrNot == 0 && myHunter.SlashAttackCoolReturn() == 0 || myHunter.ArrowAttackCoolReturn() == 0)
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

              }*/
    }

}
