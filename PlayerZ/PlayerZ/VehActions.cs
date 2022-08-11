using GTA;
using GTA.Math;
using GTA.Native;
using System.Collections.Generic;

namespace PlayerZero
{
    public class VehActions
    {
        public static bool IsItARealVehicle(string sVehName)
        {
            LoggerLight.GetLogging("VehActions.IsItARealVehicle, sVehName == " + sVehName);

            bool bIsReal = false;

            int iVehHash = Function.Call<int>(Hash.GET_HASH_KEY, sVehName);
            if (Function.Call<bool>(Hash.IS_MODEL_A_VEHICLE, iVehHash))
                bIsReal = true;

            return bIsReal;
        }
        public static string RandVeh(int iVechList)
        {
            LoggerLight.GetLogging("VehActions.RandVeh, iVechList == " + iVechList);

            List<string> sVehicles = new List<string>();

            if (iVechList == 1)
            {
                sVehicles.Add("PFISTER811");
                sVehicles.Add("ADDER");
                sVehicles.Add("AUTARCH");
                sVehicles.Add("BANSHEE2");
                sVehicles.Add("BULLET");
                sVehicles.Add("CHEETAH");
                sVehicles.Add("CYCLONE");
                sVehicles.Add("ENTITYXF");
                sVehicles.Add("ENTITY2");
                sVehicles.Add("SHEAVA");
                sVehicles.Add("FMJ");
                sVehicles.Add("GP1");
                sVehicles.Add("INFERNUS");
                sVehicles.Add("ITALIGTB");
                sVehicles.Add("ITALIGTB2");
                sVehicles.Add("OSIRIS");
                sVehicles.Add("NERO");
                sVehicles.Add("NERO2");
                sVehicles.Add("PENETRATOR");
                sVehicles.Add("LE7B");
                sVehicles.Add("REAPER");
                sVehicles.Add("VOLTIC2");
                sVehicles.Add("SC1");
                sVehicles.Add("SULTANRS");
                sVehicles.Add("T20");
                sVehicles.Add("TAIPAN");
                sVehicles.Add("TEMPESTA");
                sVehicles.Add("TEZERACT");
                sVehicles.Add("TURISMOR");
                sVehicles.Add("TYRANT");
                sVehicles.Add("TYRUS");
                sVehicles.Add("VACCA");
                sVehicles.Add("VAGNER");
                sVehicles.Add("VISIONE");
                sVehicles.Add("VOLTIC");
                sVehicles.Add("PROTOTIPO");
                sVehicles.Add("XA21");
                sVehicles.Add("ZENTORNO");
                if (DataStore.bTrainM)
                {
                    sVehicles.Add("DEVESTE");
                    sVehicles.Add("EMERUS");
                    sVehicles.Add("FURIA");
                    sVehicles.Add("KRIEGER");
                    sVehicles.Add("THRAX");
                    sVehicles.Add("ZORRUSSO");
                    sVehicles.Add("TIGON");
                    sVehicles.Add("champion"); //Dewbauchee Champion
                    sVehicles.Add("ignus"); //Pegassi Ignus
                    sVehicles.Add("zeno"); //Overflod Zeno
                    sVehicles.Add("lm87"); //
                    sVehicles.Add("torero2"); //
                }
            }            //Super
            else if (iVechList == 2)
            {
                sVehicles.Add("COGCABRIO"); //
                sVehicles.Add("EXEMPLAR"); //
                sVehicles.Add("F620"); //
                sVehicles.Add("FELON"); //
                sVehicles.Add("FELON2"); //<!-- Felon GT -->
                sVehicles.Add("JACKAL"); //
                sVehicles.Add("ORACLE"); //
                sVehicles.Add("ORACLE2"); //<!-- Oracle XS -->
                if (DataStore.bTrainM)
                {
                    sVehicles.Add("PREVION"); //<!-- PREVION -->
                    sVehicles.Add("kanjosj"); //<!--  -->
                    sVehicles.Add("postlude"); //<!--  -->
                }
            }       //Coupe
            else if (iVechList == 3)
            {
                sVehicles.Add("NINEF"); //
                sVehicles.Add("NINEF2"); //<!-- 9F Cabrio -->
                sVehicles.Add("ALPHA"); //
                sVehicles.Add("BANSHEE"); //
                sVehicles.Add("BESTIAGTS"); //
                sVehicles.Add("BLISTA2"); //<!-- Blista Compact -->
                sVehicles.Add("BUFFALO"); //
                sVehicles.Add("BUFFALO2"); //<!-- Buffalo S -->
                sVehicles.Add("CARBONIZZARE"); //
                sVehicles.Add("COMET2"); //<!-- Comet -->
                sVehicles.Add("COMET3"); //<!-- Comet Retro Custom -->
                sVehicles.Add("COMET4"); //<!-- Comet Safari -->
                sVehicles.Add("COMET5"); //<!-- Comet SR -->
                sVehicles.Add("COQUETTE"); //
                sVehicles.Add("TAMPA2"); //<!-- Drift Tampa -->
                sVehicles.Add("ELEGY"); //<!-- Elegy Retro Custom -->
                sVehicles.Add("ELEGY2"); //<!-- Elegy RH8 -->
                sVehicles.Add("FELTZER2"); //<!-- Feltzer -->
                sVehicles.Add("FLASHGT"); //
                sVehicles.Add("FUROREGT"); //
                sVehicles.Add("FUSILADE"); //
                sVehicles.Add("FUTO"); //
                sVehicles.Add("GB200"); //
                sVehicles.Add("BLISTA3"); //<!-- Go Go Monkey Blista -->
                sVehicles.Add("HOTRING"); //
                sVehicles.Add("JESTER"); //
                sVehicles.Add("JESTER2"); //<!-- Jester (Racecar) -->
                sVehicles.Add("JESTER3"); //<!-- Jester Classic -->
                sVehicles.Add("KHAMELION"); //
                sVehicles.Add("KURUMA"); //
                sVehicles.Add("LYNX"); //
                sVehicles.Add("MASSACRO"); //
                sVehicles.Add("MASSACRO2"); //<!-- Massacro (Racecar) -->
                sVehicles.Add("NEON"); //
                sVehicles.Add("OMNIS"); //
                sVehicles.Add("PARIAH"); //
                sVehicles.Add("PENUMBRA"); //
                sVehicles.Add("RAIDEN"); //
                sVehicles.Add("RAPIDGT"); //
                sVehicles.Add("RAPIDGT2"); //<!-- Rapid GT Cabrio -->
                sVehicles.Add("RAPTOR"); //
                sVehicles.Add("REVOLTER"); //
                sVehicles.Add("RUSTON"); //
                sVehicles.Add("SCHAFTER4"); //<!-- Schafter LWB -->
                sVehicles.Add("SCHAFTER3"); //<!-- Schafter V12 -->
                sVehicles.Add("SCHWARZER"); //
                sVehicles.Add("SENTINEL3"); //<!-- Sentinel Classic -->
                sVehicles.Add("SEVEN70"); //
                sVehicles.Add("SPECTER"); //
                sVehicles.Add("SPECTER2"); //<!-- Specter Custom -->
                sVehicles.Add("BUFFALO3"); //<!-- Sprunk Buffalo -->
                sVehicles.Add("STREITER"); //
                sVehicles.Add("SULTAN"); //
                sVehicles.Add("SURANO"); //
                sVehicles.Add("TROPOS"); //
                sVehicles.Add("VERLIERER2"); //
                if (DataStore.bTrainM)
                {
                    sVehicles.Add("DRAFTER"); //<!-- 8F Drafter -->
                    sVehicles.Add("IMORGON"); //
                    sVehicles.Add("ISSI7"); //<!-- Issi Sport -->
                    sVehicles.Add("ITALIGTO"); //
                    sVehicles.Add("JUGULAR"); //
                    sVehicles.Add("KOMODA"); //
                    sVehicles.Add("LOCUST"); //
                    sVehicles.Add("NEO"); //
                    sVehicles.Add("PARAGON"); //
                    sVehicles.Add("PARAGON2"); //<!-- Paragon R (Armored) -->
                    sVehicles.Add("SCHLAGEN"); //
                    sVehicles.Add("SUGOI"); //
                    sVehicles.Add("SULTAN2"); //<!-- Sultan Classic -->
                    sVehicles.Add("VSTR"); //<!-- V-STR -->
                    sVehicles.Add("COQUETTE4"); //<!-- Coquette D10  -->
                    sVehicles.Add("PENUMBRA2"); //<!-- Penumbra FF   -->
                    sVehicles.Add("ITALIRSX"); //><!-- Grotti Itali RSX -->Spports
                    sVehicles.Add("CALICO"); //
                    sVehicles.Add("COMET6"); //
                    sVehicles.Add("CYPHER"); //
                    sVehicles.Add("EUROS"); //
                    sVehicles.Add("FUTO2"); //
                    sVehicles.Add("GROWLER"); //
                    sVehicles.Add("JESTER4"); //
                    sVehicles.Add("REMUS"); //
                    sVehicles.Add("RT3000"); //
                    sVehicles.Add("SULTAN3"); //
                    sVehicles.Add("VECTRE"); //
                    sVehicles.Add("ZR350"); //
                    sVehicles.Add("comet7"); //Pfister Comet S2 Cabrio
                    sVehicles.Add("corsita"); //
                    sVehicles.Add("omnisegt"); //
                    sVehicles.Add("tenf2"); //
                    sVehicles.Add("sentinel4"); //
                    sVehicles.Add("sm722"); //
                }
            }       //Sport
            else if (iVechList == 4)
            {
                sVehicles.Add("BLADE"); //
                sVehicles.Add("BUCCANEER2"); //<!-- Buccaneer Custom -->
                sVehicles.Add("STALION2"); //<!-- Burger Shot Stallion -->
                sVehicles.Add("CHINO2"); //<!-- Chino Custom -->
                sVehicles.Add("COQUETTE3"); //<!-- Coquette BlackFin -->
                sVehicles.Add("DOMINATOR3"); //<!-- Dominator GTX -->
                sVehicles.Add("YOSEMITE2"); //<!-- Drift Yosemite -->
                sVehicles.Add("ELLIE"); //
                sVehicles.Add("FACTION2"); //<!-- Faction Custom -->
                sVehicles.Add("FACTION3"); //<!-- Faction Custom Donk -->
                sVehicles.Add("GAUNTLET"); //
                sVehicles.Add("HERMES"); //
                sVehicles.Add("HOTKNIFE"); //
                sVehicles.Add("HUSTLER"); //
                sVehicles.Add("SLAMVAN2"); //<!-- Lost Slamvan -->
                sVehicles.Add("LURCHER"); //
                sVehicles.Add("MOONBEAM2"); //<!-- Moonbeam Custom -->
                sVehicles.Add("NIGHTSHADE"); //
                sVehicles.Add("PICADOR"); //
                sVehicles.Add("DOMINATOR2"); //<!-- Pisswasser Dominator -->
                sVehicles.Add("GAUNTLET2"); //<!-- Redwood Gauntlet -->
                sVehicles.Add("SABREGT2"); //<!-- Sabre Turbo Custom -->
                sVehicles.Add("SLAMVAN3"); //<!-- Slamvan Custom -->
                sVehicles.Add("TAMPA"); //
                sVehicles.Add("VIGERO"); //
                sVehicles.Add("VIRGO"); //
                sVehicles.Add("VIRGO3"); //<!-- Virgo Classic -->
                sVehicles.Add("VIRGO2"); //<!-- Virgo Classic Custom -->
                sVehicles.Add("VOODOO2"); //<!-- Voodoo Custom -->
                sVehicles.Add("YOSEMITE"); //
                if (DataStore.bTrainM)
                {
                    sVehicles.Add("CLIQUE"); //
                    sVehicles.Add("DEVIANT"); //
                    sVehicles.Add("GAUNTLET3"); //<!-- Gauntlet Classic -->
                    sVehicles.Add("GAUNTLET4"); //<!-- Gauntlet Hellfire -->
                    sVehicles.Add("PEYOTE2"); //<!-- Peyote Gasser -->
                    sVehicles.Add("IMPALER"); //
                    sVehicles.Add("TULIP"); //
                    sVehicles.Add("VAMOS"); //
                    sVehicles.Add("DUKES3"); //
                    sVehicles.Add("GAUNTLET5"); //
                    sVehicles.Add("MANANA2"); //
                    sVehicles.Add("PEYOTE3"); //
                    sVehicles.Add("GLENDALE2"); //
                    sVehicles.Add("YOSEMITE3"); //
                    sVehicles.Add("DOMINATOR7"); //
                    sVehicles.Add("DOMINATOR8"); //
                    sVehicles.Add("buffalo4"); //Bravado Buffalo STX
                    sVehicles.Add("greenwood"); //
                    sVehicles.Add("ruiner4"); //
                    sVehicles.Add("vigero2"); //
                    sVehicles.Add("weevil2"); //
                }
            }       //Mussle
            else if (iVechList == 5)
            {
                sVehicles.Add("Z190"); //<!-- 190z -->
                sVehicles.Add("ARDENT"); //
                sVehicles.Add("CASCO"); //
                sVehicles.Add("CHEBUREK"); //
                sVehicles.Add("CHEETAH2"); //<!-- Cheetah Classic -->
                sVehicles.Add("COQUETTE2"); //<!-- Coquette Classic -->
                sVehicles.Add("FAGALOA"); //
                sVehicles.Add("BTYPE2"); //<!-- FrÃ¤nken Stange -->
                sVehicles.Add("GT500"); //
                sVehicles.Add("INFERNUS2"); //<!-- Infernus Classic -->
                sVehicles.Add("JB700"); //
                sVehicles.Add("MAMBA"); //
                sVehicles.Add("MANANA"); //
                sVehicles.Add("MICHELLI"); //
                sVehicles.Add("MONROE"); //
                sVehicles.Add("PEYOTE"); //
                sVehicles.Add("PIGALLE"); //
                sVehicles.Add("RAPIDGT3"); //<!-- Rapid GT Classic -->
                sVehicles.Add("RETINUE"); //
                sVehicles.Add("BTYPE"); //<!-- Roosevelt -->
                sVehicles.Add("BTYPE3"); //<!-- Roosevelt Valor -->
                sVehicles.Add("SAVESTRA"); //
                sVehicles.Add("STINGER"); //
                sVehicles.Add("STINGERGT"); //
                sVehicles.Add("FELTZER3"); //<!-- Stirling GT -->
                sVehicles.Add("SWINGER"); //
                sVehicles.Add("TORERO"); //
                sVehicles.Add("TORNADO"); //
                sVehicles.Add("TORNADO2"); //<!-- Tornado Cabrio -->
                sVehicles.Add("TORNADO3"); //<!-- Rusty Tornado -->
                sVehicles.Add("TORNADO4"); //<!-- Mariachi Tornado -->
                sVehicles.Add("TORNADO5"); //<!-- Tornado Custom -->
                sVehicles.Add("TORNADO6"); //<!-- Tornado Rat Rod -->
                sVehicles.Add("TURISMO2"); //<!-- Turismo Classic -->
                sVehicles.Add("VISERIS"); //
                sVehicles.Add("ZTYPE"); //
                if (DataStore.bTrainM)
                {
                    sVehicles.Add("DYNASTY"); //
                    sVehicles.Add("JB7002"); //<!-- JB 700W -->
                    sVehicles.Add("NEBULA"); //
                    sVehicles.Add("RETINUE2"); //<!-- Retinue MkII -->
                    sVehicles.Add("ZION3"); //<!-- Zion Classic -->
                    sVehicles.Add("COQUETTE4"); //<!-- Coquette D10  -->
                    sVehicles.Add("TOREADOR"); //><!-- Pegassi Toreador -->sportsClassic
                }
            }       //SportsClassic
            else if (iVechList == 6)
            {
                sVehicles.Add("BRIOSO"); //
                sVehicles.Add("ISSI3"); //<!-- Issi Classic -->
                if (DataStore.bTrainM)
                {
                    sVehicles.Add("ASBO"); //
                    sVehicles.Add("KANJO"); //<!-- Blista Kanjo -->
                    sVehicles.Add("CLUB");
                    sVehicles.Add("BRIOSO2"); //>Grotti Brioso 300
                    sVehicles.Add("WEEVIL"); //><!-- BF Weevil -->
                    sVehicles.Add("brioso3");
                }
            }       //Compact
            else if (iVechList == 7)
            {
                sVehicles.Add("COGNOSCENTI"); //
                sVehicles.Add("COGNOSCENTI2"); //<!-- Cognoscenti (Armored) -->
                sVehicles.Add("COG55"); //<!-- Cognoscenti 55 -->
                sVehicles.Add("COG552"); //<!-- Cognoscenti 55 (Armored) -->
                sVehicles.Add("PRIMO2"); //<!-- Primo Custom -->
                sVehicles.Add("SCHAFTER6"); //<!-- Schafter LWB (Armored) -->
                sVehicles.Add("SCHAFTER5"); //<!-- Schafter V12 (Armored) -->
                if (DataStore.bTrainM)
                {
                    sVehicles.Add("TAILGATER2"); //
                    sVehicles.Add("WARRENER2"); //
                    sVehicles.Add("cinquemila"); //Lampadati Cinquemila
                    sVehicles.Add("deity"); //Enus Deity
                }
            }       //Sedan
            else if (iVechList == 8)
            {
                sVehicles.Add("BRAWLER"); //
                sVehicles.Add("TROPHYTRUCK2"); //<!-- Desert Raid -->
                sVehicles.Add("DUBSTA3"); //<!-- Dubsta 6x6 -->
                sVehicles.Add("FREECRAWLER"); //
                sVehicles.Add("HELLION");
                sVehicles.Add("KALAHARI"); //
                sVehicles.Add("KAMACHO"); //
                sVehicles.Add("RIATA"); //
                sVehicles.Add("REBEL"); //<!-- Rusty Rebel -->
                sVehicles.Add("TROPHYTRUCK"); //
                sVehicles.Add("RALLYTRUCK"); //<!-- Dune -->
                if (DataStore.bTrainM)
                {
                    sVehicles.Add("CARACARA2"); //<!-- Caracara 4x4 -->
                    sVehicles.Add("EVERON"); //
                    sVehicles.Add("OUTLAW"); //
                    sVehicles.Add("VAGRANT"); //
                    sVehicles.Add("ZHABA"); //
                    sVehicles.Add("WINKY"); //><!-- Vapid Winky -->	
                    sVehicles.Add("SQUADDIE");
                    sVehicles.Add("patriot3"); //Mammoth Patriot Mil-Spec
                    sVehicles.Add("draugur"); //
                }
            }       //Offroad
            else if (iVechList == 9)
            {
                sVehicles.Add("AKUMA"); //
                sVehicles.Add("AVARUS"); //
                sVehicles.Add("CHIMERA"); //
                sVehicles.Add("DEFILER"); //
                sVehicles.Add("DIABLOUS"); //
                sVehicles.Add("DIABLOUS2"); //<!-- Diabolus Custom -->
                sVehicles.Add("DOUBLE"); //
                sVehicles.Add("ENDURO"); //
                sVehicles.Add("ESSKEY"); //
                sVehicles.Add("FAGGIO2"); //
                sVehicles.Add("FAGGIO3"); //<!-- Faggio Mod -->
                sVehicles.Add("FAGGIO"); //<!-- Faggio Sport -->
                sVehicles.Add("GARGOYLE"); //
                sVehicles.Add("HAKUCHOU"); //
                sVehicles.Add("HAKUCHOU2"); //<!-- Hakuchou Drag -->
                sVehicles.Add("HEXER"); //
                sVehicles.Add("INNOVATION"); //
                sVehicles.Add("LECTRO"); //
                sVehicles.Add("MANCHEZ"); //
                sVehicles.Add("NEMESIS"); //
                sVehicles.Add("NIGHTBLADE"); //
                sVehicles.Add("PCJ"); //
                sVehicles.Add("RATBIKE"); //
                sVehicles.Add("RUFFIAN"); //
                sVehicles.Add("SANCHEZ"); //<!-- Sanchez livery variant -->
                sVehicles.Add("SANCHEZ2"); //
                sVehicles.Add("THRUST"); //
                sVehicles.Add("VADER"); //
                sVehicles.Add("VINDICATOR"); //
                sVehicles.Add("WOLFSBANE"); //
                sVehicles.Add("ZOMBIEB"); //<!-- Zombie Chopper -->
                if (DataStore.bTrainM)
                {
                    sVehicles.Add("MANCHEZ2"); //><!-- Maibatsu Manchez Scout -->
                    sVehicles.Add("reever"); //Western Reever
                    sVehicles.Add("shinobi"); //Nagasaki Shinobi
                }
            }       //Motorcycle
            else if (iVechList == 10)
            {
                sVehicles.Add("DUKES2"); //<!-- Duke O'Death -->
                sVehicles.Add("CARACARA"); //
                sVehicles.Add("DUNE3"); //<!-- Dune FAV -->
                sVehicles.Add("DUNE4"); //<!-- Ramp Buggy mission variant -->
                sVehicles.Add("DUNE5"); //<!-- Ramp Buggy -->
                sVehicles.Add("MARSHALL"); //
                sVehicles.Add("MONSTER"); //<!-- Liberator -->
                sVehicles.Add("MENACER"); //
                sVehicles.Add("NIGHTSHARK"); //
                sVehicles.Add("TECHNICAL"); //
                sVehicles.Add("TECHNICAL2"); //<!-- Technical Aqua -->
                sVehicles.Add("TECHNICAL3"); //<!-- Technical Custom -->
                sVehicles.Add("MULE4"); //<!-- Mule Custom -->
                sVehicles.Add("POUNDER2"); //<!-- Pounder Custom -->
                sVehicles.Add("SPEEDO4"); //<!-- Speedo Custom -->
                sVehicles.Add("PHANTOM2"); //<!-- Phantom Wedge -->
                sVehicles.Add("STOCKADE"); //<!-- Securicar -->
                sVehicles.Add("BOXVILLE5"); //<!-- Armored Boxville -->
                sVehicles.Add("TERBYTE"); //
                sVehicles.Add("BRICKADE"); //
                sVehicles.Add("HALFTRACK"); //
                sVehicles.Add("VIGILANTE"); //
                sVehicles.Add("TAMPA3"); //<!-- Weaponized Tampa -->
                sVehicles.Add("RUINER2"); //<!-- Ruiner 2000 -->
                sVehicles.Add("STROMBERG"); //
                sVehicles.Add("DELUXO"); //
                sVehicles.Add("SCRAMJET"); //
                if (DataStore.bTrainM)
                {
                    sVehicles.Add("ZR380"); //<!-- Apocalypse ZR380 -->
                    sVehicles.Add("ZR3802"); //<!-- Future Shock ZR380 -->
                    sVehicles.Add("ZR3803"); //<!-- Nightmare ZR380 -->
                    sVehicles.Add("DOMINATOR4"); //<!-- Apocalypse Dominator -->
                    sVehicles.Add("IMPALER2"); //<!-- Apocalypse Impaler -->
                    sVehicles.Add("IMPERATOR"); //<!-- Apocalypse Imperator -->
                    sVehicles.Add("SLAMVAN4"); //<!-- Apocalypse Slamvan -->
                    sVehicles.Add("DOMINATOR5"); //<!-- Future Shock Dominator -->
                    sVehicles.Add("IMPALER3"); //<!-- Future Shock Impaler -->
                    sVehicles.Add("IMPERATOR2"); //<!-- Future Shock Imperator -->
                    sVehicles.Add("SLAMVAN5"); //<!-- Future Shock Slamvan -->
                    sVehicles.Add("DOMINATOR6"); //<!-- Nightmare Dominator -->
                    sVehicles.Add("IMPALER4"); //<!-- Nightmare Impaler -->
                    sVehicles.Add("IMPERATOR3"); //<!-- Nightmare Imperator -->
                    sVehicles.Add("SLAMVAN6"); //<!-- Nightmare Slamvan -->
                    sVehicles.Add("CERBERUS"); //<!-- Apocalypse Cerberus -->
                    sVehicles.Add("CERBERUS2"); //<!-- Future Shock Cerberus -->
                    sVehicles.Add("CERBERUS3"); //<!-- Nightmare Cerberus -->
                    sVehicles.Add("ISSI4"); //<!-- Apocalypse Issi -->
                    sVehicles.Add("ISSI5"); //<!-- Future Shock Issi -->
                    sVehicles.Add("ISSI6"); //<!-- Nightmare Issi -->
                    sVehicles.Add("BRUISER"); //<!-- Apocalypse Bruiser -->
                    sVehicles.Add("BRUTUS"); //<!-- Apocalypse Brutus -->
                    sVehicles.Add("MONSTER3"); //<!-- Apocalypse Sasquatch -->
                    sVehicles.Add("BRUISER2"); //<!-- Future Shock Bruiser -->
                    sVehicles.Add("BRUTUS2"); //<!-- Future Shock Brutus -->
                    sVehicles.Add("MONSTER4"); //<!-- Future Shock Sasquatch -->
                    sVehicles.Add("BRUISER3"); //<!-- Nightmare Bruiser -->
                    sVehicles.Add("BRUTUS3"); //<!-- Nightmare Brutus -->
                    sVehicles.Add("MONSTER5"); //<!-- Nightmare Sasquatch -->
                    sVehicles.Add("SCARAB"); //<!-- Apocalypse Scarab -->
                    sVehicles.Add("SCARAB2"); //<!-- Future Shock Scarab -->
                    sVehicles.Add("SCARAB3"); //<!-- Nightmare Scarab -->
                }
            }       //Millatary / SaintsRow
            else if (iVechList == 11)
            {
                sVehicles.Add("KURUMA2"); //<!-- Kuruma (Armored) -->
                sVehicles.Add("RHINO"); //
                sVehicles.Add("KHANJALI"); //<!-- TM-02 Khanjali -->
                sVehicles.Add("INSURGENT"); //
                sVehicles.Add("INSURGENT2"); //<!-- Insurgent Pick-Up -->
                sVehicles.Add("INSURGENT3"); //<!-- Insurgent Pick-Up Custom -->
                sVehicles.Add("CHERNOBOG"); //
                sVehicles.Add("APC"); //
                sVehicles.Add("RIOT2"); //<!-- RCV -->
            }       //Tanks and Rockets
            else if (iVechList == 12)
            {
                sVehicles.Add("STRIKEFORCE"); //<!-- B-11 Strikeforce -->
                sVehicles.Add("HYDRA"); //
                sVehicles.Add("STARLING"); //<!-- LF-22 Starling -->
                sVehicles.Add("LAZER"); //<!-- P-996 LAZER -->
                sVehicles.Add("PYRO"); //
                sVehicles.Add("ROGUE"); //
                sVehicles.Add("MOLOTOK"); //<!-- V-65 Molotok -->
                sVehicles.Add("volatol"); //
                sVehicles.Add("bombushka");
                sVehicles.Add("seabreeze");
                sVehicles.Add("nokota");
                sVehicles.Add("mogul");
                sVehicles.Add("microlight");
                sVehicles.Add("howard");
                sVehicles.Add("tula");
                sVehicles.Add("alphaz1");
            }       //Attack Planes
            else if (iVechList == 13)
            {
                sVehicles.Add("BUZZARD"); //<!-- Buzzard Attack Chopper -->
                sVehicles.Add("HUNTER"); //<!-- FH-1 Hunter -->
                sVehicles.Add("SAVAGE"); //
                if (DataStore.bTrainM)
                {
                    sVehicles.Add("seasparrow2"); //
                    sVehicles.Add("seasparrow");
                    sVehicles.Add("akula");
                    sVehicles.Add("havok");
                }
            }       //Attack Helicopters
            else
            {
                sVehicles.Add("BJXL"); //
            }

            if (sVehicles.Count == 0)
                sVehicles.Add("BJXL");

            return sVehicles[ReturnValues.RandInt(0, sVehicles.Count - 1)];
        }
        public static Vehicle VehicleSpawn(string sVehModel, Vector3 VecLocal, float VecHead, bool bAddPlayer, int iAddtoBrain, bool bAirVeh, bool bAsProp)
        {
            LoggerLight.GetLogging("VehActions.VehicleSpawn, sVehModel == " + sVehModel + ", bAddPlayer == " + bAddPlayer);

            Vehicle BuildVehicle = null;

            if (!IsItARealVehicle(sVehModel))
                sVehModel = "MAMBA";

            int iVehHash = Function.Call<int>(Hash.GET_HASH_KEY, sVehModel);

            if (Function.Call<bool>(Hash.IS_MODEL_IN_CDIMAGE, iVehHash) && Function.Call<bool>(Hash.IS_MODEL_A_VEHICLE, iVehHash))
            {
                Function.Call(Hash.REQUEST_MODEL, iVehHash);
                while (!Function.Call<bool>(Hash.HAS_MODEL_LOADED, iVehHash))
                    Script.Wait(1);

                BuildVehicle = Function.Call<Vehicle>(Hash.CREATE_VEHICLE, iVehHash, VecLocal.X, VecLocal.Y, VecLocal.Z, VecHead, true, true);

                if (!bAsProp)
                {
                    BuildVehicle.IsPersistent = true;

                    if (bAddPlayer)
                    {
                        PedActions.GenPlayerPed(BuildVehicle.Position, BuildVehicle.Heading, BuildVehicle, -1, -1, "");

                        if (DataStore.iCurrentPlayerz + 7 < DataStore.MySettings.iMaxPlayers && ReturnValues.RandInt(0, 20) < 5)
                        {
                            for (int i = 0; i < BuildVehicle.PassengerSeats - 1; i++)
                                PedActions.GenPlayerPed(BuildVehicle.Position, BuildVehicle.Heading, BuildVehicle, i, -1, "");
                        }
                    }
                    else
                    {
                        int ThisBrain = PedActions.ReteaveBrain(iAddtoBrain);
                        DataStore.PedList[ThisBrain].ThisVeh = BuildVehicle;
                        PedActions.WarptoAnyVeh(BuildVehicle, DataStore.PedList[ThisBrain].ThisPed, -1);
                        DataStore.PedList[ThisBrain].EnterVehQue = false;
                        if (bAirVeh)
                            DataStore.PedList[ThisBrain].Pilot = true;
                        else
                            DataStore.PedList[ThisBrain].Driver = true;
                        if (DataStore.PedList[ThisBrain].ThisBlip != null)
                            ClearUp.ClearPedBlips(iAddtoBrain);

                        DataStore.PedList[ThisBrain].ThisBlip = BlipActions.PedBlimp(DataStore.PedList[ThisBrain].ThisPed, OhMyBlip(BuildVehicle), DataStore.PedList[ThisBrain].MyName, DataStore.PedList[ThisBrain].Colours);
                        PedActions.DriveTooo(DataStore.PedList[ThisBrain].ThisPed, false);
                    }
                    
                    if (bAirVeh)
                        MaxOutAllModsNoWheels(BuildVehicle);
                    else
                        MaxModsRandExtras(BuildVehicle);
                }


                Function.Call(Hash.SET_MODEL_AS_NO_LONGER_NEEDED, iVehHash);
            }
            else
                BuildVehicle = null;

            return BuildVehicle;
        }
        private static void MaxModsRandExtras(Vehicle Vehic)
        {
            LoggerLight.GetLogging("VehActions.MaxModsRandExtras");

            bool bMotoBike = Vehic.ClassType == VehicleClass.Motorcycles;

            for (int i = 0; i < 50; i++)
            {
                int iSpoilher = Function.Call<int>(Hash.GET_NUM_VEHICLE_MODS, Vehic.Handle, i);

                if (i == 11 || i == 12 || i == 13 || i == 15 || i == 16)
                {
                    iSpoilher -= 1;
                    Function.Call(Hash.SET_VEHICLE_MOD, Vehic.Handle, i, iSpoilher, true);
                }
                else if (i == 18 || i == 22)
                    iSpoilher = -1;

                else if (i == 24)
                {
                    iSpoilher = Function.Call<int>(Hash.GET_VEHICLE_MOD, Vehic.Handle, 23);
                    if (bMotoBike)
                        Function.Call(Hash.SET_VEHICLE_MOD, Vehic.Handle, i, iSpoilher, true);

                }
                else
                {
                    if (iSpoilher != 0)
                        iSpoilher = ReturnValues.RandInt(0, iSpoilher - 1);

                    Function.Call(Hash.SET_VEHICLE_MOD, Vehic.Handle, i, ReturnValues.RandInt(0, iSpoilher - 1), true);
                }
            }
            if (Vehic.ClassType != VehicleClass.Cycles || Vehic.ClassType != VehicleClass.Helicopters || Vehic.ClassType != VehicleClass.Boats || Vehic.ClassType != VehicleClass.Planes)
            {
                Function.Call(Hash.TOGGLE_VEHICLE_MOD, Vehic, 18, true);
                Function.Call(Hash.TOGGLE_VEHICLE_MOD, Vehic, 22, true);
            }
        }
        private static void MaxOutAllModsNoWheels(Vehicle Vehic)
        {
            LoggerLight.GetLogging("VehActions.MaxOutAllModsNoWheels");

            Function.Call(Hash.SET_VEHICLE_MOD_KIT, Vehic.Handle, 0);
            for (int i = 0; i < 50; i++)
            {
                int iSpoilher = Function.Call<int>(Hash.GET_NUM_VEHICLE_MODS, Vehic.Handle, i);

                if (i == 18 || i == 22 || i == 23 || i == 24)
                {

                }
                else
                {
                    iSpoilher -= 1;
                    Function.Call(Hash.SET_VEHICLE_MOD, Vehic.Handle, i, iSpoilher, true);
                }
            }
            if (Vehic.ClassType != VehicleClass.Cycles || Vehic.ClassType != VehicleClass.Helicopters || Vehic.ClassType != VehicleClass.Boats || Vehic.ClassType != VehicleClass.Planes)
            {
                Function.Call(Hash.TOGGLE_VEHICLE_MOD, Vehic, 18, true);
                Function.Call(Hash.TOGGLE_VEHICLE_MOD, Vehic, 22, true);
            }
            Function.Call(Hash._SET_VEHICLE_LANDING_GEAR, Vehic.Handle, 3);
        }
        public static int OhMyBlip(Vehicle MyVehic)
        {
            LoggerLight.GetLogging("VehActions.OhMyBlip");

            int iVehHash = MyVehic.Model.GetHashCode();
            int iBeLip = 1;

            if (MyVehic.ClassType == VehicleClass.Boats)
                iBeLip = 427;
            else if (MyVehic.ClassType == VehicleClass.Helicopters)
                iBeLip = 422;
            else if (MyVehic.ClassType == VehicleClass.Motorcycles)
                iBeLip = 226;
            else if (MyVehic.ClassType == VehicleClass.Planes)
                iBeLip = 424;
            else
                iBeLip = 225;

            if (iVehHash == Function.Call<int>(Hash.GET_HASH_KEY, "CRUSADER"))
                iBeLip = 800;
            else if (iVehHash == Function.Call<int>(Hash.GET_HASH_KEY, "SLAMVAN3"))
                iBeLip = 799;
            else if (iVehHash == Function.Call<int>(Hash.GET_HASH_KEY, "VALKYRIE2") || iVehHash == Function.Call<int>(Hash.GET_HASH_KEY, "VALKYRIE"))
                iBeLip = 759;
            else if (iVehHash == Function.Call<int>(Hash.GET_HASH_KEY, "SQUADDIE"))
                iBeLip = 757;
            else if (iVehHash == Function.Call<int>(Hash.GET_HASH_KEY, "seasparrow2"))
                iBeLip = 753;
            else if (iVehHash == Function.Call<int>(Hash.GET_HASH_KEY, "WINKY"))
                iBeLip = 745;
            else if (iVehHash == Function.Call<int>(Hash.GET_HASH_KEY, "ZHABA"))
                iBeLip = 737;
            else if (iVehHash == Function.Call<int>(Hash.GET_HASH_KEY, "OUTLAW"))
                iBeLip = 735;
            else if (iVehHash == Function.Call<int>(Hash.GET_HASH_KEY, "VAGRANT"))
                iBeLip = 736;
            else if (iVehHash == Function.Call<int>(Hash.GET_HASH_KEY, "ZR380") || iVehHash == Function.Call<int>(Hash.GET_HASH_KEY, "ZR3802") || iVehHash == Function.Call<int>(Hash.GET_HASH_KEY, "ZR3803"))
                iBeLip = 669;
            else if (iVehHash == Function.Call<int>(Hash.GET_HASH_KEY, "SLAMVAN4") || iVehHash == Function.Call<int>(Hash.GET_HASH_KEY, "SLAMVAN5") || iVehHash == Function.Call<int>(Hash.GET_HASH_KEY, "SLAMVAN6"))
                iBeLip = 668;
            else if (iVehHash == Function.Call<int>(Hash.GET_HASH_KEY, "SCARAB") || iVehHash == Function.Call<int>(Hash.GET_HASH_KEY, "SCARAB2") || iVehHash == Function.Call<int>(Hash.GET_HASH_KEY, "SCARAB3"))
                iBeLip = 667;
            else if (iVehHash == Function.Call<int>(Hash.GET_HASH_KEY, "MONSTER3") || iVehHash == Function.Call<int>(Hash.GET_HASH_KEY, "MONSTER4") || iVehHash == Function.Call<int>(Hash.GET_HASH_KEY, "MONSTER5"))
                iBeLip = 666;
            else if (iVehHash == Function.Call<int>(Hash.GET_HASH_KEY, "ISSI4") || iVehHash == Function.Call<int>(Hash.GET_HASH_KEY, "ISSI5") || iVehHash == Function.Call<int>(Hash.GET_HASH_KEY, "ISSI6"))
                iBeLip = 665;
            else if (iVehHash == Function.Call<int>(Hash.GET_HASH_KEY, "IMPERATOR") || iVehHash == Function.Call<int>(Hash.GET_HASH_KEY, "IMPERATOR2") || iVehHash == Function.Call<int>(Hash.GET_HASH_KEY, "IMPERATOR3"))
                iBeLip = 664;
            else if (iVehHash == Function.Call<int>(Hash.GET_HASH_KEY, "IMPALER2") || iVehHash == Function.Call<int>(Hash.GET_HASH_KEY, "IMPALER3") || iVehHash == Function.Call<int>(Hash.GET_HASH_KEY, "IMPALER4"))
                iBeLip = 663;
            else if (iVehHash == Function.Call<int>(Hash.GET_HASH_KEY, "DOMINATOR4") || iVehHash == Function.Call<int>(Hash.GET_HASH_KEY, "DOMINATOR5") || iVehHash == Function.Call<int>(Hash.GET_HASH_KEY, "DOMINATOR6"))
                iBeLip = 662;
            else if (iVehHash == Function.Call<int>(Hash.GET_HASH_KEY, "CERBERUS") || iVehHash == Function.Call<int>(Hash.GET_HASH_KEY, "CERBERUS2") || iVehHash == Function.Call<int>(Hash.GET_HASH_KEY, "CERBERUS3"))
                iBeLip = 660;
            else if (iVehHash == Function.Call<int>(Hash.GET_HASH_KEY, "BRUTUS") || iVehHash == Function.Call<int>(Hash.GET_HASH_KEY, "BRUTUS2") || iVehHash == Function.Call<int>(Hash.GET_HASH_KEY, "BRUTUS3"))
                iBeLip = 659;
            else if (iVehHash == Function.Call<int>(Hash.GET_HASH_KEY, "BRUISER") || iVehHash == Function.Call<int>(Hash.GET_HASH_KEY, "BRUISER2") || iVehHash == Function.Call<int>(Hash.GET_HASH_KEY, "BRUISER3"))
                iBeLip = 658;
            else if (iVehHash == Function.Call<int>(Hash.GET_HASH_KEY, "STRIKEFORCE"))
                iBeLip = 640;
            else if (iVehHash == Function.Call<int>(Hash.GET_HASH_KEY, "OPPRESSOR2"))
                iBeLip = 639;
            else if (iVehHash == Function.Call<int>(Hash.GET_HASH_KEY, "SCRAMJET"))
                iBeLip = 634;
            else if (iVehHash == Function.Call<int>(Hash.GET_HASH_KEY, "seasparrow"))
                iBeLip = 612;
            else if (iVehHash == Function.Call<int>(Hash.GET_HASH_KEY, "APC"))
                iBeLip = 603;
            else if (iVehHash == Function.Call<int>(Hash.GET_HASH_KEY, "akula"))
                iBeLip = 602;
            else if (iVehHash == Function.Call<int>(Hash.GET_HASH_KEY, "volatol"))
                iBeLip = 600;
            else if (iVehHash == Function.Call<int>(Hash.GET_HASH_KEY, "KHANJALI"))
                iBeLip = 598;
            else if (iVehHash == Function.Call<int>(Hash.GET_HASH_KEY, "DELUXO"))
                iBeLip = 596;
            else if (iVehHash == Function.Call<int>(Hash.GET_HASH_KEY, "bombushka"))
                iBeLip = 585;
            else if (iVehHash == Function.Call<int>(Hash.GET_HASH_KEY, "seabreeze"))
                iBeLip = 584;
            else if (iVehHash == Function.Call<int>(Hash.GET_HASH_KEY, "STARLING"))
                iBeLip = 583;
            else if (iVehHash == Function.Call<int>(Hash.GET_HASH_KEY, "ROGUE"))
                iBeLip = 582;
            else if (iVehHash == Function.Call<int>(Hash.GET_HASH_KEY, "PYRO"))
                iBeLip = 581;
            else if (iVehHash == Function.Call<int>(Hash.GET_HASH_KEY, "nokota"))
                iBeLip = 580;
            else if (iVehHash == Function.Call<int>(Hash.GET_HASH_KEY, "MOLOTOK"))
                iBeLip = 579;
            else if (iVehHash == Function.Call<int>(Hash.GET_HASH_KEY, "mogul"))
                iBeLip = 578;
            else if (iVehHash == Function.Call<int>(Hash.GET_HASH_KEY, "microlight"))
                iBeLip = 577;
            else if (iVehHash == Function.Call<int>(Hash.GET_HASH_KEY, "HUNTER"))
                iBeLip = 576;
            else if (iVehHash == Function.Call<int>(Hash.GET_HASH_KEY, "howard"))
                iBeLip = 575;
            else if (iVehHash == Function.Call<int>(Hash.GET_HASH_KEY, "havok"))
                iBeLip = 574;
            else if (iVehHash == Function.Call<int>(Hash.GET_HASH_KEY, "tula"))
                iBeLip = 573;
            else if (iVehHash == Function.Call<int>(Hash.GET_HASH_KEY, "alphaz1"))
                iBeLip = 572;
            else if (iVehHash == Function.Call<int>(Hash.GET_HASH_KEY, "DUNE3"))
                iBeLip = 561;
            else if (iVehHash == Function.Call<int>(Hash.GET_HASH_KEY, "HALFTRACK"))
                iBeLip = 560;
            else if (iVehHash == Function.Call<int>(Hash.GET_HASH_KEY, "OPPRESSOR"))
                iBeLip = 559;
            else if (iVehHash == Function.Call<int>(Hash.GET_HASH_KEY, "TECHNICAL2"))
                iBeLip = 534;
            else if (iVehHash == Function.Call<int>(Hash.GET_HASH_KEY, "voltic2"))
                iBeLip = 533;
            else if (iVehHash == Function.Call<int>(Hash.GET_HASH_KEY, "wastelander"))
                iBeLip = 532;
            else if (iVehHash == Function.Call<int>(Hash.GET_HASH_KEY, "DUNE4") || iVehHash == Function.Call<int>(Hash.GET_HASH_KEY, "DUNE5"))
                iBeLip = 531;
            else if (iVehHash == Function.Call<int>(Hash.GET_HASH_KEY, "RUINER2"))
                iBeLip = 530;
            else if (iVehHash == Function.Call<int>(Hash.GET_HASH_KEY, "PHANTOM2"))
                iBeLip = 528;
            else if (iVehHash == Function.Call<int>(Hash.GET_HASH_KEY, "RHINO"))
                iBeLip = 421;

            return iBeLip;
        }
    }
}
