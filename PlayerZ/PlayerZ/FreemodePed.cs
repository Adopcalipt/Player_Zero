using GTA;
using GTA.Native;
using PlayerZero.Classes;
using System.Collections.Generic;
using System.IO;

namespace PlayerZero
{
    public class FreemodePed
    {
        private static readonly List<Tattoo> maleTats01 = new List<Tattoo>
        {
            new Tattoo("mpsum2_overlays", "MP_Sum2_Tat_006_M", "Painted Micro SMG"),
            new Tattoo("mpsum2_overlays", "MP_Sum2_Tat_007_M", "Weapon King"),
            new Tattoo("mpsum2_overlays", "MP_Sum2_Tat_035_M", "Sniff Sniff"),
            new Tattoo("mpsum2_overlays", "MP_Sum2_Tat_036_M", "Charm Pattern"),
            new Tattoo("mpsum2_overlays", "MP_Sum2_Tat_037_M", "Witch & Skull"),
            new Tattoo("mpsum2_overlays", "MP_Sum2_Tat_038_M", "Pumpkin Bug"),
            new Tattoo("mpsum2_overlays", "MP_Sum2_Tat_039_M", "Sinner"),
            new Tattoo("mpsum2_overlays", "MP_Sum2_Tat_057_M", "Gray Demon"),

            new Tattoo("mpsecurity_overlays", "MP_Security_Tat_004_M", "Hood Heart"),
            new Tattoo("mpsecurity_overlays", "MP_Security_Tat_008_M", "Los Santos Tag"),
            new Tattoo("mpsecurity_overlays", "MP_Security_Tat_013_M", "Blessed Boombox"),
            new Tattoo("mpsecurity_overlays", "MP_Security_Tat_014_M", "Chamberlain Hills"),
            new Tattoo("mpsecurity_overlays", "MP_Security_Tat_015_M", "Smoking Barrels"),
            new Tattoo("mpsecurity_overlays", "MP_Security_Tat_026_M", "Dollar Guns Crossed"),

            new Tattoo("mpheist4_overlays", "MP_Heist4_Tat_021_M", "Skull Surfer"),//
            new Tattoo("mpheist4_overlays", "MP_Heist4_Tat_020_M", "Speaker Tower"),//
            new Tattoo("mpheist4_overlays", "MP_Heist4_Tat_019_M", "Record Shot"),//
            new Tattoo("mpheist4_overlays", "MP_Heist4_Tat_018_M", "Record Head"),//
            new Tattoo("mpheist4_overlays", "MP_Heist4_Tat_017_M", "Tropical Sorcerer"),//
            new Tattoo("mpheist4_overlays", "MP_Heist4_Tat_016_M", "Rose Panther"),//
            new Tattoo("mpheist4_overlays", "MP_Heist4_Tat_015_M", "Paradise Ukulele"),//
            new Tattoo("mpheist4_overlays", "MP_Heist4_Tat_014_M", "Paradise Nap"),//
            new Tattoo("mpheist4_overlays", "MP_Heist4_Tat_013_M", "Wild Dancers"),//

            new Tattoo("mpheist3_overlays", "mpHeist3_Tat_039_M", "Space Rangers"),//
            new Tattoo("mpheist3_overlays", "mpHeist3_Tat_038_M", "Robot Bubblegum"),//
            new Tattoo("mpheist3_overlays", "mpHeist3_Tat_036_M", "LS Shield"),//
            new Tattoo("mpheist3_overlays", "mpHeist3_Tat_030_M", "Howitzer"),//
            new Tattoo("mpheist3_overlays", "mpHeist3_Tat_028_M", "Bananas Gone Bad"),//
            new Tattoo("mpheist3_overlays", "mpHeist3_Tat_027_M", "Epsilon"),//
            new Tattoo("mpheist3_overlays", "mpHeist3_Tat_024_M", "Mount Chiliad"),//
            new Tattoo("mpheist3_overlays", "mpHeist3_Tat_023_M", "Bigfoot"),//

            new Tattoo("mpvinewood_overlays", "mp_vinewood_tat_032_M", "Play Your Ace"),//
            new Tattoo("mpvinewood_overlays", "MP_Vinewood_Tat_029_M", "The Table"),//
            new Tattoo("mpvinewood_overlays", "MP_Vinewood_Tat_021_M", "Show Your Hand"),//
            new Tattoo("mpvinewood_overlays", "MP_Vinewood_Tat_017_M", "Roll the Dice"),//
            new Tattoo("mpvinewood_overlays", "MP_Vinewood_Tat_015_M", "The Jolly Joker"),//
            new Tattoo("mpvinewood_overlays", "MP_Vinewood_Tat_011_M", "Life's a Gamble"),//
            new Tattoo("mpvinewood_overlays", "MP_Vinewood_Tat_010_M", "Photo Finish"),//
            new Tattoo("mpvinewood_overlays", "MP_Vinewood_Tat_009_M", "Till Death Do Us Part"),//
            new Tattoo("mpvinewood_overlays", "MP_Vinewood_Tat_008_M", "Snake Eyes"),//
            new Tattoo("mpvinewood_overlays", "MP_Vinewood_Tat_007_M", "777"),//
            new Tattoo("mpvinewood_overlays", "MP_Vinewood_Tat_006_M", "Wheel of Suits"),//
            new Tattoo("mpvinewood_overlays", "MP_Vinewood_Tat_001_M", "Jackpot"),//

            new Tattoo("mpchristmas2017_overlays", "MP_Christmas2017_Tattoo_027_M", "Molon Labe"),
            new Tattoo("mpchristmas2017_overlays", "MP_Christmas2017_Tattoo_024_M", "Dragon Slayer"),
            new Tattoo("mpchristmas2017_overlays", "MP_Christmas2017_Tattoo_022_M", "Spartan and Horse"),
            new Tattoo("mpchristmas2017_overlays", "MP_Christmas2017_Tattoo_021_M", "Spartan and Lion"),
            new Tattoo("mpchristmas2017_overlays", "MP_Christmas2017_Tattoo_016_M", "Odin and Raven"),
            new Tattoo("mpchristmas2017_overlays", "MP_Christmas2017_Tattoo_015_M", "Samurai Combat"),
            new Tattoo("mpchristmas2017_overlays", "MP_Christmas2017_Tattoo_011_M", "Weathered Skull"),
            new Tattoo("mpchristmas2017_overlays", "MP_Christmas2017_Tattoo_010_M", "Spartan Shield"),
            new Tattoo("mpchristmas2017_overlays", "MP_Christmas2017_Tattoo_009_M", "Norse Rune"),
            new Tattoo("mpchristmas2017_overlays", "MP_Christmas2017_Tattoo_005_M", "Ghost Dragon"),
            new Tattoo("mpchristmas2017_overlays", "MP_Christmas2017_Tattoo_002_M", "Kabuto"),

            new Tattoo("mpsmuggler_overlays", "MP_Smuggler_Tattoo_025_M", "Claimed By The Beast"),
            new Tattoo("mpsmuggler_overlays", "MP_Smuggler_Tattoo_024_M", "Pirate Captain"),
            new Tattoo("mpsmuggler_overlays", "MP_Smuggler_Tattoo_022_M", "X Marks The Spot"),
            new Tattoo("mpsmuggler_overlays", "MP_Smuggler_Tattoo_018_M", "Finders Keepers"),
            new Tattoo("mpsmuggler_overlays", "MP_Smuggler_Tattoo_017_M", "Framed Tall Ship"),
            new Tattoo("mpsmuggler_overlays", "MP_Smuggler_Tattoo_016_M", "Skull Compass"),
            new Tattoo("mpsmuggler_overlays", "MP_Smuggler_Tattoo_013_M", "Torn Wings"),
            new Tattoo("mpsmuggler_overlays", "MP_Smuggler_Tattoo_009_M", "Tall Ship Conflict"),
            new Tattoo("mpsmuggler_overlays", "MP_Smuggler_Tattoo_006_M", "Never Surrender"),
            new Tattoo("mpsmuggler_overlays", "MP_Smuggler_Tattoo_003_M", "Give Nothing Back"),

            new Tattoo("mpairraces_overlays", "MP_Airraces_Tattoo_007_M", "Eagle Eyes"),
            new Tattoo("mpairraces_overlays", "MP_Airraces_Tattoo_005_M", "Parachute Belle"),
            new Tattoo("mpairraces_overlays", "MP_Airraces_Tattoo_004_M", "Balloon Pioneer"),
            new Tattoo("mpairraces_overlays", "MP_Airraces_Tattoo_002_M", "Winged Bombshell"),
            new Tattoo("mpairraces_overlays", "MP_Airraces_Tattoo_001_M", "Pilot Skull"),

            new Tattoo("mpgunrunning_overlays", "MP_Gunrunning_Tattoo_022_M", "Explosive Heart"),//
            new Tattoo("mpgunrunning_overlays", "MP_Gunrunning_Tattoo_019_M", "Pistol Wings"),//
            new Tattoo("mpgunrunning_overlays", "MP_Gunrunning_Tattoo_018_M", "Dual Wield Skull"),//
            new Tattoo("mpgunrunning_overlays", "MP_Gunrunning_Tattoo_014_M", "Backstabber"),//
            new Tattoo("mpgunrunning_overlays", "MP_Gunrunning_Tattoo_013_M", "Wolf Insignia"),//
            new Tattoo("mpgunrunning_overlays", "MP_Gunrunning_Tattoo_009_M", "Butterfly Knife"),//
            new Tattoo("mpgunrunning_overlays", "MP_Gunrunning_Tattoo_001_M", "Crossed Weapons"),//
            new Tattoo("mpgunrunning_overlays", "MP_Gunrunning_Tattoo_000_M", "Bullet Proof"),//

            new Tattoo("mpimportexport_overlays", "MP_MP_ImportExport_Tat_011_M", "Talk Shit Get Hit"),//
            new Tattoo("mpimportexport_overlays", "MP_MP_ImportExport_Tat_010_M", "Take the Wheel"),//
            new Tattoo("mpimportexport_overlays", "MP_MP_ImportExport_Tat_009_M", "Serpents of Destruction"),//
            new Tattoo("mpimportexport_overlays", "MP_MP_ImportExport_Tat_002_M", "Tuned to Death"),//
            new Tattoo("mpimportexport_overlays", "MP_MP_ImportExport_Tat_001_M", "Power Plant"),//
            new Tattoo("mpimportexport_overlays", "MP_MP_ImportExport_Tat_000_M", "Block Back"),//

            new Tattoo("mpbiker_overlays", "MP_MP_Biker_Tat_043_M", "Ride Forever"),//
            new Tattoo("mpbiker_overlays", "MP_MP_Biker_Tat_030_M", "Brothers For Life"),//
            new Tattoo("mpbiker_overlays", "MP_MP_Biker_Tat_021_M", "Flaming Reaper"),//
            new Tattoo("mpbiker_overlays", "MP_MP_Biker_Tat_017_M", "Clawed Beast"),//
            new Tattoo("mpbiker_overlays", "MP_MP_Biker_Tat_011_M", "R.I.P. My Brothers"),//
            new Tattoo("mpbiker_overlays", "MP_MP_Biker_Tat_008_M", "Freedom Wheels"),//
            new Tattoo("mpbiker_overlays", "MP_MP_Biker_Tat_006_M", "Chopper Freedom"),//

            new Tattoo("mpstunt_overlays", "MP_MP_Stunt_tat_048_M", "Racing Doll"),//
            new Tattoo("mpstunt_overlays", "MP_MP_Stunt_tat_046_M", "Full Throttle"),//
            new Tattoo("mpstunt_overlays", "MP_MP_Stunt_tat_041_M", "Brapp"),//
            new Tattoo("mpstunt_overlays", "MP_MP_Stunt_tat_040_M", "Monkey Chopper"),//
            new Tattoo("mpstunt_overlays", "MP_MP_Stunt_tat_037_M", "Big Grills"),//
            new Tattoo("mpstunt_overlays", "MP_MP_Stunt_tat_034_M", "Feather Road Kill"),//
            new Tattoo("mpstunt_overlays", "MP_MP_Stunt_tat_030_M", "Man's Ruin"),//
            new Tattoo("mpstunt_overlays", "MP_MP_Stunt_tat_029_M", "Majestic Finish"),//
            new Tattoo("mpstunt_overlays", "MP_MP_Stunt_tat_026_M", "Winged Wheel"),//
            new Tattoo("mpstunt_overlays", "MP_MP_Stunt_tat_024_M", "Road Kill"),//

            new Tattoo("mplowrider2_overlays", "MP_LR_Tat_032_M", "Reign Over"),//
            new Tattoo("mplowrider2_overlays", "MP_LR_Tat_031_M", "Dead Pretty"),//
            new Tattoo("mplowrider2_overlays", "MP_LR_Tat_008_M", "Love the Game"),//
            new Tattoo("mplowrider2_overlays", "MP_LR_Tat_000_M", "SA Assault"),//

            new Tattoo("mplowrider_overlays", "MP_LR_Tat_021_M", "Sad Angel"),//
            new Tattoo("mplowrider_overlays", "MP_LR_Tat_014_M", "Love is Blind"),//
            new Tattoo("mplowrider_overlays", "MP_LR_Tat_010_M", "Bad Angel"),//
            new Tattoo("mplowrider_overlays", "MP_LR_Tat_009_M", "Amazon"),//

            new Tattoo("mpluxe2_overlays", "MP_Luxe_Tat_029_M", "Geometric Design"),//   
            new Tattoo("mpluxe2_overlays", "MP_Luxe_Tat_022_M", "Cloaked Angel"),//  
            new Tattoo("mpluxe_overlays", "MP_Luxe_Tat_024_M", "Feather Mural"),//    
            new Tattoo("mpluxe_overlays", "MP_Luxe_Tat_006_M", "Adorned Wolf"),//   

            new Tattoo("mpchristmas2_overlays", "MP_Xmas2_M_Tat_015", "Japanese Warrior"),//  
            new Tattoo("mpchristmas2_overlays", "MP_Xmas2_M_Tat_011", "Roaring Tiger"),//      
            new Tattoo("mpchristmas2_overlays", "MP_Xmas2_M_Tat_006", "Carp Shaded"),//   
            new Tattoo("mpchristmas2_overlays", "MP_Xmas2_M_Tat_005", "Carp Outline"),//   

            new Tattoo("mphipster_overlays", "FM_Hip_M_Tat_046", "Triangles"),// 
            new Tattoo("mphipster_overlays", "FM_Hip_M_Tat_041", "Tooth"),// 
            new Tattoo("mphipster_overlays", "FM_Hip_M_Tat_032", "Paper Plane"),// 
            new Tattoo("mphipster_overlays", "FM_Hip_M_Tat_031", "Skateboard"),//   
            new Tattoo("mphipster_overlays", "FM_Hip_M_Tat_030", "Shark Fin"),//
            new Tattoo("mphipster_overlays", "FM_Hip_M_Tat_025", "Watch Your Step"),//  
            new Tattoo("mphipster_overlays", "FM_Hip_M_Tat_024", "Pyamid"),//   
            new Tattoo("mphipster_overlays", "FM_Hip_M_Tat_012", "Antlers"),//  
            new Tattoo("mphipster_overlays", "FM_Hip_M_Tat_011", "Infinity"),
            new Tattoo("mphipster_overlays", "FM_Hip_M_Tat_000", "Crossed Arrows"),

            new Tattoo("mpbusiness_overlays", "MP_Buis_M_Back_000", "Makin' Paper"),

            new Tattoo("mpbeach_overlays", "MP_Bea_M_Back_000", "Ship Arms"),

            new Tattoo("multiplayer_overlays", "FM_Tat_M_045", "Skulls and Rose"),//
            new Tattoo("multiplayer_overlays", "FM_Tat_M_030", "Lucky Celtic Dogs"),//
            new Tattoo("multiplayer_overlays", "FM_Tat_M_020", "Dragon"),//
            new Tattoo("multiplayer_overlays", "FM_Tat_M_019", "The Wages of Sin"),//Survival Award
            new Tattoo("multiplayer_overlays", "FM_Tat_M_016", "Evil Clown"),//
            new Tattoo("multiplayer_overlays", "FM_Tat_M_013", "Eagle and Serpent"),//
            new Tattoo("multiplayer_overlays", "FM_Tat_M_011", "LS Script"),//
            new Tattoo("multiplayer_overlays", "FM_Tat_M_009", "Skull on the Cross"),//

            new Tattoo("multiplayer_overlays", "FM_Tat_Award_M_019", "Clown Dual Wield Dollars"),//
            new Tattoo("multiplayer_overlays", "FM_Tat_Award_M_018", "Clown Dual Wield"),//
            new Tattoo("multiplayer_overlays", "FM_Tat_Award_M_017", "Clown and Gun"),//
            new Tattoo("multiplayer_overlays", "FM_Tat_Award_M_016", "Clown"),//
            new Tattoo("multiplayer_overlays", "FM_Tat_Award_M_014", "Trust No One"),//Car Bomb Award
            new Tattoo("multiplayer_overlays", "FM_Tat_Award_M_008", "Los Santos Customs"),//Mod a Car Award
            new Tattoo("multiplayer_overlays", "FM_Tat_Award_M_005", "Angel"),//Win Every Game Mode Award
          //Unknowen
            new Tattoo("multiplayer_overlays", "FM_Tat_M_046", "Zip?"),//Zip???
            new Tattoo("mpchristmas2018_overlays", "MP_Christmas2018_Tat_000_M", "Unknowen")//
        };
        private static readonly List<Tattoo> maleTats02 = new List<Tattoo>
        {
            new Tattoo("mpsum2_overlays", "MP_Sum2_Tat_003_M", "Bullet Mouth"),
            new Tattoo("mpsum2_overlays", "MP_Sum2_Tat_004_M", "Smoking Barrel"),
            new Tattoo("mpsum2_overlays", "MP_Sum2_Tat_040_M", "Carved Pumpkin"),
            new Tattoo("mpsum2_overlays", "MP_Sum2_Tat_041_M", "Branched Werewolf"),
            new Tattoo("mpsum2_overlays", "MP_Sum2_Tat_042_M", "Winged Skull"),
            new Tattoo("mpsum2_overlays", "MP_Sum2_Tat_058_M", "Shrieking Dragon"),
            new Tattoo("mpsum2_overlays", "MP_Sum2_Tat_059_M", "Swords & City"),

            new Tattoo("mpsecurity_overlays", "MP_Security_Tat_016_M", "All From The Same Tree"),
            new Tattoo("mpsecurity_overlays", "MP_Security_Tat_017_M", "King of the Jungle"),
            new Tattoo("mpsecurity_overlays", "MP_Security_Tat_018_M", "Night Owl"),

            new Tattoo("mpheist4_overlays", "MP_Heist4_Tat_023_M", "Techno Glitch"),
            new Tattoo("mpheist4_overlays", "MP_Heist4_Tat_022_M", "Paradise Sirens"),

            new Tattoo("mpheist3_overlays", "mpHeist3_Tat_035_M", "LS Panic"),
            new Tattoo("mpheist3_overlays", "mpHeist3_Tat_033_M", "LS City"),
            new Tattoo("mpheist3_overlays", "mpHeist3_Tat_026_M", "Dignity"),
            new Tattoo("mpheist3_overlays", "mpHeist3_Tat_025_M", "Davis"),

            new Tattoo("mpvinewood_overlays", "mp_vinewood_tat_022_M", "Blood Money"),
            new Tattoo("mpvinewood_overlays", "mp_vinewood_tat_003_M", "Royal Flush"),
            new Tattoo("mpvinewood_overlays", "mp_vinewood_tat_000_M", "In the Pocket"),

            new Tattoo("mpchristmas2017_overlays", "MP_Christmas2017_Tattoo_026_M", "Spartan Skull"),
            new Tattoo("mpchristmas2017_overlays", "MP_Christmas2017_Tattoo_020_M", "Medusa's Gaze"),
            new Tattoo("mpchristmas2017_overlays", "MP_Christmas2017_Tattoo_019_M", "Strike Force"),
            new Tattoo("mpchristmas2017_overlays", "MP_Christmas2017_Tattoo_003_M", "Native Warrior"),
            new Tattoo("mpchristmas2017_overlays", "MP_Christmas2017_Tattoo_000_M", "Thor - Goblin"),

            new Tattoo("mpsmuggler_overlays", "MP_Smuggler_Tattoo_021_M", "Dead Tales"),
            new Tattoo("mpsmuggler_overlays", "MP_Smuggler_Tattoo_019_M", "Lost At Sea"),
            new Tattoo("mpsmuggler_overlays", "MP_Smuggler_Tattoo_007_M", "No Honor"),
            new Tattoo("mpsmuggler_overlays", "MP_Smuggler_Tattoo_000_M", "Bless The Dead"),

            new Tattoo("mpairraces_overlays", "MP_Airraces_Tattoo_000_M", "Turbulence"),

            new Tattoo("mpgunrunning_overlays", "MP_Gunrunning_Tattoo_028_M", "Micro SMG Chain"),
            new Tattoo("mpgunrunning_overlays", "MP_Gunrunning_Tattoo_020_M", "Crowned Weapons"),
            new Tattoo("mpgunrunning_overlays", "MP_Gunrunning_Tattoo_017_M", "Dog Tags"),
            new Tattoo("mpgunrunning_overlays", "MP_Gunrunning_Tattoo_012_M", "Dollar Daggers"),

            new Tattoo("mpbiker_overlays", "MP_MP_Biker_Tat_060_M", "We Are The Mods!"),
            new Tattoo("mpbiker_overlays", "MP_MP_Biker_Tat_059_M", "Faggio"),
            new Tattoo("mpbiker_overlays", "MP_MP_Biker_Tat_058_M", "Reaper Vulture"),
            new Tattoo("mpbiker_overlays", "MP_MP_Biker_Tat_050_M", "Unforgiven"),
            new Tattoo("mpbiker_overlays", "MP_MP_Biker_Tat_041_M", "No Regrets"),
            new Tattoo("mpbiker_overlays", "MP_MP_Biker_Tat_034_M", "Brotherhood of Bikes"),
            new Tattoo("mpbiker_overlays", "MP_MP_Biker_Tat_032_M", "Western Eagle"),
            new Tattoo("mpbiker_overlays", "MP_MP_Biker_Tat_029_M", "Bone Wrench"),
            new Tattoo("mpbiker_overlays", "MP_MP_Biker_Tat_026_M", "American Dream"),
            new Tattoo("mpbiker_overlays", "MP_MP_Biker_Tat_023_M", "Western MC"),
            new Tattoo("mpbiker_overlays", "MP_MP_Biker_Tat_019_M", "Gruesome Talons"),
            new Tattoo("mpbiker_overlays", "MP_MP_Biker_Tat_018_M", "Skeletal Chopper"),
            new Tattoo("mpbiker_overlays", "MP_MP_Biker_Tat_013_M", "Demon Crossbones"),
            new Tattoo("mpbiker_overlays", "MP_MP_Biker_Tat_005_M", "Made In America"),
            new Tattoo("mpbiker_overlays", "MP_MP_Biker_Tat_001_M", "Both Barrels"),
            new Tattoo("mpbiker_overlays", "MP_MP_Biker_Tat_000_M", "Demon Rider"),

            new Tattoo("mpstunt_overlays", "MP_MP_Stunt_tat_044_M", "Ram Skull"),
            new Tattoo("mpstunt_overlays", "MP_MP_Stunt_tat_033_M", "Sugar Skull Trucker"),
            new Tattoo("mpstunt_overlays", "MP_MP_Stunt_tat_027_M", "Punk Road Hog"),
            new Tattoo("mpstunt_overlays", "MP_MP_Stunt_tat_019_M", "Engine Heart"),
            new Tattoo("mpstunt_overlays", "MP_MP_Stunt_tat_018_M", "Vintage Bully"),
            new Tattoo("mpstunt_overlays", "MP_MP_Stunt_tat_011_M", "Wheels of Death"),

            new Tattoo("mplowrider2_overlays", "MP_LR_Tat_019_M", "Death Behind"),
            new Tattoo("mplowrider2_overlays", "MP_LR_Tat_012_M", "Royal Kiss"),

            new Tattoo("mplowrider_overlays", "MP_LR_Tat_026_M", "Royal Takeover"),
            new Tattoo("mplowrider_overlays", "MP_LR_Tat_013_M", "Love Gamble"),
            new Tattoo("mplowrider_overlays", "MP_LR_Tat_002_M", "Holy Mary"),
            new Tattoo("mplowrider_overlays", "MP_LR_Tat_001_M", "King Fight"),

            new Tattoo("mpluxe2_overlays", "MP_Luxe_Tat_027_M", "Cobra Dawn"),
            new Tattoo("mpluxe2_overlays", "MP_Luxe_Tat_025_M", "Reaper Sway"),
            new Tattoo("mpluxe2_overlays", "MP_Luxe_Tat_012_M", "Geometric Galaxy"),
            new Tattoo("mpluxe2_overlays", "MP_Luxe_Tat_002_M", "The Howler"),

            new Tattoo("mpluxe_overlays", "MP_Luxe_Tat_015_M", "Smoking Sisters"),
            new Tattoo("mpluxe_overlays", "MP_Luxe_Tat_014_M", "Ancient Queen"),
            new Tattoo("mpluxe_overlays", "MP_Luxe_Tat_008_M", "Flying Eye"),
            new Tattoo("mpluxe_overlays", "MP_Luxe_Tat_007_M", "Eye of the Griffin"),

            new Tattoo("mpchristmas2_overlays", "MP_Xmas2_M_Tat_019", "Royal Dagger Color"),
            new Tattoo("mpchristmas2_overlays", "MP_Xmas2_M_Tat_018", "Royal Dagger Outline"),
            new Tattoo("mpchristmas2_overlays", "MP_Xmas2_M_Tat_017", "Loose Lips Color"),
            new Tattoo("mpchristmas2_overlays", "MP_Xmas2_M_Tat_016", "Loose Lips Outline"),
            new Tattoo("mpchristmas2_overlays", "MP_Xmas2_M_Tat_009", "Time To Die"),

            new Tattoo("mphipster_overlays", "FM_Hip_M_Tat_047", "Cassette"),
            new Tattoo("mphipster_overlays", "FM_Hip_M_Tat_033", "Stag"),
            new Tattoo("mphipster_overlays", "FM_Hip_M_Tat_013", "Boombox"),
            new Tattoo("mphipster_overlays", "FM_Hip_M_Tat_002", "Chemistry"),

            new Tattoo("mpbusiness_overlays", "MP_Buis_M_Chest_001", "$$$"),
            new Tattoo("mpbusiness_overlays", "MP_Buis_M_Chest_000", "Rich"),
            new Tattoo("mpbeach_overlays", "MP_Bea_M_Chest_001", "Tribal Shark"),
            new Tattoo("mpbeach_overlays", "MP_Bea_M_Chest_000", "Tribal Hammerhead"),

            new Tattoo("multiplayer_overlays", "FM_Tat_M_044", "Stone Cross"),
            new Tattoo("multiplayer_overlays", "FM_Tat_M_034", "Flaming Shamrock"),
            new Tattoo("multiplayer_overlays", "FM_Tat_M_025", "LS Bold"),
            new Tattoo("multiplayer_overlays", "FM_Tat_M_024", "Flaming Cross"),
            new Tattoo("multiplayer_overlays", "FM_Tat_M_010", "LS Flames"),

            new Tattoo("multiplayer_overlays", "FM_Tat_Award_M_013", "Seven Deadly Sins"),//Kill 1000 Players Award
            new Tattoo("multiplayer_overlays", "FM_Tat_Award_M_012", "Embellished Scroll"),//Kill 500 Players Award
            new Tattoo("multiplayer_overlays", "FM_Tat_Award_M_011", "Blank Scroll"),////Kill 100 Players Award?
            new Tattoo("multiplayer_overlays", "FM_Tat_Award_M_003", "Blackjack")
        };
        private static readonly List<Tattoo> maleTats03 = new List<Tattoo>
        {
            new Tattoo("mpsum2_overlays", "MP_Sum2_Tat_005_M", "Concealed"),
            new Tattoo("mpsum2_overlays", "MP_Sum2_Tat_043_M", "Cursed Saki"),
            new Tattoo("mpsum2_overlays", "MP_Sum2_Tat_044_M", "Smouldering Bat & Skull"),
            new Tattoo("mpsum2_overlays", "MP_Sum2_Tat_060_M", "Blaine County"),
            new Tattoo("mpsum2_overlays", "MP_Sum2_Tat_061_M", "Angry Possum"),
            new Tattoo("mpsum2_overlays", "MP_Sum2_Tat_062_M", "Floral Demon"),

            new Tattoo("mpsecurity_overlays", "MP_Security_Tat_024_M", "Beatbox Silhouette"),
            new Tattoo("mpsecurity_overlays", "MP_Security_Tat_025_M", "Davis Flames"),

            new Tattoo("mpheist4_overlays", "MP_Heist4_Tat_030_M", "Radio Tape"),
            new Tattoo("mpheist4_overlays", "MP_Heist4_Tat_004_M", "Skeleton Breeze"),

            new Tattoo("mpheist3_overlays", "mpHeist3_Tat_037_M", "LadyBug"),
            new Tattoo("mpheist3_overlays", "mpHeist3_Tat_029_M", "Fatal Incursion"),

            new Tattoo("mpvinewood_overlays", "mp_vinewood_tat_031_M", "Gambling Royalty"),
            new Tattoo("mpvinewood_overlays", "mp_vinewood_tat_024_M", "Cash Mouth"),
            new Tattoo("mpvinewood_overlays", "mp_vinewood_tat_016_M", "Rose and Aces"),
            new Tattoo("mpvinewood_overlays", "mp_vinewood_tat_012_M", "Skull of Suits"),

            new Tattoo("mpchristmas2017_overlays", "MP_Christmas2017_Tattoo_008_M", "Spartan Warrior"),

            new Tattoo("mpsmuggler_overlays", "MP_Smuggler_Tattoo_015_M", "Jolly Roger"),
            new Tattoo("mpsmuggler_overlays", "MP_Smuggler_Tattoo_010_M", "See You In Hell"),
            new Tattoo("mpsmuggler_overlays", "MP_Smuggler_Tattoo_002_M", "Dead Lies"),

            new Tattoo("mpairraces_overlays", "MP_Airraces_Tattoo_006_M", "Bombs Away"),

            new Tattoo("mpgunrunning_overlays", "MP_Gunrunning_Tattoo_029_M", "Win Some Lose Some"),
            new Tattoo("mpgunrunning_overlays", "MP_Gunrunning_Tattoo_010_M", "Cash Money"),

            new Tattoo("mpbiker_overlays", "MP_MP_Biker_Tat_052_M", "Biker Mount"),
            new Tattoo("mpbiker_overlays", "MP_MP_Biker_Tat_039_M", "Gas Guzzler"),
            new Tattoo("mpbiker_overlays", "MP_MP_Biker_Tat_031_M", "Gear Head"),
            new Tattoo("mpbiker_overlays", "MP_MP_Biker_Tat_010_M", "Skull Of Taurus"),
            new Tattoo("mpbiker_overlays", "MP_MP_Biker_Tat_003_M", "Web Rider"),

            new Tattoo("mpstunt_overlays", "MP_MP_Stunt_tat_014_M", "Bat Cat of Spades"),
            new Tattoo("mpstunt_overlays", "MP_MP_Stunt_tat_012_M", "Punk Biker"),

            new Tattoo("mplowrider2_overlays", "MP_LR_Tat_016_M", "Two Face"),
            new Tattoo("mplowrider2_overlays", "MP_LR_Tat_011_M", "Lady Liberty"),

            new Tattoo("mplowrider_overlays", "MP_LR_Tat_004_M", "Gun Mic"),

            new Tattoo("mpluxe_overlays", "MP_Luxe_Tat_003_M", "Abstract Skull"),

            new Tattoo("mpchristmas2_overlays", "MP_Xmas2_M_Tat_028", "Executioner"),
            new Tattoo("mpchristmas2_overlays", "MP_Xmas2_M_Tat_013", "Lizard"),

            new Tattoo("mphipster_overlays", "FM_Hip_M_Tat_035", "Sewn Heart"),
            new Tattoo("mphipster_overlays", "FM_Hip_M_Tat_029", "Sad"),
            new Tattoo("mphipster_overlays", "FM_Hip_M_Tat_006", "Feather Birds"),

            new Tattoo("mpbusiness_overlays", "MP_Buis_M_Stomach_000", "Refined Hustler"),

            new Tattoo("mpbeach_overlays", "MP_Bea_M_Stom_001", "Wheel"),
            new Tattoo("mpbeach_overlays", "MP_Bea_M_Stom_000", "Swordfish"),


            new Tattoo("multiplayer_overlays", "FM_Tat_M_036", "Way of the Gun"),//500 Pistol kills Award
            new Tattoo("multiplayer_overlays", "FM_Tat_M_029", "Trinity Knot"),
            new Tattoo("multiplayer_overlays", "FM_Tat_M_012", "Los Santos Bills"),
            new Tattoo("multiplayer_overlays", "FM_Tat_M_004", "Faith"),

            new Tattoo("multiplayer_overlays", "FM_Tat_Award_M_004", "Hustler"),//Make 50000 from betting Award
        };
        private static readonly List<Tattoo> maleTats04 = new List<Tattoo>
        {
            new Tattoo("mpsum2_overlays", "MP_Sum2_Tat_000_M", "Live Fast Mono"),
            new Tattoo("mpsum2_overlays", "MP_Sum2_Tat_001_M", "Live Fast Color"),
            new Tattoo("mpsum2_overlays", "MP_Sum2_Tat_018_M", "Branched Skull"),
            new Tattoo("mpsum2_overlays", "MP_Sum2_Tat_019_M", "Scythed Corpse"),
            new Tattoo("mpsum2_overlays", "MP_Sum2_Tat_020_M", "Scythed Corpse & Reaper"),
            new Tattoo("mpsum2_overlays", "MP_Sum2_Tat_021_M", "Third Eye"),
            new Tattoo("mpsum2_overlays", "MP_Sum2_Tat_022_M", "Pierced Third Eye"),
            new Tattoo("mpsum2_overlays", "MP_Sum2_Tat_023_M", "Lip Drip"),
            new Tattoo("mpsum2_overlays", "MP_Sum2_Tat_024_M", "Skin Mask"),
            new Tattoo("mpsum2_overlays", "MP_Sum2_Tat_025_M", "Webbed Scythe"),
            new Tattoo("mpsum2_overlays", "MP_Sum2_Tat_026_M", "Oni Demon"),
            new Tattoo("mpsum2_overlays", "MP_Sum2_Tat_027_M", "Bat Wings"),

            new Tattoo("mpsecurity_overlays", "MP_Security_Tat_001_M", "Bright Diamond"),
            new Tattoo("mpsecurity_overlays", "MP_Security_Tat_002_M", "Hustle"),
            new Tattoo("mpsecurity_overlays", "MP_Security_Tat_027_M", "Black Widow"),

            new Tattoo("mpheist3_overlays", "mpHeist3_Tat_044_M", "Clubs"),
            new Tattoo("mpheist3_overlays", "mpHeist3_Tat_043_M", "Diamonds"),
            new Tattoo("mpheist3_overlays", "mpHeist3_Tat_042_M", "Hearts"),

            new Tattoo("mpheist3_overlays", "mpHeist3_Tat_022_M", "Thong's Sword"),
            new Tattoo("mpheist3_overlays", "mpHeist3_Tat_021_M", "Wanted"),
            new Tattoo("mpheist3_overlays", "mpHeist3_Tat_020_M", "UFO"),
            new Tattoo("mpheist3_overlays", "mpHeist3_Tat_019_M", "Teddy Bear"),
            new Tattoo("mpheist3_overlays", "mpHeist3_Tat_018_M", "Stitches"),
            new Tattoo("mpheist3_overlays", "mpHeist3_Tat_017_M", "Space Monkey"),
            new Tattoo("mpheist3_overlays", "mpHeist3_Tat_016_M", "Sleepy"),
            new Tattoo("mpheist3_overlays", "mpHeist3_Tat_015_M", "On/Off"),
            new Tattoo("mpheist3_overlays", "mpHeist3_Tat_014_M", "LS Wings"),
            new Tattoo("mpheist3_overlays", "mpHeist3_Tat_013_M", "LS Star"),
            new Tattoo("mpheist3_overlays", "mpHeist3_Tat_012_M", "Razor Pop"),
            new Tattoo("mpheist3_overlays", "mpHeist3_Tat_011_M", "Lipstick Kiss"),
            new Tattoo("mpheist3_overlays", "mpHeist3_Tat_010_M", "Green Leaf"),
            new Tattoo("mpheist3_overlays", "mpHeist3_Tat_009_M", "Knifed"),
            new Tattoo("mpheist3_overlays", "mpHeist3_Tat_008_M", "Ice Cream"),
            new Tattoo("mpheist3_overlays", "mpHeist3_Tat_007_M", "Two Horns"),
            new Tattoo("mpheist3_overlays", "mpHeist3_Tat_006_M", "Crowned"),
            new Tattoo("mpheist3_overlays", "mpHeist3_Tat_005_M", "Spades"),
            new Tattoo("mpheist3_overlays", "mpHeist3_Tat_004_M", "Bandage"),
            new Tattoo("mpheist3_overlays", "mpHeist3_Tat_003_M", "Assault Rifle"),
            new Tattoo("mpheist3_overlays", "mpHeist3_Tat_002_M", "Animal"),
            new Tattoo("mpheist3_overlays", "mpHeist3_Tat_001_M", "Ace of Spades"),
            new Tattoo("mpheist3_overlays", "mpHeist3_Tat_000_M", "Five Stars"),

            new Tattoo("mpsmuggler_overlays", "MP_Smuggler_Tattoo_012_M", "Thief"),
            new Tattoo("mpsmuggler_overlays", "MP_Smuggler_Tattoo_011_M", "Sinner"),

            new Tattoo("mpgunrunning_overlays", "MP_Gunrunning_Tattoo_003_M", "Lock and Load"),

            new Tattoo("mpbiker_overlays", "MP_MP_Biker_Tat_051_M", "Western Stylized"),
            new Tattoo("mpbiker_overlays", "MP_MP_Biker_Tat_038_M", "FTW"),
            new Tattoo("mpbiker_overlays", "MP_MP_Biker_Tat_009_M", "Morbid Arachnid"),

            new Tattoo("mpstunt_overlays", "MP_MP_Stunt_tat_042_M", "Flaming Quad"),
            new Tattoo("mpstunt_overlays", "MP_MP_Stunt_tat_017_M", "Bat Wheel"),
            new Tattoo("mpstunt_overlays", "MP_MP_Stunt_Tat_006_M", "Toxic Spider"),
            new Tattoo("mpstunt_overlays", "MP_MP_Stunt_Tat_004_M", "Scorpion"),
            new Tattoo("mpstunt_overlays", "MP_MP_Stunt_Tat_000_M", "Stunt Skull"),

            new Tattoo("mpchristmas2_overlays", "MP_Xmas2_M_Tat_029", "Beautiful Death"),
            new Tattoo("mpchristmas2_overlays", "MP_Xmas2_M_Tat_025", "Snake Head Color"),
            new Tattoo("mpchristmas2_overlays", "MP_Xmas2_M_Tat_024", "Snake Head Outline"),
            new Tattoo("mpchristmas2_overlays", "MP_Xmas2_M_Tat_007", "Los Muertos"),

            new Tattoo("mphipster_overlays", "FM_Hip_M_Tat_021", "Geo Fox"),
            new Tattoo("mphipster_overlays", "FM_Hip_M_Tat_005", "Beautiful Eye"),

            new Tattoo("mpbusiness_overlays", "MP_Buis_M_Neck_003", "$100"),
            new Tattoo("mpbusiness_overlays", "MP_Buis_M_Neck_002", "Script Dollar Sign"),
            new Tattoo("mpbusiness_overlays", "MP_Buis_M_Neck_001", "Bold Dollar Sign"),
            new Tattoo("mpbusiness_overlays", "MP_Buis_M_Neck_000", "Cash is King"),

            new Tattoo("mpbeach_overlays", "MP_Bea_M_Head_002", "Shark"),

            new Tattoo("mpbeach_overlays", "MP_Bea_M_Neck_001", "Surfs Up"),
            new Tattoo("mpbeach_overlays", "MP_Bea_M_Neck_000", "Little Fish"),

            new Tattoo("mpbeach_overlays", "MP_Bea_M_Head_001", "Surf LS"),
            new Tattoo("mpbeach_overlays", "MP_Bea_M_Head_000", "Pirate Skull"),

            new Tattoo("multiplayer_overlays", "FM_Tat_Award_M_000", "Skull")
        };
        private static readonly List<Tattoo> maleTats05 = new List<Tattoo>
        {
            new Tattoo("mpsum2_overlays", "MP_Sum2_Tat_008_M", "Bigness Chimp"),
            new Tattoo("mpsum2_overlays", "MP_Sum2_Tat_009_M", "Up-n-Atomizer Design"),
            new Tattoo("mpsum2_overlays", "MP_Sum2_Tat_010_M", "Rocket Launcher Girl"),
            new Tattoo("mpsum2_overlays", "MP_Sum2_Tat_028_M", "Laser Eyes Skull"),
            new Tattoo("mpsum2_overlays", "MP_Sum2_Tat_029_M", "Classic Vampire"),
            new Tattoo("mpsum2_overlays", "MP_Sum2_Tat_049_M", "Demon Drummer"),

            new Tattoo("mpsecurity_overlays", "MP_Security_Tat_006_M", "Skeleton Shot"),
            new Tattoo("mpsecurity_overlays", "MP_Security_Tat_010_M", "Music Is The Remedy"),
            new Tattoo("mpsecurity_overlays", "MP_Security_Tat_011_M", "Serpent Mic"),
            new Tattoo("mpsecurity_overlays", "MP_Security_Tat_019_M", "Weed Knuckles"),

            new Tattoo("mpheist4_overlays", "MP_Heist4_Tat_009_M", "Scratch Panther"),

            new Tattoo("mpheist3_overlays", "mpHeist3_Tat_041_M", "Mighty Thog"),
            new Tattoo("mpheist3_overlays", "mpHeist3_Tat_040_M", "Tiger Heart"),

            new Tattoo("mpvinewood_overlays", "MP_Vinewood_Tat_026_M", "Banknote Rose"),
            new Tattoo("mpvinewood_overlays", "MP_Vinewood_Tat_019_M", "Can't Win Them All"),
            new Tattoo("mpvinewood_overlays", "MP_Vinewood_Tat_014_M", "Vice"),
            new Tattoo("mpvinewood_overlays", "MP_Vinewood_Tat_005_M", "Get Lucky"),
            new Tattoo("mpvinewood_overlays", "MP_Vinewood_Tat_002_M", "Suits"),

            new Tattoo("mpchristmas2017_overlays", "MP_Christmas2017_Tattoo_029_M", "Cerberus"),
            new Tattoo("mpchristmas2017_overlays", "MP_Christmas2017_Tattoo_025_M", "Winged Serpent"),
            new Tattoo("mpchristmas2017_overlays", "MP_Christmas2017_Tattoo_013_M", "Katana"),
            new Tattoo("mpchristmas2017_overlays", "MP_Christmas2017_Tattoo_007_M", "Spartan Combat"),
            new Tattoo("mpchristmas2017_overlays", "MP_Christmas2017_Tattoo_004_M", "Tiger and Mask"),
            new Tattoo("mpchristmas2017_overlays", "MP_Christmas2017_Tattoo_001_M", "Viking Warrior"),

            new Tattoo("mpsmuggler_overlays", "MP_Smuggler_Tattoo_014_M", "Mermaid's Curse"),
            new Tattoo("mpsmuggler_overlays", "MP_Smuggler_Tattoo_008_M", "Horrors Of The Deep"),
            new Tattoo("mpsmuggler_overlays", "MP_Smuggler_Tattoo_004_M", "Honor"),

            new Tattoo("mpairraces_overlays", "MP_Airraces_Tattoo_003_M", "Toxic Trails"),

            new Tattoo("mpgunrunning_overlays", "MP_Gunrunning_Tattoo_027_M", "Serpent Revolver"),
            new Tattoo("mpgunrunning_overlays", "MP_Gunrunning_Tattoo_025_M", "Praying Skull"),
            new Tattoo("mpgunrunning_overlays", "MP_Gunrunning_Tattoo_016_M", "Blood Money"),
            new Tattoo("mpgunrunning_overlays", "MP_Gunrunning_Tattoo_015_M", "Spiked Skull"),
            new Tattoo("mpgunrunning_overlays", "MP_Gunrunning_Tattoo_008_M", "Bandolier"),
            new Tattoo("mpgunrunning_overlays", "MP_Gunrunning_Tattoo_004_M", "Sidearm"),

            new Tattoo("mpimportexport_overlays", "MP_MP_ImportExport_Tat_008_M", "Scarlett"),
            new Tattoo("mpimportexport_overlays", "MP_MP_ImportExport_Tat_004_M", "Piston Sleeve"),

            new Tattoo("mpbiker_overlays", "MP_MP_Biker_Tat_055_M", "Poison Scorpion"),
            new Tattoo("mpbiker_overlays", "MP_MP_Biker_Tat_053_M", "Muffler Helmet"),
            new Tattoo("mpbiker_overlays", "MP_MP_Biker_Tat_045_M", "Ride Hard Die Fast"),
            new Tattoo("mpbiker_overlays", "MP_MP_Biker_Tat_035_M", "Chain Fist"),
            new Tattoo("mpbiker_overlays", "MP_MP_Biker_Tat_025_M", "Good Luck"),
            new Tattoo("mpbiker_overlays", "MP_MP_Biker_Tat_024_M", "Live to Ride"),
            new Tattoo("mpbiker_overlays", "MP_MP_Biker_Tat_020_M", "Cranial Rose"),
            new Tattoo("mpbiker_overlays", "MP_MP_Biker_Tat_016_M", "Macabre Tree"),
            new Tattoo("mpbiker_overlays", "MP_MP_Biker_Tat_012_M", "Urban Stunter"),

            new Tattoo("mpstunt_overlays", "MP_MP_Stunt_tat_043_M", "Engine Arm"),
            new Tattoo("mpstunt_overlays", "MP_MP_Stunt_tat_039_M", "Kaboom"),
            new Tattoo("mpstunt_overlays", "MP_MP_Stunt_tat_035_M", "Stuntman's End"),
            new Tattoo("mpstunt_overlays", "MP_MP_Stunt_tat_023_M", "Tanked"),
            new Tattoo("mpstunt_overlays", "MP_MP_Stunt_tat_022_M", "Piston Head"),
            new Tattoo("mpstunt_overlays", "MP_MP_Stunt_tat_008_M", "Moonlight Ride"),
            new Tattoo("mpstunt_overlays", "MP_MP_Stunt_tat_002_M", "Big Cat"),
            new Tattoo("mpstunt_overlays", "MP_MP_Stunt_tat_001_M", "8 Eyed Skull"),

            new Tattoo("mplowrider2_overlays", "MP_LR_Tat_022_M", "My Crazy Life"),
            new Tattoo("mplowrider2_overlays", "MP_LR_Tat_018_M", "Skeleton Party"),
            new Tattoo("mplowrider2_overlays", "MP_LR_Tat_006_M", "Love Hustle"),

            new Tattoo("mplowrider_overlays", "MP_LR_Tat_033_M", "City Sorrow"),//
            new Tattoo("mplowrider_overlays", "MP_LR_Tat_027_M", "Los Santos Life"),//
            new Tattoo("mplowrider_overlays", "MP_LR_Tat_005_M", "No Evil"),//

            new Tattoo("mpluxe2_overlays", "MP_Luxe_Tat_028_M", "Python Skull"),
            new Tattoo("mpluxe2_overlays", "MP_Luxe_Tat_018_M", "Divine Goddess"),
            new Tattoo("mpluxe2_overlays", "MP_Luxe_Tat_016_M", "Egyptian Mural"),
            new Tattoo("mpluxe2_overlays", "MP_Luxe_Tat_005_M", "Fatal Dagger"),


            new Tattoo("mpluxe_overlays", "MP_Luxe_Tat_021_M", "Gabriel"),
            new Tattoo("mpluxe_overlays", "MP_Luxe_Tat_020_M", "Archangel and Mary"),
            new Tattoo("mpluxe_overlays", "MP_Luxe_Tat_009_M", "Floral Symmetry"),

            new Tattoo("mpchristmas2_overlays", "MP_Xmas2_M_Tat_021", "Time's Up Color"),
            new Tattoo("mpchristmas2_overlays", "MP_Xmas2_M_Tat_020", "Time's Up Outline"),
            new Tattoo("mpchristmas2_overlays", "MP_Xmas2_M_Tat_012", "8 Ball Skull"),
            new Tattoo("mpchristmas2_overlays", "MP_Xmas2_M_Tat_010", "Electric Snake"),
            new Tattoo("mpchristmas2_overlays", "MP_Xmas2_M_Tat_000", "Skull Rider"),

            new Tattoo("mphipster_overlays", "FM_Hip_M_Tat_048", "Peace"),
            new Tattoo("mphipster_overlays", "FM_Hip_M_Tat_043", "Triangle White"),
            new Tattoo("mphipster_overlays", "FM_Hip_M_Tat_039", "Sleeve"),
            new Tattoo("mphipster_overlays", "FM_Hip_M_Tat_037", "Sunrise"),
            new Tattoo("mphipster_overlays", "FM_Hip_M_Tat_034", "Stop"),
            new Tattoo("mphipster_overlays", "FM_Hip_M_Tat_028", "Thorny Rose"),
            new Tattoo("mphipster_overlays", "FM_Hip_M_Tat_027", "Padlock"),
            new Tattoo("mphipster_overlays", "FM_Hip_M_Tat_026", "Pizza"),
            new Tattoo("mphipster_overlays", "FM_Hip_M_Tat_016", "Lightning Bolt"),
            new Tattoo("mphipster_overlays", "FM_Hip_M_Tat_015", "Mustache"),
            new Tattoo("mphipster_overlays", "FM_Hip_M_Tat_007", "Bricks"),
            new Tattoo("mphipster_overlays", "FM_Hip_M_Tat_003", "Diamond Sparkle"),

            new Tattoo("mpbusiness_overlays", "MP_Buis_M_LeftArm_001", "All-Seeing Eye"),
            new Tattoo("mpbusiness_overlays", "MP_Buis_M_LeftArm_000", "$100 Bill"),

            new Tattoo("mpbeach_overlays", "MP_Bea_M_LArm_000", "Tiki Tower"),
            new Tattoo("mpbeach_overlays", "MP_Bea_M_LArm_001", "Mermaid L.S."),

            new Tattoo("multiplayer_overlays", "FM_Tat_M_041", "Dope Skull"),
            new Tattoo("multiplayer_overlays", "FM_Tat_M_031", "Lady M"),
            new Tattoo("multiplayer_overlays", "FM_Tat_M_015", "Zodiac Skull"),
            new Tattoo("multiplayer_overlays", "FM_Tat_M_006", "Oriental Mural"),
            new Tattoo("multiplayer_overlays", "FM_Tat_M_005", "Serpents"),

            new Tattoo("multiplayer_overlays", "FM_Tat_Award_M_015", "Racing Brunette"),
            new Tattoo("multiplayer_overlays", "FM_Tat_Award_M_007", "Racing Blonde"),
            new Tattoo("multiplayer_overlays", "FM_Tat_Award_M_001", "Burning Heart"),//50 Death Match Award
                                                                                                                                          //not on list
            new Tattoo("mpluxe2_overlays", "MP_Luxe_Tat_031_M", "Geometric Design")
        };
        private static readonly List<Tattoo> maleTats06 = new List<Tattoo>
        {
            new Tattoo("mpsum2_overlays", "MP_Sum2_Tat_011_M", "Nothing Mini About It"),
            new Tattoo("mpsum2_overlays", "MP_Sum2_Tat_012_M", "Snake Revolver"),
            new Tattoo("mpsum2_overlays", "MP_Sum2_Tat_013_M", "Weapon Sleeve"),
            new Tattoo("mpsum2_overlays", "MP_Sum2_Tat_030_M", "Centipede"),
            new Tattoo("mpsum2_overlays", "MP_Sum2_Tat_031_M", "Fleshy Eye"),
            new Tattoo("mpsum2_overlays", "MP_Sum2_Tat_045_M", "Armored Arm"),
            new Tattoo("mpsum2_overlays", "MP_Sum2_Tat_046_M", "Demon Smile"),
            new Tattoo("mpsum2_overlays", "MP_Sum2_Tat_047_M", "Angel & Devil"),
            new Tattoo("mpsum2_overlays", "MP_Sum2_Tat_048_M", "Death Is Certain"),

            new Tattoo("mpsecurity_overlays", "MP_Security_Tat_000_M", "Hood Skeleton"),
            new Tattoo("mpsecurity_overlays", "MP_Security_Tat_005_M", "Peacock"),
            new Tattoo("mpsecurity_overlays", "MP_Security_Tat_007_M", "Ballas 4 Life"),
            new Tattoo("mpsecurity_overlays", "MP_Security_Tat_009_M", "Ascension"),
            new Tattoo("mpsecurity_overlays", "MP_Security_Tat_012_M", "Zombie Rhymes"),
            new Tattoo("mpsecurity_overlays", "MP_Security_Tat_020_M", "Dog Fist"),

            new Tattoo("mpheist4_overlays", "MP_Heist4_Tat_032_M", "K.U.L.T.99.1 FM"),
            new Tattoo("mpheist4_overlays", "MP_Heist4_Tat_031_M", "Octopus Shades"),
            new Tattoo("mpheist4_overlays", "MP_Heist4_Tat_026_M", "Shark Water"),
            new Tattoo("mpheist4_overlays", "MP_Heist4_Tat_012_M", "Still Slipping"),
            new Tattoo("mpheist4_overlays", "MP_Heist4_Tat_011_M", "Soulwax"),
            new Tattoo("mpheist4_overlays", "MP_Heist4_Tat_008_M", "Smiley Glitch"),
            new Tattoo("mpheist4_overlays", "MP_Heist4_Tat_007_M", "Skeleton DJ"),
            new Tattoo("mpheist4_overlays", "MP_Heist4_Tat_006_M", "Music Locker"),
            new Tattoo("mpheist4_overlays", "MP_Heist4_Tat_005_M", "LSUR"),
            new Tattoo("mpheist4_overlays", "MP_Heist4_Tat_003_M", "Lighthouse"),
            new Tattoo("mpheist4_overlays", "MP_Heist4_Tat_002_M", "Jellyfish Shades"),
            new Tattoo("mpheist4_overlays", "MP_Heist4_Tat_001_M", "Tropical Dude"),
            new Tattoo("mpheist4_overlays", "MP_Heist4_Tat_000_M", "Headphone Splat"),

            new Tattoo("mpheist3_overlays", "mpHeist3_Tat_034_M", "LS Monogram"),

            new Tattoo("mpvinewood_overlays", "MP_Vinewood_Tat_028_M", "Skull and Aces"),
            new Tattoo("mpvinewood_overlays", "MP_Vinewood_Tat_025_M", "Queen of Roses"),
            new Tattoo("mpvinewood_overlays", "MP_Vinewood_Tat_018_M", "The Gambler's Life"),
            new Tattoo("mpvinewood_overlays", "MP_Vinewood_Tat_004_M", "Lady Luck"),

            new Tattoo("mpchristmas2017_overlays", "MP_Christmas2017_Tattoo_028_M", "Spartan Mural"),
            new Tattoo("mpchristmas2017_overlays", "MP_Christmas2017_Tattoo_023_M", "Samurai Tallship"),
            new Tattoo("mpchristmas2017_overlays", "MP_Christmas2017_Tattoo_018_M", "Muscle Tear"),
            new Tattoo("mpchristmas2017_overlays", "MP_Christmas2017_Tattoo_017_M", "Feather Sleeve"),
            new Tattoo("mpchristmas2017_overlays", "MP_Christmas2017_Tattoo_014_M", "Celtic Band"),
            new Tattoo("mpchristmas2017_overlays", "MP_Christmas2017_Tattoo_012_M", "Tiger Headdress"),
            new Tattoo("mpchristmas2017_overlays", "MP_Christmas2017_Tattoo_006_M", "Medusa"),

            new Tattoo("mpsmuggler_overlays", "MP_Smuggler_Tattoo_023_M", "Stylized Kraken"),
            new Tattoo("mpsmuggler_overlays", "MP_Smuggler_Tattoo_005_M", "Mutiny"),
            new Tattoo("mpsmuggler_overlays", "MP_Smuggler_Tattoo_001_M", "Crackshot"),

            new Tattoo("mpgunrunning_overlays", "MP_Gunrunning_Tattoo_024_M", "Combat Reaper"),
            new Tattoo("mpgunrunning_overlays", "MP_Gunrunning_Tattoo_021_M", "Have a Nice Day"),
            new Tattoo("mpgunrunning_overlays", "MP_Gunrunning_Tattoo_002_M", "Grenade"),

            new Tattoo("mpimportexport_overlays", "MP_MP_ImportExport_Tat_007_M", "Drive Forever"),
            new Tattoo("mpimportexport_overlays", "MP_MP_ImportExport_Tat_006_M", "Engulfed Block"),
            new Tattoo("mpimportexport_overlays", "MP_MP_ImportExport_Tat_005_M", "Dialed In"),
            new Tattoo("mpimportexport_overlays", "MP_MP_ImportExport_Tat_003_M", "Mechanical Sleeve"),

            new Tattoo("mpbiker_overlays", "MP_MP_Biker_Tat_054_M", "Mum"),
            new Tattoo("mpbiker_overlays", "MP_MP_Biker_Tat_049_M", "These Colors Don't Run"),
            new Tattoo("mpbiker_overlays", "MP_MP_Biker_Tat_047_M", "Snake Bike"),
            new Tattoo("mpbiker_overlays", "MP_MP_Biker_Tat_046_M", "Skull Chain"),
            new Tattoo("mpbiker_overlays", "MP_MP_Biker_Tat_042_M", "Grim Rider"),
            new Tattoo("mpbiker_overlays", "MP_MP_Biker_Tat_033_M", "Eagle Emblem"),
            new Tattoo("mpbiker_overlays", "MP_MP_Biker_Tat_014_M", "Lady Mortality"),
            new Tattoo("mpbiker_overlays", "MP_MP_Biker_Tat_007_M", "Swooping Eagle"),

            new Tattoo("mpstunt_overlays", "MP_MP_Stunt_tat_049_M", "Seductive Mechanic"),
            new Tattoo("mpstunt_overlays", "MP_MP_Stunt_tat_038_M", "One Down Five Up"),
            new Tattoo("mpstunt_overlays", "MP_MP_Stunt_tat_036_M", "Biker Stallion"),
            new Tattoo("mpstunt_overlays", "MP_MP_Stunt_tat_016_M", "Coffin Racer"),
            new Tattoo("mpstunt_overlays", "MP_MP_Stunt_tat_010_M", "Grave Vulture"),
            new Tattoo("mpstunt_overlays", "MP_MP_Stunt_tat_009_M", "Arachnid of Death"),
            new Tattoo("mpstunt_overlays", "MP_MP_Stunt_tat_003_M", "Poison Wrench"),

            new Tattoo("mplowrider2_overlays", "MP_LR_Tat_035_M", "Black Tears"),
            new Tattoo("mplowrider2_overlays", "MP_LR_Tat_028_M", "Loving Los Muertos"),
            new Tattoo("mplowrider2_overlays", "MP_LR_Tat_003_M", "Lady Vamp"),

            new Tattoo("mplowrider_overlays", "MP_LR_Tat_015_M", "Seductress"),

            new Tattoo("mpluxe2_overlays", "MP_LUXE_TAT_026_M", "Floral Print"),
            new Tattoo("mpluxe2_overlays", "MP_LUXE_TAT_017_M", "Heavenly Deity"),
            new Tattoo("mpluxe2_overlays", "MP_LUXE_TAT_010_M", "Intrometric"),

            new Tattoo("mpluxe_overlays", "MP_LUXE_TAT_019_M", "Geisha Bloom"),
            new Tattoo("mpluxe_overlays", "MP_LUXE_TAT_013_M", "Mermaid Harpist"),
            new Tattoo("mpluxe_overlays", "MP_LUXE_TAT_004_M", "Floral Raven"),

            new Tattoo("mpchristmas2_overlays", "MP_Xmas2_M_Tat_027", "Fuck Luck Color"),
            new Tattoo("mpchristmas2_overlays", "MP_Xmas2_M_Tat_026", "Fuck Luck Outline"),
            new Tattoo("mpchristmas2_overlays", "MP_Xmas2_M_Tat_023", "You're Next Color"),
            new Tattoo("mpchristmas2_overlays", "MP_Xmas2_M_Tat_022", "You're Next Outline"),
            new Tattoo("mpchristmas2_overlays", "MP_Xmas2_M_Tat_008", "Death Before Dishonor"),
            new Tattoo("mpchristmas2_overlays", "MP_Xmas2_M_Tat_004", "Snake Shaded"),
            new Tattoo("mpchristmas2_overlays", "MP_Xmas2_M_Tat_003", "Snake Outline"),

            new Tattoo("mphipster_overlays", "FM_Hip_M_Tat_045", "Mesh Band"),
            new Tattoo("mphipster_overlays", "FM_Hip_M_Tat_044", "Triangle Black"),
            new Tattoo("mphipster_overlays", "FM_Hip_M_Tat_036", "Shapes"),
            new Tattoo("mphipster_overlays", "FM_Hip_M_Tat_023", "Smiley"),
            new Tattoo("mphipster_overlays", "FM_Hip_M_Tat_022", "Pencil"),
            new Tattoo("mphipster_overlays", "FM_Hip_M_Tat_020", "Geo Pattern"),
            new Tattoo("mphipster_overlays", "FM_Hip_M_Tat_018", "Origami"),
            new Tattoo("mphipster_overlays", "FM_Hip_M_Tat_017", "Eye Triangle"),
            new Tattoo("mphipster_overlays", "FM_Hip_M_Tat_014", "Spray Can"),
            new Tattoo("mphipster_overlays", "FM_Hip_M_Tat_010", "Horseshoe"),
            new Tattoo("mphipster_overlays", "FM_Hip_M_Tat_008", "Cube"),
            new Tattoo("mphipster_overlays", "FM_Hip_M_Tat_004", "Bone"),
            new Tattoo("mphipster_overlays", "FM_Hip_M_Tat_001", "Single Arrow"),

            new Tattoo("mpbusiness_overlays", "MP_Buis_M_RightArm_001", "Green"),
            new Tattoo("mpbusiness_overlays", "MP_Buis_M_RightArm_000", "Dollar Skull"),

            new Tattoo("mpbeach_overlays", "MP_Bea_M_RArm_001", "Vespucci Beauty"),
            new Tattoo("mpbeach_overlays", "MP_Bea_M_RArm_000", "Tribal Sun"),

            new Tattoo("multiplayer_overlays", "FM_Tat_M_047", "Lion"),
            new Tattoo("multiplayer_overlays", "FM_Tat_M_038", "Dagger"),
            new Tattoo("multiplayer_overlays", "FM_Tat_M_028", "Mermaid"),
            new Tattoo("multiplayer_overlays", "FM_Tat_M_027", "Virgin Mary"),
            new Tattoo("multiplayer_overlays", "FM_Tat_M_018", "Serpent Skull"),
            new Tattoo("multiplayer_overlays", "FM_Tat_M_014", "Flower Mural"),
            new Tattoo("multiplayer_overlays", "FM_Tat_M_003", "Dragons and Skull"),
            new Tattoo("multiplayer_overlays", "FM_Tat_M_001", "Dragons"),
            new Tattoo("multiplayer_overlays", "FM_Tat_M_000", "Brotherhood"),

            new Tattoo("multiplayer_overlays", "FM_Tat_Award_M_010", "Ride or Die"),
            new Tattoo("multiplayer_overlays", "FM_Tat_Award_M_002", "Grim Reaper Smoking Gun"),
                    //Not In List
            new Tattoo("mpluxe2_overlays", "MP_LUXE_TAT_030_M", "Geometric Design")
        };
        private static readonly List<Tattoo> maleTats07 = new List<Tattoo>
        {
            new Tattoo("mpsum2_overlays", "MP_Sum2_Tat_002_M", "Cobra Biker"),
            new Tattoo("mpsum2_overlays", "MP_Sum2_Tat_014_M", "Minimal SMG"),
            new Tattoo("mpsum2_overlays", "MP_Sum2_Tat_015_M", "Minimal Advanced Rifle"),
            new Tattoo("mpsum2_overlays", "MP_Sum2_Tat_016_M", "Minimal Sniper Rifle"),
            new Tattoo("mpsum2_overlays", "MP_Sum2_Tat_032_M", "Many-eyed Goat"),
            new Tattoo("mpsum2_overlays", "MP_Sum2_Tat_053_M", "Mobster Skull"),
            new Tattoo("mpsum2_overlays", "MP_Sum2_Tat_054_M", "Wounded Head"),
            new Tattoo("mpsum2_overlays", "MP_Sum2_Tat_055_M", "Stabbed Skull"),
            new Tattoo("mpsum2_overlays", "MP_Sum2_Tat_056_M", "Tiger Blade"),

            new Tattoo("mpsecurity_overlays", "MP_Security_Tat_022_M", "LS Smoking Cartridges"),
            new Tattoo("mpsecurity_overlays", "MP_Security_Tat_023_M", "Trust"),

            new Tattoo("mpheist4_overlays", "MP_Heist4_Tat_029_M", "Soundwaves"),
            new Tattoo("mpheist4_overlays", "MP_Heist4_Tat_028_M", "Skull Waters"),
            new Tattoo("mpheist4_overlays", "MP_Heist4_Tat_025_M", "Glow Princess"),
            new Tattoo("mpheist4_overlays", "MP_Heist4_Tat_024_M", "Pineapple Skull"),
            new Tattoo("mpheist4_overlays", "MP_Heist4_Tat_010_M", "Tropical Serpent"),

            new Tattoo("mpheist3_overlays", "mpHeist3_Tat_032_M", "Love Fist"),

            new Tattoo("mpvinewood_overlays", "MP_Vinewood_Tat_027_M", "8-Ball Rose"),//
            new Tattoo("mpvinewood_overlays", "MP_Vinewood_Tat_013_M", "One-armed Bandit"),//

            new Tattoo("mpgunrunning_overlays", "MP_Gunrunning_Tattoo_023_M", "Rose Revolver"),
            new Tattoo("mpgunrunning_overlays", "MP_Gunrunning_Tattoo_011_M", "Death Skull"),
            new Tattoo("mpgunrunning_overlays", "MP_Gunrunning_Tattoo_007_M", "Stylized Tiger"),
            new Tattoo("mpgunrunning_overlays", "MP_Gunrunning_Tattoo_005_M", "Patriot Skull"),

            new Tattoo("mpbiker_overlays", "MP_MP_Biker_Tat_057_M", "Laughing Skull"),
            new Tattoo("mpbiker_overlays", "MP_MP_Biker_Tat_056_M", "Bone Cruiser"),
            new Tattoo("mpbiker_overlays", "MP_MP_Biker_Tat_044_M", "Ride Free"),
            new Tattoo("mpbiker_overlays", "MP_MP_Biker_Tat_037_M", "Scorched Soul"),
            new Tattoo("mpbiker_overlays", "MP_MP_Biker_Tat_036_M", "Engulfed Skull"),
            new Tattoo("mpbiker_overlays", "MP_MP_Biker_Tat_027_M", "Bad Luck"),
            new Tattoo("mpbiker_overlays", "MP_MP_Biker_Tat_015_M", "Ride or Die"),
            new Tattoo("mpbiker_overlays", "MP_MP_Biker_Tat_002_M", "Rose Tribute"),

            new Tattoo("mpstunt_overlays", "MP_MP_Stunt_tat_031_M", "Stunt Jesus"),
            new Tattoo("mpstunt_overlays", "MP_MP_Stunt_tat_028_M", "Quad Goblin"),
            new Tattoo("mpstunt_overlays", "MP_MP_Stunt_tat_021_M", "Golden Cobra"),
            new Tattoo("mpstunt_overlays", "MP_MP_Stunt_tat_013_M", "Dirt Track Hero"),
            new Tattoo("mpstunt_overlays", "MP_MP_Stunt_tat_007_M", "Dagger Devil"),

            new Tattoo("mplowrider2_overlays", "MP_LR_Tat_029_M", "Death Us Do Part"),

            new Tattoo("mplowrider_overlays", "MP_LR_Tat_020_M", "Presidents"),//
            new Tattoo("mplowrider_overlays", "MP_LR_Tat_007_M", "LS Serpent"),//

            new Tattoo("mpluxe2_overlays", "MP_Luxe_Tat_011_M", "Cross of Roses"),
            new Tattoo("mpluxe_overlays", "MP_LUXE_TAT_000_M", "Serpent of Death"),

            new Tattoo("mpchristmas2_overlays", "MP_Xmas2_M_Tat_002", "Spider Color"),
            new Tattoo("mpchristmas2_overlays", "MP_Xmas2_M_Tat_001", "Spider Outline"),

            new Tattoo("mphipster_overlays", "FM_Hip_M_Tat_040", "Black Anchor"),
            new Tattoo("mphipster_overlays", "FM_Hip_M_Tat_019", "Charm"),
            new Tattoo("mphipster_overlays", "FM_Hip_M_Tat_009", "Squares"),

            new Tattoo("mpbeach_overlays", "MP_Bea_M_Lleg_000", "Tribal Star"),

            new Tattoo("multiplayer_overlays", "FM_Tat_M_032", "Faith"),
            new Tattoo("multiplayer_overlays", "FM_Tat_M_037", "Grim Reaper"),
            new Tattoo("multiplayer_overlays", "FM_Tat_M_035", "Dragon"),
            new Tattoo("multiplayer_overlays", "FM_Tat_M_033", "Chinese Dragon"),
            new Tattoo("multiplayer_overlays", "FM_Tat_M_026", "Smoking Dagger"),
            new Tattoo("multiplayer_overlays", "FM_Tat_M_023", "Hottie"),
            new Tattoo("multiplayer_overlays", "FM_Tat_M_021", "Serpent Skull"),
            new Tattoo("multiplayer_overlays", "FM_Tat_M_008", "Dragon Mural"),
            new Tattoo("multiplayer_overlays", "FM_Tat_M_002", "Melting Skull"),

            new Tattoo("multiplayer_overlays", "FM_Tat_Award_M_009", "Dragon and Dagger")
        };
        private static readonly List<Tattoo> maleTats08 = new List<Tattoo>
        {
            new Tattoo("mpsum2_overlays", "MP_Sum2_Tat_017_M", "Skull Grenade"),
            new Tattoo("mpsum2_overlays", "MP_Sum2_Tat_033_M", "Three-eyed Demon"),
            new Tattoo("mpsum2_overlays", "MP_Sum2_Tat_034_M", "Smoldering Reaper"),
            new Tattoo("mpsum2_overlays", "MP_Sum2_Tat_050_M", "Gold Gun"),
            new Tattoo("mpsum2_overlays", "MP_Sum2_Tat_051_M", "Blue Serpent"),
            new Tattoo("mpsum2_overlays", "MP_Sum2_Tat_052_M", "Night Demon"),

            new Tattoo("mpsecurity_overlays", "MP_Security_Tat_003_M", "Bandana Knife"),
            new Tattoo("mpsecurity_overlays", "MP_Security_Tat_021_M", "Graffiti Skull"),

            new Tattoo("mpheist4_overlays", "MP_Heist4_Tat_027_M", "Skullphones"),

            new Tattoo("mpheist3_overlays", "mpHeist3_Tat_031_M", "Kifflom"),

            new Tattoo("mpvinewood_overlays", "MP_Vinewood_Tat_020_M", "Cash is King"),

            new Tattoo("mpsmuggler_overlays", "MP_Smuggler_Tattoo_020_M", "Homeward Bound"),

            new Tattoo("mpgunrunning_overlays", "MP_Gunrunning_Tattoo_030_M", "Pistol Ace"),
            new Tattoo("mpgunrunning_overlays", "MP_Gunrunning_Tattoo_026_M", "Restless Skull"),
            new Tattoo("mpgunrunning_overlays", "MP_Gunrunning_Tattoo_006_M", "Combat Skull"),

            new Tattoo("mpbiker_overlays", "MP_MP_Biker_Tat_048_M", "STFU"),
            new Tattoo("mpbiker_overlays", "MP_MP_Biker_Tat_040_M", "American Made"),
            new Tattoo("mpbiker_overlays", "MP_MP_Biker_Tat_028_M", "Dusk Rider"),
            new Tattoo("mpbiker_overlays", "MP_MP_Biker_Tat_022_M", "Western Insignia"),
            new Tattoo("mpbiker_overlays", "MP_MP_Biker_Tat_004_M", "Dragon's Fury"),

            new Tattoo("mpstunt_overlays", "MP_MP_Stunt_tat_047_M", "Brake Knife"),
            new Tattoo("mpstunt_overlays", "MP_MP_Stunt_tat_045_M", "Severed Hand"),
            new Tattoo("mpstunt_overlays", "MP_MP_Stunt_tat_032_M", "Wheelie Mouse"),
            new Tattoo("mpstunt_overlays", "MP_MP_Stunt_tat_025_M", "Speed Freak"),
            new Tattoo("mpstunt_overlays", "MP_MP_Stunt_tat_020_M", "Piston Angel"),
            new Tattoo("mpstunt_overlays", "MP_MP_Stunt_tat_015_M", "Praying Gloves"),
            new Tattoo("mpstunt_overlays", "MP_MP_Stunt_tat_005_M", "Demon Spark Plug"),

            new Tattoo("mplowrider2_overlays", "MP_LR_Tat_030_M", "San Andreas Prayer"),

            new Tattoo("mplowrider_overlays", "MP_LR_Tat_023_M", "Dance of Hearts"),
            new Tattoo("mplowrider_overlays", "MP_LR_Tat_017_M", "Ink Me"),

            new Tattoo("mpluxe2_overlays", "MP_LUXE_TAT_023_M", "Starmetric"),

            new Tattoo("mpluxe_overlays", "MP_LUXE_TAT_001_M", "Elaborate Los Muertos"),

            new Tattoo("mpchristmas2_overlays", "MP_Xmas2_M_Tat_014", "Floral Dagger"),

            new Tattoo("mphipster_overlays", "FM_Hip_M_Tat_042", "Sparkplug"),
            new Tattoo("mphipster_overlays", "FM_Hip_M_Tat_038", "Grub"),

            new Tattoo("mpbeach_overlays", "MP_Bea_M_Rleg_000", "Tribal Tiki Tower"),

            new Tattoo("multiplayer_overlays", "FM_Tat_M_043", "Indian Ram"),
            new Tattoo("multiplayer_overlays", "FM_Tat_M_042", "Flaming Scorpion"),
            new Tattoo("multiplayer_overlays", "FM_Tat_M_040", "Flaming Skull"),
            new Tattoo("multiplayer_overlays", "FM_Tat_M_039", "Broken Skull"),
            new Tattoo("multiplayer_overlays", "FM_Tat_M_022", "Fiery Dragon"),
            new Tattoo("multiplayer_overlays", "FM_Tat_M_017", "Tribal"),
            new Tattoo("multiplayer_overlays", "FM_Tat_M_007", "The Warrior"),

            new Tattoo("multiplayer_overlays", "FM_Tat_Award_M_006", "Skull and Sword")
        };

        private static readonly List<Tattoo> femaleTats01 = new List<Tattoo>
        {
            new Tattoo("mpsum2_overlays", "MP_Sum2_Tat_006_F", "Painted Micro SMG"),
            new Tattoo("mpsum2_overlays", "MP_Sum2_Tat_007_F", "Weapon King"),
            new Tattoo("mpsum2_overlays", "MP_Sum2_Tat_035_F", "Sniff Sniff"),
            new Tattoo("mpsum2_overlays", "MP_Sum2_Tat_036_F", "Charm Pattern"),
            new Tattoo("mpsum2_overlays", "MP_Sum2_Tat_037_F", "Witch & Skull"),
            new Tattoo("mpsum2_overlays", "MP_Sum2_Tat_038_F", "Pumpkin Bug"),
            new Tattoo("mpsum2_overlays", "MP_Sum2_Tat_039_F", "Sinner"),
            new Tattoo("mpsum2_overlays", "MP_Sum2_Tat_057_F", "Gray Demon"),

            new Tattoo("mpsecurity_overlays", "MP_Security_Tat_004_F", "Hood Heart"),
            new Tattoo("mpsecurity_overlays", "MP_Security_Tat_008_F", "Los Santos Tag"),
            new Tattoo("mpsecurity_overlays", "MP_Security_Tat_013_F", "Blessed Boombox"),
            new Tattoo("mpsecurity_overlays", "MP_Security_Tat_014_F", "Chamberlain Hills"),
            new Tattoo("mpsecurity_overlays", "MP_Security_Tat_015_F", "Smoking Barrels"),
            new Tattoo("mpsecurity_overlays", "MP_Security_Tat_026_F", "Dollar Guns Crossed"),

            new Tattoo("mpheist4_overlays", "MP_Heist4_Tat_021_F", "Skull Surfer"),//
            new Tattoo("mpheist4_overlays", "MP_Heist4_Tat_020_F", "Speaker Tower"),//
            new Tattoo("mpheist4_overlays", "MP_Heist4_Tat_019_F", "Record Shot"),//
            new Tattoo("mpheist4_overlays", "MP_Heist4_Tat_018_F", "Record Head"),//
            new Tattoo("mpheist4_overlays", "MP_Heist4_Tat_017_F", "Tropical Sorcerer"),//
            new Tattoo("mpheist4_overlays", "MP_Heist4_Tat_016_F", "Rose Panther"),//
            new Tattoo("mpheist4_overlays", "MP_Heist4_Tat_015_F", "Paradise Ukulele"),//
            new Tattoo("mpheist4_overlays", "MP_Heist4_Tat_014_F", "Paradise Nap"),//
            new Tattoo("mpheist4_overlays", "MP_Heist4_Tat_013_F", "Wild Dancers"),//

            new Tattoo("mpheist3_overlays", "mpHeist3_Tat_039_F", "Space Rangers"),//
            new Tattoo("mpheist3_overlays", "mpHeist3_Tat_038_F", "Robot Bubblegum"),//
            new Tattoo("mpheist3_overlays", "mpHeist3_Tat_036_F", "LS Shield"),//
            new Tattoo("mpheist3_overlays", "mpHeist3_Tat_030_F", "Howitzer"),//
            new Tattoo("mpheist3_overlays", "mpHeist3_Tat_028_F", "Bananas Gone Bad"),//
            new Tattoo("mpheist3_overlays", "mpHeist3_Tat_027_F", "Epsilon"),//
            new Tattoo("mpheist3_overlays", "mpHeist3_Tat_024_F", "Mount Chiliad"),//
            new Tattoo("mpheist3_overlays", "mpHeist3_Tat_023_F", "Bigfoot"),//

            new Tattoo("mpvinewood_overlays", "mp_vinewood_tat_032_F", "Play Your Ace"),//
            new Tattoo("mpvinewood_overlays", "MP_Vinewood_Tat_029_F", "The Table"),//
            new Tattoo("mpvinewood_overlays", "MP_Vinewood_Tat_021_F", "Show Your Hand"),//
            new Tattoo("mpvinewood_overlays", "MP_Vinewood_Tat_017_F", "Roll the Dice"),//
            new Tattoo("mpvinewood_overlays", "MP_Vinewood_Tat_015_F", "The Jolly Joker"),//
            new Tattoo("mpvinewood_overlays", "MP_Vinewood_Tat_011_F", "Life's a Gamble"),//
            new Tattoo("mpvinewood_overlays", "MP_Vinewood_Tat_010_F", "Photo Finish"),//
            new Tattoo("mpvinewood_overlays", "MP_Vinewood_Tat_009_F", "Till Death Do Us Part"),//
            new Tattoo("mpvinewood_overlays", "MP_Vinewood_Tat_008_F", "Snake Eyes"),//
            new Tattoo("mpvinewood_overlays", "MP_Vinewood_Tat_007_F", "777"),//
            new Tattoo("mpvinewood_overlays", "MP_Vinewood_Tat_006_F", "Wheel of Suits"),//
            new Tattoo("mpvinewood_overlays", "MP_Vinewood_Tat_001_F", "Jackpot"),//

            new Tattoo("mpchristmas2017_overlays", "MP_Christmas2017_Tattoo_027_F", "Molon Labe"),
            new Tattoo("mpchristmas2017_overlays", "MP_Christmas2017_Tattoo_024_F", "Dragon Slayer"),
            new Tattoo("mpchristmas2017_overlays", "MP_Christmas2017_Tattoo_022_F", "Spartan and Horse"),
            new Tattoo("mpchristmas2017_overlays", "MP_Christmas2017_Tattoo_021_F", "Spartan and Lion"),
            new Tattoo("mpchristmas2017_overlays", "MP_Christmas2017_Tattoo_016_F", "Odin and Raven"),
            new Tattoo("mpchristmas2017_overlays", "MP_Christmas2017_Tattoo_015_F", "Samurai Combat"),
            new Tattoo("mpchristmas2017_overlays", "MP_Christmas2017_Tattoo_011_F", "Weathered Skull"),
            new Tattoo("mpchristmas2017_overlays", "MP_Christmas2017_Tattoo_010_F", "Spartan Shield"),
            new Tattoo("mpchristmas2017_overlays", "MP_Christmas2017_Tattoo_009_F", "Norse Rune"),
            new Tattoo("mpchristmas2017_overlays", "MP_Christmas2017_Tattoo_005_F", "Ghost Dragon"),
            new Tattoo("mpchristmas2017_overlays", "MP_Christmas2017_Tattoo_002_F", "Kabuto"),

            new Tattoo("mpsmuggler_overlays", "MP_Smuggler_Tattoo_025_F", "Claimed By The Beast"),
            new Tattoo("mpsmuggler_overlays", "MP_Smuggler_Tattoo_024_F", "Pirate Captain"),
            new Tattoo("mpsmuggler_overlays", "MP_Smuggler_Tattoo_022_F", "X Marks The Spot"),
            new Tattoo("mpsmuggler_overlays", "MP_Smuggler_Tattoo_018_F", "Finders Keepers"),
            new Tattoo("mpsmuggler_overlays", "MP_Smuggler_Tattoo_017_F", "Framed Tall Ship"),
            new Tattoo("mpsmuggler_overlays", "MP_Smuggler_Tattoo_016_F", "Skull Compass"),
            new Tattoo("mpsmuggler_overlays", "MP_Smuggler_Tattoo_013_F", "Torn Wings"),
            new Tattoo("mpsmuggler_overlays", "MP_Smuggler_Tattoo_009_F", "Tall Ship Conflict"),
            new Tattoo("mpsmuggler_overlays", "MP_Smuggler_Tattoo_006_F", "Never Surrender"),
            new Tattoo("mpsmuggler_overlays", "MP_Smuggler_Tattoo_003_F", "Give Nothing Back"),

            new Tattoo("mpairraces_overlays", "MP_Airraces_Tattoo_007_F", "Eagle Eyes"),
            new Tattoo("mpairraces_overlays", "MP_Airraces_Tattoo_005_F", "Parachute Belle"),
            new Tattoo("mpairraces_overlays", "MP_Airraces_Tattoo_004_F", "Balloon Pioneer"),
            new Tattoo("mpairraces_overlays", "MP_Airraces_Tattoo_002_F", "Winged Bombshell"),
            new Tattoo("mpairraces_overlays", "MP_Airraces_Tattoo_001_F", "Pilot Skull"),

            new Tattoo("mpgunrunning_overlays", "MP_Gunrunning_Tattoo_022_F", "Explosive Heart"),
            new Tattoo("mpgunrunning_overlays", "MP_Gunrunning_Tattoo_019_F", "Pistol Wings"),
            new Tattoo("mpgunrunning_overlays", "MP_Gunrunning_Tattoo_018_F", "Dual Wield Skull"),
            new Tattoo("mpgunrunning_overlays", "MP_Gunrunning_Tattoo_014_F", "Backstabber"),
            new Tattoo("mpgunrunning_overlays", "MP_Gunrunning_Tattoo_013_F", "Wolf Insignia"),
            new Tattoo("mpgunrunning_overlays", "MP_Gunrunning_Tattoo_009_F", "Butterfly Knife"),
            new Tattoo("mpgunrunning_overlays", "MP_Gunrunning_Tattoo_001_F", "Crossed Weapons"),
            new Tattoo("mpgunrunning_overlays", "MP_Gunrunning_Tattoo_000_F", "Bullet Proof"),

            new Tattoo("mpimportexport_overlays", "MP_MP_ImportExport_Tat_011_F", "Talk Shit Get Hit"),
            new Tattoo("mpimportexport_overlays", "MP_MP_ImportExport_Tat_010_F", "Take the Wheel"),
            new Tattoo("mpimportexport_overlays", "MP_MP_ImportExport_Tat_009_F", "Serpents of Destruction"),
            new Tattoo("mpimportexport_overlays", "MP_MP_ImportExport_Tat_002_F", "Tuned to Death"),
            new Tattoo("mpimportexport_overlays", "MP_MP_ImportExport_Tat_001_F", "Power Plant"),
            new Tattoo("mpimportexport_overlays", "MP_MP_ImportExport_Tat_000_F", "Block Back"),

            new Tattoo("mpbiker_overlays", "MP_MP_Biker_Tat_043_F", "Ride Forever"),
            new Tattoo("mpbiker_overlays", "MP_MP_Biker_Tat_030_F", "Brothers For Life"),
            new Tattoo("mpbiker_overlays", "MP_MP_Biker_Tat_021_F", "Flaming Reaper"),
            new Tattoo("mpbiker_overlays", "MP_MP_Biker_Tat_017_F", "Clawed Beast"),
            new Tattoo("mpbiker_overlays", "MP_MP_Biker_Tat_011_F", "R.I.P. My Brothers"),
            new Tattoo("mpbiker_overlays", "MP_MP_Biker_Tat_008_F", "Freedom Wheels"),
            new Tattoo("mpbiker_overlays", "MP_MP_Biker_Tat_006_F", "Chopper Freedom"),

            new Tattoo("mpstunt_overlays", "MP_MP_Stunt_tat_048_F", "Racing Doll"),
            new Tattoo("mpstunt_overlays", "MP_MP_Stunt_tat_046_F", "Full Throttle"),
            new Tattoo("mpstunt_overlays", "MP_MP_Stunt_tat_041_F", "Brapp"),
            new Tattoo("mpstunt_overlays", "MP_MP_Stunt_tat_040_F", "Monkey Chopper"),
            new Tattoo("mpstunt_overlays", "MP_MP_Stunt_tat_037_F", "Big Grills"),
            new Tattoo("mpstunt_overlays", "MP_MP_Stunt_tat_034_F", "Feather Road Kill"),
            new Tattoo("mpstunt_overlays", "MP_MP_Stunt_tat_030_F", "Man's Ruin"),
            new Tattoo("mpstunt_overlays", "MP_MP_Stunt_tat_029_F", "Majestic Finish"),
            new Tattoo("mpstunt_overlays", "MP_MP_Stunt_tat_026_F", "Winged Wheel"),
            new Tattoo("mpstunt_overlays", "MP_MP_Stunt_tat_024_F", "Road Kill"),

            new Tattoo("mplowrider2_overlays", "MP_LR_Tat_032_F", "Reign Over"),
            new Tattoo("mplowrider2_overlays", "MP_LR_Tat_031_F", "Dead Pretty"),
            new Tattoo("mplowrider2_overlays", "MP_LR_Tat_008_F", "Love the Game"),
            new Tattoo("mplowrider2_overlays", "MP_LR_Tat_000_F", "SA Assault"),

            new Tattoo("mplowrider_overlays", "MP_LR_Tat_021_F", "Sad Angel"),//
            new Tattoo("mplowrider_overlays", "MP_LR_Tat_014_F", "Love is Blind"),//
            new Tattoo("mplowrider_overlays", "MP_LR_Tat_010_F", "Bad Angel"),//
            new Tattoo("mplowrider_overlays", "MP_LR_Tat_009_F", "Amazon"),//

            new Tattoo("mpluxe2_overlays", "MP_Luxe_Tat_029_F", "Geometric Design"),
            new Tattoo("mpluxe2_overlays", "MP_Luxe_Tat_022_F", "Cloaked Angel"),
            new Tattoo("mpluxe_overlays", "MP_LUXE_TAT_024_F", "Feather Mural"),
            new Tattoo("mpluxe_overlays", "MP_LUXE_TAT_006_F", "Adorned Wolf"),

            new Tattoo("mpchristmas2_overlays", "MP_Xmas2_F_Tat_015", "Japanese Warrior"),
            new Tattoo("mpchristmas2_overlays", "MP_Xmas2_F_Tat_011", "Roaring Tiger"),
            new Tattoo("mpchristmas2_overlays", "MP_Xmas2_F_Tat_006", "Carp Shaded"),
            new Tattoo("mpchristmas2_overlays", "MP_Xmas2_F_Tat_005", "Carp Outline"),

            new Tattoo("mphipster_overlays", "FM_Hip_F_Tat_046", "Triangles"),
            new Tattoo("mphipster_overlays", "FM_Hip_F_Tat_041", "Tooth"),
            new Tattoo("mphipster_overlays", "FM_Hip_F_Tat_032", "Paper Plane"),
            new Tattoo("mphipster_overlays", "FM_Hip_F_Tat_031", "Skateboard"),
            new Tattoo("mphipster_overlays", "FM_Hip_F_Tat_030", "Shark Fin"),
            new Tattoo("mphipster_overlays", "FM_Hip_F_Tat_025", "Watch Your Step"),
            new Tattoo("mphipster_overlays", "FM_Hip_F_Tat_024", "Pyamid"),
            new Tattoo("mphipster_overlays", "FM_Hip_F_Tat_012", "Antlers"),
            new Tattoo("mphipster_overlays", "FM_Hip_F_Tat_011", "Infinity"),
            new Tattoo("mphipster_overlays", "FM_Hip_F_Tat_000", "Crossed Arrows"),

            new Tattoo("mpbusiness_overlays", "MP_Buis_F_Back_001", "Gold Digger"),
            new Tattoo("mpbusiness_overlays", "MP_Buis_F_Back_000", "Respect"),

            new Tattoo("mpbeach_overlays", "MP_Bea_F_Should_000", "Sea Horses"),
            new Tattoo("mpbeach_overlays", "MP_Bea_F_Back_002", "Shrimp"),
            new Tattoo("mpbeach_overlays", "MP_Bea_F_Should_001", "Catfish"),
            new Tattoo("mpbeach_overlays", "MP_Bea_F_Back_000", "Rock Solid"),
            new Tattoo("mpbeach_overlays", "MP_Bea_F_Back_001", "Hibiscus Flower Duo"),

            new Tattoo("multiplayer_overlays", "FM_Tat_F_045", "Skulls and Rose"),
            new Tattoo("multiplayer_overlays", "FM_Tat_F_030", "Lucky Celtic Dogs"),
            new Tattoo("multiplayer_overlays", "FM_Tat_F_020", "Dragon"),
            new Tattoo("multiplayer_overlays", "FM_Tat_F_019", "The Wages of Sin"),
            new Tattoo("multiplayer_overlays", "FM_Tat_F_016", "Evil Clown"),
            new Tattoo("multiplayer_overlays", "FM_Tat_F_013", "Eagle and Serpent"),
            new Tattoo("multiplayer_overlays", "FM_Tat_F_011", "LS Script"),
            new Tattoo("multiplayer_overlays", "FM_Tat_F_009", "Skull on the Cross"),

            new Tattoo("multiplayer_overlays", "FM_Tat_Award_F_019", "Clown Dual Wield Dollars"),
            new Tattoo("multiplayer_overlays", "FM_Tat_Award_F_018", "Clown Dual Wield"),
            new Tattoo("multiplayer_overlays", "FM_Tat_Award_F_017", "Clown and Gun"),
            new Tattoo("multiplayer_overlays", "FM_Tat_Award_F_016", "Clown"),
            new Tattoo("multiplayer_overlays", "FM_Tat_Award_F_014", "Trust No One"),//Car Bomb Award
            new Tattoo("multiplayer_overlays", "FM_Tat_Award_F_008", "Los Santos Customs"),//Mod a Car Award
            new Tattoo("multiplayer_overlays", "FM_Tat_Award_F_005", "Angel"),//Win Every Game Mode Award
                                                                                                                              //Not In List
            new Tattoo("multiplayer_overlays", "FM_Tat_F_046", "Zip?"),
            new Tattoo("mphipster_overlays", "FM_Hip_F_Tat_006", "Feather Birds"),
            new Tattoo("mpchristmas2018_overlays", "MP_Christmas2018_Tat_000_F", "???")
        };
        private static readonly List<Tattoo> femaleTats02 = new List<Tattoo>
        {
            new Tattoo("mpsum2_overlays", "MP_Sum2_Tat_003_F", "Bullet Mouth"),
            new Tattoo("mpsum2_overlays", "MP_Sum2_Tat_004_F", "Smoking Barrel"),
            new Tattoo("mpsum2_overlays", "MP_Sum2_Tat_040_F", "Carved Pumpkin"),
            new Tattoo("mpsum2_overlays", "MP_Sum2_Tat_041_F", "Branched Werewolf"),
            new Tattoo("mpsum2_overlays", "MP_Sum2_Tat_042_F", "Winged Skull"),
            new Tattoo("mpsum2_overlays", "MP_Sum2_Tat_058_F", "Shrieking Dragon"),
            new Tattoo("mpsum2_overlays", "MP_Sum2_Tat_059_F", "Swords & City"),

            new Tattoo("mpsecurity_overlays", "MP_Security_Tat_016_F", "All From The Same Tree"),
            new Tattoo("mpsecurity_overlays", "MP_Security_Tat_017_F", "King of the Jungle"),
            new Tattoo("mpsecurity_overlays", "MP_Security_Tat_018_F", "Night Owl"),

            new Tattoo("mpheist4_overlays", "MP_Heist4_Tat_023_F", "Techno Glitch"),//
            new Tattoo("mpheist4_overlays", "MP_Heist4_Tat_022_F", "Paradise Sirens"),//

            new Tattoo("mpheist3_overlays", "mpHeist3_Tat_035_F", "LS Panic"),
            new Tattoo("mpheist3_overlays", "mpHeist3_Tat_033_F", "LS City"),
            new Tattoo("mpheist3_overlays", "mpHeist3_Tat_026_F", "Dignity"),
            new Tattoo("mpheist3_overlays", "mpHeist3_Tat_025_F", "Davis"),

            new Tattoo("mpvinewood_overlays", "mp_vinewood_tat_022_F", "Blood Money"),//
            new Tattoo("mpvinewood_overlays", "mp_vinewood_tat_003_F", "Royal Flush"),//
            new Tattoo("mpvinewood_overlays", "mp_vinewood_tat_000_F", "In the Pocket"),//

            new Tattoo("mpchristmas2017_overlays", "MP_Christmas2017_Tattoo_026_F", "Spartan Skull"),
            new Tattoo("mpchristmas2017_overlays", "MP_Christmas2017_Tattoo_020_F", "Medusa's Gaze"),
            new Tattoo("mpchristmas2017_overlays", "MP_Christmas2017_Tattoo_019_F", "Strike Force"),
            new Tattoo("mpchristmas2017_overlays", "MP_Christmas2017_Tattoo_003_F", "Native Warrior"),
            new Tattoo("mpchristmas2017_overlays", "MP_Christmas2017_Tattoo_000_F", "Thor - Goblin"),

            new Tattoo("mpsmuggler_overlays", "MP_Smuggler_Tattoo_021_F", "Dead Tales"),
            new Tattoo("mpsmuggler_overlays", "MP_Smuggler_Tattoo_019_F", "Lost At Sea"),
            new Tattoo("mpsmuggler_overlays", "MP_Smuggler_Tattoo_007_F", "No Honor"),
            new Tattoo("mpsmuggler_overlays", "MP_Smuggler_Tattoo_000_F", "Bless The Dead"),

            new Tattoo("mpairraces_overlays", "MP_Airraces_Tattoo_000_F", "Turbulence"),

            new Tattoo("mpgunrunning_overlays", "MP_Gunrunning_Tattoo_028_F", "Micro SMG Chain"),
            new Tattoo("mpgunrunning_overlays", "MP_Gunrunning_Tattoo_020_F", "Crowned Weapons"),
            new Tattoo("mpgunrunning_overlays", "MP_Gunrunning_Tattoo_017_F", "Dog Tags"),
            new Tattoo("mpgunrunning_overlays", "MP_Gunrunning_Tattoo_012_F", "Dollar Daggers"),

            new Tattoo("mpbiker_overlays", "MP_MP_Biker_Tat_060_F", "We Are The Mods!"),
            new Tattoo("mpbiker_overlays", "MP_MP_Biker_Tat_059_F", "Faggio"),
            new Tattoo("mpbiker_overlays", "MP_MP_Biker_Tat_058_F", "Reaper Vulture"),
            new Tattoo("mpbiker_overlays", "MP_MP_Biker_Tat_050_F", "Unforgiven"),
            new Tattoo("mpbiker_overlays", "MP_MP_Biker_Tat_041_F", "No Regrets"),
            new Tattoo("mpbiker_overlays", "MP_MP_Biker_Tat_034_F", "Brotherhood of Bikes"),
            new Tattoo("mpbiker_overlays", "MP_MP_Biker_Tat_032_F", "Western Eagle"),
            new Tattoo("mpbiker_overlays", "MP_MP_Biker_Tat_029_F", "Bone Wrench"),
            new Tattoo("mpbiker_overlays", "MP_MP_Biker_Tat_026_F", "American Dream"),
            new Tattoo("mpbiker_overlays", "MP_MP_Biker_Tat_023_F", "Western MC"),
            new Tattoo("mpbiker_overlays", "MP_MP_Biker_Tat_019_F", "Gruesome Talons"),
            new Tattoo("mpbiker_overlays", "MP_MP_Biker_Tat_018_F", "Skeletal Chopper"),
            new Tattoo("mpbiker_overlays", "MP_MP_Biker_Tat_013_F", "Demon Crossbones"),
            new Tattoo("mpbiker_overlays", "MP_MP_Biker_Tat_005_F", "Made In America"),
            new Tattoo("mpbiker_overlays", "MP_MP_Biker_Tat_001_F", "Both Barrels"),
            new Tattoo("mpbiker_overlays", "MP_MP_Biker_Tat_000_F", "Demon Rider"),

            new Tattoo("mpstunt_overlays", "MP_MP_Stunt_tat_044_F", "Ram Skull"),
            new Tattoo("mpstunt_overlays", "MP_MP_Stunt_tat_033_F", "Sugar Skull Trucker"),
            new Tattoo("mpstunt_overlays", "MP_MP_Stunt_tat_027_F", "Punk Road Hog"),
            new Tattoo("mpstunt_overlays", "MP_MP_Stunt_tat_019_F", "Engine Heart"),
            new Tattoo("mpstunt_overlays", "MP_MP_Stunt_tat_018_F", "Vintage Bully"),
            new Tattoo("mpstunt_overlays", "MP_MP_Stunt_tat_011_F", "Wheels of Death"),

            new Tattoo("mplowrider2_overlays", "MP_LR_Tat_019_F", "Death Behind"),
            new Tattoo("mplowrider2_overlays", "MP_LR_Tat_012_F", "Royal Kiss"),

            new Tattoo("mplowrider_overlays", "MP_LR_Tat_026_F", "Royal Takeover"),//
            new Tattoo("mplowrider_overlays", "MP_LR_Tat_013_F", "Love Gamble"),//
            new Tattoo("mplowrider_overlays", "MP_LR_Tat_002_F", "Holy Mary"),//
            new Tattoo("mplowrider_overlays", "MP_LR_Tat_001_F", "King Fight"),//

            new Tattoo("mpluxe2_overlays", "MP_Luxe_Tat_027_F", "Cobra Dawn"),
            new Tattoo("mpluxe2_overlays", "MP_Luxe_Tat_025_F", "Reaper Sway"),
            new Tattoo("mpluxe2_overlays", "MP_Luxe_Tat_012_F", "Geometric Galaxy"),
            new Tattoo("mpluxe2_overlays", "MP_Luxe_Tat_002_F", "The Howler"),

            new Tattoo("mpluxe_overlays", "MP_Luxe_Tat_015_F", "Smoking Sisters"),
            new Tattoo("mpluxe_overlays", "MP_Luxe_Tat_014_F", "Ancient Queen"),
            new Tattoo("mpluxe_overlays", "MP_Luxe_Tat_008_F", "Flying Eye"),
            new Tattoo("mpluxe_overlays", "MP_Luxe_Tat_007_F", "Eye of the Griffin"),

            new Tattoo("mpchristmas2_overlays", "MP_Xmas2_F_Tat_019", "Royal Dagger Color"),
            new Tattoo("mpchristmas2_overlays", "MP_Xmas2_F_Tat_018", "Royal Dagger Outline"),
            new Tattoo("mpchristmas2_overlays", "MP_Xmas2_F_Tat_017", "Loose Lips Color"),
            new Tattoo("mpchristmas2_overlays", "MP_Xmas2_F_Tat_016", "Loose Lips Outline"),
            new Tattoo("mpchristmas2_overlays", "MP_Xmas2_F_Tat_009", "Time To Die"),

            new Tattoo("mphipster_overlays", "FM_Hip_F_Tat_047", "Cassette"),
            new Tattoo("mphipster_overlays", "FM_Hip_F_Tat_033", "Stag"),
            new Tattoo("mphipster_overlays", "FM_Hip_F_Tat_013", "Boombox"),
            new Tattoo("mphipster_overlays", "FM_Hip_F_Tat_002", "Chemistry"),

            new Tattoo("mpbusiness_overlays", "MP_Buis_F_Chest_002", "Love Money"),
            new Tattoo("mpbusiness_overlays", "MP_Buis_F_Chest_001", "Makin' Money"),
            new Tattoo("mpbusiness_overlays", "MP_Buis_F_Chest_000", "High Roller"),

            new Tattoo("mpbeach_overlays", "MP_Bea_F_Chest_001", "Anchor"),
            new Tattoo("mpbeach_overlays", "MP_Bea_F_Chest_000", "Anchor"),
            new Tattoo("mpbeach_overlays", "MP_Bea_F_Chest_002", "Los Santos Wreath"),

            new Tattoo("multiplayer_overlays", "FM_Tat_F_044", "Stone Cross"),
            new Tattoo("multiplayer_overlays", "FM_Tat_F_034", "Flaming Shamrock"),
            new Tattoo("multiplayer_overlays", "FM_Tat_F_025", "LS Bold"),
            new Tattoo("multiplayer_overlays", "FM_Tat_F_024", "Flaming Cross"),
            new Tattoo("multiplayer_overlays", "FM_Tat_F_010", "LS Flames"),

            new Tattoo("multiplayer_overlays", "FM_Tat_Award_F_013", "Seven Deadly Sins"),//Kill 1000 Players Award
            new Tattoo("multiplayer_overlays", "FM_Tat_Award_F_012", "Embellished Scroll"),//Kill 500 Players Award
            new Tattoo("multiplayer_overlays", "FM_Tat_Award_F_011", "Blank Scroll"),////Kill 100 Players Award?
            new Tattoo("multiplayer_overlays", "FM_Tat_Award_F_003", "Blackjack")
        };
        private static readonly List<Tattoo> femaleTats03 = new List<Tattoo>
        {
            new Tattoo("mpsum2_overlays", "MP_Sum2_Tat_005_F", "Concealed"),
            new Tattoo("mpsum2_overlays", "MP_Sum2_Tat_043_F", "Cursed Saki"),
            new Tattoo("mpsum2_overlays", "MP_Sum2_Tat_044_F", "Smouldering Bat & Skull"),
            new Tattoo("mpsum2_overlays", "MP_Sum2_Tat_060_F", "Blaine County"),
            new Tattoo("mpsum2_overlays", "MP_Sum2_Tat_061_F", "Angry Possum"),
            new Tattoo("mpsum2_overlays", "MP_Sum2_Tat_062_F", "Floral Demon"),

            new Tattoo("mpsecurity_overlays", "MP_Security_Tat_024_F", "Beatbox Silhouette"),
            new Tattoo("mpsecurity_overlays", "MP_Security_Tat_025_F", "Davis Flames"),

            new Tattoo("mpheist4_overlays", "MP_Heist4_Tat_030_F", "Radio Tape"),//
            new Tattoo("mpheist4_overlays", "MP_Heist4_Tat_004_F", "Skeleton Breeze"),//

            new Tattoo("mpheist3_overlays", "mpHeist3_Tat_037_F", "LadyBug"),
            new Tattoo("mpheist3_overlays", "mpHeist3_Tat_029_F", "Fatal Incursion"),

            new Tattoo("mpvinewood_overlays", "mp_vinewood_tat_031_F", "Gambling Royalty"),//
            new Tattoo("mpvinewood_overlays", "mp_vinewood_tat_024_F", "Cash Mouth"),//
            new Tattoo("mpvinewood_overlays", "mp_vinewood_tat_016_F", "Rose and Aces"),//
            new Tattoo("mpvinewood_overlays", "mp_vinewood_tat_012_F", "Skull of Suits"),//

            new Tattoo("mpchristmas2017_overlays", "MP_Christmas2017_Tattoo_008_F", "Spartan Warrior"),

            new Tattoo("mpsmuggler_overlays", "MP_Smuggler_Tattoo_015_F", "Jolly Roger"),
            new Tattoo("mpsmuggler_overlays", "MP_Smuggler_Tattoo_010_F", "See You In Hell"),
            new Tattoo("mpsmuggler_overlays", "MP_Smuggler_Tattoo_002_F", "Dead Lies"),

            new Tattoo("mpairraces_overlays", "MP_Airraces_Tattoo_006_F", "Bombs Away"),

            new Tattoo("mpgunrunning_overlays", "MP_Gunrunning_Tattoo_029_F", "Win Some Lose Some"),
            new Tattoo("mpgunrunning_overlays", "MP_Gunrunning_Tattoo_010_F", "Cash Money"),

            new Tattoo("mpbiker_overlays", "MP_MP_Biker_Tat_052_F", "Biker Mount"),
            new Tattoo("mpbiker_overlays", "MP_MP_Biker_Tat_039_F", "Gas Guzzler"),
            new Tattoo("mpbiker_overlays", "MP_MP_Biker_Tat_031_F", "Gear Head"),
            new Tattoo("mpbiker_overlays", "MP_MP_Biker_Tat_010_F", "Skull Of Taurus"),
            new Tattoo("mpbiker_overlays", "MP_MP_Biker_Tat_003_F", "Web Rider"),

            new Tattoo("mpstunt_overlays", "MP_MP_Stunt_tat_014_F", "Bat Cat of Spades"),
            new Tattoo("mpstunt_overlays", "MP_MP_Stunt_tat_012_F", "Punk Biker"),

            new Tattoo("mplowrider2_overlays", "MP_LR_Tat_016_F", "Two Face"),
            new Tattoo("mplowrider2_overlays", "MP_LR_Tat_011_F", "Lady Liberty"),

            new Tattoo("mplowrider_overlays", "MP_LR_Tat_004_F", "Gun Mic"),//

            new Tattoo("mpluxe_overlays", "MP_Luxe_Tat_003_F", "Abstract Skull"),

            new Tattoo("mpchristmas2_overlays", "MP_Xmas2_F_Tat_028", "Executioner"),
            new Tattoo("mpchristmas2_overlays", "MP_Xmas2_F_Tat_013", "Lizard"),

            new Tattoo("mphipster_overlays", "FM_Hip_F_Tat_035", "Sewn Heart"),
            new Tattoo("mphipster_overlays", "FM_Hip_F_Tat_029", "Sad"),
            new Tattoo("mphipster_overlays", "FM_Hip_F_Tat_006", "Feather Birds"),

            new Tattoo("mpbusiness_overlays", "MP_Buis_F_Stom_002", "Money Bag"),
            new Tattoo("mpbusiness_overlays", "MP_Buis_F_Stom_001", "Santo Capra Logo"),
            new Tattoo("mpbusiness_overlays", "MP_Buis_F_Stom_000", "Diamond Back"),

            new Tattoo("mpbeach_overlays", "MP_Bea_F_Stom_000", "Swallow"),
            new Tattoo("mpbeach_overlays", "MP_Bea_F_Stom_002", "Dolphin"),
            new Tattoo("mpbeach_overlays", "MP_Bea_F_Stom_001", "Hibiscus Flower"),
            new Tattoo("mpbeach_overlays", "MP_Bea_F_RSide_000", "Love Dagger"),

            new Tattoo("multiplayer_overlays", "FM_Tat_F_036", "Way of the Gun"),//500 Pistol kills Award
            new Tattoo("multiplayer_overlays", "FM_Tat_F_029", "Trinity Knot"),
            new Tattoo("multiplayer_overlays", "FM_Tat_F_012", "Los Santos Bills"),
            new Tattoo("multiplayer_overlays", "FM_Tat_F_004", "Faith"),

            new Tattoo("multiplayer_overlays", "FM_Tat_Award_F_004", "Hustler")
        };
        private static readonly List<Tattoo> femaleTats04 = new List<Tattoo>
        {
            new Tattoo("mpsum2_overlays", "MP_Sum2_Tat_000_F", "Live Fast Mono"),
            new Tattoo("mpsum2_overlays", "MP_Sum2_Tat_001_F", "Live Fast Color"),
            new Tattoo("mpsum2_overlays", "MP_Sum2_Tat_018_F", "Branched Skull"),
            new Tattoo("mpsum2_overlays", "MP_Sum2_Tat_019_F", "Scythed Corpse"),
            new Tattoo("mpsum2_overlays", "MP_Sum2_Tat_020_F", "Scythed Corpse & Reaper"),
            new Tattoo("mpsum2_overlays", "MP_Sum2_Tat_021_F", "Third Eye"),
            new Tattoo("mpsum2_overlays", "MP_Sum2_Tat_022_F", "Pierced Third Eye"),
            new Tattoo("mpsum2_overlays", "MP_Sum2_Tat_023_F", "Lip Drip"),
            new Tattoo("mpsum2_overlays", "MP_Sum2_Tat_024_F", "Skin Mask"),
            new Tattoo("mpsum2_overlays", "MP_Sum2_Tat_025_F", "Webbed Scythe"),
            new Tattoo("mpsum2_overlays", "MP_Sum2_Tat_026_F", "Oni Demon"),
            new Tattoo("mpsum2_overlays", "MP_Sum2_Tat_027_F", "Bat Wings"),

            new Tattoo("mpsecurity_overlays", "MP_Security_Tat_001_F", "Bright Diamond"),
            new Tattoo("mpsecurity_overlays", "MP_Security_Tat_002_F", "Hustle"),
            new Tattoo("mpsecurity_overlays", "MP_Security_Tat_027_F", "Black Widow"),

            new Tattoo("mpheist3_overlays", "mpHeist3_Tat_044_F", "Clubs"),
            new Tattoo("mpheist3_overlays", "mpHeist3_Tat_043_F", "Diamonds"),
            new Tattoo("mpheist3_overlays", "mpHeist3_Tat_042_F", "Hearts"),

            new Tattoo("mpheist3_overlays", "mpHeist3_Tat_022_F", "Thong's Sword"),
            new Tattoo("mpheist3_overlays", "mpHeist3_Tat_021_F", "Wanted"),
            new Tattoo("mpheist3_overlays", "mpHeist3_Tat_020_F", "UFO"),
            new Tattoo("mpheist3_overlays", "mpHeist3_Tat_019_F", "Teddy Bear"),
            new Tattoo("mpheist3_overlays", "mpHeist3_Tat_018_F", "Stitches"),
            new Tattoo("mpheist3_overlays", "mpHeist3_Tat_017_F", "Space Monkey"),
            new Tattoo("mpheist3_overlays", "mpHeist3_Tat_016_F", "Sleepy"),
            new Tattoo("mpheist3_overlays", "mpHeist3_Tat_015_F", "On/Off"),
            new Tattoo("mpheist3_overlays", "mpHeist3_Tat_014_F", "LS Wings"),
            new Tattoo("mpheist3_overlays", "mpHeist3_Tat_013_F", "LS Star"),
            new Tattoo("mpheist3_overlays", "mpHeist3_Tat_012_F", "Razor Pop"),
            new Tattoo("mpheist3_overlays", "mpHeist3_Tat_011_F", "Lipstick Kiss"),
            new Tattoo("mpheist3_overlays", "mpHeist3_Tat_010_F", "Green Leaf"),
            new Tattoo("mpheist3_overlays", "mpHeist3_Tat_009_F", "Knifed"),
            new Tattoo("mpheist3_overlays", "mpHeist3_Tat_008_F", "Ice Cream"),
            new Tattoo("mpheist3_overlays", "mpHeist3_Tat_007_F", "Two Horns"),
            new Tattoo("mpheist3_overlays", "mpHeist3_Tat_006_F", "Crowned"),
            new Tattoo("mpheist3_overlays", "mpHeist3_Tat_005_F", "Spades"),
            new Tattoo("mpheist3_overlays", "mpHeist3_Tat_004_F", "Bandage"),
            new Tattoo("mpheist3_overlays", "mpHeist3_Tat_003_F", "Assault Rifle"),
            new Tattoo("mpheist3_overlays", "mpHeist3_Tat_002_F", "Animal"),
            new Tattoo("mpheist3_overlays", "mpHeist3_Tat_001_F", "Ace of Spades"),
            new Tattoo("mpheist3_overlays", "mpHeist3_Tat_000_F", "Five Stars"),

            new Tattoo("mpsmuggler_overlays", "MP_Smuggler_Tattoo_012_F", "Thief"),
            new Tattoo("mpsmuggler_overlays", "MP_Smuggler_Tattoo_011_F", "Sinner"),

            new Tattoo("mpgunrunning_overlays", "MP_Gunrunning_Tattoo_003_F", "Lock and Load"),

            new Tattoo("mpbiker_overlays", "MP_MP_Biker_Tat_051_F", "Western Stylized"),
            new Tattoo("mpbiker_overlays", "MP_MP_Biker_Tat_038_F", "FTW"),
            new Tattoo("mpbiker_overlays", "MP_MP_Biker_Tat_009_F", "Morbid Arachnid"),

            new Tattoo("mpstunt_overlays", "MP_MP_Stunt_tat_042_F", "Flaming Quad"),
            new Tattoo("mpstunt_overlays", "MP_MP_Stunt_tat_017_F", "Bat Wheel"),
            new Tattoo("mpstunt_overlays", "MP_MP_Stunt_Tat_006_F", "Toxic Spider"),
            new Tattoo("mpstunt_overlays", "MP_MP_Stunt_Tat_004_F", "Scorpion"),
            new Tattoo("mpstunt_overlays", "MP_MP_Stunt_Tat_000_F", "Stunt Skull"),

            new Tattoo("mpchristmas2_overlays", "MP_Xmas2_F_Tat_029", "Beautiful Death"),
            new Tattoo("mpchristmas2_overlays", "MP_Xmas2_F_Tat_025", "Snake Head Color"),
            new Tattoo("mpchristmas2_overlays", "MP_Xmas2_F_Tat_024", "Snake Head Outline"),
            new Tattoo("mpchristmas2_overlays", "MP_Xmas2_F_Tat_007", "Los Muertos"),

            new Tattoo("mphipster_overlays", "FM_Hip_F_Tat_021", "Geo Fox"),
            new Tattoo("mphipster_overlays", "FM_Hip_F_Tat_005", "Beautiful Eye"),

            new Tattoo("mpbusiness_overlays", "MP_Buis_F_Neck_001", "Money Rose"),
            new Tattoo("mpbusiness_overlays", "MP_Buis_F_Neck_000", "Val-de-Grace Logo"),

            new Tattoo("mpbeach_overlays", "MP_Bea_F_Neck_000", "Tribal Butterfly"),
            new Tattoo("mpbeach_overlays", "MP_Bea_F_Neck_000", "Little Fish"),

            new Tattoo("multiplayer_overlays", "FM_Tat_Award_F_000", "Skull")
        };
        private static readonly List<Tattoo> femaleTats05 = new List<Tattoo>
        {
            new Tattoo("mpsum2_overlays", "MP_Sum2_Tat_008_F", "Bigness Chimp"),
            new Tattoo("mpsum2_overlays", "MP_Sum2_Tat_009_F", "Up-n-Atomizer Design"),
            new Tattoo("mpsum2_overlays", "MP_Sum2_Tat_010_F", "Rocket Launcher Girl"),
            new Tattoo("mpsum2_overlays", "MP_Sum2_Tat_028_F", "Laser Eyes Skull"),
            new Tattoo("mpsum2_overlays", "MP_Sum2_Tat_029_F", "Classic Vampire"),
            new Tattoo("mpsum2_overlays", "MP_Sum2_Tat_049_F", "Demon Drummer"),

            new Tattoo("mpsecurity_overlays", "MP_Security_Tat_006_F", "Skeleton Shot"),
            new Tattoo("mpsecurity_overlays", "MP_Security_Tat_010_F", "Music Is The Remedy"),
            new Tattoo("mpsecurity_overlays", "MP_Security_Tat_011_F", "Serpent Mic"),
            new Tattoo("mpsecurity_overlays", "MP_Security_Tat_019_F", "Weed Knuckles"),

            new Tattoo("mpheist4_overlays", "MP_Heist4_Tat_009_F", "Scratch Panther"),

            new Tattoo("mpheist3_overlays", "mpHeist3_Tat_041_F", "Mighty Thog"),
            new Tattoo("mpheist3_overlays", "mpHeist3_Tat_040_F", "Tiger Heart"),

            new Tattoo("mpvinewood_overlays", "MP_Vinewood_Tat_026_F", "Banknote Rose"),//
            new Tattoo("mpvinewood_overlays", "MP_Vinewood_Tat_019_F", "Can't Win Them All"),//
            new Tattoo("mpvinewood_overlays", "MP_Vinewood_Tat_014_F", "Vice"),//
            new Tattoo("mpvinewood_overlays", "MP_Vinewood_Tat_005_F", "Get Lucky"),//
            new Tattoo("mpvinewood_overlays", "MP_Vinewood_Tat_002_F", "Suits"),//

            new Tattoo("mpchristmas2017_overlays", "MP_Christmas2017_Tattoo_029_F", "Cerberus"),
            new Tattoo("mpchristmas2017_overlays", "MP_Christmas2017_Tattoo_025_F", "Winged Serpent"),
            new Tattoo("mpchristmas2017_overlays", "MP_Christmas2017_Tattoo_013_F", "Katana"),
            new Tattoo("mpchristmas2017_overlays", "MP_Christmas2017_Tattoo_007_F", "Spartan Combat"),
            new Tattoo("mpchristmas2017_overlays", "MP_Christmas2017_Tattoo_004_F", "Tiger and Mask"),
            new Tattoo("mpchristmas2017_overlays", "MP_Christmas2017_Tattoo_001_F", "Viking Warrior"),

            new Tattoo("mpsmuggler_overlays", "MP_Smuggler_Tattoo_014_F", "Mermaid's Curse"),
            new Tattoo("mpsmuggler_overlays", "MP_Smuggler_Tattoo_008_F", "Horrors Of The Deep"),
            new Tattoo("mpsmuggler_overlays", "MP_Smuggler_Tattoo_004_F", "Honor"),

            new Tattoo("mpairraces_overlays", "MP_Airraces_Tattoo_003_F", "Toxic Trails"),

            new Tattoo("mpgunrunning_overlays", "MP_Gunrunning_Tattoo_027_F", "Serpent Revolver"),
            new Tattoo("mpgunrunning_overlays", "MP_Gunrunning_Tattoo_025_F", "Praying Skull"),
            new Tattoo("mpgunrunning_overlays", "MP_Gunrunning_Tattoo_016_F", "Blood Money"),
            new Tattoo("mpgunrunning_overlays", "MP_Gunrunning_Tattoo_015_F", "Spiked Skull"),
            new Tattoo("mpgunrunning_overlays", "MP_Gunrunning_Tattoo_008_F", "Bandolier"),
            new Tattoo("mpgunrunning_overlays", "MP_Gunrunning_Tattoo_004_F", "Sidearm"),

            new Tattoo("mpimportexport_overlays", "MP_MP_ImportExport_Tat_008_F", "Scarlett"),
            new Tattoo("mpimportexport_overlays", "MP_MP_ImportExport_Tat_004_F", "Piston Sleeve"),

            new Tattoo("mpbiker_overlays", "MP_MP_Biker_Tat_055_F", "Poison Scorpion"),
            new Tattoo("mpbiker_overlays", "MP_MP_Biker_Tat_053_F", "Muffler Helmet"),
            new Tattoo("mpbiker_overlays", "MP_MP_Biker_Tat_045_F", "Ride Hard Die Fast"),
            new Tattoo("mpbiker_overlays", "MP_MP_Biker_Tat_035_F", "Chain Fist"),
            new Tattoo("mpbiker_overlays", "MP_MP_Biker_Tat_025_F", "Good Luck"),
            new Tattoo("mpbiker_overlays", "MP_MP_Biker_Tat_024_F", "Live to Ride"),
            new Tattoo("mpbiker_overlays", "MP_MP_Biker_Tat_020_F", "Cranial Rose"),
            new Tattoo("mpbiker_overlays", "MP_MP_Biker_Tat_016_F", "Macabre Tree"),
            new Tattoo("mpbiker_overlays", "MP_MP_Biker_Tat_012_F", "Urban Stunter"),

            new Tattoo("mpstunt_overlays", "MP_MP_Stunt_tat_043_F", "Engine Arm"),
            new Tattoo("mpstunt_overlays", "MP_MP_Stunt_tat_039_F", "Kaboom"),
            new Tattoo("mpstunt_overlays", "MP_MP_Stunt_tat_035_F", "Stuntman's End"),
            new Tattoo("mpstunt_overlays", "MP_MP_Stunt_tat_023_F", "Tanked"),
            new Tattoo("mpstunt_overlays", "MP_MP_Stunt_tat_022_F", "Piston Head"),
            new Tattoo("mpstunt_overlays", "MP_MP_Stunt_tat_008_F", "Moonlight Ride"),
            new Tattoo("mpstunt_overlays", "MP_MP_Stunt_tat_002_F", "Big Cat"),
            new Tattoo("mpstunt_overlays", "MP_MP_Stunt_tat_001_F", "8 Eyed Skull"),

            new Tattoo("mplowrider2_overlays", "MP_LR_Tat_022_F", "My Crazy Life"),
            new Tattoo("mplowrider2_overlays", "MP_LR_Tat_018_F", "Skeleton Party"),
            new Tattoo("mplowrider2_overlays", "MP_LR_Tat_006_F", "Love Hustle"),

            new Tattoo("mplowrider_overlays", "MP_LR_Tat_033_F", "City Sorrow"),//
            new Tattoo("mplowrider_overlays", "MP_LR_Tat_027_F", "Los Santos Life"),//
            new Tattoo("mplowrider_overlays", "MP_LR_Tat_005_F", "No Evil"),//

            new Tattoo("mpluxe2_overlays", "MP_Luxe_Tat_028_F", "Python Skull"),
            new Tattoo("mpluxe2_overlays", "MP_Luxe_Tat_018_F", "Divine Goddess"),
            new Tattoo("mpluxe2_overlays", "MP_Luxe_Tat_016_F", "Egyptian Mural"),
            new Tattoo("mpluxe2_overlays", "MP_Luxe_Tat_005_F", "Fatal Dagger"),

            new Tattoo("mpluxe_overlays", "MP_Luxe_Tat_021_F", "Gabriel"),
            new Tattoo("mpluxe_overlays", "MP_Luxe_Tat_020_F", "Archangel and Mary"),
            new Tattoo("mpluxe_overlays", "MP_Luxe_Tat_009_F", "Floral Symmetry"),

            new Tattoo("mpchristmas2_overlays", "MP_Xmas2_F_Tat_021", "Time's Up Color"),
            new Tattoo("mpchristmas2_overlays", "MP_Xmas2_F_Tat_020", "Time's Up Outline"),
            new Tattoo("mpchristmas2_overlays", "MP_Xmas2_F_Tat_012", "8 Ball Skull"),
            new Tattoo("mpchristmas2_overlays", "MP_Xmas2_F_Tat_010", "Electric Snake"),
            new Tattoo("mpchristmas2_overlays", "MP_Xmas2_F_Tat_000", "Skull Rider"),

            new Tattoo("mphipster_overlays", "FM_Hip_F_Tat_048", "Peace"),
            new Tattoo("mphipster_overlays", "FM_Hip_F_Tat_043", "Triangle White"),
            new Tattoo("mphipster_overlays", "FM_Hip_F_Tat_039", "Sleeve"),
            new Tattoo("mphipster_overlays", "FM_Hip_F_Tat_037", "Sunrise"),
            new Tattoo("mphipster_overlays", "FM_Hip_F_Tat_034", "Stop"),
            new Tattoo("mphipster_overlays", "FM_Hip_F_Tat_028", "Thorny Rose"),
            new Tattoo("mphipster_overlays", "FM_Hip_F_Tat_027", "Padlock"),
            new Tattoo("mphipster_overlays", "FM_Hip_F_Tat_026", "Pizza"),
            new Tattoo("mphipster_overlays", "FM_Hip_F_Tat_016", "Lightning Bolt"),
            new Tattoo("mphipster_overlays", "FM_Hip_F_Tat_015", "Mustache"),
            new Tattoo("mphipster_overlays", "FM_Hip_F_Tat_007", "Bricks"),
            new Tattoo("mphipster_overlays", "FM_Hip_F_Tat_003", "Diamond Sparkle"),

            new Tattoo("mpbusiness_overlays", "MP_Buis_F_LArm_000", "Greed is Good"),

            new Tattoo("mpbeach_overlays", "MP_Bea_F_LArm_001", "Parrot"),
            new Tattoo("mpbeach_overlays", "MP_Bea_F_LArm_000", "Tribal Flower"),

            new Tattoo("multiplayer_overlays", "FM_Tat_F_041", "Dope Skull"),
            new Tattoo("multiplayer_overlays", "FM_Tat_F_031", "Lady M"),
            new Tattoo("multiplayer_overlays", "FM_Tat_F_015", "Zodiac Skull"),
            new Tattoo("multiplayer_overlays", "FM_Tat_F_006", "Oriental Mural"),
            new Tattoo("multiplayer_overlays", "FM_Tat_F_005", "Serpents"),

            new Tattoo("multiplayer_overlays", "FM_Tat_Award_F_015", "Racing Brunette"),
            new Tattoo("multiplayer_overlays", "FM_Tat_Award_F_007", "Racing Blonde"),
            new Tattoo("multiplayer_overlays", "FM_Tat_Award_F_001", "Burning Heart"),//50 Death Match Award
                                                                                                                                      //not on list
            new Tattoo("mpluxe2_overlays", "MP_Luxe_Tat_031_F", "Geometric Design")
        };
        private static readonly List<Tattoo> femaleTats06 = new List<Tattoo>
        {
            new Tattoo("mpsum2_overlays", "MP_Sum2_Tat_011_F", "Nothing Mini About It"),
            new Tattoo("mpsum2_overlays", "MP_Sum2_Tat_012_F", "Snake Revolver"),
            new Tattoo("mpsum2_overlays", "MP_Sum2_Tat_013_F", "Weapon Sleeve"),
            new Tattoo("mpsum2_overlays", "MP_Sum2_Tat_030_F", "Centipede"),
            new Tattoo("mpsum2_overlays", "MP_Sum2_Tat_031_F", "Fleshy Eye"),
            new Tattoo("mpsum2_overlays", "MP_Sum2_Tat_045_F", "Armored Arm"),
            new Tattoo("mpsum2_overlays", "MP_Sum2_Tat_046_F", "Demon Smile"),
            new Tattoo("mpsum2_overlays", "MP_Sum2_Tat_047_F", "Angel & Devil"),
            new Tattoo("mpsum2_overlays", "MP_Sum2_Tat_048_F", "Death Is Certain"),

            new Tattoo("mpsecurity_overlays", "MP_Security_Tat_000_F", "Hood Skeleton"),
            new Tattoo("mpsecurity_overlays", "MP_Security_Tat_005_F", "Peacock"),
            new Tattoo("mpsecurity_overlays", "MP_Security_Tat_007_F", "Ballas 4 Life"),
            new Tattoo("mpsecurity_overlays", "MP_Security_Tat_009_F", "Ascension"),
            new Tattoo("mpsecurity_overlays", "MP_Security_Tat_012_F", "Zombie Rhymes"),
            new Tattoo("mpsecurity_overlays", "MP_Security_Tat_020_F", "Dog Fist"),

            new Tattoo("mpheist4_overlays", "MP_Heist4_Tat_032_F", "K.U.L.T.99.1 FM"),
            new Tattoo("mpheist4_overlays", "MP_Heist4_Tat_031_F", "Octopus Shades"),
            new Tattoo("mpheist4_overlays", "MP_Heist4_Tat_026_F", "Shark Water"),
            new Tattoo("mpheist4_overlays", "MP_Heist4_Tat_012_F", "Still Slipping"),
            new Tattoo("mpheist4_overlays", "MP_Heist4_Tat_011_F", "Soulwax"),
            new Tattoo("mpheist4_overlays", "MP_Heist4_Tat_008_F", "Smiley Glitch"),
            new Tattoo("mpheist4_overlays", "MP_Heist4_Tat_007_F", "Skeleton DJ"),
            new Tattoo("mpheist4_overlays", "MP_Heist4_Tat_006_F", "Music Locker"),
            new Tattoo("mpheist4_overlays", "MP_Heist4_Tat_005_F", "LSUR"),
            new Tattoo("mpheist4_overlays", "MP_Heist4_Tat_003_F", "Lighthouse"),
            new Tattoo("mpheist4_overlays", "MP_Heist4_Tat_002_F", "Jellyfish Shades"),
            new Tattoo("mpheist4_overlays", "MP_Heist4_Tat_001_F", "Tropical Dude"),
            new Tattoo("mpheist4_overlays", "MP_Heist4_Tat_000_F", "Headphone Splat"),

            new Tattoo("mpheist3_overlays", "mpHeist3_Tat_034_F", "LS Monogram"),

            new Tattoo("mpvinewood_overlays", "MP_Vinewood_Tat_028_F", "Skull and Aces"),//
            new Tattoo("mpvinewood_overlays", "MP_Vinewood_Tat_025_F", "Queen of Roses"),//
            new Tattoo("mpvinewood_overlays", "MP_Vinewood_Tat_018_F", "The Gambler's Life"),//
            new Tattoo("mpvinewood_overlays", "MP_Vinewood_Tat_004_F", "Lady Luck"),//

            new Tattoo("mpchristmas2017_overlays", "MP_Christmas2017_Tattoo_028_F", "Spartan Mural"),
            new Tattoo("mpchristmas2017_overlays", "MP_Christmas2017_Tattoo_023_F", "Samurai Tallship"),
            new Tattoo("mpchristmas2017_overlays", "MP_Christmas2017_Tattoo_018_F", "Muscle Tear"),
            new Tattoo("mpchristmas2017_overlays", "MP_Christmas2017_Tattoo_017_F", "Feather Sleeve"),
            new Tattoo("mpchristmas2017_overlays", "MP_Christmas2017_Tattoo_014_F", "Celtic Band"),
            new Tattoo("mpchristmas2017_overlays", "MP_Christmas2017_Tattoo_012_F", "Tiger Headdress"),
            new Tattoo("mpchristmas2017_overlays", "MP_Christmas2017_Tattoo_006_F", "Medusa"),

            new Tattoo("mpsmuggler_overlays", "MP_Smuggler_Tattoo_023_F", "Stylized Kraken"),
            new Tattoo("mpsmuggler_overlays", "MP_Smuggler_Tattoo_005_F", "Mutiny"),
            new Tattoo("mpsmuggler_overlays", "MP_Smuggler_Tattoo_001_F", "Crackshot"),

            new Tattoo("mpgunrunning_overlays", "MP_Gunrunning_Tattoo_024_F", "Combat Reaper"),
            new Tattoo("mpgunrunning_overlays", "MP_Gunrunning_Tattoo_021_F", "Have a Nice Day"),
            new Tattoo("mpgunrunning_overlays", "MP_Gunrunning_Tattoo_002_F", "Grenade"),

            new Tattoo("mpimportexport_overlays", "MP_MP_ImportExport_Tat_007_F", "Drive Forever"),
            new Tattoo("mpimportexport_overlays", "MP_MP_ImportExport_Tat_006_F", "Engulfed Block"),
            new Tattoo("mpimportexport_overlays", "MP_MP_ImportExport_Tat_005_F", "Dialed In"),
            new Tattoo("mpimportexport_overlays", "MP_MP_ImportExport_Tat_003_F", "Mechanical Sleeve"),

            new Tattoo("mpbiker_overlays", "MP_MP_Biker_Tat_054_F", "Mum"),
            new Tattoo("mpbiker_overlays", "MP_MP_Biker_Tat_049_F", "These Colors Don't Run"),
            new Tattoo("mpbiker_overlays", "MP_MP_Biker_Tat_047_F", "Snake Bike"),
            new Tattoo("mpbiker_overlays", "MP_MP_Biker_Tat_046_F", "Skull Chain"),
            new Tattoo("mpbiker_overlays", "MP_MP_Biker_Tat_042_F", "Grim Rider"),
            new Tattoo("mpbiker_overlays", "MP_MP_Biker_Tat_033_F", "Eagle Emblem"),
            new Tattoo("mpbiker_overlays", "MP_MP_Biker_Tat_014_F", "Lady Mortality"),
            new Tattoo("mpbiker_overlays", "MP_MP_Biker_Tat_007_F", "Swooping Eagle"),

            new Tattoo("mpstunt_overlays", "MP_MP_Stunt_tat_049_F", "Seductive Mechanic"),
            new Tattoo("mpstunt_overlays", "MP_MP_Stunt_tat_038_F", "One Down Five Up"),
            new Tattoo("mpstunt_overlays", "MP_MP_Stunt_tat_036_F", "Biker Stallion"),
            new Tattoo("mpstunt_overlays", "MP_MP_Stunt_tat_016_F", "Coffin Racer"),
            new Tattoo("mpstunt_overlays", "MP_MP_Stunt_tat_010_F", "Grave Vulture"),
            new Tattoo("mpstunt_overlays", "MP_MP_Stunt_tat_009_F", "Arachnid of Death"),
            new Tattoo("mpstunt_overlays", "MP_MP_Stunt_tat_003_F", "Poison Wrench"),

            new Tattoo("mplowrider2_overlays", "MP_LR_Tat_035_F", "Black Tears"),
            new Tattoo("mplowrider2_overlays", "MP_LR_Tat_028_F", "Loving Los Muertos"),
            new Tattoo("mplowrider2_overlays", "MP_LR_Tat_003_F", "Lady Vamp"),

            new Tattoo("mplowrider_overlays", "MP_LR_Tat_015_F", "Seductress"),//

            new Tattoo("mpluxe2_overlays", "MP_LUXE_TAT_026_F", "Floral Print"),
            new Tattoo("mpluxe2_overlays", "MP_LUXE_TAT_017_F", "Heavenly Deity"),
            new Tattoo("mpluxe2_overlays", "MP_LUXE_TAT_010_F", "Intrometric"),

            new Tattoo("mpluxe_overlays", "MP_LUXE_TAT_019_F", "Geisha Bloom"),
            new Tattoo("mpluxe_overlays", "MP_LUXE_TAT_013_F", "Mermaid Harpist"),
            new Tattoo("mpluxe_overlays", "MP_LUXE_TAT_004_F", "Floral Raven"),

            new Tattoo("mpchristmas2_overlays", "MP_Xmas2_F_Tat_027", "Fuck Luck Color"),
            new Tattoo("mpchristmas2_overlays", "MP_Xmas2_F_Tat_026", "Fuck Luck Outline"),
            new Tattoo("mpchristmas2_overlays", "MP_Xmas2_F_Tat_023", "You're Next Color"),
            new Tattoo("mpchristmas2_overlays", "MP_Xmas2_F_Tat_022", "You're Next Outline"),
            new Tattoo("mpchristmas2_overlays", "MP_Xmas2_F_Tat_008", "Death Before Dishonor"),
            new Tattoo("mpchristmas2_overlays", "MP_Xmas2_F_Tat_004", "Snake Shaded"),
            new Tattoo("mpchristmas2_overlays", "MP_Xmas2_F_Tat_003", "Snake Outline"),

            new Tattoo("mphipster_overlays", "FM_Hip_F_Tat_045", "Mesh Band"),
            new Tattoo("mphipster_overlays", "FM_Hip_F_Tat_044", "Triangle Black"),
            new Tattoo("mphipster_overlays", "FM_Hip_F_Tat_036", "Shapes"),
            new Tattoo("mphipster_overlays", "FM_Hip_F_Tat_023", "Smiley"),
            new Tattoo("mphipster_overlays", "FM_Hip_F_Tat_022", "Pencil"),
            new Tattoo("mphipster_overlays", "FM_Hip_F_Tat_020", "Geo Pattern"),
            new Tattoo("mphipster_overlays", "FM_Hip_F_Tat_018", "Origami"),
            new Tattoo("mphipster_overlays", "FM_Hip_F_Tat_017", "Eye Triangle"),
            new Tattoo("mphipster_overlays", "FM_Hip_F_Tat_014", "Spray Can"),
            new Tattoo("mphipster_overlays", "FM_Hip_F_Tat_010", "Horseshoe"),
            new Tattoo("mphipster_overlays", "FM_Hip_F_Tat_008", "Cube"),
            new Tattoo("mphipster_overlays", "FM_Hip_F_Tat_004", "Bone"),
            new Tattoo("mphipster_overlays", "FM_Hip_F_Tat_001", "Single Arrow"),

            new Tattoo("mpbusiness_overlays", "MP_Buis_F_RArm_000", "Dollar Sign"),

            new Tattoo("mpbeach_overlays", "MP_Bea_F_RArm_001", "Tribal Fish"),

            new Tattoo("multiplayer_overlays", "FM_Tat_F_047", "Lion"),
            new Tattoo("multiplayer_overlays", "FM_Tat_F_038", "Dagger"),
            new Tattoo("multiplayer_overlays", "FM_Tat_F_028", "Mermaid"),
            new Tattoo("multiplayer_overlays", "FM_Tat_F_027", "Virgin Mary"),
            new Tattoo("multiplayer_overlays", "FM_Tat_F_018", "Serpent Skull"),
            new Tattoo("multiplayer_overlays", "FM_Tat_F_014", "Flower Mural"),
            new Tattoo("multiplayer_overlays", "FM_Tat_F_003", "Dragons and Skull"),
            new Tattoo("multiplayer_overlays", "FM_Tat_F_001", "Dragons"),
                //TatThis.Addnew Tattoo("multiplayer_overlays", "FM_Tat_F_000", "Brotherhood") ,-empty load?

            new Tattoo("multiplayer_overlays", "FM_Tat_Award_F_010", "Ride or Die"),
            new Tattoo("multiplayer_overlays", "FM_Tat_Award_F_002", "Grim Reaper Smoking Gun"),//Clear 5 Gang Atacks in a Day Award
                                                                                                                                                //Not In List
            new Tattoo("mpluxe2_overlays", "MP_LUXE_TAT_030_F", "Geometric Design")
        };
        private static readonly List<Tattoo> femaleTats07 = new List<Tattoo>
        {
            new Tattoo("mpsum2_overlays", "MP_Sum2_Tat_002_F", "Cobra Biker"),
            new Tattoo("mpsum2_overlays", "MP_Sum2_Tat_014_F", "Minimal SMG"),
            new Tattoo("mpsum2_overlays", "MP_Sum2_Tat_015_F", "Minimal Advanced Rifle"),
            new Tattoo("mpsum2_overlays", "MP_Sum2_Tat_016_F", "Minimal Sniper Rifle"),
            new Tattoo("mpsum2_overlays", "MP_Sum2_Tat_032_F", "Many-eyed Goat"),
            new Tattoo("mpsum2_overlays", "MP_Sum2_Tat_053_F", "Mobster Skull"),
            new Tattoo("mpsum2_overlays", "MP_Sum2_Tat_054_F", "Wounded Head"),
            new Tattoo("mpsum2_overlays", "MP_Sum2_Tat_055_F", "Stabbed Skull"),
            new Tattoo("mpsum2_overlays", "MP_Sum2_Tat_056_F", "Tiger Blade"),

            new Tattoo("mpsecurity_overlays", "MP_Security_Tat_022_F", "LS Smoking Cartridges"),
            new Tattoo("mpsecurity_overlays", "MP_Security_Tat_023_F", "Trust"),

            new Tattoo("mpheist4_overlays", "MP_Heist4_Tat_029_F", "Soundwaves"),
            new Tattoo("mpheist4_overlays", "MP_Heist4_Tat_028_F", "Skull Waters"),
            new Tattoo("mpheist4_overlays", "MP_Heist4_Tat_025_F", "Glow Princess"),
            new Tattoo("mpheist4_overlays", "MP_Heist4_Tat_024_F", "Pineapple Skull"),
            new Tattoo("mpheist4_overlays", "MP_Heist4_Tat_010_F", "Tropical Serpent"),

            new Tattoo("mpheist3_overlays", "mpHeist3_Tat_032_F", "Love Fist"),

            new Tattoo("mpvinewood_overlays", "MP_Vinewood_Tat_027_F", "8-Ball Rose"),//
            new Tattoo("mpvinewood_overlays", "MP_Vinewood_Tat_013_F", "One-armed Bandit"),//

            new Tattoo("mpgunrunning_overlays", "MP_Gunrunning_Tattoo_023_F", "Rose Revolver"),
            new Tattoo("mpgunrunning_overlays", "MP_Gunrunning_Tattoo_011_F", "Death Skull"),
            new Tattoo("mpgunrunning_overlays", "MP_Gunrunning_Tattoo_007_F", "Stylized Tiger"),
            new Tattoo("mpgunrunning_overlays", "MP_Gunrunning_Tattoo_005_F", "Patriot Skull"),

            new Tattoo("mpbiker_overlays", "MP_MP_Biker_Tat_057_F", "Laughing Skull"),
            new Tattoo("mpbiker_overlays", "MP_MP_Biker_Tat_056_F", "Bone Cruiser"),
            new Tattoo("mpbiker_overlays", "MP_MP_Biker_Tat_044_F", "Ride Free"),
            new Tattoo("mpbiker_overlays", "MP_MP_Biker_Tat_037_F", "Scorched Soul"),
            new Tattoo("mpbiker_overlays", "MP_MP_Biker_Tat_036_F", "Engulfed Skull"),
            new Tattoo("mpbiker_overlays", "MP_MP_Biker_Tat_027_F", "Bad Luck"),
            new Tattoo("mpbiker_overlays", "MP_MP_Biker_Tat_015_F", "Ride or Die"),
            new Tattoo("mpbiker_overlays", "MP_MP_Biker_Tat_002_F", "Rose Tribute"),

            new Tattoo("mpstunt_overlays", "MP_MP_Stunt_tat_031_F", "Stunt Jesus"),
            new Tattoo("mpstunt_overlays", "MP_MP_Stunt_tat_028_F", "Quad Goblin"),
            new Tattoo("mpstunt_overlays", "MP_MP_Stunt_tat_021_F", "Golden Cobra"),
            new Tattoo("mpstunt_overlays", "MP_MP_Stunt_tat_013_F", "Dirt Track Hero"),
            new Tattoo("mpstunt_overlays", "MP_MP_Stunt_tat_007_F", "Dagger Devil"),

            new Tattoo("mplowrider2_overlays", "MP_LR_Tat_029_F", "Death Us Do Part"),

            new Tattoo("mplowrider_overlays", "MP_LR_Tat_020_F", "Presidents"),//
            new Tattoo("mplowrider_overlays", "MP_LR_Tat_007_F", "LS Serpent"),//

            new Tattoo("mpluxe2_overlays", "MP_Luxe_Tat_011_F", "Cross of Roses"),
            new Tattoo("mpluxe_overlays", "MP_LUXE_TAT_000_F", "Serpent of Death"),

            new Tattoo("mpchristmas2_overlays", "MP_Xmas2_F_Tat_002", "Spider Color"),
            new Tattoo("mpchristmas2_overlays", "MP_Xmas2_F_Tat_001", "Spider Outline"),

            new Tattoo("mphipster_overlays", "FM_Hip_F_Tat_040", "Black Anchor"),
            new Tattoo("mphipster_overlays", "FM_Hip_F_Tat_019", "Charm"),
            new Tattoo("mphipster_overlays", "FM_Hip_F_Tat_009", "Squares"),

            new Tattoo("mpbusiness_overlays", "MP_Buis_F_LLeg_000", "Single"),

            new Tattoo("multiplayer_overlays", "FM_Tat_F_032", "Faith"),
            new Tattoo("multiplayer_overlays", "FM_Tat_F_037", "Grim Reaper"),
            new Tattoo("multiplayer_overlays", "FM_Tat_F_035", "Dragon"),
            new Tattoo("multiplayer_overlays", "FM_Tat_F_033", "Chinese Dragon"),
            new Tattoo("multiplayer_overlays", "FM_Tat_F_026", "Smoking Dagger"),
            new Tattoo("multiplayer_overlays", "FM_Tat_F_023", "Hottie"),
            new Tattoo("multiplayer_overlays", "FM_Tat_F_021", "Serpent Skull"),
            new Tattoo("multiplayer_overlays", "FM_Tat_F_008", "Dragon Mural"),
            new Tattoo("multiplayer_overlays", "FM_Tat_F_002", "Melting Skull"),

            new Tattoo("multiplayer_overlays", "FM_Tat_Award_F_009", "Dragon and Dagger")
        };
        private static readonly List<Tattoo> femaleTats08 = new List<Tattoo>
        {
            new Tattoo("mpsum2_overlays", "MP_Sum2_Tat_017_F", "Skull Grenade"),
            new Tattoo("mpsum2_overlays", "MP_Sum2_Tat_033_F", "Three-eyed Demon"),
            new Tattoo("mpsum2_overlays", "MP_Sum2_Tat_034_F", "Smoldering Reaper"),
            new Tattoo("mpsum2_overlays", "MP_Sum2_Tat_050_F", "Gold Gun"),
            new Tattoo("mpsum2_overlays", "MP_Sum2_Tat_051_F", "Blue Serpent"),
            new Tattoo("mpsum2_overlays", "MP_Sum2_Tat_052_F", "Night Demon"),

            new Tattoo("mpsecurity_overlays", "MP_Security_Tat_003_F", "Bandana Knife"),
            new Tattoo("mpsecurity_overlays", "MP_Security_Tat_021_F", "Graffiti Skull"),

            new Tattoo("mpheist4_overlays", "MP_Heist4_Tat_027_F", "Skullphones"),

            new Tattoo("mpheist3_overlays", "mpHeist3_Tat_031_F", "Kifflom"),

            new Tattoo("mpvinewood_overlays", "MP_Vinewood_Tat_020_F", "Cash is King"),//

            new Tattoo("mpsmuggler_overlays", "MP_Smuggler_Tattoo_020_F", "Homeward Bound"),

            new Tattoo("mpgunrunning_overlays", "MP_Gunrunning_Tattoo_030_F", "Pistol Ace"),
            new Tattoo("mpgunrunning_overlays", "MP_Gunrunning_Tattoo_026_F", "Restless Skull"),
            new Tattoo("mpgunrunning_overlays", "MP_Gunrunning_Tattoo_006_F", "Combat Skull"),

            new Tattoo("mpbiker_overlays", "MP_MP_Biker_Tat_048_F", "STFU"),
            new Tattoo("mpbiker_overlays", "MP_MP_Biker_Tat_040_F", "American Made"),
            new Tattoo("mpbiker_overlays", "MP_MP_Biker_Tat_028_F", "Dusk Rider"),
            new Tattoo("mpbiker_overlays", "MP_MP_Biker_Tat_022_F", "Western Insignia"),
            new Tattoo("mpbiker_overlays", "MP_MP_Biker_Tat_004_F", "Dragon's Fury"),

            new Tattoo("mpstunt_overlays", "MP_MP_Stunt_tat_047_F", "Brake Knife"),
            new Tattoo("mpstunt_overlays", "MP_MP_Stunt_tat_045_F", "Severed Hand"),
            new Tattoo("mpstunt_overlays", "MP_MP_Stunt_tat_032_F", "Wheelie Mouse"),
            new Tattoo("mpstunt_overlays", "MP_MP_Stunt_tat_025_F", "Speed Freak"),
            new Tattoo("mpstunt_overlays", "MP_MP_Stunt_tat_020_F", "Piston Angel"),
            new Tattoo("mpstunt_overlays", "MP_MP_Stunt_tat_015_F", "Praying Gloves"),
            new Tattoo("mpstunt_overlays", "MP_MP_Stunt_tat_005_F", "Demon Spark Plug"),

            new Tattoo("mplowrider2_overlays", "MP_LR_Tat_030_F", "San Andreas Prayer"),

            new Tattoo("mplowrider_overlays", "MP_LR_Tat_023_F", "Dance of Hearts"),//
            new Tattoo("mplowrider_overlays", "MP_LR_Tat_017_F", "Ink Me"),//

            new Tattoo("mpluxe2_overlays", "MP_LUXE_TAT_023_F", "Starmetric"),

            new Tattoo("mpluxe_overlays", "MP_LUXE_TAT_001_F", "Elaborate Los Muertos"),

            new Tattoo("mpchristmas2_overlays", "MP_Xmas2_F_Tat_014", "Floral Dagger"),

            new Tattoo("mphipster_overlays", "FM_Hip_F_Tat_042", "Sparkplug"),
            new Tattoo("mphipster_overlays", "FM_Hip_F_Tat_038", "Grub"),

            new Tattoo("mpbusiness_overlays", "MP_Buis_F_RLeg_000", "Diamond Crown"),

            new Tattoo("mpbeach_overlays", "MP_Bea_F_RLeg_000", "School of Fish"),

            new Tattoo("multiplayer_overlays", "FM_Tat_F_043", "Indian Ram"),
            new Tattoo("multiplayer_overlays", "FM_Tat_F_042", "Flaming Scorpion"),
            new Tattoo("multiplayer_overlays", "FM_Tat_F_040", "Flaming Skull"),
            new Tattoo("multiplayer_overlays", "FM_Tat_F_039", "Broken Skull"),
            new Tattoo("multiplayer_overlays", "FM_Tat_F_022", "Fiery Dragon"),
            new Tattoo("multiplayer_overlays", "FM_Tat_F_017", "Tribal"),
            new Tattoo("multiplayer_overlays", "FM_Tat_F_007", "The Warrior"),

            new Tattoo("multiplayer_overlays", "FM_Tat_Award_F_006", "Skull and Sword")
        };

        public static PedFixtures MakeFaces()
        {
            LoggerLight.GetLogging("FreemodePed.MakeFaces");

            bool bMale = false;
            PedFixtures myFixtures = new PedFixtures();

            if (RandomNum.FindRandom(13, 0, 20) < 10)
                bMale = true;

            //****************FaceShape/Colour****************
            int shapeFirstID;
            int shapeSecondID;
            int shapeThirdID;
            int skinFirstID;
            int skinSecondID;
            int skinThirdID;
            float shapeMix;
            float skinMix;
            float thirdMix;

            if (bMale)
            {
                myFixtures.PFMale = true;
                shapeFirstID = RandomNum.RandInt(0, 20);
                shapeSecondID = RandomNum.RandInt(0, 20);
                shapeThirdID = shapeFirstID;
                skinFirstID = shapeFirstID;
                skinSecondID = shapeSecondID;
                skinThirdID = shapeThirdID;
            }
            else
            {
                myFixtures.PFMale = false;
                shapeFirstID = RandomNum.RandInt(21, 41);
                shapeSecondID = RandomNum.RandInt(21, 41);
                shapeThirdID = shapeFirstID;
                skinFirstID = shapeFirstID;
                skinSecondID = shapeSecondID;
                skinThirdID = shapeThirdID;
            }

            shapeMix = ReturnValues.RandFlow(-0.9f, 0.9f);
            skinMix = ReturnValues.RandFlow(0.9f, 0.99f);
            thirdMix = ReturnValues.RandFlow(-0.9f, 0.9f);

            myFixtures.PFshapeFirstID = shapeFirstID;
            myFixtures.PFshapeSecondID = shapeSecondID;
            myFixtures.PFshapeThirdID = shapeThirdID;
            myFixtures.PFskinFirstID = skinFirstID;
            myFixtures.PFskinSecondID = skinSecondID;
            myFixtures.PFskinThirdID = skinThirdID;
            myFixtures.PFshapeMix = shapeMix;
            myFixtures.PFskinMix = skinMix;
            myFixtures.PFthirdMix = thirdMix;

            //****************Face****************
            for (int i = 0; i < 12; i++)
            {
                int iColour = 0;
                int iChange = RandomNum.RandInt(0, Function.Call<int>(Hash._GET_NUM_HEAD_OVERLAY_VALUES, i));
                float fVar = ReturnValues.RandFlow(0.45f, 0.99f);

                if (i == 0)
                {
                    iChange = RandomNum.RandInt(0, iChange);
                }//Blemishes
                else if (i == 1)
                {
                    if (bMale)
                        iChange = RandomNum.RandInt(0, iChange);
                    else
                        iChange = 255;
                    iColour = 1;
                }//Facial Hair
                else if (i == 2)
                {
                    iChange = RandomNum.RandInt(0, iChange);
                    iColour = 1;
                }//Eyebrows
                else if (i == 3)
                {
                    iChange = 255;
                }//Ageing
                else if (i == 4)
                {
                    int iFace = RandomNum.RandInt(0, 50);
                    if (iFace < 30)
                    {
                        iChange = RandomNum.RandInt(0, 15);
                    }
                    else if (iFace < 45)
                    {
                        iChange = RandomNum.RandInt(0, iChange);
                        fVar = ReturnValues.RandFlow(0.85f, 0.99f);
                    }
                    else
                        iChange = 255;
                }//Makeup
                else if (i == 5)
                {
                    if (!bMale)
                    {
                        iChange = RandomNum.RandInt(0, iChange);
                        fVar = ReturnValues.RandFlow(0.15f, 0.39f);
                    }
                    else
                        iChange = 255;
                    iColour = 2;
                }//Blush
                else if (i == 6)
                {
                    iChange = RandomNum.RandInt(0, iChange);
                }//Complexion
                else if (i == 7)
                {
                    iChange = 255;
                }//Sun Damage
                else if (i == 8)
                {
                    if (!bMale)
                        iChange = RandomNum.RandInt(0, iChange);
                    else
                        iChange = 255;
                    iColour = 2;
                }//Lipstick
                else if (i == 9)
                {
                    iChange = RandomNum.RandInt(0, iChange);
                }//Moles/Freckles
                else if (i == 10)
                {
                    if (bMale)
                        iChange = RandomNum.RandInt(0, iChange);
                    else
                        iChange = 255;
                    iColour = 1;
                }//Chest Hair
                else if (i == 11)
                {
                    iChange = RandomNum.RandInt(0, iChange);
                }//Body Blemishes

                int AddColour = 0;

                if (iColour > 0)
                    AddColour = RandomNum.RandInt(0, 64);

                myFixtures.PFFeature.Add(iChange);
                myFixtures.PFColour.Add(AddColour);
                myFixtures.PFAmount.Add(fVar);
            }
            //****************Hair****************
            int iHairStyle;

            if (bMale)
                iHairStyle = RandomNum.RandInt(37, 76);
            else
                iHairStyle = RandomNum.RandInt(37, 80);

            myFixtures.iHairCut = iHairStyle;

            int iHair = RandomNum.RandInt(0, Function.Call<int>(Hash._GET_NUM_HAIR_COLORS));
            int iHair2 = RandomNum.RandInt(0, Function.Call<int>(Hash._GET_NUM_HAIR_COLORS));

            myFixtures.PFHair01 = iHair;
            myFixtures.PFHair02 = iHair2;

            //****************Tattoos****************
            if (RandomNum.RandInt(1, 10) < 5)
            {
                List<Tattoo> Tatty = AddRandTats(bMale);
                int iCount = RandomNum.RandInt(1, Tatty.Count);

                for (int i = 0; i < iCount; i++)
                {
                    int iTat = RandomNum.RandInt(0, Tatty.Count - 1);
                    myFixtures.TatBase.Add(Tatty[iTat].BaseName);
                    myFixtures.TatName.Add(Tatty[iTat].TatName);
                    Tatty.RemoveAt(iTat);
                }
            }
            //****************Clothing****************
            ClothBank myCloth = new ClothBank();
            int iCloth;
            if (bMale)
            {
                if (DataStore.iEventFind == -1)
                {
                    if (DataStore.maleCloth.Count > 0)
                    {
                        iCloth = RandomNum.FindRandom(40, 0, DataStore.maleCloth.Count - 1);
                        myCloth = DataStore.maleCloth[iCloth];
                    }
                    else
                        myCloth = PickOutfit(true, -1);
                }
                else
                {
                    if (DataStore.iEventFind == 1)
                        myCloth = PickOutfit(true, RandomNum.RandInt(99, 101));
                    else
                        myCloth = PickOutfit(true, RandomNum.RandInt(74, 80));
                }

            }
            else
            {
                if (DataStore.iEventFind == -1)
                {
                    if (DataStore.femaleCloth.Count > 0)
                    {
                        iCloth = RandomNum.FindRandom(41, 0, DataStore.femaleCloth.Count - 1);
                        myCloth = DataStore.femaleCloth[iCloth];
                    }
                    else
                        myCloth = PickOutfit(false, -1);
                }
                else
                {
                    if (DataStore.iEventFind == 1)
                        myCloth = PickOutfit(true, RandomNum.RandInt(99, 117));
                    else
                        myCloth = PickOutfit(true, RandomNum.RandInt(101, 80));
                }
            }
            myCloth.ClothA[2] = iHairStyle;

            myFixtures.PedCloth = myCloth;

            return myFixtures;
        }
        public static void OnlineFaces(Ped Pedx, PedFixtures pFixtures)
        {
            LoggerLight.GetLogging("FreemodePed OnlineFace Loaded Fixtures");

            //****************FaceShape/Colour****************
            if (pFixtures.PedCloth.Title != "Body_Suit")
            {
                int shapeFirstID = 0;
                int shapeSecondID = 0;
                int shapeThirdID = 0;
                int skinFirstID = 1;
                int skinSecondID = 1;
                int skinThirdID = 1;
                float shapeMix = 0.0f;
                float skinMix = 0.0f;
                float thirdMix = 0.0f;
                bool isParent = false;

                shapeFirstID = pFixtures.PFshapeFirstID;
                shapeSecondID = pFixtures.PFshapeSecondID;
                shapeThirdID = pFixtures.PFshapeThirdID;
                skinFirstID = pFixtures.PFskinFirstID;
                skinSecondID = pFixtures.PFskinSecondID;
                skinThirdID = pFixtures.PFskinThirdID;
                shapeMix = pFixtures.PFshapeMix;
                skinMix = pFixtures.PFskinMix;
                thirdMix = pFixtures.PFthirdMix;

                Function.Call(Hash.SET_PED_HEAD_BLEND_DATA, Pedx.Handle, shapeFirstID, shapeSecondID, shapeThirdID, skinFirstID, skinSecondID, skinThirdID, shapeMix, skinMix, thirdMix, isParent);
            }
            //****************Face****************
            for (int i = 0; i < 12; i++)
            {
                int iColour = 0;

                if (i == 1)
                {
                    iColour = 1;
                }//Facial Hair
                else if (i == 2)
                {
                    iColour = 1;
                }//Eyebrows
                else if (i == 5)
                {
                    iColour = 2;
                }//Blush
                else if (i == 8)
                {
                    iColour = 2;
                }//Lipstick
                else if (i == 10)
                {
                    iColour = 1;
                }//Chest Hair

                int iChange = pFixtures.PFFeature[i];
                int AddColour = pFixtures.PFColour[i];
                float fVar = pFixtures.PFAmount[i];

                Function.Call(Hash.SET_PED_HEAD_OVERLAY, Pedx.Handle, i, iChange, fVar);

                if (iColour > 0)
                    Function.Call(Hash._SET_PED_HEAD_OVERLAY_COLOR, Pedx.Handle, i, iColour, AddColour, 0);
            }
            //****************Clothing****************
            OnlineDress(Pedx, pFixtures.PedCloth);
            //****************Hair****************
            //int iHairStyle = pFixtures.iHairCut;

            //Function.Call(Hash.SET_PED_COMPONENT_VARIATION, Pedx.Handle, 2, iHairStyle, 0, 2);//hair

            int iHair = pFixtures.PFHair01;
            int iHair2 = pFixtures.PFHair02;

            Function.Call(Hash._SET_PED_HAIR_COLOR, Pedx.Handle, iHair, iHair2);
            //****************Tattoos****************
            for (int i = 0; i < pFixtures.TatBase.Count; i++)
                Function.Call(Hash._SET_PED_DECORATION, Pedx.Handle, Function.Call<int>(Hash.GET_HASH_KEY, pFixtures.TatBase[i]), Function.Call<int>(Hash.GET_HASH_KEY, pFixtures.TatName[i]));
        }
        public static void OnlineDress(Ped Pedx, ClothBank MyCloths)
        {
            LoggerLight.GetLogging("FreemodePed.OnlineSavedWard");

            Function.Call(Hash.CLEAR_ALL_PED_PROPS, Pedx.Handle);

            for (int i = 0; i < MyCloths.ClothA.Count; i++)
                Function.Call(Hash.SET_PED_COMPONENT_VARIATION, Pedx.Handle, i, MyCloths.ClothA[i], MyCloths.ClothB[i], 2);

            for (int i = 0; i < MyCloths.ExtraA.Count; i++)
                Function.Call(Hash.SET_PED_PROP_INDEX, Pedx.Handle, i, MyCloths.ExtraA[i], MyCloths.ExtraB[i], false);
        }
        public static List<Tattoo> AddRandTats(bool bMale)
        {
            LoggerLight.GetLogging("FreemodePed.AddRandTats");
            List<Tattoo> Tatlist = new List<Tattoo>();

            if (bMale)
            {
                Tatlist.Add(maleTats01[RandomNum.RandInt(0, maleTats01.Count - 1)]);
                Tatlist.Add(maleTats02[RandomNum.RandInt(0, maleTats02.Count - 1)]);
                Tatlist.Add(maleTats03[RandomNum.RandInt(0, maleTats03.Count - 1)]);
                Tatlist.Add(maleTats04[RandomNum.RandInt(0, maleTats04.Count - 1)]);
                Tatlist.Add(maleTats05[RandomNum.RandInt(0, maleTats05.Count - 1)]);
                Tatlist.Add(maleTats06[RandomNum.RandInt(0, maleTats06.Count - 1)]);
                Tatlist.Add(maleTats07[RandomNum.RandInt(0, maleTats07.Count - 1)]);
                Tatlist.Add(maleTats08[RandomNum.RandInt(0, maleTats08.Count - 1)]);
            }
            else
            {
                Tatlist.Add(femaleTats01[RandomNum.RandInt(0, femaleTats01.Count - 1)]);
                Tatlist.Add(femaleTats02[RandomNum.RandInt(0, femaleTats02.Count - 1)]);
                Tatlist.Add(femaleTats03[RandomNum.RandInt(0, femaleTats03.Count - 1)]);
                Tatlist.Add(femaleTats04[RandomNum.RandInt(0, femaleTats04.Count - 1)]);
                Tatlist.Add(femaleTats05[RandomNum.RandInt(0, femaleTats05.Count - 1)]);
                Tatlist.Add(femaleTats06[RandomNum.RandInt(0, femaleTats06.Count - 1)]);
                Tatlist.Add(femaleTats07[RandomNum.RandInt(0, femaleTats07.Count - 1)]);
                Tatlist.Add(femaleTats08[RandomNum.RandInt(0, femaleTats08.Count - 1)]);
            }

            return Tatlist;
        }
        public static ClothBank PickOutfit(bool bMale, int iSelecta)
        {
            LoggerLight.GetLogging("FreemodePed.PickOutfit");

            ClothBank myCB = new ClothBank();

            int iRand;
            if (bMale)
            {
                if (iSelecta == -1)
                    iRand = RandomNum.FindRandom(50, 0, 101);
                else 
                    iRand = iSelecta;

                if (iRand == 0)
                {
                    int iText = RandomNum.RandInt(0, 19);
                    myCB = new ClothBank
                    {
                        Title = "Body_Suit",
                        ClothA = new List<int> { 0, 134, 0, 3, 106, 0, 83, 0, 15, 0, 0, 274 },
                        ClothB = new List<int> { 0, iText, 0, 0, iText, 0, iText, 0, 0, 0, 0, iText },
                        ExtraA = new List<int> { -1, -1, -1, -1, -1 },
                        ExtraB = new List<int> { -1, -1, -1, -1, -1 }
                    };
                }
                else if (iRand == 1)
                {
                    int iText = RandomNum.RandInt(0, 11);
                    myCB = new ClothBank
                    {
                        Title = "Body_Suit",
                        ClothA = new List<int> { 0, 123, 0, 3, 95, 0, 68, 0, 15, 0, 0, 246 },
                        ClothB = new List<int> { 0, iText, 0, 0, iText, 0, iText, 0, 0, 0, 0, iText },
                        ExtraA = new List<int> { -1, -1, -1, -1, -1 },
                        ExtraB = new List<int> { -1, -1, -1, -1, -1 }
                    };
                }
                else if (iRand == 2)
                {
                    myCB = new ClothBank
                    {
                        Title = "Body_Suit",
                        ClothA = new List<int> { 0, 102, -1, 3, 85, 0, 58, 0, 15, 0, 0, 201 },
                        ClothB = new List<int> { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                        ExtraA = new List<int> { -1, -1, -1, -1, -1 },
                        ExtraB = new List<int> { -1, -1, -1, -1, -1 }
                    };
                }
                else if (iRand == 3)
                {
                    myCB = new ClothBank
                    {
                        Title = "M48",
                        ClothA = new List<int> { 0, 146, 0, 54, 33, 0, 25, 0, 15, 0, 0, 289 },
                        ClothB = new List<int> { 0, 10, 0, 0, 0, 0, 0, 0, 0, 0, 0, 11 },
                        ExtraA = new List<int> { -1, -1, -1, -1, -1 },
                        ExtraB = new List<int> { -1, -1, -1, -1, -1 }
                    };
                }
                else if (iRand == 4)
                {
                    myCB = new ClothBank
                    {
                        Title = "M49",
                        ClothA = new List<int> { 0, 142, 0, 123, 103, 0, 79, 0, 15, 0, 0, 158 },
                        ClothB = new List<int> { 0, 1, 0, 1, 3, 0, 0, 0, 0, 0, 0, 1 },
                        ExtraA = new List<int> { -1, -1, -1, -1, -1 },
                        ExtraB = new List<int> { -1, -1, -1, -1, -1 }
                    };
                }
                else if (iRand == 5)
                {
                    myCB = new ClothBank
                    {
                        Title = "M50",
                        ClothA = new List<int> { 0, 0, 0, 0, 42, 0, 32, 0, 15, 0, 0, 282 },
                        ClothB = new List<int> { 0, 0, 0, 0, 3, 0, 5, 0, 0, 0, 0, 7 },
                        ExtraA = new List<int> { 37, -1, -1, -1, -1 },
                        ExtraB = new List<int> { 0, -1, -1, -1, -1 }
                    };
                }
                else if (iRand == 6)
                {
                    myCB = new ClothBank
                    {
                        Title = "M51",
                        ClothA = new List<int> { 0, 138, 0, 17, 107, 0, 84, 0, 15, 0, 0, 275 },
                        ClothB = new List<int> { 0, 2, 0, 0, 3, 0, 0, 0, 0, 0, 0, 2 },
                        ExtraA = new List<int> { -1, -1, -1, -1, -1 },
                        ExtraB = new List<int> { -1, -1, -1, -1, -1 }
                    };
                }
                else if (iRand == 7)
                {
                    myCB = new ClothBank
                    {
                        Title = "M52",
                        ClothA = new List<int> { 0, 0, 0, 35, 72, 0, 50, 0, 15, 0, 0, 161 },
                        ClothB = new List<int> { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 3 },
                        ExtraA = new List<int> { -1, 8, -1, -1, -1 },
                        ExtraB = new List<int> { -1, 1, -1, -1, -1 }
                    };
                }
                else if (iRand == 8)
                {
                    myCB = new ClothBank
                    {
                        Title = "M53",
                        ClothA = new List<int> { 0, 137, 0, 33, 105, 0, 85, 0, 15, 0, 0, 276 },
                        ClothB = new List<int> { 0, 0, 0, 0, 9, 0, 13, 0, 0, 0, 0, 7 },
                        ExtraA = new List<int> { -1, -1, -1, -1, -1 },
                        ExtraB = new List<int> { -1, -1, -1, -1, -1 }
                    };
                }
                else if (iRand == 9)
                {
                    myCB = new ClothBank
                    {
                        Title = "M54",
                        ClothA = new List<int> { 0, 145, 0, 4, 16, 0, 57, 0, 15, 0, 0, 281 },
                        ClothB = new List<int> { 0, 0, 0, 0, 2, 0, 9, 0, 0, 0, 0, 9 },
                        ExtraA = new List<int> { -1, -1, -1, -1, -1 },
                        ExtraB = new List<int> { -1, -1, -1, -1, -1 }
                    };
                }
                else if (iRand == 10)
                {
                    myCB = new ClothBank
                    {
                        Title = "M55",
                        ClothA = new List<int> { 0, 0, 0, 4, 69, 0, 77, 0, 15, 0, 0, 279 },
                        ClothB = new List<int> { 0, 0, 0, 0, 10, 0, 19, 0, 0, 0, 0, 11 },
                        ExtraA = new List<int> { 132, -1, -1, -1, -1 },
                        ExtraB = new List<int> { 1, -1, -1, -1, -1 }
                    };
                }
                else if (iRand == 11)
                {
                    int iText = RandomNum.RandInt(0, 7);
                    myCB = new ClothBank
                    {
                        Title = "M11",
                        ClothA = new List<int> { 0, 0, 0, 166, 110, 0, 88, 0, 15, 0, 0, 283 },
                        ClothB = new List<int> { 0, 0, 0, iText, iText, 0, iText, 0, 0, 0, 0, iText },
                        ExtraA = new List<int> { 133, -1, -1, -1, -1 },
                        ExtraB = new List<int> { 7, -1, -1, -1, -1 }
                    };
                }
                else if (iRand == 12)
                {
                    myCB = new ClothBank
                    {
                        Title = "M19",
                        ClothA = new List<int> { 0, 147, 0, 17, 112, 0, 57, 0, 15, 0, 0, 285 },
                        ClothB = new List<int> { 0, 0, 0, 0, 0, 0, 10, 0, 0, 0, 0, 0 },
                        ExtraA = new List<int> { -1, 27, -1, -1, -1 },
                        ExtraB = new List<int> { -1, 0, -1, -1, -1 }
                    };
                }
                else if (iRand == 13)
                {
                    myCB = new ClothBank
                    {
                        Title = "M20",
                        ClothA = new List<int> { 0, 0, 0, 167, 113, 0, 90, 0, 15, 0, 0, 286 },
                        ClothB = new List<int> { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                        ExtraA = new List<int> { 134, -1, -1, -1, -1 },
                        ExtraB = new List<int> { 0, -1, -1, -1, -1 }
                    };
                }
                else if (iRand == 14)
                {
                    myCB = new ClothBank
                    {
                        Title = "M21",
                        ClothA = new List<int> { 0, 0, 0, 4, 111, 0, 89, 0, 143, 0, 61, 284 },
                        ClothB = new List<int> { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                        ExtraA = new List<int> { -1, -1, -1, -1, -1 },
                        ExtraB = new List<int> { -1, -1, -1, -1, -1 }
                    };
                }
                else if (iRand == 15)
                {
                    int iText = RandomNum.RandInt(0, 17);
                    myCB = new ClothBank
                    {
                        Title = "M22",
                        ClothA = new List<int> { 0, 0, 0, 165, 109, 0, 87, 0, 15, 0, 0, 278 },
                        ClothB = new List<int> { 0, 0, 0, iText, iText, 0, iText, 0, 0, 0, 0, iText },
                        ExtraA = new List<int> { 129, -1, -1, -1, -1 },
                        ExtraB = new List<int> { iText, -1, -1, -1, -1 }
                    };
                }
                else if (iRand == 16)
                {
                    int iText = RandomNum.RandInt(0, 11);
                    int iBod = RandomNum.RandInt(139, 141);
                    myCB = new ClothBank
                    {
                        Title = "Halloween",
                        ClothA = new List<int> { 0, iBod, 0, 164, 108, 0, 33, 0, 15, 0, 0, 277 },
                        ClothB = new List<int> { 0, iText, 0, iText, iText, 0, 0, 0, 0, 0, 0, iText },
                        ExtraA = new List<int> { -1, -1, -1, -1, -1 },
                        ExtraB = new List<int> { -1, -1, -1, -1, -1 }
                    };
                }
                else if (iRand == 17)
                {
                    int iText = RandomNum.RandInt(0, 13);
                    myCB = new ClothBank
                    {
                        Title = "M26",
                        ClothA = new List<int> { 0, 135, 0, 3, 114, 0, 78, 0, 15, 0, 0, 287 },
                        ClothB = new List<int> { 0, iText, 0, 0, iText, 0, iText, 0, 0, 0, 0, iText },
                        ExtraA = new List<int> { -1, -1, -1, -1, -1 },
                        ExtraB = new List<int> { -1, -1, -1, -1, -1 }
                    };
                }
                else if (iRand == 18)
                {
                    int iText = RandomNum.RandInt(0, 10);
                    myCB = new ClothBank
                    {
                        Title = "M27",
                        ClothA = new List<int> { 0, 0, -1, 17, 77, 0, 55, 0, 15, 0, 0, 178 },
                        ClothB = new List<int> { 0, 0, 0, 0, iText, 0, iText, 0, 0, 0, 0, iText },
                        ExtraA = new List<int> { 91, -1, -1, -1, -1 },
                        ExtraB = new List<int> { iText, -1, -1, -1, -1 }
                    };
                }
                else if (iRand == 19)
                {
                    int iText = RandomNum.RandInt(0, 9);
                    myCB = new ClothBank
                    {
                        Title = "M26",
                        ClothA = new List<int> { 0, 135, 0, 3, 114, 0, 78, 0, 15, 0, 0, 287 },
                        ClothB = new List<int> { 0, iText, 0, 0, iText, 0, iText, 0, 0, 0, 0, iText },
                        ExtraA = new List<int> { -1, -1, -1, -1, -1 },
                        ExtraB = new List<int> { -1, -1, -1, -1, -1 }
                    };
                }
                else if (iRand == 20)
                {
                    int iText = RandomNum.RandInt(0, 10);
                    myCB = new ClothBank
                    {
                        Title = "M29",
                        ClothA = new List<int> { 0, 0, -1, 15, 16, 0, 1, 16, 15, 0, 0, 15 },
                        ClothB = new List<int> { 0, 0, 0, 0, iText, 0, 7, 2, 0, 0, 0, 0 },
                        ExtraA = new List<int> { -1, -1, -1, -1, -1 },
                        ExtraB = new List<int> { -1, -1, -1, -1, -1 }
                    };
                }
                else if (iRand == 21)
                {
                    int iText = RandomNum.RandInt(0, 15);
                    int iText2 = RandomNum.RandInt(0, 5);
                    myCB = new ClothBank
                    {
                        Title = "M30",
                        ClothA = new List<int> { 0, 0, -1, 5, 15, 0, 16, 0, 15, 0, 0, 17 },
                        ClothB = new List<int> { 0, 0, 0, 0, iText, 0, 5, 0, 0, 0, 0, iText2 },
                        ExtraA = new List<int> { -1, -1, -1, -1, -1 },
                        ExtraB = new List<int> { -1, -1, -1, -1, -1 }
                    };
                }
                else if (iRand == 22)
                {
                    int iText = RandomNum.RandInt(0, 11);
                    int iText2 = RandomNum.RandInt(0, 2);
                    myCB = new ClothBank
                    {
                        Title = "M31",
                        ClothA = new List<int> { 0, 0, -1, 5, 18, 0, 16, 0, 15, 0, 0, 5 },
                        ClothB = new List<int> { 0, 0, 0, 0, iText, 0, 4, 0, 0, 0, 0, iText2 },
                        ExtraA = new List<int> { -1, -1, -1, -1, -1 },
                        ExtraB = new List<int> { -1, -1, -1, -1, -1 }
                    };
                }
                else if (iRand == 23)
                {
                    myCB = new ClothBank
                    {
                        Title = "M23",
                        ClothA = new List<int> { 0, 0, -1, 19, 72, 0, 53, 0, 82, 0, 0, 157 },
                        ClothB = new List<int> { 0, 0, 0, 0, 0, 0, 0, 0, 12, 0, 0, 0 },
                        ExtraA = new List<int> { 83, 24, -1, -1, -1 },
                        ExtraB = new List<int> { 0, 1, -1, -1, -1 }
                    };
                }
                else if (iRand == 24)
                {
                    myCB = new ClothBank
                    {
                        Title = "M24",
                        ClothA = new List<int> { 0, 0, -1, 14, 62, 0, 7, 0, 23, 0, 0, 167 },
                        ClothB = new List<int> { 0, 0, 0, 0, 2, 0, 15, 0, 2, 0, 0, 3 },
                        ExtraA = new List<int> { 76, 0, -1, -1, -1 },
                        ExtraB = new List<int> { 4, 0, -1, -1, -1 }
                    };
                }
                else if (iRand == 25)
                {
                    myCB = new ClothBank
                    {
                        Title = "M25",
                        ClothA = new List<int> { 0, 0, -1, 130, 73, 0, 56, 0, 15, 0, 0, 162 },
                        ClothB = new List<int> { 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 3 },
                        ExtraA = new List<int> { 83, 8, -1, -1, -1 },
                        ExtraB = new List<int> { 0, 6, -1, -1, -1 }
                    };
                }
                else if (iRand == 26)
                {
                    myCB = new ClothBank
                    {
                        Title = "M26",
                        ClothA = new List<int> { 0, 0, 10, 6, 4, 0, 23, 0, 15, 0, 0, 184 },
                        ClothB = new List<int> { 0, 0, 0, 0, 4, 0, 9, 0, 0, 0, 0, 1 },
                        ExtraA = new List<int> { 29, -1, -1, -1, -1 },
                        ExtraB = new List<int> { 0, -1, -1, -1, -1 }
                    };
                }
                else if (iRand == 27)
                {
                    myCB = new ClothBank
                    {
                        Title = "M27",
                        ClothA = new List<int> { 0, 0, -1, 6, 9, 0, 51, 0, 15, 0, 0, 165 },
                        ClothB = new List<int> { 0, 0, 0, 0, 11, 0, 0, 0, 0, 0, 0, 1 },
                        ExtraA = new List<int> { 83, 25, -1, -1, -1 },
                        ExtraB = new List<int> { 5, 7, -1, -1, -1 }
                    };
                }
                else if (iRand == 28)
                {
                    myCB = new ClothBank
                    {
                        Title = "M28",
                        ClothA = new List<int> { 0, 0, -1, 0, 69, 0, 7, 0, 83, 0, 0, 180 },
                        ClothB = new List<int> { 0, 0, 0, 0, 2, 0, 1, 0, 0, 0, 0, 2 },
                        ExtraA = new List<int> { 77, 25, -1, -1, -1 },
                        ExtraB = new List<int> { 18, 0, -1, -1, -1 }
                    };
                }
                else if (iRand == 29)
                {
                    myCB = new ClothBank
                    {
                        Title = "M29",
                        ClothA = new List<int> { 0, 0, -1, 116, 74, 0, 52, 0, 15, 0, 0, 173 },
                        ClothB = new List<int> { 0, 0, 0, 1, 4, 0, 0, 0, 0, 0, 0, 2 },
                        ExtraA = new List<int> { -1, 8, -1, -1, -1 },
                        ExtraB = new List<int> { -1, 1, -1, -1, -1 }
                    };
                }
                else if (iRand == 30)
                {
                    myCB = new ClothBank
                    {
                        Title = "M30",
                        ClothA = new List<int> { 0, 0, -1, 30, 72, 0, 50, 0, 83, 0, 0, 158 },
                        ClothB = new List<int> { 0, 0, 0, 1, 4, 0, 0, 0, 8, 0, 0, 1 },
                        ExtraA = new List<int> { -1, 24, -1, -1, -1 },
                        ExtraB = new List<int> { -1, 0, -1, -1, -1 }
                    };
                }
                else if (iRand == 31)
                {
                    myCB = new ClothBank
                    {
                        Title = "M31",
                        ClothA = new List<int> { 0, 0, -1, 24, 76, 0, 56, 0, 15, 0, 0, 174 },
                        ClothB = new List<int> { 0, 0, 0, 0, 3, 0, 0, 0, 0, 0, 0, 0 },
                        ExtraA = new List<int> { -1, 9, -1, -1, -1 },
                        ExtraB = new List<int> { -1, 0, -1, -1, -1 }
                    };
                }
                else if (iRand == 32)
                {
                    myCB = new ClothBank
                    {
                        Title = "M32",
                        ClothA = new List<int> { 0, 0, -1, 22, 4, 0, 53, 0, 75, 0, 0, 181 },
                        ClothB = new List<int> { 0, 0, 0, 0, 2, 0, 0, 0, 3, 0, 0, 0 },
                        ExtraA = new List<int> { -1, 0, -1, -1, -1 },
                        ExtraB = new List<int> { -1, 0, -1, -1, -1 }
                    };
                }
                else if (iRand == 33)
                {
                    myCB = new ClothBank
                    {
                        Title = "M33",
                        ClothA = new List<int> { 0, 0, -1, 8, 4, 0, 4, 0, 15, 0, 0, 38 },
                        ClothB = new List<int> { 0, 0, 0, 0, 4, 0, 1, 0, 0, 0, 0, 0 },
                        ExtraA = new List<int> { -1, -1, -1, -1, -1 },
                        ExtraB = new List<int> { -1, -1, -1, -1, -1 }
                    };
                }
                else if (iRand == 34)
                {
                    myCB = new ClothBank
                    {
                        Title = "M34",
                        ClothA = new List<int> { 0, 0, -1, 0, 0, 0, 1, 17, 15, 0, 0, 33 },
                        ClothB = new List<int> { 0, 0, 0, 0, 5, 0, 0, 0, 0, 0, 0, 0 },
                        ExtraA = new List<int> { -1, -1, -1, -1, -1 },
                        ExtraB = new List<int> { -1, -1, -1, -1, -1 }
                    };
                }
                else if (iRand == 35)
                {
                    myCB = new ClothBank
                    {
                        Title = "M35",
                        ClothA = new List<int> { 0, 0, -1, 12, 1, 0, 1, 0, 15, 0, 0, 41 },
                        ClothB = new List<int> { 0, 0, 0, 0, 14, 0, 4, 0, 0, 0, 0, 0 },
                        ExtraA = new List<int> { -1, -1, -1, -1, -1 },
                        ExtraB = new List<int> { -1, -1, -1, -1, -1 }
                    };
                }
                else if (iRand == 36)
                {
                    myCB = new ClothBank
                    {
                        Title = "M36",
                        ClothA = new List<int> { 0, 0, -1, 0, 0, 0, 0, 0, 15, 0, 0, 1 },
                        ClothB = new List<int> { 0, 0, 0, 0, 2, 0, 10, 0, 0, 0, 0, 0 },
                        ExtraA = new List<int> { -1, -1, -1, -1, -1 },
                        ExtraB = new List<int> { -1, -1, -1, -1, -1 }
                    };
                }
                else if (iRand == 37)
                {
                    myCB = new ClothBank
                    {
                        Title = "M37",
                        ClothA = new List<int> { 0, 0, -1, 0, 1, 0, 1, 0, 15, 0, 0, 22 },
                        ClothB = new List<int> { 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 1 },
                        ExtraA = new List<int> { -1, -1, -1, -1, -1 },
                        ExtraB = new List<int> { -1, -1, -1, -1, -1 }
                    };
                }
                else if (iRand == 38)
                {
                    myCB = new ClothBank
                    {
                        Title = "M38",
                        ClothA = new List<int> { 0, 0, -1, 4, 82, 0, 12, 15, 0, 0, 0, 192 },
                        ClothB = new List<int> { 0, 0, 0, 0, 6, 0, 3, 0, 1, 0, 0, 1 },
                        ExtraA = new List<int> { -1, 8, -1, -1, -1 },
                        ExtraB = new List<int> { -1, 2, -1, -1, -1 }
                    };
                }
                else if (iRand == 39)
                {
                    myCB = new ClothBank
                    {
                        Title = "M39",
                        ClothA = new List<int> { 0, 106, -1, 131, 87, 0, 62, 0, 15, 0, 0, 205 },
                        ClothB = new List<int> { 0, 25, 0, 0, 9, 0, 3, 0, 0, 0, 0, 0 },
                        ExtraA = new List<int> { -1, -1, -1, -1, -1 },
                        ExtraB = new List<int> { -1, -1, -1, -1, -1 }
                    };
                }
                else if (iRand == 40)
                {
                    myCB = new ClothBank
                    {
                        Title = "M40",
                        ClothA = new List<int> { 0, 0, -1, 4, 37, 0, 20, 38, 10, 0, 0, 142 },
                        ClothB = new List<int> { 0, 0, 0, 0, 2, 0, 5, 6, 2, 0, 0, 2 },
                        ExtraA = new List<int> { 29, -1, -1, -1, -1 },
                        ExtraB = new List<int> { 0, -1, -1, -1, -1 }
                    };
                }
                else if (iRand == 41)
                {
                    myCB = new ClothBank
                    {
                        Title = "M41",
                        ClothA = new List<int> { 0, 0, -1, 12, 37, 0, 20, 0, 73, 0, 0, 137 },
                        ClothB = new List<int> { 0, 0, 0, 0, 2, 0, 5, 0, 1, 0, 0, 0 },
                        ExtraA = new List<int> { -1, 17, -1, -1, -1 },
                        ExtraB = new List<int> { -1, 9, -1, -1, -1 }
                    };
                }
                else if (iRand == 42)
                {
                    int iText = RandomNum.RandInt(0, 9);
                    myCB = new ClothBank
                    {
                        Title = "M100",
                        ClothA = new List<int> { 0, 0, -1, 14, 68, 0, 48, 0, 15, 0, 0, 149 },
                        ClothB = new List<int> { 0, 0, 0, 0, iText, 0, 0, 0, 0, 0, 0, iText },
                        ExtraA = new List<int> { 78, -1, -1, -1, -1 },
                        ExtraB = new List<int> { iText, -1, -1, -1, -1 }
                    };
                }
                else if (iRand == 43)
                {
                    myCB = new ClothBank
                    {
                        Title = "M43",
                        ClothA = new List<int> { 0, 0, -1, 22, 92, 0, 24, 0, 15, 0, 0, 228 },
                        ClothB = new List<int> { 0, 0, 0, 0, 14, 0, 0, 0, 0, 0, 0, 14 },
                        ExtraA = new List<int> { 114, 5, -1, -1, -1 },
                        ExtraB = new List<int> { 10, 1, -1, -1, -1 }
                    };
                }
                else if (iRand == 44)
                {
                    myCB = new ClothBank
                    {
                        Title = "M44",
                        ClothA = new List<int> { 0, 0, -1, 12, 13, 0, 7, 0, 11, 0, 0, 35 },
                        ClothB = new List<int> { 0, 0, 0, 0, 0, 0, 15, 0, 2, 0, 0, 0 },
                        ExtraA = new List<int> { -1, -1, -1, -1, -1 },
                        ExtraB = new List<int> { -1, -1, -1, -1, -1 }
                    };
                }
                else if (iRand == 45)
                {
                    myCB = new ClothBank
                    {
                        Title = "M99",
                        ClothA = new List<int> { 0, 59, -1, 97, 0, 0, 1, 0, 15, 0, 0, 79 },
                        ClothB = new List<int> { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                        ExtraA = new List<int> { -1, -1, -1, -1, -1 },
                        ExtraB = new List<int> { -1, -1, -1, -1, -1 }
                    };
                }
                else if (iRand == 46)
                {
                    myCB = new ClothBank
                    {
                        Title = "M46",
                        ClothA = new List<int> { 0, 0, -1, 4, 37, 0, 15, 28, 31, 0, 0, 140 },
                        ClothB = new List<int> { 0, 0, 0, 0, 0, 0, 10, 1, 0, 0, 0, 2 },
                        ExtraA = new List<int> { -1, 17, -1, -1, -1 },
                        ExtraB = new List<int> { -1, 5, -1, -1, -1 }
                    };
                }
                else if (iRand == 47)
                {
                    myCB = new ClothBank
                    {
                        Title = "M47",
                        ClothA = new List<int> { 0, 0, -1, 4, 20, 0, 40, 11, 35, 0, 0, 27 },
                        ClothB = new List<int> { 0, 0, 0, 0, 2, 0, 9, 2, 0, 0, 0, 0 },
                        ExtraA = new List<int> { -1, -1, -1, -1, -1 },
                        ExtraB = new List<int> { -1, -1, -1, -1, -1 }
                    };
                }
                else if (iRand == 48)
                {
                    myCB = new ClothBank
                    {
                        Title = "M48",
                        ClothA = new List<int> { 0, 0, -1, 0, 0, 0, 2, 0, 15, 0, 0, 0 },
                        ClothB = new List<int> { 0, 0, 0, 0, 1, 0, 6, 0, 0, 0, 0, 1 },
                        ExtraA = new List<int> { -1, -1, 0, -1, -1 },
                        ExtraB = new List<int> { -1, -1, 0, -1, -1 }
                    };
                }
                else if (iRand == 49)
                {
                    myCB = new ClothBank
                    {
                        Title = "M49",
                        ClothA = new List<int> { 0, 0, -1, 4, 9, 0, 27, 0, 15, 0, 0, 138 },
                        ClothB = new List<int> { 0, 0, 0, 0, 14, 0, 0, 0, 0, 0, 0, 2 },
                        ExtraA = new List<int> { 6, 16, -1, -1, -1 },
                        ExtraB = new List<int> { 5, 2, -1, -1, -1 }
                    };
                }
                else if (iRand == 50)
                {
                    myCB = new ClothBank
                    {
                        Title = "M50",
                        ClothA = new List<int> { 0, 0, -1, 141, 87, 0, 60, 0, 15, 0, 0, 221 },
                        ClothB = new List<int> { 0, 0, 0, 16, 16, 0, 3, 0, 0, 0, 0, 9 },
                        ExtraA = new List<int> { 108, 5, -1, -1, -1 },
                        ExtraB = new List<int> { 16, 0, -1, -1, -1 }
                    };
                }
                else if (iRand == 51)
                {
                    myCB = new ClothBank
                    {
                        Title = "M51",
                        ClothA = new List<int> { 0, 0, -1, 145, 87, 0, 62, 0, 15, 0, 0, 222 },
                        ClothB = new List<int> { 0, 0, 0, 19, 17, 0, 7, 0, 0, 0, 0, 23 },
                        ExtraA = new List<int> { 107, 5, -1, -1, -1 },
                        ExtraB = new List<int> { 17, 5, -1, -1, -1 }
                    };
                }
                else if (iRand == 52)
                {
                    myCB = new ClothBank
                    {
                        Title = "M52",
                        ClothA = new List<int> { 0, 0, -1, 14, 56, 0, 37, 0, 15, 0, 0, 114 },
                        ClothB = new List<int> { 0, 0, 0, 0, 5, 0, 3, 0, 0, 0, 0, 5 },
                        ExtraA = new List<int> { 13, 8, -1, -1, -1 },
                        ExtraB = new List<int> { 2, 5, -1, -1, -1 }
                    };
                }
                else if (iRand == 53)
                {
                    myCB = new ClothBank
                    {
                        Title = "M53",
                        ClothA = new List<int> { 0, 0, -1, 6, 83, 0, 29, 89, 15, 0, 0, 190 },
                        ClothB = new List<int> { 0, 0, 0, 0, 3, 0, 0, 0, 0, 0, 0, 0 },
                        ExtraA = new List<int> { 96, -1, -1, -1, -1 },
                        ExtraB = new List<int> { 6, -1, -1, -1, -1 }
                    };
                }
                else if (iRand == 54)
                {
                    myCB = new ClothBank
                    {
                        Title = "M54",
                        ClothA = new List<int> { 0, 107, -1, 41, 89, 0, 61, 0, 15, 0, 0, 208 },
                        ClothB = new List<int> { 0, 18, 0, 0, 20, 0, 0, 0, 0, 0, 0, 11 },
                        ExtraA = new List<int> { 96, -1, -1, -1, -1 },
                        ExtraB = new List<int> { 0, -1, -1, -1, -1 }
                    };
                }
                else if (iRand == 55)
                {
                    myCB = new ClothBank
                    {
                        Title = "M55",
                        ClothA = new List<int> { 0, 0, -1, 35, 79, 0, 43, 0, 15, 0, 0, 204 },
                        ClothB = new List<int> { 0, 0, 0, 0, 0, 0, 3, 0, 0, 0, 0, 0 },
                        ExtraA = new List<int> { 96, 7, -1, -1, -1 },
                        ExtraB = new List<int> { 0, 0, -1, -1, -1 }
                    };
                }
                else if (iRand == 56)
                {
                    myCB = new ClothBank
                    {
                        Title = "M56",
                        ClothA = new List<int> { 0, 104, -1, 141, 87, 0, 60, 0, 15, 0, 0, 220 },
                        ClothB = new List<int> { 0, 25, 0, 3, 3, 0, 0, 0, 0, 0, 0, 20 },
                        ExtraA = new List<int> { -1, -1, -1, -1, -1 },
                        ExtraB = new List<int> { -1, -1, -1, -1, -1 }
                    };
                }
                else if (iRand == 57)
                {
                    myCB = new ClothBank
                    {
                        Title = "M57",
                        ClothA = new List<int> { 0, 0, -1, 4, 63, 0, 2, 0, 15, 0, 0, 139 },
                        ClothB = new List<int> { 0, 0, 0, 0, 0, 0, 13, 0, 0, 0, 0, 3 },
                        ExtraA = new List<int> { -1, 3, -1, -1, -1 },
                        ExtraB = new List<int> { -1, 9, -1, -1, -1 }
                    };
                }
                else if (iRand == 58)
                {
                    myCB = new ClothBank
                    {
                        Title = "M58",
                        ClothA = new List<int> { 0, 0, -1, 4, 60, 0, 36, 0, 72, 0, 0, 108 },
                        ClothB = new List<int> { 0, 0, 0, 0, 2, 0, 3, 0, 3, 0, 0, 4 },
                        ExtraA = new List<int> { -1, 13, -1, -1, -1 },
                        ExtraB = new List<int> { -1, 0, -1, -1, -1 }
                    };
                }
                else if (iRand == 59)
                {
                    myCB = new ClothBank
                    {
                        Title = "M59",
                        ClothA = new List<int> { 0, 0, -1, 23, 43, 0, 57, 0, 15, 4, 0, 5 },
                        ClothB = new List<int> { 0, 0, 0, 0, 0, 0, 6, 0, 0, 1, 0, 2 },
                        ExtraA = new List<int> { -1, -1, -1, -1, -1 },
                        ExtraB = new List<int> { -1, -1, -1, -1, -1 }
                    };
                }
                else if (iRand == 60)
                {
                    myCB = new ClothBank
                    {
                        Title = "M60",
                        ClothA = new List<int> { 0, 0, -1, 6, 7, 0, 28, 0, 15, 0, 0, 75 },
                        ClothB = new List<int> { 0, 0, 0, 0, 0, 0, 3, 0, 0, 0, 0, 10 },
                        ExtraA = new List<int> { 66, 18, -1, -1, -1 },
                        ExtraB = new List<int> { 0, 1, -1, -1, -1 }
                    };
                }
                else if (iRand == 61)
                {
                    myCB = new ClothBank
                    {
                        Title = "M61",
                        ClothA = new List<int> { 0, 0, -1, 37, 33, 0, 27, 0, 15, 10, 0, 222 },
                        ClothB = new List<int> { 0, 0, 0, 0, 0, 0, 0, 0, 0, 4, 0, 20 },
                        ExtraA = new List<int> { 112, -1, -1, -1, -1 },
                        ExtraB = new List<int> { 4, -1, -1, -1, -1 }
                    };
                }
                else if (iRand == 62)
                {
                    myCB = new ClothBank
                    {
                        Title = "M62",
                        ClothA = new List<int> { 0, 0, -1, 4, 78, 0, 57, 0, 0, 0, 0, 191 },
                        ClothB = new List<int> { 0, 0, 0, 0, 7, 0, 4, 0, 5, 0, 0, 0 },
                        ExtraA = new List<int> { 55, 13, -1, -1, -1 },
                        ExtraB = new List<int> { 19, 2, -1, -1, -1 }
                    };
                }
                else if (iRand == 63)
                {
                    myCB = new ClothBank
                    {
                        Title = "M63",
                        ClothA = new List<int> { 0, 0, -1, 0, 43, 0, 57, 51, 81, 0, 0, 170 },
                        ClothB = new List<int> { 0, 0, 0, 0, 1, 0, 6, 0, 2, 0, 0, 3 },
                        ExtraA = new List<int> { -1, 18, -1, -1, -1 },
                        ExtraB = new List<int> { -1, 3, -1, -1, -1 }
                    };
                }
                else if (iRand == 64)
                {
                    myCB = new ClothBank
                    {
                        Title = "M64",
                        ClothA = new List<int> { 0, 0, -1, 0, 82, 0, 57, 0, 15, 0, 0, 193 },
                        ClothB = new List<int> { 0, 0, 0, 0, 2, 0, 3, 0, 0, 0, 0, 5 },
                        ExtraA = new List<int> { 96, 7, -1, -1, -1 },
                        ExtraB = new List<int> { 1, 2, -1, -1, -1 }
                    };
                }
                else if (iRand == 65)
                {
                    myCB = new ClothBank
                    {
                        Title = "M65",
                        ClothA = new List<int> { 0, 0, -1, 141, 87, 0, 60, 0, 101, 0, 0, 212 },
                        ClothB = new List<int> { 0, 0, 0, 19, 6, 0, 7, 0, 6, 0, 0, 18 },
                        ExtraA = new List<int> { 105, 23, -1, -1, -1 },
                        ExtraB = new List<int> { 23, 9, -1, -1, -1 }
                    };
                }
                else if (iRand == 66)
                {
                    myCB = new ClothBank
                    {
                        Title = "M66",
                        ClothA = new List<int> { 0, 0, -1, 0, 43, 0, 57, 15, 15, 0, 0, 193 },
                        ClothB = new List<int> { 0, 0, 0, 0, 0, 0, 10, 0, 0, 0, 0, 22 },
                        ExtraA = new List<int> { 55, 2, 32, -1, -1 },
                        ExtraB = new List<int> { 1, 0, 0, -1, -1 }
                    };
                }
                else if (iRand == 67)
                {
                    myCB = new ClothBank
                    {
                        Title = "M67",
                        ClothA = new List<int> { 0, 0, -1, 4, 78, 0, 57, 123, 23, 0, 0, 191 },
                        ClothB = new List<int> { 0, 0, 0, 0, 6, 0, 9, 0, 0, 0, 0, 23 },
                        ExtraA = new List<int> { 96, 7, -1, -1, -1 },
                        ExtraB = new List<int> { 0, 3, -1, -1, -1 }
                    };
                }
                else if (iRand == 68)
                {
                    myCB = new ClothBank
                    {
                        Title = "M68",
                        ClothA = new List<int> { 0, 101, -1, 33, 78, 0, 57, 0, 71, 0, 0, 203 },
                        ClothB = new List<int> { 0, 13, 0, 0, 2, 0, 10, 0, 3, 0, 0, 25 },
                        ExtraA = new List<int> { 102, 13, -1, -1, -1 },
                        ExtraB = new List<int> { 7, 5, -1, -1, -1 }
                    };
                }
                else if (iRand == 69)
                {
                    myCB = new ClothBank
                    {
                        Title = "M69",
                        ClothA = new List<int> { 0, 0, -1, 6, 59, 0, 27, 85, 5, 0, 0, 35 },
                        ClothB = new List<int> { 0, 0, 0, 0, 7, 0, 0, 0, 2, 0, 0, 1 },
                        ExtraA = new List<int> { 55, 23, -1, -1, -1 },
                        ExtraB = new List<int> { 1, 9, -1, -1, -1 }
                    };
                }
                else if (iRand == 70)
                {
                    myCB = new ClothBank
                    {
                        Title = "M70",
                        ClothA = new List<int> { 0, 0, -1, 4, 28, 0, 20, 12, 10, 0, 0, 35 },
                        ClothB = new List<int> { 0, 0, 0, 0, 12, 0, 2, 2, 14, 0, 0, 4 },
                        ExtraA = new List<int> { -1, -1, -1, -1, -1 },
                        ExtraB = new List<int> { -1, -1, -1, -1, -1 }
                    };
                }
                else if (iRand == 71)
                {
                    myCB = new ClothBank
                    {
                        Title = "M71",
                        ClothA = new List<int> { 0, 0, -1, 0, 27, 0, 12, 0, 15, 0, 0, 22 },
                        ClothB = new List<int> { 0, 0, 0, 0, 6, 0, 15, 0, 0, 0, 0, 1 },
                        ExtraA = new List<int> { -1, -1, -1, -1, -1 },
                        ExtraB = new List<int> { -1, -1, -1, -1, -1 }
                    };
                }
                else if (iRand == 72)
                {
                    myCB = new ClothBank
                    {
                        Title = "M72",
                        ClothA = new List<int> { 0, 0, -1, 12, 27, 0, 23, 22, 26, 0, 0, 35 },
                        ClothB = new List<int> { 0, 0, 0, 0, 1, 0, 10, 6, 6, 0, 0, 2 },
                        ExtraA = new List<int> { -1, -1, -1, -1, -1 },
                        ExtraB = new List<int> { -1, -1, -1, -1, -1 }
                    };
                }
                else if (iRand == 73)
                {
                    myCB = new ClothBank
                    {
                        Title = "M73",
                        ClothA = new List<int> { 0, 0, -1, 14, 26, 0, 23, 0, 23, 0, 0, 24 },
                        ClothB = new List<int> { 0, 0, 0, 0, 10, 0, 8, 0, 0, 0, 0, 2 },
                        ExtraA = new List<int> { -1, -1, -1, -1, -1 },
                        ExtraB = new List<int> { -1, -1, -1, -1, -1 }
                    };
                }
                else if (iRand == 74)
                {
                    myCB = new ClothBank
                    {
                        Title = "Crimbo",
                        ClothA = new List<int> { 0, 0, -1, 1, 32, 0, 4, 34, 15, 0, 0, 52 },
                        ClothB = new List<int> { 0, 0, 0, 0, 3, 0, 0, 3, 0, 0, 0, 3 },
                        ExtraA = new List<int> { 43, -1, -1, -1, -1 },
                        ExtraB = new List<int> { 3, -1, -1, -1, -1 }
                    };
                }
                else if (iRand == 75)
                {
                    myCB = new ClothBank
                    {
                        Title = "Crimbo",
                        ClothA = new List<int> { 0, 0, -1, 1, 32, 0, 4, 34, 15, 0, 0, 52 },
                        ClothB = new List<int> { 0, 0, 0, 0, 0, 0, 2, 0, 0, 0, 0, 0 },
                        ExtraA = new List<int> { 40, -1, -1, -1, -1 },
                        ExtraB = new List<int> { 3, -1, -1, -1, -1 }
                    };
                }
                else if (iRand == 76)
                {
                    myCB = new ClothBank
                    {
                        Title = "Crimbo",
                        ClothA = new List<int> { 0, 0, -1, 1, 32, 0, 17, 0, 15, 0, 0, 52 },
                        ClothB = new List<int> { 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 1 },
                        ExtraA = new List<int> { 42, -1, -1, -1, -1 },
                        ExtraB = new List<int> { 0, -1, -1, -1, -1 }
                    };
                }
                else if (iRand == 77)
                {
                    myCB = new ClothBank
                    {
                        Title = "Crimbo",
                        ClothA = new List<int> { 0, 0, -1, 1, 3, 0, 4, 0, 15, 0, 0, 51 },
                        ClothB = new List<int> { 0, 0, 0, 0, 0, 0, 4, 0, 0, 0, 0, 0 },
                        ExtraA = new List<int> { 40, -1, -1, -1, -1 },
                        ExtraB = new List<int> { 6, -1, -1, -1, -1 }
                    };
                }
                else if (iRand == 78)
                {
                    myCB = new ClothBank
                    {
                        Title = "Crimbo",
                        ClothA = new List<int> { 0, 0, -1, 1, 26, 0, 17, 34, 15, 0, 0, 51 },
                        ClothB = new List<int> { 0, 0, 0, 0, 3, 0, 0, 0, 0, 0, 0, 1 },
                        ExtraA = new List<int> { 42, -1, -1, -1, -1 },
                        ExtraB = new List<int> { 2, -1, -1, -1, -1 }
                    };
                }
                else if (iRand == 79)
                {
                    myCB = new ClothBank
                    {
                        Title = "Crimbo",
                        ClothA = new List<int> { 0, 0, -1, 1, 26, 0, 12, 0, 15, 0, 0, 51 },
                        ClothB = new List<int> { 0, 0, 0, 0, 3, 0, 8, 0, 0, 0, 0, 2 },
                        ExtraA = new List<int> { 41, -1, -1, -1, -1 },
                        ExtraB = new List<int> { 0, -1, -1, -1, -1 }
                    };
                }
                else if (iRand == 80)
                {
                    myCB = new ClothBank
                    {
                        Title = "Crimbo",
                        ClothA = new List<int> { 0, 0, -1, 1, 32, 0, 4, 0, 15, 0, 0, 52 },
                        ClothB = new List<int> { 0, 0, 0, 0, 2, 0, 1, 0, 0, 0, 0, 2 },
                        ExtraA = new List<int> { 44, -1, -1, -1, -1 },
                        ExtraB = new List<int> { 1, -1, -1, -1, -1 }
                    };
                }
                else if (iRand == 81)
                {
                    myCB = new ClothBank
                    {
                        Title = "M81",
                        ClothA = new List<int> { 0, 0, -1, 14, 26, 0, 22, 0, 23, 0, 0, 35 },
                        ClothB = new List<int> { 0, 0, 0, 0, 8, 0, 11, 0, 0, 0, 0, 6 },
                        ExtraA = new List<int> { -1, -1, -1, -1, -1 },
                        ExtraB = new List<int> { -1, -1, -1, -1, -1 }
                    };
                }
                else if (iRand == 82)
                {
                    myCB = new ClothBank
                    {
                        Title = "M82",
                        ClothA = new List<int> { 0, 0, -1, 0, 27, 0, 1, 0, 15, 0, 0, 22 },
                        ClothB = new List<int> { 0, 0, 0, 0, 4, 0, 9, 0, 0, 0, 0, 0 },
                        ExtraA = new List<int> { -1, -1, -1, -1, -1 },
                        ExtraB = new List<int> { -1, -1, -1, -1, -1 }
                    };
                }
                else if (iRand == 83)
                {
                    myCB = new ClothBank
                    {
                        Title = "M83",
                        ClothA = new List<int> { 0, 0, -1, 0, 26, 0, 7, 0, 15, 0, 0, 44 },
                        ClothB = new List<int> { 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 3 },
                        ExtraA = new List<int> { -1, -1, -1, -1, -1 },
                        ExtraB = new List<int> { -1, -1, -1, -1, -1 }
                    };
                }
                else if (iRand == 84)
                {
                    myCB = new ClothBank
                    {
                        Title = "M84",
                        ClothA = new List<int> { 0, 0, -1, 12, 22, 0, 21, 21, 28, 0, 0, 24 },
                        ClothB = new List<int> { 0, 0, 0, 0, 5, 0, 10, 12, 13, 0, 0, 5 },
                        ExtraA = new List<int> { -1, -1, -1, -1, -1 },
                        ExtraB = new List<int> { -1, -1, -1, -1, -1 }
                    };
                }
                else if (iRand == 85)
                {
                    myCB = new ClothBank
                    {
                        Title = "M85",
                        ClothA = new List<int> { 0, 0, -1, 11, 25, 0, 21, 21, 15, 0, 0, 26 },
                        ClothB = new List<int> { 0, 0, 0, 0, 0, 0, 0, 11, 0, 0, 0, 2 },
                        ExtraA = new List<int> { -1, -1, -1, -1, -1 },
                        ExtraB = new List<int> { -1, -1, -1, -1, -1 }
                    };
                }
                else if (iRand == 86)
                {
                    myCB = new ClothBank
                    {
                        Title = "M86",
                        ClothA = new List<int> { 0, 0, -1, 12, 25, 0, 10, 0, 32, 0, 0, 31 },
                        ClothB = new List<int> { 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 1 },
                        ExtraA = new List<int> { -1, -1, -1, -1, -1 },
                        ExtraB = new List<int> { -1, -1, -1, -1, -1 }
                    };
                }
                else if (iRand == 87)
                {
                    myCB = new ClothBank
                    {
                        Title = "M87",
                        ClothA = new List<int> { 0, 0, 0, 8, 104, 0, 20, 129, 15, 0, 0, 272 },
                        ClothB = new List<int> { 0, 0, 0, 0, 0, 0, 3, 0, 0, 0, 0, 0 },
                        ExtraA = new List<int> { -1, -1, -1, -1, -1 },
                        ExtraB = new List<int> { -1, -1, -1, -1, -1 }
                    };
                }
                else if (iRand == 88)
                {
                    myCB = new ClothBank
                    {
                        Title = "M98",
                        ClothA = new List<int> { 0, 0, -1, 0, 35, 0, 25, 0, 58, 0, 0, 55 },
                        ClothB = new List<int> { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                        ExtraA = new List<int> { 46, 1, -1, -1, -1 },
                        ExtraB = new List<int> { 0, 1, -1, -1, -1 }
                    };
                }
                else if (iRand == 89)
                {
                    myCB = new ClothBank
                    {
                        Title = "M89",
                        ClothA = new List<int> { 0, 0, 0, 4, 116, 0, 21, 26, 146, 0, 0, 292 },
                        ClothB = new List<int> { 0, 0, 0, 0, 1, 0, 0, 2, 2, 0, 0, 2 },
                        ExtraA = new List<int> { -1, -1, -1, -1, -1 },
                        ExtraB = new List<int> { -1, -1, -1, -1, -1 }
                    };
                }
                else if (iRand == 90)
                {
                    myCB = new ClothBank
                    {
                        Title = "M90",
                        ClothA = new List<int> { 0, 148, 74, 168, 115, 0, 91, 0, 145, 0, 0, 291 },
                        ClothB = new List<int> { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                        ExtraA = new List<int> { -1, -1, -1, -1, -1 },
                        ExtraB = new List<int> { -1, -1, -1, -1, -1 }
                    };
                }
                else if (iRand == 91)
                {
                    myCB = new ClothBank
                    {
                        Title = "M91",
                        ClothA = new List<int> { 0, 0, 0, 11, 10, 0, 36, 0, 15, 0, 78, 319 },
                        ClothB = new List<int> { 0, 0, 0, 0, 0, 0, 3, 0, 0, 0, 0, 5 },
                        ExtraA = new List<int> { 149, -1, -1, -1, -1 },
                        ExtraB = new List<int> { 0, -1, -1, -1, -1 }
                    };
                }
                else if (iRand == 92)
                {
                    myCB = new ClothBank
                    {
                        Title = "M91",
                        ClothA = new List<int> { 0, 104, -1, 42, 84, 0, 33, 0, 97, 0, 0, 186 },
                        ClothB = new List<int> { 0, 25, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                        ExtraA = new List<int> { 39, -1, -1, -1, -1 },
                        ExtraB = new List<int> { 0, -1, -1, -1, -1 }
                    };
                }
                else if (iRand == 93)
                {
                    myCB = new ClothBank
                    {
                        Title = "M92",
                        ClothA = new List<int> { 0, 0, -1, 11, 31, 0, 24, 0, 15, 4, 0, 26 },
                        ClothB = new List<int> { 0, 0, 0, 0, 2, 0, 0, 0, 0, 1, 0, 2 },
                        ExtraA = new List<int> { -1, -1, -1, -1, -1 },
                        ExtraB = new List<int> { -1, -1, -1, -1, -1 }
                    };
                }
                else if (iRand == 94)
                {
                    myCB = new ClothBank
                    {
                        Title = "M93",
                        ClothA = new List<int> { 0, 0, -1, 85, 96, 0, 51, 127, 15, 0, 58, 250 },
                        ClothB = new List<int> { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0 },
                        ExtraA = new List<int> { 122, -1, -1, -1, -1 },
                        ExtraB = new List<int> { 0, -1, -1, -1, -1 }
                    };
                }
                else if (iRand == 95)
                {
                    myCB = new ClothBank
                    {
                        Title = "M94",
                        ClothA = new List<int> { 0, 0, -1, 85, 96, 0, 51, 127, 15, 0, 58, 250 },
                        ClothB = new List<int> { 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 1, 1 },
                        ExtraA = new List<int> { 122, -1, -1, -1, -1 },
                        ExtraB = new List<int> { 1, -1, -1, -1, -1 }
                    };
                }
                else if (iRand == 96)
                {
                    myCB = new ClothBank
                    {
                        Title = "M95",
                        ClothA = new List<int> { 0, 0, -1, 90, 96, 0, 51, 126, 15, 0, 57, 249 },
                        ClothB = new List<int> { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                        ExtraA = new List<int> { 122, -1, -1, -1, -1 },
                        ExtraB = new List<int> { 0, -1, -1, -1, -1 }
                    };
                }
                else if (iRand == 97)
                {
                    myCB = new ClothBank
                    {
                        Title = "M96",
                        ClothA = new List<int> { 0, 0, -1, 90, 96, 0, 51, 126, 15, 0, 57, 249 },
                        ClothB = new List<int> { 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 1 },
                        ExtraA = new List<int> { 122, -1, -1, -1, -1 },
                        ExtraB = new List<int> { 1, -1, -1, -1, -1 }
                    };
                }
                else if (iRand == 98)
                {
                    myCB = new ClothBank
                    {
                        Title = "M97",
                        ClothA = new List<int> { 0, 0, -1, 0, 35, 0, 25, 0, 58, 0, 0, 55 },
                        ClothB = new List<int> { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                        ExtraA = new List<int> { -1, -1, -1, -1, -1 },
                        ExtraB = new List<int> { -1, -1, -1, -1, -1 }
                    };
                }
                else if (iRand == 99)
                {
                    myCB = new ClothBank
                    {
                        Title = "Halloween",
                        ClothA = new List<int> { 0, 180, 0, 169, 127, 0, 33, 0, 15, 0, 0, 333 },
                        ClothB = new List<int> { 0, 12, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                        ExtraA = new List<int> { -1, -1, -1, -1, -1 },
                        ExtraB = new List<int> { -1, -1, -1, -1, -1 }
                    };
                }
                else if (iRand == 100)
                {
                    myCB = new ClothBank
                    {
                        Title = "Halloween",
                        ClothA = new List<int> { 0, 94, -1, 33, 79, 0, 52, 0, 15, 0, 0, 153 },
                        ClothB = new List<int> { 0, 4, 0, 0, 0, 0, 1, 0, 0, 0, 0, 7 },
                        ExtraA = new List<int> { -1, -1, -1, -1, -1 },
                        ExtraB = new List<int> { -1, -1, -1, -1, -1 }
                    };
                }
                else
                {
                    myCB = new ClothBank
                    {
                        Title = "Halloween",
                        ClothA = new List<int> { 0, 95, -1, 44, 39, 0, 27, 0, 15, 0, 0, 66 },
                        ClothB = new List<int> { 0, 7, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                        ExtraA = new List<int> { -1, -1, -1, -1, -1 },
                        ExtraB = new List<int> { -1, -1, -1, -1, -1 }
                    };
                }
            }
            else
            {
                if (iSelecta == -1)
                    iRand = RandomNum.FindRandom(51, 0, 117);
                else
                    iRand = iSelecta;

                if (iRand == 0)
                {
                    int iText = RandomNum.RandInt(0, 19);
                    myCB = new ClothBank
                    {
                        Title = "Body_Suit",
                        ClothA = new List<int> { 21, 134, 0, 8, 113, 0, 87, 0, 6, 0, 0, 287 },
                        ClothB = new List<int> { 0, iText, 0, 0, iText, 0, iText, 0, 0, 0, 0, iText },
                        ExtraA = new List<int> { -1, -1, -1, -1, -1 },
                        ExtraB = new List<int> { -1, -1, -1, -1, -1 }
                    };
                }
                else if (iRand == 1)
                {
                    int iText = RandomNum.RandInt(0, 11);
                    myCB = new ClothBank
                    {
                        Title = "Body_Suit",
                        ClothA = new List<int> { 0, 123, 0, -1, 98, 0, 71, 0, 14, 0, 0, 254 },
                        ClothB = new List<int> { 0, iText, 0, 0, iText, 0, iText, 0, 0, 0, 0, iText },
                        ExtraA = new List<int> { -1, -1, -1, -1, -1 },
                        ExtraB = new List<int> { -1, -1, -1, -1, -1 }
                    };
                }
                else if (iRand == 2)
                {
                    int iText = RandomNum.RandInt(0, 10);
                    myCB = new ClothBank
                    {
                        Title = "F2",
                        ClothA = new List<int> { 0, 0, -1, 18, 79, 0, 58, 0, 3, 0, 0, 180 },
                        ClothB = new List<int> { 0, 0, 0, 0, iText, 0, iText, 0, 0, 0, 0, iText },
                        ExtraA = new List<int> { 90, -1, -1, -1, -1 },
                        ExtraB = new List<int> { iText, -1, -1, -1, -1 }
                    };
                }
                else if (iRand == 3)
                {
                    myCB = new ClothBank
                    {
                        Title = "F3",
                        ClothA = new List<int> { 21, 0, 0, 3, 71, 0, 81, 0, 14, 0, 0, 292 },
                        ClothB = new List<int> { 0, 0, 0, 0, 10, 0, 19, 0, 0, 0, 0, 11 },
                        ExtraA = new List<int> { 131, -1, -1, -1, -1 },
                        ExtraB = new List<int> { 1, -1, -1, -1, -1 }
                    };
                }
                else if (iRand == 4)
                {
                    myCB = new ClothBank
                    {
                        Title = "F4",
                        ClothA = new List<int> { 21, 145, 0, 1, 16, 0, 60, 0, 2, 0, 0, 294 },
                        ClothB = new List<int> { 0, 0, 0, 0, 0, 0, 9, 0, 0, 0, 0, 9 },
                        ExtraA = new List<int> { -1, -1, -1, -1, -1 },
                        ExtraB = new List<int> { -1, -1, -1, -1, -1 }
                    };
                }
                else if (iRand == 5)
                {
                    myCB = new ClothBank
                    {
                        Title = "F5",
                        ClothA = new List<int> { 21, 137, 0, 36, 112, 0, 89, 0, 6, 0, 0, 289 },
                        ClothB = new List<int> { 0, 0, 0, 0, 9, 0, 3, 0, 0, 0, 0, 7 },
                        ExtraA = new List<int> { -1, -1, -1, -1, -1 },
                        ExtraB = new List<int> { -1, -1, -1, -1, -1 }
                    };
                }
                else if (iRand == 6)
                {
                    myCB = new ClothBank
                    {
                        Title = "F6",
                        ClothA = new List<int> { 21, 0, 0, 39, 75, 0, 51, 0, 15, 0, 0, 158 },
                        ClothB = new List<int> { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 3 },
                        ExtraA = new List<int> { -1, 11, -1, -1, -1 },
                        ExtraB = new List<int> { -1, 0, -1, -1, -1 }
                    };
                }
                else if (iRand == 7)
                {
                    myCB = new ClothBank
                    {
                        Title = "F7",
                        ClothA = new List<int> { 21, 138, 0, 18, 114, 0, 88, 0, 6, 0, 0, 288 },
                        ClothB = new List<int> { 0, 2, 0, 0, 9, 0, 0, 0, 0, 0, 0, 2 },
                        ExtraA = new List<int> { -1, -1, -1, -1, -1 },
                        ExtraB = new List<int> { -1, -1, -1, -1, -1 }
                    };
                }
                else if (iRand == 8)
                {
                    myCB = new ClothBank
                    {
                        Title = "F8",
                        ClothA = new List<int> { 21, 0, 0, 14, 2, 0, 11, 0, 2, 0, 0, 295 },
                        ClothB = new List<int> { 0, 0, 0, 0, 1, 0, 3, 0, 0, 0, 0, 7 },
                        ExtraA = new List<int> { 36, -1, -1, -1, -1 },
                        ExtraB = new List<int> { 0, -1, -1, -1, -1 }
                    };
                }
                else if (iRand == 9)
                {
                    myCB = new ClothBank
                    {
                        Title = "F9",
                        ClothA = new List<int> { 21, 142, 0, 140, 110, 0, 83, 0, 15, 0, 0, 155 },
                        ClothB = new List<int> { 0, 1, 0, 1, 3, 0, 0, 0, 0, 0, 0, 1 },
                        ExtraA = new List<int> { -1, -1, -1, -1, -1 },
                        ExtraB = new List<int> { -1, -1, -1, -1, -1 }
                    };
                }
                else if (iRand == 10)
                {
                    myCB = new ClothBank
                    {
                        Title = "F10",
                        ClothA = new List<int> { 21, 146, 0, 149, 32, 0, 25, 0, 3, 0, 0, 302 },
                        ClothB = new List<int> { 0, 10, 0, 0, 0, 0, 0, 0, 0, 0, 0, 11 },
                        ExtraA = new List<int> { -1, -1, -1, -1, -1 },
                        ExtraB = new List<int> { -1, -1, -1, -1, -1 }
                    };
                }
                else if (iRand == 11)
                {
                    int iText = RandomNum.RandInt(0, 11);
                    myCB = new ClothBank
                    {
                        Title = "F11",
                        ClothA = new List<int> { 21, 0, 0, 207, 117, 0, 92, 0, 6, 0, 0, 296 },
                        ClothB = new List<int> { 0, 0, 0, iText, iText, 0, iText, 0, 0, 0, 0, iText },
                        ExtraA = new List<int> { 132, -1, -1, -1, -1 },
                        ExtraB = new List<int> { iText, -1, -1, -1, -1 }
                    };
                }
                else if (iRand == 12)
                {
                    int iText = RandomNum.RandInt(0, 11);
                    int iBod = RandomNum.RandInt(63, 64);
                    myCB = new ClothBank
                    {
                        Title = "F117",
                        ClothA = new List<int> { 0, 0, 76, 15, iBod, 0, 41, 0, 3, 0, 0, 111 },
                        ClothB = new List<int> { 0, 0, 0, 0, iText, 0, iText, 0, 0, 0, 0, iText },
                        ExtraA = new List<int> { -1, -1, -1, -1, -1 },
                        ExtraB = new List<int> { -1, -1, -1, -1, -1 }
                    };
                }
                else if (iRand == 13)
                {
                    int iText = RandomNum.RandInt(0, 13);
                    myCB = new ClothBank
                    {
                        Title = "F13",
                        ClothA = new List<int> { 21, 135, 0, 8, 121, 0, 82, 0, 6, 0, 0, 300 },
                        ClothB = new List<int> { 0, iText, 0, 0, iText, 0, iText, 0, 0, 0, 0, iText },
                        ExtraA = new List<int> { -1, -1, -1, -1, -1 },
                        ExtraB = new List<int> { -1, -1, -1, -1, -1 }
                    };
                }
                else if (iRand == 14)
                {
                    int iText = RandomNum.RandInt(0, 9);
                    myCB = new ClothBank
                    {
                        Title = "F14",
                        ClothA = new List<int> { 0, 0, -1, 7, 70, 0, 49, 0, 14, 0, 0, 146 },
                        ClothB = new List<int> { 0, 0, 0, 0, iText, 0, 0, 0, 0, 0, 0, iText },
                        ExtraA = new List<int> { 77, -1, -1, -1, -1 },
                        ExtraB = new List<int> { iText, -1, -1, -1, -1 }
                    };
                }
                else if (iRand == 15)
                {
                    myCB = new ClothBank
                    {
                        Title = "F15",
                        ClothA = new List<int> { 0, 0, -1, 109, 99, 0, 52, 97, 3, 0, 66, 258 },
                        ClothB = new List<int> { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                        ExtraA = new List<int> { 121, -1, -1, -1, -1 },
                        ExtraB = new List<int> { 0, -1, -1, -1, -1 }
                    };
                }
                else if (iRand == 16)
                {
                    myCB = new ClothBank
                    {
                        Title = "F16",
                        ClothA = new List<int> { 0, 0, -1, 109, 99, 0, 52, 97, 3, 0, 66, 258 },
                        ClothB = new List<int> { 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 1 },
                        ExtraA = new List<int> { 121, -1, -1, -1, -1 },
                        ExtraB = new List<int> { 1, -1, -1, -1, -1 }
                    };
                }
                else if (iRand == 17)
                {
                    myCB = new ClothBank
                    {
                        Title = "F17",
                        ClothA = new List<int> { 0, 0, -1, 105, 99, 0, 52, 96, 3, 0, 65, 257 },
                        ClothB = new List<int> { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                        ExtraA = new List<int> { 121, -1, -1, -1, -1 },
                        ExtraB = new List<int> { 0, -1, -1, -1, -1 }
                    };
                }
                else if (iRand == 18)
                {
                    myCB = new ClothBank
                    {
                        Title = "F18",
                        ClothA = new List<int> { 0, 0, -1, 105, 99, 0, 52, 96, 3, 0, 65, 257 },
                        ClothB = new List<int> { 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 1 },
                        ExtraA = new List<int> { 121, -1, -1, -1, -1 },
                        ExtraB = new List<int> { 1, -1, -1, -1, -1 }
                    };
                }
                else if (iRand == 19)
                {
                    myCB = new ClothBank
                    {
                        Title = "F19",
                        ClothA = new List<int> { 0, 0, -1, 14, 34, 0, 25, 0, 35, 0, 0, 48 },
                        ClothB = new List<int> { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                        ExtraA = new List<int> { -1, -1, -1, -1, -1 },
                        ExtraB = new List<int> { -1, -1, -1, -1, -1 }
                    };
                }
                else if (iRand == 20)
                {
                    myCB = new ClothBank
                    {
                        Title = "F20",
                        ClothA = new List<int> { 0, 0, -1, 14, 34, 0, 25, 0, 35, 0, 0, 48 },
                        ClothB = new List<int> { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                        ExtraA = new List<int> { 45, 0, -1, -1, -1 },
                        ExtraB = new List<int> { 0, 0, -1, -1, -1 }
                    };
                }
                else if (iRand == 21)
                {
                    int iText = RandomNum.RandInt(0, 11);
                    int iText2 = RandomNum.RandInt(0, 8);
                    int iText3 = RandomNum.RandInt(0, 4);
                    myCB = new ClothBank
                    {
                        Title = "F21",
                        ClothA = new List<int> { 0, 0, -1, 11, 17, 0, 16, 11, 3, 0, 0, 36 },
                        ClothB = new List<int> { 0, 0, 0, 0, iText, 0, iText2, 2, 0, 0, 0, iText3 },
                        ExtraA = new List<int> { -1, -1, -1, -1, -1 },
                        ExtraB = new List<int> { -1, -1, -1, -1, -1 }
                    };
                }
                else if (iRand == 22)
                {
                    int iText = RandomNum.RandInt(0, 15);
                    int iText2 = RandomNum.RandInt(0, 11);
                    myCB = new ClothBank
                    {
                        Title = "F22",
                        ClothA = new List<int> { 0, 0, -1, 15, 12, 0, 3, 11, 3, 0, 0, 18 },
                        ClothB = new List<int> { 0, 0, 0, 0, iText, 0, iText, 1, 0, 0, 0, iText2 },
                        ExtraA = new List<int> { -1, -1, -1, -1, -1 },
                        ExtraB = new List<int> { -1, -1, -1, -1, -1 }
                    };
                }
                else if (iRand == 23)
                {
                    int iText = RandomNum.RandInt(0, 11);
                    int iText2 = RandomNum.RandInt(0, 15);
                    int iText3 = RandomNum.RandInt(0, 6);
                    myCB = new ClothBank
                    {
                        Title = "F23",
                        ClothA = new List<int> { 0, 0, -1, 5, 16, 0, 15, 10, 16, 0, 0, 31 },
                        ClothB = new List<int> { 0, 0, 0, 0, iText, 0, iText2, 0, 4, 0, 0, iText3 },
                        ExtraA = new List<int> { -1, -1, -1, -1, -1 },
                        ExtraB = new List<int> { -1, -1, -1, -1, -1 }
                    };
                }
                else if (iRand == 24)
                {
                    int iText = RandomNum.RandInt(0, 11);
                    int iText2 = RandomNum.RandInt(0, 11);
                    int iText3 = RandomNum.RandInt(0, 15);
                    myCB = new ClothBank
                    {
                        Title = "F24",
                        ClothA = new List<int> { 0, 0, -1, 11, 17, 0, 16, 3, 3, 0, 0, 11 },
                        ClothB = new List<int> { 0, 0, 0, 0, iText, 0, iText2, 1, 0, 0, 0, iText3 },
                        ExtraA = new List<int> { -1, -1, -1, -1, -1 },
                        ExtraB = new List<int> { -1, -1, -1, -1, -1 }
                    };
                }
                else if (iRand == 25)
                {
                    int iText = RandomNum.RandInt(0, 12);
                    int iText2 = RandomNum.RandInt(0, 11);
                    int iText3 = RandomNum.RandInt(0, 11);
                    myCB = new ClothBank
                    {
                        Title = "F25",
                        ClothA = new List<int> { 0, 0, -1, 15, 25, 0, 16, 1, 3, 0, 0, 18 },
                        ClothB = new List<int> { 0, 0, 0, 0, iText, 0, iText2, 2, 0, 0, 0, iText3 },
                        ExtraA = new List<int> { -1, -1, -1, -1, -1 },
                        ExtraB = new List<int> { -1, -1, -1, -1, -1 }
                    };
                }
                else if (iRand == 26)
                {
                    myCB = new ClothBank
                    {
                        Title = "F26",
                        ClothA = new List<int> { 0, 0, -1, 31, 78, 0, 54, 0, 86, 0, 0, 181 },
                        ClothB = new List<int> { 0, 0, 0, 0, 2, 0, 0, 0, 0, 0, 0, 0 },
                        ExtraA = new List<int> { 82, 26, -1, -1, -1 },
                        ExtraB = new List<int> { 0, 1, -1, -1, -1 }
                    };
                }
                else if (iRand == 27)
                {
                    myCB = new ClothBank
                    {
                        Title = "F27",
                        ClothA = new List<int> { 0, 0, -1, 3, 73, 0, 11, 0, 72, 0, 0, 164 },
                        ClothB = new List<int> { 0, 0, 0, 0, 3, 0, 3, 0, 2, 0, 0, 3 },
                        ExtraA = new List<int> { 75, -1, -1, -1, -1 },
                        ExtraB = new List<int> { 4, -1, -1, -1, -1 }
                    };
                }
                else if (iRand == 28)
                {
                    myCB = new ClothBank
                    {
                        Title = "F28",
                        ClothA = new List<int> { 0, 0, -1, 147, 76, 0, 53, 0, 15, 0, 0, 159 },
                        ClothB = new List<int> { 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 3 },
                        ExtraA = new List<int> { 82, 11, -1, -1, -1 },
                        ExtraB = new List<int> { 0, 3, -1, -1, -1 }
                    };
                }
                else if (iRand == 29)
                {
                    myCB = new ClothBank
                    {
                        Title = "F29",
                        ClothA = new List<int> { 0, 0, -1, 3, 0, 0, 13, 0, 3, 0, 15, 186 },
                        ClothB = new List<int> { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1 },
                        ExtraA = new List<int> { -1, -1, -1, -1, -1 },
                        ExtraB = new List<int> { -1, -1, -1, -1, -1 }
                    };
                }
                else if (iRand == 30)
                {
                    myCB = new ClothBank
                    {
                        Title = "F30",
                        ClothA = new List<int> { 0, 0, -1, 3, 11, 0, 51, 0, 3, 0, 0, 162 },
                        ClothB = new List<int> { 0, 0, 0, 0, 10, 0, 0, 0, 0, 0, 0, 1 },
                        ExtraA = new List<int> { 82, 27, -1, -1, -1 },
                        ExtraB = new List<int> { 5, 7, -1, -1, -1 }
                    };
                }
                else if (iRand == 31)
                {
                    myCB = new ClothBank
                    {
                        Title = "F31",
                        ClothA = new List<int> { 0, 0, -1, 4, 71, 0, 11, 0, 3, 0, 0, 171 },
                        ClothB = new List<int> { 0, 0, 0, 0, 2, 0, 2, 0, 0, 0, 0, 1 },
                        ExtraA = new List<int> { 76, 27, -1, -1, -1 },
                        ExtraB = new List<int> { 6, 6, -1, -1, -1 }
                    };
                }
                else if (iRand == 32)
                {
                    myCB = new ClothBank
                    {
                        Title = "F32",
                        ClothA = new List<int> { 0, 0, -1, 133, 77, 0, 53, 0, 17, 0, 0, 175 },
                        ClothB = new List<int> { 0, 0, 0, 1, 2, 0, 0, 0, 3, 0, 0, 2 },
                        ExtraA = new List<int> { -1, 11, -1, -1, -1 },
                        ExtraB = new List<int> { -1, 4, -1, -1, -1 }
                    };
                }
                else if (iRand == 33)
                {
                    myCB = new ClothBank
                    {
                        Title = "F33",
                        ClothA = new List<int> { 0, 0, -1, 24, 76, 0, 51, 0, 3, 0, 0, 173 },
                        ClothB = new List<int> { 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0 },
                        ExtraA = new List<int> { -1, 26, -1, -1, -1 },
                        ExtraB = new List<int> { -1, 0, -1, -1, -1 }
                    };
                }
                else if (iRand == 34)
                {
                    myCB = new ClothBank
                    {
                        Title = "F34",
                        ClothA = new List<int> { 0, 0, -1, 27, 74, 0, 51, 0, 3, 0, 0, 176 },
                        ClothB = new List<int> { 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0 },
                        ExtraA = new List<int> { -1, 10, -1, -1, -1 },
                        ExtraB = new List<int> { -1, 1, -1, -1, -1 }
                    };
                }
                else if (iRand == 35)
                {
                    myCB = new ClothBank
                    {
                        Title = "F35",
                        ClothA = new List<int> { 0, 0, -1, 23, 0, 0, 56, 0, 77, 0, 0, 163 },
                        ClothB = new List<int> { 0, 0, 0, 0, 8, 0, 0, 0, 3, 0, 0, 0 },
                        ExtraA = new List<int> { -1, -1, -1, -1, -1 },
                        ExtraB = new List<int> { -1, -1, -1, -1, -1 }
                    };
                }
                else if (iRand == 36)
                {
                    myCB = new ClothBank
                    {
                        Title = "F36",
                        ClothA = new List<int> { 0, 0, -1, 0, 16, 0, 2, 2, 3, 0, 0, 0 },
                        ClothB = new List<int> { 0, 0, 0, 0, 4, 0, 5, 1, 0, 0, 0, 11 },
                        ExtraA = new List<int> { -1, -1, -1, -1, -1 },
                        ExtraB = new List<int> { -1, -1, -1, -1, -1 }
                    };
                }
                else if (iRand == 37)
                {
                    myCB = new ClothBank
                    {
                        Title = "F37",
                        ClothA = new List<int> { 0, 0, -1, 2, 2, 0, 2, 5, 3, 0, 0, 2 },
                        ClothB = new List<int> { 0, 0, 0, 0, 2, 0, 0, 4, 0, 0, 0, 6 },
                        ExtraA = new List<int> { -1, -1, -1, -1, -1 },
                        ExtraB = new List<int> { -1, -1, -1, -1, -1 }
                    };
                }
                else if (iRand == 38)
                {
                    myCB = new ClothBank
                    {
                        Title = "F38",
                        ClothA = new List<int> { 0, 0, -1, 9, 4, 0, 13, 1, 3, 0, 0, 9 },
                        ClothB = new List<int> { 0, 0, 0, 0, 9, 0, 12, 2, 0, 0, 0, 9 },
                        ExtraA = new List<int> { -1, -1, -1, -1, -1 },
                        ExtraB = new List<int> { -1, -1, -1, -1, -1 }
                    };
                }
                else if (iRand == 39)
                {
                    myCB = new ClothBank
                    {
                        Title = "F39",
                        ClothA = new List<int> { 0, 0, -1, 3, 2, 0, 16, 2, 3, 0, 0, 3 },
                        ClothB = new List<int> { 0, 0, 0, 0, 0, 0, 6, 1, 0, 0, 0, 11 },
                        ExtraA = new List<int> { -1, -1, -1, -1, -1 },
                        ExtraB = new List<int> { -1, -1, -1, -1, -1 }
                    };
                }
                else if (iRand == 40)
                {
                    myCB = new ClothBank
                    {
                        Title = "F40",
                        ClothA = new List<int> { 0, 0, -1, 2, 3, 0, 16, 1, 3, 0, 0, 2 },
                        ClothB = new List<int> { 0, 0, 0, 0, 7, 0, 11, 0, 0, 0, 0, 15 },
                        ExtraA = new List<int> { -1, -1, -1, -1, -1 },
                        ExtraB = new List<int> { -1, -1, -1, -1, -1 }
                    };
                }
                else if (iRand == 41)
                {
                    myCB = new ClothBank
                    {
                        Title = "F41",
                        ClothA = new List<int> { 0, 0, -1, 3, 3, 0, 16, 1, 3, 0, 0, 3 },
                        ClothB = new List<int> { 0, 0, 0, 0, 11, 0, 1, 3, 0, 0, 0, 10 },
                        ExtraA = new List<int> { -1, -1, -1, -1, -1 },
                        ExtraB = new List<int> { -1, -1, -1, -1, -1 }
                    };
                }
                else if (iRand == 42)
                {
                    int iText = RandomNum.RandInt(0, 20);
                    int iText2 = 0;
                    if (iText == 1)
                        iText2 = 1;
                    else if (iText == 2 || iText == 6)
                        iText2 = 2;
                    else if (iText == 3)
                        iText2 = 3;
                    else if (iText == 5)
                        iText2 = 4;
                    myCB = new ClothBank
                    {
                        Title = "F42",
                        ClothA = new List<int> { 21, 0, 0, 7, 102, 0, 77, 0, 14, 0, 0, 262 },
                        ClothB = new List<int> { 0, 0, 0, 0, iText, 0, iText2, 0, 0, 0, 0, iText },
                        ExtraA = new List<int> { -1, -1, -1, -1, -1 },
                        ExtraB = new List<int> { -1, -1, -1, -1, -1 }
                    };
                }
                else if (iRand == 43)
                {
                    myCB = new ClothBank
                    {
                        Title = "F43",
                        ClothA = new List<int> { 0, 0, -1, 3, 84, 0, 30, 0, 51, 0, 0, 194 },
                        ClothB = new List<int> { 0, 0, 0, 0, 9, 0, 0, 0, 0, 0, 0, 1 },
                        ExtraA = new List<int> { -1, 11, -1, -1, -1 },
                        ExtraB = new List<int> { -1, 3, -1, -1, -1 }
                    };
                }
                else if (iRand == 44)
                {
                    myCB = new ClothBank
                    {
                        Title = "F44",
                        ClothA = new List<int> { 0, 106, -1, 147, 90, 0, 65, 0, 15, 0, 0, 207 },
                        ClothB = new List<int> { 0, 25, 0, 0, 9, 0, 3, 0, 0, 0, 0, 0 },
                        ExtraA = new List<int> { -1, -1, -1, -1, -1 },
                        ExtraB = new List<int> { -1, -1, -1, -1, -1 }
                    };
                }
                else if (iRand == 45)
                {
                    myCB = new ClothBank
                    {
                        Title = "F45",
                        ClothA = new List<int> { 0, 0, -1, 3, 41, 0, 29, 20, 39, 0, 0, 97 },
                        ClothB = new List<int> { 0, 0, 0, 0, 2, 0, 2, 5, 4, 0, 0, 0 },
                        ExtraA = new List<int> { -1, -1, -1, -1, -1 },
                        ExtraB = new List<int> { -1, -1, -1, -1, -1 }
                    };
                }
                else if (iRand == 46)
                {
                    myCB = new ClothBank
                    {
                        Title = "F46",
                        ClothA = new List<int> { 0, 0, -1, 7, 54, 0, 0, 22, 38, 0, 0, 139 },
                        ClothB = new List<int> { 0, 0, 0, 0, 2, 0, 1, 6, 2, 0, 0, 2 },
                        ExtraA = new List<int> { 28, -1, -1, -1, -1 },
                        ExtraB = new List<int> { 4, -1, -1, -1, -1 }
                    };
                }
                else if (iRand == 47)
                {
                    myCB = new ClothBank
                    {
                        Title = "F47",
                        ClothA = new List<int> { 0, 0, -1, 36, 41, 0, 29, 0, 67, 0, 0, 107 },
                        ClothB = new List<int> { 0, 0, 0, 0, 2, 0, 0, 0, 4, 0, 0, 0 },
                        ExtraA = new List<int> { -1, 10, -1, -1, -1 },
                        ExtraB = new List<int> { -1, 1, -1, -1, -1 }
                    };
                }
                else if (iRand == 48)
                {
                    myCB = new ClothBank
                    {
                        Title = "F48",
                        ClothA = new List<int> { 0, 0, -1, 3, 24, 0, 19, 0, 75, 0, 0, 134 },
                        ClothB = new List<int> { 0, 0, 0, 0, 3, 0, 9, 0, 1, 0, 0, 0 },
                        ExtraA = new List<int> { -1, 14, -1, -1, -1 },
                        ExtraB = new List<int> { -1, 0, -1, -1, -1 }
                    };
                }
                else if (iRand == 49)
                {
                    myCB = new ClothBank
                    {
                        Title = "F116",
                        ClothA = new List<int> { 0, 0, -1, 4, 30, 0, 24, 5, 3, 0, 0, 4 },
                        ClothB = new List<int> { 0, 0, 0, 0, 1, 0, 0, 4, 0, 0, 0, 14 },
                        ExtraA = new List<int> { -1, -1, -1, -1, -1 },
                        ExtraB = new List<int> { -1, -1, -1, -1, -1 }
                    };
                }
                else if (iRand == 50)
                {
                    myCB = new ClothBank
                    {
                        Title = "F50",
                        ClothA = new List<int> { 0, 0, -1, 23, 95, 0, 24, 0, 14, 0, 0, 238 },
                        ClothB = new List<int> { 0, 0, 0, 0, 14, 0, 0, 0, 0, 0, 0, 14 },
                        ExtraA = new List<int> { 113, 11, -1, -1, -1 },
                        ExtraB = new List<int> { 10, 1, -1, -1, -1 }
                    };
                }
                else if (iRand == 51)
                {
                    myCB = new ClothBank
                    {
                        Title = "F51",
                        ClothA = new List<int> { 0, 0, -1, 7, 27, 0, 11, 0, 39, 0, 0, 66 },
                        ClothB = new List<int> { 0, 0, 0, 0, 0, 0, 2, 0, 2, 0, 0, 0 },
                        ExtraA = new List<int> { -1, -1, -1, -1, -1 },
                        ExtraB = new List<int> { -1, -1, -1, -1, -1 }
                    };
                }
                else if (iRand == 52)
                {
                    myCB = new ClothBank
                    {
                        Title = "F115",
                        ClothA = new List<int> { 0, 0, -1, 4, 3, 0, 4, 1, 3, 0, 0, 32 },
                        ClothB = new List<int> { 0, 0, 0, 0, 8, 0, 2, 0, 0, 0, 0, 0 },
                        ExtraA = new List<int> { -1, -1, -1, -1, -1 },
                        ExtraB = new List<int> { -1, -1, -1, -1, -1 }
                    };
                }
                else if (iRand == 53)
                {
                    myCB = new ClothBank
                    {
                        Title = "F53",
                        ClothA = new List<int> { 0, 0, -1, 7, 64, 0, 0, 0, 13, 0, 0, 137 },
                        ClothB = new List<int> { 0, 0, 0, 0, 3, 0, 0, 0, 6, 0, 0, 2 },
                        ExtraA = new List<int> { -1, 18, -1, -1, -1 },
                        ExtraB = new List<int> { -1, 0, -1, -1, -1 }
                    };
                }
                else if (iRand == 54)
                {
                    myCB = new ClothBank
                    {
                        Title = "F54",
                        ClothA = new List<int> { 0, 0, -1, 9, 0, 0, 38, 0, 2, 0, 0, 96 },
                        ClothB = new List<int> { 0, 0, 0, 0, 10, 0, 2, 0, 0, 0, 0, 0 },
                        ExtraA = new List<int> { -1, 11, -1, -1, -1 },
                        ExtraB = new List<int> { -1, 2, -1, -1, -1 }
                    };
                }
                else if (iRand == 55)
                {
                    myCB = new ClothBank
                    {
                        Title = "F55",
                        ClothA = new List<int> { 0, 0, -1, 3, 0, 0, 30, 0, 2, 18, 0, 103 },
                        ClothB = new List<int> { 0, 0, 0, 0, 0, 0, 0, 0, 0, 2, 0, 1 },
                        ExtraA = new List<int> { 6, -1, -1, -1, -1 },
                        ExtraB = new List<int> { 0, -1, -1, -1, -1 }
                    };
                }
                else if (iRand == 56)
                {
                    myCB = new ClothBank
                    {
                        Title = "F56",
                        ClothA = new List<int> { 0, 0, -1, 3, 43, 0, 4, 84, 65, 0, 0, 100 },
                        ClothB = new List<int> { 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0 },
                        ExtraA = new List<int> { 55, -1, -1, -1, -1 },
                        ExtraB = new List<int> { 24, -1, -1, -1, -1 }
                    };
                }
                else if (iRand == 57)
                {
                    myCB = new ClothBank
                    {
                        Title = "F57",
                        ClothA = new List<int> { 0, 0, -1, 3, 64, 0, 6, 23, 41, 0, 0, 58 },
                        ClothB = new List<int> { 0, 0, 0, 0, 1, 0, 0, 0, 2, 0, 0, 0 },
                        ExtraA = new List<int> { -1, -1, -1, -1, -1 },
                        ExtraB = new List<int> { -1, -1, -1, -1, -1 }
                    };
                }
                else if (iRand == 58)
                {
                    myCB = new ClothBank
                    {
                        Title = "F58",
                        ClothA = new List<int> { 0, 0, -1, 2, 0, 0, 10, 0, 2, 0, 0, 2 },
                        ClothB = new List<int> { 0, 0, 0, 0, 0, 0, 3, 0, 0, 0, 0, 1 },
                        ExtraA = new List<int> { -1, -1, 0, -1, -1 },
                        ExtraB = new List<int> { -1, -1, 0, -1, -1 }
                    };
                }
                else if (iRand == 59)
                {
                    myCB = new ClothBank
                    {
                        Title = "F59",
                        ClothA = new List<int> { 0, 0, -1, 3, 11, 0, 30, 0, 3, 0, 0, 135 },
                        ClothB = new List<int> { 0, 0, 0, 0, 10, 0, 0, 0, 0, 0, 0, 2 },
                        ExtraA = new List<int> { 14, 0, -1, -1, -1 },
                        ExtraB = new List<int> { 0, 4, -1, -1, -1 }
                    };
                }
                else if (iRand == 60)
                {
                    myCB = new ClothBank
                    {
                        Title = "F60",
                        ClothA = new List<int> { 0, 0, -1, 174, 90, 0, 63, 0, 3, 0, 0, 231 },
                        ClothB = new List<int> { 0, 0, 0, 16, 16, 0, 3, 0, 0, 0, 0, 9 },
                        ExtraA = new List<int> { 107, 11, -1, -1, -1 },
                        ExtraB = new List<int> { 16, 1, -1, -1, -1 }
                    };
                }
                else if (iRand == 61)
                {
                    myCB = new ClothBank
                    {
                        Title = "F61",
                        ClothA = new List<int> { 0, 0, -1, 180, 90, 0, 65, 0, 14, 0, 0, 232 },
                        ClothB = new List<int> { 0, 0, 0, 19, 17, 0, 7, 0, 0, 0, 0, 23 },
                        ExtraA = new List<int> { 106, 11, -1, -1, -1 },
                        ExtraB = new List<int> { 17, 6, -1, -1, -1 }
                    };
                }
                else if (iRand == 62)
                {
                    myCB = new ClothBank
                    {
                        Title = "F62",
                        ClothA = new List<int> { 0, 0, -1, 0, 57, 0, 38, 0, 3, 0, 0, 105 },
                        ClothB = new List<int> { 0, 0, 0, 0, 5, 0, 3, 0, 0, 0, 0, 5 },
                        ExtraA = new List<int> { 2, 11, -1, -1, -1 },
                        ExtraB = new List<int> { 1, 3, -1, -1, -1 }
                    };
                }
                else if (iRand == 63)
                {
                    myCB = new ClothBank
                    {
                        Title = "F63",
                        ClothA = new List<int> { 0, 0, -1, 0, 85, 0, 31, 67, 3, 0, 0, 192 },
                        ClothB = new List<int> { 0, 0, 0, 0, 3, 0, 0, 0, 0, 0, 0, 0 },
                        ExtraA = new List<int> { 95, -1, 13, -1, -1 },
                        ExtraB = new List<int> { 6, -1, 0, -1, -1 }
                    };
                }
                else if (iRand == 64)
                {
                    myCB = new ClothBank
                    {
                        Title = "F64",
                        ClothA = new List<int> { 0, 107, -1, 55, 92, 0, 62, 0, 2, 0, 0, 226 },
                        ClothB = new List<int> { 0, 18, 0, 0, 20, 0, 20, 0, 0, 0, 0, 11 },
                        ExtraA = new List<int> { 95, -1, -1, -1, -1 },
                        ExtraB = new List<int> { 0, -1, -1, -1, -1 }
                    };
                }
                else if (iRand == 65)
                {
                    myCB = new ClothBank
                    {
                        Title = "F65",
                        ClothA = new List<int> { 0, 0, -1, 3, 50, 0, 37, 0, 66, 0, 0, 104 },
                        ClothB = new List<int> { 0, 0, 0, 0, 0, 0, 3, 0, 5, 0, 0, 0 },
                        ExtraA = new List<int> { -1, 2, -1, -1, -1 },
                        ExtraB = new List<int> { -1, 0, -1, -1, -1 }
                    };
                }
                else if (iRand == 66)
                {
                    myCB = new ClothBank
                    {
                        Title = "F66",
                        ClothA = new List<int> { 0, 0, -1, 40, 85, 0, 7, 0, 3, 0, 0, 206 },
                        ClothB = new List<int> { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                        ExtraA = new List<int> { 95, 24, -1, -1, -1 },
                        ExtraB = new List<int> { 0, 0, -1, -1, -1 }
                    };
                }
                else if (iRand == 67)
                {
                    myCB = new ClothBank
                    {
                        Title = "F67",
                        ClothA = new List<int> { 0, 0, -1, 7, 0, 0, 3, 85, 55, 0, 0, 66 },
                        ClothB = new List<int> { 0, 0, 0, 0, 0, 0, 0, 0, 2, 0, 0, 0 },
                        ExtraA = new List<int> { 58, -1, -1, -1, -1 },
                        ExtraB = new List<int> { 2, -1, -1, -1, -1 }
                    };
                }
                else if (iRand == 68)
                {
                    myCB = new ClothBank
                    {
                        Title = "F68",
                        ClothA = new List<int> { 0, 0, -1, 3, 51, 0, 37, 0, 3, 0, 0, 102 },
                        ClothB = new List<int> { 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0 },
                        ExtraA = new List<int> { -1, 16, -1, -1, -1 },
                        ExtraB = new List<int> { -1, 6, -1, -1, -1 }
                    };
                }
                else if (iRand == 69)
                {
                    myCB = new ClothBank
                    {
                        Title = "F69",
                        ClothA = new List<int> { 0, 104, -1, 174, 90, 0, 63, 0, 14, 0, 0, 230 },
                        ClothB = new List<int> { 0, 25, 0, 3, 3, 0, 0, 0, 0, 0, 0, 20 },
                        ExtraA = new List<int> { -1, -1, -1, -1, -1 },
                        ExtraB = new List<int> { -1, -1, -1, -1, -1 }
                    };
                }
                else if (iRand == 70)
                {
                    myCB = new ClothBank
                    {
                        Title = "F70",
                        ClothA = new List<int> { 0, 0, -1, 36, 37, 0, 29, 0, 39, 0, 0, 65 },
                        ClothB = new List<int> { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 10 },
                        ExtraA = new List<int> { -1, -1, -1, -1, -1 },
                        ExtraB = new List<int> { -1, -1, -1, -1, -1 }
                    };
                }
                else if (iRand == 71)
                {
                    myCB = new ClothBank
                    {
                        Title = "F71",
                        ClothA = new List<int> { 0, 0, -1, 3, 37, 0, 29, 22, 38, 0, 0, 7 },
                        ClothB = new List<int> { 0, 0, 0, 0, 0, 0, 0, 6, 4, 0, 0, 0 },
                        ExtraA = new List<int> { -1, -1, -1, -1, -1 },
                        ExtraB = new List<int> { -1, -1, -1, -1, -1 }
                    };
                }
                else if (iRand == 72)
                {
                    myCB = new ClothBank
                    {
                        Title = "F72",
                        ClothA = new List<int> { 0, 0, -1, 3, 1, 0, 10, 0, 2, 0, 0, 136 },
                        ClothB = new List<int> { 0, 0, 0, 0, 8, 0, 2, 0, 0, 0, 0, 3 },
                        ExtraA = new List<int> { -1, 19, -1, -1, -1 },
                        ExtraB = new List<int> { -1, 9, -1, -1, -1 }
                    };
                }
                else if (iRand == 73)
                {
                    myCB = new ClothBank
                    {
                        Title = "F73",
                        ClothA = new List<int> { 0, 0, -1, 3, 63, 0, 41, 0, 76, 0, 0, 99 },
                        ClothB = new List<int> { 0, 0, 0, 0, 2, 0, 2, 0, 3, 0, 0, 2 },
                        ExtraA = new List<int> { -1, 2, -1, -1, -1 },
                        ExtraB = new List<int> { -1, 0, -1, -1, -1 }
                    };
                }
                else if (iRand == 74)
                {
                    myCB = new ClothBank
                    {
                        Title = "F74",
                        ClothA = new List<int> { 0, 0, -1, 31, 0, 0, 60, 0, 3, 1, 0, 73 },
                        ClothB = new List<int> { 0, 0, 0, 0, 0, 0, 6, 0, 0, 1, 0, 1 },
                        ExtraA = new List<int> { -1, -1, 14, -1, -1 },
                        ExtraB = new List<int> { -1, -1, 0, -1, -1 }
                    };
                }
                else if (iRand == 75)
                {
                    myCB = new ClothBank
                    {
                        Title = "F75",
                        ClothA = new List<int> { 0, 0, -1, 7, 43, 0, 20, 0, 13, 0, 0, 69 },
                        ClothB = new List<int> { 0, 0, 0, 0, 4, 0, 0, 0, 6, 0, 0, 0 },
                        ExtraA = new List<int> { -1, 11, -1, -1, -1 },
                        ExtraB = new List<int> { -1, 2, -1, -1, -1 }
                    };
                }
                else if (iRand == 76)
                {
                    myCB = new ClothBank
                    {
                        Title = "F76",
                        ClothA = new List<int> { 0, 0, -1, 41, 32, 0, 26, 0, 14, 12, 0, 232 },
                        ClothB = new List<int> { 0, 0, 0, 0, 0, 0, 0, 0, 0, 4, 0, 20 },
                        ExtraA = new List<int> { 111, -1, -1, -1, -1 },
                        ExtraB = new List<int> { 4, -1, -1, -1, -1 }
                    };
                }
                else if (iRand == 77)
                {
                    myCB = new ClothBank
                    {
                        Title = "F77",
                        ClothA = new List<int> { 0, 0, -1, 3, 80, 0, 60, 0, 69, 0, 0, 193 },
                        ClothB = new List<int> { 0, 0, 0, 0, 7, 0, 4, 0, 2, 0, 0, 0 },
                        ExtraA = new List<int> { 55, 11, -1, -1, -1 },
                        ExtraB = new List<int> { 19, 2, -1, -1, -1 }
                    };
                }
                else if (iRand == 78)
                {
                    myCB = new ClothBank
                    {
                        Title = "F78",
                        ClothA = new List<int> { 0, 0, -1, 139, 73, 0, 60, 38, 52, 0, 0, 167 },
                        ClothB = new List<int> { 0, 0, 0, 0, 1, 0, 10, 0, 1, 0, 0, 3 },
                        ExtraA = new List<int> { -1, 11, 13, -1, -1 },
                        ExtraB = new List<int> { -1, 0, 0, -1, -1 }
                    };
                }
                else if (iRand == 79)
                {
                    myCB = new ClothBank
                    {
                        Title = "F79",
                        ClothA = new List<int> { 0, 0, -1, 3, 54, 0, 30, 0, 3, 0, 0, 98 },
                        ClothB = new List<int> { 0, 0, 0, 0, 2, 0, 0, 0, 0, 0, 0, 2 },
                        ExtraA = new List<int> { -1, 24, -1, -1, -1 },
                        ExtraB = new List<int> { -1, 0, -1, -1, -1 }
                    };
                }
                else if (iRand == 80)
                {
                    myCB = new ClothBank
                    {
                        Title = "F80",
                        ClothA = new List<int> { 0, 0, -1, 4, 84, 0, 60, 0, 3, 0, 0, 195 },
                        ClothB = new List<int> { 0, 0, 0, 0, 2, 0, 3, 0, 0, 0, 0, 5 },
                        ExtraA = new List<int> { 95, -1, 16, -1, -1 },
                        ExtraB = new List<int> { 1, -1, 0, -1, -1 }
                    };
                }
                else if (iRand == 81)
                {
                    myCB = new ClothBank
                    {
                        Title = "F81",
                        ClothA = new List<int> { 0, 0, -1, 174, 90, 0, 63, 0, 130, 0, 0, 216 },
                        ClothB = new List<int> { 0, 0, 0, 19, 6, 0, 7, 0, 6, 0, 0, 18 },
                        ExtraA = new List<int> { 104, 25, -1, -1, -1 },
                        ExtraB = new List<int> { 23, 6, -1, -1, -1 }
                    };
                }
                else if (iRand == 82)
                {
                    myCB = new ClothBank
                    {
                        Title = "F82",
                        ClothA = new List<int> { 0, 0, -1, 3, 55, 0, 29, 0, 38, 0, 0, 95 },
                        ClothB = new List<int> { 0, 0, 0, 0, 0, 0, 2, 0, 10, 0, 0, 0 },
                        ExtraA = new List<int> { -1, -1, -1, -1, -1 },
                        ExtraB = new List<int> { -1, -1, -1, -1, -1 }
                    };
                }
                else if (iRand == 83)
                {
                    myCB = new ClothBank
                    {
                        Title = "F83",
                        ClothA = new List<int> { 0, 0, -1, 4, 83, 0, 60, 0, 3, 0, 0, 195 },
                        ClothB = new List<int> { 0, 0, 0, 0, 0, 0, 10, 0, 0, 0, 0, 2 },
                        ExtraA = new List<int> { 55, -1, 14, -1, -1 },
                        ExtraB = new List<int> { 1, -1, 0, -1, -1 }
                    };
                }
                else if (iRand == 84)
                {
                    myCB = new ClothBank
                    {
                        Title = "F84",
                        ClothA = new List<int> { 0, 0, -1, 3, 80, 0, 60, 93, 59, 0, 0, 193 },
                        ClothB = new List<int> { 0, 0, 0, 0, 6, 0, 9, 0, 0, 0, 0, 23 },
                        ExtraA = new List<int> { 95, 2, -1, -1, -1 },
                        ExtraB = new List<int> { 0, 5, -1, -1, -1 }
                    };
                }
                else if (iRand == 85)
                {
                    myCB = new ClothBank
                    {
                        Title = "F85",
                        ClothA = new List<int> { 0, 101, -1, 23, 80, 0, 60, 0, 3, 0, 0, 205 },
                        ClothB = new List<int> { 0, 13, 0, 0, 2, 0, 10, 0, 0, 0, 0, 25 },
                        ExtraA = new List<int> { 101, 3, -1, -1, -1 },
                        ExtraB = new List<int> { 7, 3, -1, -1, -1 }
                    };
                }
                else if (iRand == 86)
                {
                    myCB = new ClothBank
                    {
                        Title = "F86",
                        ClothA = new List<int> { 0, 0, -1, 7, 61, 0, 26, 53, 13, 0, 0, 34 },
                        ClothB = new List<int> { 0, 0, 0, 0, 7, 0, 0, 0, 0, 0, 0, 0 },
                        ExtraA = new List<int> { 55, 25, -1, -1, -1 },
                        ExtraB = new List<int> { 1, 9, -1, -1, -1 }
                    };
                }
                else if (iRand == 87)
                {
                    myCB = new ClothBank
                    {
                        Title = "Crimbo",
                        ClothA = new List<int> { 0, 0, -1, 3, 31, 0, 2, 17, 3, 0, 0, 45 },
                        ClothB = new List<int> { 0, 0, 0, 0, 3, 0, 13, 2, 0, 0, 0, 3 },
                        ExtraA = new List<int> { 42, -1, -1, -1, -1 },
                        ExtraB = new List<int> { 3, -1, -1, -1, -1 }
                    };
                }
                else if (iRand == 88)
                {
                    myCB = new ClothBank
                    {
                        Title = "Crimbo",
                        ClothA = new List<int> { 0, 0, -1, 3, 31, 0, 2, 17, 3, 0, 0, 45 },
                        ClothB = new List<int> { 0, 0, 0, 0, 0, 0, 13, 0, 0, 0, 0, 0 },
                        ExtraA = new List<int> { 39, -1, -1, -1, -1 },
                        ExtraB = new List<int> { 3, -1, -1, -1, -1 }
                    };
                }
                else if (iRand == 89)
                {
                    myCB = new ClothBank
                    {
                        Title = "Crimbo",
                        ClothA = new List<int> { 0, 0, -1, 3, 31, 0, 17, 0, 3, 0, 0, 45 },
                        ClothB = new List<int> { 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 1 },
                        ExtraA = new List<int> { 41, -1, -1, -1, -1 },
                        ExtraB = new List<int> { 0, -1, -1, -1, -1 }
                    };
                }
                else if (iRand == 90)
                {
                    myCB = new ClothBank
                    {
                        Title = "Crimbo",
                        ClothA = new List<int> { 0, 0, -1, 3, 2, 0, 2, 0, 3, 0, 0, 44 },
                        ClothB = new List<int> { 0, 0, 0, 0, 0, 0, 13, 0, 0, 0, 0, 0 },
                        ExtraA = new List<int> { 39, -1, -1, -1, -1 },
                        ExtraB = new List<int> { 6, -1, -1, -1, -1 }
                    };
                }
                else if (iRand == 91)
                {
                    myCB = new ClothBank
                    {
                        Title = "Crimbo",
                        ClothA = new List<int> { 0, 0, -1, 3, 27, 0, 17, 17, 3, 0, 0, 44 },
                        ClothB = new List<int> { 0, 0, 0, 0, 2, 0, 0, 0, 0, 0, 0, 1 },
                        ExtraA = new List<int> { 41, -1, -1, -1, -1 },
                        ExtraB = new List<int> { 2, -1, -1, -1, -1 }
                    };
                }
                else if (iRand == 92)
                {
                    myCB = new ClothBank
                    {
                        Title = "Crimbo",
                        ClothA = new List<int> { 0, 0, -1, 3, 0, 0, 22, 0, 3, 0, 0, 44 },
                        ClothB = new List<int> { 0, 0, 0, 0, 7, 0, 0, 0, 0, 0, 0, 2 },
                        ExtraA = new List<int> { 40, -1, -1, -1, -1 },
                        ExtraB = new List<int> { 0, -1, -1, -1, -1 }
                    };
                }
                else if (iRand == 93)
                {
                    myCB = new ClothBank
                    {
                        Title = "Crimbo",
                        ClothA = new List<int> { 0, 0, -1, 3, 31, 0, 2, 0, 3, 0, 0, 45 },
                        ClothB = new List<int> { 0, 0, 0, 0, 2, 0, 5, 0, 0, 0, 0, 2 },
                        ExtraA = new List<int> { 43, -1, -1, -1, -1 },
                        ExtraB = new List<int> { 1, -1, -1, -1, -1 }
                    };
                }
                else if (iRand == 94)
                {
                    int iText = RandomNum.RandInt(0, 15);
                    int iText2 = RandomNum.RandInt(0, 11);
                    int iText3 = RandomNum.RandInt(0, 8);
                    myCB = new ClothBank
                    {
                        Title = "F94",
                        ClothA = new List<int> { 0, 0, -1, 4, 0, 0, 20, 11, 3, 0, 0, 33 },
                        ClothB = new List<int> { 0, 0, 0, 0, iText, 0, iText2, 0, 0, 0, 0, iText3 },
                        ExtraA = new List<int> { -1, -1, -1, -1, -1 },
                        ExtraB = new List<int> { -1, -1, -1, -1, -1 }
                    };
                }
                else if (iRand == 95)
                {
                    int iText = RandomNum.RandInt(0, 15);
                    int iText2 = RandomNum.RandInt(0, 11);
                    int iText3 = RandomNum.RandInt(0, 8);
                    int iText4 = RandomNum.RandInt(0, 6);
                    myCB = new ClothBank
                    {
                        Title = "F95",
                        ClothA = new List<int> { 0, 0, -1, 5, 27, 0, 19, 4, 28, 0, 0, 31 },
                        ClothB = new List<int> { 0, 0, 0, 0, iText, 0, iText2, 2, iText3, 0, 0, iText4 },
                        ExtraA = new List<int> { -1, -1, -1, -1, -1 },
                        ExtraB = new List<int> { -1, -1, -1, -1, -1 }
                    };
                }
                else if (iRand == 95)
                {
                    myCB = new ClothBank
                    {
                        Title = "F95",
                        ClothA = new List<int> { 0, 0, -1, 5, 27, 0, 19, 4, 28, 0, 0, 31 },
                        ClothB = new List<int> { 0, 0, 0, 0, 15, 0, 11, 2, 8, 0, 0, 6 },
                        ExtraA = new List<int> { -1, -1, -1, -1, -1 },
                        ExtraB = new List<int> { -1, -1, -1, -1, -1 }
                    };
                }
                else if (iRand == 96)
                {
                    myCB = new ClothBank
                    {
                        Title = "F96",
                        ClothA = new List<int> { 0, 0, -1, 6, 36, 0, 20, 6, 13, 0, 0, 25 },
                        ClothB = new List<int> { 0, 0, 0, 0, 2, 0, 0, 0, 0, 0, 0, 2 },
                        ExtraA = new List<int> { -1, -1, -1, -1, -1 },
                        ExtraB = new List<int> { -1, -1, -1, -1, -1 }
                    };
                }
                else if (iRand == 97)
                {
                    myCB = new ClothBank
                    {
                        Title = "F97",
                        ClothA = new List<int> { 0, 0, -1, 6, 6, 0, 13, 6, 25, 0, 0, 7 },
                        ClothB = new List<int> { 0, 0, 0, 0, 0, 0, 0, 0, 6, 0, 0, 0 },
                        ExtraA = new List<int> { -1, -1, -1, -1, -1 },
                        ExtraB = new List<int> { -1, -1, -1, -1, -1 }
                    };
                }
                else if (iRand == 98)
                {
                    myCB = new ClothBank
                    {
                        Title = "F98",
                        ClothA = new List<int> { 0, 0, -1, 0, 7, 0, 19, 1, 24, 0, 0, 28 },
                        ClothB = new List<int> { 0, 0, 0, 0, 2, 0, 9, 1, 3, 0, 0, 10 },
                        ExtraA = new List<int> { -1, -1, -1, -1, -1 },
                        ExtraB = new List<int> { -1, -1, -1, -1, -1 }
                    };
                }
                else if (iRand == 99)
                {
                    myCB = new ClothBank
                    {
                        Title = "F99",
                        ClothA = new List<int> { 21, 0, 0, 3, 111, 0, 29, 99, 6, 0, 0, 285 },
                        ClothB = new List<int> { 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0 },
                        ExtraA = new List<int> { -1, -1, -1, -1, -1 },
                        ExtraB = new List<int> { -1, -1, -1, -1, -1 }
                    };
                }
                else if (iRand == 100)
                {
                    int iText = RandomNum.RandInt(0, 25);
                    myCB = new ClothBank
                    {
                        Title = "F100",
                        ClothA = new List<int> { 21, 0, 0, 11, 15, 0, 6, 0, 3, 0, 0, 322 },
                        ClothB = new List<int> { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, iText },
                        ExtraA = new List<int> { -1, -1, -1, -1, -1 },
                        ExtraB = new List<int> { -1, -1, -1, -1, -1 }
                    };
                }
                else if (iRand == 101)
                {
                    myCB = new ClothBank
                    {
                        Title = "F101",
                        ClothA = new List<int> { 21, 0, 0, 9, 6, 0, 37, 0, 2, 0, 87, 330 },
                        ClothB = new List<int> { 0, 0, 0, 0, 0, 0, 3, 0, 0, 0, 0, 5 },
                        ExtraA = new List<int> { 148, -1, -1, -1, -1 },
                        ExtraB = new List<int> { 0, -1, -1, -1, -1 }
                    };
                }
                else if (iRand == 102)
                {
                    myCB = new ClothBank
                    {
                        Title = "F102",
                        ClothA = new List<int> { 0, 0, -1, 14, 14, 0, 3, 3, 3, 0, 0, 14 },
                        ClothB = new List<int> { 0, 0, 0, 0, 8, 0, 1, 5, 0, 0, 0, 0 },
                        ExtraA = new List<int> { -1, -1, -1, -1, -1 },
                        ExtraB = new List<int> { -1, -1, -1, -1, -1 }
                    };
                }
                else if (iRand == 103)
                {
                    myCB = new ClothBank
                    {
                        Title = "F103",
                        ClothA = new List<int> { 0, 0, -1, 14, 2, 0, 10, 0, 3, 0, 0, 14 },
                        ClothB = new List<int> { 0, 0, 0, 0, 0, 0, 2, 0, 0, 0, 0, 7 },
                        ExtraA = new List<int> { -1, -1, -1, -1, -1 },
                        ExtraB = new List<int> { -1, -1, -1, -1, -1 }
                    };
                }
                else if (iRand == 104)
                {
                    myCB = new ClothBank
                    {
                        Title = "F104",
                        ClothA = new List<int> { 0, 0, -1, 7, 14, 0, 11, 2, 15, 0, 0, 10 },
                        ClothB = new List<int> { 0, 0, 0, 0, 9, 0, 0, 4, 0, 0, 0, 0 },
                        ExtraA = new List<int> { -1, -1, -1, -1, -1 },
                        ExtraB = new List<int> { -1, -1, -1, -1, -1 }
                    };
                }
                else if (iRand == 105)
                {
                    myCB = new ClothBank
                    {
                        Title = "F105",
                        ClothA = new List<int> { 0, 0, -1, 11, 2, 0, 10, 3, 3, 0, 0, 11 },
                        ClothB = new List<int> { 0, 0, 0, 0, 2, 0, 2, 3, 0, 0, 0, 0 },
                        ExtraA = new List<int> { -1, -1, -1, -1, -1 },
                        ExtraB = new List<int> { -1, -1, -1, -1, -1 }
                    };
                }
                else if (iRand == 106)
                {
                    myCB = new ClothBank
                    {
                        Title = "F106",
                        ClothA = new List<int> { 0, 0, -1, 14, 12, 0, 10, 3, 3, 0, 0, 14 },
                        ClothB = new List<int> { 0, 0, 0, 0, 8, 0, 3, 4, 0, 0, 0, 10 },
                        ExtraA = new List<int> { -1, -1, -1, -1, -1 },
                        ExtraB = new List<int> { -1, -1, -1, -1, -1 }
                    };
                }
                else if (iRand == 107)
                {
                    myCB = new ClothBank
                    {
                        Title = "F107",
                        ClothA = new List<int> { 0, 0, -1, 7, 2, 0, 11, 0, 16, 0, 0, 10 },
                        ClothB = new List<int> { 0, 0, 0, 0, 2, 0, 1, 0, 1, 0, 0, 7 },
                        ExtraA = new List<int> { -1, -1, -1, -1, -1 },
                        ExtraB = new List<int> { -1, -1, -1, -1, -1 }
                    };
                }
                else if (iRand == 108)
                {
                    myCB = new ClothBank
                    {
                        Title = "F108",
                        ClothA = new List<int> { 0, 0, -1, 7, 10, 0, 1, 1, 5, 0, 0, 10 },
                        ClothB = new List<int> { 0, 0, 0, 0, 2, 0, 13, 1, 0, 0, 0, 10 },
                        ExtraA = new List<int> { -1, -1, -1, -1, -1 },
                        ExtraB = new List<int> { -1, -1, -1, -1, -1 }
                    };
                }
                else if (iRand == 109)
                {
                    myCB = new ClothBank
                    {
                        Title = "F109",
                        ClothA = new List<int> { 0, 0, -1, 14, 12, 0, 4, 0, 3, 0, 0, 14 },
                        ClothB = new List<int> { 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 4 },
                        ExtraA = new List<int> { -1, -1, -1, -1, -1 },
                        ExtraB = new List<int> { -1, -1, -1, -1, -1 }
                    };
                }
                else if (iRand == 110)
                {
                    int iText = RandomNum.RandInt(0, 15);
                    int iText2 = RandomNum.RandInt(0, 15);
                    myCB = new ClothBank
                    {
                        Title = "F110",
                        ClothA = new List<int> { 0, 0, -1, 4, 4, 0, 3, 2, 3, 0, 0, 32 },
                        ClothB = new List<int> { 0, 0, 0, 0, iText, 0, iText2, 2, 0, 0, 0, 1 },
                        ExtraA = new List<int> { -1, -1, -1, -1, -1 },
                        ExtraB = new List<int> { -1, -1, -1, -1, -1 }
                    };
                }
                else if (iRand == 111)
                {
                    myCB = new ClothBank
                    {
                        Title = "F111",
                        ClothA = new List<int> { 0, 0, -1, 3, 11, 0, 3, 1, 3, 0, 0, 3 },
                        ClothB = new List<int> { 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 12 },
                        ExtraA = new List<int> { -1, -1, -1, -1, -1 },
                        ExtraB = new List<int> { -1, -1, -1, -1, -1 }
                    };
                }
                else if (iRand == 112)
                {
                    myCB = new ClothBank
                    {
                        Title = "F112",
                        ClothA = new List<int> { 0, 0, -1, 3, 3, 0, 3, 0, 3, 0, 0, 3 },
                        ClothB = new List<int> { 0, 0, 0, 0, 15, 0, 15, 0, 0, 0, 0, 1 },
                        ExtraA = new List<int> { -1, -1, -1, -1, -1 },
                        ExtraB = new List<int> { -1, -1, -1, -1, -1 }
                    };
                }
                else if (iRand == 113)
                {
                    myCB = new ClothBank
                    {
                        Title = "F113",
                        ClothA = new List<int> { 0, 0, -1, 3, 11, 0, 7, 0, 14, 0, 0, 43 },
                        ClothB = new List<int> { 0, 0, 0, 0, 10, 0, 6, 0, 0, 0, 0, 4 },
                        ExtraA = new List<int> { -1, -1, -1, -1, -1 },
                        ExtraB = new List<int> { -1, -1, -1, -1, -1 }
                    };
                }
                else if (iRand == 114)
                {
                    myCB = new ClothBank
                    {
                        Title = "F114",
                        ClothA = new List<int> { 0, 0, -1, 4, 27, 0, 11, 2, 3, 0, 0, 5 },
                        ClothB = new List<int> { 0, 0, 0, 0, 0, 0, 1, 4, 0, 0, 0, 9 },
                        ExtraA = new List<int> { -1, -1, -1, -1, -1 },
                        ExtraB = new List<int> { -1, -1, -1, -1, -1 }
                    };
                }
                else if (iRand == 115)
                {
                    myCB = new ClothBank
                    {
                        Title = "Halloween",
                        ClothA = new List<int> { 0, 94, -1, 36, 81, 0, 53, 0, 3, 0, 0, 150 },
                        ClothB = new List<int> { 0, 4, 0, 0, 0, 0, 1, 0, 0, 0, 0, 7 },
                        ExtraA = new List<int> { -1, -1, -1, -1, -1 },
                        ExtraB = new List<int> { -1, -1, -1, -1, -1 }
                    };
                }
                else if (iRand == 116)
                {
                    myCB = new ClothBank
                    {
                        Title = "Halloween",
                        ClothA = new List<int> { 0, 95, -1, 49, 39, 0, 26, 0, 3, 0, 0, 60 },
                        ClothB = new List<int> { 0, 7, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                        ExtraA = new List<int> { -1, -1, -1, -1, -1 },
                        ExtraB = new List<int> { -1, -1, -1, -1, -1 }
                    };
                }
                else 
                {
                    int iText = RandomNum.RandInt(0, 11);
                    int iBod = RandomNum.RandInt(139, 141);
                    myCB = new ClothBank
                    {
                        Title = "Halloween",
                        ClothA = new List<int> { 21, iBod, 0, 205, 115, 0, 34, 0, 6, 0, 0, 290 },
                        ClothB = new List<int> { 0, iText, 0, iText, iText, 0, 0, 0, 0, 0, 0, iText },
                        ExtraA = new List<int> { -1, -1, -1, -1, -1 },
                        ExtraB = new List<int> { -1, -1, -1, -1, -1 }
                    };
                }

            }

            return myCB;
        }
    }
}
