using GTA;
using GTA.Math;
using GTA.Native;
using PlayerZero.Classes;
using System.Collections.Generic;
using System.IO;
using System;

namespace PlayerZero
{
    public class DataStore
    {
        public static bool bTrainM { get; set; }
        public static bool bLoadUp { get; set; }
        public static bool bHeistPop { get; set; }
        public static bool bHackerIn { get; set; }
        public static bool bPiggyBack { get; set; }
        public static bool bPlayerPiggyBack { get; set; }
        public static bool bHackEvent { get; set; }
        public static bool bDisabled { get; set; }
        public static bool bMenuOpen { get; set; }
        public static bool bShowPlayerz { get; set; }
        public static bool bScan { get; set; }
        public static bool bClearingUp { get; set; }
        public static bool bPlanePort { get; set; }

        public static readonly string sVersion = "1.8";
        public static readonly string sSaveCont = "" + Directory.GetCurrentDirectory() + "/Scripts/PlayerZero/SavedContacts.xml";
        public static readonly string sPzFolder = "" + Directory.GetCurrentDirectory() + "/Scripts/PlayerZero/";
        public static readonly string sBeeLogs = "" + Directory.GetCurrentDirectory() + "/Scripts/PlayerZero/PlayerZLog.txt";
        public static readonly string sOutfits = "" + Directory.GetCurrentDirectory() + "/Scripts/PlayerZero/Outfits.xml";
        public static readonly string sHasNSPM = "" + Directory.GetCurrentDirectory() + "/Scripts/NSPM";
        public static readonly string sHasWard = "" + Directory.GetCurrentDirectory() + "/Scripts/NSPM/Wardrobe";
        public static readonly string sPlayerM = "" + Directory.GetCurrentDirectory() + "/Scripts/NSPM/Wardrobe/FreemodeMale.Xml";
        public static readonly string sPlayerF = "" + Directory.GetCurrentDirectory() + "/Scripts/NSPM/Wardrobe/FreemodeFemale.Xml";
        public static readonly string sXmasTree = "prop_xmas_ext";
        public static string sMoneyPicker { get; set; }

        public static int iFollow { get; set; }
        public static int iFolPos { get; set; }
        public static int iOrbBurnOut { get; set; }
        public static int iFindingTime { get; set; }
        public static int iScale { get; set; }
        public static int iMoneyDrops { get; set; }
        public static int iMoneyDropRate { get; set; }
        public static int iPlaneLand { get; set; }
        public static int iNextPlayer { get; set; }
        public static int iEventFind { get; set; }


        public static int GP_Player { get; set; }
        public static readonly int iFollowMe = Function.Call<int>(Hash.CREATE_GROUP);
        public static readonly int Gp_Friend = World.AddRelationshipGroup("FrendlyNPCs");
        public static readonly int GP_Attack = World.AddRelationshipGroup("AttackNPCs");
        public static readonly int Gp_Follow = World.AddRelationshipGroup("FollowerNPCs");
        public static readonly int GP_Mental = World.AddRelationshipGroup("MentalNPCs");

        public static Prop WindMill { get; set; }
        public static Vector3 LetsGoHere { get; set; }

        public static Vector3 FlyMeToo { get; set; }
        public static List<Vector3> AFKPlayers { get; set; }
        public static List<string> ControlSym { get; set; }
        public static List<string> PlayerNames { get; set; }

        public static List<bool> BeOnOff { get; set; }
        public static List<int> iTimers { get; set; }
        public static ContactList PlayContList { get; set; }

        public static List<Prop> Plops { get; set; }
        public static List<Vehicle> Vicks { get; set; }

        public static Vehicle ThatPlane { get; set; }

        public static List<PlayerBrain> PedList { get; set; }
        public static List<AfkPlayer> AFKList { get; set; }

        public static List<ClothBank> maleCloth = new List<ClothBank>();
        public static List<ClothBank> femaleCloth = new List<ClothBank>();

        public static List<PlayerBrain> AddtoPedList { get; set; }

        public static JoinMe WalkFriend { get; set; }
        public static JoinMe DriveFriend { get; set; }
        public static JoinMe FlyFriend { get; set; }

        public static PZSettings MySettings { get; set; }

        public static void LoadSettings()
        {
            if (File.Exists(sBeeLogs))
                File.Delete(sBeeLogs);

            ObjectLog.ClearThis();

            MySettings = IniSettings.LoadIniSetts();

            bTrainM = true;//set the test then set to false
            bLoadUp = false;
            bHeistPop = true;
            bHackerIn = false;
            bPiggyBack = false;
            bPlayerPiggyBack = false;
            bHackEvent = false;
            bDisabled = false;
            bMenuOpen = false;
            bShowPlayerz = false;
            bScan = false;
            bClearingUp = false;
            bPlanePort = false;

            iFollow = 0;
            iFolPos = 0;
            iOrbBurnOut = 0;
            iFindingTime = 0;
            iScale = 0;
            iMoneyDrops = -1;
            iMoneyDropRate = 0;
            iPlaneLand = 0;
            iNextPlayer = Game.GameTime + RandomNum.RandInt(DataStore.MySettings.MinWait, DataStore.MySettings.MaxWait);
            iEventFind = CrimboHalo();

            GP_Player = Game.Player.Character.RelationshipGroup;

            ThatPlane = null;
            WindMill = null;
            WalkFriend = null;
            DriveFriend = null;
            FlyFriend = null;
            LetsGoHere = Vector3.Zero;
            FlyMeToo = Vector3.Zero;

            AFKPlayers = AppLocalList();
            ControlSym = ControlSymLoad();

            BeOnOff = new List<bool>();
            iTimers = new List<int>();
            PlayerNames = new List<string>();
            PlayContList = LoadContsXml();

            maleCloth = FindOutfits(true);
            femaleCloth = FindOutfits(false);

            Plops = new List<Prop>();
            Vicks = new List<Vehicle>();

            PedList = new List<PlayerBrain>();
            AFKList = new List<AfkPlayer>();

            AddtoPedList = new List<PlayerBrain>();
        }
        public static int CrimboHalo()
        {
            int iBe = -1;

            if (DateTime.Today.Month == 9 && DateTime.Today.Day == 31)
                iBe = 1;
            else if (DateTime.Today.Month == 12 && DateTime.Today.Day == 25)
                iBe = 2;

            return iBe;
        }
        public static List<ClothBank> FindOutfits(bool bMale)
        {
            LoggerLight.GetLogging("FreemodePed.FindOutfits");
            List<ClothBank> CB = new List<ClothBank>();

            if (Directory.Exists(sHasNSPM))
            {
                if (Directory.Exists(sHasWard))
                {
                    if (bMale)
                    {
                        if (File.Exists(sPlayerM))
                        {
                            ClothBankXML TheOutXML = XmlReadWrite.LoadOutfitXML(sPlayerM);
                            for (int i = 0; i < TheOutXML.Outfits.Count; i++)
                                CB.Add(TheOutXML.Outfits[i]);
                        }
                    }
                    else
                    {
                        if (File.Exists(sPlayerF))
                        {
                            ClothBankXML TheOutXML = XmlReadWrite.LoadOutfitXML(sPlayerF);
                            for (int i = 0; i < TheOutXML.Outfits.Count; i++)
                                CB.Add(TheOutXML.Outfits[i]);
                        }
                    }
                }
            }
            return CB;
        }
        private static ContactList LoadContsXml()
        {
            LoggerLight.GetLogging("DataStore.LoadContsXml");

            ContactList brainList;

            if (File.Exists(sSaveCont))
                brainList = XmlReadWrite.LoadContact(sSaveCont);
            else
                brainList = new ContactList();

            return brainList;
        }
        private static List<string> ControlSymLoad()
        {
            LoggerLight.GetLogging("DataStore.ControlSymLoad");
            List<string> ControlList = new List<string>
            {
                " ~INPUT_NEXT_CAMERA~ ",//V~ ");//BACK
                " ~INPUT_LOOK_LR~ ",//MOUSE RIGHT~ ");//RIGHT STICK
                " ~INPUT_LOOK_UD~ ",//MOUSE DOWN~ ");//RIGHT STICK
                " ~INPUT_LOOK_UP_ONLY~ ",//(NONE);~ ");//RIGHT STICK
                " ~INPUT_LOOK_DOWN_ONLY~ ",//MOUSE DOWN~ ");//RIGHT STICK
                " ~INPUT_LOOK_LEFT_ONLY~ ",//(NONE);~ ");//RIGHT STICK
                " ~INPUT_LOOK_RIGHT_ONLY~ ",//MOUSE RIGHT~ ");//RIGHT STICK
                " ~INPUT_CINEMATIC_SLOWMO~ ",//(NONE);~ ");//R3
                " ~INPUT_SCRIPTED_FLY_UD~ ",//S~ ");//LEFT STICK
                " ~INPUT_SCRIPTED_FLY_LR~ ",//D~ ");//LEFT STICK
                " ~INPUT_SCRIPTED_FLY_ZUP~ ",//PAGEUP~ ");//LT
                " ~INPUT_SCRIPTED_FLY_ZDOWN~ ",//PAGEDOWN~ ");//RT
                " ~INPUT_WEAPON_WHEEL_UD~ ",//MOUSE DOWN~ ");//RIGHT STICK
                " ~INPUT_WEAPON_WHEEL_LR~ ",//MOUSE RIGHT~ ");//RIGHT STICK
                " ~INPUT_WEAPON_WHEEL_NEXT~ ",//SCROLLWHEEL DOWN~ ");//DPAD RIGHT
                " ~INPUT_WEAPON_WHEEL_PREV~ ",//SCROLLWHEEL UP~ ");//DPAD LEFT
                " ~INPUT_SELECT_NEXT_WEAPON~ ",//SCROLLWHEEL DOWN~ ");//(NONE);
                " ~INPUT_SELECT_PREV_WEAPON~ ",//SCROLLWHEEL UP~ ");//(NONE);
                " ~INPUT_SKIP_CUTSCENE~ ",//ENTER / LEFT MOUSE BUTTON / SPACEBAR~ ");//A
                " ~INPUT_CHARACTER_WHEEL~ ",//LEFT ALT~ ");//DPAD DOWN
                " ~INPUT_MULTIPLAYER_INFO~ ",//Z~ ");//DPAD DOWN
                " ~INPUT_SPRINT~ ",//LEFT SHIFT~ ");//A
                " ~INPUT_JUMP~ ",//SPACEBAR~ ");//X
                " ~INPUT_ENTER~ ",//F~ ");//Y
                " ~INPUT_ATTACK~ ",//LEFT MOUSE BUTTON~ ");//RT
                " ~INPUT_AIM~ ",//RIGHT MOUSE BUTTON~ ");//LT
                " ~INPUT_LOOK_BEHIND~ ",//C~ ");//R3
                " ~INPUT_PHONE~ ",//ARROW UP / SCROLLWHEEL BUTTON (PRESS);~ ");//DPAD UP
                " ~INPUT_SPECIAL_ABILITY~ ",//(NONE);~ ");//L3
                " ~INPUT_SPECIAL_ABILITY_SECONDARY~ ",//B~ ");//R3
                " ~INPUT_MOVE_LR~ ",//D~ ");//LEFT STICK
                " ~INPUT_MOVE_UD~ ",//S~ ");//LEFT STICK
                " ~INPUT_MOVE_UP_ONLY~ ",//W~ ");//LEFT STICK
                " ~INPUT_MOVE_DOWN_ONLY~ ",//S~ ");//LEFT STICK
                " ~INPUT_MOVE_LEFT_ONLY~ ",//A~ ");//LEFT STICK
                " ~INPUT_MOVE_RIGHT_ONLY~ ",//D~ ");//LEFT STICK
                " ~INPUT_DUCK~ ",//LEFT CTRL~ ");//L3
                " ~INPUT_SELECT_WEAPON~ ",//TAB~ ");//LB
                " ~INPUT_PICKUP~ ",//E~ ");//LB
                " ~INPUT_SNIPER_ZOOM~ ",//[~ ");//LEFT STICK
                " ~INPUT_SNIPER_ZOOM_IN_ONLY~ ",//]~ ");//LEFT STICK
                " ~INPUT_SNIPER_ZOOM_OUT_ONLY~ ",//[~ ");//LEFT STICK
                " ~INPUT_SNIPER_ZOOM_IN_SECONDARY~ ",//]~ ");//DPAD UP
                " ~INPUT_SNIPER_ZOOM_OUT_SECONDARY~ ",//[~ ");//DPAD DOWN
                " ~INPUT_COVER~ ",//Q~ ");//RB
                " ~INPUT_RELOAD~ ",//R~ ");//B
                " ~INPUT_TALK~ ",//E~ ");//DPAD RIGHT
                " ~INPUT_DETONATE~ ",//G~ ");//DPAD LEFT
                " ~INPUT_HUD_SPECIAL~ ",//Z~ ");//DPAD DOWN
                " ~INPUT_ARREST~ ",//F~ ");//Y
                " ~INPUT_ACCURATE_AIM~ ",//SCROLLWHEEL DOWN~ ");//R3
                " ~INPUT_CONTEXT~ ",//E~ ");//DPAD RIGHT
                " ~INPUT_CONTEXT_SECONDARY~ ",//Q~ ");//DPAD LEFT
                " ~INPUT_WEAPON_SPECIAL~ ",//(NONE);~ ");//Y
                " ~INPUT_WEAPON_SPECIAL_TWO~ ",//E~ ");//DPAD RIGHT
                " ~INPUT_DIVE~ ",//SPACEBAR~ ");//RB
                " ~INPUT_DROP_WEAPON~ ",//F9~ ");//Y
                " ~INPUT_DROP_AMMO~ ",//F10~ ");//B
                " ~INPUT_THROW_GRENADE~ ",//G~ ");//DPAD LEFT
                " ~INPUT_VEH_MOVE_LR~ ",//D~ ");//LEFT STICK
                " ~INPUT_VEH_MOVE_UD~ ",//LEFT CTRL~ ");//LEFT STICK
                " ~INPUT_VEH_MOVE_UP_ONLY~ ",//LEFT SHIFT~ ");//LEFT STICK
                " ~INPUT_VEH_MOVE_DOWN_ONLY~ ",//LEFT CTRL~ ");//LEFT STICK
                " ~INPUT_VEH_MOVE_LEFT_ONLY~ ",//A~ ");//LEFT STICK
                " ~INPUT_VEH_MOVE_RIGHT_ONLY~ ",//D~ ");//LEFT STICK
                " ~INPUT_VEH_SPECIAL~ ",//(NONE);~ ");//(NONE);
                " ~INPUT_VEH_GUN_LR~ ",//MOUSE RIGHT~ ");//RIGHT STICK
                " ~INPUT_VEH_GUN_UD~ ",//MOUSE DOWN~ ");//RIGHT STICK
                " ~INPUT_VEH_AIM~ ",//RIGHT MOUSE BUTTON~ ");//LB
                " ~INPUT_VEH_ATTACK~ ",//LEFT MOUSE BUTTON~ ");//RB
                " ~INPUT_VEH_ATTACK2~ ",//RIGHT MOUSE BUTTON~ ");//A
                " ~INPUT_VEH_ACCELERATE~ ",//W~ ");//RT
                " ~INPUT_VEH_BRAKE~ ",//S~ ");//LT
                " ~INPUT_VEH_DUCK~ ",//X~ ");//A
                " ~INPUT_VEH_HEADLIGHT~ ",//H~ ");//DPAD RIGHT
                " ~INPUT_VEH_EXIT~ ",//F~ ");//Y
                " ~INPUT_VEH_HANDBRAKE~ ",//SPACEBAR~ ");//RB
                " ~INPUT_VEH_HOTWIRE_LEFT~ ",//W~ ");//LT
                " ~INPUT_VEH_HOTWIRE_RIGHT~ ",//S~ ");//RT
                " ~INPUT_VEH_LOOK_BEHIND~ ",//C~ ");//R3
                " ~INPUT_VEH_CIN_CAM~ ",//R~ ");//B
                " ~INPUT_VEH_NEXT_RADIO~ ",//.~ ");//(NONE);
                " ~INPUT_VEH_PREV_RADIO~ ",//,~ ");//(NONE);
                " ~INPUT_VEH_NEXT_RADIO_TRACK~ ",//=~ ");//(NONE);
                " ~INPUT_VEH_PREV_RADIO_TRACK~ ",//-~ ");//(NONE);
                " ~INPUT_VEH_RADIO_WHEEL~ ",//Q~ ");//DPAD LEFT
                " ~INPUT_VEH_HORN~ ",//E~ ");//L3
                " ~INPUT_VEH_FLY_THROTTLE_UP~ ",//W~ ");//RT
                " ~INPUT_VEH_FLY_THROTTLE_DOWN~ ",//S~ ");//LT
                " ~INPUT_VEH_FLY_YAW_LEFT~ ",//A~ ");//LB
                " ~INPUT_VEH_FLY_YAW_RIGHT~ ",//D~ ");//RB
                " ~INPUT_VEH_PASSENGER_AIM~ ",//RIGHT MOUSE BUTTON~ ");//LT
                " ~INPUT_VEH_PASSENGER_ATTACK~ ",//LEFT MOUSE BUTTON~ ");//RT
                " ~INPUT_VEH_SPECIAL_ABILITY_FRANKLIN~ ",//(NONE);~ ");//R3
                " ~INPUT_VEH_STUNT_UD~ ",//(NONE);~ ");//(NONE);
                " ~INPUT_VEH_CINEMATIC_UD~ ",//MOUSE DOWN~ ");//RIGHT STICK
                " ~INPUT_VEH_CINEMATIC_UP_ONLY~ ",//NUMPAD- / SCROLLWHEEL UP~ ");//(NONE);
                " ~INPUT_VEH_CINEMATIC_DOWN_ONLY~ ",//NUMPAD+ / SCROLLWHEEL DOWN~ ");//(NONE);
                " ~INPUT_VEH_CINEMATIC_LR~ ",//MOUSE RIGHT~ ");//RIGHT STICK
                " ~INPUT_VEH_SELECT_NEXT_WEAPON~ ",//SCROLLWHEEL UP~ ");//X
                " ~INPUT_VEH_SELECT_PREV_WEAPON~ ",//[~ ");//(NONE);
                " ~INPUT_VEH_ROOF~ ",//H~ ");//DPAD RIGHT
                " ~INPUT_VEH_JUMP~ ",//SPACEBAR~ ");//RB
                " ~INPUT_VEH_GRAPPLING_HOOK~ ",//E~ ");//DPAD RIGHT
                " ~INPUT_VEH_SHUFFLE~ ",//H~ ");//DPAD RIGHT
                " ~INPUT_VEH_DROP_PROJECTILE~ ",//X~ ");//A
                " ~INPUT_VEH_MOUSE_CONTROL_OVERRIDE~ ",//LEFT MOUSE BUTTON~ ");//(NONE);
                " ~INPUT_VEH_FLY_ROLL_LR~ ",//NUMPAD 6~ ");//LEFT STICK
                " ~INPUT_VEH_FLY_ROLL_LEFT_ONLY~ ",//NUMPAD 4~ ");//LEFT STICK
                " ~INPUT_VEH_FLY_ROLL_RIGHT_ONLY~ ",//NUMPAD 6~ ");//LEFT STICK
                " ~INPUT_VEH_FLY_PITCH_UD~ ",//NUMPAD 5~ ");//LEFT STICK
                " ~INPUT_VEH_FLY_PITCH_UP_ONLY~ ",//NUMPAD 8~ ");//LEFT STICK
                " ~INPUT_VEH_FLY_PITCH_DOWN_ONLY~ ",//NUMPAD 5~ ");//LEFT STICK
                " ~INPUT_VEH_FLY_UNDERCARRIAGE~ ",//G~ ");//L3
                " ~INPUT_VEH_FLY_ATTACK~ ",//RIGHT MOUSE BUTTON~ ");//A
                " ~INPUT_VEH_FLY_SELECT_NEXT_WEAPON~ ",//SCROLLWHEEL UP~ ");//DPAD LEFT
                " ~INPUT_VEH_FLY_SELECT_PREV_WEAPON~ ",//[~ ");//(NONE);
                " ~INPUT_VEH_FLY_SELECT_TARGET_LEFT~ ",//NUMPAD 7~ ");//LB
                " ~INPUT_VEH_FLY_SELECT_TARGET_RIGHT~ ",//NUMPAD 9~ ");//RB
                " ~INPUT_VEH_FLY_VERTICAL_FLIGHT_MODE~ ",//E~ ");//DPAD RIGHT
                " ~INPUT_VEH_FLY_DUCK~ ",//X~ ");//A
                " ~INPUT_VEH_FLY_ATTACK_CAMERA~ ",//INSERT~ ");//R3
                " ~INPUT_VEH_FLY_MOUSE_CONTROL_OVERRIDE~ ",//LEFT MOUSE BUTTON~ ");//(NONE);
                " ~INPUT_VEH_SUB_TURN_LR~ ",//NUMPAD 6~ ");//LEFT STICK
                " ~INPUT_VEH_SUB_TURN_LEFT_ONLY~ ",//NUMPAD 4~ ");//LEFT STICK
                " ~INPUT_VEH_SUB_TURN_RIGHT_ONLY~ ",//NUMPAD 6~ ");//LEFT STICK
                " ~INPUT_VEH_SUB_PITCH_UD~ ",//NUMPAD 5~ ");//LEFT STICK
                " ~INPUT_VEH_SUB_PITCH_UP_ONLY~ ",//NUMPAD 8~ ");//LEFT STICK
                " ~INPUT_VEH_SUB_PITCH_DOWN_ONLY~ ",//NUMPAD 5~ ");//LEFT STICK
                " ~INPUT_VEH_SUB_THROTTLE_UP~ ",//W~ ");//RT
                " ~INPUT_VEH_SUB_THROTTLE_DOWN~ ",//S~ ");//LT
                " ~INPUT_VEH_SUB_ASCEND~ ",//LEFT SHIFT~ ");//X
                " ~INPUT_VEH_SUB_DESCEND~ ",//LEFT CTRL~ ");//A
                " ~INPUT_VEH_SUB_TURN_HARD_LEFT~ ",//A~ ");//LB
                " ~INPUT_VEH_SUB_TURN_HARD_RIGHT~ ",//D~ ");//RB
                " ~INPUT_VEH_SUB_MOUSE_CONTROL_OVERRIDE~ ",//LEFT MOUSE BUTTON~ ");//(NONE);
                " ~INPUT_VEH_PUSHBIKE_PEDAL~ ",//W~ ");//A
                " ~INPUT_VEH_PUSHBIKE_SPRINT~ ",//CAPSLOCK~ ");//A
                " ~INPUT_VEH_PUSHBIKE_FRONT_BRAKE~ ",//Q~ ");//LT
                " ~INPUT_VEH_PUSHBIKE_REAR_BRAKE~ ",//S~ ");//RT
                " ~INPUT_MELEE_ATTACK_LIGHT~ ",//R~ ");//B
                " ~INPUT_MELEE_ATTACK_HEAVY~ ",//Q~ ");//A
                " ~INPUT_MELEE_ATTACK_ALTERNATE~ ",//LEFT MOUSE BUTTON~ ");//RT
                " ~INPUT_MELEE_BLOCK~ ",//SPACEBAR~ ");//X
                " ~INPUT_PARACHUTE_DEPLOY~ ",//F / LEFT MOUSE BUTTON~ ");//Y
                " ~INPUT_PARACHUTE_DETACH~ ",//F~ ");//Y
                " ~INPUT_PARACHUTE_TURN_LR~ ",//D~ ");//LEFT STICK
                " ~INPUT_PARACHUTE_TURN_LEFT_ONLY~ ",//A~ ");//LEFT STICK
                " ~INPUT_PARACHUTE_TURN_RIGHT_ONLY~ ",//D~ ");//LEFT STICK
                " ~INPUT_PARACHUTE_PITCH_UD~ ",//S~ ");//LEFT STICK
                " ~INPUT_PARACHUTE_PITCH_UP_ONLY~ ",//W~ ");//LEFT STICK
                " ~INPUT_PARACHUTE_PITCH_DOWN_ONLY~ ",//S~ ");//LEFT STICK
                " ~INPUT_PARACHUTE_BRAKE_LEFT~ ",//Q~ ");//LB
                " ~INPUT_PARACHUTE_BRAKE_RIGHT~ ",//E~ ");//RB
                " ~INPUT_PARACHUTE_SMOKE~ ",//X~ ");//A
                " ~INPUT_PARACHUTE_PRECISION_LANDING~ ",//LEFT SHIFT~ ");//(NONE);
                " ~INPUT_MAP~ ",//(NONE);~ ");//(NONE);
                " ~INPUT_SELECT_WEAPON_UNARMED~ ",//1~ ");//(NONE);
                " ~INPUT_SELECT_WEAPON_MELEE~ ",//2~ ");//(NONE);
                " ~INPUT_SELECT_WEAPON_HANDGUN~ ",//6~ ");//(NONE);
                " ~INPUT_SELECT_WEAPON_SHOTGUN~ ",//3~ ");//(NONE);
                " ~INPUT_SELECT_WEAPON_SMG~ ",//7~ ");//(NONE);
                " ~INPUT_SELECT_WEAPON_AUTO_RIFLE~ ",//8~ ");//(NONE);
                " ~INPUT_SELECT_WEAPON_SNIPER~ ",//9~ ");//(NONE);
                " ~INPUT_SELECT_WEAPON_HEAVY~ ",//4~ ");//(NONE);
                " ~INPUT_SELECT_WEAPON_SPECIAL~ ",//5~ ");//(NONE);
                " ~INPUT_SELECT_CHARACTER_MICHAEL~ ",//F5~ ");//(NONE);
                " ~INPUT_SELECT_CHARACTER_FRANKLIN~ ",//F6~ ");//(NONE);
                " ~INPUT_SELECT_CHARACTER_TREVOR~ ",//F7~ ");//(NONE);
                " ~INPUT_SELECT_CHARACTER_MULTIPLAYER~ ",//F8 (CONSOLE);~ ");//(NONE);
                " ~INPUT_SAVE_REPLAY_CLIP~ ",//F3~ ");//B
                " ~INPUT_SPECIAL_ABILITY_PC~ ",//CAPSLOCK~ ");//(NONE);
                " ~INPUT_CELLPHONE_UP~ ",//ARROW UP~ ");//DPAD UP
                " ~INPUT_CELLPHONE_DOWN~ ",//ARROW DOWN~ ");//DPAD DOWN
                " ~INPUT_CELLPHONE_LEFT~ ",//ARROW LEFT~ ");//DPAD LEFT
                " ~INPUT_CELLPHONE_RIGHT~ ",//ARROW RIGHT~ ");//DPAD RIGHT
                " ~INPUT_CELLPHONE_SELECT~ ",//ENTER / LEFT MOUSE BUTTON~ ");//A
                " ~INPUT_CELLPHONE_CANCEL~ ",//BACKSPACE / ESC / RIGHT MOUSE BUTTON~ ");//B
                " ~INPUT_CELLPHONE_OPTION~ ",//DELETE~ ");//Y
                " ~INPUT_CELLPHONE_EXTRA_OPTION~ ",//SPACEBAR~ ");//X
                " ~INPUT_CELLPHONE_SCROLL_FORWARD~ ",//SCROLLWHEEL DOWN~ ");//(NONE);
                " ~INPUT_CELLPHONE_SCROLL_BACKWARD~ ",//SCROLLWHEEL UP~ ");//(NONE);
                " ~INPUT_CELLPHONE_CAMERA_FOCUS_LOCK~ ",//L~ ");//RT
                " ~INPUT_CELLPHONE_CAMERA_GRID~ ",//G~ ");//RB
                " ~INPUT_CELLPHONE_CAMERA_SELFIE~ ",//E~ ");//R3
                " ~INPUT_CELLPHONE_CAMERA_DOF~ ",//F~ ");//LB
                " ~INPUT_CELLPHONE_CAMERA_EXPRESSION~ ",//X~ ");//L3
                " ~INPUT_FRONTEND_DOWN~ ",//ARROW DOWN~ ");//DPAD DOWN
                " ~INPUT_FRONTEND_UP~ ",//ARROW UP~ ");//DPAD UP
                " ~INPUT_FRONTEND_LEFT~ ",//ARROW LEFT~ ");//DPAD LEFT
                " ~INPUT_FRONTEND_RIGHT~ ",//ARROW RIGHT~ ");//DPAD RIGHT
                " ~INPUT_FRONTEND_RDOWN~ ",//ENTER~ ");//A
                " ~INPUT_FRONTEND_RUP~ ",//TAB~ ");//Y
                " ~INPUT_FRONTEND_RLEFT~ ",//(NONE);~ ");//X
                " ~INPUT_FRONTEND_RRIGHT~ ",//BACKSPACE~ ");//B
                " ~INPUT_FRONTEND_AXIS_X~ ",//D~ ");//LEFT STICK
                " ~INPUT_FRONTEND_AXIS_Y~ ",//S~ ");//LEFT STICK
                " ~INPUT_FRONTEND_RIGHT_AXIS_X~ ",//]~ ");//RIGHT STICK
                " ~INPUT_FRONTEND_RIGHT_AXIS_Y~ ",//SCROLLWHEEL DOWN~ ");//RIGHT STICK
                " ~INPUT_FRONTEND_PAUSE~ ",//P~ ");//START
                " ~INPUT_FRONTEND_PAUSE_ALTERNATE~ ",//ESC~ ");//(NONE);
                " ~INPUT_FRONTEND_ACCEPT~ ",//ENTER / NUMPAD ENTER~ ");//A
                " ~INPUT_FRONTEND_CANCEL~ ",//BACKSPACE / ESC~ ");//B
                " ~INPUT_FRONTEND_X~ ",//SPACEBAR~ ");//X
                " ~INPUT_FRONTEND_Y~ ",//TAB~ ");//Y
                " ~INPUT_FRONTEND_LB~ ",//Q~ ");//LB
                " ~INPUT_FRONTEND_RB~ ",//E~ ");//RB
                " ~INPUT_FRONTEND_LT~ ",//PAGE DOWN~ ");//LT
                " ~INPUT_FRONTEND_RT~ ",//PAGE UP~ ");//RT
                " ~INPUT_FRONTEND_LS~ ",//LEFT SHIFT~ ");//L3
                " ~INPUT_FRONTEND_RS~ ",//LEFT CONTROL~ ");//R3
                " ~INPUT_FRONTEND_LEADERBOARD~ ",//TAB~ ");//RB
                " ~INPUT_FRONTEND_SOCIAL_CLUB~ ",//HOME~ ");//BACK
                " ~INPUT_FRONTEND_SOCIAL_CLUB_SECONDARY~ ",//HOME~ ");//RB
                " ~INPUT_FRONTEND_DELETE~ ",//DELETE~ ");//X
                " ~INPUT_FRONTEND_ENDSCREEN_ACCEPT~ ",//ENTER~ ");//A
                " ~INPUT_FRONTEND_ENDSCREEN_EXPAND~ ",//SPACEBAR~ ");//X
                " ~INPUT_FRONTEND_SELECT~ ",//CAPSLOCK~ ");//BACK
                " ~INPUT_SCRIPT_LEFT_AXIS_X~ ",//D~ ");//LEFT STICK
                " ~INPUT_SCRIPT_LEFT_AXIS_Y~ ",//S~ ");//LEFT STICK
                " ~INPUT_SCRIPT_RIGHT_AXIS_X~ ",//MOUSE RIGHT~ ");//RIGHT STICK
                " ~INPUT_SCRIPT_RIGHT_AXIS_Y~ ",//MOUSE DOWN~ ");//RIGHT STICK
                " ~INPUT_SCRIPT_RUP~ ",//RIGHT MOUSE BUTTON~ ");//Y
                " ~INPUT_SCRIPT_RDOWN~ ",//LEFT MOUSE BUTTON~ ");//A
                " ~INPUT_SCRIPT_RLEFT~ ",//LEFT CTRL~ ");//X
                " ~INPUT_SCRIPT_RRIGHT~ ",//RIGHT MOUSE BUTTON~ ");//B
                " ~INPUT_SCRIPT_LB~ ",//(NONE);~ ");//LB
                " ~INPUT_SCRIPT_RB~ ",//(NONE);~ ");//RB
                " ~INPUT_SCRIPT_LT~ ",//(NONE);~ ");//LT
                " ~INPUT_SCRIPT_RT~ ",//LEFT MOUSE BUTTON~ ");//RT
                " ~INPUT_SCRIPT_LS~ ",//(NONE);~ ");//L3
                " ~INPUT_SCRIPT_RS~ ",//(NONE);~ ");//R3
                " ~INPUT_SCRIPT_PAD_UP~ ",//W~ ");//DPAD UP
                " ~INPUT_SCRIPT_PAD_DOWN~ ",//S~ ");//DPAD DOWN
                " ~INPUT_SCRIPT_PAD_LEFT~ ",//A~ ");//DPAD LEFT
                " ~INPUT_SCRIPT_PAD_RIGHT~ ",//D~ ");//DPAD RIGHT
                " ~INPUT_SCRIPT_SELECT~ ",//V~ ");//BACK
                " ~INPUT_CURSOR_ACCEPT~ ",//LEFT MOUSE BUTTON~ ");//(NONE);
                " ~INPUT_CURSOR_CANCEL~ ",//RIGHT MOUSE BUTTON~ ");//(NONE);
                " ~INPUT_CURSOR_X~ ",//(NONE);~ ");//(NONE);
                " ~INPUT_CURSOR_Y~ ",//(NONE);~ ");//(NONE);
                " ~INPUT_CURSOR_SCROLL_UP~ ",//SCROLLWHEEL UP~ ");//(NONE);
                " ~INPUT_CURSOR_SCROLL_DOWN~ ",//SCROLLWHEEL DOWN~ ");//(NONE);
                " ~INPUT_ENTER_CHEAT_CODE~ ",//~ / `~ ");//(NONE);
                " ~INPUT_INTERACTION_MENU~ ",//M~ ");//BACK
                " ~INPUT_MP_TEXT_CHAT_ALL~ ",//T~ ");//(NONE);
                " ~INPUT_MP_TEXT_CHAT_TEAM~ ",//Y~ ");//(NONE);
                " ~INPUT_MP_TEXT_CHAT_FRIENDS~ ",//(NONE);~ ");//(NONE);
                " ~INPUT_MP_TEXT_CHAT_CREW~ ",//(NONE);~ ");//(NONE);
                " ~INPUT_PUSH_TO_TALK~ ",//N~ ");//(NONE);
                " ~INPUT_CREATOR_LS~ ",//R~ ");//L3
                " ~INPUT_CREATOR_RS~ ",//F~ ");//R3
                " ~INPUT_CREATOR_LT~ ",//X~ ");//LT
                " ~INPUT_CREATOR_RT~ ",//C~ ");//RT
                " ~INPUT_CREATOR_MENU_TOGGLE~ ",//LEFT SHIFT~ ");//(NONE);
                " ~INPUT_CREATOR_ACCEPT~ ",//SPACEBAR~ ");//A
                " ~INPUT_CREATOR_DELETE~ ",//DELETE~ ");//X
                " ~INPUT_ATTACK2~ ",//LEFT MOUSE BUTTON~ ");//RT
                " ~INPUT_RAPPEL_JUMP~ ",//(NONE);~ ");//A
                " ~INPUT_RAPPEL_LONG_JUMP~ ",//(NONE);~ ");//X
                " ~INPUT_RAPPEL_SMASH_WINDOW~ ",//(NONE);~ ");//RT
                " ~INPUT_PREV_WEAPON~ ",//SCROLLWHEEL UP~ ");//DPAD LEFT
                " ~INPUT_NEXT_WEAPON~ ",//SCROLLWHEEL DOWN~ ");//DPAD RIGHT
                " ~INPUT_MELEE_ATTACK1~ ",//R~ ");//B
                " ~INPUT_MELEE_ATTACK2~ ",//Q~ ");//A
                " ~INPUT_WHISTLE~ ",//(NONE);~ ");//(NONE);
                " ~INPUT_MOVE_LEFT~ ",//D~ ");//LEFT STICK
                " ~INPUT_MOVE_RIGHT~ ",//D~ ");//LEFT STICK
                " ~INPUT_MOVE_UP~ ",//S~ ");//LEFT STICK
                " ~INPUT_MOVE_DOWN~ ",//S~ ");//LEFT STICK
                " ~INPUT_LOOK_LEFT~ ",//MOUSE RIGHT~ ");//RIGHT STICK
                " ~INPUT_LOOK_RIGHT~ ",//MOUSE RIGHT~ ");//RIGHT STICK
                " ~INPUT_LOOK_UP~ ",//MOUSE DOWN~ ");//RIGHT STICK
                " ~INPUT_LOOK_DOWN~ ",//MOUSE DOWN~ ");//RIGHT STICK
                " ~INPUT_SNIPER_ZOOM_IN~ ",//[~ ");//RIGHT STICK
                " ~INPUT_SNIPER_ZOOM_OUT~ ",//[~ ");//RIGHT STICK
                " ~INPUT_SNIPER_ZOOM_IN_ALTERNATE~ ",//[~ ");//LEFT STICK
                " ~INPUT_SNIPER_ZOOM_OUT_ALTERNATE~ ",//[~ ");//LEFT STICK
                " ~INPUT_VEH_MOVE_LEFT~ ",//D~ ");//LEFT STICK
                " ~INPUT_VEH_MOVE_RIGHT~ ",//D~ ");//LEFT STICK
                " ~INPUT_VEH_MOVE_UP~ ",//LEFT CTRL~ ");//LEFT STICK
                " ~INPUT_VEH_MOVE_DOWN~ ",//LEFT CTRL~ ");//LEFT STICK
                " ~INPUT_VEH_GUN_LEFT~ ",//MOUSE RIGHT~ ");//RIGHT STICK
                " ~INPUT_VEH_GUN_RIGHT~ ",//MOUSE RIGHT~ ");//RIGHT STICK
                " ~INPUT_VEH_GUN_UP~ ",//MOUSE RIGHT~ ");//RIGHT STICK
                " ~INPUT_VEH_GUN_DOWN~ ",//MOUSE RIGHT~ ");//RIGHT STICK
                " ~INPUT_VEH_LOOK_LEFT~ ",//MOUSE RIGHT~ ");//RIGHT STICK
                " ~INPUT_VEH_LOOK_RIGHT~ ",//MOUSE RIGHT~ ");//RIGHT STICK
                " ~INPUT_REPLAY_START_STOP_RECORDING~ ",//F1~ ");//A
                " ~INPUT_REPLAY_START_STOP_RECORDING_SECONDARY~ ",//F2~ ");//X
                " ~INPUT_SCALED_LOOK_LR~ ",//MOUSE RIGHT~ ");//RIGHT STICK
                " ~INPUT_SCALED_LOOK_UD~ ",//MOUSE DOWN~ ");//RIGHT STICK
                " ~INPUT_SCALED_LOOK_UP_ONLY~ ",//(NONE);~ ");//RIGHT STICK
                " ~INPUT_SCALED_LOOK_DOWN_ONLY~ ",//(NONE);~ ");//RIGHT STICK
                " ~INPUT_SCALED_LOOK_LEFT_ONLY~ ",//(NONE);~ ");//RIGHT STICK
                " ~INPUT_SCALED_LOOK_RIGHT_ONLY~ ",//(NONE);~ ");//RIGHT STICK
                " ~INPUT_REPLAY_MARKER_DELETE~ ",//DELETE~ ");//X
                " ~INPUT_REPLAY_CLIP_DELETE~ ",//DELETE~ ");//Y
                " ~INPUT_REPLAY_PAUSE~ ",//SPACEBAR~ ");//A
                " ~INPUT_REPLAY_REWIND~ ",//ARROW DOWN~ ");//LB
                " ~INPUT_REPLAY_FFWD~ ",//ARROW UP~ ");//RB
                " ~INPUT_REPLAY_NEWMARKER~ ",//M~ ");//A
                " ~INPUT_REPLAY_RECORD~ ",//S~ ");//(NONE);
                " ~INPUT_REPLAY_SCREENSHOT~ ",//U~ ");//DPAD UP
                " ~INPUT_REPLAY_HIDEHUD~ ",//H~ ");//R3
                " ~INPUT_REPLAY_STARTPOINT~ ",//B~ ");//(NONE);
                " ~INPUT_REPLAY_ENDPOINT~ ",//N~ ");//(NONE);
                " ~INPUT_REPLAY_ADVANCE~ ",//ARROW RIGHT~ ");//DPAD RIGHT
                " ~INPUT_REPLAY_BACK~ ",//ARROW LEFT~ ");//DPAD LEFT
                " ~INPUT_REPLAY_TOOLS~ ",//T~ ");//DPAD DOWN
                " ~INPUT_REPLAY_RESTART~ ",//R~ ");//BACK
                " ~INPUT_REPLAY_SHOWHOTKEY~ ",//K~ ");//DPAD DOWN
                " ~INPUT_REPLAY_CYCLEMARKERLEFT~ ",//[~ ");//DPAD LEFT
                " ~INPUT_REPLAY_CYCLEMARKERRIGHT~ ",//]~ ");//DPAD RIGHT
                " ~INPUT_REPLAY_FOVINCREASE~ ",//NUMPAD +~ ");//RB
                " ~INPUT_REPLAY_FOVDECREASE~ ",//NUMPAD -~ ");//LB
                " ~INPUT_REPLAY_CAMERAUP~ ",//PAGE UP~ ");//(NONE);
                " ~INPUT_REPLAY_CAMERADOWN~ ",//PAGE DOWN~ ");//(NONE);
                " ~INPUT_REPLAY_SAVE~ ",//F5~ ");//START
                " ~INPUT_REPLAY_TOGGLETIME~ ",//C~ ");//(NONE);
                " ~INPUT_REPLAY_TOGGLETIPS~ ",//V~ ");//(NONE);
                " ~INPUT_REPLAY_PREVIEW~ ",//SPACEBAR~ ");//(NONE);
                " ~INPUT_REPLAY_TOGGLE_TIMELINE~ ",//ESC~ ");//(NONE);
                " ~INPUT_REPLAY_TIMELINE_PICKUP_CLIP~ ",//X~ ");//(NONE);
                " ~INPUT_REPLAY_TIMELINE_DUPLICATE_CLIP~ ",//C~ ");//(NONE);
                " ~INPUT_REPLAY_TIMELINE_PLACE_CLIP~ ",//V~ ");//(NONE);
                " ~INPUT_REPLAY_CTRL~ ",//LEFT CTRL~ ");//(NONE);
                " ~INPUT_REPLAY_TIMELINE_SAVE~ ",//F5~ ");//(NONE);
                " ~INPUT_REPLAY_PREVIEW_AUDIO~ ",//SPACEBAR~ ");//RT
                " ~INPUT_VEH_DRIVE_LOOK~ ",//LEFT MOUSE BUTTON~ ");//(NONE);
                " ~INPUT_VEH_DRIVE_LOOK2~ ",//RIGHT MOUSE BUTTON~ ");//(NONE);
                " ~INPUT_VEH_FLY_ATTACK2~ ",//RIGHT MOUSE BUTTON~ ");//(NONE);
                " ~INPUT_RADIO_WHEEL_UD~ ",//MOUSE DOWN~ ");//RIGHT STICK
                " ~INPUT_RADIO_WHEEL_LR~ ",//MOUSE RIGHT~ ");//RIGHT STICK
                " ~INPUT_VEH_SLOWMO_UD~ ",//SCROLLWHEEL DOWN~ ");//LEFT STICK
                " ~INPUT_VEH_SLOWMO_UP_ONLY~ ",//SCROLLWHEEL UP~ ");//LEFT STICK
                " ~INPUT_VEH_SLOWMO_DOWN_ONLY~ ",//SCROLLWHEEL DOWN~ ");//LEFT STICK
                " ~INPUT_VEH_HYDRAULICS_CONTROL_TOGGLE~ ",//X~ ");//A
                " ~INPUT_VEH_HYDRAULICS_CONTROL_LEFT~ ",//A~ ");//LEFT STICK
                " ~INPUT_VEH_HYDRAULICS_CONTROL_RIGHT~ ",//D~ ");//LEFT STICK
                " ~INPUT_VEH_HYDRAULICS_CONTROL_UP~ ",//LEFT SHIFT~ ");//LEFT STICK
                " ~INPUT_VEH_HYDRAULICS_CONTROL_DOWN~ ",//LEFT CTRL~ ");//LEFT STICK
                " ~INPUT_VEH_HYDRAULICS_CONTROL_UD~ ",//D~ ");//LEFT STICK
                " ~INPUT_VEH_HYDRAULICS_CONTROL_LR~ ",//LEFT CTRL~ ");//LEFT STICK
                " ~INPUT_SWITCH_VISOR~ ",//F11~ ");//DPAD RIGHT
                " ~INPUT_VEH_MELEE_HOLD~ ",//X~ ");//A
                " ~INPUT_VEH_MELEE_LEFT~ ",//LEFT MOUSE BUTTON~ ");//LB
                " ~INPUT_VEH_MELEE_RIGHT~ ",//RIGHT MOUSE BUTTON~ ");//RB
                " ~INPUT_MAP_POI~ ",//SCROLLWHEEL BUTTON (PRESS);~ ");//Y
                " ~INPUT_REPLAY_SNAPMATIC_PHOTO~ ",//TAB~ ");//X
                " ~INPUT_VEH_CAR_JUMP~ ",//E~ ");//L3
                " ~INPUT_VEH_ROCKET_BOOST~ ",//E~ ");//L3
                " ~INPUT_VEH_FLY_BOOST~ ",//LEFT SHIFT~ ");//L3
                " ~INPUT_VEH_PARACHUTE~ ",//SPACEBAR~ ");//A
                " ~INPUT_VEH_BIKE_WINGS~ ",//X~ ");//A
                " ~INPUT_VEH_FLY_BOMB_BAY~ ",//E~ ");//DPAD RIGHT
                " ~INPUT_VEH_FLY_COUNTER~ ",//E~ ");//DPAD RIGHT
                " ~INPUT_VEH_TRANSFORM~ ",//X~ ");//A
                " ~INPUT_QUAD_LOCO_REVERSE~ ",//~ ");//RB
                " ~INPUT_RESPAWN_FASTER~ ",//~ ");//
                " ~INPUT_HUDMARKER_SELECT~ "
            };

            return ControlList;
        }
        private static List<Vector3> AppLocalList()
        {
            LoggerLight.GetLogging("DataStore.AppLocalList");

            List<Vector3> AppList = new List<Vector3>
            {
                new Vector3(-259.8061F, -969.4397F, 30.21999F),     //.Name = "3 Alta St"          
                new Vector3(-48.77471F, -589.6611F, 36.95303F),     //.Name = "4 Integrity Way"           
                new Vector3(-1441.338F, -544.1608F, 33.74182F),     //.Name = "Del Perro Hts"            
                new Vector3(-778.8126F, 312.6634F, 84.69843F),      //.Name = "Eclipse Towers"           
                new Vector3(-935.0035F, -380.3281F, 37.96134F),     //.Name = "Richards Majestic"            
                new Vector3(-614.3498F, 36.95148F, 42.57011F),      //.Name = "Tinsel Towers"           
                new Vector3(-913.6771F, -456.4787F, 38.59988F),     //.Name = "Weazel Plaza"         
                new Vector3(-687.3952F, 595.6556F, 142.642F),       //.Name = "2862 Hillcrest Avenue"           
                new Vector3(-733.7261F, 594.4695F, 141.181F),       //.Name = "2866 Hillcrest Avenue"      
                new Vector3(-753.1172F, 622.2552F, 141.5237F),      //.Name = "2868 Hillcrest Avenue"
                new Vector3(-854.3719F, 695.4094F, 147.7927F),      //.Name = "2874 Hillcrest Avenue"  
                new Vector3(-1295.323F, 454.9241F, 96.50246F),      //.Name = "2113 Mad Wayne Thunder Drive"   
                new Vector3(-558.5587F, 664.2407F, 144.4564F),      //.Name = "2117 Milton Road"      
                new Vector3(346.2178F, 441.8962F, 146.702F),        //.Name = "2044 North Conker Avenue"         
                new Vector3(372.3179F, 427.8956F, 144.6842F),       //.Name = "2045 North Conker Avenue"       
                new Vector3(118.2447F, 564.3248F, 182.9595F),       //.Name = "3677 Whispymound Drive"        
                new Vector3(-176.128F, 501.8195F, 136.42F),         //.Name = "3655 Wild Oats Drive"          
                new Vector3(-968.9597F, -1433.243F, 6.679171F),     //.Name = "0115 Bay City Ave"           
                new Vector3(-763.0345F, -750.8807F, 26.87314F),     //.Name = "Dream Tower"         
                new Vector3(-1406.162F, 528.3837F, 122.8313F),      //.Name = "4 Hangman Ave"        
                new Vector3(11.93229F, 81.0723F, 77.43513F),        //.Name = "0604 Las Lagunas Blvd"      
                new Vector3(-510.2473F, 108.7654F, 62.80054F),      //.Name = "0184 Milton Rd"       
                new Vector3(284.9267F, -161.9545F, 63.61711F),      //.Name = "1162 Power St"     
                new Vector3(-301.8188F, 6328.303F, 31.88649F),      //.Name = "4401 Procopio Dr"        
                new Vector3(-107.2951F, 6529.278F, 28.85814F),      //.Name = "4584 Procopio Dr"        
                new Vector3(-628.4631F, 168.0906F, 60.14972F),      //.Name = "0504 S Mo Milton Dr"
                new Vector3(-833.2334F, -862.5095F, 19.68971F),     //.Name = "0325 South Rockford Dr"     
                new Vector3(5.622878F, 36.0617F, 70.53041F),        //.Name = "0605 Spanish Ave"       
                new Vector3(1336.244F, -1579.887F, 53.05425F),      //.Name = "12 Sustancia Rd"   
                new Vector3(-198.8772F, 86.29745F, 68.75475F),      //.Name = "The Royale"    
                new Vector3(-1607.753F, -433.6076F, 39.42718F),     //.Name = "1115 Blvd Del Perro"    
                new Vector3(-201.2312F, 185.0877F, 79.32613F),      //.Name = "1561 San Vitas St" 
                new Vector3(-1562.376F, -404.384F, 41.38401F),      //.Name = "1237 Prosperity St"      
                new Vector3(-1532.888F, -325.702F, 46.9112F),       //.Name = "0069 Cougar Ave"     
                new Vector3(-41.5342F, -59.85516F, 62.65963F),      //.Name = "2143 Las Lagunas Blvd"  
                new Vector3(1662.762F, 4775.079F, 41.00756F),       //.Name = "1893 Grapeseed Ave"     
                new Vector3(-14.08983F, 6556.662F, 32.24046F),      //.Name = "0232 Paleto Blvd"      
                new Vector3(-813.6389F, -979.8933F, 13.1934F),      //.Name = "0112 S Rockford Dr"       
                new Vector3(-663.3229F, -853.7469F, 23.44383F),     //.Name = "2057 Vespucci Blvd"         
                new Vector3(1900.146F, 3781.11F, 31.81827F)         //.Name = "140 Zancudo Ave"
            };

            return AppList;
        }
    }
}
