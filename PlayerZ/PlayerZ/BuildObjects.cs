using GTA;
using GTA.Math;
using GTA.Native;
using PlayerZero.Classes;
using System.Collections.Generic;
using System.IO;

namespace PlayerZero
{
	public class BuildObjects
	{
		private static readonly List<string> DropProplist = new List<string>
		{
			"prop_container_ld2",
			"prop_rail_boxcar5",
			"prop_rub_carwreck_12",
			"prop_ind_coalcar_01",
			"prop_rub_carwreck_13",
			"prop_cablespool_01b",
			"prop_pipes_02b",
			"prop_container_01d",
			"prop_container_05a",
			"prop_gascyl_04a"
		};
		private static readonly List<string> PreVeh_01 = new List<string>
		{
			"PFISTER811", //><!-- 811 -->
	        "ADDER", //>
	        "AUTARCH", //>
	        "BANSHEE2", //><!-- Banshee 900R -->
	        "OPENWHEEL1", //><!-- BR8, should be Open Wheel class -->
	        "BULLET", //>
	        "CHAMPION", //>
	        "CHEETAH", //>
	        "CYCLONE", //>
	        "DEVESTE", //>
	        "OPENWHEEL2", //><!-- DR1, should be Open Wheel class -->
	        "EMERUS", //>
	        "ENTITYXF", //>
	        "ENTITY2", //><!-- Entity XXR -->
	        "SHEAVA", //><!-- ETR1 -->
	        "FMJ", //>
	        "FURIA", //>
	        "GP1", //>
	        "IGNUS", //>
	        "INFERNUS", //>
	        "ITALIGTB", //>
	        "ITALIGTB2", //><!-- Itali GTB Custom -->
	        "KRIEGER", //>
	        "LM87", //>
	        "OSIRIS", //>
	        "NERO", //>
	        "NERO2", //><!-- Nero Custom -->
	        "PENETRATOR", //>
	        "FORMULA", //><!-- PR4, should be Open Wheel class -->
	        "FORMULA2", //><!-- R88, should be Open Wheel class -->
	        "LE7B", //><!-- RE-7B -->
	        "REAPER", //>
	        "VOLTIC2", //><!-- Rocket Voltic -->
	        "S80", //>
	        "SC1", //>
	        "SCRAMJET", //>
	        "SULTANRS", //>
	        "T20", //>
	        "TAIPAN", //>
	        "TEMPESTA", //>
	        "TEZERACT", //>
	        "THRAX", //>
	        "TIGON", //>
	        "TORERO2", //><!-- Torero XO -->
	        "TURISMOR", //>
	        "TYRANT", //>
	        "TYRUS", //>
	        "VACCA", //>
	        "VAGNER", //>
	        "VIGILANTE", //>
	        "VISIONE", //>
	        "VOLTIC", //>
	        "PROTOTIPO", //><!-- X80 Proto -->
	        "XA21", //>
	        "ZENO", //>
	        "ZENTORNO", //>
	        "ZORRUSSO", //>
	        "COGCABRIO", //>
	        "EXEMPLAR", //>
	        "F620", //>
	        "FELON", //>
	        "FELON2", //><!-- Felon GT -->
	        "JACKAL", //>
	        "KANJOSJ", //>
	        "ORACLE", //>
	        "ORACLE2", //><!-- Oracle XS -->
	        "POSTLUDE", //>
	        "PREVION", //>
	        "SENTINEL2", //><!-- Sentinel -->
	        "SENTINEL", //><!-- Sentinel XS -->
	        "WINDSOR", //>
	        "WINDSOR2", //><!-- Windsor Drop -->
	        "ZION", //>
	        "ZION2", //><!-- Zion Cabrio -->
	        "DRAFTER", //><!-- 8F Drafter -->
	        "NINEF", //>
	        "NINEF2", //><!-- 9F Cabrio -->
	        "TENF", //>
	        "TENF2", //><!-- 10F Widebody -->
	        "ALPHA", //>
	        "ZR380", //><!-- Apocalypse ZR380 -->
	        "BANSHEE", //>
	        "BESTIAGTS", //>
	        "BLISTA2", //><!-- Blista Compact -->
	        "BUFFALO", //>
	        "BUFFALO2", //><!-- Buffalo S -->
	        "CALICO", //><!-- Calico GTF -->
	        "CARBONIZZARE", //>
	        "COMET2", //><!-- Comet -->
	        "COMET3", //><!-- Comet Retro Custom -->
	        "COMET6", //><!-- Comet S2 -->
	        "COMET7", //><!-- Comet S2 Cabrio -->
	        "COMET4", //><!-- Comet Safari -->
	        "COMET5", //><!-- Comet SR -->
	        "COQUETTE", //>
	        "COQUETTE4", //><!-- Coquette D10 -->
	        "CORSITA", //>
	        "CYPHER", //>
	        "TAMPA2", //><!-- Drift Tampa -->
	        "ELEGY", //><!-- Elegy Retro Custom -->
	        "ELEGY2", //><!-- Elegy RH8 -->
	        "EUROS", //>
	        "FELTZER2", //><!-- Feltzer -->
	        "FLASHGT", //>
	        "FUROREGT", //>
	        "FUSILADE", //>
	        "FUTO", //>
	        "FUTO2", //><!-- Futo GTX -->
	        "ZR3802", //><!-- Future Shock ZR380 -->
	        "GB200", //>
	        "BLISTA3", //><!-- Go Go Monkey Blista -->
	        "GROWLER", //>
	        "HOTRING", //>
	        "IMORGON", //>
	        "ISSI7", //><!-- Issi Sport -->
	        "ITALIGTO", //>
	        "ITALIRSX", //>
	        "JESTER", //>
	        "JESTER2", //><!-- Jester (Racecar) -->
	        "JESTER3", //><!-- Jester Classic -->
	        "JESTER4", //><!-- Jester RR -->
	        "JUGULAR", //>
	        "KHAMELION", //>
	        "KOMODA", //>
	        "KURUMA", //>
	        "KURUMA2", //><!-- Kuruma (Armored) -->
	        "LOCUST", //>
	        "LYNX", //>
	        "MASSACRO", //>
	        "MASSACRO2", //><!-- Massacro (Racecar) -->
	        "NEO", //>
	        "NEON", //>
	        "ZR3803", //><!-- Nightmare ZR380 -->
	        "OMNIS", //>
	        "OMNISEGT", //>
	        "PARAGON", //>
	        "PARAGON2", //><!-- Paragon R (Armored) -->
	        "PARIAH", //>
	        "PENUMBRA", //>
	        "PENUMBRA2", //><!-- Penumbra FF -->
	        "RAIDEN", //>
	        "RAPIDGT", //>
	        "RAPIDGT2", //><!-- Rapid GT Cabrio -->
	        "RAPTOR", //>
	        "REMUS", //>
	        "REVOLTER", //>
	        "RT3000", //>
	        "RUSTON", //>
	        "SCHAFTER4", //><!-- Schafter LWB -->
	        "SCHAFTER3", //><!-- Schafter V12 -->
	        "SCHLAGEN", //>
	        "SCHWARZER", //>
	        "SENTINEL3", //><!-- Sentinel Classic -->
	        "SENTINEL4", //><!-- Sentinel Classic Widebody -->
	        "SEVEN70", //>
	        "SM722", //>
	        "SPECTER", //>
	        "SPECTER2", //><!-- Specter Custom -->
	        "BUFFALO3", //><!-- Sprunk Buffalo -->
	        "STREITER", //>
	        "SUGOI", //>
	        "SULTAN", //>
	        "SULTAN2", //><!-- Sultan Classic -->
	        "SULTAN3", //><!-- Sultan RS Classic -->
	        "SURANO", //>
	        "TROPOS", //>
	        "VSTR", //><!-- V-STR -->
	        "VECTRE", //>
	        "VERLIERER2", //>
	        "VETO", //><!-- Veto Classic -->
	        "VETO2", //><!-- Veto Modern -->
	        "ZR350", //>
	        "DOMINATOR4", //><!-- Apocalypse Dominator -->
	        "IMPALER2", //><!-- Apocalypse Impaler -->
	        "IMPERATOR", //><!-- Apocalypse Imperator -->
	        "SLAMVAN4", //><!-- Apocalypse Slamvan -->
	        "DUKES3", //><!-- Beater Dukes -->
	        "BLADE", //>
	        "BUCCANEER", //>
	        "BUCCANEER2", //><!-- Buccaneer Custom -->
	        "BUFFALO4", //><!-- Buffalo STX -->
	        "STALION2", //><!-- Burger Shot Stallion -->
	        "CHINO", //>
	        "CHINO2", //><!-- Chino Custom -->
	        "CLIQUE", //>
	        "COQUETTE3", //><!-- Coquette BlackFin -->
	        "DEVIANT", //>
	        "DOMINATOR", //>
	        "DOMINATOR7", //><!-- Dominator ASP -->
	        "DOMINATOR8", //><!-- Dominator GTT -->
	        "DOMINATOR3", //><!-- Dominator GTX -->
	        "YOSEMITE2", //><!-- Drift Yosemite -->
	        "DUKES2", //><!-- Duke O'Death -->
	        "DUKES", //>
	        "ELLIE", //>
	        "FACTION", //>
	        "FACTION2", //><!-- Faction Custom -->
	        "FACTION3", //><!-- Faction Custom Donk -->
	        "DOMINATOR5", //><!-- Future Shock Dominator -->
	        "IMPALER3", //><!-- Future Shock Impaler -->
	        "IMPERATOR2", //><!-- Future Shock Imperator -->
	        "SLAMVAN5", //><!-- Future Shock Slamvan -->
	        "GAUNTLET", //>
	        "GAUNTLET3", //><!-- Gauntlet Classic -->
	        "GAUNTLET5", //><!-- Gauntlet Classic Custom -->
	        "GAUNTLET4", //><!-- Gauntlet Hellfire -->
	        "GREENWOOD", //>
	        "HERMES", //>
	        "HOTKNIFE", //>
	        "HUSTLER", //>
	        "IMPALER", //>
	        "SLAMVAN2", //><!-- Lost Slamvan -->
	        "LURCHER", //>
	        "MANANA2", //><!-- Manana Custom -->
	        "MOONBEAM", //>
	        "MOONBEAM2", //><!-- Moonbeam Custom -->
	        "DOMINATOR6", //><!-- Nightmare Dominator -->
	        "IMPALER4", //><!-- Nightmare Impaler -->
	        "IMPERATOR3", //><!-- Nightmare Imperator -->
	        "SLAMVAN6", //><!-- Nightmare Slamvan -->
	        "NIGHTSHADE", //>
	        "PEYOTE2", //><!-- Peyote Gasser -->
	        "PHOENIX", //>
	        "PICADOR", //>
	        "DOMINATOR2", //><!-- Pisswasser Dominator -->
	        "RATLOADER", //>
	        "RATLOADER2", //><!-- Rat-Truck -->
	        "GAUNTLET2", //><!-- Redwood Gauntlet -->
	        "RUINER", //>
	        "RUINER3", //><!-- Ruiner 2000 wreck -->
	        "RUINER2", //><!-- Ruiner 2000 -->
	        "RUINER4", //><!-- Ruiner ZZ-8 -->
	        "SABREGT", //>
	        "SABREGT2", //><!-- Sabre Turbo Custom -->
	        "SLAMVAN", //>
	        "SLAMVAN3", //><!-- Slamvan Custom -->
	        "STALION", //>
	        "TAMPA", //>
	        "TULIP", //>
	        "VAMOS", //>
	        "VIGERO", //>
	        "VIGERO2", //><!-- Vigero ZX -->
	        "VIRGO", //>
	        "VIRGO3", //><!-- Virgo Classic -->
	        "VIRGO2", //><!-- Virgo Classic Custom -->
	        "VOODOO", //>
	        "VOODOO2", //><!-- Voodoo Custom -->
	        "TAMPA3", //><!-- Weaponized Tampa -->
	        "WEEVIL2", //><!-- Weevil Custom -->
	        "YOSEMITE", //>
	        "Z190", //><!-- 190z -->
	        "ARDENT", //>
	        "CASCO", //>
	        "CHEBUREK", //>
	        "CHEETAH2", //><!-- Cheetah Classic -->
	        "COQUETTE2", //><!-- Coquette Classic -->
	        "DELUXO", //>
	        "DYNASTY", //>
	        "FAGALOA", //>
	        "BTYPE2", //><!-- FrÃ¤nken Stange -->
	        "GT500", //>
	        "INFERNUS2", //><!-- Infernus Classic -->
	        "JB700", //>
	        "JB7002", //><!-- JB 700W -->
	        "MAMBA", //>
	        "MANANA", //>
	        "MICHELLI", //>
	        "MONROE", //>
	        "NEBULA", //>
	        "PEYOTE", //>
	        "PEYOTE3", //><!-- Peyote Custom -->
	        "PIGALLE", //>
	        "RAPIDGT3", //><!-- Rapid GT Classic -->
	        "RETINUE", //>
	        "RETINUE2", //><!-- Retinue MkII -->
	        "BTYPE", //><!-- Roosevelt -->
	        "BTYPE3", //><!-- Roosevelt Valor -->
	        "SAVESTRA", //>
	        "STINGER", //>
	        "STINGERGT", //>
	        "FELTZER3", //><!-- Stirling GT -->
	        "STROMBERG", //>
	        "SWINGER", //>
	        "TOREADOR", //>
	        "TORERO", //>
	        "TORNADO", //>
	        "TORNADO2", //><!-- Tornado Cabrio -->
	        "TORNADO3", //><!-- Rusty Tornado -->
	        "TORNADO4", //><!-- Mariachi Tornado -->
	        "TORNADO5", //><!-- Tornado Custom -->
	        "TORNADO6", //><!-- Tornado Rat Rod -->
	        "TURISMO2", //><!-- Turismo Classic -->
	        "VISERIS", //>
	        "ZTYPE", //>
	        "ZION3", //><!-- Zion Classic -->
	        "CERBERUS", //><!-- Apocalypse Cerberus -->
	        "CERBERUS2", //><!-- Future Shock Cerberus -->
	        "GUARDIAN", //>
	        "CERBERUS3", //><!-- Nightmare Cerberus -->
	        "ASEA", //>
	        "ASTEROPE", //>
	        "CINQUEMILA", //>
	        "COGNOSCENTI", //>
	        "COGNOSCENTI2", //><!-- Cognoscenti (Armored) -->
	        "COG55", //><!-- Cognoscenti 55 -->
	        "COG552", //><!-- Cognoscenti 55 (Armored) -->
	        "DEITY", //>
	        "EMPEROR", //>
	        "EMPEROR2", //><!-- Emperor beater variant -->
	        "FUGITIVE", //>
	        "GLENDALE", //>
	        "GLENDALE2", //><!-- Glendale Custom -->
	        "INGOT", //>
	        "INTRUDER", //>
	        "PREMIER", //>
	        "PRIMO", //>
	        "PRIMO2", //><!-- Primo Custom -->
	        "REGINA", //>
	        "RHINEHART", //>
	        "ROMERO", //>
	        "SCHAFTER2", //>
	        "SCHAFTER6", //><!-- Schafter LWB (Armored) -->
	        "SCHAFTER5", //><!-- Schafter V12 (Armored) -->
	        "STAFFORD", //>
	        "STANIER", //>
	        "STRATUM", //>
	        "STRETCH", //>
	        "SUPERD", //>
	        "SURGE", //>
	        "TAILGATER", //>
	        "TAILGATER2", //><!-- Tailgater S -->
	        "WARRENER", //>
	        "WARRENER2", //><!-- Warrener HKR -->
	        "WASHINGTON", //>
	        "BRUISER", //><!-- Apocalypse Bruiser -->
	        "BRUTUS", //><!-- Apocalypse Brutus -->
	        "MONSTER3", //><!-- Apocalypse Sasquatch -->
	        "BIFTA", //>
	        "BLAZER", //>
	        "BLAZER5", //><!-- Blazer Aqua -->
	        "BLAZER2", //><!-- Blazer Lifeguard -->
	        "BODHI2", //>
	        "BRAWLER", //>
	        "CARACARA2", //><!-- Caracara 4x4 -->
	        "TROPHYTRUCK2", //><!-- Desert Raid -->
	        "DRAUGUR", //>
	        "DUBSTA3", //><!-- Dubsta 6x6 -->
	        "DUNE", //>
	        "DUNE3", //><!-- Dune FAV -->
	        "DLOADER", //>
	        "EVERON", //>
	        "FREECRAWLER", //>
	        "BRUISER2", //><!-- Future Shock Bruiser -->
	        "BRUTUS2", //><!-- Future Shock Brutus -->
	        "MONSTER4", //><!-- Future Shock Sasquatch -->
	        "HELLION", //>
	        "BLAZER3", //><!-- Hot Rod Blazer -->
	        "BFINJECTION", //>
	        "KALAHARI", //>
	        "KAMACHO", //>
	        "MONSTER", //><!-- Liberator -->
	        "MARSHALL", //>
	        "MESA3", //><!-- Merryweather Mesa -->
	        "BRUISER3", //><!-- Nightmare Bruiser -->
	        "BRUTUS3", //><!-- Nightmare Brutus -->
	        "MONSTER5", //><!-- Nightmare Sasquatch -->
	        "OUTLAW", //>
	        "PATRIOT3", //><!-- Patriot Mil-Spec -->
	        "DUNE4", //><!-- Ramp Buggy mission variant -->
	        "DUNE5", //><!-- Ramp Buggy -->
	        "RANCHERXL", //>
	        "REBEL2", //>
	        "RIATA", //>
	        "REBEL", //><!-- Rusty Rebel -->
	        "SANDKING2", //><!-- Sandking SWB -->
	        "SANDKING", //><!-- Sandking XL -->
	        "DUNE2", //><!-- Space Docker -->
	        "BLAZER4", //><!-- Street Blazer -->
	        "TROPHYTRUCK", //>
	        "VAGRANT", //>
	        "VERUS", //>
	        "WINKY", //>
	        "YOSEMITE3", //><!-- Yosemite Rancher -->
	        "ZHABA", //>
	        "ASTRON", //>
	        "BALLER", //>
	        "BALLER2", //><!-- Baller 2nd gen variant -->
	        "BALLER3", //><!-- Baller LE -->
	        "BALLER5", //><!-- Baller LE (Armored) -->
	        "BALLER4", //><!-- Baller LE LWB -->
	        "BALLER6", //><!-- Baller LE LWB (Armored) -->
	        "BALLER7", //><!-- Baller ST -->
	        "BJXL", //>
	        "CAVALCADE", //>
	        "CAVALCADE2", //><!-- Cavalcade 2nd gen variant -->
	        "CONTENDER", //>
	        "DUBSTA", //>
	        "DUBSTA2", //><!-- Dubsta black variant -->
	        "FQ2", //>
	        "GRANGER", //>
	        "GRANGER2", //><!-- Granger 3600LX -->
	        "GRESLEY", //>
	        "HABANERO", //>
	        "HUNTLEY", //>
	        "IWAGEN", //>
	        "JUBILEE", //>
	        "LANDSTALKER", //>
	        "LANDSTALKER2", //><!-- Landstalker XL -->
	        "MESA", //>
	        "NOVAK", //>
	        "PATRIOT", //>
	        "PATRIOT2", //><!-- Patriot Stretch -->
	        "RADI", //>
	        "REBLA", //>
	        "ROCOTO", //>
	        "SEMINOLE", //>
	        "SEMINOLE2", //><!-- Seminole Frontier -->
	        "SERRANO", //>
	        "SQUADDIE", //>
	        "TOROS", //>
	        "XLS", //>
	        "XLS2", //><!-- XLS (Armored) -->
	        "ISSI4", //><!-- Apocalypse Issi -->
	        "ASBO", //>
	        "BLISTA", //>
	        "KANJO", //><!-- Blista Kanjo -->
	        "BRIOSO", //>
	        "BRIOSO2", //><!-- Brioso 300 -->
	        "BRIOSO3", //><!-- Brioso 300 Widebody -->
	        "CLUB", //>
	        "DILETTANTE", //>
	        "ISSI5", //><!-- Future Shock Issi -->
	        "ISSI2", //>
	        "ISSI3", //><!-- Issi Classic -->
	        "ISSI6", //><!-- Nightmare Issi -->
	        "PANTO", //>
	        "PRAIRIE", //>
	        "RHAPSODY", //>
	        "WEEVIL", //>
	        "CONTENDER", //>
	        "DUBSTA3", //><!-- Dubsta 6x6 -->
	        "GUARDIAN", //>
	        "PICADOR", //>
	        "SADLER", //>
	        "SLAMVAN", //>
	        "SLAMVAN3", //><!-- Slamvan Custom -->
	        "YOSEMITE", //>
	        "BOXVILLE5", //><!-- Armored Boxville -->
	        "BISON", //>
	        "BOBCATXL", //>
	        "BURRITO", //>
	        "CAMPER", //>
	        "GBURRITO", //><!-- Gang Burrito Lost MC variant -->
	        "GBURRITO2", //><!-- Gang Burrito heist variant -->
	        "JOURNEY", //>
	        "MINIVAN", //>
	        "MINIVAN2", //><!-- Minivan Custom -->
	        "PARADISE", //>
	        "PONY", //>
	        "PONY2", //><!-- Pony Smoke on the Water variant -->
	        "RUMPO", //>
	        "RUMPO2", //><!-- Rumpo Deludamol variant -->
	        "RUMPO3", //><!-- Rumpo Custom -->
	        "SPEEDO", //>
	        "SPEEDO4", //><!-- Speedo Custom -->
	        "SURFER", //>
	        "SURFER2", //><!-- Surfer beater variant -->
	        "TACO", //>
	        "YOUGA", //>
	        "YOUGA2", //><!-- Youga Classic -->
	        "YOUGA3", //><!-- Youga Classic 4x4 -->
	        "YOUGA4", //><!-- Youga Custom -->
	        "BRICKADE", //>
	        "RALLYTRUCK", //><!-- Dune -->
	        "SLAMTRUCK", //>
	        "WASTLNDR",
			"AKUMA", //>
	        "DEATHBIKE", //><!-- Apocalypse Deathbike -->
	        "AVARUS", //>
	        "BAGGER", //>
	        "BATI", //>
	        "BATI2", //><!-- Bati 801RR -->
	        "BF400", //>
	        "CARBONRS", //>
	        "CHIMERA", //>
	        "CLIFFHANGER", //>
	        "DAEMON", //><!-- Daemon Lost MC variant -->
	        "DAEMON2", //><!-- Daemon Bikers DLC variant -->
	        "DEFILER", //>
	        "DIABLOUS", //>
	        "DIABLOUS2", //><!-- Diabolus Custom -->
	        "DOUBLE", //>
	        "ENDURO", //>
	        "ESSKEY", //>
	        "FAGGIO2", //>
	        "FAGGIO3", //><!-- Faggio Mod -->
	        "FAGGIO", //><!-- Faggio Sport -->
	        "FCR", //>
	        "FCR2", //><!-- FCR 1000 Custom -->
	        "DEATHBIKE2", //><!-- Future Shock Deathbike -->
	        "GARGOYLE", //>
	        "HAKUCHOU", //>
	        "HAKUCHOU2", //><!-- Hakuchou Drag -->
	        "HEXER", //>
	        "INNOVATION", //>
	        "LECTRO", //>
	        "MANCHEZ", //>
	        "MANCHEZ2", //><!-- Manchez Scout -->
	        "NEMESIS", //>
	        "NIGHTBLADE", //>
	        "DEATHBIKE3", //><!-- Nightmare Deathbike -->
	        "PCJ", //>
	        "RROCKET", //><!-- Rampant Rocket -->
	        "RATBIKE", //>
	        "REEVER", //>
	        "RUFFIAN", //>
	        "SANCHEZ", //><!-- Sanchez livery variant -->
	        "SANCHEZ2", //>
	        "SANCTUS", //>
	        "SHINOBI", //>
	        "SHOTARO", //>
	        "SOVEREIGN", //>
	        "STRYDER", //>
	        "THRUST", //>
	        "VADER", //>
	        "VINDICATOR", //>
	        "VORTEX", //>
	        "WOLFSBANE", //>
	        "ZOMBIEA", //><!-- Zombie Bobber -->
	        "ZOMBIEB" //><!-- Zombie Chopper -->  
        };
		private static readonly List<string> PreVeh_02 = new List<string>
		{
			"BUZZARD2", //><!-- Buzzard -->
	        "CARGOBOB", //><!-- Military Cargobob -->
	        "CARGOBOB2", //><!-- Jetsam Cargobob -->
	        "CONADA", //>
	        "FROGGER", //>
	        "FROGGER2", //><!-- Frogger Trevor Philips Industries variant -->
	        "MAVERICK", //>
	        "POLMAV", //>
	        "SUPERVOLITO", //>
	        "SUPERVOLITO2", //><!-- SuperVolito Carbon -->
	        "SWIFT", //>
	        "SWIFT2", //><!-- Swift Deluxe -->
	        "VOLATUS", //>
			"HAVOK" //>
        };
		private static readonly List<string> PreVeh_03 = new List<string>
		{
			"CUBAN800", //>
	        "DODO", //>
	        "DUSTER", //>
	        "LUXOR", //>
	        "LUXOR2", //><!-- Luxor Deluxe -->
	        "MAMMATUS", //>
	        "MILJET", //>
	        "NIMBUS", //>
	        "BOMBUSHKA", //><!-- RM-10 Bombushka -->
	        "ALKONOST", //><!-- RO-86 Alkonost -->
	        "SHAMAL", //>
	        "TITAN", //>
	        "VELUM", //>
	        "VELUM2", //><!-- Velum 5-Seater -->
	        "VESTRA", //>
	        "VOLATOL", //>
			"ALPHAZ1", //>
	        "BESRA", //>
	        "HOWARD", //><!-- Howard NX-25 -->
	        "STUNT" //><!-- Mallard -->
        };
		private static readonly List<string> PreVeh_04 = new List<string>
		{
			"AKULA", //>
	        "ANNIHILATOR", //>
	        "ANNIHILATOR2", //><!-- Annihilator Stealth -->
	        "BUZZARD", //><!-- Buzzard Attack Chopper -->
	        "HUNTER", //><!-- FH-1 Hunter -->
	        "SAVAGE", //>
	        "SEASPARROW", //>
	        "SEASPARROW2", //><!-- Sparrow -->
	        "VALKYRIE", //>
	        "VALKYRIE2", //><!-- Valkyrie MOD.0 -->
			"HAVOK" //>
        };
		private static readonly List<string> PreVeh_05 = new List<string>
		{
			"MOGUL", //>
	        "PYRO", //>
	        "SEABREEZE", //>
	        "TULA", //>
			"STRIKEFORCE", //><!-- B-11 Strikeforce -->
	        "HYDRA", //>
	        "STARLING", //><!-- LF-22 Starling -->
	        "NOKOTA", //><!-- P-45 Nokota -->
	        "LAZER", //><!-- P-996 LAZER -->
	        "ROGUE", //>
	        "MICROLIGHT", //><!-- Ultralight -->
	        "MOLOTOK" //><!-- V-65 Molotok -->
        };
		private static readonly List<string> PreVeh_06 = new List<string>
		{
			"LIMO2", //><!-- Turreted Limo -->
	        "INSURGENT", //>
	        "INSURGENT2", //><!-- Insurgent Pick-Up -->
	        "INSURGENT3", //><!-- Insurgent Pick-Up Custom -->
	        "NIGHTSHARK", //>
	        "CARACARA", //>
	        "MENACER", //>
	        "TECHNICAL", //>
	        "TECHNICAL2", //><!-- Technical Aqua -->
	        "TECHNICAL3", //><!-- Technical Custom -->
	        "SCARAB", //><!-- Apocalypse Scarab -->
	        "APC", //>
	        "HALFTRACK", //>
	        "SCARAB2", //><!-- Future Shock Scarab -->
	        "SCARAB3", //><!-- Nightmare Scarab -->
	        "RIOT2", //><!-- RCV -->
	        "RHINO", //>
	        "KHANJALI" //><!-- TM-02 Khanjali -->
        };
		private static readonly List<string> PreVeh_07 = new List<string>
		{
			"freight",
			"freightcar",
			"freightcont1",
			"freightcont2",
			"freightgrain",
			"metrotrain",
			"tankercar",
			"ripley",
			"tanker",
			"armytrailer2",
			"armytanker",
			"cablecar",
			"alkonost",
			"volatol",
			"jet",
			"cargoplane",
			"cutter",
			"mixer2",
			"tiptruck2",
			"bulldozer",
			"dump",
			"bus",
			"coach"
		};
		private static readonly List<string> SeatTest = new List<string>
		{
			"CUBAN800", //>
	        "DODO", //>
	        "DUSTER", //>
	        "LUXOR", //>
	        "LUXOR2", //><!-- Luxor Deluxe -->
	        "MAMMATUS", //>
	        "MILJET", //>
			"ROGUE",
			"NIMBUS", //>
	        "SHAMAL", //>
	        "VELUM", //>
	        "VELUM2", //><!-- Velum 5-Seater -->
	        "VESTRA", //>
			"MOGUL", //>
	        "SEABREEZE"
		};

		private static readonly List<VehBlips> vehBlips = new List<VehBlips>
		{
			new VehBlips("BUFFALO4",825),
			new VehBlips("CHAMPION",824),
			new VehBlips("DEITY",823),
			new VehBlips("GRANGER2",821),
			new VehBlips("JUBILEE",820),
			new VehBlips("PATRIOT3",818),
			new VehBlips("CRUSADER",800),
			new VehBlips("SLAMVAN2",799),
			new VehBlips("SLAMVAN3", 799),
			new VehBlips("VALKYRIE", 759),
			new VehBlips("VALKYRIE2", 759),
			new VehBlips("SQUADDIE", 757),
			new VehBlips("ardent", 756),///Retro Sport...       
            new VehBlips("cheetah2", 756),///Retro Sport...      
            new VehBlips("infernus2", 756),///Retro Sport...      
            new VehBlips("monroe", 756),///Retro Sport...      
            new VehBlips("stromberg", 756),///Retro Sport...      
            new VehBlips("torero", 756),///Retro Sport...      
            new VehBlips("turismo2", 756),///Retro Sport...      
            new VehBlips("toreador", 756),///Retro Sport...         
            new VehBlips("PATROLBOAT",755),
			new VehBlips("DINGHY5",754),
			new VehBlips("DINGHY5",754),
			new VehBlips("seasparrow2", 753),
			new VehBlips("barracks", 750),
			new VehBlips("barracks3", 750),
			new VehBlips("verus", 749),
			new VehBlips("veto2", 748),
			new VehBlips("veto", 747),
			new VehBlips("avisa", 746),
			new VehBlips("WINKY", 745),
			new VehBlips("ZHABA", 737),
			new VehBlips("VAGRANT", 736),
			new VehBlips("OUTLAW", 735),
			new VehBlips("everon", 734),
			new VehBlips("formula",726),
			new VehBlips("formula2",726),
			new VehBlips("openwheel1",726),
			new VehBlips("openwheel2",726),
			new VehBlips("STRETCH",724),
			new VehBlips("ZR380", 669),
			new VehBlips("ZR3802", 669),
			new VehBlips("ZR3803", 669),
			new VehBlips("SLAMVAN4", 668),
			new VehBlips("SLAMVAN5", 668),
			new VehBlips("SLAMVAN6", 668),
			new VehBlips("SCARAB", 667),
			new VehBlips("SCARAB2", 667),
			new VehBlips("SCARAB3", 667),
			new VehBlips("MONSTER3", 666),
			new VehBlips("MONSTER4", 666),
			new VehBlips("MONSTER5", 666),
			new VehBlips("ISSI4", 665),
			new VehBlips("ISSI5", 665),
			new VehBlips("ISSI6", 665),
			new VehBlips("IMPERATOR", 664),
			new VehBlips("IMPERATOR2", 664),
			new VehBlips("IMPERATOR3", 664),
			new VehBlips("IMPALER2", 663),
			new VehBlips("IMPALER3", 663),
			new VehBlips("IMPALER4", 663),
			new VehBlips("DOMINATOR4", 662),
			new VehBlips("DOMINATOR5", 662),
			new VehBlips("DOMINATOR6", 662),
			new VehBlips("CERBERUS", 660),
			new VehBlips("CERBERUS2", 660),
			new VehBlips("CERBERUS3", 660),
			new VehBlips("BRUTUS", 659),
			new VehBlips("BRUTUS2", 659),
			new VehBlips("BRUTUS3", 659),
			new VehBlips("BRUISER", 658),
			new VehBlips("BRUISER2", 658),
			new VehBlips("BRUISER3", 658),
			new VehBlips("STRIKEFORCE", 640),
			new VehBlips("OPPRESSOR2", 639),
			new VehBlips("SPEEDO4", 637),
			new VehBlips("MULE4", 636),
			new VehBlips("POUNDER2", 635),
			new VehBlips("scramjet", 634),
			new VehBlips("ambulance",632),
			new VehBlips("pbus2",631),
			new VehBlips("caracara",613),
			new VehBlips("seasparrow", 612),
			new VehBlips("APC", 603),
			new VehBlips("akula", 602),
			new VehBlips("insurgent", 601),
			new VehBlips("insurgent3", 601),
			new VehBlips("volatol", 600),
			new VehBlips("alkonost", 600),
			new VehBlips("KHANJALI", 598),
			new VehBlips("DELUXO",  596),
			new VehBlips("bombushka", 585),
			new VehBlips("seabreeze", 584),
			new VehBlips("STARLING", 583),
			new VehBlips("ROGUE", 582),
			new VehBlips("PYRO", 581),
			new VehBlips("nokota", 580),
			new VehBlips("MOLOTOK", 579),
			new VehBlips("mogul", 578),
			new VehBlips("microlight", 577),
			new VehBlips("HUNTER", 576),
			new VehBlips("howard", 575),
			new VehBlips("havok", 574),
			new VehBlips("tula", 573),
			new VehBlips("alphaz1", 572),
			new VehBlips("tampa3", 562),//truureted something
            new VehBlips("DUNE3", 561),
			new VehBlips("HALFTRACK", 560),
			new VehBlips("OPPRESSOR", 559),
            //new VehBlips("", 558),//something somthing turreted
            new VehBlips("TECHNICAL2", 534),
			new VehBlips("voltic2", 533),
			new VehBlips("wastelander", 532),
			new VehBlips("DUNE4", 531),
			new VehBlips("DUNE5", 531),
			new VehBlips("RUINER2", 530),
			new VehBlips("PHANTOM2", 528),
			new VehBlips("hakuchou",522),//ModSportsBike
            new VehBlips("hakuchou2",522),//ModSportsBike
            new VehBlips("shotaro",522),//ModSportsBike
            new VehBlips("pbus",513),//BusssAny
            new VehBlips("blazer",512),//QuadAny
            new VehBlips("blazer2",512),//QuadAny
            new VehBlips("blazer3",512),//QuadAny
            new VehBlips("blazer4",512),//QuadAny
            new VehBlips("blazer5",512),//QuadAny
            new VehBlips("cargobob", 481),
			new VehBlips("cargobob2", 481),
			new VehBlips("cargobob3", 481),
			new VehBlips("cargobob4", 481),
			new VehBlips("armytanker", 479),//Trailler Box
            new VehBlips("armytrailer", 479),//Trailler Box
            new VehBlips("baletrailer", 479),//Trailler Box
            new VehBlips("boattrailer", 479),//Trailler Box
            new VehBlips("docktrailer", 479),//Trailler Box
            new VehBlips("freighttrailer", 479),//Trailler Box
            new VehBlips("graintrailer", 479),//Trailler Box
            new VehBlips("tr2", 479),//Trailler Box
            new VehBlips("tr3", 479),//Trailler Box
            new VehBlips("tr4", 479),//Trailler Box
            new VehBlips("trflat", 479),//Trailler Box
            new VehBlips("tvtrailer", 479),//Trailler Box
            new VehBlips("tanker", 479),//Trailler Box
            new VehBlips("tanker2", 479),//Trailler Box
            new VehBlips("trailerlarge", 479),//Trailler Box
            new VehBlips("trailerlogs", 479),//Trailler Box
            new VehBlips("trailersmall", 479),//Trailler Box
            new VehBlips("tanker", 479),//Trailler Box
            new VehBlips("trailers", 479),//Trailler Box
            new VehBlips("trailers2", 479),//Trailler Box
            new VehBlips("trailers3", 479),//Trailler Box
            new VehBlips("trailers4", 479),//Trailler Box
            new VehBlips("barracks2", 477),//Truck any---not intrucks
            new VehBlips("scrap", 477),//Truck any---not intrucks
            new VehBlips("towtruck", 477),//Truck any---not intrucks
            new VehBlips("rallytruck", 477),//Truck any---not intrucks
            new VehBlips("brickade", 477),//Truck any---not intrucks
            new VehBlips("firetruk",477),
			new VehBlips("seashark", 471),//Seashark
            new VehBlips("seashark2", 471),//Seashark
            new VehBlips("seashark3", 471),//Seashark
            new VehBlips("limo2", 460),
			new VehBlips("technical", 426),
			new VehBlips("technical3", 426),
			new VehBlips("RHINO", 421),
			new VehBlips("marquis",410),
			new VehBlips("avarus",348),//lost bike
            new VehBlips("chimera",348),//lost bike
            new VehBlips("cliffhanger",348),//lost bike
            new VehBlips("daemon",348),//lost bike
            new VehBlips("daemon2",348),//lost bike
            new VehBlips("gargoyle",348),//lost bike
            new VehBlips("hexer",348),//lost bike
            new VehBlips("innovation",348),//lost bike
            new VehBlips("ratbike",348),//lost bike
            new VehBlips("sanctus",348),//lost bike
            new VehBlips("wolfsbane",348),//lost bike
            new VehBlips("zombiea",348),//lost bike
            new VehBlips("zombieb",348),//lost bike
            new VehBlips("trash",318),
			new VehBlips("trash2",318),
			new VehBlips("jet",307),
			new VehBlips("luxor",307),
			new VehBlips("luxor2",307),
			new VehBlips("miljet",307),
			new VehBlips("nimbus",307),
			new VehBlips("shamal",307),
			new VehBlips("vestra",307),
			new VehBlips("velum",251),
			new VehBlips("velum2",251),
			new VehBlips("stunt",251),
			new VehBlips("mammatus",251),
			new VehBlips("duster",251),
			new VehBlips("dodo",251),
			new VehBlips("cuban800",251),
			new VehBlips("taxi",198),
			new VehBlips("dune",147),
			new VehBlips("stockade",67)
		};

		public static void DropObjects(Vector3 vTarget)
		{
			LoggerLight.GetLogging("BuildObjects.DropObjects");

			string sObject;
			Entity Plop;
			if (RandomNum.FindRandom(22, 1, 10) < 5)
			{
				sObject = DropProplist[RandomNum.FindRandom(23, 0, DropProplist.Count - 1)];
				Plop = BuildProps(sObject, vTarget, Vector3.Zero, true, false);
			}
			else
			{
				Plop = VehicleSpawn(7, vTarget, RandomNum.RandInt(1, 360), false, new PlayerBrain(), true, false);
			}
			Script.Wait(2000);
			Plop.MarkAsNoLongerNeeded();
		}
		public static Prop BuildProps(string sObject, Vector3 vPos, Vector3 vRot, bool bPush, bool bAddtolist)
		{
			LoggerLight.GetLogging("BuildObjects.BuildProps,  sObject == " + sObject);
			Prop Plop = World.CreateProp(sObject, vPos, vRot, true, false);
			if (bPush)
				Plop.ApplyForce(new Vector3(0.00f, 0.00f, 1.00f), new Vector3(0.00f, 0.00f, 1.00f));

			if (bAddtolist)
				DataStore.Plops.Add(new Prop(Plop.Handle));

			ObjectLog.BackUpAss("Prop-" + Plop.Handle);

			return Plop;
		}
		public static int OhMyBlip(Vehicle MyVehic)
		{
			LoggerLight.GetLogging("BuildObjects.OhMyBlip");

			int iBeLip = 0;
			if (MyVehic != null)
			{
				int iVehHash = MyVehic.Model.GetHashCode();

				if (MyVehic.ClassType == VehicleClass.Boats)
					iBeLip = 427;
				else if (MyVehic.ClassType == VehicleClass.Helicopters)
					iBeLip = 64;
				else if (MyVehic.ClassType == VehicleClass.Motorcycles)
					iBeLip = 226;
				else if (MyVehic.ClassType == VehicleClass.Planes)
					iBeLip = 424;
				else if (MyVehic.ClassType == VehicleClass.Vans)
					iBeLip = 616;
				else if (MyVehic.ClassType == VehicleClass.Commercial)//mule
					iBeLip = 477;
				else if (MyVehic.ClassType == VehicleClass.Industrial)//trucks
					iBeLip = 477;
				else if (MyVehic.ClassType == VehicleClass.Service)//buss
					iBeLip = 513;
				else if (MyVehic.ClassType == VehicleClass.Super)
					iBeLip = 595;
				else if (MyVehic.ClassType == VehicleClass.Sports)
					iBeLip = 523;
				else if (MyVehic.ClassType == VehicleClass.Cycles)
					iBeLip = 376;
				else
					iBeLip = 225;

				for (int i = 0; i < vehBlips.Count; i++)
				{
					if (iVehHash == Function.Call<int>(Hash.GET_HASH_KEY, vehBlips[i].VehicleKey))
						iBeLip = vehBlips[i].BlipNo;
				}
			}

			return iBeLip;
		}
		public static Blip DirectionalBlimp(Ped pEdd)
		{
			LoggerLight.GetLogging("BuildObjects.DirectionalBlimp");

			Blip MyBlip = null;

			if (!DataStore.MySettings.NoRadar)
			{
				MyBlip = Function.Call<Blip>(Hash.ADD_BLIP_FOR_ENTITY, pEdd.Handle);
				Function.Call(Hash.SET_BLIP_SPRITE, MyBlip.Handle, 399);
				Function.Call(Hash.SET_BLIP_AS_SHORT_RANGE, MyBlip.Handle, true);
				Function.Call(Hash.SET_BLIP_PRIORITY, MyBlip.Handle, 1);
				Function.Call(Hash.SET_BLIP_COLOUR, MyBlip.Handle, 85);
				Function.Call(Hash.SET_BLIP_DISPLAY, MyBlip.Handle, 8);
			}

			ObjectLog.BackUpAss("Blip-" + MyBlip.Handle);

			return MyBlip;
		}
		public static Blip PedBlimp(Ped pEdd, int iBlippy, string sName, int iColour)
		{
			LoggerLight.GetLogging("BuildObjects.PedBlimp, iBlippy == " + iBlippy + ", sName == " + sName + ", iColour" + iColour);
			Blip MyBlip = null;

			if (!DataStore.MySettings.NoRadar)
			{
				MyBlip = Function.Call<Blip>(Hash.ADD_BLIP_FOR_ENTITY, pEdd.Handle);
				Function.Call(Hash.SET_BLIP_SPRITE, MyBlip.Handle, iBlippy);
				Function.Call(Hash.SET_BLIP_AS_SHORT_RANGE, MyBlip.Handle, true);

				Function.Call(Hash.SET_BLIP_COLOUR, MyBlip.Handle, iColour);

				Function.Call(Hash.BEGIN_TEXT_COMMAND_SET_BLIP_NAME, "STRING");
				Function.Call(Hash._ADD_TEXT_COMPONENT_STRING, " Player: " + sName);
				Function.Call(Hash.END_TEXT_COMMAND_SET_BLIP_NAME, MyBlip.Handle);
				Function.Call((Hash)0xF9113A30DE5C6670, "STRING");
				Function.Call(Hash._ADD_TEXT_COMPONENT_STRING, " Player: " + sName);
				Function.Call((Hash)0xBC38B49BCB83BC9B, MyBlip.Handle);
			}

			ObjectLog.BackUpAss("Blip-" + MyBlip.Handle);

			return MyBlip;
		}
		public static Blip LocalBlip(Vector3 Vlocal, int iBlippy, string sName)
		{
			LoggerLight.GetLogging("BuildObjects, iBlippy == " + iBlippy + ", sName == " + sName);

			Blip MyBlip = Function.Call<Blip>(Hash.ADD_BLIP_FOR_COORD, Vlocal.X, Vlocal.Y, Vlocal.Z);
			Function.Call(Hash.SET_BLIP_SPRITE, MyBlip.Handle, iBlippy);
			Function.Call(Hash.SET_BLIP_AS_SHORT_RANGE, MyBlip.Handle, true);

			Function.Call(Hash.BEGIN_TEXT_COMMAND_SET_BLIP_NAME, "STRING");
			Function.Call(Hash._ADD_TEXT_COMPONENT_STRING, " Player: " + sName);
			Function.Call(Hash.END_TEXT_COMMAND_SET_BLIP_NAME, MyBlip.Handle);
			Function.Call((Hash)0xF9113A30DE5C6670, "STRING");
			Function.Call(Hash._ADD_TEXT_COMPONENT_STRING, " Player: " + sName);
			Function.Call((Hash)0xBC38B49BCB83BC9B, MyBlip.Handle);

			ObjectLog.BackUpAss("Blip-" + MyBlip.Handle);

			return MyBlip;
		}
		public static void BlipDirect(Blip Blippy, float fDir)
		{
			int iHead = (int)fDir;
			if (Blippy.Exists())
				Function.Call(Hash.SET_BLIP_ROTATION, Blippy.Handle, iHead);
		}
		public static void NewPlayer()
		{
			LoggerLight.GetLogging("BuildObjects.NewPlayer");
			DataStore.iNextPlayer = Game.GameTime + RandomNum.RandInt(DataStore.MySettings.MinWait, DataStore.MySettings.MaxWait);
			IniSettings.LoadIniSetts();

			int iHeister = PedActions.NearHiest();

			if (ReturnValues.PlayerZinSesh() + 5 < DataStore.MySettings.MaxPlayers && iHeister != -1 && DataStore.bHeistPop)
				PedActions.HeistDrips(iHeister);
			else
			{
				if (RandomNum.FindRandom(10, 1, 10) < 8)
					CreatePlayer();
				else
					InABuilding();
			}
		}
		public static void CreatePlayer()
		{
			PlayerBrain newBrain = new PlayerBrain();

			newBrain.PFMySetting = FreemodePed.MakeFaces();
			newBrain.MyName = ReturnValues.SillyNameList();

			bool bCanFill = false;
			int iBrain = RandomNum.FindRandom(16, 0, 50);

			if (DataStore.MySettings.Aggression == 1)
			{
				if (iBrain < 5)
					newBrain.SessionJumper = true;
			}
			else if (DataStore.MySettings.Aggression == 2)
			{
				if (iBrain < 2)
					newBrain.Friendly = false;
				else if (iBrain < 5)
					newBrain.SessionJumper = true;
			}
			else if (DataStore.MySettings.Aggression == 3)
			{
				if (iBrain < 5)
					newBrain.SessionJumper = true;
				else if (iBrain < 10)
					newBrain.Friendly = false;
			}
			else if (DataStore.MySettings.Aggression == 4)
			{
				if (iBrain < 5)
					newBrain.SessionJumper = true;
				else if (iBrain < 15)
					newBrain.Friendly = false;
			}
			else if (DataStore.MySettings.Aggression == 5)
			{
				if (iBrain < 5)
					newBrain.SessionJumper = true;
				else if (iBrain < 20)
					newBrain.Friendly = false;
			}
			else if (DataStore.MySettings.Aggression == 6)
			{
				if (iBrain < 5)
					newBrain.SessionJumper = true;
				else if (iBrain < 30)
					newBrain.Friendly = false;
			}
			else if (DataStore.MySettings.Aggression == 7)
			{
				if (iBrain < 5)
					newBrain.SessionJumper = true;
				else if (iBrain < 35)
					newBrain.Friendly = false;
			}
			else if (DataStore.MySettings.Aggression == 8)
			{
				if (iBrain < 40)
					newBrain.Friendly = false;
			}
			else if (DataStore.MySettings.Aggression == 9)
			{
				if (iBrain < 45)
					newBrain.Friendly = false;
			}
			else if (DataStore.MySettings.Aggression == 10)
				newBrain.Friendly = false;
			else
			{
				if (!DataStore.bHackerIn)
					newBrain.Hacker = true;
				else
					newBrain.Friendly = false;
			}

			if (!newBrain.Friendly)
				newBrain.BlipColour = 1;

			if (DataStore.MySettings.NoRadar)
			{
				newBrain.OffRadarBool = true;
				newBrain.OffRadar = -1;
			}

			if (RandomNum.FindRandom(1, 1, 10) < 4 || DataStore.MySettings.Aggression == 1 || newBrain.SessionJumper)
			{
				if (!newBrain.SessionJumper && newBrain.Friendly)
					newBrain.ApprochPlayer = true;
				FindPed MyFinda = new FindPed(35.00f, 220.00f, newBrain);
				FindStuff.MakeFrenz.Add(MyFinda);
			}
			else
			{
				int iTypeO = RandomNum.FindRandom(2, 1, 60);
				if (DataStore.MySettings.Aggression < 4)
				{
					if (iTypeO < 5 && newBrain.Friendly)
					{
						newBrain.PrefredVehicle = 3;//Plane
						newBrain.IsPlane = true;
					}
					else if (iTypeO < 20 && newBrain.Friendly)
					{
						newBrain.PrefredVehicle = 2;//Heli 
						newBrain.IsHeli = true;
						bCanFill = true;
					}
					else
					{
						newBrain.PrefredVehicle = 1;//Veh				
						bCanFill = true;
					}
				}
				else
				{
					if (iTypeO < 10 && DataStore.MySettings.Aggression > 5)
						newBrain.PrefredVehicle = 8;//Oppress
					else if (iTypeO < 15)
						newBrain.PrefredVehicle = 6;//ArmoredVeh
					else if (iTypeO < 30)
						newBrain.PrefredVehicle = 5;//AttPlane
					else if (iTypeO < 40)
					{
						newBrain.PrefredVehicle = 4;
						bCanFill = true;
					}
					else if (iTypeO < 50 && newBrain.Friendly)
						newBrain.PrefredVehicle = 3;//Plane
					else if (iTypeO < 55 && newBrain.Friendly)
					{
						newBrain.PrefredVehicle = 2;//Heli 
						bCanFill = true;
					}
					else
						newBrain.PrefredVehicle = 1;//Veh
				}

				FindVeh myCar = new FindVeh(15.00f, 145.00f, true, bCanFill, newBrain);
				FindStuff.MakeCarz.Add(myCar);
			}
		}
		public static void InABuilding()
		{
			LoggerLight.GetLogging("BuildObjects.InABuilding");

			List<int> iKeepItReal = new List<int>();

			for (int i = 0; i < DataStore.AFKList.Count; i++)
				iKeepItReal.Add(DataStore.AFKList[i].App);

			int iMit = RandomNum.RandInt(0, DataStore.AFKPlayers.Count - 1);

			while (iKeepItReal.Contains(iMit))
				iMit = RandomNum.RandInt(0, DataStore.AFKPlayers.Count - 1);

			string sName = ReturnValues.SillyNameList();
			Blip FakeB = BuildObjects.LocalBlip(DataStore.AFKPlayers[iMit], 417, sName);

			AfkPlayer MyAfk = new AfkPlayer(FakeB, Game.GameTime + RandomNum.RandInt(DataStore.MySettings.MinSession, DataStore.MySettings.MaxSession), iMit, sName, ReturnValues.RandomId(), ReturnValues.UniqueLevels());
			PlayerAI.AddToAFKList(MyAfk);
		}
		public static Ped GenPlayerPed(Vector3 vLocal, float fAce, PlayerBrain thisBrain)
		{
			LoggerLight.GetLogging("BuildObjects.GenPlayerPed, thisBrain.Driver == " + thisBrain.Driver + ", thisBrain.Follower == " + thisBrain.Follower + ", thisBrain.Friendly == " + thisBrain.Friendly + ", thisBrain.Hacker == " + thisBrain.Hacker + ", thisBrain.MyIdentity == " + thisBrain.MyIdentity + ", thisBrain.MyName == " + thisBrain.MyName);

			Ped thisPed = null;
			string sPeddy = "mp_f_freemode_01";

			if (thisBrain.PFMySetting.PFMale)
				sPeddy = "mp_m_freemode_01";

			var model = new Model(sPeddy);
			model.Request();    // Check if the model is valid
			if (model.IsInCdImage && model.IsValid)
			{
				while (!model.IsLoaded)
					Script.Wait(1);

				thisPed = Function.Call<Ped>(Hash.CREATE_PED, 4, model, vLocal.X, vLocal.Y, vLocal.Z, fAce, true, false);
				Function.Call(Hash.SET_MODEL_AS_NO_LONGER_NEEDED, model.Hash);

				if (thisPed.Exists())
				{
					int iAccuracy = RandomNum.RandInt(DataStore.MySettings.AccMin, DataStore.MySettings.AccMax);
					Function.Call(Hash.SET_PED_ACCURACY, thisPed.Handle, iAccuracy);
					thisPed.MaxHealth = RandomNum.RandInt(200, 400);
					thisPed.Health = thisPed.MaxHealth;

					Function.Call(Hash.SET_PED_CAN_SWITCH_WEAPON, thisPed.Handle, true);
					Function.Call(Hash.SET_PED_COMBAT_MOVEMENT, thisPed.Handle, 2);
					Function.Call(Hash.SET_PED_PATH_CAN_USE_CLIMBOVERS, thisPed.Handle, true);
					Function.Call(Hash.SET_PED_PATH_CAN_USE_LADDERS, thisPed.Handle, true);
					Function.Call(Hash.SET_PED_PATH_CAN_DROP_FROM_HEIGHT, thisPed.Handle, true);
					Function.Call(Hash.SET_PED_PATH_PREFER_TO_AVOID_WATER, thisPed.Handle, false);
					Function.Call(Hash.SET_PED_COMBAT_ATTRIBUTES, thisPed.Handle, 0, true);
					Function.Call(Hash.SET_PED_COMBAT_ATTRIBUTES, thisPed.Handle, 1, true);
					if (DataStore.MySettings.Aggression > 3)
						Function.Call(Hash.SET_PED_COMBAT_ATTRIBUTES, thisPed.Handle, 2, true);
					Function.Call(Hash.SET_PED_COMBAT_ATTRIBUTES, thisPed.Handle, 3, true);



					thisPed.CanBeTargetted = true;
					thisBrain.ThisPed = thisPed;

					FreemodePed.OnlineFaces(thisPed, thisBrain.PFMySetting);

					PedActions.GunningIt(thisPed);

					if (thisBrain.MyIdentity == "")
					{
						thisBrain.TimeOn = Game.GameTime + RandomNum.RandInt(DataStore.MySettings.MinSession, DataStore.MySettings.MaxSession);
						thisBrain.Level = ReturnValues.UniqueLevels();
						thisBrain.MyIdentity = ReturnValues.RandomId();
					}

					if (thisBrain.Hacker)
					{
						DataStore.bHackerIn = true;
						DataStore.bHackEvent = false;
						thisPed.RelationshipGroup = DataStore.GP_Mental;
					}
					else if (thisBrain.Follower)
					{
						thisBrain.Friendly = true;
						thisPed.RelationshipGroup = DataStore.Gp_Follow;
						PedActions.FolllowTheLeader(thisPed);
						if (thisBrain.ThisVeh == null)
							PedActions.OhDoKeepUp(thisPed);
					}
					else if (thisBrain.Friendly)
					{
						thisPed.RelationshipGroup = DataStore.Gp_Friend;
						if (thisBrain.ThisVeh == null)
							thisPed.Task.WanderAround();
					}
					else
					{
						thisBrain.BlipColour = 1;
						PedActions.FightPlayer(thisPed, false);
						if (DataStore.MySettings.Aggression > 7)
							thisPed.RelationshipGroup = DataStore.GP_Mental;
						else
							thisPed.RelationshipGroup = DataStore.GP_Attack;
					}

					if (thisBrain.ThisOppress != null)
					{
						thisBrain.ThisBlip = BuildObjects.PedBlimp(thisPed, 639, thisBrain.MyName, thisBrain.BlipColour);
					}
					else if (thisBrain.Driver)
					{
						float fAggi = DataStore.MySettings.Aggression / 100;
						Function.Call(Hash.SET_DRIVER_ABILITY, thisPed.Handle, 1.00f);
						Function.Call(Hash.SET_DRIVER_AGGRESSIVENESS, thisPed.Handle, fAggi);
						Function.Call(Hash.SET_PED_STEERS_AROUND_VEHICLES, thisPed.Handle, true);
						thisBrain.ThisBlip = PedBlimp(thisBrain.ThisPed, OhMyBlip(thisBrain.ThisVeh), thisBrain.MyName, thisBrain.BlipColour);
					}

					if (thisBrain.IsPlane)
                    {
						if (thisBrain.ThisVeh != null)
							thisBrain.ThisVeh.FreezePosition = false;

					}

					int iFindBrain = PlayerAI.ReteaveBrain(thisBrain.MyIdentity);
					if (iFindBrain != -1)
					{
						DataStore.PedList[iFindBrain].ThisPed = thisPed;

						if (!thisBrain.Friendly && DataStore.PedList[iFindBrain].OffRadar == 0 && RandomNum.RandInt(0, 40) < 10)
							DataStore.PedList[iFindBrain].OffRadar = -1;

						LoggerLight.GetLogging("FreemodePed is it radar thing??");
					}
					else
						DataStore.AddtoPedList.Add(thisBrain);
				}
				else
					thisPed = null;
			}
			else
				thisPed = null;


			ObjectLog.BackUpAss("PedX-" + thisPed.Handle);

			return thisPed;
		}
		public static bool IsItThePlane(Vehicle vMe)
		{
			bool result = false;
			if (DataStore.iFollow < 7 && !DataStore.bPlanePort)
			{
				for (int i = 0; i < SeatTest.Count; i++)
				{
					if (Function.Call<int>(Hash.GET_HASH_KEY, SeatTest[i]) == vMe.Model.Hash)
					{
						result = true;
						break;
					}
				}
			}

			return result;
		}
		public static bool IsItARealVehicle(string sVehName)
		{
			LoggerLight.GetLogging("BuildObjects.IsItARealVehicle, sVehName == " + sVehName);

			bool bIsReal = false;

			int iVehHash = Function.Call<int>(Hash.GET_HASH_KEY, sVehName);
			if (Function.Call<bool>(Hash.IS_MODEL_A_VEHICLE, iVehHash))
				bIsReal = true;

			return bIsReal;
		}
		public static string RandVeh(int iVechList)
		{
			LoggerLight.GetLogging("BuildObjects.RandVeh, iVechList == " + iVechList);
			string sVeh = "ZENTORNO";

			if (iVechList == 1)
				sVeh = PreVeh_01[RandomNum.FindRandom(30, 0, PreVeh_01.Count - 1)];
			else if (iVechList == 2)
				sVeh = PreVeh_02[RandomNum.FindRandom(31, 0, PreVeh_02.Count - 1)];
			else if (iVechList == 3)
				sVeh = PreVeh_03[RandomNum.FindRandom(32, 0, PreVeh_03.Count - 1)];
			else if (iVechList == 4)
				sVeh = PreVeh_04[RandomNum.FindRandom(33, 0, PreVeh_04.Count - 1)];
			else if (iVechList == 5)
				sVeh = PreVeh_05[RandomNum.FindRandom(34, 0, PreVeh_05.Count - 1)];
			else if (iVechList == 6)
				sVeh = PreVeh_06[RandomNum.FindRandom(35, 0, PreVeh_06.Count - 1)];
			else if (iVechList == 7)
				sVeh = PreVeh_07[RandomNum.FindRandom(36, 0, PreVeh_07.Count - 1)];
			else if (iVechList == 8)
				sVeh = "hydra";
			else if (iVechList == 9)
				sVeh = "oppressor2";

			return sVeh;
		}
		public static Vehicle VehicleSpawn(int iVehList, Vector3 VecLocal, float VecHead, bool bAddPlayer, PlayerBrain myBrains, bool bAsProp, bool bCanFill)
		{
			LoggerLight.GetLogging("BuildObjects.VehicleSpawn, myBrains.PrefredVehicle == " + myBrains.PrefredVehicle + ", bAddPlayer == " + bAddPlayer + ", bAsProp == " + bAsProp);

			Vehicle BuildVehicle = null;
			string sVehModel;

			sVehModel = RandVeh(iVehList);

			if (!IsItARealVehicle(sVehModel))
				sVehModel = "MAMBA";

			int iVehHash = Function.Call<int>(Hash.GET_HASH_KEY, sVehModel);

			if (Function.Call<bool>(Hash.IS_MODEL_IN_CDIMAGE, iVehHash) && Function.Call<bool>(Hash.IS_MODEL_A_VEHICLE, iVehHash))
			{
				Function.Call(Hash.REQUEST_MODEL, iVehHash);
				while (!Function.Call<bool>(Hash.HAS_MODEL_LOADED, iVehHash))
					Script.Wait(1);

				BuildVehicle = Function.Call<Vehicle>(Hash.CREATE_VEHICLE, iVehHash, VecLocal.X, VecLocal.Y, VecLocal.Z, VecHead, true, true);

				if (BuildVehicle.ClassType == VehicleClass.Helicopters || BuildVehicle.ClassType == VehicleClass.Planes)
					MaxOutAllModsNoWheels(BuildVehicle);
				else
					MaxModsRandExtras(BuildVehicle);

				if (iVehList != 9)
				{
					if (!bAsProp)
					{
						myBrains.ThisVeh = BuildVehicle;
						myBrains.Driver = true;

						if (myBrains.PrefredVehicle == 5 || myBrains.PrefredVehicle == 3 || myBrains.PrefredVehicle == 8)
						{
							BuildVehicle.FreezePosition = true;
							myBrains.IsPlane = true;
						}
						else if (myBrains.PrefredVehicle == 4 || myBrains.PrefredVehicle == 2)
							myBrains.IsHeli = true;

						if (!ReturnValues.HasASeat(BuildVehicle))
							myBrains.ApprochPlayer = false;

						BuildVehicle.IsPersistent = true;

						if (iVehList == 8)
						{
							myBrains.ThisOppress = OppressorTime(BuildVehicle);
						}
						else if (myBrains.Friendly && IsItThePlane(BuildVehicle))
						{
							myBrains.PlaneLand = 10;
							BuildVehicle.FreezePosition = true;
							myBrains.ApprochPlayer = false;
							DataStore.bPlanePort = true;
							bCanFill = false;
						}

						if (bAddPlayer)
						{
							Ped Driver = BuildObjects.GenPlayerPed(BuildVehicle.Position, BuildVehicle.Heading, myBrains);
							PedActions.WarptoAnyVeh(BuildVehicle, Driver, -1);
							BuildVehicle.EngineRunning = true;

							int iSpare = -1;
							if (!myBrains.Friendly)
								iSpare = 0;

							if (bCanFill && ReturnValues.PlayerZinSesh() + BuildVehicle.PassengerSeats + iSpare < DataStore.MySettings.MaxPlayers)
							{
								for (int i = 0; i < BuildVehicle.PassengerSeats + iSpare; i++)
								{
									PlayerBrain newBrain = new PlayerBrain();
									newBrain.PFMySetting = FreemodePed.MakeFaces();
									newBrain.MyName = ReturnValues.SillyNameList();
									newBrain.Driver = false;
									newBrain.Passenger = true;
									newBrain.Friendly = myBrains.Friendly;
									Ped CarPed = BuildObjects.GenPlayerPed(BuildVehicle.Position, 0f, newBrain);
									PedActions.WarptoAnyVeh(BuildVehicle, CarPed, i);
								}
							}
						}
						else
						{
							int ThisBrain = PlayerAI.ReteaveBrain(myBrains.MyIdentity);

							if (ThisBrain != -1)
							{
								DataStore.PedList[ThisBrain] = myBrains;
								PedActions.WarptoAnyVeh(BuildVehicle, myBrains.ThisPed, -1);
							}
						}
					}
					else
						BuildVehicle.ApplyForce(new Vector3(0.00f, 0.00f, 1.00f), new Vector3(0.00f, 0.00f, 1.00f));
				}

				Function.Call(Hash.SET_MODEL_AS_NO_LONGER_NEEDED, iVehHash);
			}
			else
				BuildVehicle = null;

			ObjectLog.BackUpAss("Vehs-" + BuildVehicle.Handle);

			return BuildVehicle;
		}
		private static Vehicle OppressorTime(Vehicle VStickTo)
		{
			LoggerLight.GetLogging("BuildObjects.OppressorTime");
			Vehicle Bike = VehicleSpawn(9, VStickTo.Position, 0.00f, false, new PlayerBrain(), false, false);
			Bike.IsPersistent = true;
			Function.Call(Hash.ATTACH_ENTITY_TO_ENTITY, Bike.Handle, VStickTo.Handle, Function.Call<int>(Hash.GET_PED_BONE_INDEX, VStickTo.Handle, 0), 0.00f, 3.32999945f, -0.10f, 0.00f, 0.00f, 0.00f, false, false, false, false, 2, true);

			VStickTo.IsVisible = false;
			Bike.IsVisible = true;

			return Bike;
		}
		private static void MaxModsRandExtras(Vehicle Vehic)
		{
			LoggerLight.GetLogging("BuildObjects.MaxModsRandExtras");

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
						iSpoilher = RandomNum.RandInt(0, iSpoilher - 1);

					Function.Call(Hash.SET_VEHICLE_MOD, Vehic.Handle, i, RandomNum.RandInt(0, iSpoilher - 1), true);
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
			LoggerLight.GetLogging("BuildObjects.MaxOutAllModsNoWheels");

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
		public static void StayOnGround(Vehicle Vhick)
		{
			LoggerLight.GetLogging("BuildObjects.StayOnGround");
			while (!Vhick.IsOnAllWheels)
			{
				Function.Call<bool>(Hash.SET_VEHICLE_ON_GROUND_PROPERLY, Vhick.Handle);
				Script.Wait(10);
			}
		}
	} 
}
