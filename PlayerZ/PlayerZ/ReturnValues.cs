using GTA;
using GTA.Math;
using GTA.Native;
using PlayerZero.Classes;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace PlayerZero
{
    public class ReturnValues
    {
        public static List<Tattoo> AddRandTats(bool bMale)
        {
            List<Tattoo> Tatlist = new List<Tattoo>();

            if (bMale)
            {
                List<Tattoo> TatThis = new List<Tattoo>();
                TatThis.Add(new Tattoo { BaseName = "mpsum2_overlays", TatName = "MP_Sum2_Tat_006_M", Name = "Painted Micro SMG" });
                TatThis.Add(new Tattoo { BaseName = "mpsum2_overlays", TatName = "MP_Sum2_Tat_007_M", Name = "Weapon King" });
                TatThis.Add(new Tattoo { BaseName = "mpsum2_overlays", TatName = "MP_Sum2_Tat_035_M", Name = "Sniff Sniff" });
                TatThis.Add(new Tattoo { BaseName = "mpsum2_overlays", TatName = "MP_Sum2_Tat_036_M", Name = "Charm Pattern" });
                TatThis.Add(new Tattoo { BaseName = "mpsum2_overlays", TatName = "MP_Sum2_Tat_037_M", Name = "Witch & Skull" });
                TatThis.Add(new Tattoo { BaseName = "mpsum2_overlays", TatName = "MP_Sum2_Tat_038_M", Name = "Pumpkin Bug" });
                TatThis.Add(new Tattoo { BaseName = "mpsum2_overlays", TatName = "MP_Sum2_Tat_039_M", Name = "Sinner" });
                TatThis.Add(new Tattoo { BaseName = "mpsum2_overlays", TatName = "MP_Sum2_Tat_057_M", Name = "Gray Demon" });

                TatThis.Add(new Tattoo { BaseName = "mpsecurity_overlays", TatName = "MP_Security_Tat_004_M", Name = "Hood Heart" });
                TatThis.Add(new Tattoo { BaseName = "mpsecurity_overlays", TatName = "MP_Security_Tat_008_M", Name = "Los Santos Tag" });
                TatThis.Add(new Tattoo { BaseName = "mpsecurity_overlays", TatName = "MP_Security_Tat_013_M", Name = "Blessed Boombox" });
                TatThis.Add(new Tattoo { BaseName = "mpsecurity_overlays", TatName = "MP_Security_Tat_014_M", Name = "Chamberlain Hills" });
                TatThis.Add(new Tattoo { BaseName = "mpsecurity_overlays", TatName = "MP_Security_Tat_015_M", Name = "Smoking Barrels" });
                TatThis.Add(new Tattoo { BaseName = "mpsecurity_overlays", TatName = "MP_Security_Tat_026_M", Name = "Dollar Guns Crossed" });

                TatThis.Add(new Tattoo { BaseName = "mpheist4_overlays", TatName = "MP_Heist4_Tat_021_M", Name = "Skull Surfer" });//
                TatThis.Add(new Tattoo { BaseName = "mpheist4_overlays", TatName = "MP_Heist4_Tat_020_M", Name = "Speaker Tower" });//
                TatThis.Add(new Tattoo { BaseName = "mpheist4_overlays", TatName = "MP_Heist4_Tat_019_M", Name = "Record Shot" });//
                TatThis.Add(new Tattoo { BaseName = "mpheist4_overlays", TatName = "MP_Heist4_Tat_018_M", Name = "Record Head" });//
                TatThis.Add(new Tattoo { BaseName = "mpheist4_overlays", TatName = "MP_Heist4_Tat_017_M", Name = "Tropical Sorcerer" });//
                TatThis.Add(new Tattoo { BaseName = "mpheist4_overlays", TatName = "MP_Heist4_Tat_016_M", Name = "Rose Panther" });//
                TatThis.Add(new Tattoo { BaseName = "mpheist4_overlays", TatName = "MP_Heist4_Tat_015_M", Name = "Paradise Ukulele" });//
                TatThis.Add(new Tattoo { BaseName = "mpheist4_overlays", TatName = "MP_Heist4_Tat_014_M", Name = "Paradise Nap" });//
                TatThis.Add(new Tattoo { BaseName = "mpheist4_overlays", TatName = "MP_Heist4_Tat_013_M", Name = "Wild Dancers" });//

                TatThis.Add(new Tattoo { BaseName = "mpheist3_overlays", TatName = "mpHeist3_Tat_039_M", Name = "Space Rangers" });//
                TatThis.Add(new Tattoo { BaseName = "mpheist3_overlays", TatName = "mpHeist3_Tat_038_M", Name = "Robot Bubblegum" });//
                TatThis.Add(new Tattoo { BaseName = "mpheist3_overlays", TatName = "mpHeist3_Tat_036_M", Name = "LS Shield" });//
                TatThis.Add(new Tattoo { BaseName = "mpheist3_overlays", TatName = "mpHeist3_Tat_030_M", Name = "Howitzer" });//
                TatThis.Add(new Tattoo { BaseName = "mpheist3_overlays", TatName = "mpHeist3_Tat_028_M", Name = "Bananas Gone Bad" });//
                TatThis.Add(new Tattoo { BaseName = "mpheist3_overlays", TatName = "mpHeist3_Tat_027_M", Name = "Epsilon" });//
                TatThis.Add(new Tattoo { BaseName = "mpheist3_overlays", TatName = "mpHeist3_Tat_024_M", Name = "Mount Chiliad" });//
                TatThis.Add(new Tattoo { BaseName = "mpheist3_overlays", TatName = "mpHeist3_Tat_023_M", Name = "Bigfoot" });//

                TatThis.Add(new Tattoo { BaseName = "mpvinewood_overlays", TatName = "mp_vinewood_tat_032_M", Name = "Play Your Ace" });//
                TatThis.Add(new Tattoo { BaseName = "mpvinewood_overlays", TatName = "MP_Vinewood_Tat_029_M", Name = "The Table" });//
                TatThis.Add(new Tattoo { BaseName = "mpvinewood_overlays", TatName = "MP_Vinewood_Tat_021_M", Name = "Show Your Hand" });//
                TatThis.Add(new Tattoo { BaseName = "mpvinewood_overlays", TatName = "MP_Vinewood_Tat_017_M", Name = "Roll the Dice" });//
                TatThis.Add(new Tattoo { BaseName = "mpvinewood_overlays", TatName = "MP_Vinewood_Tat_015_M", Name = "The Jolly Joker" });//
                TatThis.Add(new Tattoo { BaseName = "mpvinewood_overlays", TatName = "MP_Vinewood_Tat_011_M", Name = "Life's a Gamble" });//
                TatThis.Add(new Tattoo { BaseName = "mpvinewood_overlays", TatName = "MP_Vinewood_Tat_010_M", Name = "Photo Finish" });//
                TatThis.Add(new Tattoo { BaseName = "mpvinewood_overlays", TatName = "MP_Vinewood_Tat_009_M", Name = "Till Death Do Us Part" });//
                TatThis.Add(new Tattoo { BaseName = "mpvinewood_overlays", TatName = "MP_Vinewood_Tat_008_M", Name = "Snake Eyes" });//
                TatThis.Add(new Tattoo { BaseName = "mpvinewood_overlays", TatName = "MP_Vinewood_Tat_007_M", Name = "777" });//
                TatThis.Add(new Tattoo { BaseName = "mpvinewood_overlays", TatName = "MP_Vinewood_Tat_006_M", Name = "Wheel of Suits" });//
                TatThis.Add(new Tattoo { BaseName = "mpvinewood_overlays", TatName = "MP_Vinewood_Tat_001_M", Name = "Jackpot" });//

                TatThis.Add(new Tattoo { BaseName = "mpchristmas2017_overlays", TatName = "MP_Christmas2017_Tattoo_027_M", Name = "Molon Labe" });
                TatThis.Add(new Tattoo { BaseName = "mpchristmas2017_overlays", TatName = "MP_Christmas2017_Tattoo_024_M", Name = "Dragon Slayer" });
                TatThis.Add(new Tattoo { BaseName = "mpchristmas2017_overlays", TatName = "MP_Christmas2017_Tattoo_022_M", Name = "Spartan and Horse" });
                TatThis.Add(new Tattoo { BaseName = "mpchristmas2017_overlays", TatName = "MP_Christmas2017_Tattoo_021_M", Name = "Spartan and Lion" });
                TatThis.Add(new Tattoo { BaseName = "mpchristmas2017_overlays", TatName = "MP_Christmas2017_Tattoo_016_M", Name = "Odin and Raven" });
                TatThis.Add(new Tattoo { BaseName = "mpchristmas2017_overlays", TatName = "MP_Christmas2017_Tattoo_015_M", Name = "Samurai Combat" });
                TatThis.Add(new Tattoo { BaseName = "mpchristmas2017_overlays", TatName = "MP_Christmas2017_Tattoo_011_M", Name = "Weathered Skull" });
                TatThis.Add(new Tattoo { BaseName = "mpchristmas2017_overlays", TatName = "MP_Christmas2017_Tattoo_010_M", Name = "Spartan Shield" });
                TatThis.Add(new Tattoo { BaseName = "mpchristmas2017_overlays", TatName = "MP_Christmas2017_Tattoo_009_M", Name = "Norse Rune" });
                TatThis.Add(new Tattoo { BaseName = "mpchristmas2017_overlays", TatName = "MP_Christmas2017_Tattoo_005_M", Name = "Ghost Dragon" });
                TatThis.Add(new Tattoo { BaseName = "mpchristmas2017_overlays", TatName = "MP_Christmas2017_Tattoo_002_M", Name = "Kabuto" });

                TatThis.Add(new Tattoo { BaseName = "mpsmuggler_overlays", TatName = "MP_Smuggler_Tattoo_025_M", Name = "Claimed By The Beast" });
                TatThis.Add(new Tattoo { BaseName = "mpsmuggler_overlays", TatName = "MP_Smuggler_Tattoo_024_M", Name = "Pirate Captain" });
                TatThis.Add(new Tattoo { BaseName = "mpsmuggler_overlays", TatName = "MP_Smuggler_Tattoo_022_M", Name = "X Marks The Spot" });
                TatThis.Add(new Tattoo { BaseName = "mpsmuggler_overlays", TatName = "MP_Smuggler_Tattoo_018_M", Name = "Finders Keepers" });
                TatThis.Add(new Tattoo { BaseName = "mpsmuggler_overlays", TatName = "MP_Smuggler_Tattoo_017_M", Name = "Framed Tall Ship" });
                TatThis.Add(new Tattoo { BaseName = "mpsmuggler_overlays", TatName = "MP_Smuggler_Tattoo_016_M", Name = "Skull Compass" });
                TatThis.Add(new Tattoo { BaseName = "mpsmuggler_overlays", TatName = "MP_Smuggler_Tattoo_013_M", Name = "Torn Wings" });
                TatThis.Add(new Tattoo { BaseName = "mpsmuggler_overlays", TatName = "MP_Smuggler_Tattoo_009_M", Name = "Tall Ship Conflict" });
                TatThis.Add(new Tattoo { BaseName = "mpsmuggler_overlays", TatName = "MP_Smuggler_Tattoo_006_M", Name = "Never Surrender" });
                TatThis.Add(new Tattoo { BaseName = "mpsmuggler_overlays", TatName = "MP_Smuggler_Tattoo_003_M", Name = "Give Nothing Back" });

                TatThis.Add(new Tattoo { BaseName = "mpairraces_overlays", TatName = "MP_Airraces_Tattoo_007_M", Name = "Eagle Eyes" });
                TatThis.Add(new Tattoo { BaseName = "mpairraces_overlays", TatName = "MP_Airraces_Tattoo_005_M", Name = "Parachute Belle" });
                TatThis.Add(new Tattoo { BaseName = "mpairraces_overlays", TatName = "MP_Airraces_Tattoo_004_M", Name = "Balloon Pioneer" });
                TatThis.Add(new Tattoo { BaseName = "mpairraces_overlays", TatName = "MP_Airraces_Tattoo_002_M", Name = "Winged Bombshell" });
                TatThis.Add(new Tattoo { BaseName = "mpairraces_overlays", TatName = "MP_Airraces_Tattoo_001_M", Name = "Pilot Skull" });

                TatThis.Add(new Tattoo { BaseName = "mpgunrunning_overlays", TatName = "MP_Gunrunning_Tattoo_022_M", Name = "Explosive Heart" });//
                TatThis.Add(new Tattoo { BaseName = "mpgunrunning_overlays", TatName = "MP_Gunrunning_Tattoo_019_M", Name = "Pistol Wings" });//
                TatThis.Add(new Tattoo { BaseName = "mpgunrunning_overlays", TatName = "MP_Gunrunning_Tattoo_018_M", Name = "Dual Wield Skull" });//
                TatThis.Add(new Tattoo { BaseName = "mpgunrunning_overlays", TatName = "MP_Gunrunning_Tattoo_014_M", Name = "Backstabber" });//
                TatThis.Add(new Tattoo { BaseName = "mpgunrunning_overlays", TatName = "MP_Gunrunning_Tattoo_013_M", Name = "Wolf Insignia" });//
                TatThis.Add(new Tattoo { BaseName = "mpgunrunning_overlays", TatName = "MP_Gunrunning_Tattoo_009_M", Name = "Butterfly Knife" });//
                TatThis.Add(new Tattoo { BaseName = "mpgunrunning_overlays", TatName = "MP_Gunrunning_Tattoo_001_M", Name = "Crossed Weapons" });//
                TatThis.Add(new Tattoo { BaseName = "mpgunrunning_overlays", TatName = "MP_Gunrunning_Tattoo_000_M", Name = "Bullet Proof" });//

                TatThis.Add(new Tattoo { BaseName = "mpimportexport_overlays", TatName = "MP_MP_ImportExport_Tat_011_M", Name = "Talk Shit Get Hit" });//
                TatThis.Add(new Tattoo { BaseName = "mpimportexport_overlays", TatName = "MP_MP_ImportExport_Tat_010_M", Name = "Take the Wheel" });//
                TatThis.Add(new Tattoo { BaseName = "mpimportexport_overlays", TatName = "MP_MP_ImportExport_Tat_009_M", Name = "Serpents of Destruction" });//
                TatThis.Add(new Tattoo { BaseName = "mpimportexport_overlays", TatName = "MP_MP_ImportExport_Tat_002_M", Name = "Tuned to Death" });//
                TatThis.Add(new Tattoo { BaseName = "mpimportexport_overlays", TatName = "MP_MP_ImportExport_Tat_001_M", Name = "Power Plant" });//
                TatThis.Add(new Tattoo { BaseName = "mpimportexport_overlays", TatName = "MP_MP_ImportExport_Tat_000_M", Name = "Block Back" });//

                TatThis.Add(new Tattoo { BaseName = "mpbiker_overlays", TatName = "MP_MP_Biker_Tat_043_M", Name = "Ride Forever" });//
                TatThis.Add(new Tattoo { BaseName = "mpbiker_overlays", TatName = "MP_MP_Biker_Tat_030_M", Name = "Brothers For Life" });//
                TatThis.Add(new Tattoo { BaseName = "mpbiker_overlays", TatName = "MP_MP_Biker_Tat_021_M", Name = "Flaming Reaper" });//
                TatThis.Add(new Tattoo { BaseName = "mpbiker_overlays", TatName = "MP_MP_Biker_Tat_017_M", Name = "Clawed Beast" });//
                TatThis.Add(new Tattoo { BaseName = "mpbiker_overlays", TatName = "MP_MP_Biker_Tat_011_M", Name = "R.I.P. My Brothers" });//
                TatThis.Add(new Tattoo { BaseName = "mpbiker_overlays", TatName = "MP_MP_Biker_Tat_008_M", Name = "Freedom Wheels" });//
                TatThis.Add(new Tattoo { BaseName = "mpbiker_overlays", TatName = "MP_MP_Biker_Tat_006_M", Name = "Chopper Freedom" });//

                TatThis.Add(new Tattoo { BaseName = "mpstunt_overlays", TatName = "MP_MP_Stunt_tat_048_M", Name = "Racing Doll" });//
                TatThis.Add(new Tattoo { BaseName = "mpstunt_overlays", TatName = "MP_MP_Stunt_tat_046_M", Name = "Full Throttle" });//
                TatThis.Add(new Tattoo { BaseName = "mpstunt_overlays", TatName = "MP_MP_Stunt_tat_041_M", Name = "Brapp" });//
                TatThis.Add(new Tattoo { BaseName = "mpstunt_overlays", TatName = "MP_MP_Stunt_tat_040_M", Name = "Monkey Chopper" });//
                TatThis.Add(new Tattoo { BaseName = "mpstunt_overlays", TatName = "MP_MP_Stunt_tat_037_M", Name = "Big Grills" });//
                TatThis.Add(new Tattoo { BaseName = "mpstunt_overlays", TatName = "MP_MP_Stunt_tat_034_M", Name = "Feather Road Kill" });//
                TatThis.Add(new Tattoo { BaseName = "mpstunt_overlays", TatName = "MP_MP_Stunt_tat_030_M", Name = "Man's Ruin" });//
                TatThis.Add(new Tattoo { BaseName = "mpstunt_overlays", TatName = "MP_MP_Stunt_tat_029_M", Name = "Majestic Finish" });//
                TatThis.Add(new Tattoo { BaseName = "mpstunt_overlays", TatName = "MP_MP_Stunt_tat_026_M", Name = "Winged Wheel" });//
                TatThis.Add(new Tattoo { BaseName = "mpstunt_overlays", TatName = "MP_MP_Stunt_tat_024_M", Name = "Road Kill" });//

                TatThis.Add(new Tattoo { BaseName = "mplowrider2_overlays", TatName = "MP_LR_Tat_032_M", Name = "Reign Over" });//
                TatThis.Add(new Tattoo { BaseName = "mplowrider2_overlays", TatName = "MP_LR_Tat_031_M", Name = "Dead Pretty" });//
                TatThis.Add(new Tattoo { BaseName = "mplowrider2_overlays", TatName = "MP_LR_Tat_008_M", Name = "Love the Game" });//
                TatThis.Add(new Tattoo { BaseName = "mplowrider2_overlays", TatName = "MP_LR_Tat_000_M", Name = "SA Assault" });//

                TatThis.Add(new Tattoo { BaseName = "mplowrider_overlays", TatName = "MP_LR_Tat_021_M", Name = "Sad Angel" });//
                TatThis.Add(new Tattoo { BaseName = "mplowrider_overlays", TatName = "MP_LR_Tat_014_M", Name = "Love is Blind" });//
                TatThis.Add(new Tattoo { BaseName = "mplowrider_overlays", TatName = "MP_LR_Tat_010_M", Name = "Bad Angel" });//
                TatThis.Add(new Tattoo { BaseName = "mplowrider_overlays", TatName = "MP_LR_Tat_009_M", Name = "Amazon" });//

                TatThis.Add(new Tattoo { BaseName = "mpluxe2_overlays", TatName = "MP_Luxe_Tat_029_M", Name = "Geometric Design" });//   
                TatThis.Add(new Tattoo { BaseName = "mpluxe2_overlays", TatName = "MP_Luxe_Tat_022_M", Name = "Cloaked Angel" });//  
                TatThis.Add(new Tattoo { BaseName = "mpluxe_overlays", TatName = "MP_Luxe_Tat_024_M", Name = "Feather Mural" });//    
                TatThis.Add(new Tattoo { BaseName = "mpluxe_overlays", TatName = "MP_Luxe_Tat_006_M", Name = "Adorned Wolf" });//   

                TatThis.Add(new Tattoo { BaseName = "mpchristmas2_overlays", TatName = "MP_Xmas2_M_Tat_015", Name = "Japanese Warrior" });//          
                TatThis.Add(new Tattoo { BaseName = "mpchristmas2_overlays", TatName = "MP_Xmas2_M_Tat_011", Name = "Roaring Tiger" });//      
                TatThis.Add(new Tattoo { BaseName = "mpchristmas2_overlays", TatName = "MP_Xmas2_M_Tat_006", Name = "Carp Shaded" });//   
                TatThis.Add(new Tattoo { BaseName = "mpchristmas2_overlays", TatName = "MP_Xmas2_M_Tat_005", Name = "Carp Outline" });//   

                TatThis.Add(new Tattoo { BaseName = "mphipster_overlays", TatName = "FM_Hip_M_Tat_046", Name = "Triangles" });//         
                TatThis.Add(new Tattoo { BaseName = "mphipster_overlays", TatName = "FM_Hip_M_Tat_041", Name = "Tooth" });//         
                TatThis.Add(new Tattoo { BaseName = "mphipster_overlays", TatName = "FM_Hip_M_Tat_032", Name = "Paper Plane" });//         
                TatThis.Add(new Tattoo { BaseName = "mphipster_overlays", TatName = "FM_Hip_M_Tat_031", Name = "Skateboard" });//           
                TatThis.Add(new Tattoo { BaseName = "mphipster_overlays", TatName = "FM_Hip_M_Tat_030", Name = "Shark Fin" });//        
                TatThis.Add(new Tattoo { BaseName = "mphipster_overlays", TatName = "FM_Hip_M_Tat_025", Name = "Watch Your Step" });//          
                TatThis.Add(new Tattoo { BaseName = "mphipster_overlays", TatName = "FM_Hip_M_Tat_024", Name = "Pyamid" });//   
                TatThis.Add(new Tattoo { BaseName = "mphipster_overlays", TatName = "FM_Hip_M_Tat_012", Name = "Antlers" });//  
                TatThis.Add(new Tattoo { BaseName = "mphipster_overlays", TatName = "FM_Hip_M_Tat_011", Name = "Infinity" });
                TatThis.Add(new Tattoo { BaseName = "mphipster_overlays", TatName = "FM_Hip_M_Tat_000", Name = "Crossed Arrows" });

                TatThis.Add(new Tattoo { BaseName = "mpbusiness_overlays", TatName = "MP_Buis_M_Back_000", Name = "Makin' Paper" });

                TatThis.Add(new Tattoo { BaseName = "mpbeach_overlays", TatName = "MP_Bea_M_Back_000", Name = "Ship Arms" });

                TatThis.Add(new Tattoo { BaseName = "multiplayer_overlays", TatName = "FM_Tat_M_045", Name = "Skulls and Rose" });//
                TatThis.Add(new Tattoo { BaseName = "multiplayer_overlays", TatName = "FM_Tat_M_030", Name = "Lucky Celtic Dogs" });//
                TatThis.Add(new Tattoo { BaseName = "multiplayer_overlays", TatName = "FM_Tat_M_020", Name = "Dragon" });//
                TatThis.Add(new Tattoo { BaseName = "multiplayer_overlays", TatName = "FM_Tat_M_019", Name = "The Wages of Sin" });//Survival Award
                TatThis.Add(new Tattoo { BaseName = "multiplayer_overlays", TatName = "FM_Tat_M_016", Name = "Evil Clown" });//
                TatThis.Add(new Tattoo { BaseName = "multiplayer_overlays", TatName = "FM_Tat_M_013", Name = "Eagle and Serpent" });//
                TatThis.Add(new Tattoo { BaseName = "multiplayer_overlays", TatName = "FM_Tat_M_011", Name = "LS Script" });//
                TatThis.Add(new Tattoo { BaseName = "multiplayer_overlays", TatName = "FM_Tat_M_009", Name = "Skull on the Cross" });//

                TatThis.Add(new Tattoo { BaseName = "multiplayer_overlays", TatName = "FM_Tat_Award_M_019", Name = "Clown Dual Wield Dollars" });//
                TatThis.Add(new Tattoo { BaseName = "multiplayer_overlays", TatName = "FM_Tat_Award_M_018", Name = "Clown Dual Wield" });//
                TatThis.Add(new Tattoo { BaseName = "multiplayer_overlays", TatName = "FM_Tat_Award_M_017", Name = "Clown and Gun" });//
                TatThis.Add(new Tattoo { BaseName = "multiplayer_overlays", TatName = "FM_Tat_Award_M_016", Name = "Clown" });//
                TatThis.Add(new Tattoo { BaseName = "multiplayer_overlays", TatName = "FM_Tat_Award_M_014", Name = "Trust No One" });//Car Bomb Award
                TatThis.Add(new Tattoo { BaseName = "multiplayer_overlays", TatName = "FM_Tat_Award_M_008", Name = "Los Santos Customs" });//Mod a Car Award
                TatThis.Add(new Tattoo { BaseName = "multiplayer_overlays", TatName = "FM_Tat_Award_M_005", Name = "Angel" });//Win Every Game Mode Award
                                                                                                                              //Unknowen
                TatThis.Add(new Tattoo { BaseName = "multiplayer_overlays", TatName = "FM_Tat_M_046", Name = "Zip?" });//Zip???
                TatThis.Add(new Tattoo { BaseName = "mpchristmas2018_overlays", TatName = "MP_Christmas2018_Tat_000_M", Name = "Unknowen" });//

                Tatlist.Add(TatThis[RandInt(0, TatThis.Count - 1)]);
                TatThis.Clear();

                TatThis.Add(new Tattoo { BaseName = "mpsum2_overlays", TatName = "MP_Sum2_Tat_003_M", Name = "Bullet Mouth" });
                TatThis.Add(new Tattoo { BaseName = "mpsum2_overlays", TatName = "MP_Sum2_Tat_004_M", Name = "Smoking Barrel" });
                TatThis.Add(new Tattoo { BaseName = "mpsum2_overlays", TatName = "MP_Sum2_Tat_040_M", Name = "Carved Pumpkin" });
                TatThis.Add(new Tattoo { BaseName = "mpsum2_overlays", TatName = "MP_Sum2_Tat_041_M", Name = "Branched Werewolf" });
                TatThis.Add(new Tattoo { BaseName = "mpsum2_overlays", TatName = "MP_Sum2_Tat_042_M", Name = "Winged Skull" });
                TatThis.Add(new Tattoo { BaseName = "mpsum2_overlays", TatName = "MP_Sum2_Tat_058_M", Name = "Shrieking Dragon" });
                TatThis.Add(new Tattoo { BaseName = "mpsum2_overlays", TatName = "MP_Sum2_Tat_059_M", Name = "Swords & City" });

                TatThis.Add(new Tattoo { BaseName = "mpsecurity_overlays", TatName = "MP_Security_Tat_016_M", Name = "All From The Same Tree" });
                TatThis.Add(new Tattoo { BaseName = "mpsecurity_overlays", TatName = "MP_Security_Tat_017_M", Name = "King of the Jungle" });
                TatThis.Add(new Tattoo { BaseName = "mpsecurity_overlays", TatName = "MP_Security_Tat_018_M", Name = "Night Owl" });

                TatThis.Add(new Tattoo { BaseName = "mpheist4_overlays", TatName = "MP_Heist4_Tat_023_M", Name = "Techno Glitch" });
                TatThis.Add(new Tattoo { BaseName = "mpheist4_overlays", TatName = "MP_Heist4_Tat_022_M", Name = "Paradise Sirens" });

                TatThis.Add(new Tattoo { BaseName = "mpheist3_overlays", TatName = "mpHeist3_Tat_035_M", Name = "LS Panic" });
                TatThis.Add(new Tattoo { BaseName = "mpheist3_overlays", TatName = "mpHeist3_Tat_033_M", Name = "LS City" });
                TatThis.Add(new Tattoo { BaseName = "mpheist3_overlays", TatName = "mpHeist3_Tat_026_M", Name = "Dignity" });
                TatThis.Add(new Tattoo { BaseName = "mpheist3_overlays", TatName = "mpHeist3_Tat_025_M", Name = "Davis" });

                TatThis.Add(new Tattoo { BaseName = "mpvinewood_overlays", TatName = "mp_vinewood_tat_022_M", Name = "Blood Money" });
                TatThis.Add(new Tattoo { BaseName = "mpvinewood_overlays", TatName = "mp_vinewood_tat_003_M", Name = "Royal Flush" });
                TatThis.Add(new Tattoo { BaseName = "mpvinewood_overlays", TatName = "mp_vinewood_tat_000_M", Name = "In the Pocket" });

                TatThis.Add(new Tattoo { BaseName = "mpchristmas2017_overlays", TatName = "MP_Christmas2017_Tattoo_026_M", Name = "Spartan Skull" });
                TatThis.Add(new Tattoo { BaseName = "mpchristmas2017_overlays", TatName = "MP_Christmas2017_Tattoo_020_M", Name = "Medusa's Gaze" });
                TatThis.Add(new Tattoo { BaseName = "mpchristmas2017_overlays", TatName = "MP_Christmas2017_Tattoo_019_M", Name = "Strike Force" });
                TatThis.Add(new Tattoo { BaseName = "mpchristmas2017_overlays", TatName = "MP_Christmas2017_Tattoo_003_M", Name = "Native Warrior" });
                TatThis.Add(new Tattoo { BaseName = "mpchristmas2017_overlays", TatName = "MP_Christmas2017_Tattoo_000_M", Name = "Thor - Goblin" });

                TatThis.Add(new Tattoo { BaseName = "mpsmuggler_overlays", TatName = "MP_Smuggler_Tattoo_021_M", Name = "Dead Tales" });
                TatThis.Add(new Tattoo { BaseName = "mpsmuggler_overlays", TatName = "MP_Smuggler_Tattoo_019_M", Name = "Lost At Sea" });
                TatThis.Add(new Tattoo { BaseName = "mpsmuggler_overlays", TatName = "MP_Smuggler_Tattoo_007_M", Name = "No Honor" });
                TatThis.Add(new Tattoo { BaseName = "mpsmuggler_overlays", TatName = "MP_Smuggler_Tattoo_000_M", Name = "Bless The Dead" });

                TatThis.Add(new Tattoo { BaseName = "mpairraces_overlays", TatName = "MP_Airraces_Tattoo_000_M", Name = "Turbulence" });

                TatThis.Add(new Tattoo { BaseName = "mpgunrunning_overlays", TatName = "MP_Gunrunning_Tattoo_028_M", Name = "Micro SMG Chain" });
                TatThis.Add(new Tattoo { BaseName = "mpgunrunning_overlays", TatName = "MP_Gunrunning_Tattoo_020_M", Name = "Crowned Weapons" });
                TatThis.Add(new Tattoo { BaseName = "mpgunrunning_overlays", TatName = "MP_Gunrunning_Tattoo_017_M", Name = "Dog Tags" });
                TatThis.Add(new Tattoo { BaseName = "mpgunrunning_overlays", TatName = "MP_Gunrunning_Tattoo_012_M", Name = "Dollar Daggers" });

                TatThis.Add(new Tattoo { BaseName = "mpbiker_overlays", TatName = "MP_MP_Biker_Tat_060_M", Name = "We Are The Mods!" });
                TatThis.Add(new Tattoo { BaseName = "mpbiker_overlays", TatName = "MP_MP_Biker_Tat_059_M", Name = "Faggio" });
                TatThis.Add(new Tattoo { BaseName = "mpbiker_overlays", TatName = "MP_MP_Biker_Tat_058_M", Name = "Reaper Vulture" });
                TatThis.Add(new Tattoo { BaseName = "mpbiker_overlays", TatName = "MP_MP_Biker_Tat_050_M", Name = "Unforgiven" });
                TatThis.Add(new Tattoo { BaseName = "mpbiker_overlays", TatName = "MP_MP_Biker_Tat_041_M", Name = "No Regrets" });
                TatThis.Add(new Tattoo { BaseName = "mpbiker_overlays", TatName = "MP_MP_Biker_Tat_034_M", Name = "Brotherhood of Bikes" });
                TatThis.Add(new Tattoo { BaseName = "mpbiker_overlays", TatName = "MP_MP_Biker_Tat_032_M", Name = "Western Eagle" });
                TatThis.Add(new Tattoo { BaseName = "mpbiker_overlays", TatName = "MP_MP_Biker_Tat_029_M", Name = "Bone Wrench" });
                TatThis.Add(new Tattoo { BaseName = "mpbiker_overlays", TatName = "MP_MP_Biker_Tat_026_M", Name = "American Dream" });
                TatThis.Add(new Tattoo { BaseName = "mpbiker_overlays", TatName = "MP_MP_Biker_Tat_023_M", Name = "Western MC" });
                TatThis.Add(new Tattoo { BaseName = "mpbiker_overlays", TatName = "MP_MP_Biker_Tat_019_M", Name = "Gruesome Talons" });
                TatThis.Add(new Tattoo { BaseName = "mpbiker_overlays", TatName = "MP_MP_Biker_Tat_018_M", Name = "Skeletal Chopper" });
                TatThis.Add(new Tattoo { BaseName = "mpbiker_overlays", TatName = "MP_MP_Biker_Tat_013_M", Name = "Demon Crossbones" });
                TatThis.Add(new Tattoo { BaseName = "mpbiker_overlays", TatName = "MP_MP_Biker_Tat_005_M", Name = "Made In America" });
                TatThis.Add(new Tattoo { BaseName = "mpbiker_overlays", TatName = "MP_MP_Biker_Tat_001_M", Name = "Both Barrels" });
                TatThis.Add(new Tattoo { BaseName = "mpbiker_overlays", TatName = "MP_MP_Biker_Tat_000_M", Name = "Demon Rider" });

                TatThis.Add(new Tattoo { BaseName = "mpstunt_overlays", TatName = "MP_MP_Stunt_tat_044_M", Name = "Ram Skull" });
                TatThis.Add(new Tattoo { BaseName = "mpstunt_overlays", TatName = "MP_MP_Stunt_tat_033_M", Name = "Sugar Skull Trucker" });
                TatThis.Add(new Tattoo { BaseName = "mpstunt_overlays", TatName = "MP_MP_Stunt_tat_027_M", Name = "Punk Road Hog" });
                TatThis.Add(new Tattoo { BaseName = "mpstunt_overlays", TatName = "MP_MP_Stunt_tat_019_M", Name = "Engine Heart" });
                TatThis.Add(new Tattoo { BaseName = "mpstunt_overlays", TatName = "MP_MP_Stunt_tat_018_M", Name = "Vintage Bully" });
                TatThis.Add(new Tattoo { BaseName = "mpstunt_overlays", TatName = "MP_MP_Stunt_tat_011_M", Name = "Wheels of Death" });

                TatThis.Add(new Tattoo { BaseName = "mplowrider2_overlays", TatName = "MP_LR_Tat_019_M", Name = "Death Behind" });
                TatThis.Add(new Tattoo { BaseName = "mplowrider2_overlays", TatName = "MP_LR_Tat_012_M", Name = "Royal Kiss" });

                TatThis.Add(new Tattoo { BaseName = "mplowrider_overlays", TatName = "MP_LR_Tat_026_M", Name = "Royal Takeover" });
                TatThis.Add(new Tattoo { BaseName = "mplowrider_overlays", TatName = "MP_LR_Tat_013_M", Name = "Love Gamble" });
                TatThis.Add(new Tattoo { BaseName = "mplowrider_overlays", TatName = "MP_LR_Tat_002_M", Name = "Holy Mary" });
                TatThis.Add(new Tattoo { BaseName = "mplowrider_overlays", TatName = "MP_LR_Tat_001_M", Name = "King Fight" });

                TatThis.Add(new Tattoo { BaseName = "mpluxe2_overlays", TatName = "MP_Luxe_Tat_027_M", Name = "Cobra Dawn" });
                TatThis.Add(new Tattoo { BaseName = "mpluxe2_overlays", TatName = "MP_Luxe_Tat_025_M", Name = "Reaper Sway" });
                TatThis.Add(new Tattoo { BaseName = "mpluxe2_overlays", TatName = "MP_Luxe_Tat_012_M", Name = "Geometric Galaxy" });
                TatThis.Add(new Tattoo { BaseName = "mpluxe2_overlays", TatName = "MP_Luxe_Tat_002_M", Name = "The Howler" });

                TatThis.Add(new Tattoo { BaseName = "mpluxe_overlays", TatName = "MP_Luxe_Tat_015_M", Name = "Smoking Sisters" });
                TatThis.Add(new Tattoo { BaseName = "mpluxe_overlays", TatName = "MP_Luxe_Tat_014_M", Name = "Ancient Queen" });
                TatThis.Add(new Tattoo { BaseName = "mpluxe_overlays", TatName = "MP_Luxe_Tat_008_M", Name = "Flying Eye" });
                TatThis.Add(new Tattoo { BaseName = "mpluxe_overlays", TatName = "MP_Luxe_Tat_007_M", Name = "Eye of the Griffin" });

                TatThis.Add(new Tattoo { BaseName = "mpchristmas2_overlays", TatName = "MP_Xmas2_M_Tat_019", Name = "Royal Dagger Color" });
                TatThis.Add(new Tattoo { BaseName = "mpchristmas2_overlays", TatName = "MP_Xmas2_M_Tat_018", Name = "Royal Dagger Outline" });
                TatThis.Add(new Tattoo { BaseName = "mpchristmas2_overlays", TatName = "MP_Xmas2_M_Tat_017", Name = "Loose Lips Color" });
                TatThis.Add(new Tattoo { BaseName = "mpchristmas2_overlays", TatName = "MP_Xmas2_M_Tat_016", Name = "Loose Lips Outline" });
                TatThis.Add(new Tattoo { BaseName = "mpchristmas2_overlays", TatName = "MP_Xmas2_M_Tat_009", Name = "Time To Die" });

                TatThis.Add(new Tattoo { BaseName = "mphipster_overlays", TatName = "FM_Hip_M_Tat_047", Name = "Cassette" });
                TatThis.Add(new Tattoo { BaseName = "mphipster_overlays", TatName = "FM_Hip_M_Tat_033", Name = "Stag" });
                TatThis.Add(new Tattoo { BaseName = "mphipster_overlays", TatName = "FM_Hip_M_Tat_013", Name = "Boombox" });
                TatThis.Add(new Tattoo { BaseName = "mphipster_overlays", TatName = "FM_Hip_M_Tat_002", Name = "Chemistry" });

                TatThis.Add(new Tattoo { BaseName = "mpbusiness_overlays", TatName = "MP_Buis_M_Chest_001", Name = "$$$" });
                TatThis.Add(new Tattoo { BaseName = "mpbusiness_overlays", TatName = "MP_Buis_M_Chest_000", Name = "Rich" });
                TatThis.Add(new Tattoo { BaseName = "mpbeach_overlays", TatName = "MP_Bea_M_Chest_001", Name = "Tribal Shark" });
                TatThis.Add(new Tattoo { BaseName = "mpbeach_overlays", TatName = "MP_Bea_M_Chest_000", Name = "Tribal Hammerhead" });

                TatThis.Add(new Tattoo { BaseName = "multiplayer_overlays", TatName = "FM_Tat_M_044", Name = "Stone Cross" });
                TatThis.Add(new Tattoo { BaseName = "multiplayer_overlays", TatName = "FM_Tat_M_034", Name = "Flaming Shamrock" });
                TatThis.Add(new Tattoo { BaseName = "multiplayer_overlays", TatName = "FM_Tat_M_025", Name = "LS Bold" });
                TatThis.Add(new Tattoo { BaseName = "multiplayer_overlays", TatName = "FM_Tat_M_024", Name = "Flaming Cross" });
                TatThis.Add(new Tattoo { BaseName = "multiplayer_overlays", TatName = "FM_Tat_M_010", Name = "LS Flames" });

                TatThis.Add(new Tattoo { BaseName = "multiplayer_overlays", TatName = "FM_Tat_Award_M_013", Name = "Seven Deadly Sins" });//Kill 1000 Players Award
                TatThis.Add(new Tattoo { BaseName = "multiplayer_overlays", TatName = "FM_Tat_Award_M_012", Name = "Embellished Scroll" });//Kill 500 Players Award
                TatThis.Add(new Tattoo { BaseName = "multiplayer_overlays", TatName = "FM_Tat_Award_M_011", Name = "Blank Scroll" });////Kill 100 Players Award?
                TatThis.Add(new Tattoo { BaseName = "multiplayer_overlays", TatName = "FM_Tat_Award_M_003", Name = "Blackjack" });

                Tatlist.Add(TatThis[RandInt(0, TatThis.Count - 1)]);
                TatThis.Clear();

                TatThis.Add(new Tattoo { BaseName = "mpsum2_overlays", TatName = "MP_Sum2_Tat_005_M", Name = "Concealed" });
                TatThis.Add(new Tattoo { BaseName = "mpsum2_overlays", TatName = "MP_Sum2_Tat_043_M", Name = "Cursed Saki" });
                TatThis.Add(new Tattoo { BaseName = "mpsum2_overlays", TatName = "MP_Sum2_Tat_044_M", Name = "Smouldering Bat & Skull" });
                TatThis.Add(new Tattoo { BaseName = "mpsum2_overlays", TatName = "MP_Sum2_Tat_060_M", Name = "Blaine County" });
                TatThis.Add(new Tattoo { BaseName = "mpsum2_overlays", TatName = "MP_Sum2_Tat_061_M", Name = "Angry Possum" });
                TatThis.Add(new Tattoo { BaseName = "mpsum2_overlays", TatName = "MP_Sum2_Tat_062_M", Name = "Floral Demon" });

                TatThis.Add(new Tattoo { BaseName = "mpsecurity_overlays", TatName = "MP_Security_Tat_024_M", Name = "Beatbox Silhouette" });
                TatThis.Add(new Tattoo { BaseName = "mpsecurity_overlays", TatName = "MP_Security_Tat_025_M", Name = "Davis Flames" });

                TatThis.Add(new Tattoo { BaseName = "mpheist4_overlays", TatName = "MP_Heist4_Tat_030_M", Name = "Radio Tape" });
                TatThis.Add(new Tattoo { BaseName = "mpheist4_overlays", TatName = "MP_Heist4_Tat_004_M", Name = "Skeleton Breeze" });

                TatThis.Add(new Tattoo { BaseName = "mpheist3_overlays", TatName = "mpHeist3_Tat_037_M", Name = "LadyBug" });
                TatThis.Add(new Tattoo { BaseName = "mpheist3_overlays", TatName = "mpHeist3_Tat_029_M", Name = "Fatal Incursion" });

                TatThis.Add(new Tattoo { BaseName = "mpvinewood_overlays", TatName = "mp_vinewood_tat_031_M", Name = "Gambling Royalty" });
                TatThis.Add(new Tattoo { BaseName = "mpvinewood_overlays", TatName = "mp_vinewood_tat_024_M", Name = "Cash Mouth" });
                TatThis.Add(new Tattoo { BaseName = "mpvinewood_overlays", TatName = "mp_vinewood_tat_016_M", Name = "Rose and Aces" });
                TatThis.Add(new Tattoo { BaseName = "mpvinewood_overlays", TatName = "mp_vinewood_tat_012_M", Name = "Skull of Suits" });

                TatThis.Add(new Tattoo { BaseName = "mpchristmas2017_overlays", TatName = "MP_Christmas2017_Tattoo_008_M", Name = "Spartan Warrior" });

                TatThis.Add(new Tattoo { BaseName = "mpsmuggler_overlays", TatName = "MP_Smuggler_Tattoo_015_M", Name = "Jolly Roger" });
                TatThis.Add(new Tattoo { BaseName = "mpsmuggler_overlays", TatName = "MP_Smuggler_Tattoo_010_M", Name = "See You In Hell" });
                TatThis.Add(new Tattoo { BaseName = "mpsmuggler_overlays", TatName = "MP_Smuggler_Tattoo_002_M", Name = "Dead Lies" });

                TatThis.Add(new Tattoo { BaseName = "mpairraces_overlays", TatName = "MP_Airraces_Tattoo_006_M", Name = "Bombs Away" });

                TatThis.Add(new Tattoo { BaseName = "mpgunrunning_overlays", TatName = "MP_Gunrunning_Tattoo_029_M", Name = "Win Some Lose Some" });
                TatThis.Add(new Tattoo { BaseName = "mpgunrunning_overlays", TatName = "MP_Gunrunning_Tattoo_010_M", Name = "Cash Money" });

                TatThis.Add(new Tattoo { BaseName = "mpbiker_overlays", TatName = "MP_MP_Biker_Tat_052_M", Name = "Biker Mount" });
                TatThis.Add(new Tattoo { BaseName = "mpbiker_overlays", TatName = "MP_MP_Biker_Tat_039_M", Name = "Gas Guzzler" });
                TatThis.Add(new Tattoo { BaseName = "mpbiker_overlays", TatName = "MP_MP_Biker_Tat_031_M", Name = "Gear Head" });
                TatThis.Add(new Tattoo { BaseName = "mpbiker_overlays", TatName = "MP_MP_Biker_Tat_010_M", Name = "Skull Of Taurus" });
                TatThis.Add(new Tattoo { BaseName = "mpbiker_overlays", TatName = "MP_MP_Biker_Tat_003_M", Name = "Web Rider" });

                TatThis.Add(new Tattoo { BaseName = "mpstunt_overlays", TatName = "MP_MP_Stunt_tat_014_M", Name = "Bat Cat of Spades" });
                TatThis.Add(new Tattoo { BaseName = "mpstunt_overlays", TatName = "MP_MP_Stunt_tat_012_M", Name = "Punk Biker" });

                TatThis.Add(new Tattoo { BaseName = "mplowrider2_overlays", TatName = "MP_LR_Tat_016_M", Name = "Two Face" });
                TatThis.Add(new Tattoo { BaseName = "mplowrider2_overlays", TatName = "MP_LR_Tat_011_M", Name = "Lady Liberty" });

                TatThis.Add(new Tattoo { BaseName = "mplowrider_overlays", TatName = "MP_LR_Tat_004_M", Name = "Gun Mic" });

                TatThis.Add(new Tattoo { BaseName = "mpluxe_overlays", TatName = "MP_Luxe_Tat_003_M", Name = "Abstract Skull" });

                TatThis.Add(new Tattoo { BaseName = "mpchristmas2_overlays", TatName = "MP_Xmas2_M_Tat_028", Name = "Executioner" });
                TatThis.Add(new Tattoo { BaseName = "mpchristmas2_overlays", TatName = "MP_Xmas2_M_Tat_013", Name = "Lizard" });

                TatThis.Add(new Tattoo { BaseName = "mphipster_overlays", TatName = "FM_Hip_M_Tat_035", Name = "Sewn Heart" });
                TatThis.Add(new Tattoo { BaseName = "mphipster_overlays", TatName = "FM_Hip_M_Tat_029", Name = "Sad" });
                TatThis.Add(new Tattoo { BaseName = "mphipster_overlays", TatName = "FM_Hip_M_Tat_006", Name = "Feather Birds" });

                TatThis.Add(new Tattoo { BaseName = "mpbusiness_overlays", TatName = "MP_Buis_M_Stomach_000", Name = "Refined Hustler" });

                TatThis.Add(new Tattoo { BaseName = "mpbeach_overlays", TatName = "MP_Bea_M_Stom_001", Name = "Wheel" });
                TatThis.Add(new Tattoo { BaseName = "mpbeach_overlays", TatName = "MP_Bea_M_Stom_000", Name = "Swordfish" });


                TatThis.Add(new Tattoo { BaseName = "multiplayer_overlays", TatName = "FM_Tat_M_036", Name = "Way of the Gun" });//500 Pistol kills Award
                TatThis.Add(new Tattoo { BaseName = "multiplayer_overlays", TatName = "FM_Tat_M_029", Name = "Trinity Knot" });
                TatThis.Add(new Tattoo { BaseName = "multiplayer_overlays", TatName = "FM_Tat_M_012", Name = "Los Santos Bills" });
                TatThis.Add(new Tattoo { BaseName = "multiplayer_overlays", TatName = "FM_Tat_M_004", Name = "Faith" });

                TatThis.Add(new Tattoo { BaseName = "multiplayer_overlays", TatName = "FM_Tat_Award_M_004", Name = "Hustler" });//Make 50000 from betting Award

                Tatlist.Add(TatThis[RandInt(0, TatThis.Count - 1)]);
                TatThis.Clear();

                TatThis.Add(new Tattoo { BaseName = "mpsum2_overlays", TatName = "MP_Sum2_Tat_000_M", Name = "Live Fast Mono" });
                TatThis.Add(new Tattoo { BaseName = "mpsum2_overlays", TatName = "MP_Sum2_Tat_001_M", Name = "Live Fast Color" });
                TatThis.Add(new Tattoo { BaseName = "mpsum2_overlays", TatName = "MP_Sum2_Tat_018_M", Name = "Branched Skull" });
                TatThis.Add(new Tattoo { BaseName = "mpsum2_overlays", TatName = "MP_Sum2_Tat_019_M", Name = "Scythed Corpse" });
                TatThis.Add(new Tattoo { BaseName = "mpsum2_overlays", TatName = "MP_Sum2_Tat_020_M", Name = "Scythed Corpse & Reaper" });
                TatThis.Add(new Tattoo { BaseName = "mpsum2_overlays", TatName = "MP_Sum2_Tat_021_M", Name = "Third Eye" });
                TatThis.Add(new Tattoo { BaseName = "mpsum2_overlays", TatName = "MP_Sum2_Tat_022_M", Name = "Pierced Third Eye" });
                TatThis.Add(new Tattoo { BaseName = "mpsum2_overlays", TatName = "MP_Sum2_Tat_023_M", Name = "Lip Drip" });
                TatThis.Add(new Tattoo { BaseName = "mpsum2_overlays", TatName = "MP_Sum2_Tat_024_M", Name = "Skin Mask" });
                TatThis.Add(new Tattoo { BaseName = "mpsum2_overlays", TatName = "MP_Sum2_Tat_025_M", Name = "Webbed Scythe" });
                TatThis.Add(new Tattoo { BaseName = "mpsum2_overlays", TatName = "MP_Sum2_Tat_026_M", Name = "Oni Demon" });
                TatThis.Add(new Tattoo { BaseName = "mpsum2_overlays", TatName = "MP_Sum2_Tat_027_M", Name = "Bat Wings" });

                TatThis.Add(new Tattoo { BaseName = "mpsecurity_overlays", TatName = "MP_Security_Tat_001_M", Name = "Bright Diamond" });
                TatThis.Add(new Tattoo { BaseName = "mpsecurity_overlays", TatName = "MP_Security_Tat_002_M", Name = "Hustle" });
                TatThis.Add(new Tattoo { BaseName = "mpsecurity_overlays", TatName = "MP_Security_Tat_027_M", Name = "Black Widow" });

                TatThis.Add(new Tattoo { BaseName = "mpheist3_overlays", TatName = "mpHeist3_Tat_044_M", Name = "Clubs" });
                TatThis.Add(new Tattoo { BaseName = "mpheist3_overlays", TatName = "mpHeist3_Tat_043_M", Name = "Diamonds" });
                TatThis.Add(new Tattoo { BaseName = "mpheist3_overlays", TatName = "mpHeist3_Tat_042_M", Name = "Hearts" });

                TatThis.Add(new Tattoo { BaseName = "mpheist3_overlays", TatName = "mpHeist3_Tat_022_M", Name = "Thong's Sword" });
                TatThis.Add(new Tattoo { BaseName = "mpheist3_overlays", TatName = "mpHeist3_Tat_021_M", Name = "Wanted" });
                TatThis.Add(new Tattoo { BaseName = "mpheist3_overlays", TatName = "mpHeist3_Tat_020_M", Name = "UFO" });
                TatThis.Add(new Tattoo { BaseName = "mpheist3_overlays", TatName = "mpHeist3_Tat_019_M", Name = "Teddy Bear" });
                TatThis.Add(new Tattoo { BaseName = "mpheist3_overlays", TatName = "mpHeist3_Tat_018_M", Name = "Stitches" });
                TatThis.Add(new Tattoo { BaseName = "mpheist3_overlays", TatName = "mpHeist3_Tat_017_M", Name = "Space Monkey" });
                TatThis.Add(new Tattoo { BaseName = "mpheist3_overlays", TatName = "mpHeist3_Tat_016_M", Name = "Sleepy" });
                TatThis.Add(new Tattoo { BaseName = "mpheist3_overlays", TatName = "mpHeist3_Tat_015_M", Name = "On/Off" });
                TatThis.Add(new Tattoo { BaseName = "mpheist3_overlays", TatName = "mpHeist3_Tat_014_M", Name = "LS Wings" });
                TatThis.Add(new Tattoo { BaseName = "mpheist3_overlays", TatName = "mpHeist3_Tat_013_M", Name = "LS Star" });
                TatThis.Add(new Tattoo { BaseName = "mpheist3_overlays", TatName = "mpHeist3_Tat_012_M", Name = "Razor Pop" });
                TatThis.Add(new Tattoo { BaseName = "mpheist3_overlays", TatName = "mpHeist3_Tat_011_M", Name = "Lipstick Kiss" });
                TatThis.Add(new Tattoo { BaseName = "mpheist3_overlays", TatName = "mpHeist3_Tat_010_M", Name = "Green Leaf" });
                TatThis.Add(new Tattoo { BaseName = "mpheist3_overlays", TatName = "mpHeist3_Tat_009_M", Name = "Knifed" });
                TatThis.Add(new Tattoo { BaseName = "mpheist3_overlays", TatName = "mpHeist3_Tat_008_M", Name = "Ice Cream" });
                TatThis.Add(new Tattoo { BaseName = "mpheist3_overlays", TatName = "mpHeist3_Tat_007_M", Name = "Two Horns" });
                TatThis.Add(new Tattoo { BaseName = "mpheist3_overlays", TatName = "mpHeist3_Tat_006_M", Name = "Crowned" });
                TatThis.Add(new Tattoo { BaseName = "mpheist3_overlays", TatName = "mpHeist3_Tat_005_M", Name = "Spades" });
                TatThis.Add(new Tattoo { BaseName = "mpheist3_overlays", TatName = "mpHeist3_Tat_004_M", Name = "Bandage" });
                TatThis.Add(new Tattoo { BaseName = "mpheist3_overlays", TatName = "mpHeist3_Tat_003_M", Name = "Assault Rifle" });
                TatThis.Add(new Tattoo { BaseName = "mpheist3_overlays", TatName = "mpHeist3_Tat_002_M", Name = "Animal" });
                TatThis.Add(new Tattoo { BaseName = "mpheist3_overlays", TatName = "mpHeist3_Tat_001_M", Name = "Ace of Spades" });
                TatThis.Add(new Tattoo { BaseName = "mpheist3_overlays", TatName = "mpHeist3_Tat_000_M", Name = "Five Stars" });

                TatThis.Add(new Tattoo { BaseName = "mpsmuggler_overlays", TatName = "MP_Smuggler_Tattoo_012_M", Name = "Thief" });
                TatThis.Add(new Tattoo { BaseName = "mpsmuggler_overlays", TatName = "MP_Smuggler_Tattoo_011_M", Name = "Sinner" });

                TatThis.Add(new Tattoo { BaseName = "mpgunrunning_overlays", TatName = "MP_Gunrunning_Tattoo_003_M", Name = "Lock and Load" });

                TatThis.Add(new Tattoo { BaseName = "mpbiker_overlays", TatName = "MP_MP_Biker_Tat_051_M", Name = "Western Stylized" });
                TatThis.Add(new Tattoo { BaseName = "mpbiker_overlays", TatName = "MP_MP_Biker_Tat_038_M", Name = "FTW" });
                TatThis.Add(new Tattoo { BaseName = "mpbiker_overlays", TatName = "MP_MP_Biker_Tat_009_M", Name = "Morbid Arachnid" });

                TatThis.Add(new Tattoo { BaseName = "mpstunt_overlays", TatName = "MP_MP_Stunt_tat_042_M", Name = "Flaming Quad" });
                TatThis.Add(new Tattoo { BaseName = "mpstunt_overlays", TatName = "MP_MP_Stunt_tat_017_M", Name = "Bat Wheel" });
                TatThis.Add(new Tattoo { BaseName = "mpstunt_overlays", TatName = "MP_MP_Stunt_Tat_006_M", Name = "Toxic Spider" });
                TatThis.Add(new Tattoo { BaseName = "mpstunt_overlays", TatName = "MP_MP_Stunt_Tat_004_M", Name = "Scorpion" });
                TatThis.Add(new Tattoo { BaseName = "mpstunt_overlays", TatName = "MP_MP_Stunt_Tat_000_M", Name = "Stunt Skull" });

                TatThis.Add(new Tattoo { BaseName = "mpchristmas2_overlays", TatName = "MP_Xmas2_M_Tat_029", Name = "Beautiful Death" });
                TatThis.Add(new Tattoo { BaseName = "mpchristmas2_overlays", TatName = "MP_Xmas2_M_Tat_025", Name = "Snake Head Color" });
                TatThis.Add(new Tattoo { BaseName = "mpchristmas2_overlays", TatName = "MP_Xmas2_M_Tat_024", Name = "Snake Head Outline" });
                TatThis.Add(new Tattoo { BaseName = "mpchristmas2_overlays", TatName = "MP_Xmas2_M_Tat_007", Name = "Los Muertos" });

                TatThis.Add(new Tattoo { BaseName = "mphipster_overlays", TatName = "FM_Hip_M_Tat_021", Name = "Geo Fox" });
                TatThis.Add(new Tattoo { BaseName = "mphipster_overlays", TatName = "FM_Hip_M_Tat_005", Name = "Beautiful Eye" });

                TatThis.Add(new Tattoo { BaseName = "mpbusiness_overlays", TatName = "MP_Buis_M_Neck_003", Name = "$100" });
                TatThis.Add(new Tattoo { BaseName = "mpbusiness_overlays", TatName = "MP_Buis_M_Neck_002", Name = "Script Dollar Sign" });
                TatThis.Add(new Tattoo { BaseName = "mpbusiness_overlays", TatName = "MP_Buis_M_Neck_001", Name = "Bold Dollar Sign" });
                TatThis.Add(new Tattoo { BaseName = "mpbusiness_overlays", TatName = "MP_Buis_M_Neck_000", Name = "Cash is King" });

                TatThis.Add(new Tattoo { BaseName = "mpbeach_overlays", TatName = "MP_Bea_M_Head_002", Name = "Shark" });

                TatThis.Add(new Tattoo { BaseName = "mpbeach_overlays", TatName = "MP_Bea_M_Neck_001", Name = "Surfs Up" });
                TatThis.Add(new Tattoo { BaseName = "mpbeach_overlays", TatName = "MP_Bea_M_Neck_000", Name = "Little Fish" });

                TatThis.Add(new Tattoo { BaseName = "mpbeach_overlays", TatName = "MP_Bea_M_Head_001", Name = "Surf LS" });
                TatThis.Add(new Tattoo { BaseName = "mpbeach_overlays", TatName = "MP_Bea_M_Head_000", Name = "Pirate Skull" });

                TatThis.Add(new Tattoo { BaseName = "multiplayer_overlays", TatName = "FM_Tat_Award_M_000", Name = "Skull" });

                Tatlist.Add(TatThis[RandInt(0, TatThis.Count - 1)]);
                TatThis.Clear();

                TatThis.Add(new Tattoo { BaseName = "mpsum2_overlays", TatName = "MP_Sum2_Tat_008_M", Name = "Bigness Chimp" });
                TatThis.Add(new Tattoo { BaseName = "mpsum2_overlays", TatName = "MP_Sum2_Tat_009_M", Name = "Up-n-Atomizer Design" });
                TatThis.Add(new Tattoo { BaseName = "mpsum2_overlays", TatName = "MP_Sum2_Tat_010_M", Name = "Rocket Launcher Girl" });
                TatThis.Add(new Tattoo { BaseName = "mpsum2_overlays", TatName = "MP_Sum2_Tat_028_M", Name = "Laser Eyes Skull" });
                TatThis.Add(new Tattoo { BaseName = "mpsum2_overlays", TatName = "MP_Sum2_Tat_029_M", Name = "Classic Vampire" });
                TatThis.Add(new Tattoo { BaseName = "mpsum2_overlays", TatName = "MP_Sum2_Tat_049_M", Name = "Demon Drummer" });

                TatThis.Add(new Tattoo { BaseName = "mpsecurity_overlays", TatName = "MP_Security_Tat_006_M", Name = "Skeleton Shot" });
                TatThis.Add(new Tattoo { BaseName = "mpsecurity_overlays", TatName = "MP_Security_Tat_010_M", Name = "Music Is The Remedy" });
                TatThis.Add(new Tattoo { BaseName = "mpsecurity_overlays", TatName = "MP_Security_Tat_011_M", Name = "Serpent Mic" });
                TatThis.Add(new Tattoo { BaseName = "mpsecurity_overlays", TatName = "MP_Security_Tat_019_M", Name = "Weed Knuckles" });

                TatThis.Add(new Tattoo { BaseName = "mpheist4_overlays", TatName = "MP_Heist4_Tat_009_M", Name = "Scratch Panther" });

                TatThis.Add(new Tattoo { BaseName = "mpheist3_overlays", TatName = "mpHeist3_Tat_041_M", Name = "Mighty Thog" });
                TatThis.Add(new Tattoo { BaseName = "mpheist3_overlays", TatName = "mpHeist3_Tat_040_M", Name = "Tiger Heart" });

                TatThis.Add(new Tattoo { BaseName = "mpvinewood_overlays", TatName = "MP_Vinewood_Tat_026_M", Name = "Banknote Rose" });
                TatThis.Add(new Tattoo { BaseName = "mpvinewood_overlays", TatName = "MP_Vinewood_Tat_019_M", Name = "Can't Win Them All" });
                TatThis.Add(new Tattoo { BaseName = "mpvinewood_overlays", TatName = "MP_Vinewood_Tat_014_M", Name = "Vice" });
                TatThis.Add(new Tattoo { BaseName = "mpvinewood_overlays", TatName = "MP_Vinewood_Tat_005_M", Name = "Get Lucky" });
                TatThis.Add(new Tattoo { BaseName = "mpvinewood_overlays", TatName = "MP_Vinewood_Tat_002_M", Name = "Suits" });

                TatThis.Add(new Tattoo { BaseName = "mpchristmas2017_overlays", TatName = "MP_Christmas2017_Tattoo_029_M", Name = "Cerberus" });
                TatThis.Add(new Tattoo { BaseName = "mpchristmas2017_overlays", TatName = "MP_Christmas2017_Tattoo_025_M", Name = "Winged Serpent" });
                TatThis.Add(new Tattoo { BaseName = "mpchristmas2017_overlays", TatName = "MP_Christmas2017_Tattoo_013_M", Name = "Katana" });
                TatThis.Add(new Tattoo { BaseName = "mpchristmas2017_overlays", TatName = "MP_Christmas2017_Tattoo_007_M", Name = "Spartan Combat" });
                TatThis.Add(new Tattoo { BaseName = "mpchristmas2017_overlays", TatName = "MP_Christmas2017_Tattoo_004_M", Name = "Tiger and Mask" });
                TatThis.Add(new Tattoo { BaseName = "mpchristmas2017_overlays", TatName = "MP_Christmas2017_Tattoo_001_M", Name = "Viking Warrior" });

                TatThis.Add(new Tattoo { BaseName = "mpsmuggler_overlays", TatName = "MP_Smuggler_Tattoo_014_M", Name = "Mermaid's Curse" });
                TatThis.Add(new Tattoo { BaseName = "mpsmuggler_overlays", TatName = "MP_Smuggler_Tattoo_008_M", Name = "Horrors Of The Deep" });
                TatThis.Add(new Tattoo { BaseName = "mpsmuggler_overlays", TatName = "MP_Smuggler_Tattoo_004_M", Name = "Honor" });

                TatThis.Add(new Tattoo { BaseName = "mpairraces_overlays", TatName = "MP_Airraces_Tattoo_003_M", Name = "Toxic Trails" });

                TatThis.Add(new Tattoo { BaseName = "mpgunrunning_overlays", TatName = "MP_Gunrunning_Tattoo_027_M", Name = "Serpent Revolver" });
                TatThis.Add(new Tattoo { BaseName = "mpgunrunning_overlays", TatName = "MP_Gunrunning_Tattoo_025_M", Name = "Praying Skull" });
                TatThis.Add(new Tattoo { BaseName = "mpgunrunning_overlays", TatName = "MP_Gunrunning_Tattoo_016_M", Name = "Blood Money" });
                TatThis.Add(new Tattoo { BaseName = "mpgunrunning_overlays", TatName = "MP_Gunrunning_Tattoo_015_M", Name = "Spiked Skull" });
                TatThis.Add(new Tattoo { BaseName = "mpgunrunning_overlays", TatName = "MP_Gunrunning_Tattoo_008_M", Name = "Bandolier" });
                TatThis.Add(new Tattoo { BaseName = "mpgunrunning_overlays", TatName = "MP_Gunrunning_Tattoo_004_M", Name = "Sidearm" });

                TatThis.Add(new Tattoo { BaseName = "mpimportexport_overlays", TatName = "MP_MP_ImportExport_Tat_008_M", Name = "Scarlett" });
                TatThis.Add(new Tattoo { BaseName = "mpimportexport_overlays", TatName = "MP_MP_ImportExport_Tat_004_M", Name = "Piston Sleeve" });

                TatThis.Add(new Tattoo { BaseName = "mpbiker_overlays", TatName = "MP_MP_Biker_Tat_055_M", Name = "Poison Scorpion" });
                TatThis.Add(new Tattoo { BaseName = "mpbiker_overlays", TatName = "MP_MP_Biker_Tat_053_M", Name = "Muffler Helmet" });
                TatThis.Add(new Tattoo { BaseName = "mpbiker_overlays", TatName = "MP_MP_Biker_Tat_045_M", Name = "Ride Hard Die Fast" });
                TatThis.Add(new Tattoo { BaseName = "mpbiker_overlays", TatName = "MP_MP_Biker_Tat_035_M", Name = "Chain Fist" });
                TatThis.Add(new Tattoo { BaseName = "mpbiker_overlays", TatName = "MP_MP_Biker_Tat_025_M", Name = "Good Luck" });
                TatThis.Add(new Tattoo { BaseName = "mpbiker_overlays", TatName = "MP_MP_Biker_Tat_024_M", Name = "Live to Ride" });
                TatThis.Add(new Tattoo { BaseName = "mpbiker_overlays", TatName = "MP_MP_Biker_Tat_020_M", Name = "Cranial Rose" });
                TatThis.Add(new Tattoo { BaseName = "mpbiker_overlays", TatName = "MP_MP_Biker_Tat_016_M", Name = "Macabre Tree" });
                TatThis.Add(new Tattoo { BaseName = "mpbiker_overlays", TatName = "MP_MP_Biker_Tat_012_M", Name = "Urban Stunter" });

                TatThis.Add(new Tattoo { BaseName = "mpstunt_overlays", TatName = "MP_MP_Stunt_tat_043_M", Name = "Engine Arm" });
                TatThis.Add(new Tattoo { BaseName = "mpstunt_overlays", TatName = "MP_MP_Stunt_tat_039_M", Name = "Kaboom" });
                TatThis.Add(new Tattoo { BaseName = "mpstunt_overlays", TatName = "MP_MP_Stunt_tat_035_M", Name = "Stuntman's End" });
                TatThis.Add(new Tattoo { BaseName = "mpstunt_overlays", TatName = "MP_MP_Stunt_tat_023_M", Name = "Tanked" });
                TatThis.Add(new Tattoo { BaseName = "mpstunt_overlays", TatName = "MP_MP_Stunt_tat_022_M", Name = "Piston Head" });
                TatThis.Add(new Tattoo { BaseName = "mpstunt_overlays", TatName = "MP_MP_Stunt_tat_008_M", Name = "Moonlight Ride" });
                TatThis.Add(new Tattoo { BaseName = "mpstunt_overlays", TatName = "MP_MP_Stunt_tat_002_M", Name = "Big Cat" });
                TatThis.Add(new Tattoo { BaseName = "mpstunt_overlays", TatName = "MP_MP_Stunt_tat_001_M", Name = "8 Eyed Skull" });

                TatThis.Add(new Tattoo { BaseName = "mplowrider2_overlays", TatName = "MP_LR_Tat_022_M", Name = "My Crazy Life" });
                TatThis.Add(new Tattoo { BaseName = "mplowrider2_overlays", TatName = "MP_LR_Tat_018_M", Name = "Skeleton Party" });
                TatThis.Add(new Tattoo { BaseName = "mplowrider2_overlays", TatName = "MP_LR_Tat_006_M", Name = "Love Hustle" });

                TatThis.Add(new Tattoo { BaseName = "mplowrider_overlays", TatName = "MP_LR_Tat_033_M", Name = "City Sorrow" });//
                TatThis.Add(new Tattoo { BaseName = "mplowrider_overlays", TatName = "MP_LR_Tat_027_M", Name = "Los Santos Life" });//
                TatThis.Add(new Tattoo { BaseName = "mplowrider_overlays", TatName = "MP_LR_Tat_005_M", Name = "No Evil" });//

                TatThis.Add(new Tattoo { BaseName = "mpluxe2_overlays", TatName = "MP_Luxe_Tat_028_M", Name = "Python Skull" });
                TatThis.Add(new Tattoo { BaseName = "mpluxe2_overlays", TatName = "MP_Luxe_Tat_018_M", Name = "Divine Goddess" });
                TatThis.Add(new Tattoo { BaseName = "mpluxe2_overlays", TatName = "MP_Luxe_Tat_016_M", Name = "Egyptian Mural" });
                TatThis.Add(new Tattoo { BaseName = "mpluxe2_overlays", TatName = "MP_Luxe_Tat_005_M", Name = "Fatal Dagger" });


                TatThis.Add(new Tattoo { BaseName = "mpluxe_overlays", TatName = "MP_Luxe_Tat_021_M", Name = "Gabriel" });
                TatThis.Add(new Tattoo { BaseName = "mpluxe_overlays", TatName = "MP_Luxe_Tat_020_M", Name = "Archangel and Mary" });
                TatThis.Add(new Tattoo { BaseName = "mpluxe_overlays", TatName = "MP_Luxe_Tat_009_M", Name = "Floral Symmetry" });

                TatThis.Add(new Tattoo { BaseName = "mpchristmas2_overlays", TatName = "MP_Xmas2_M_Tat_021", Name = "Time's Up Color" });
                TatThis.Add(new Tattoo { BaseName = "mpchristmas2_overlays", TatName = "MP_Xmas2_M_Tat_020", Name = "Time's Up Outline" });
                TatThis.Add(new Tattoo { BaseName = "mpchristmas2_overlays", TatName = "MP_Xmas2_M_Tat_012", Name = "8 Ball Skull" });
                TatThis.Add(new Tattoo { BaseName = "mpchristmas2_overlays", TatName = "MP_Xmas2_M_Tat_010", Name = "Electric Snake" });
                TatThis.Add(new Tattoo { BaseName = "mpchristmas2_overlays", TatName = "MP_Xmas2_M_Tat_000", Name = "Skull Rider" });

                TatThis.Add(new Tattoo { BaseName = "mphipster_overlays", TatName = "FM_Hip_M_Tat_048", Name = "Peace" });
                TatThis.Add(new Tattoo { BaseName = "mphipster_overlays", TatName = "FM_Hip_M_Tat_043", Name = "Triangle White" });
                TatThis.Add(new Tattoo { BaseName = "mphipster_overlays", TatName = "FM_Hip_M_Tat_039", Name = "Sleeve" });
                TatThis.Add(new Tattoo { BaseName = "mphipster_overlays", TatName = "FM_Hip_M_Tat_037", Name = "Sunrise" });
                TatThis.Add(new Tattoo { BaseName = "mphipster_overlays", TatName = "FM_Hip_M_Tat_034", Name = "Stop" });
                TatThis.Add(new Tattoo { BaseName = "mphipster_overlays", TatName = "FM_Hip_M_Tat_028", Name = "Thorny Rose" });
                TatThis.Add(new Tattoo { BaseName = "mphipster_overlays", TatName = "FM_Hip_M_Tat_027", Name = "Padlock" });
                TatThis.Add(new Tattoo { BaseName = "mphipster_overlays", TatName = "FM_Hip_M_Tat_026", Name = "Pizza" });
                TatThis.Add(new Tattoo { BaseName = "mphipster_overlays", TatName = "FM_Hip_M_Tat_016", Name = "Lightning Bolt" });
                TatThis.Add(new Tattoo { BaseName = "mphipster_overlays", TatName = "FM_Hip_M_Tat_015", Name = "Mustache" });
                TatThis.Add(new Tattoo { BaseName = "mphipster_overlays", TatName = "FM_Hip_M_Tat_007", Name = "Bricks" });
                TatThis.Add(new Tattoo { BaseName = "mphipster_overlays", TatName = "FM_Hip_M_Tat_003", Name = "Diamond Sparkle" });

                TatThis.Add(new Tattoo { BaseName = "mpbusiness_overlays", TatName = "MP_Buis_M_LeftArm_001", Name = "All-Seeing Eye" });
                TatThis.Add(new Tattoo { BaseName = "mpbusiness_overlays", TatName = "MP_Buis_M_LeftArm_000", Name = "$100 Bill" });

                TatThis.Add(new Tattoo { BaseName = "mpbeach_overlays", TatName = "MP_Bea_M_LArm_000", Name = "Tiki Tower" });
                TatThis.Add(new Tattoo { BaseName = "mpbeach_overlays", TatName = "MP_Bea_M_LArm_001", Name = "Mermaid L.S." });

                TatThis.Add(new Tattoo { BaseName = "multiplayer_overlays", TatName = "FM_Tat_M_041", Name = "Dope Skull" });
                TatThis.Add(new Tattoo { BaseName = "multiplayer_overlays", TatName = "FM_Tat_M_031", Name = "Lady M" });
                TatThis.Add(new Tattoo { BaseName = "multiplayer_overlays", TatName = "FM_Tat_M_015", Name = "Zodiac Skull" });
                TatThis.Add(new Tattoo { BaseName = "multiplayer_overlays", TatName = "FM_Tat_M_006", Name = "Oriental Mural" });
                TatThis.Add(new Tattoo { BaseName = "multiplayer_overlays", TatName = "FM_Tat_M_005", Name = "Serpents" });

                TatThis.Add(new Tattoo { BaseName = "multiplayer_overlays", TatName = "FM_Tat_Award_M_015", Name = "Racing Brunette" });
                TatThis.Add(new Tattoo { BaseName = "multiplayer_overlays", TatName = "FM_Tat_Award_M_007", Name = "Racing Blonde" });
                TatThis.Add(new Tattoo { BaseName = "multiplayer_overlays", TatName = "FM_Tat_Award_M_001", Name = "Burning Heart" });//50 Death Match Award
                                                                                                                                      //not on list
                TatThis.Add(new Tattoo { BaseName = "mpluxe2_overlays", TatName = "MP_Luxe_Tat_031_M", Name = "Geometric Design" });

                Tatlist.Add(TatThis[RandInt(0, TatThis.Count - 1)]);
                TatThis.Clear();

                TatThis.Add(new Tattoo { BaseName = "mpsum2_overlays", TatName = "MP_Sum2_Tat_011_M", Name = "Nothing Mini About It" });
                TatThis.Add(new Tattoo { BaseName = "mpsum2_overlays", TatName = "MP_Sum2_Tat_012_M", Name = "Snake Revolver" });
                TatThis.Add(new Tattoo { BaseName = "mpsum2_overlays", TatName = "MP_Sum2_Tat_013_M", Name = "Weapon Sleeve" });
                TatThis.Add(new Tattoo { BaseName = "mpsum2_overlays", TatName = "MP_Sum2_Tat_030_M", Name = "Centipede" });
                TatThis.Add(new Tattoo { BaseName = "mpsum2_overlays", TatName = "MP_Sum2_Tat_031_M", Name = "Fleshy Eye" });
                TatThis.Add(new Tattoo { BaseName = "mpsum2_overlays", TatName = "MP_Sum2_Tat_045_M", Name = "Armored Arm" });
                TatThis.Add(new Tattoo { BaseName = "mpsum2_overlays", TatName = "MP_Sum2_Tat_046_M", Name = "Demon Smile" });
                TatThis.Add(new Tattoo { BaseName = "mpsum2_overlays", TatName = "MP_Sum2_Tat_047_M", Name = "Angel & Devil" });
                TatThis.Add(new Tattoo { BaseName = "mpsum2_overlays", TatName = "MP_Sum2_Tat_048_M", Name = "Death Is Certain" });

                TatThis.Add(new Tattoo { BaseName = "mpsecurity_overlays", TatName = "MP_Security_Tat_000_M", Name = "Hood Skeleton" });
                TatThis.Add(new Tattoo { BaseName = "mpsecurity_overlays", TatName = "MP_Security_Tat_005_M", Name = "Peacock" });
                TatThis.Add(new Tattoo { BaseName = "mpsecurity_overlays", TatName = "MP_Security_Tat_007_M", Name = "Ballas 4 Life" });
                TatThis.Add(new Tattoo { BaseName = "mpsecurity_overlays", TatName = "MP_Security_Tat_009_M", Name = "Ascension" });
                TatThis.Add(new Tattoo { BaseName = "mpsecurity_overlays", TatName = "MP_Security_Tat_012_M", Name = "Zombie Rhymes" });
                TatThis.Add(new Tattoo { BaseName = "mpsecurity_overlays", TatName = "MP_Security_Tat_020_M", Name = "Dog Fist" });

                TatThis.Add(new Tattoo { BaseName = "mpheist4_overlays", TatName = "MP_Heist4_Tat_032_M", Name = "K.U.L.T.99.1 FM" });
                TatThis.Add(new Tattoo { BaseName = "mpheist4_overlays", TatName = "MP_Heist4_Tat_031_M", Name = "Octopus Shades" });
                TatThis.Add(new Tattoo { BaseName = "mpheist4_overlays", TatName = "MP_Heist4_Tat_026_M", Name = "Shark Water" });
                TatThis.Add(new Tattoo { BaseName = "mpheist4_overlays", TatName = "MP_Heist4_Tat_012_M", Name = "Still Slipping" });
                TatThis.Add(new Tattoo { BaseName = "mpheist4_overlays", TatName = "MP_Heist4_Tat_011_M", Name = "Soulwax" });
                TatThis.Add(new Tattoo { BaseName = "mpheist4_overlays", TatName = "MP_Heist4_Tat_008_M", Name = "Smiley Glitch" });
                TatThis.Add(new Tattoo { BaseName = "mpheist4_overlays", TatName = "MP_Heist4_Tat_007_M", Name = "Skeleton DJ" });
                TatThis.Add(new Tattoo { BaseName = "mpheist4_overlays", TatName = "MP_Heist4_Tat_006_M", Name = "Music Locker" });
                TatThis.Add(new Tattoo { BaseName = "mpheist4_overlays", TatName = "MP_Heist4_Tat_005_M", Name = "LSUR" });
                TatThis.Add(new Tattoo { BaseName = "mpheist4_overlays", TatName = "MP_Heist4_Tat_003_M", Name = "Lighthouse" });
                TatThis.Add(new Tattoo { BaseName = "mpheist4_overlays", TatName = "MP_Heist4_Tat_002_M", Name = "Jellyfish Shades" });
                TatThis.Add(new Tattoo { BaseName = "mpheist4_overlays", TatName = "MP_Heist4_Tat_001_M", Name = "Tropical Dude" });
                TatThis.Add(new Tattoo { BaseName = "mpheist4_overlays", TatName = "MP_Heist4_Tat_000_M", Name = "Headphone Splat" });

                TatThis.Add(new Tattoo { BaseName = "mpheist3_overlays", TatName = "mpHeist3_Tat_034_M", Name = "LS Monogram" });

                TatThis.Add(new Tattoo { BaseName = "mpvinewood_overlays", TatName = "MP_Vinewood_Tat_028_M", Name = "Skull and Aces" });
                TatThis.Add(new Tattoo { BaseName = "mpvinewood_overlays", TatName = "MP_Vinewood_Tat_025_M", Name = "Queen of Roses" });
                TatThis.Add(new Tattoo { BaseName = "mpvinewood_overlays", TatName = "MP_Vinewood_Tat_018_M", Name = "The Gambler's Life" });
                TatThis.Add(new Tattoo { BaseName = "mpvinewood_overlays", TatName = "MP_Vinewood_Tat_004_M", Name = "Lady Luck" });

                TatThis.Add(new Tattoo { BaseName = "mpchristmas2017_overlays", TatName = "MP_Christmas2017_Tattoo_028_M", Name = "Spartan Mural" });
                TatThis.Add(new Tattoo { BaseName = "mpchristmas2017_overlays", TatName = "MP_Christmas2017_Tattoo_023_M", Name = "Samurai Tallship" });
                TatThis.Add(new Tattoo { BaseName = "mpchristmas2017_overlays", TatName = "MP_Christmas2017_Tattoo_018_M", Name = "Muscle Tear" });
                TatThis.Add(new Tattoo { BaseName = "mpchristmas2017_overlays", TatName = "MP_Christmas2017_Tattoo_017_M", Name = "Feather Sleeve" });
                TatThis.Add(new Tattoo { BaseName = "mpchristmas2017_overlays", TatName = "MP_Christmas2017_Tattoo_014_M", Name = "Celtic Band" });
                TatThis.Add(new Tattoo { BaseName = "mpchristmas2017_overlays", TatName = "MP_Christmas2017_Tattoo_012_M", Name = "Tiger Headdress" });
                TatThis.Add(new Tattoo { BaseName = "mpchristmas2017_overlays", TatName = "MP_Christmas2017_Tattoo_006_M", Name = "Medusa" });

                TatThis.Add(new Tattoo { BaseName = "mpsmuggler_overlays", TatName = "MP_Smuggler_Tattoo_023_M", Name = "Stylized Kraken" });
                TatThis.Add(new Tattoo { BaseName = "mpsmuggler_overlays", TatName = "MP_Smuggler_Tattoo_005_M", Name = "Mutiny" });
                TatThis.Add(new Tattoo { BaseName = "mpsmuggler_overlays", TatName = "MP_Smuggler_Tattoo_001_M", Name = "Crackshot" });

                TatThis.Add(new Tattoo { BaseName = "mpgunrunning_overlays", TatName = "MP_Gunrunning_Tattoo_024_M", Name = "Combat Reaper" });
                TatThis.Add(new Tattoo { BaseName = "mpgunrunning_overlays", TatName = "MP_Gunrunning_Tattoo_021_M", Name = "Have a Nice Day" });
                TatThis.Add(new Tattoo { BaseName = "mpgunrunning_overlays", TatName = "MP_Gunrunning_Tattoo_002_M", Name = "Grenade" });

                TatThis.Add(new Tattoo { BaseName = "mpimportexport_overlays", TatName = "MP_MP_ImportExport_Tat_007_M", Name = "Drive Forever" });
                TatThis.Add(new Tattoo { BaseName = "mpimportexport_overlays", TatName = "MP_MP_ImportExport_Tat_006_M", Name = "Engulfed Block" });
                TatThis.Add(new Tattoo { BaseName = "mpimportexport_overlays", TatName = "MP_MP_ImportExport_Tat_005_M", Name = "Dialed In" });
                TatThis.Add(new Tattoo { BaseName = "mpimportexport_overlays", TatName = "MP_MP_ImportExport_Tat_003_M", Name = "Mechanical Sleeve" });

                TatThis.Add(new Tattoo { BaseName = "mpbiker_overlays", TatName = "MP_MP_Biker_Tat_054_M", Name = "Mum" });
                TatThis.Add(new Tattoo { BaseName = "mpbiker_overlays", TatName = "MP_MP_Biker_Tat_049_M", Name = "These Colors Don't Run" });
                TatThis.Add(new Tattoo { BaseName = "mpbiker_overlays", TatName = "MP_MP_Biker_Tat_047_M", Name = "Snake Bike" });
                TatThis.Add(new Tattoo { BaseName = "mpbiker_overlays", TatName = "MP_MP_Biker_Tat_046_M", Name = "Skull Chain" });
                TatThis.Add(new Tattoo { BaseName = "mpbiker_overlays", TatName = "MP_MP_Biker_Tat_042_M", Name = "Grim Rider" });
                TatThis.Add(new Tattoo { BaseName = "mpbiker_overlays", TatName = "MP_MP_Biker_Tat_033_M", Name = "Eagle Emblem" });
                TatThis.Add(new Tattoo { BaseName = "mpbiker_overlays", TatName = "MP_MP_Biker_Tat_014_M", Name = "Lady Mortality" });
                TatThis.Add(new Tattoo { BaseName = "mpbiker_overlays", TatName = "MP_MP_Biker_Tat_007_M", Name = "Swooping Eagle" });

                TatThis.Add(new Tattoo { BaseName = "mpstunt_overlays", TatName = "MP_MP_Stunt_tat_049_M", Name = "Seductive Mechanic" });
                TatThis.Add(new Tattoo { BaseName = "mpstunt_overlays", TatName = "MP_MP_Stunt_tat_038_M", Name = "One Down Five Up" });
                TatThis.Add(new Tattoo { BaseName = "mpstunt_overlays", TatName = "MP_MP_Stunt_tat_036_M", Name = "Biker Stallion" });
                TatThis.Add(new Tattoo { BaseName = "mpstunt_overlays", TatName = "MP_MP_Stunt_tat_016_M", Name = "Coffin Racer" });
                TatThis.Add(new Tattoo { BaseName = "mpstunt_overlays", TatName = "MP_MP_Stunt_tat_010_M", Name = "Grave Vulture" });
                TatThis.Add(new Tattoo { BaseName = "mpstunt_overlays", TatName = "MP_MP_Stunt_tat_009_M", Name = "Arachnid of Death" });
                TatThis.Add(new Tattoo { BaseName = "mpstunt_overlays", TatName = "MP_MP_Stunt_tat_003_M", Name = "Poison Wrench" });

                TatThis.Add(new Tattoo { BaseName = "mplowrider2_overlays", TatName = "MP_LR_Tat_035_M", Name = "Black Tears" });
                TatThis.Add(new Tattoo { BaseName = "mplowrider2_overlays", TatName = "MP_LR_Tat_028_M", Name = "Loving Los Muertos" });
                TatThis.Add(new Tattoo { BaseName = "mplowrider2_overlays", TatName = "MP_LR_Tat_003_M", Name = "Lady Vamp" });

                TatThis.Add(new Tattoo { BaseName = "mplowrider_overlays", TatName = "MP_LR_Tat_015_M", Name = "Seductress" });

                TatThis.Add(new Tattoo { BaseName = "mpluxe2_overlays", TatName = "MP_LUXE_TAT_026_M", Name = "Floral Print" });
                TatThis.Add(new Tattoo { BaseName = "mpluxe2_overlays", TatName = "MP_LUXE_TAT_017_M", Name = "Heavenly Deity" });
                TatThis.Add(new Tattoo { BaseName = "mpluxe2_overlays", TatName = "MP_LUXE_TAT_010_M", Name = "Intrometric" });

                TatThis.Add(new Tattoo { BaseName = "mpluxe_overlays", TatName = "MP_LUXE_TAT_019_M", Name = "Geisha Bloom" });
                TatThis.Add(new Tattoo { BaseName = "mpluxe_overlays", TatName = "MP_LUXE_TAT_013_M", Name = "Mermaid Harpist" });
                TatThis.Add(new Tattoo { BaseName = "mpluxe_overlays", TatName = "MP_LUXE_TAT_004_M", Name = "Floral Raven" });

                TatThis.Add(new Tattoo { BaseName = "mpchristmas2_overlays", TatName = "MP_Xmas2_M_Tat_027", Name = "Fuck Luck Color" });
                TatThis.Add(new Tattoo { BaseName = "mpchristmas2_overlays", TatName = "MP_Xmas2_M_Tat_026", Name = "Fuck Luck Outline" });
                TatThis.Add(new Tattoo { BaseName = "mpchristmas2_overlays", TatName = "MP_Xmas2_M_Tat_023", Name = "You're Next Color" });
                TatThis.Add(new Tattoo { BaseName = "mpchristmas2_overlays", TatName = "MP_Xmas2_M_Tat_022", Name = "You're Next Outline" });
                TatThis.Add(new Tattoo { BaseName = "mpchristmas2_overlays", TatName = "MP_Xmas2_M_Tat_008", Name = "Death Before Dishonor" });
                TatThis.Add(new Tattoo { BaseName = "mpchristmas2_overlays", TatName = "MP_Xmas2_M_Tat_004", Name = "Snake Shaded" });
                TatThis.Add(new Tattoo { BaseName = "mpchristmas2_overlays", TatName = "MP_Xmas2_M_Tat_003", Name = "Snake Outline" });

                TatThis.Add(new Tattoo { BaseName = "mphipster_overlays", TatName = "FM_Hip_M_Tat_045", Name = "Mesh Band" });
                TatThis.Add(new Tattoo { BaseName = "mphipster_overlays", TatName = "FM_Hip_M_Tat_044", Name = "Triangle Black" });
                TatThis.Add(new Tattoo { BaseName = "mphipster_overlays", TatName = "FM_Hip_M_Tat_036", Name = "Shapes" });
                TatThis.Add(new Tattoo { BaseName = "mphipster_overlays", TatName = "FM_Hip_M_Tat_023", Name = "Smiley" });
                TatThis.Add(new Tattoo { BaseName = "mphipster_overlays", TatName = "FM_Hip_M_Tat_022", Name = "Pencil" });
                TatThis.Add(new Tattoo { BaseName = "mphipster_overlays", TatName = "FM_Hip_M_Tat_020", Name = "Geo Pattern" });
                TatThis.Add(new Tattoo { BaseName = "mphipster_overlays", TatName = "FM_Hip_M_Tat_018", Name = "Origami" });
                TatThis.Add(new Tattoo { BaseName = "mphipster_overlays", TatName = "FM_Hip_M_Tat_017", Name = "Eye Triangle" });
                TatThis.Add(new Tattoo { BaseName = "mphipster_overlays", TatName = "FM_Hip_M_Tat_014", Name = "Spray Can" });
                TatThis.Add(new Tattoo { BaseName = "mphipster_overlays", TatName = "FM_Hip_M_Tat_010", Name = "Horseshoe" });
                TatThis.Add(new Tattoo { BaseName = "mphipster_overlays", TatName = "FM_Hip_M_Tat_008", Name = "Cube" });
                TatThis.Add(new Tattoo { BaseName = "mphipster_overlays", TatName = "FM_Hip_M_Tat_004", Name = "Bone" });
                TatThis.Add(new Tattoo { BaseName = "mphipster_overlays", TatName = "FM_Hip_M_Tat_001", Name = "Single Arrow" });

                TatThis.Add(new Tattoo { BaseName = "mpbusiness_overlays", TatName = "MP_Buis_M_RightArm_001", Name = "Green" });
                TatThis.Add(new Tattoo { BaseName = "mpbusiness_overlays", TatName = "MP_Buis_M_RightArm_000", Name = "Dollar Skull" });

                TatThis.Add(new Tattoo { BaseName = "mpbeach_overlays", TatName = "MP_Bea_M_RArm_001", Name = "Vespucci Beauty" });
                TatThis.Add(new Tattoo { BaseName = "mpbeach_overlays", TatName = "MP_Bea_M_RArm_000", Name = "Tribal Sun" });

                TatThis.Add(new Tattoo { BaseName = "multiplayer_overlays", TatName = "FM_Tat_M_047", Name = "Lion" });
                TatThis.Add(new Tattoo { BaseName = "multiplayer_overlays", TatName = "FM_Tat_M_038", Name = "Dagger" });
                TatThis.Add(new Tattoo { BaseName = "multiplayer_overlays", TatName = "FM_Tat_M_028", Name = "Mermaid" });
                TatThis.Add(new Tattoo { BaseName = "multiplayer_overlays", TatName = "FM_Tat_M_027", Name = "Virgin Mary" });
                TatThis.Add(new Tattoo { BaseName = "multiplayer_overlays", TatName = "FM_Tat_M_018", Name = "Serpent Skull" });
                TatThis.Add(new Tattoo { BaseName = "multiplayer_overlays", TatName = "FM_Tat_M_014", Name = "Flower Mural" });
                TatThis.Add(new Tattoo { BaseName = "multiplayer_overlays", TatName = "FM_Tat_M_003", Name = "Dragons and Skull" });
                TatThis.Add(new Tattoo { BaseName = "multiplayer_overlays", TatName = "FM_Tat_M_001", Name = "Dragons" });
                TatThis.Add(new Tattoo { BaseName = "multiplayer_overlays", TatName = "FM_Tat_M_000", Name = "Brotherhood" });

                TatThis.Add(new Tattoo { BaseName = "multiplayer_overlays", TatName = "FM_Tat_Award_M_010", Name = "Ride or Die" });
                TatThis.Add(new Tattoo { BaseName = "multiplayer_overlays", TatName = "FM_Tat_Award_M_002", Name = "Grim Reaper Smoking Gun" });
                //Not In List
                TatThis.Add(new Tattoo { BaseName = "mpluxe2_overlays", TatName = "MP_LUXE_TAT_030_M", Name = "Geometric Design" });

                Tatlist.Add(TatThis[RandInt(0, TatThis.Count - 1)]);
                TatThis.Clear();

                TatThis.Add(new Tattoo { BaseName = "mpsum2_overlays", TatName = "MP_Sum2_Tat_002_M", Name = "Cobra Biker" });
                TatThis.Add(new Tattoo { BaseName = "mpsum2_overlays", TatName = "MP_Sum2_Tat_014_M", Name = "Minimal SMG" });
                TatThis.Add(new Tattoo { BaseName = "mpsum2_overlays", TatName = "MP_Sum2_Tat_015_M", Name = "Minimal Advanced Rifle" });
                TatThis.Add(new Tattoo { BaseName = "mpsum2_overlays", TatName = "MP_Sum2_Tat_016_M", Name = "Minimal Sniper Rifle" });
                TatThis.Add(new Tattoo { BaseName = "mpsum2_overlays", TatName = "MP_Sum2_Tat_032_M", Name = "Many-eyed Goat" });
                TatThis.Add(new Tattoo { BaseName = "mpsum2_overlays", TatName = "MP_Sum2_Tat_053_M", Name = "Mobster Skull" });
                TatThis.Add(new Tattoo { BaseName = "mpsum2_overlays", TatName = "MP_Sum2_Tat_054_M", Name = "Wounded Head" });
                TatThis.Add(new Tattoo { BaseName = "mpsum2_overlays", TatName = "MP_Sum2_Tat_055_M", Name = "Stabbed Skull" });
                TatThis.Add(new Tattoo { BaseName = "mpsum2_overlays", TatName = "MP_Sum2_Tat_056_M", Name = "Tiger Blade" });

                TatThis.Add(new Tattoo { BaseName = "mpsecurity_overlays", TatName = "MP_Security_Tat_022_M", Name = "LS Smoking Cartridges" });
                TatThis.Add(new Tattoo { BaseName = "mpsecurity_overlays", TatName = "MP_Security_Tat_023_M", Name = "Trust" });

                TatThis.Add(new Tattoo { BaseName = "mpheist4_overlays", TatName = "MP_Heist4_Tat_029_M", Name = "Soundwaves" });
                TatThis.Add(new Tattoo { BaseName = "mpheist4_overlays", TatName = "MP_Heist4_Tat_028_M", Name = "Skull Waters" });
                TatThis.Add(new Tattoo { BaseName = "mpheist4_overlays", TatName = "MP_Heist4_Tat_025_M", Name = "Glow Princess" });
                TatThis.Add(new Tattoo { BaseName = "mpheist4_overlays", TatName = "MP_Heist4_Tat_024_M", Name = "Pineapple Skull" });
                TatThis.Add(new Tattoo { BaseName = "mpheist4_overlays", TatName = "MP_Heist4_Tat_010_M", Name = "Tropical Serpent" });

                TatThis.Add(new Tattoo { BaseName = "mpheist3_overlays", TatName = "mpHeist3_Tat_032_M", Name = "Love Fist" });

                TatThis.Add(new Tattoo { BaseName = "mpvinewood_overlays", TatName = "MP_Vinewood_Tat_027_M", Name = "8-Ball Rose" });//
                TatThis.Add(new Tattoo { BaseName = "mpvinewood_overlays", TatName = "MP_Vinewood_Tat_013_M", Name = "One-armed Bandit" });//

                TatThis.Add(new Tattoo { BaseName = "mpgunrunning_overlays", TatName = "MP_Gunrunning_Tattoo_023_M", Name = "Rose Revolver" });
                TatThis.Add(new Tattoo { BaseName = "mpgunrunning_overlays", TatName = "MP_Gunrunning_Tattoo_011_M", Name = "Death Skull" });
                TatThis.Add(new Tattoo { BaseName = "mpgunrunning_overlays", TatName = "MP_Gunrunning_Tattoo_007_M", Name = "Stylized Tiger" });
                TatThis.Add(new Tattoo { BaseName = "mpgunrunning_overlays", TatName = "MP_Gunrunning_Tattoo_005_M", Name = "Patriot Skull" });

                TatThis.Add(new Tattoo { BaseName = "mpbiker_overlays", TatName = "MP_MP_Biker_Tat_057_M", Name = "Laughing Skull" });
                TatThis.Add(new Tattoo { BaseName = "mpbiker_overlays", TatName = "MP_MP_Biker_Tat_056_M", Name = "Bone Cruiser" });
                TatThis.Add(new Tattoo { BaseName = "mpbiker_overlays", TatName = "MP_MP_Biker_Tat_044_M", Name = "Ride Free" });
                TatThis.Add(new Tattoo { BaseName = "mpbiker_overlays", TatName = "MP_MP_Biker_Tat_037_M", Name = "Scorched Soul" });
                TatThis.Add(new Tattoo { BaseName = "mpbiker_overlays", TatName = "MP_MP_Biker_Tat_036_M", Name = "Engulfed Skull" });
                TatThis.Add(new Tattoo { BaseName = "mpbiker_overlays", TatName = "MP_MP_Biker_Tat_027_M", Name = "Bad Luck" });
                TatThis.Add(new Tattoo { BaseName = "mpbiker_overlays", TatName = "MP_MP_Biker_Tat_015_M", Name = "Ride or Die" });
                TatThis.Add(new Tattoo { BaseName = "mpbiker_overlays", TatName = "MP_MP_Biker_Tat_002_M", Name = "Rose Tribute" });

                TatThis.Add(new Tattoo { BaseName = "mpstunt_overlays", TatName = "MP_MP_Stunt_tat_031_M", Name = "Stunt Jesus" });
                TatThis.Add(new Tattoo { BaseName = "mpstunt_overlays", TatName = "MP_MP_Stunt_tat_028_M", Name = "Quad Goblin" });
                TatThis.Add(new Tattoo { BaseName = "mpstunt_overlays", TatName = "MP_MP_Stunt_tat_021_M", Name = "Golden Cobra" });
                TatThis.Add(new Tattoo { BaseName = "mpstunt_overlays", TatName = "MP_MP_Stunt_tat_013_M", Name = "Dirt Track Hero" });
                TatThis.Add(new Tattoo { BaseName = "mpstunt_overlays", TatName = "MP_MP_Stunt_tat_007_M", Name = "Dagger Devil" });

                TatThis.Add(new Tattoo { BaseName = "mplowrider2_overlays", TatName = "MP_LR_Tat_029_M", Name = "Death Us Do Part" });

                TatThis.Add(new Tattoo { BaseName = "mplowrider_overlays", TatName = "MP_LR_Tat_020_M", Name = "Presidents" });//
                TatThis.Add(new Tattoo { BaseName = "mplowrider_overlays", TatName = "MP_LR_Tat_007_M", Name = "LS Serpent" });//

                TatThis.Add(new Tattoo { BaseName = "mpluxe2_overlays", TatName = "MP_Luxe_Tat_011_M", Name = "Cross of Roses" });
                TatThis.Add(new Tattoo { BaseName = "mpluxe_overlays", TatName = "MP_LUXE_TAT_000_M", Name = "Serpent of Death" });

                TatThis.Add(new Tattoo { BaseName = "mpchristmas2_overlays", TatName = "MP_Xmas2_M_Tat_002", Name = "Spider Color" });
                TatThis.Add(new Tattoo { BaseName = "mpchristmas2_overlays", TatName = "MP_Xmas2_M_Tat_001", Name = "Spider Outline" });

                TatThis.Add(new Tattoo { BaseName = "mphipster_overlays", TatName = "FM_Hip_M_Tat_040", Name = "Black Anchor" });
                TatThis.Add(new Tattoo { BaseName = "mphipster_overlays", TatName = "FM_Hip_M_Tat_019", Name = "Charm" });
                TatThis.Add(new Tattoo { BaseName = "mphipster_overlays", TatName = "FM_Hip_M_Tat_009", Name = "Squares" });

                TatThis.Add(new Tattoo { BaseName = "mpbeach_overlays", TatName = "MP_Bea_M_Lleg_000", Name = "Tribal Star" });

                TatThis.Add(new Tattoo { BaseName = "multiplayer_overlays", TatName = "FM_Tat_M_032", Name = "Faith" });
                TatThis.Add(new Tattoo { BaseName = "multiplayer_overlays", TatName = "FM_Tat_M_037", Name = "Grim Reaper" });
                TatThis.Add(new Tattoo { BaseName = "multiplayer_overlays", TatName = "FM_Tat_M_035", Name = "Dragon" });
                TatThis.Add(new Tattoo { BaseName = "multiplayer_overlays", TatName = "FM_Tat_M_033", Name = "Chinese Dragon" });
                TatThis.Add(new Tattoo { BaseName = "multiplayer_overlays", TatName = "FM_Tat_M_026", Name = "Smoking Dagger" });
                TatThis.Add(new Tattoo { BaseName = "multiplayer_overlays", TatName = "FM_Tat_M_023", Name = "Hottie" });
                TatThis.Add(new Tattoo { BaseName = "multiplayer_overlays", TatName = "FM_Tat_M_021", Name = "Serpent Skull" });
                TatThis.Add(new Tattoo { BaseName = "multiplayer_overlays", TatName = "FM_Tat_M_008", Name = "Dragon Mural" });
                TatThis.Add(new Tattoo { BaseName = "multiplayer_overlays", TatName = "FM_Tat_M_002", Name = "Melting Skull" });

                TatThis.Add(new Tattoo { BaseName = "multiplayer_overlays", TatName = "FM_Tat_Award_M_009", Name = "Dragon and Dagger" });

                Tatlist.Add(TatThis[RandInt(0, TatThis.Count - 1)]);
                TatThis.Clear();

                TatThis.Add(new Tattoo { BaseName = "mpsum2_overlays", TatName = "MP_Sum2_Tat_017_M", Name = "Skull Grenade" });
                TatThis.Add(new Tattoo { BaseName = "mpsum2_overlays", TatName = "MP_Sum2_Tat_033_M", Name = "Three-eyed Demon" });
                TatThis.Add(new Tattoo { BaseName = "mpsum2_overlays", TatName = "MP_Sum2_Tat_034_M", Name = "Smoldering Reaper" });
                TatThis.Add(new Tattoo { BaseName = "mpsum2_overlays", TatName = "MP_Sum2_Tat_050_M", Name = "Gold Gun" });
                TatThis.Add(new Tattoo { BaseName = "mpsum2_overlays", TatName = "MP_Sum2_Tat_051_M", Name = "Blue Serpent" });
                TatThis.Add(new Tattoo { BaseName = "mpsum2_overlays", TatName = "MP_Sum2_Tat_052_M", Name = "Night Demon" });

                TatThis.Add(new Tattoo { BaseName = "mpsecurity_overlays", TatName = "MP_Security_Tat_003_M", Name = "Bandana Knife" });
                TatThis.Add(new Tattoo { BaseName = "mpsecurity_overlays", TatName = "MP_Security_Tat_021_M", Name = "Graffiti Skull" });

                TatThis.Add(new Tattoo { BaseName = "mpheist4_overlays", TatName = "MP_Heist4_Tat_027_M", Name = "Skullphones" });

                TatThis.Add(new Tattoo { BaseName = "mpheist3_overlays", TatName = "mpHeist3_Tat_031_M", Name = "Kifflom" });

                TatThis.Add(new Tattoo { BaseName = "mpvinewood_overlays", TatName = "MP_Vinewood_Tat_020_M", Name = "Cash is King" });

                TatThis.Add(new Tattoo { BaseName = "mpsmuggler_overlays", TatName = "MP_Smuggler_Tattoo_020_M", Name = "Homeward Bound" });

                TatThis.Add(new Tattoo { BaseName = "mpgunrunning_overlays", TatName = "MP_Gunrunning_Tattoo_030_M", Name = "Pistol Ace" });
                TatThis.Add(new Tattoo { BaseName = "mpgunrunning_overlays", TatName = "MP_Gunrunning_Tattoo_026_M", Name = "Restless Skull" });
                TatThis.Add(new Tattoo { BaseName = "mpgunrunning_overlays", TatName = "MP_Gunrunning_Tattoo_006_M", Name = "Combat Skull" });

                TatThis.Add(new Tattoo { BaseName = "mpbiker_overlays", TatName = "MP_MP_Biker_Tat_048_M", Name = "STFU" });
                TatThis.Add(new Tattoo { BaseName = "mpbiker_overlays", TatName = "MP_MP_Biker_Tat_040_M", Name = "American Made" });
                TatThis.Add(new Tattoo { BaseName = "mpbiker_overlays", TatName = "MP_MP_Biker_Tat_028_M", Name = "Dusk Rider" });
                TatThis.Add(new Tattoo { BaseName = "mpbiker_overlays", TatName = "MP_MP_Biker_Tat_022_M", Name = "Western Insignia" });
                TatThis.Add(new Tattoo { BaseName = "mpbiker_overlays", TatName = "MP_MP_Biker_Tat_004_M", Name = "Dragon's Fury" });

                TatThis.Add(new Tattoo { BaseName = "mpstunt_overlays", TatName = "MP_MP_Stunt_tat_047_M", Name = "Brake Knife" });
                TatThis.Add(new Tattoo { BaseName = "mpstunt_overlays", TatName = "MP_MP_Stunt_tat_045_M", Name = "Severed Hand" });
                TatThis.Add(new Tattoo { BaseName = "mpstunt_overlays", TatName = "MP_MP_Stunt_tat_032_M", Name = "Wheelie Mouse" });
                TatThis.Add(new Tattoo { BaseName = "mpstunt_overlays", TatName = "MP_MP_Stunt_tat_025_M", Name = "Speed Freak" });
                TatThis.Add(new Tattoo { BaseName = "mpstunt_overlays", TatName = "MP_MP_Stunt_tat_020_M", Name = "Piston Angel" });
                TatThis.Add(new Tattoo { BaseName = "mpstunt_overlays", TatName = "MP_MP_Stunt_tat_015_M", Name = "Praying Gloves" });
                TatThis.Add(new Tattoo { BaseName = "mpstunt_overlays", TatName = "MP_MP_Stunt_tat_005_M", Name = "Demon Spark Plug" });

                TatThis.Add(new Tattoo { BaseName = "mplowrider2_overlays", TatName = "MP_LR_Tat_030_M", Name = "San Andreas Prayer" });

                TatThis.Add(new Tattoo { BaseName = "mplowrider_overlays", TatName = "MP_LR_Tat_023_M", Name = "Dance of Hearts" });
                TatThis.Add(new Tattoo { BaseName = "mplowrider_overlays", TatName = "MP_LR_Tat_017_M", Name = "Ink Me" });

                TatThis.Add(new Tattoo { BaseName = "mpluxe2_overlays", TatName = "MP_LUXE_TAT_023_M", Name = "Starmetric" });

                TatThis.Add(new Tattoo { BaseName = "mpluxe_overlays", TatName = "MP_LUXE_TAT_001_M", Name = "Elaborate Los Muertos" });

                TatThis.Add(new Tattoo { BaseName = "mpchristmas2_overlays", TatName = "MP_Xmas2_M_Tat_014", Name = "Floral Dagger" });

                TatThis.Add(new Tattoo { BaseName = "mphipster_overlays", TatName = "FM_Hip_M_Tat_042", Name = "Sparkplug" });
                TatThis.Add(new Tattoo { BaseName = "mphipster_overlays", TatName = "FM_Hip_M_Tat_038", Name = "Grub" });

                TatThis.Add(new Tattoo { BaseName = "mpbeach_overlays", TatName = "MP_Bea_M_Rleg_000", Name = "Tribal Tiki Tower" });

                TatThis.Add(new Tattoo { BaseName = "multiplayer_overlays", TatName = "FM_Tat_M_043", Name = "Indian Ram" });
                TatThis.Add(new Tattoo { BaseName = "multiplayer_overlays", TatName = "FM_Tat_M_042", Name = "Flaming Scorpion" });
                TatThis.Add(new Tattoo { BaseName = "multiplayer_overlays", TatName = "FM_Tat_M_040", Name = "Flaming Skull" });
                TatThis.Add(new Tattoo { BaseName = "multiplayer_overlays", TatName = "FM_Tat_M_039", Name = "Broken Skull" });
                TatThis.Add(new Tattoo { BaseName = "multiplayer_overlays", TatName = "FM_Tat_M_022", Name = "Fiery Dragon" });
                TatThis.Add(new Tattoo { BaseName = "multiplayer_overlays", TatName = "FM_Tat_M_017", Name = "Tribal" });
                TatThis.Add(new Tattoo { BaseName = "multiplayer_overlays", TatName = "FM_Tat_M_007", Name = "The Warrior" });

                TatThis.Add(new Tattoo { BaseName = "multiplayer_overlays", TatName = "FM_Tat_Award_M_006", Name = "Skull and Sword" });

                Tatlist.Add(TatThis[RandInt(0, TatThis.Count - 1)]);
                TatThis.Clear();
            }
            else
            {
                List<Tattoo> TatThis = new List<Tattoo>();

                TatThis.Add(new Tattoo { BaseName = "mpsum2_overlays", TatName = "MP_Sum2_Tat_006_F", Name = "Painted Micro SMG" });
                TatThis.Add(new Tattoo { BaseName = "mpsum2_overlays", TatName = "MP_Sum2_Tat_007_F", Name = "Weapon King" });
                TatThis.Add(new Tattoo { BaseName = "mpsum2_overlays", TatName = "MP_Sum2_Tat_035_F", Name = "Sniff Sniff" });
                TatThis.Add(new Tattoo { BaseName = "mpsum2_overlays", TatName = "MP_Sum2_Tat_036_F", Name = "Charm Pattern" });
                TatThis.Add(new Tattoo { BaseName = "mpsum2_overlays", TatName = "MP_Sum2_Tat_037_F", Name = "Witch & Skull" });
                TatThis.Add(new Tattoo { BaseName = "mpsum2_overlays", TatName = "MP_Sum2_Tat_038_F", Name = "Pumpkin Bug" });
                TatThis.Add(new Tattoo { BaseName = "mpsum2_overlays", TatName = "MP_Sum2_Tat_039_F", Name = "Sinner" });
                TatThis.Add(new Tattoo { BaseName = "mpsum2_overlays", TatName = "MP_Sum2_Tat_057_F", Name = "Gray Demon" });

                TatThis.Add(new Tattoo { BaseName = "mpsecurity_overlays", TatName = "MP_Security_Tat_004_F", Name = "Hood Heart" });
                TatThis.Add(new Tattoo { BaseName = "mpsecurity_overlays", TatName = "MP_Security_Tat_008_F", Name = "Los Santos Tag" });
                TatThis.Add(new Tattoo { BaseName = "mpsecurity_overlays", TatName = "MP_Security_Tat_013_F", Name = "Blessed Boombox" });
                TatThis.Add(new Tattoo { BaseName = "mpsecurity_overlays", TatName = "MP_Security_Tat_014_F", Name = "Chamberlain Hills" });
                TatThis.Add(new Tattoo { BaseName = "mpsecurity_overlays", TatName = "MP_Security_Tat_015_F", Name = "Smoking Barrels" });
                TatThis.Add(new Tattoo { BaseName = "mpsecurity_overlays", TatName = "MP_Security_Tat_026_F", Name = "Dollar Guns Crossed" });

                TatThis.Add(new Tattoo { BaseName = "mpheist4_overlays", TatName = "MP_Heist4_Tat_021_F", Name = "Skull Surfer" });//
                TatThis.Add(new Tattoo { BaseName = "mpheist4_overlays", TatName = "MP_Heist4_Tat_020_F", Name = "Speaker Tower" });//
                TatThis.Add(new Tattoo { BaseName = "mpheist4_overlays", TatName = "MP_Heist4_Tat_019_F", Name = "Record Shot" });//
                TatThis.Add(new Tattoo { BaseName = "mpheist4_overlays", TatName = "MP_Heist4_Tat_018_F", Name = "Record Head" });//
                TatThis.Add(new Tattoo { BaseName = "mpheist4_overlays", TatName = "MP_Heist4_Tat_017_F", Name = "Tropical Sorcerer" });//
                TatThis.Add(new Tattoo { BaseName = "mpheist4_overlays", TatName = "MP_Heist4_Tat_016_F", Name = "Rose Panther" });//
                TatThis.Add(new Tattoo { BaseName = "mpheist4_overlays", TatName = "MP_Heist4_Tat_015_F", Name = "Paradise Ukulele" });//
                TatThis.Add(new Tattoo { BaseName = "mpheist4_overlays", TatName = "MP_Heist4_Tat_014_F", Name = "Paradise Nap" });//
                TatThis.Add(new Tattoo { BaseName = "mpheist4_overlays", TatName = "MP_Heist4_Tat_013_F", Name = "Wild Dancers" });//

                TatThis.Add(new Tattoo { BaseName = "mpheist3_overlays", TatName = "mpHeist3_Tat_039_F", Name = "Space Rangers" });//
                TatThis.Add(new Tattoo { BaseName = "mpheist3_overlays", TatName = "mpHeist3_Tat_038_F", Name = "Robot Bubblegum" });//
                TatThis.Add(new Tattoo { BaseName = "mpheist3_overlays", TatName = "mpHeist3_Tat_036_F", Name = "LS Shield" });//
                TatThis.Add(new Tattoo { BaseName = "mpheist3_overlays", TatName = "mpHeist3_Tat_030_F", Name = "Howitzer" });//
                TatThis.Add(new Tattoo { BaseName = "mpheist3_overlays", TatName = "mpHeist3_Tat_028_F", Name = "Bananas Gone Bad" });//
                TatThis.Add(new Tattoo { BaseName = "mpheist3_overlays", TatName = "mpHeist3_Tat_027_F", Name = "Epsilon" });//
                TatThis.Add(new Tattoo { BaseName = "mpheist3_overlays", TatName = "mpHeist3_Tat_024_F", Name = "Mount Chiliad" });//
                TatThis.Add(new Tattoo { BaseName = "mpheist3_overlays", TatName = "mpHeist3_Tat_023_F", Name = "Bigfoot" });//

                TatThis.Add(new Tattoo { BaseName = "mpvinewood_overlays", TatName = "mp_vinewood_tat_032_F", Name = "Play Your Ace" });//
                TatThis.Add(new Tattoo { BaseName = "mpvinewood_overlays", TatName = "MP_Vinewood_Tat_029_F", Name = "The Table" });//
                TatThis.Add(new Tattoo { BaseName = "mpvinewood_overlays", TatName = "MP_Vinewood_Tat_021_F", Name = "Show Your Hand" });//
                TatThis.Add(new Tattoo { BaseName = "mpvinewood_overlays", TatName = "MP_Vinewood_Tat_017_F", Name = "Roll the Dice" });//
                TatThis.Add(new Tattoo { BaseName = "mpvinewood_overlays", TatName = "MP_Vinewood_Tat_015_F", Name = "The Jolly Joker" });//
                TatThis.Add(new Tattoo { BaseName = "mpvinewood_overlays", TatName = "MP_Vinewood_Tat_011_F", Name = "Life's a Gamble" });//
                TatThis.Add(new Tattoo { BaseName = "mpvinewood_overlays", TatName = "MP_Vinewood_Tat_010_F", Name = "Photo Finish" });//
                TatThis.Add(new Tattoo { BaseName = "mpvinewood_overlays", TatName = "MP_Vinewood_Tat_009_F", Name = "Till Death Do Us Part" });//
                TatThis.Add(new Tattoo { BaseName = "mpvinewood_overlays", TatName = "MP_Vinewood_Tat_008_F", Name = "Snake Eyes" });//
                TatThis.Add(new Tattoo { BaseName = "mpvinewood_overlays", TatName = "MP_Vinewood_Tat_007_F", Name = "777" });//
                TatThis.Add(new Tattoo { BaseName = "mpvinewood_overlays", TatName = "MP_Vinewood_Tat_006_F", Name = "Wheel of Suits" });//
                TatThis.Add(new Tattoo { BaseName = "mpvinewood_overlays", TatName = "MP_Vinewood_Tat_001_F", Name = "Jackpot" });//

                TatThis.Add(new Tattoo { BaseName = "mpchristmas2017_overlays", TatName = "MP_Christmas2017_Tattoo_027_F", Name = "Molon Labe" });
                TatThis.Add(new Tattoo { BaseName = "mpchristmas2017_overlays", TatName = "MP_Christmas2017_Tattoo_024_F", Name = "Dragon Slayer" });
                TatThis.Add(new Tattoo { BaseName = "mpchristmas2017_overlays", TatName = "MP_Christmas2017_Tattoo_022_F", Name = "Spartan and Horse" });
                TatThis.Add(new Tattoo { BaseName = "mpchristmas2017_overlays", TatName = "MP_Christmas2017_Tattoo_021_F", Name = "Spartan and Lion" });
                TatThis.Add(new Tattoo { BaseName = "mpchristmas2017_overlays", TatName = "MP_Christmas2017_Tattoo_016_F", Name = "Odin and Raven" });
                TatThis.Add(new Tattoo { BaseName = "mpchristmas2017_overlays", TatName = "MP_Christmas2017_Tattoo_015_F", Name = "Samurai Combat" });
                TatThis.Add(new Tattoo { BaseName = "mpchristmas2017_overlays", TatName = "MP_Christmas2017_Tattoo_011_F", Name = "Weathered Skull" });
                TatThis.Add(new Tattoo { BaseName = "mpchristmas2017_overlays", TatName = "MP_Christmas2017_Tattoo_010_F", Name = "Spartan Shield" });
                TatThis.Add(new Tattoo { BaseName = "mpchristmas2017_overlays", TatName = "MP_Christmas2017_Tattoo_009_F", Name = "Norse Rune" });
                TatThis.Add(new Tattoo { BaseName = "mpchristmas2017_overlays", TatName = "MP_Christmas2017_Tattoo_005_F", Name = "Ghost Dragon" });
                TatThis.Add(new Tattoo { BaseName = "mpchristmas2017_overlays", TatName = "MP_Christmas2017_Tattoo_002_F", Name = "Kabuto" });

                TatThis.Add(new Tattoo { BaseName = "mpsmuggler_overlays", TatName = "MP_Smuggler_Tattoo_025_F", Name = "Claimed By The Beast" });
                TatThis.Add(new Tattoo { BaseName = "mpsmuggler_overlays", TatName = "MP_Smuggler_Tattoo_024_F", Name = "Pirate Captain" });
                TatThis.Add(new Tattoo { BaseName = "mpsmuggler_overlays", TatName = "MP_Smuggler_Tattoo_022_F", Name = "X Marks The Spot" });
                TatThis.Add(new Tattoo { BaseName = "mpsmuggler_overlays", TatName = "MP_Smuggler_Tattoo_018_F", Name = "Finders Keepers" });
                TatThis.Add(new Tattoo { BaseName = "mpsmuggler_overlays", TatName = "MP_Smuggler_Tattoo_017_F", Name = "Framed Tall Ship" });
                TatThis.Add(new Tattoo { BaseName = "mpsmuggler_overlays", TatName = "MP_Smuggler_Tattoo_016_F", Name = "Skull Compass" });
                TatThis.Add(new Tattoo { BaseName = "mpsmuggler_overlays", TatName = "MP_Smuggler_Tattoo_013_F", Name = "Torn Wings" });
                TatThis.Add(new Tattoo { BaseName = "mpsmuggler_overlays", TatName = "MP_Smuggler_Tattoo_009_F", Name = "Tall Ship Conflict" });
                TatThis.Add(new Tattoo { BaseName = "mpsmuggler_overlays", TatName = "MP_Smuggler_Tattoo_006_F", Name = "Never Surrender" });
                TatThis.Add(new Tattoo { BaseName = "mpsmuggler_overlays", TatName = "MP_Smuggler_Tattoo_003_F", Name = "Give Nothing Back" });

                TatThis.Add(new Tattoo { BaseName = "mpairraces_overlays", TatName = "MP_Airraces_Tattoo_007_F", Name = "Eagle Eyes" });
                TatThis.Add(new Tattoo { BaseName = "mpairraces_overlays", TatName = "MP_Airraces_Tattoo_005_F", Name = "Parachute Belle" });
                TatThis.Add(new Tattoo { BaseName = "mpairraces_overlays", TatName = "MP_Airraces_Tattoo_004_F", Name = "Balloon Pioneer" });
                TatThis.Add(new Tattoo { BaseName = "mpairraces_overlays", TatName = "MP_Airraces_Tattoo_002_F", Name = "Winged Bombshell" });
                TatThis.Add(new Tattoo { BaseName = "mpairraces_overlays", TatName = "MP_Airraces_Tattoo_001_F", Name = "Pilot Skull" });

                TatThis.Add(new Tattoo { BaseName = "mpgunrunning_overlays", TatName = "MP_Gunrunning_Tattoo_022_F", Name = "Explosive Heart" });
                TatThis.Add(new Tattoo { BaseName = "mpgunrunning_overlays", TatName = "MP_Gunrunning_Tattoo_019_F", Name = "Pistol Wings" });
                TatThis.Add(new Tattoo { BaseName = "mpgunrunning_overlays", TatName = "MP_Gunrunning_Tattoo_018_F", Name = "Dual Wield Skull" });
                TatThis.Add(new Tattoo { BaseName = "mpgunrunning_overlays", TatName = "MP_Gunrunning_Tattoo_014_F", Name = "Backstabber" });
                TatThis.Add(new Tattoo { BaseName = "mpgunrunning_overlays", TatName = "MP_Gunrunning_Tattoo_013_F", Name = "Wolf Insignia" });
                TatThis.Add(new Tattoo { BaseName = "mpgunrunning_overlays", TatName = "MP_Gunrunning_Tattoo_009_F", Name = "Butterfly Knife" });
                TatThis.Add(new Tattoo { BaseName = "mpgunrunning_overlays", TatName = "MP_Gunrunning_Tattoo_001_F", Name = "Crossed Weapons" });
                TatThis.Add(new Tattoo { BaseName = "mpgunrunning_overlays", TatName = "MP_Gunrunning_Tattoo_000_F", Name = "Bullet Proof" });

                TatThis.Add(new Tattoo { BaseName = "mpimportexport_overlays", TatName = "MP_MP_ImportExport_Tat_011_F", Name = "Talk Shit Get Hit" });
                TatThis.Add(new Tattoo { BaseName = "mpimportexport_overlays", TatName = "MP_MP_ImportExport_Tat_010_F", Name = "Take the Wheel" });
                TatThis.Add(new Tattoo { BaseName = "mpimportexport_overlays", TatName = "MP_MP_ImportExport_Tat_009_F", Name = "Serpents of Destruction" });
                TatThis.Add(new Tattoo { BaseName = "mpimportexport_overlays", TatName = "MP_MP_ImportExport_Tat_002_F", Name = "Tuned to Death" });
                TatThis.Add(new Tattoo { BaseName = "mpimportexport_overlays", TatName = "MP_MP_ImportExport_Tat_001_F", Name = "Power Plant" });
                TatThis.Add(new Tattoo { BaseName = "mpimportexport_overlays", TatName = "MP_MP_ImportExport_Tat_000_F", Name = "Block Back" });

                TatThis.Add(new Tattoo { BaseName = "mpbiker_overlays", TatName = "MP_MP_Biker_Tat_043_F", Name = "Ride Forever" });
                TatThis.Add(new Tattoo { BaseName = "mpbiker_overlays", TatName = "MP_MP_Biker_Tat_030_F", Name = "Brothers For Life" });
                TatThis.Add(new Tattoo { BaseName = "mpbiker_overlays", TatName = "MP_MP_Biker_Tat_021_F", Name = "Flaming Reaper" });
                TatThis.Add(new Tattoo { BaseName = "mpbiker_overlays", TatName = "MP_MP_Biker_Tat_017_F", Name = "Clawed Beast" });
                TatThis.Add(new Tattoo { BaseName = "mpbiker_overlays", TatName = "MP_MP_Biker_Tat_011_F", Name = "R.I.P. My Brothers" });
                TatThis.Add(new Tattoo { BaseName = "mpbiker_overlays", TatName = "MP_MP_Biker_Tat_008_F", Name = "Freedom Wheels" });
                TatThis.Add(new Tattoo { BaseName = "mpbiker_overlays", TatName = "MP_MP_Biker_Tat_006_F", Name = "Chopper Freedom" });

                TatThis.Add(new Tattoo { BaseName = "mpstunt_overlays", TatName = "MP_MP_Stunt_tat_048_F", Name = "Racing Doll" });
                TatThis.Add(new Tattoo { BaseName = "mpstunt_overlays", TatName = "MP_MP_Stunt_tat_046_F", Name = "Full Throttle" });
                TatThis.Add(new Tattoo { BaseName = "mpstunt_overlays", TatName = "MP_MP_Stunt_tat_041_F", Name = "Brapp" });
                TatThis.Add(new Tattoo { BaseName = "mpstunt_overlays", TatName = "MP_MP_Stunt_tat_040_F", Name = "Monkey Chopper" });
                TatThis.Add(new Tattoo { BaseName = "mpstunt_overlays", TatName = "MP_MP_Stunt_tat_037_F", Name = "Big Grills" });
                TatThis.Add(new Tattoo { BaseName = "mpstunt_overlays", TatName = "MP_MP_Stunt_tat_034_F", Name = "Feather Road Kill" });
                TatThis.Add(new Tattoo { BaseName = "mpstunt_overlays", TatName = "MP_MP_Stunt_tat_030_F", Name = "Man's Ruin" });
                TatThis.Add(new Tattoo { BaseName = "mpstunt_overlays", TatName = "MP_MP_Stunt_tat_029_F", Name = "Majestic Finish" });
                TatThis.Add(new Tattoo { BaseName = "mpstunt_overlays", TatName = "MP_MP_Stunt_tat_026_F", Name = "Winged Wheel" });
                TatThis.Add(new Tattoo { BaseName = "mpstunt_overlays", TatName = "MP_MP_Stunt_tat_024_F", Name = "Road Kill" });

                TatThis.Add(new Tattoo { BaseName = "mplowrider2_overlays", TatName = "MP_LR_Tat_032_F", Name = "Reign Over" });
                TatThis.Add(new Tattoo { BaseName = "mplowrider2_overlays", TatName = "MP_LR_Tat_031_F", Name = "Dead Pretty" });
                TatThis.Add(new Tattoo { BaseName = "mplowrider2_overlays", TatName = "MP_LR_Tat_008_F", Name = "Love the Game" });
                TatThis.Add(new Tattoo { BaseName = "mplowrider2_overlays", TatName = "MP_LR_Tat_000_F", Name = "SA Assault" });

                TatThis.Add(new Tattoo { BaseName = "mplowrider_overlays", TatName = "MP_LR_Tat_021_F", Name = "Sad Angel" });//
                TatThis.Add(new Tattoo { BaseName = "mplowrider_overlays", TatName = "MP_LR_Tat_014_F", Name = "Love is Blind" });//
                TatThis.Add(new Tattoo { BaseName = "mplowrider_overlays", TatName = "MP_LR_Tat_010_F", Name = "Bad Angel" });//
                TatThis.Add(new Tattoo { BaseName = "mplowrider_overlays", TatName = "MP_LR_Tat_009_F", Name = "Amazon" });//

                TatThis.Add(new Tattoo { BaseName = "mpluxe2_overlays", TatName = "MP_Luxe_Tat_029_F", Name = "Geometric Design" });
                TatThis.Add(new Tattoo { BaseName = "mpluxe2_overlays", TatName = "MP_Luxe_Tat_022_F", Name = "Cloaked Angel" });
                TatThis.Add(new Tattoo { BaseName = "mpluxe_overlays", TatName = "MP_LUXE_TAT_024_F", Name = "Feather Mural" });
                TatThis.Add(new Tattoo { BaseName = "mpluxe_overlays", TatName = "MP_LUXE_TAT_006_F", Name = "Adorned Wolf" });

                TatThis.Add(new Tattoo { BaseName = "mpchristmas2_overlays", TatName = "MP_Xmas2_F_Tat_015", Name = "Japanese Warrior" });
                TatThis.Add(new Tattoo { BaseName = "mpchristmas2_overlays", TatName = "MP_Xmas2_F_Tat_011", Name = "Roaring Tiger" });
                TatThis.Add(new Tattoo { BaseName = "mpchristmas2_overlays", TatName = "MP_Xmas2_F_Tat_006", Name = "Carp Shaded" });
                TatThis.Add(new Tattoo { BaseName = "mpchristmas2_overlays", TatName = "MP_Xmas2_F_Tat_005", Name = "Carp Outline" });

                TatThis.Add(new Tattoo { BaseName = "mphipster_overlays", TatName = "FM_Hip_F_Tat_046", Name = "Triangles" });
                TatThis.Add(new Tattoo { BaseName = "mphipster_overlays", TatName = "FM_Hip_F_Tat_041", Name = "Tooth" });
                TatThis.Add(new Tattoo { BaseName = "mphipster_overlays", TatName = "FM_Hip_F_Tat_032", Name = "Paper Plane" });
                TatThis.Add(new Tattoo { BaseName = "mphipster_overlays", TatName = "FM_Hip_F_Tat_031", Name = "Skateboard" });
                TatThis.Add(new Tattoo { BaseName = "mphipster_overlays", TatName = "FM_Hip_F_Tat_030", Name = "Shark Fin" });
                TatThis.Add(new Tattoo { BaseName = "mphipster_overlays", TatName = "FM_Hip_F_Tat_025", Name = "Watch Your Step" });
                TatThis.Add(new Tattoo { BaseName = "mphipster_overlays", TatName = "FM_Hip_F_Tat_024", Name = "Pyamid" });
                TatThis.Add(new Tattoo { BaseName = "mphipster_overlays", TatName = "FM_Hip_F_Tat_012", Name = "Antlers" });
                TatThis.Add(new Tattoo { BaseName = "mphipster_overlays", TatName = "FM_Hip_F_Tat_011", Name = "Infinity" });
                TatThis.Add(new Tattoo { BaseName = "mphipster_overlays", TatName = "FM_Hip_F_Tat_000", Name = "Crossed Arrows" });

                TatThis.Add(new Tattoo { BaseName = "mpbusiness_overlays", TatName = "MP_Buis_F_Back_001", Name = "Gold Digger" });
                TatThis.Add(new Tattoo { BaseName = "mpbusiness_overlays", TatName = "MP_Buis_F_Back_000", Name = "Respect" });

                TatThis.Add(new Tattoo { BaseName = "mpbeach_overlays", TatName = "MP_Bea_F_Should_000", Name = "Sea Horses" });
                TatThis.Add(new Tattoo { BaseName = "mpbeach_overlays", TatName = "MP_Bea_F_Back_002", Name = "Shrimp" });
                TatThis.Add(new Tattoo { BaseName = "mpbeach_overlays", TatName = "MP_Bea_F_Should_001", Name = "Catfish" });
                TatThis.Add(new Tattoo { BaseName = "mpbeach_overlays", TatName = "MP_Bea_F_Back_000", Name = "Rock Solid" });
                TatThis.Add(new Tattoo { BaseName = "mpbeach_overlays", TatName = "MP_Bea_F_Back_001", Name = "Hibiscus Flower Duo" });

                TatThis.Add(new Tattoo { BaseName = "multiplayer_overlays", TatName = "FM_Tat_F_045", Name = "Skulls and Rose" });
                TatThis.Add(new Tattoo { BaseName = "multiplayer_overlays", TatName = "FM_Tat_F_030", Name = "Lucky Celtic Dogs" });
                TatThis.Add(new Tattoo { BaseName = "multiplayer_overlays", TatName = "FM_Tat_F_020", Name = "Dragon" });
                TatThis.Add(new Tattoo { BaseName = "multiplayer_overlays", TatName = "FM_Tat_F_019", Name = "The Wages of Sin" });
                TatThis.Add(new Tattoo { BaseName = "multiplayer_overlays", TatName = "FM_Tat_F_016", Name = "Evil Clown" });
                TatThis.Add(new Tattoo { BaseName = "multiplayer_overlays", TatName = "FM_Tat_F_013", Name = "Eagle and Serpent" });
                TatThis.Add(new Tattoo { BaseName = "multiplayer_overlays", TatName = "FM_Tat_F_011", Name = "LS Script" });
                TatThis.Add(new Tattoo { BaseName = "multiplayer_overlays", TatName = "FM_Tat_F_009", Name = "Skull on the Cross" });

                TatThis.Add(new Tattoo { BaseName = "multiplayer_overlays", TatName = "FM_Tat_Award_F_019", Name = "Clown Dual Wield Dollars" });
                TatThis.Add(new Tattoo { BaseName = "multiplayer_overlays", TatName = "FM_Tat_Award_F_018", Name = "Clown Dual Wield" });
                TatThis.Add(new Tattoo { BaseName = "multiplayer_overlays", TatName = "FM_Tat_Award_F_017", Name = "Clown and Gun" });
                TatThis.Add(new Tattoo { BaseName = "multiplayer_overlays", TatName = "FM_Tat_Award_F_016", Name = "Clown" });
                TatThis.Add(new Tattoo { BaseName = "multiplayer_overlays", TatName = "FM_Tat_Award_F_014", Name = "Trust No One" });//Car Bomb Award
                TatThis.Add(new Tattoo { BaseName = "multiplayer_overlays", TatName = "FM_Tat_Award_F_008", Name = "Los Santos Customs" });//Mod a Car Award
                TatThis.Add(new Tattoo { BaseName = "multiplayer_overlays", TatName = "FM_Tat_Award_F_005", Name = "Angel" });//Win Every Game Mode Award
                                                                                                                              //Not In List
                TatThis.Add(new Tattoo { BaseName = "multiplayer_overlays", TatName = "FM_Tat_F_046", Name = "Zip?" });
                TatThis.Add(new Tattoo { BaseName = "mphipster_overlays", TatName = "FM_Hip_F_Tat_006", Name = "Feather Birds" });
                TatThis.Add(new Tattoo { BaseName = "mpchristmas2018_overlays", TatName = "MP_Christmas2018_Tat_000_F", Name = "???" });

                Tatlist.Add(TatThis[RandInt(0, TatThis.Count - 1)]);
                TatThis.Clear();

                TatThis.Add(new Tattoo { BaseName = "mpsum2_overlays", TatName = "MP_Sum2_Tat_003_F", Name = "Bullet Mouth" });
                TatThis.Add(new Tattoo { BaseName = "mpsum2_overlays", TatName = "MP_Sum2_Tat_004_F", Name = "Smoking Barrel" });
                TatThis.Add(new Tattoo { BaseName = "mpsum2_overlays", TatName = "MP_Sum2_Tat_040_F", Name = "Carved Pumpkin" });
                TatThis.Add(new Tattoo { BaseName = "mpsum2_overlays", TatName = "MP_Sum2_Tat_041_F", Name = "Branched Werewolf" });
                TatThis.Add(new Tattoo { BaseName = "mpsum2_overlays", TatName = "MP_Sum2_Tat_042_F", Name = "Winged Skull" });
                TatThis.Add(new Tattoo { BaseName = "mpsum2_overlays", TatName = "MP_Sum2_Tat_058_F", Name = "Shrieking Dragon" });
                TatThis.Add(new Tattoo { BaseName = "mpsum2_overlays", TatName = "MP_Sum2_Tat_059_F", Name = "Swords & City" });

                TatThis.Add(new Tattoo { BaseName = "mpsecurity_overlays", TatName = "MP_Security_Tat_016_F", Name = "All From The Same Tree" });
                TatThis.Add(new Tattoo { BaseName = "mpsecurity_overlays", TatName = "MP_Security_Tat_017_F", Name = "King of the Jungle" });
                TatThis.Add(new Tattoo { BaseName = "mpsecurity_overlays", TatName = "MP_Security_Tat_018_F", Name = "Night Owl" });

                TatThis.Add(new Tattoo { BaseName = "mpheist4_overlays", TatName = "MP_Heist4_Tat_023_F", Name = "Techno Glitch" });//
                TatThis.Add(new Tattoo { BaseName = "mpheist4_overlays", TatName = "MP_Heist4_Tat_022_F", Name = "Paradise Sirens" });//

                TatThis.Add(new Tattoo { BaseName = "mpheist3_overlays", TatName = "mpHeist3_Tat_035_F", Name = "LS Panic" });
                TatThis.Add(new Tattoo { BaseName = "mpheist3_overlays", TatName = "mpHeist3_Tat_033_F", Name = "LS City" });
                TatThis.Add(new Tattoo { BaseName = "mpheist3_overlays", TatName = "mpHeist3_Tat_026_F", Name = "Dignity" });
                TatThis.Add(new Tattoo { BaseName = "mpheist3_overlays", TatName = "mpHeist3_Tat_025_F", Name = "Davis" });

                TatThis.Add(new Tattoo { BaseName = "mpvinewood_overlays", TatName = "mp_vinewood_tat_022_F", Name = "Blood Money" });//
                TatThis.Add(new Tattoo { BaseName = "mpvinewood_overlays", TatName = "mp_vinewood_tat_003_F", Name = "Royal Flush" });//
                TatThis.Add(new Tattoo { BaseName = "mpvinewood_overlays", TatName = "mp_vinewood_tat_000_F", Name = "In the Pocket" });//

                TatThis.Add(new Tattoo { BaseName = "mpchristmas2017_overlays", TatName = "MP_Christmas2017_Tattoo_026_F", Name = "Spartan Skull" });
                TatThis.Add(new Tattoo { BaseName = "mpchristmas2017_overlays", TatName = "MP_Christmas2017_Tattoo_020_F", Name = "Medusa's Gaze" });
                TatThis.Add(new Tattoo { BaseName = "mpchristmas2017_overlays", TatName = "MP_Christmas2017_Tattoo_019_F", Name = "Strike Force" });
                TatThis.Add(new Tattoo { BaseName = "mpchristmas2017_overlays", TatName = "MP_Christmas2017_Tattoo_003_F", Name = "Native Warrior" });
                TatThis.Add(new Tattoo { BaseName = "mpchristmas2017_overlays", TatName = "MP_Christmas2017_Tattoo_000_F", Name = "Thor - Goblin" });

                TatThis.Add(new Tattoo { BaseName = "mpsmuggler_overlays", TatName = "MP_Smuggler_Tattoo_021_F", Name = "Dead Tales" });
                TatThis.Add(new Tattoo { BaseName = "mpsmuggler_overlays", TatName = "MP_Smuggler_Tattoo_019_F", Name = "Lost At Sea" });
                TatThis.Add(new Tattoo { BaseName = "mpsmuggler_overlays", TatName = "MP_Smuggler_Tattoo_007_F", Name = "No Honor" });
                TatThis.Add(new Tattoo { BaseName = "mpsmuggler_overlays", TatName = "MP_Smuggler_Tattoo_000_F", Name = "Bless The Dead" });

                TatThis.Add(new Tattoo { BaseName = "mpairraces_overlays", TatName = "MP_Airraces_Tattoo_000_F", Name = "Turbulence" });

                TatThis.Add(new Tattoo { BaseName = "mpgunrunning_overlays", TatName = "MP_Gunrunning_Tattoo_028_F", Name = "Micro SMG Chain" });
                TatThis.Add(new Tattoo { BaseName = "mpgunrunning_overlays", TatName = "MP_Gunrunning_Tattoo_020_F", Name = "Crowned Weapons" });
                TatThis.Add(new Tattoo { BaseName = "mpgunrunning_overlays", TatName = "MP_Gunrunning_Tattoo_017_F", Name = "Dog Tags" });
                TatThis.Add(new Tattoo { BaseName = "mpgunrunning_overlays", TatName = "MP_Gunrunning_Tattoo_012_F", Name = "Dollar Daggers" });

                TatThis.Add(new Tattoo { BaseName = "mpbiker_overlays", TatName = "MP_MP_Biker_Tat_060_F", Name = "We Are The Mods!" });
                TatThis.Add(new Tattoo { BaseName = "mpbiker_overlays", TatName = "MP_MP_Biker_Tat_059_F", Name = "Faggio" });
                TatThis.Add(new Tattoo { BaseName = "mpbiker_overlays", TatName = "MP_MP_Biker_Tat_058_F", Name = "Reaper Vulture" });
                TatThis.Add(new Tattoo { BaseName = "mpbiker_overlays", TatName = "MP_MP_Biker_Tat_050_F", Name = "Unforgiven" });
                TatThis.Add(new Tattoo { BaseName = "mpbiker_overlays", TatName = "MP_MP_Biker_Tat_041_F", Name = "No Regrets" });
                TatThis.Add(new Tattoo { BaseName = "mpbiker_overlays", TatName = "MP_MP_Biker_Tat_034_F", Name = "Brotherhood of Bikes" });
                TatThis.Add(new Tattoo { BaseName = "mpbiker_overlays", TatName = "MP_MP_Biker_Tat_032_F", Name = "Western Eagle" });
                TatThis.Add(new Tattoo { BaseName = "mpbiker_overlays", TatName = "MP_MP_Biker_Tat_029_F", Name = "Bone Wrench" });
                TatThis.Add(new Tattoo { BaseName = "mpbiker_overlays", TatName = "MP_MP_Biker_Tat_026_F", Name = "American Dream" });
                TatThis.Add(new Tattoo { BaseName = "mpbiker_overlays", TatName = "MP_MP_Biker_Tat_023_F", Name = "Western MC" });
                TatThis.Add(new Tattoo { BaseName = "mpbiker_overlays", TatName = "MP_MP_Biker_Tat_019_F", Name = "Gruesome Talons" });
                TatThis.Add(new Tattoo { BaseName = "mpbiker_overlays", TatName = "MP_MP_Biker_Tat_018_F", Name = "Skeletal Chopper" });
                TatThis.Add(new Tattoo { BaseName = "mpbiker_overlays", TatName = "MP_MP_Biker_Tat_013_F", Name = "Demon Crossbones" });
                TatThis.Add(new Tattoo { BaseName = "mpbiker_overlays", TatName = "MP_MP_Biker_Tat_005_F", Name = "Made In America" });
                TatThis.Add(new Tattoo { BaseName = "mpbiker_overlays", TatName = "MP_MP_Biker_Tat_001_F", Name = "Both Barrels" });
                TatThis.Add(new Tattoo { BaseName = "mpbiker_overlays", TatName = "MP_MP_Biker_Tat_000_F", Name = "Demon Rider" });

                TatThis.Add(new Tattoo { BaseName = "mpstunt_overlays", TatName = "MP_MP_Stunt_tat_044_F", Name = "Ram Skull" });
                TatThis.Add(new Tattoo { BaseName = "mpstunt_overlays", TatName = "MP_MP_Stunt_tat_033_F", Name = "Sugar Skull Trucker" });
                TatThis.Add(new Tattoo { BaseName = "mpstunt_overlays", TatName = "MP_MP_Stunt_tat_027_F", Name = "Punk Road Hog" });
                TatThis.Add(new Tattoo { BaseName = "mpstunt_overlays", TatName = "MP_MP_Stunt_tat_019_F", Name = "Engine Heart" });
                TatThis.Add(new Tattoo { BaseName = "mpstunt_overlays", TatName = "MP_MP_Stunt_tat_018_F", Name = "Vintage Bully" });
                TatThis.Add(new Tattoo { BaseName = "mpstunt_overlays", TatName = "MP_MP_Stunt_tat_011_F", Name = "Wheels of Death" });

                TatThis.Add(new Tattoo { BaseName = "mplowrider2_overlays", TatName = "MP_LR_Tat_019_F", Name = "Death Behind" });
                TatThis.Add(new Tattoo { BaseName = "mplowrider2_overlays", TatName = "MP_LR_Tat_012_F", Name = "Royal Kiss" });

                TatThis.Add(new Tattoo { BaseName = "mplowrider_overlays", TatName = "MP_LR_Tat_026_F", Name = "Royal Takeover" });//
                TatThis.Add(new Tattoo { BaseName = "mplowrider_overlays", TatName = "MP_LR_Tat_013_F", Name = "Love Gamble" });//
                TatThis.Add(new Tattoo { BaseName = "mplowrider_overlays", TatName = "MP_LR_Tat_002_F", Name = "Holy Mary" });//
                TatThis.Add(new Tattoo { BaseName = "mplowrider_overlays", TatName = "MP_LR_Tat_001_F", Name = "King Fight" });//

                TatThis.Add(new Tattoo { BaseName = "mpluxe2_overlays", TatName = "MP_Luxe_Tat_027_F", Name = "Cobra Dawn" });
                TatThis.Add(new Tattoo { BaseName = "mpluxe2_overlays", TatName = "MP_Luxe_Tat_025_F", Name = "Reaper Sway" });
                TatThis.Add(new Tattoo { BaseName = "mpluxe2_overlays", TatName = "MP_Luxe_Tat_012_F", Name = "Geometric Galaxy" });
                TatThis.Add(new Tattoo { BaseName = "mpluxe2_overlays", TatName = "MP_Luxe_Tat_002_F", Name = "The Howler" });

                TatThis.Add(new Tattoo { BaseName = "mpluxe_overlays", TatName = "MP_Luxe_Tat_015_F", Name = "Smoking Sisters" });
                TatThis.Add(new Tattoo { BaseName = "mpluxe_overlays", TatName = "MP_Luxe_Tat_014_F", Name = "Ancient Queen" });
                TatThis.Add(new Tattoo { BaseName = "mpluxe_overlays", TatName = "MP_Luxe_Tat_008_F", Name = "Flying Eye" });
                TatThis.Add(new Tattoo { BaseName = "mpluxe_overlays", TatName = "MP_Luxe_Tat_007_F", Name = "Eye of the Griffin" });

                TatThis.Add(new Tattoo { BaseName = "mpchristmas2_overlays", TatName = "MP_Xmas2_F_Tat_019", Name = "Royal Dagger Color" });
                TatThis.Add(new Tattoo { BaseName = "mpchristmas2_overlays", TatName = "MP_Xmas2_F_Tat_018", Name = "Royal Dagger Outline" });
                TatThis.Add(new Tattoo { BaseName = "mpchristmas2_overlays", TatName = "MP_Xmas2_F_Tat_017", Name = "Loose Lips Color" });
                TatThis.Add(new Tattoo { BaseName = "mpchristmas2_overlays", TatName = "MP_Xmas2_F_Tat_016", Name = "Loose Lips Outline" });
                TatThis.Add(new Tattoo { BaseName = "mpchristmas2_overlays", TatName = "MP_Xmas2_F_Tat_009", Name = "Time To Die" });

                TatThis.Add(new Tattoo { BaseName = "mphipster_overlays", TatName = "FM_Hip_F_Tat_047", Name = "Cassette" });
                TatThis.Add(new Tattoo { BaseName = "mphipster_overlays", TatName = "FM_Hip_F_Tat_033", Name = "Stag" });
                TatThis.Add(new Tattoo { BaseName = "mphipster_overlays", TatName = "FM_Hip_F_Tat_013", Name = "Boombox" });
                TatThis.Add(new Tattoo { BaseName = "mphipster_overlays", TatName = "FM_Hip_F_Tat_002", Name = "Chemistry" });

                TatThis.Add(new Tattoo { BaseName = "mpbusiness_overlays", TatName = "MP_Buis_F_Chest_002", Name = "Love Money" });
                TatThis.Add(new Tattoo { BaseName = "mpbusiness_overlays", TatName = "MP_Buis_F_Chest_001", Name = "Makin' Money" });
                TatThis.Add(new Tattoo { BaseName = "mpbusiness_overlays", TatName = "MP_Buis_F_Chest_000", Name = "High Roller" });

                TatThis.Add(new Tattoo { BaseName = "mpbeach_overlays", TatName = "MP_Bea_F_Chest_001", Name = "Anchor" });
                TatThis.Add(new Tattoo { BaseName = "mpbeach_overlays", TatName = "MP_Bea_F_Chest_000", Name = "Anchor" });
                TatThis.Add(new Tattoo { BaseName = "mpbeach_overlays", TatName = "MP_Bea_F_Chest_002", Name = "Los Santos Wreath" });

                TatThis.Add(new Tattoo { BaseName = "multiplayer_overlays", TatName = "FM_Tat_F_044", Name = "Stone Cross" });
                TatThis.Add(new Tattoo { BaseName = "multiplayer_overlays", TatName = "FM_Tat_F_034", Name = "Flaming Shamrock" });
                TatThis.Add(new Tattoo { BaseName = "multiplayer_overlays", TatName = "FM_Tat_F_025", Name = "LS Bold" });
                TatThis.Add(new Tattoo { BaseName = "multiplayer_overlays", TatName = "FM_Tat_F_024", Name = "Flaming Cross" });
                TatThis.Add(new Tattoo { BaseName = "multiplayer_overlays", TatName = "FM_Tat_F_010", Name = "LS Flames" });

                TatThis.Add(new Tattoo { BaseName = "multiplayer_overlays", TatName = "FM_Tat_Award_F_013", Name = "Seven Deadly Sins" });//Kill 1000 Players Award
                TatThis.Add(new Tattoo { BaseName = "multiplayer_overlays", TatName = "FM_Tat_Award_F_012", Name = "Embellished Scroll" });//Kill 500 Players Award
                TatThis.Add(new Tattoo { BaseName = "multiplayer_overlays", TatName = "FM_Tat_Award_F_011", Name = "Blank Scroll" });////Kill 100 Players Award?
                TatThis.Add(new Tattoo { BaseName = "multiplayer_overlays", TatName = "FM_Tat_Award_F_003", Name = "Blackjack" });
                //

                Tatlist.Add(TatThis[RandInt(0, TatThis.Count - 1)]);
                TatThis.Clear();

                TatThis.Add(new Tattoo { BaseName = "mpsum2_overlays", TatName = "MP_Sum2_Tat_005_F", Name = "Concealed" });
                TatThis.Add(new Tattoo { BaseName = "mpsum2_overlays", TatName = "MP_Sum2_Tat_043_F", Name = "Cursed Saki" });
                TatThis.Add(new Tattoo { BaseName = "mpsum2_overlays", TatName = "MP_Sum2_Tat_044_F", Name = "Smouldering Bat & Skull" });
                TatThis.Add(new Tattoo { BaseName = "mpsum2_overlays", TatName = "MP_Sum2_Tat_060_F", Name = "Blaine County" });
                TatThis.Add(new Tattoo { BaseName = "mpsum2_overlays", TatName = "MP_Sum2_Tat_061_F", Name = "Angry Possum" });
                TatThis.Add(new Tattoo { BaseName = "mpsum2_overlays", TatName = "MP_Sum2_Tat_062_F", Name = "Floral Demon" });

                TatThis.Add(new Tattoo { BaseName = "mpsecurity_overlays", TatName = "MP_Security_Tat_024_F", Name = "Beatbox Silhouette" });
                TatThis.Add(new Tattoo { BaseName = "mpsecurity_overlays", TatName = "MP_Security_Tat_025_F", Name = "Davis Flames" });

                TatThis.Add(new Tattoo { BaseName = "mpheist4_overlays", TatName = "MP_Heist4_Tat_030_F", Name = "Radio Tape" });//
                TatThis.Add(new Tattoo { BaseName = "mpheist4_overlays", TatName = "MP_Heist4_Tat_004_F", Name = "Skeleton Breeze" });//

                TatThis.Add(new Tattoo { BaseName = "mpheist3_overlays", TatName = "mpHeist3_Tat_037_F", Name = "LadyBug" });
                TatThis.Add(new Tattoo { BaseName = "mpheist3_overlays", TatName = "mpHeist3_Tat_029_F", Name = "Fatal Incursion" });

                TatThis.Add(new Tattoo { BaseName = "mpvinewood_overlays", TatName = "mp_vinewood_tat_031_F", Name = "Gambling Royalty" });//
                TatThis.Add(new Tattoo { BaseName = "mpvinewood_overlays", TatName = "mp_vinewood_tat_024_F", Name = "Cash Mouth" });//
                TatThis.Add(new Tattoo { BaseName = "mpvinewood_overlays", TatName = "mp_vinewood_tat_016_F", Name = "Rose and Aces" });//
                TatThis.Add(new Tattoo { BaseName = "mpvinewood_overlays", TatName = "mp_vinewood_tat_012_F", Name = "Skull of Suits" });//

                TatThis.Add(new Tattoo { BaseName = "mpchristmas2017_overlays", TatName = "MP_Christmas2017_Tattoo_008_F", Name = "Spartan Warrior" });

                TatThis.Add(new Tattoo { BaseName = "mpsmuggler_overlays", TatName = "MP_Smuggler_Tattoo_015_F", Name = "Jolly Roger" });
                TatThis.Add(new Tattoo { BaseName = "mpsmuggler_overlays", TatName = "MP_Smuggler_Tattoo_010_F", Name = "See You In Hell" });
                TatThis.Add(new Tattoo { BaseName = "mpsmuggler_overlays", TatName = "MP_Smuggler_Tattoo_002_F", Name = "Dead Lies" });

                TatThis.Add(new Tattoo { BaseName = "mpairraces_overlays", TatName = "MP_Airraces_Tattoo_006_F", Name = "Bombs Away" });

                TatThis.Add(new Tattoo { BaseName = "mpgunrunning_overlays", TatName = "MP_Gunrunning_Tattoo_029_F", Name = "Win Some Lose Some" });
                TatThis.Add(new Tattoo { BaseName = "mpgunrunning_overlays", TatName = "MP_Gunrunning_Tattoo_010_F", Name = "Cash Money" });

                TatThis.Add(new Tattoo { BaseName = "mpbiker_overlays", TatName = "MP_MP_Biker_Tat_052_F", Name = "Biker Mount" });
                TatThis.Add(new Tattoo { BaseName = "mpbiker_overlays", TatName = "MP_MP_Biker_Tat_039_F", Name = "Gas Guzzler" });
                TatThis.Add(new Tattoo { BaseName = "mpbiker_overlays", TatName = "MP_MP_Biker_Tat_031_F", Name = "Gear Head" });
                TatThis.Add(new Tattoo { BaseName = "mpbiker_overlays", TatName = "MP_MP_Biker_Tat_010_F", Name = "Skull Of Taurus" });
                TatThis.Add(new Tattoo { BaseName = "mpbiker_overlays", TatName = "MP_MP_Biker_Tat_003_F", Name = "Web Rider" });

                TatThis.Add(new Tattoo { BaseName = "mpstunt_overlays", TatName = "MP_MP_Stunt_tat_014_F", Name = "Bat Cat of Spades" });
                TatThis.Add(new Tattoo { BaseName = "mpstunt_overlays", TatName = "MP_MP_Stunt_tat_012_F", Name = "Punk Biker" });

                TatThis.Add(new Tattoo { BaseName = "mplowrider2_overlays", TatName = "MP_LR_Tat_016_F", Name = "Two Face" });
                TatThis.Add(new Tattoo { BaseName = "mplowrider2_overlays", TatName = "MP_LR_Tat_011_F", Name = "Lady Liberty" });

                TatThis.Add(new Tattoo { BaseName = "mplowrider_overlays", TatName = "MP_LR_Tat_004_F", Name = "Gun Mic" });//

                TatThis.Add(new Tattoo { BaseName = "mpluxe_overlays", TatName = "MP_Luxe_Tat_003_F", Name = "Abstract Skull" });

                TatThis.Add(new Tattoo { BaseName = "mpchristmas2_overlays", TatName = "MP_Xmas2_F_Tat_028", Name = "Executioner" });
                TatThis.Add(new Tattoo { BaseName = "mpchristmas2_overlays", TatName = "MP_Xmas2_F_Tat_013", Name = "Lizard" });

                TatThis.Add(new Tattoo { BaseName = "mphipster_overlays", TatName = "FM_Hip_F_Tat_035", Name = "Sewn Heart" });
                TatThis.Add(new Tattoo { BaseName = "mphipster_overlays", TatName = "FM_Hip_F_Tat_029", Name = "Sad" });
                TatThis.Add(new Tattoo { BaseName = "mphipster_overlays", TatName = "FM_Hip_F_Tat_006", Name = "Feather Birds" });

                TatThis.Add(new Tattoo { BaseName = "mpbusiness_overlays", TatName = "MP_Buis_F_Stom_002", Name = "Money Bag" });
                TatThis.Add(new Tattoo { BaseName = "mpbusiness_overlays", TatName = "MP_Buis_F_Stom_001", Name = "Santo Capra Logo" });
                TatThis.Add(new Tattoo { BaseName = "mpbusiness_overlays", TatName = "MP_Buis_F_Stom_000", Name = "Diamond Back" });

                TatThis.Add(new Tattoo { BaseName = "mpbeach_overlays", TatName = "MP_Bea_F_Stom_000", Name = "Swallow" });
                TatThis.Add(new Tattoo { BaseName = "mpbeach_overlays", TatName = "MP_Bea_F_Stom_002", Name = "Dolphin" });
                TatThis.Add(new Tattoo { BaseName = "mpbeach_overlays", TatName = "MP_Bea_F_Stom_001", Name = "Hibiscus Flower" });
                TatThis.Add(new Tattoo { BaseName = "mpbeach_overlays", TatName = "MP_Bea_F_RSide_000", Name = "Love Dagger" });

                TatThis.Add(new Tattoo { BaseName = "multiplayer_overlays", TatName = "FM_Tat_F_036", Name = "Way of the Gun" });//500 Pistol kills Award
                TatThis.Add(new Tattoo { BaseName = "multiplayer_overlays", TatName = "FM_Tat_F_029", Name = "Trinity Knot" });
                TatThis.Add(new Tattoo { BaseName = "multiplayer_overlays", TatName = "FM_Tat_F_012", Name = "Los Santos Bills" });
                TatThis.Add(new Tattoo { BaseName = "multiplayer_overlays", TatName = "FM_Tat_F_004", Name = "Faith" });

                TatThis.Add(new Tattoo { BaseName = "multiplayer_overlays", TatName = "FM_Tat_Award_F_004", Name = "Hustler" });//Make 50000 from betting Award

                Tatlist.Add(TatThis[RandInt(0, TatThis.Count - 1)]);
                TatThis.Clear();

                TatThis.Add(new Tattoo { BaseName = "mpsum2_overlays", TatName = "MP_Sum2_Tat_000_F", Name = "Live Fast Mono" });
                TatThis.Add(new Tattoo { BaseName = "mpsum2_overlays", TatName = "MP_Sum2_Tat_001_F", Name = "Live Fast Color" });
                TatThis.Add(new Tattoo { BaseName = "mpsum2_overlays", TatName = "MP_Sum2_Tat_018_F", Name = "Branched Skull" });
                TatThis.Add(new Tattoo { BaseName = "mpsum2_overlays", TatName = "MP_Sum2_Tat_019_F", Name = "Scythed Corpse" });
                TatThis.Add(new Tattoo { BaseName = "mpsum2_overlays", TatName = "MP_Sum2_Tat_020_F", Name = "Scythed Corpse & Reaper" });
                TatThis.Add(new Tattoo { BaseName = "mpsum2_overlays", TatName = "MP_Sum2_Tat_021_F", Name = "Third Eye" });
                TatThis.Add(new Tattoo { BaseName = "mpsum2_overlays", TatName = "MP_Sum2_Tat_022_F", Name = "Pierced Third Eye" });
                TatThis.Add(new Tattoo { BaseName = "mpsum2_overlays", TatName = "MP_Sum2_Tat_023_F", Name = "Lip Drip" });
                TatThis.Add(new Tattoo { BaseName = "mpsum2_overlays", TatName = "MP_Sum2_Tat_024_F", Name = "Skin Mask" });
                TatThis.Add(new Tattoo { BaseName = "mpsum2_overlays", TatName = "MP_Sum2_Tat_025_F", Name = "Webbed Scythe" });
                TatThis.Add(new Tattoo { BaseName = "mpsum2_overlays", TatName = "MP_Sum2_Tat_026_F", Name = "Oni Demon" });
                TatThis.Add(new Tattoo { BaseName = "mpsum2_overlays", TatName = "MP_Sum2_Tat_027_F", Name = "Bat Wings" });

                TatThis.Add(new Tattoo { BaseName = "mpsecurity_overlays", TatName = "MP_Security_Tat_001_F", Name = "Bright Diamond" });
                TatThis.Add(new Tattoo { BaseName = "mpsecurity_overlays", TatName = "MP_Security_Tat_002_F", Name = "Hustle" });
                TatThis.Add(new Tattoo { BaseName = "mpsecurity_overlays", TatName = "MP_Security_Tat_027_F", Name = "Black Widow" });

                TatThis.Add(new Tattoo { BaseName = "mpheist3_overlays", TatName = "mpHeist3_Tat_044_F", Name = "Clubs" });
                TatThis.Add(new Tattoo { BaseName = "mpheist3_overlays", TatName = "mpHeist3_Tat_043_F", Name = "Diamonds" });
                TatThis.Add(new Tattoo { BaseName = "mpheist3_overlays", TatName = "mpHeist3_Tat_042_F", Name = "Hearts" });

                TatThis.Add(new Tattoo { BaseName = "mpheist3_overlays", TatName = "mpHeist3_Tat_022_F", Name = "Thong's Sword" });
                TatThis.Add(new Tattoo { BaseName = "mpheist3_overlays", TatName = "mpHeist3_Tat_021_F", Name = "Wanted" });
                TatThis.Add(new Tattoo { BaseName = "mpheist3_overlays", TatName = "mpHeist3_Tat_020_F", Name = "UFO" });
                TatThis.Add(new Tattoo { BaseName = "mpheist3_overlays", TatName = "mpHeist3_Tat_019_F", Name = "Teddy Bear" });
                TatThis.Add(new Tattoo { BaseName = "mpheist3_overlays", TatName = "mpHeist3_Tat_018_F", Name = "Stitches" });
                TatThis.Add(new Tattoo { BaseName = "mpheist3_overlays", TatName = "mpHeist3_Tat_017_F", Name = "Space Monkey" });
                TatThis.Add(new Tattoo { BaseName = "mpheist3_overlays", TatName = "mpHeist3_Tat_016_F", Name = "Sleepy" });
                TatThis.Add(new Tattoo { BaseName = "mpheist3_overlays", TatName = "mpHeist3_Tat_015_F", Name = "On/Off" });
                TatThis.Add(new Tattoo { BaseName = "mpheist3_overlays", TatName = "mpHeist3_Tat_014_F", Name = "LS Wings" });
                TatThis.Add(new Tattoo { BaseName = "mpheist3_overlays", TatName = "mpHeist3_Tat_013_F", Name = "LS Star" });
                TatThis.Add(new Tattoo { BaseName = "mpheist3_overlays", TatName = "mpHeist3_Tat_012_F", Name = "Razor Pop" });
                TatThis.Add(new Tattoo { BaseName = "mpheist3_overlays", TatName = "mpHeist3_Tat_011_F", Name = "Lipstick Kiss" });
                TatThis.Add(new Tattoo { BaseName = "mpheist3_overlays", TatName = "mpHeist3_Tat_010_F", Name = "Green Leaf" });
                TatThis.Add(new Tattoo { BaseName = "mpheist3_overlays", TatName = "mpHeist3_Tat_009_F", Name = "Knifed" });
                TatThis.Add(new Tattoo { BaseName = "mpheist3_overlays", TatName = "mpHeist3_Tat_008_F", Name = "Ice Cream" });
                TatThis.Add(new Tattoo { BaseName = "mpheist3_overlays", TatName = "mpHeist3_Tat_007_F", Name = "Two Horns" });
                TatThis.Add(new Tattoo { BaseName = "mpheist3_overlays", TatName = "mpHeist3_Tat_006_F", Name = "Crowned" });
                TatThis.Add(new Tattoo { BaseName = "mpheist3_overlays", TatName = "mpHeist3_Tat_005_F", Name = "Spades" });
                TatThis.Add(new Tattoo { BaseName = "mpheist3_overlays", TatName = "mpHeist3_Tat_004_F", Name = "Bandage" });
                TatThis.Add(new Tattoo { BaseName = "mpheist3_overlays", TatName = "mpHeist3_Tat_003_F", Name = "Assault Rifle" });
                TatThis.Add(new Tattoo { BaseName = "mpheist3_overlays", TatName = "mpHeist3_Tat_002_F", Name = "Animal" });
                TatThis.Add(new Tattoo { BaseName = "mpheist3_overlays", TatName = "mpHeist3_Tat_001_F", Name = "Ace of Spades" });
                TatThis.Add(new Tattoo { BaseName = "mpheist3_overlays", TatName = "mpHeist3_Tat_000_F", Name = "Five Stars" });

                TatThis.Add(new Tattoo { BaseName = "mpsmuggler_overlays", TatName = "MP_Smuggler_Tattoo_012_F", Name = "Thief" });
                TatThis.Add(new Tattoo { BaseName = "mpsmuggler_overlays", TatName = "MP_Smuggler_Tattoo_011_F", Name = "Sinner" });

                TatThis.Add(new Tattoo { BaseName = "mpgunrunning_overlays", TatName = "MP_Gunrunning_Tattoo_003_F", Name = "Lock and Load" });

                TatThis.Add(new Tattoo { BaseName = "mpbiker_overlays", TatName = "MP_MP_Biker_Tat_051_F", Name = "Western Stylized" });
                TatThis.Add(new Tattoo { BaseName = "mpbiker_overlays", TatName = "MP_MP_Biker_Tat_038_F", Name = "FTW" });
                TatThis.Add(new Tattoo { BaseName = "mpbiker_overlays", TatName = "MP_MP_Biker_Tat_009_F", Name = "Morbid Arachnid" });

                TatThis.Add(new Tattoo { BaseName = "mpstunt_overlays", TatName = "MP_MP_Stunt_tat_042_F", Name = "Flaming Quad" });
                TatThis.Add(new Tattoo { BaseName = "mpstunt_overlays", TatName = "MP_MP_Stunt_tat_017_F", Name = "Bat Wheel" });
                TatThis.Add(new Tattoo { BaseName = "mpstunt_overlays", TatName = "MP_MP_Stunt_Tat_006_F", Name = "Toxic Spider" });
                TatThis.Add(new Tattoo { BaseName = "mpstunt_overlays", TatName = "MP_MP_Stunt_Tat_004_F", Name = "Scorpion" });
                TatThis.Add(new Tattoo { BaseName = "mpstunt_overlays", TatName = "MP_MP_Stunt_Tat_000_F", Name = "Stunt Skull" });

                TatThis.Add(new Tattoo { BaseName = "mpchristmas2_overlays", TatName = "MP_Xmas2_F_Tat_029", Name = "Beautiful Death" });
                TatThis.Add(new Tattoo { BaseName = "mpchristmas2_overlays", TatName = "MP_Xmas2_F_Tat_025", Name = "Snake Head Color" });
                TatThis.Add(new Tattoo { BaseName = "mpchristmas2_overlays", TatName = "MP_Xmas2_F_Tat_024", Name = "Snake Head Outline" });
                TatThis.Add(new Tattoo { BaseName = "mpchristmas2_overlays", TatName = "MP_Xmas2_F_Tat_007", Name = "Los Muertos" });

                TatThis.Add(new Tattoo { BaseName = "mphipster_overlays", TatName = "FM_Hip_F_Tat_021", Name = "Geo Fox" });
                TatThis.Add(new Tattoo { BaseName = "mphipster_overlays", TatName = "FM_Hip_F_Tat_005", Name = "Beautiful Eye" });

                TatThis.Add(new Tattoo { BaseName = "mpbusiness_overlays", TatName = "MP_Buis_F_Neck_001", Name = "Money Rose" });
                TatThis.Add(new Tattoo { BaseName = "mpbusiness_overlays", TatName = "MP_Buis_F_Neck_000", Name = "Val-de-Grace Logo" });

                TatThis.Add(new Tattoo { BaseName = "mpbeach_overlays", TatName = "MP_Bea_F_Neck_000", Name = "Tribal Butterfly" });
                TatThis.Add(new Tattoo { BaseName = "mpbeach_overlays", TatName = "MP_Bea_F_Neck_000", Name = "Little Fish" });

                TatThis.Add(new Tattoo { BaseName = "multiplayer_overlays", TatName = "FM_Tat_Award_F_000", Name = "Skull" });//500 Headshots Award

                Tatlist.Add(TatThis[RandInt(0, TatThis.Count - 1)]);
                TatThis.Clear();

                TatThis.Add(new Tattoo { BaseName = "mpsum2_overlays", TatName = "MP_Sum2_Tat_008_F", Name = "Bigness Chimp" });
                TatThis.Add(new Tattoo { BaseName = "mpsum2_overlays", TatName = "MP_Sum2_Tat_009_F", Name = "Up-n-Atomizer Design" });
                TatThis.Add(new Tattoo { BaseName = "mpsum2_overlays", TatName = "MP_Sum2_Tat_010_F", Name = "Rocket Launcher Girl" });
                TatThis.Add(new Tattoo { BaseName = "mpsum2_overlays", TatName = "MP_Sum2_Tat_028_F", Name = "Laser Eyes Skull" });
                TatThis.Add(new Tattoo { BaseName = "mpsum2_overlays", TatName = "MP_Sum2_Tat_029_F", Name = "Classic Vampire" });
                TatThis.Add(new Tattoo { BaseName = "mpsum2_overlays", TatName = "MP_Sum2_Tat_049_F", Name = "Demon Drummer" });

                TatThis.Add(new Tattoo { BaseName = "mpsecurity_overlays", TatName = "MP_Security_Tat_006_F", Name = "Skeleton Shot" });
                TatThis.Add(new Tattoo { BaseName = "mpsecurity_overlays", TatName = "MP_Security_Tat_010_F", Name = "Music Is The Remedy" });
                TatThis.Add(new Tattoo { BaseName = "mpsecurity_overlays", TatName = "MP_Security_Tat_011_F", Name = "Serpent Mic" });
                TatThis.Add(new Tattoo { BaseName = "mpsecurity_overlays", TatName = "MP_Security_Tat_019_F", Name = "Weed Knuckles" });

                TatThis.Add(new Tattoo { BaseName = "mpheist4_overlays", TatName = "MP_Heist4_Tat_009_F", Name = "Scratch Panther" });

                TatThis.Add(new Tattoo { BaseName = "mpheist3_overlays", TatName = "mpHeist3_Tat_041_F", Name = "Mighty Thog" });
                TatThis.Add(new Tattoo { BaseName = "mpheist3_overlays", TatName = "mpHeist3_Tat_040_F", Name = "Tiger Heart" });

                TatThis.Add(new Tattoo { BaseName = "mpvinewood_overlays", TatName = "MP_Vinewood_Tat_026_F", Name = "Banknote Rose" });//
                TatThis.Add(new Tattoo { BaseName = "mpvinewood_overlays", TatName = "MP_Vinewood_Tat_019_F", Name = "Can't Win Them All" });//
                TatThis.Add(new Tattoo { BaseName = "mpvinewood_overlays", TatName = "MP_Vinewood_Tat_014_F", Name = "Vice" });//
                TatThis.Add(new Tattoo { BaseName = "mpvinewood_overlays", TatName = "MP_Vinewood_Tat_005_F", Name = "Get Lucky" });//
                TatThis.Add(new Tattoo { BaseName = "mpvinewood_overlays", TatName = "MP_Vinewood_Tat_002_F", Name = "Suits" });//

                TatThis.Add(new Tattoo { BaseName = "mpchristmas2017_overlays", TatName = "MP_Christmas2017_Tattoo_029_F", Name = "Cerberus" });
                TatThis.Add(new Tattoo { BaseName = "mpchristmas2017_overlays", TatName = "MP_Christmas2017_Tattoo_025_F", Name = "Winged Serpent" });
                TatThis.Add(new Tattoo { BaseName = "mpchristmas2017_overlays", TatName = "MP_Christmas2017_Tattoo_013_F", Name = "Katana" });
                TatThis.Add(new Tattoo { BaseName = "mpchristmas2017_overlays", TatName = "MP_Christmas2017_Tattoo_007_F", Name = "Spartan Combat" });
                TatThis.Add(new Tattoo { BaseName = "mpchristmas2017_overlays", TatName = "MP_Christmas2017_Tattoo_004_F", Name = "Tiger and Mask" });
                TatThis.Add(new Tattoo { BaseName = "mpchristmas2017_overlays", TatName = "MP_Christmas2017_Tattoo_001_F", Name = "Viking Warrior" });

                TatThis.Add(new Tattoo { BaseName = "mpsmuggler_overlays", TatName = "MP_Smuggler_Tattoo_014_F", Name = "Mermaid's Curse" });
                TatThis.Add(new Tattoo { BaseName = "mpsmuggler_overlays", TatName = "MP_Smuggler_Tattoo_008_F", Name = "Horrors Of The Deep" });
                TatThis.Add(new Tattoo { BaseName = "mpsmuggler_overlays", TatName = "MP_Smuggler_Tattoo_004_F", Name = "Honor" });

                TatThis.Add(new Tattoo { BaseName = "mpairraces_overlays", TatName = "MP_Airraces_Tattoo_003_F", Name = "Toxic Trails" });

                TatThis.Add(new Tattoo { BaseName = "mpgunrunning_overlays", TatName = "MP_Gunrunning_Tattoo_027_F", Name = "Serpent Revolver" });
                TatThis.Add(new Tattoo { BaseName = "mpgunrunning_overlays", TatName = "MP_Gunrunning_Tattoo_025_F", Name = "Praying Skull" });
                TatThis.Add(new Tattoo { BaseName = "mpgunrunning_overlays", TatName = "MP_Gunrunning_Tattoo_016_F", Name = "Blood Money" });
                TatThis.Add(new Tattoo { BaseName = "mpgunrunning_overlays", TatName = "MP_Gunrunning_Tattoo_015_F", Name = "Spiked Skull" });
                TatThis.Add(new Tattoo { BaseName = "mpgunrunning_overlays", TatName = "MP_Gunrunning_Tattoo_008_F", Name = "Bandolier" });
                TatThis.Add(new Tattoo { BaseName = "mpgunrunning_overlays", TatName = "MP_Gunrunning_Tattoo_004_F", Name = "Sidearm" });

                TatThis.Add(new Tattoo { BaseName = "mpimportexport_overlays", TatName = "MP_MP_ImportExport_Tat_008_F", Name = "Scarlett" });
                TatThis.Add(new Tattoo { BaseName = "mpimportexport_overlays", TatName = "MP_MP_ImportExport_Tat_004_F", Name = "Piston Sleeve" });

                TatThis.Add(new Tattoo { BaseName = "mpbiker_overlays", TatName = "MP_MP_Biker_Tat_055_F", Name = "Poison Scorpion" });
                TatThis.Add(new Tattoo { BaseName = "mpbiker_overlays", TatName = "MP_MP_Biker_Tat_053_F", Name = "Muffler Helmet" });
                TatThis.Add(new Tattoo { BaseName = "mpbiker_overlays", TatName = "MP_MP_Biker_Tat_045_F", Name = "Ride Hard Die Fast" });
                TatThis.Add(new Tattoo { BaseName = "mpbiker_overlays", TatName = "MP_MP_Biker_Tat_035_F", Name = "Chain Fist" });
                TatThis.Add(new Tattoo { BaseName = "mpbiker_overlays", TatName = "MP_MP_Biker_Tat_025_F", Name = "Good Luck" });
                TatThis.Add(new Tattoo { BaseName = "mpbiker_overlays", TatName = "MP_MP_Biker_Tat_024_F", Name = "Live to Ride" });
                TatThis.Add(new Tattoo { BaseName = "mpbiker_overlays", TatName = "MP_MP_Biker_Tat_020_F", Name = "Cranial Rose" });
                TatThis.Add(new Tattoo { BaseName = "mpbiker_overlays", TatName = "MP_MP_Biker_Tat_016_F", Name = "Macabre Tree" });
                TatThis.Add(new Tattoo { BaseName = "mpbiker_overlays", TatName = "MP_MP_Biker_Tat_012_F", Name = "Urban Stunter" });

                TatThis.Add(new Tattoo { BaseName = "mpstunt_overlays", TatName = "MP_MP_Stunt_tat_043_F", Name = "Engine Arm" });
                TatThis.Add(new Tattoo { BaseName = "mpstunt_overlays", TatName = "MP_MP_Stunt_tat_039_F", Name = "Kaboom" });
                TatThis.Add(new Tattoo { BaseName = "mpstunt_overlays", TatName = "MP_MP_Stunt_tat_035_F", Name = "Stuntman's End" });
                TatThis.Add(new Tattoo { BaseName = "mpstunt_overlays", TatName = "MP_MP_Stunt_tat_023_F", Name = "Tanked" });
                TatThis.Add(new Tattoo { BaseName = "mpstunt_overlays", TatName = "MP_MP_Stunt_tat_022_F", Name = "Piston Head" });
                TatThis.Add(new Tattoo { BaseName = "mpstunt_overlays", TatName = "MP_MP_Stunt_tat_008_F", Name = "Moonlight Ride" });
                TatThis.Add(new Tattoo { BaseName = "mpstunt_overlays", TatName = "MP_MP_Stunt_tat_002_F", Name = "Big Cat" });
                TatThis.Add(new Tattoo { BaseName = "mpstunt_overlays", TatName = "MP_MP_Stunt_tat_001_F", Name = "8 Eyed Skull" });

                TatThis.Add(new Tattoo { BaseName = "mplowrider2_overlays", TatName = "MP_LR_Tat_022_F", Name = "My Crazy Life" });
                TatThis.Add(new Tattoo { BaseName = "mplowrider2_overlays", TatName = "MP_LR_Tat_018_F", Name = "Skeleton Party" });
                TatThis.Add(new Tattoo { BaseName = "mplowrider2_overlays", TatName = "MP_LR_Tat_006_F", Name = "Love Hustle" });

                TatThis.Add(new Tattoo { BaseName = "mplowrider_overlays", TatName = "MP_LR_Tat_033_F", Name = "City Sorrow" });//
                TatThis.Add(new Tattoo { BaseName = "mplowrider_overlays", TatName = "MP_LR_Tat_027_F", Name = "Los Santos Life" });//
                TatThis.Add(new Tattoo { BaseName = "mplowrider_overlays", TatName = "MP_LR_Tat_005_F", Name = "No Evil" });//

                TatThis.Add(new Tattoo { BaseName = "mpluxe2_overlays", TatName = "MP_Luxe_Tat_028_F", Name = "Python Skull" });
                TatThis.Add(new Tattoo { BaseName = "mpluxe2_overlays", TatName = "MP_Luxe_Tat_018_F", Name = "Divine Goddess" });
                TatThis.Add(new Tattoo { BaseName = "mpluxe2_overlays", TatName = "MP_Luxe_Tat_016_F", Name = "Egyptian Mural" });
                TatThis.Add(new Tattoo { BaseName = "mpluxe2_overlays", TatName = "MP_Luxe_Tat_005_F", Name = "Fatal Dagger" });

                TatThis.Add(new Tattoo { BaseName = "mpluxe_overlays", TatName = "MP_Luxe_Tat_021_F", Name = "Gabriel" });
                TatThis.Add(new Tattoo { BaseName = "mpluxe_overlays", TatName = "MP_Luxe_Tat_020_F", Name = "Archangel and Mary" });
                TatThis.Add(new Tattoo { BaseName = "mpluxe_overlays", TatName = "MP_Luxe_Tat_009_F", Name = "Floral Symmetry" });

                TatThis.Add(new Tattoo { BaseName = "mpchristmas2_overlays", TatName = "MP_Xmas2_F_Tat_021", Name = "Time's Up Color" });
                TatThis.Add(new Tattoo { BaseName = "mpchristmas2_overlays", TatName = "MP_Xmas2_F_Tat_020", Name = "Time's Up Outline" });
                TatThis.Add(new Tattoo { BaseName = "mpchristmas2_overlays", TatName = "MP_Xmas2_F_Tat_012", Name = "8 Ball Skull" });
                TatThis.Add(new Tattoo { BaseName = "mpchristmas2_overlays", TatName = "MP_Xmas2_F_Tat_010", Name = "Electric Snake" });
                TatThis.Add(new Tattoo { BaseName = "mpchristmas2_overlays", TatName = "MP_Xmas2_F_Tat_000", Name = "Skull Rider" });

                TatThis.Add(new Tattoo { BaseName = "mphipster_overlays", TatName = "FM_Hip_F_Tat_048", Name = "Peace" });
                TatThis.Add(new Tattoo { BaseName = "mphipster_overlays", TatName = "FM_Hip_F_Tat_043", Name = "Triangle White" });
                TatThis.Add(new Tattoo { BaseName = "mphipster_overlays", TatName = "FM_Hip_F_Tat_039", Name = "Sleeve" });
                TatThis.Add(new Tattoo { BaseName = "mphipster_overlays", TatName = "FM_Hip_F_Tat_037", Name = "Sunrise" });
                TatThis.Add(new Tattoo { BaseName = "mphipster_overlays", TatName = "FM_Hip_F_Tat_034", Name = "Stop" });
                TatThis.Add(new Tattoo { BaseName = "mphipster_overlays", TatName = "FM_Hip_F_Tat_028", Name = "Thorny Rose" });
                TatThis.Add(new Tattoo { BaseName = "mphipster_overlays", TatName = "FM_Hip_F_Tat_027", Name = "Padlock" });
                TatThis.Add(new Tattoo { BaseName = "mphipster_overlays", TatName = "FM_Hip_F_Tat_026", Name = "Pizza" });
                TatThis.Add(new Tattoo { BaseName = "mphipster_overlays", TatName = "FM_Hip_F_Tat_016", Name = "Lightning Bolt" });
                TatThis.Add(new Tattoo { BaseName = "mphipster_overlays", TatName = "FM_Hip_F_Tat_015", Name = "Mustache" });
                TatThis.Add(new Tattoo { BaseName = "mphipster_overlays", TatName = "FM_Hip_F_Tat_007", Name = "Bricks" });
                TatThis.Add(new Tattoo { BaseName = "mphipster_overlays", TatName = "FM_Hip_F_Tat_003", Name = "Diamond Sparkle" });

                TatThis.Add(new Tattoo { BaseName = "mpbusiness_overlays", TatName = "MP_Buis_F_LArm_000", Name = "Greed is Good" });

                TatThis.Add(new Tattoo { BaseName = "mpbeach_overlays", TatName = "MP_Bea_F_LArm_001", Name = "Parrot" });
                TatThis.Add(new Tattoo { BaseName = "mpbeach_overlays", TatName = "MP_Bea_F_LArm_000", Name = "Tribal Flower" });

                TatThis.Add(new Tattoo { BaseName = "multiplayer_overlays", TatName = "FM_Tat_F_041", Name = "Dope Skull" });
                TatThis.Add(new Tattoo { BaseName = "multiplayer_overlays", TatName = "FM_Tat_F_031", Name = "Lady M" });
                TatThis.Add(new Tattoo { BaseName = "multiplayer_overlays", TatName = "FM_Tat_F_015", Name = "Zodiac Skull" });
                TatThis.Add(new Tattoo { BaseName = "multiplayer_overlays", TatName = "FM_Tat_F_006", Name = "Oriental Mural" });
                TatThis.Add(new Tattoo { BaseName = "multiplayer_overlays", TatName = "FM_Tat_F_005", Name = "Serpents" });

                TatThis.Add(new Tattoo { BaseName = "multiplayer_overlays", TatName = "FM_Tat_Award_F_015", Name = "Racing Brunette" });
                TatThis.Add(new Tattoo { BaseName = "multiplayer_overlays", TatName = "FM_Tat_Award_F_007", Name = "Racing Blonde" });
                TatThis.Add(new Tattoo { BaseName = "multiplayer_overlays", TatName = "FM_Tat_Award_F_001", Name = "Burning Heart" });//50 Death Match Award
                                                                                                                                      //not on list
                TatThis.Add(new Tattoo { BaseName = "mpluxe2_overlays", TatName = "MP_Luxe_Tat_031_F", Name = "Geometric Design" });

                Tatlist.Add(TatThis[RandInt(0, TatThis.Count - 1)]);
                TatThis.Clear();

                TatThis.Add(new Tattoo { BaseName = "mpsum2_overlays", TatName = "MP_Sum2_Tat_011_F", Name = "Nothing Mini About It" });
                TatThis.Add(new Tattoo { BaseName = "mpsum2_overlays", TatName = "MP_Sum2_Tat_012_F", Name = "Snake Revolver" });
                TatThis.Add(new Tattoo { BaseName = "mpsum2_overlays", TatName = "MP_Sum2_Tat_013_F", Name = "Weapon Sleeve" });
                TatThis.Add(new Tattoo { BaseName = "mpsum2_overlays", TatName = "MP_Sum2_Tat_030_F", Name = "Centipede" });
                TatThis.Add(new Tattoo { BaseName = "mpsum2_overlays", TatName = "MP_Sum2_Tat_031_F", Name = "Fleshy Eye" });
                TatThis.Add(new Tattoo { BaseName = "mpsum2_overlays", TatName = "MP_Sum2_Tat_045_F", Name = "Armored Arm" });
                TatThis.Add(new Tattoo { BaseName = "mpsum2_overlays", TatName = "MP_Sum2_Tat_046_F", Name = "Demon Smile" });
                TatThis.Add(new Tattoo { BaseName = "mpsum2_overlays", TatName = "MP_Sum2_Tat_047_F", Name = "Angel & Devil" });
                TatThis.Add(new Tattoo { BaseName = "mpsum2_overlays", TatName = "MP_Sum2_Tat_048_F", Name = "Death Is Certain" });

                TatThis.Add(new Tattoo { BaseName = "mpsecurity_overlays", TatName = "MP_Security_Tat_000_F", Name = "Hood Skeleton" });
                TatThis.Add(new Tattoo { BaseName = "mpsecurity_overlays", TatName = "MP_Security_Tat_005_F", Name = "Peacock" });
                TatThis.Add(new Tattoo { BaseName = "mpsecurity_overlays", TatName = "MP_Security_Tat_007_F", Name = "Ballas 4 Life" });
                TatThis.Add(new Tattoo { BaseName = "mpsecurity_overlays", TatName = "MP_Security_Tat_009_F", Name = "Ascension" });
                TatThis.Add(new Tattoo { BaseName = "mpsecurity_overlays", TatName = "MP_Security_Tat_012_F", Name = "Zombie Rhymes" });
                TatThis.Add(new Tattoo { BaseName = "mpsecurity_overlays", TatName = "MP_Security_Tat_020_F", Name = "Dog Fist" });

                TatThis.Add(new Tattoo { BaseName = "mpheist4_overlays", TatName = "MP_Heist4_Tat_032_F", Name = "K.U.L.T.99.1 FM" });
                TatThis.Add(new Tattoo { BaseName = "mpheist4_overlays", TatName = "MP_Heist4_Tat_031_F", Name = "Octopus Shades" });
                TatThis.Add(new Tattoo { BaseName = "mpheist4_overlays", TatName = "MP_Heist4_Tat_026_F", Name = "Shark Water" });
                TatThis.Add(new Tattoo { BaseName = "mpheist4_overlays", TatName = "MP_Heist4_Tat_012_F", Name = "Still Slipping" });
                TatThis.Add(new Tattoo { BaseName = "mpheist4_overlays", TatName = "MP_Heist4_Tat_011_F", Name = "Soulwax" });
                TatThis.Add(new Tattoo { BaseName = "mpheist4_overlays", TatName = "MP_Heist4_Tat_008_F", Name = "Smiley Glitch" });
                TatThis.Add(new Tattoo { BaseName = "mpheist4_overlays", TatName = "MP_Heist4_Tat_007_F", Name = "Skeleton DJ" });
                TatThis.Add(new Tattoo { BaseName = "mpheist4_overlays", TatName = "MP_Heist4_Tat_006_F", Name = "Music Locker" });
                TatThis.Add(new Tattoo { BaseName = "mpheist4_overlays", TatName = "MP_Heist4_Tat_005_F", Name = "LSUR" });
                TatThis.Add(new Tattoo { BaseName = "mpheist4_overlays", TatName = "MP_Heist4_Tat_003_F", Name = "Lighthouse" });
                TatThis.Add(new Tattoo { BaseName = "mpheist4_overlays", TatName = "MP_Heist4_Tat_002_F", Name = "Jellyfish Shades" });
                TatThis.Add(new Tattoo { BaseName = "mpheist4_overlays", TatName = "MP_Heist4_Tat_001_F", Name = "Tropical Dude" });
                TatThis.Add(new Tattoo { BaseName = "mpheist4_overlays", TatName = "MP_Heist4_Tat_000_F", Name = "Headphone Splat" });

                TatThis.Add(new Tattoo { BaseName = "mpheist3_overlays", TatName = "mpHeist3_Tat_034_F", Name = "LS Monogram" });

                TatThis.Add(new Tattoo { BaseName = "mpvinewood_overlays", TatName = "MP_Vinewood_Tat_028_F", Name = "Skull and Aces" });//
                TatThis.Add(new Tattoo { BaseName = "mpvinewood_overlays", TatName = "MP_Vinewood_Tat_025_F", Name = "Queen of Roses" });//
                TatThis.Add(new Tattoo { BaseName = "mpvinewood_overlays", TatName = "MP_Vinewood_Tat_018_F", Name = "The Gambler's Life" });//
                TatThis.Add(new Tattoo { BaseName = "mpvinewood_overlays", TatName = "MP_Vinewood_Tat_004_F", Name = "Lady Luck" });//

                TatThis.Add(new Tattoo { BaseName = "mpchristmas2017_overlays", TatName = "MP_Christmas2017_Tattoo_028_F", Name = "Spartan Mural" });
                TatThis.Add(new Tattoo { BaseName = "mpchristmas2017_overlays", TatName = "MP_Christmas2017_Tattoo_023_F", Name = "Samurai Tallship" });
                TatThis.Add(new Tattoo { BaseName = "mpchristmas2017_overlays", TatName = "MP_Christmas2017_Tattoo_018_F", Name = "Muscle Tear" });
                TatThis.Add(new Tattoo { BaseName = "mpchristmas2017_overlays", TatName = "MP_Christmas2017_Tattoo_017_F", Name = "Feather Sleeve" });
                TatThis.Add(new Tattoo { BaseName = "mpchristmas2017_overlays", TatName = "MP_Christmas2017_Tattoo_014_F", Name = "Celtic Band" });
                TatThis.Add(new Tattoo { BaseName = "mpchristmas2017_overlays", TatName = "MP_Christmas2017_Tattoo_012_F", Name = "Tiger Headdress" });
                TatThis.Add(new Tattoo { BaseName = "mpchristmas2017_overlays", TatName = "MP_Christmas2017_Tattoo_006_F", Name = "Medusa" });

                TatThis.Add(new Tattoo { BaseName = "mpsmuggler_overlays", TatName = "MP_Smuggler_Tattoo_023_F", Name = "Stylized Kraken" });
                TatThis.Add(new Tattoo { BaseName = "mpsmuggler_overlays", TatName = "MP_Smuggler_Tattoo_005_F", Name = "Mutiny" });
                TatThis.Add(new Tattoo { BaseName = "mpsmuggler_overlays", TatName = "MP_Smuggler_Tattoo_001_F", Name = "Crackshot" });

                TatThis.Add(new Tattoo { BaseName = "mpgunrunning_overlays", TatName = "MP_Gunrunning_Tattoo_024_F", Name = "Combat Reaper" });
                TatThis.Add(new Tattoo { BaseName = "mpgunrunning_overlays", TatName = "MP_Gunrunning_Tattoo_021_F", Name = "Have a Nice Day" });
                TatThis.Add(new Tattoo { BaseName = "mpgunrunning_overlays", TatName = "MP_Gunrunning_Tattoo_002_F", Name = "Grenade" });

                TatThis.Add(new Tattoo { BaseName = "mpimportexport_overlays", TatName = "MP_MP_ImportExport_Tat_007_F", Name = "Drive Forever" });
                TatThis.Add(new Tattoo { BaseName = "mpimportexport_overlays", TatName = "MP_MP_ImportExport_Tat_006_F", Name = "Engulfed Block" });
                TatThis.Add(new Tattoo { BaseName = "mpimportexport_overlays", TatName = "MP_MP_ImportExport_Tat_005_F", Name = "Dialed In" });
                TatThis.Add(new Tattoo { BaseName = "mpimportexport_overlays", TatName = "MP_MP_ImportExport_Tat_003_F", Name = "Mechanical Sleeve" });

                TatThis.Add(new Tattoo { BaseName = "mpbiker_overlays", TatName = "MP_MP_Biker_Tat_054_F", Name = "Mum" });
                TatThis.Add(new Tattoo { BaseName = "mpbiker_overlays", TatName = "MP_MP_Biker_Tat_049_F", Name = "These Colors Don't Run" });
                TatThis.Add(new Tattoo { BaseName = "mpbiker_overlays", TatName = "MP_MP_Biker_Tat_047_F", Name = "Snake Bike" });
                TatThis.Add(new Tattoo { BaseName = "mpbiker_overlays", TatName = "MP_MP_Biker_Tat_046_F", Name = "Skull Chain" });
                TatThis.Add(new Tattoo { BaseName = "mpbiker_overlays", TatName = "MP_MP_Biker_Tat_042_F", Name = "Grim Rider" });
                TatThis.Add(new Tattoo { BaseName = "mpbiker_overlays", TatName = "MP_MP_Biker_Tat_033_F", Name = "Eagle Emblem" });
                TatThis.Add(new Tattoo { BaseName = "mpbiker_overlays", TatName = "MP_MP_Biker_Tat_014_F", Name = "Lady Mortality" });
                TatThis.Add(new Tattoo { BaseName = "mpbiker_overlays", TatName = "MP_MP_Biker_Tat_007_F", Name = "Swooping Eagle" });

                TatThis.Add(new Tattoo { BaseName = "mpstunt_overlays", TatName = "MP_MP_Stunt_tat_049_F", Name = "Seductive Mechanic" });
                TatThis.Add(new Tattoo { BaseName = "mpstunt_overlays", TatName = "MP_MP_Stunt_tat_038_F", Name = "One Down Five Up" });
                TatThis.Add(new Tattoo { BaseName = "mpstunt_overlays", TatName = "MP_MP_Stunt_tat_036_F", Name = "Biker Stallion" });
                TatThis.Add(new Tattoo { BaseName = "mpstunt_overlays", TatName = "MP_MP_Stunt_tat_016_F", Name = "Coffin Racer" });
                TatThis.Add(new Tattoo { BaseName = "mpstunt_overlays", TatName = "MP_MP_Stunt_tat_010_F", Name = "Grave Vulture" });
                TatThis.Add(new Tattoo { BaseName = "mpstunt_overlays", TatName = "MP_MP_Stunt_tat_009_F", Name = "Arachnid of Death" });
                TatThis.Add(new Tattoo { BaseName = "mpstunt_overlays", TatName = "MP_MP_Stunt_tat_003_F", Name = "Poison Wrench" });

                TatThis.Add(new Tattoo { BaseName = "mplowrider2_overlays", TatName = "MP_LR_Tat_035_F", Name = "Black Tears" });
                TatThis.Add(new Tattoo { BaseName = "mplowrider2_overlays", TatName = "MP_LR_Tat_028_F", Name = "Loving Los Muertos" });
                TatThis.Add(new Tattoo { BaseName = "mplowrider2_overlays", TatName = "MP_LR_Tat_003_F", Name = "Lady Vamp" });

                TatThis.Add(new Tattoo { BaseName = "mplowrider_overlays", TatName = "MP_LR_Tat_015_F", Name = "Seductress" });//

                TatThis.Add(new Tattoo { BaseName = "mpluxe2_overlays", TatName = "MP_LUXE_TAT_026_F", Name = "Floral Print" });
                TatThis.Add(new Tattoo { BaseName = "mpluxe2_overlays", TatName = "MP_LUXE_TAT_017_F", Name = "Heavenly Deity" });
                TatThis.Add(new Tattoo { BaseName = "mpluxe2_overlays", TatName = "MP_LUXE_TAT_010_F", Name = "Intrometric" });

                TatThis.Add(new Tattoo { BaseName = "mpluxe_overlays", TatName = "MP_LUXE_TAT_019_F", Name = "Geisha Bloom" });
                TatThis.Add(new Tattoo { BaseName = "mpluxe_overlays", TatName = "MP_LUXE_TAT_013_F", Name = "Mermaid Harpist" });
                TatThis.Add(new Tattoo { BaseName = "mpluxe_overlays", TatName = "MP_LUXE_TAT_004_F", Name = "Floral Raven" });

                TatThis.Add(new Tattoo { BaseName = "mpchristmas2_overlays", TatName = "MP_Xmas2_F_Tat_027", Name = "Fuck Luck Color" });
                TatThis.Add(new Tattoo { BaseName = "mpchristmas2_overlays", TatName = "MP_Xmas2_F_Tat_026", Name = "Fuck Luck Outline" });
                TatThis.Add(new Tattoo { BaseName = "mpchristmas2_overlays", TatName = "MP_Xmas2_F_Tat_023", Name = "You're Next Color" });
                TatThis.Add(new Tattoo { BaseName = "mpchristmas2_overlays", TatName = "MP_Xmas2_F_Tat_022", Name = "You're Next Outline" });
                TatThis.Add(new Tattoo { BaseName = "mpchristmas2_overlays", TatName = "MP_Xmas2_F_Tat_008", Name = "Death Before Dishonor" });
                TatThis.Add(new Tattoo { BaseName = "mpchristmas2_overlays", TatName = "MP_Xmas2_F_Tat_004", Name = "Snake Shaded" });
                TatThis.Add(new Tattoo { BaseName = "mpchristmas2_overlays", TatName = "MP_Xmas2_F_Tat_003", Name = "Snake Outline" });

                TatThis.Add(new Tattoo { BaseName = "mphipster_overlays", TatName = "FM_Hip_F_Tat_045", Name = "Mesh Band" });
                TatThis.Add(new Tattoo { BaseName = "mphipster_overlays", TatName = "FM_Hip_F_Tat_044", Name = "Triangle Black" });
                TatThis.Add(new Tattoo { BaseName = "mphipster_overlays", TatName = "FM_Hip_F_Tat_036", Name = "Shapes" });
                TatThis.Add(new Tattoo { BaseName = "mphipster_overlays", TatName = "FM_Hip_F_Tat_023", Name = "Smiley" });
                TatThis.Add(new Tattoo { BaseName = "mphipster_overlays", TatName = "FM_Hip_F_Tat_022", Name = "Pencil" });
                TatThis.Add(new Tattoo { BaseName = "mphipster_overlays", TatName = "FM_Hip_F_Tat_020", Name = "Geo Pattern" });
                TatThis.Add(new Tattoo { BaseName = "mphipster_overlays", TatName = "FM_Hip_F_Tat_018", Name = "Origami" });
                TatThis.Add(new Tattoo { BaseName = "mphipster_overlays", TatName = "FM_Hip_F_Tat_017", Name = "Eye Triangle" });
                TatThis.Add(new Tattoo { BaseName = "mphipster_overlays", TatName = "FM_Hip_F_Tat_014", Name = "Spray Can" });
                TatThis.Add(new Tattoo { BaseName = "mphipster_overlays", TatName = "FM_Hip_F_Tat_010", Name = "Horseshoe" });
                TatThis.Add(new Tattoo { BaseName = "mphipster_overlays", TatName = "FM_Hip_F_Tat_008", Name = "Cube" });
                TatThis.Add(new Tattoo { BaseName = "mphipster_overlays", TatName = "FM_Hip_F_Tat_004", Name = "Bone" });
                TatThis.Add(new Tattoo { BaseName = "mphipster_overlays", TatName = "FM_Hip_F_Tat_001", Name = "Single Arrow" });

                TatThis.Add(new Tattoo { BaseName = "mpbusiness_overlays", TatName = "MP_Buis_F_RArm_000", Name = "Dollar Sign" });

                TatThis.Add(new Tattoo { BaseName = "mpbeach_overlays", TatName = "MP_Bea_F_RArm_001", Name = "Tribal Fish" });

                TatThis.Add(new Tattoo { BaseName = "multiplayer_overlays", TatName = "FM_Tat_F_047", Name = "Lion" });
                TatThis.Add(new Tattoo { BaseName = "multiplayer_overlays", TatName = "FM_Tat_F_038", Name = "Dagger" });
                TatThis.Add(new Tattoo { BaseName = "multiplayer_overlays", TatName = "FM_Tat_F_028", Name = "Mermaid" });
                TatThis.Add(new Tattoo { BaseName = "multiplayer_overlays", TatName = "FM_Tat_F_027", Name = "Virgin Mary" });
                TatThis.Add(new Tattoo { BaseName = "multiplayer_overlays", TatName = "FM_Tat_F_018", Name = "Serpent Skull" });
                TatThis.Add(new Tattoo { BaseName = "multiplayer_overlays", TatName = "FM_Tat_F_014", Name = "Flower Mural" });
                TatThis.Add(new Tattoo { BaseName = "multiplayer_overlays", TatName = "FM_Tat_F_003", Name = "Dragons and Skull" });
                TatThis.Add(new Tattoo { BaseName = "multiplayer_overlays", TatName = "FM_Tat_F_001", Name = "Dragons" });
                //TatThis.Add(new Tattoo { BaseName = "multiplayer_overlays", TatName = "FM_Tat_F_000", Name = "Brotherhood" } );-empty load?

                TatThis.Add(new Tattoo { BaseName = "multiplayer_overlays", TatName = "FM_Tat_Award_F_010", Name = "Ride or Die" });
                TatThis.Add(new Tattoo { BaseName = "multiplayer_overlays", TatName = "FM_Tat_Award_F_002", Name = "Grim Reaper Smoking Gun" });//Clear 5 Gang Atacks in a Day Award
                                                                                                                                                //Not In List
                TatThis.Add(new Tattoo { BaseName = "mpluxe2_overlays", TatName = "MP_LUXE_TAT_030_F", Name = "Geometric Design" });

                Tatlist.Add(TatThis[RandInt(0, TatThis.Count - 1)]);
                TatThis.Clear();

                TatThis.Add(new Tattoo { BaseName = "mpsum2_overlays", TatName = "MP_Sum2_Tat_002_F", Name = "Cobra Biker" });
                TatThis.Add(new Tattoo { BaseName = "mpsum2_overlays", TatName = "MP_Sum2_Tat_014_F", Name = "Minimal SMG" });
                TatThis.Add(new Tattoo { BaseName = "mpsum2_overlays", TatName = "MP_Sum2_Tat_015_F", Name = "Minimal Advanced Rifle" });
                TatThis.Add(new Tattoo { BaseName = "mpsum2_overlays", TatName = "MP_Sum2_Tat_016_F", Name = "Minimal Sniper Rifle" });
                TatThis.Add(new Tattoo { BaseName = "mpsum2_overlays", TatName = "MP_Sum2_Tat_032_F", Name = "Many-eyed Goat" });
                TatThis.Add(new Tattoo { BaseName = "mpsum2_overlays", TatName = "MP_Sum2_Tat_053_F", Name = "Mobster Skull" });
                TatThis.Add(new Tattoo { BaseName = "mpsum2_overlays", TatName = "MP_Sum2_Tat_054_F", Name = "Wounded Head" });
                TatThis.Add(new Tattoo { BaseName = "mpsum2_overlays", TatName = "MP_Sum2_Tat_055_F", Name = "Stabbed Skull" });
                TatThis.Add(new Tattoo { BaseName = "mpsum2_overlays", TatName = "MP_Sum2_Tat_056_F", Name = "Tiger Blade" });

                TatThis.Add(new Tattoo { BaseName = "mpsecurity_overlays", TatName = "MP_Security_Tat_022_F", Name = "LS Smoking Cartridges" });
                TatThis.Add(new Tattoo { BaseName = "mpsecurity_overlays", TatName = "MP_Security_Tat_023_F", Name = "Trust" });

                TatThis.Add(new Tattoo { BaseName = "mpheist4_overlays", TatName = "MP_Heist4_Tat_029_F", Name = "Soundwaves" });
                TatThis.Add(new Tattoo { BaseName = "mpheist4_overlays", TatName = "MP_Heist4_Tat_028_F", Name = "Skull Waters" });
                TatThis.Add(new Tattoo { BaseName = "mpheist4_overlays", TatName = "MP_Heist4_Tat_025_F", Name = "Glow Princess" });
                TatThis.Add(new Tattoo { BaseName = "mpheist4_overlays", TatName = "MP_Heist4_Tat_024_F", Name = "Pineapple Skull" });
                TatThis.Add(new Tattoo { BaseName = "mpheist4_overlays", TatName = "MP_Heist4_Tat_010_F", Name = "Tropical Serpent" });

                TatThis.Add(new Tattoo { BaseName = "mpheist3_overlays", TatName = "mpHeist3_Tat_032_F", Name = "Love Fist" });

                TatThis.Add(new Tattoo { BaseName = "mpvinewood_overlays", TatName = "MP_Vinewood_Tat_027_F", Name = "8-Ball Rose" });//
                TatThis.Add(new Tattoo { BaseName = "mpvinewood_overlays", TatName = "MP_Vinewood_Tat_013_F", Name = "One-armed Bandit" });//

                TatThis.Add(new Tattoo { BaseName = "mpgunrunning_overlays", TatName = "MP_Gunrunning_Tattoo_023_F", Name = "Rose Revolver" });
                TatThis.Add(new Tattoo { BaseName = "mpgunrunning_overlays", TatName = "MP_Gunrunning_Tattoo_011_F", Name = "Death Skull" });
                TatThis.Add(new Tattoo { BaseName = "mpgunrunning_overlays", TatName = "MP_Gunrunning_Tattoo_007_F", Name = "Stylized Tiger" });
                TatThis.Add(new Tattoo { BaseName = "mpgunrunning_overlays", TatName = "MP_Gunrunning_Tattoo_005_F", Name = "Patriot Skull" });

                TatThis.Add(new Tattoo { BaseName = "mpbiker_overlays", TatName = "MP_MP_Biker_Tat_057_F", Name = "Laughing Skull" });
                TatThis.Add(new Tattoo { BaseName = "mpbiker_overlays", TatName = "MP_MP_Biker_Tat_056_F", Name = "Bone Cruiser" });
                TatThis.Add(new Tattoo { BaseName = "mpbiker_overlays", TatName = "MP_MP_Biker_Tat_044_F", Name = "Ride Free" });
                TatThis.Add(new Tattoo { BaseName = "mpbiker_overlays", TatName = "MP_MP_Biker_Tat_037_F", Name = "Scorched Soul" });
                TatThis.Add(new Tattoo { BaseName = "mpbiker_overlays", TatName = "MP_MP_Biker_Tat_036_F", Name = "Engulfed Skull" });
                TatThis.Add(new Tattoo { BaseName = "mpbiker_overlays", TatName = "MP_MP_Biker_Tat_027_F", Name = "Bad Luck" });
                TatThis.Add(new Tattoo { BaseName = "mpbiker_overlays", TatName = "MP_MP_Biker_Tat_015_F", Name = "Ride or Die" });
                TatThis.Add(new Tattoo { BaseName = "mpbiker_overlays", TatName = "MP_MP_Biker_Tat_002_F", Name = "Rose Tribute" });

                TatThis.Add(new Tattoo { BaseName = "mpstunt_overlays", TatName = "MP_MP_Stunt_tat_031_F", Name = "Stunt Jesus" });
                TatThis.Add(new Tattoo { BaseName = "mpstunt_overlays", TatName = "MP_MP_Stunt_tat_028_F", Name = "Quad Goblin" });
                TatThis.Add(new Tattoo { BaseName = "mpstunt_overlays", TatName = "MP_MP_Stunt_tat_021_F", Name = "Golden Cobra" });
                TatThis.Add(new Tattoo { BaseName = "mpstunt_overlays", TatName = "MP_MP_Stunt_tat_013_F", Name = "Dirt Track Hero" });
                TatThis.Add(new Tattoo { BaseName = "mpstunt_overlays", TatName = "MP_MP_Stunt_tat_007_F", Name = "Dagger Devil" });

                TatThis.Add(new Tattoo { BaseName = "mplowrider2_overlays", TatName = "MP_LR_Tat_029_F", Name = "Death Us Do Part" });

                TatThis.Add(new Tattoo { BaseName = "mplowrider_overlays", TatName = "MP_LR_Tat_020_F", Name = "Presidents" });//
                TatThis.Add(new Tattoo { BaseName = "mplowrider_overlays", TatName = "MP_LR_Tat_007_F", Name = "LS Serpent" });//

                TatThis.Add(new Tattoo { BaseName = "mpluxe2_overlays", TatName = "MP_Luxe_Tat_011_F", Name = "Cross of Roses" });
                TatThis.Add(new Tattoo { BaseName = "mpluxe_overlays", TatName = "MP_LUXE_TAT_000_F", Name = "Serpent of Death" });

                TatThis.Add(new Tattoo { BaseName = "mpchristmas2_overlays", TatName = "MP_Xmas2_F_Tat_002", Name = "Spider Color" });
                TatThis.Add(new Tattoo { BaseName = "mpchristmas2_overlays", TatName = "MP_Xmas2_F_Tat_001", Name = "Spider Outline" });

                TatThis.Add(new Tattoo { BaseName = "mphipster_overlays", TatName = "FM_Hip_F_Tat_040", Name = "Black Anchor" });
                TatThis.Add(new Tattoo { BaseName = "mphipster_overlays", TatName = "FM_Hip_F_Tat_019", Name = "Charm" });
                TatThis.Add(new Tattoo { BaseName = "mphipster_overlays", TatName = "FM_Hip_F_Tat_009", Name = "Squares" });

                TatThis.Add(new Tattoo { BaseName = "mpbusiness_overlays", TatName = "MP_Buis_F_LLeg_000", Name = "Single" });

                TatThis.Add(new Tattoo { BaseName = "multiplayer_overlays", TatName = "FM_Tat_F_032", Name = "Faith" });
                TatThis.Add(new Tattoo { BaseName = "multiplayer_overlays", TatName = "FM_Tat_F_037", Name = "Grim Reaper" });
                TatThis.Add(new Tattoo { BaseName = "multiplayer_overlays", TatName = "FM_Tat_F_035", Name = "Dragon" });
                TatThis.Add(new Tattoo { BaseName = "multiplayer_overlays", TatName = "FM_Tat_F_033", Name = "Chinese Dragon" });
                TatThis.Add(new Tattoo { BaseName = "multiplayer_overlays", TatName = "FM_Tat_F_026", Name = "Smoking Dagger" });
                TatThis.Add(new Tattoo { BaseName = "multiplayer_overlays", TatName = "FM_Tat_F_023", Name = "Hottie" });
                TatThis.Add(new Tattoo { BaseName = "multiplayer_overlays", TatName = "FM_Tat_F_021", Name = "Serpent Skull" });
                TatThis.Add(new Tattoo { BaseName = "multiplayer_overlays", TatName = "FM_Tat_F_008", Name = "Dragon Mural" });
                TatThis.Add(new Tattoo { BaseName = "multiplayer_overlays", TatName = "FM_Tat_F_002", Name = "Melting Skull" });

                TatThis.Add(new Tattoo { BaseName = "multiplayer_overlays", TatName = "FM_Tat_Award_F_009", Name = "Dragon and Dagger" });

                Tatlist.Add(TatThis[RandInt(0, TatThis.Count - 1)]);
                TatThis.Clear();

                TatThis.Add(new Tattoo { BaseName = "mpsum2_overlays", TatName = "MP_Sum2_Tat_017_F", Name = "Skull Grenade" });
                TatThis.Add(new Tattoo { BaseName = "mpsum2_overlays", TatName = "MP_Sum2_Tat_033_F", Name = "Three-eyed Demon" });
                TatThis.Add(new Tattoo { BaseName = "mpsum2_overlays", TatName = "MP_Sum2_Tat_034_F", Name = "Smoldering Reaper" });
                TatThis.Add(new Tattoo { BaseName = "mpsum2_overlays", TatName = "MP_Sum2_Tat_050_F", Name = "Gold Gun" });
                TatThis.Add(new Tattoo { BaseName = "mpsum2_overlays", TatName = "MP_Sum2_Tat_051_F", Name = "Blue Serpent" });
                TatThis.Add(new Tattoo { BaseName = "mpsum2_overlays", TatName = "MP_Sum2_Tat_052_F", Name = "Night Demon" });

                TatThis.Add(new Tattoo { BaseName = "mpsecurity_overlays", TatName = "MP_Security_Tat_003_F", Name = "Bandana Knife" });
                TatThis.Add(new Tattoo { BaseName = "mpsecurity_overlays", TatName = "MP_Security_Tat_021_F", Name = "Graffiti Skull" });

                TatThis.Add(new Tattoo { BaseName = "mpheist4_overlays", TatName = "MP_Heist4_Tat_027_F", Name = "Skullphones" });

                TatThis.Add(new Tattoo { BaseName = "mpheist3_overlays", TatName = "mpHeist3_Tat_031_F", Name = "Kifflom" });

                TatThis.Add(new Tattoo { BaseName = "mpvinewood_overlays", TatName = "MP_Vinewood_Tat_020_F", Name = "Cash is King" });//

                TatThis.Add(new Tattoo { BaseName = "mpsmuggler_overlays", TatName = "MP_Smuggler_Tattoo_020_F", Name = "Homeward Bound" });

                TatThis.Add(new Tattoo { BaseName = "mpgunrunning_overlays", TatName = "MP_Gunrunning_Tattoo_030_F", Name = "Pistol Ace" });
                TatThis.Add(new Tattoo { BaseName = "mpgunrunning_overlays", TatName = "MP_Gunrunning_Tattoo_026_F", Name = "Restless Skull" });
                TatThis.Add(new Tattoo { BaseName = "mpgunrunning_overlays", TatName = "MP_Gunrunning_Tattoo_006_F", Name = "Combat Skull" });

                TatThis.Add(new Tattoo { BaseName = "mpbiker_overlays", TatName = "MP_MP_Biker_Tat_048_F", Name = "STFU" });
                TatThis.Add(new Tattoo { BaseName = "mpbiker_overlays", TatName = "MP_MP_Biker_Tat_040_F", Name = "American Made" });
                TatThis.Add(new Tattoo { BaseName = "mpbiker_overlays", TatName = "MP_MP_Biker_Tat_028_F", Name = "Dusk Rider" });
                TatThis.Add(new Tattoo { BaseName = "mpbiker_overlays", TatName = "MP_MP_Biker_Tat_022_F", Name = "Western Insignia" });
                TatThis.Add(new Tattoo { BaseName = "mpbiker_overlays", TatName = "MP_MP_Biker_Tat_004_F", Name = "Dragon's Fury" });

                TatThis.Add(new Tattoo { BaseName = "mpstunt_overlays", TatName = "MP_MP_Stunt_tat_047_F", Name = "Brake Knife" });
                TatThis.Add(new Tattoo { BaseName = "mpstunt_overlays", TatName = "MP_MP_Stunt_tat_045_F", Name = "Severed Hand" });
                TatThis.Add(new Tattoo { BaseName = "mpstunt_overlays", TatName = "MP_MP_Stunt_tat_032_F", Name = "Wheelie Mouse" });
                TatThis.Add(new Tattoo { BaseName = "mpstunt_overlays", TatName = "MP_MP_Stunt_tat_025_F", Name = "Speed Freak" });
                TatThis.Add(new Tattoo { BaseName = "mpstunt_overlays", TatName = "MP_MP_Stunt_tat_020_F", Name = "Piston Angel" });
                TatThis.Add(new Tattoo { BaseName = "mpstunt_overlays", TatName = "MP_MP_Stunt_tat_015_F", Name = "Praying Gloves" });
                TatThis.Add(new Tattoo { BaseName = "mpstunt_overlays", TatName = "MP_MP_Stunt_tat_005_F", Name = "Demon Spark Plug" });

                TatThis.Add(new Tattoo { BaseName = "mplowrider2_overlays", TatName = "MP_LR_Tat_030_F", Name = "San Andreas Prayer" });

                TatThis.Add(new Tattoo { BaseName = "mplowrider_overlays", TatName = "MP_LR_Tat_023_F", Name = "Dance of Hearts" });//
                TatThis.Add(new Tattoo { BaseName = "mplowrider_overlays", TatName = "MP_LR_Tat_017_F", Name = "Ink Me" });//

                TatThis.Add(new Tattoo { BaseName = "mpluxe2_overlays", TatName = "MP_LUXE_TAT_023_F", Name = "Starmetric" });

                TatThis.Add(new Tattoo { BaseName = "mpluxe_overlays", TatName = "MP_LUXE_TAT_001_F", Name = "Elaborate Los Muertos" });

                TatThis.Add(new Tattoo { BaseName = "mpchristmas2_overlays", TatName = "MP_Xmas2_F_Tat_014", Name = "Floral Dagger" });

                TatThis.Add(new Tattoo { BaseName = "mphipster_overlays", TatName = "FM_Hip_F_Tat_042", Name = "Sparkplug" });
                TatThis.Add(new Tattoo { BaseName = "mphipster_overlays", TatName = "FM_Hip_F_Tat_038", Name = "Grub" });

                TatThis.Add(new Tattoo { BaseName = "mpbusiness_overlays", TatName = "MP_Buis_F_RLeg_000", Name = "Diamond Crown" });

                TatThis.Add(new Tattoo { BaseName = "mpbeach_overlays", TatName = "MP_Bea_F_RLeg_000", Name = "School of Fish" });

                TatThis.Add(new Tattoo { BaseName = "multiplayer_overlays", TatName = "FM_Tat_F_043", Name = "Indian Ram" });
                TatThis.Add(new Tattoo { BaseName = "multiplayer_overlays", TatName = "FM_Tat_F_042", Name = "Flaming Scorpion" });
                TatThis.Add(new Tattoo { BaseName = "multiplayer_overlays", TatName = "FM_Tat_F_040", Name = "Flaming Skull" });
                TatThis.Add(new Tattoo { BaseName = "multiplayer_overlays", TatName = "FM_Tat_F_039", Name = "Broken Skull" });
                TatThis.Add(new Tattoo { BaseName = "multiplayer_overlays", TatName = "FM_Tat_F_022", Name = "Fiery Dragon" });
                TatThis.Add(new Tattoo { BaseName = "multiplayer_overlays", TatName = "FM_Tat_F_017", Name = "Tribal" });
                TatThis.Add(new Tattoo { BaseName = "multiplayer_overlays", TatName = "FM_Tat_F_007", Name = "The Warrior" });

                TatThis.Add(new Tattoo { BaseName = "multiplayer_overlays", TatName = "FM_Tat_Award_F_006", Name = "Skull and Sword" });//Collect 25 Bounties Award

                Tatlist.Add(TatThis[RandInt(0, TatThis.Count - 1)]);
                TatThis.Clear();
            }

            return Tatlist;
        }
        public static Vector3 YoPoza()
        {
            Ped XPed = Game.Player.Character;
            return XPed.Position;
        }
        public static void SetBTimer(int AddTime, int iSetPos)
        {
            if (iSetPos != -1)
            {
                if (iSetPos < DataStore.iTimers.Count)
                    DataStore.iTimers[iSetPos] = Game.GameTime + AddTime;
                else
                    DataStore.iTimers.Add(Game.GameTime + AddTime);
            }
            else
                DataStore.iTimers.Add(Game.GameTime + AddTime);
        }
        public static bool BTimer(int iMyTime)
        {
            bool bIntime = false;

            if (iMyTime < DataStore.iTimers.Count)
            {
                if (DataStore.iTimers[iMyTime] < Game.GameTime)
                    bIntime = true;
            }
            return bIntime;
        }
        public static int FindRandom(int iList, int iMin, int iMax)
        {
            LoggerLight.GetLogging("ReturnValues.FindRandom, iList == " + iList);

            int iBe = 0;
            RandomPlusList XSets = new RandomPlusList();

            if (File.Exists(DataStore.sRandFile))
            {
                XSets = XmlReadWrite.LoadRando(DataStore.sRandFile);

                if (XSets.BigRanList.Count() < iList + 1)
                {
                    for (int i = XSets.BigRanList.Count() - 1; i < iList + 1; i++)
                    {
                        RandomPlus iBlank = new RandomPlus();
                        XSets.BigRanList.Add(iBlank);
                    }
                }

                for (int i = 0; i < XSets.BigRanList[iList].RandNums.Count; i++)
                {
                    if (XSets.BigRanList[iList].RandNums[i] > iMax || XSets.BigRanList[iList].RandNums[i] < iMin)
                        XSets.BigRanList[iList].RandNums.RemoveAt(i);
                }

                if (XSets.BigRanList[iList].RandNums.Count == 0)
                {
                    for (int i = iMin; i < iMax + 1; i++)
                        XSets.BigRanList[iList].RandNums.Add(i);
                }

                int iRanNum = ReturnValues.RandInt(0, XSets.BigRanList[iList].RandNums.Count - 1);
                iBe = XSets.BigRanList[iList].RandNums[iRanNum];
                XSets.BigRanList[iList].RandNums.RemoveAt(iRanNum);
            }
            else
            {
                for (int i = 0; i < iList + 1; i++)
                {
                    RandomPlus iBlank = new RandomPlus();
                    XSets.BigRanList.Add(iBlank);
                }

                for (int i = iMin; i < iMax + 1; i++)
                    XSets.BigRanList[iList].RandNums.Add(i);

                int iRanNum = ReturnValues.RandInt(0, XSets.BigRanList[iList].RandNums.Count - 1);
                iBe = XSets.BigRanList[iList].RandNums[iRanNum];
                XSets.BigRanList[iList].RandNums.RemoveAt(iRanNum);
            }
            XmlReadWrite.SaveRando(XSets, DataStore.sRandFile);

            return iBe;
        }
        public static string SillyNameList()
        {
            LoggerLight.GetLogging("ReturnValues.SillyNameList");
            string MySilly = "";

            List<string> sSillyNames = new List<string>
            {
                "0",              //0
                "1",              //1
                "2",              //2
                "3",              //3
                "4",              //4
                "5",              //5
                "6",              //6
                "7",              //7
                "8",              //8
                "9",              //9
                "ay",             //10
                "ee",             //11
                "igh",            //12
                "ow",             //13
                "oo",             //14
                "or",             //15
                "air",            //16
                "ir",             //17
                "ou",             //18
                "oy",             //19
                "ai",             //20
                "ea",             //21
                "ie",             //22
                "ew",             //23
                "ur",             //24
                "ow",             //25
                "oi",             //26
                "ire",            //27
                "ear",            //28
                "ure",            //29
                "tion",           //30
                "ey",             //31
                "ore",            //32
                "ere",            //33
                "oor",            //34
                "X",              //35
                "-",              //36
                "^",              //37
                "*",              //38
                "#",              //39
                "$",              //40
                "TyHrd",          //41
                "Luzz",           //42
                "Killz",          //43
                "| | |",          //44
                "{[]}",           //45
                "A",              //46
                "B",              //47
                "C",              ///48
                "D",              ///49
                "E",              ///50
                "F",              ///51
                "G",              ///52
                "H",              ///53
                "I",              ///54
                "J",              ///55
                "K",              ///56
                "L",              ///57
                "M",              ///58
                "N",              ///59
                "O",              ///60
                "P"               ///61
            };

            int iName = ReturnValues.FindRandom(3, 2, 3);

            for (int i = 0; i < iName; i++)
                MySilly = MySilly + sSillyNames[ReturnValues.FindRandom(4, 10, 34)];

            MySilly.Remove(0, 1);
            MySilly = sSillyNames[ReturnValues.FindRandom(5, 46, 61)] + MySilly;

            if (MySilly.Length < 8)
            {
                if (ReturnValues.FindRandom(7, 0, 20) < 15)
                {
                    iName = ReturnValues.FindRandom(8, 1, 4);
                    for (int i = 0; i < iName; i++)
                        MySilly = MySilly + sSillyNames[ReturnValues.FindRandom(9, 0, 9)];
                }
                else
                {
                    string sPrefix1 = sSillyNames[ReturnValues.FindRandom(10, 35, 40)];
                    string sPrefix2 = sSillyNames[ReturnValues.FindRandom(11, 35, 40)];

                    MySilly = sPrefix1 + sPrefix2 + MySilly + sPrefix2 + sPrefix1;
                }
            }
            else if (MySilly.Length < 4)
                MySilly = MySilly + sSillyNames[ReturnValues.FindRandom(6, 41, 45)];

            return MySilly;
        }
        public static int RandInt(int iMin, int iMax)
        {
            return Function.Call<int>(Hash.GET_RANDOM_INT_IN_RANGE, iMin, iMax);
        }
        public static float RandFlow(float fMin, float fMax)
        {
            return Function.Call<float>(Hash.GET_RANDOM_INT_IN_RANGE, fMin, fMax);
        }
        public static bool BeInAngle(float fRange, float fValue_01, float fValue_02)
        {
            LoggerLight.GetLogging("ReturnValues.BeInAngle, fRange == " + fRange + ", fValue_01 == " + fValue_01 + ", fValue_02 == " + fValue_02);

            bool bInRange = false;

            if (fValue_01 < fRange)
            {
                if (fValue_02 > 360.00 - fRange)
                    bInRange = true;
            }
            else if (fValue_02 < fRange)
            {
                if (fValue_01 > 360.00 - fRange)
                    bInRange = true;
            }
            else if (fValue_01 < fValue_02 + fRange)
            {
                if (fValue_01 > fValue_02 - fRange)
                    bInRange = true;
            }

            return bInRange;
        }
        public static bool WhileButtonDown(int CButt, bool bDisable)
        {
            if (bDisable)
                ButtonDisabler(CButt); ;

            bool bButt = Function.Call<bool>(Hash.IS_DISABLED_CONTROL_PRESSED, 0, CButt);

            if (bButt)
            {
                while (!Function.Call<bool>(Hash.IS_DISABLED_CONTROL_JUST_RELEASED, 0, CButt))
                    Script.Wait(1);
            }

            return bButt;
        }
        public static bool ButtonDown(int CButt, bool bDisable)
        {
            if (bDisable)
                ButtonDisabler(CButt);
            return Function.Call<bool>(Hash.IS_DISABLED_CONTROL_PRESSED, 0, CButt);
        }
        private static void ButtonDisabler(int LButt)
        {
            Function.Call(Hash.DISABLE_CONTROL_ACTION, 0, LButt, true);
        }
    }
}
