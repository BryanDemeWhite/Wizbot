using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.IO;
using GiphyDotNet.Model.Parameters;
using GiphyDotNet.Manager;
using ByteDev.Giphy;
using System.Net.Http;
using ByteDev.Giphy.Request;
//using Discord.js;
//using json = nlohmann::json;


namespace WizBot.Modules
{
    [Group("")]
    public class Testing : ModuleBase<SocketCommandContext>
    {
        [Command("Gif")]
        public async Task Gif([Remainder] string text)
        {
            const string giphyApiKey = "MISSING KEY*******"; //PUT YOUR KEY HERE

            var client = new GiphyApiClient(new HttpClient());

            var request = new SearchRequest(giphyApiKey) { Query = text, Limit = 3 };

            var response = await client.SearchAsync(request);

            Console.Write(response.Gifs.First().Images.Original.Url);

            await ReplyAsync("URL:" + response.Gifs.First().Images.Original.Url); // how to send message

        }
    }

    [Group("GM")]
    public class Everyone : ModuleBase<SocketCommandContext>
    {
        EmbedBuilder builder1 = new EmbedBuilder();
      //  EmbedBuilder builder2 = new EmbedBuilder();
       // EmbedBuilder builder3 = new EmbedBuilder();
      //  EmbedBuilder builder4 = new EmbedBuilder();
       // EmbedBuilder builder5 = new EmbedBuilder();
        public static string[] HunterUserLock = new string[] { "None" };
        public static int IsHunter = 0;
        /// ///////////////////////////////////////////////////////////      
        public static string[] MageUserLock = new string[] { "None" };
        public static int IsMage = 0;
        /// ////////////////////////////////////////////////////
        public static string[] KnightUserLock = new string[] { "None" };
        public static int IsKnight = 0;
        /// ////////////////////////////////////////////////////
        public static int HunterGold = 100;
        public static int HunterEXP = 0;

        private static int HunterOverFlow = 0;
        public static int HunterLevel = 1;
        public static int NextHunterLevel =100;
        public static int FightingOrNot = 1;
        public static int MageGold = 100;
        public static int MageEXP = 0;
        public static int MageStatPoint = 0;
        public static int HunterStatPoint = 0;

        private static int MageOverFlow = 0;
        public static int MageLevel = 1;
        public static int NextMageLevel = 100;
      
        // public static int Potions = 3;
        public static int ShopInv = 0;
        public static int MageNotAFK = 0;
        public static int HunterNotAFK = 0;
        public static int MonsterDeathCounter = 0;
        public static int LevelUpper = 0;
        public static int DifficultyUp = 1;
        public static int Debugger = 0;
       // public static int Debugger2 = 0;

        //  private static int Gold = 100;
        Random rand = new Random();
        Thing.MonsterHealth myMonster = new Thing.MonsterHealth();
      //  HunterOnly Hunter = new HunterOnly();

        string[] Mobs = new string[]

         {
              "https://i.imgur.com/TegzlT8.png",
              "https://i.imgur.com/3MGehwd.png",
              "https://i.imgur.com/4GsGrJD.png",
              "https://i.imgur.com/9GTmsLe.png",
              "https://i.imgur.com/40JbBgd.png"
        };

        // const fs = require("fs");


        /*
        [Command("CreateAccount")]
        public async Task Accountty()
        {
            Player player = new Player()
            {
                Id = Context.Message.Author.Id,
                Name = Context.Message.Author.Username,
                Gold = 0,
              //  Abilities = new List<string>()
              //  { 
              //  }

            };
            string strResultJson = JsonConvert.SerializeObject(player);
            builder1.WithTitle("You have been added")

                     .WithColor(Color.Gold);
              //   .WithThumbnailUrl("https://i.imgur.com/feet0rM.png");
            await ReplyAsync("", false, builder1.Build());
          //  File.AppendText(@"player.json", strResultJson);
            // Console.WriteLine("Stored")
    
        } */

        [Command("bruh")]
        public async Task bruh()
        {

            string data = @"player.json";

            string myID = "Wizard";
            int index = @"player.json".IndexOf(myID);

            builder1.WithTitle("hi " + index)

                    .WithColor(Color.Gold);

            await ReplyAsync("", false, builder1.Build());
        }





            [Command("Loot")]
        public async Task MonsterLoot()
        {
            if (Everyone.FightingOrNot == 4)
            {
                if (HunterNotAFK == 1)
                {

                    myMonster.LootDo();
                    myMonster.PlayerEXPDo();

                    builder1.WithTitle("You Gained " + myMonster.LootReturn() + " Gold")
                          .AddField("-------------------------------", "Aswell as " + myMonster.PlayerEXPReturn() + " EXP For Defeating The Monster!")
                            .AddField("-------------------------------", HunterName())

                     ///   .AddField("-------------------------------", Context.User.Mention)
                     .WithColor(Color.Gold)
                  .WithThumbnailUrl("https://i.imgur.com/feet0rM.png");
                    await ReplyAsync("", false, builder1.Build());

                    HunterGold += myMonster.LootReturn();
                    HunterEXP += myMonster.PlayerEXPReturn();




                    if (HunterEXP >= NextHunterLevel)
                    {
                        await HunterLevelUp();
                    }

                    Everyone.HunterNotAFK = 0;
                    Everyone.FightingOrNot = 1;

                }
                if (MageNotAFK == 1)
                {

                    myMonster.LootDo();
                    myMonster.PlayerEXPDo();

                    builder1.WithTitle("You Gained " + myMonster.LootReturn() + " Gold")
                          .AddField("-------------------------------", "Aswell as " + myMonster.PlayerEXPReturn() + " EXP For Defeating The Monster!")
                            .AddField("-------------------------------", MageName())

                         //    .AddField("-------------------------------", Context.User.Mention)
                         .WithColor(Color.Gold)
                  .WithThumbnailUrl("https://i.imgur.com/feet0rM.png");
                    await ReplyAsync("", false, builder1.Build());

                    MageGold += myMonster.LootReturn();
                    MageEXP += myMonster.PlayerEXPReturn();




                    if (MageEXP >= NextMageLevel)
                    {
                        await MageLevelUp();
                    }

                    Everyone.MageNotAFK = 0;
                    Everyone.FightingOrNot = 1;

                }
            }
        }
        public async Task HunterLevelUp()
        {
            HunterLevel += 1;
            HunterOverFlow = HunterEXP - NextHunterLevel;
            HunterEXP = HunterOverFlow;
            NextHunterLevel = HunterLevel * 100;
            HunterStatPoint++;
            builder1.WithTitle("You Level Up To Level: " + HunterLevel)
                         .AddField("-------------------------------", "EXP: " + HunterEXP + "/" + NextHunterLevel)

                  .AddField("-------------------------------", Context.User.Mention)
                        .WithColor(Color.Magenta)
                 .WithThumbnailUrl("https://i.imgur.com/74XiRva.png");
            await ReplyAsync("", false, builder1.Build());

        }
        public async Task MageLevelUp()
        {
            MageLevel += 1;
            MageOverFlow = MageEXP - NextMageLevel;
            MageEXP = MageOverFlow;
            NextMageLevel = MageLevel * 100;
            MageStatPoint++;
            builder1.WithTitle("You Level Up To Level: " + MageLevel)
                         .AddField("-------------------------------", "EXP: " + MageEXP + "/" + NextMageLevel)

                  .AddField("-------------------------------", Context.User.Mention)
                        .WithColor(Color.Magenta)
                 .WithThumbnailUrl("https://i.imgur.com/74XiRva.png");
            await ReplyAsync("", false, builder1.Build());

        }
        public async Task HunterGetLootANDLevelsFromChest()
        {
            myMonster.LootDo();
            builder1.WithTitle("You Find " + myMonster.LootReturn() + " Gold")
                   .AddField("-------------------------------", Context.User.Mention)
                            .WithColor(Color.Gold)
                      .WithThumbnailUrl("https://i.imgur.com/feet0rM.png");
            await ReplyAsync("", false, builder1.Build());
           HunterGold += myMonster.LootReturn();
            Everyone.HunterNotAFK = 0;

        }
        public async Task MageGetLootANDLevelsFromChest()
        {
            myMonster.LootDo();
            builder1.WithTitle("You Find " + myMonster.LootReturn() + " Gold")
                  .AddField("-------------------------------", Context.User.Mention)
                           .WithColor(Color.Gold)
                    .WithThumbnailUrl("https://i.imgur.com/feet0rM.png");
            await ReplyAsync("", false, builder1.Build());
            Everyone.MageGold += myMonster.LootReturn();
            Everyone.MageNotAFK = 0;


        }
        [Command("DifCheck")]
        public async Task difAsync()
        {
          
            builder1.WithTitle("Current Difficulty")
                  .AddField("Monsters Killed:", Debugger)
                   .AddField("Monster Tier:", DifficultyUp)
                           .WithColor(Color.DarkRed)
                    .WithThumbnailUrl("https://i.imgur.com/OwOMxF3.png");
            await ReplyAsync("", false, builder1.Build());
        }
        [Command("Class Hunter")]
        public async Task testAsync()
        {
            if (IsHunter == 0 && Context.User.Username != MageUserLock[0])
            {
                HunterUserLock[0] = Context.User.Username;
           //     await ReplyAsync(Context.User.Mention);
           //     await ReplyAsync(HunterUserLock[0]);
                builder1.AddField("Name:", Context.User.Username)
                .AddField("Role:", "Hunter")
                   .AddField("Abilities:", "Bow, Shoot, Dagger, Stab, PoisonTip")
                 .WithThumbnailUrl(Context.User.GetAvatarUrl());
                await ReplyAsync("", false, builder1.Build());
                IsHunter = 1;
            }
        }
        [Command("Class Mage")]
        public async Task mageAsync()
        {
            if (IsMage == 0 && Context.User.Username != HunterUserLock[0])
            {
                MageUserLock[0] = Context.User.Username;
            //    await ReplyAsync(Context.User.Mention);
            //    await ReplyAsync(MageUserLock[0]);
                builder1.AddField("Name:", Context.User.Username)
                .AddField("Role:", "Mage")
                   .AddField("Abilities:", "Wack, MagicMissile, Fireball, Meditate")
                 .WithThumbnailUrl(Context.User.GetAvatarUrl());
                await ReplyAsync("", false, builder1.Build());
                IsMage = 1;
            }
        }
        [Command("Open")]
        public async Task openchestAsync()
        {
            if (FightingOrNot == 3)
            {
                if (myMonster.Travely() <= 5)
                {
                    builder1.WithTitle("Your Party Opens The Chest");
                    await ReplyAsync("", false, builder1.Build());
                    if (MageNotAFK == 1)
                    {
                        await MageGetLootANDLevelsFromChest();

                    }
                    if (HunterNotAFK == 1)
                    {

                        await HunterGetLootANDLevelsFromChest();

                    }
                    FightingOrNot = 1;



                }
                else if (myMonster.Travely() > 5 && myMonster.Travely() <= 10)
                {
                    builder1.WithTitle("The Chest Turned Out To Be A Mimic!")
                       .WithColor(Color.Red)
               .AddField("Level: ", myMonster.LevelFinder())
               .AddField("Monster Health: ", +myMonster.GetHealth() + "/" + myMonster.GetfullHealth())
             .WithImageUrl("https://i.imgur.com/WnuvAjG.png");
                    await ReplyAsync("", false, builder1.Build());
                    FightingOrNot = 0;
                }
            

          
            }
            else
            {
                builder1.WithTitle("The Is Nothing To Open");
                await ReplyAsync("", false, builder1.Build());
            }
        }
        [Command("Queue")]
        public async Task lootAsync()
        {
            if (FightingOrNot == 3)
            {
                
           
                if (MageUserLock[0] == Context.User.Username)
                    
                {
                    builder1.WithTitle("You Have Queued For The Chest")
                        .AddField("-------------------------------", Context.User.Mention);
                    await ReplyAsync("", false, builder1.Build());
                    MageNotAFK = 1;

                }
                else if (HunterUserLock[0] == Context.User.Username)
                {
                    builder1.WithTitle("You Have Queued For The Chest")
                       .AddField("-------------------------------","Hunter  "+ Context.User.Mention);
                    await ReplyAsync("", false, builder1.Build());
                    HunterNotAFK = 1;

                }
                else
                {
                    builder1.WithTitle(Context.User.Mention + "YOU DO NOT HAVE A CLASS!");
                    await ReplyAsync("", false, builder1.Build());
                }


            }
            else
            {
                builder1.WithTitle("The Is Nothing To Queue For");
                await ReplyAsync("", false, builder1.Build());
            }
        }
      
        [Command("Class Knight")]
        public async Task knightAsync()
        {
            if (IsMage == 0)
            {
                KnightUserLock[0] = Context.User.Username;
             //   await ReplyAsync(Context.User.Mention);
            //    await ReplyAsync(KnightUserLock[0]);
                builder1.AddField("Name:", Context.User.Mention)
                .AddField("Role:", "Knight")
                   .AddField("Abilities:", "Slash, ShieldBash, Enhance, Block, Taunt")
                   .AddField("Passive:", "Armored")
                 .WithThumbnailUrl(Context.User.GetAvatarUrl());
                await ReplyAsync("", false, builder1.Build());
                IsKnight = 1;
            }
        }
        public string HunterName() // for monster
        {

            return HunterUserLock[0];
        }
        public string MageName() // for monster
        {

            return MageUserLock[0];
        }
        public string KnightName() // for monster
        {

            return KnightUserLock[0];
        }
        [Command("leave")]
        public async Task shopleaveAsync()
        {


            if (Everyone.FightingOrNot == 2 || FightingOrNot==3)
            {
                builder1.WithTitle("Your Party Leaves The Area");
                await ReplyAsync("", false, builder1.Build());
                //  await ReplyAsync("You Leave The Shop");
                Everyone.FightingOrNot = 1;
            }
        }
        [Command("revive")]
        public async Task reviveAsync()
        {


            if (Context.User.Username == "Wizard530")
            {
                builder1.WithTitle("The Wizard Revives All");
                await ReplyAsync("", false, builder1.Build());
                HunterOnly.HunterAlive = 0;
                MageOnly.MageAlive = 0;
                Thing.MageHealth.MageCurrentHP = 500;
                Thing.HunterHealth.HunterCurrentHP = 500;


            }
        }

        [Command("checker")]
        public async Task checkAsync()
        {
            await ReplyAsync("Mage Attacked" + MageNotAFK);
            await ReplyAsync("Hunter Attacked" + HunterNotAFK);

        }
      
        [Command("travel")]

        public async Task SpawnAsync()
        {

            if (FightingOrNot == 1)
            {
                myMonster.Randomizor();
                myMonster.Respawn();
                if (myMonster.Travely() > 10 && myMonster.Travely() < 90) // Monster
                {
                    int randomMobIndex = rand.Next(Mobs.Length);
                    string MonsterToSpawn = Mobs[randomMobIndex];
                    builder1.WithTitle("While Traveling... You Come Across A Monster")
                        .WithColor(Color.Red)
                    .AddField("Level: ", myMonster.LevelFinder())
                    .AddField("Monster Health: ", +myMonster.GetHealth() + "/" + myMonster.GetfullHealth())
                    .WithImageUrl(MonsterToSpawn);
                    await ReplyAsync("", false, builder1.Build());
                    FightingOrNot = 0;
                }
                else if (myMonster.Travely() <= 10) //TRAP OR CHEST
                {
                    builder1.WithTitle("You Find What Appears To Be A Treasure Chest While Traveling")

                  .AddField("------------------------------ - ","WARNING.... Use '!Gm Queue' To be eligible gain loot, Once All Party Members Use '!Gm Queue' Have One Party Member Use !Gm Open")
                        .WithColor(Color.Purple)
                       .WithThumbnailUrl("https://i.imgur.com/9nMPrXq.png");
                    await ReplyAsync("", false, builder1.Build());
                    FightingOrNot = 3;

                }
                else if (myMonster.Travely() >= 90)// SHOP
                {
                    FightingOrNot = 2;
                    builder1.WithTitle("You Happen Apon A Shop!")
                          .WithColor(Color.Orange)
                  // .AddField("-------------------------------", "Your Party Gold:" + Gold)
                  .AddField("-------------------------------", "Use '!buy ItemName' Buy an item.")
                   .AddField("-------------------------------", "Use !leave to close the shop and be able to travel to the next location.")


                            .WithImageUrl("https://i.imgur.com/RowQCxP.png");
                    await ReplyAsync("", false, builder1.Build());
                    ShopInv = rand.Next(1, 3);
                    if (ShopInv == 1)
                    {
                        builder1.WithTitle("The Shop Is Selling:")
                        .WithColor(Color.Orange)
                         //    .AddInlineField("FireBall Tome ", "Gain Ability To Cast A Powerful Fireball! Has Chance To Burn Foe! 300G")
                         .AddField("Healing Potion ", "Buy A Healing Potion, Consumed On Use! Heals For 100HP-150HP! 50G")
                         .AddField("Poison Potion *Hunter Only* ", "Buy A Poison Potion, Consumed On Use! Deals Damage Over Time! 50G");
                        await ReplyAsync("", false, builder1.Build());

                    }
                    else if (ShopInv == 2)
                    {
                        builder1.WithTitle("The Shop Is Selling:")
                       .WithColor(Color.Orange)
                        //   .AddInlineField("Lightning Tome", "Gain Ability To Conjure Powerful Lightning! Has Chance To Paralize Foe! 300G")
                        .AddField("Healing Potion ", "Buy A Healing Potion, Consumed On Use! Heals For 100HP-150HP! 50G")
                         .AddField("Mana Potion *Mage Only* ", "Buy A Mana Potion, Consumed On Use! Refresh All Abilities! 50G");
                        await ReplyAsync("", false, builder1.Build());

                    }
                    else if (ShopInv == 3)
                    {
                        builder1.WithTitle("The Shop Is Selling:")
                          .WithColor(Color.Orange)
                           //      .AddInlineField("Poison Tome", "Gain Ability To Posion Enemies! 300G")
                           .AddField("Healing Potion ", "Buy A Healing Potion, Consumed On Use! Heals For 100HP-150HP! 50G");
                        await ReplyAsync("", false, builder1.Build());
                    }

                    //  await ReplyAsync("");
                    // await ReplyAsync("Use '!buy ItemName' Buy an item.");
                    // await ReplyAsync("Use !leave to close the shop and be able to travel to the next location.");

                }
            }
            else if (FightingOrNot == 0)
            {
                builder1.WithTitle("You cannot do that while in battle.");
                await ReplyAsync("", false, builder1.Build());
                // await ReplyAsync("You cannot do that while in battle.");

            }
            else if (FightingOrNot == 2|| FightingOrNot == 3)
            {
                builder1.WithTitle("Use '!gm leave' to leave an area while out of combat.");
                await ReplyAsync("", false, builder1.Build());
                // await ReplyAsync("You cannot do that while in battle.");

            }


        }
        [Command("help")]
        public async Task helpAsync()
        {
            builder1.WithTitle("Commands:")
                   .WithColor(Color.Purple)
                    .AddField("-------------------------------", "Use '!gm class <ClassName>' to choose a Class; Current Classes: Mage, Hunter")
                               .AddField("-------------------------------", "Use '!gm travel' to adventure forward")
                              .AddField("-------------------------------", "Use '!gm inv' to see your items and class info")
                              .AddField("-------------------------------", "Use '!gm healingpotion' to heal for between 100 and 150 HP")
                             .AddField("-------------------------------", "Use '!gm buy <ItemName>'when in a shop to buy equipment")
                              .AddField("-------------------------------", "Use '!gm leave' to leave a shop")
                              .AddField("-------------------------------", "Use '!gm loot' to loot a monster after defeating it");


            await ReplyAsync("", false, builder1.Build());

        }
    }
  
}
