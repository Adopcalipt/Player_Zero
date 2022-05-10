using System;
using System.IO;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
using System.Drawing;
using System.Windows.Forms;

namespace PlayerZeroSettings
{
    public partial class Form1 : Form
    {
        private int _lastFormSize;
        private IniFile MyIni = null;
        private IniSettings MyIniSettings = null;
        private readonly string sPath = "" + Directory.GetCurrentDirectory() + "/PZSet.ini";
        private List<string> ControlerSet = new List<string>();

        class IniFile   // revision 11
        {
            string Path;
            string EXE = "PZSet";

            [DllImport("kernel32", CharSet = CharSet.Unicode)]
            static extern long WritePrivateProfileString(string Section, string Key, string Value, string FilePath);

            [DllImport("kernel32", CharSet = CharSet.Unicode)]
            static extern int GetPrivateProfileString(string Section, string Key, string Default, StringBuilder RetVal, int Size, string FilePath);

            public IniFile()
            {
                Path = "" + Directory.GetCurrentDirectory() + "/PZSet.ini";
            }
            public string Read(string Key, string Section = null)
            {
                var RetVal = new StringBuilder(255);
                GetPrivateProfileString(Section ?? EXE, Key, "", RetVal, 255, Path);
                return RetVal.ToString();
            }
            public void Write(string Key, string Value, string Section = null)
            {
                WritePrivateProfileString(Section ?? EXE, Key, Value, Path);
            }
            public void DeleteKey(string Key, string Section = null)
            {
                Write(Key, null, Section ?? EXE);
            }
            public void DeleteSection(string Section = null)
            {
                Write(null, null, Section ?? EXE);
            }
            public bool KeyExists(string Key, string Section = null)
            {
                return Read(Key, Section).Length > 0;
            }
        }
        public decimal GetSeconds(bool bMin, int iThous)
        {
            decimal MyD = iThous / 1000;

            if (bMin)
                MyD = MyD / 60;
            return MyD;
        }
        public int SetSeconds(bool bMin, decimal iSec)
        {
            int MyI = (int)iSec * 1000;

            if (bMin)
                MyI = MyI * 60;
            return MyI;
        }
        public int ReadMyInt(string sTing)
        {
            int iNum = 0;
            int iTimes = 0;
            for (int i = sTing.Length - 1; i > -1; i--)
            {
                int iAdd = 0;
                string sComp = sTing.Substring(i, 1);

                if (sComp == "1")
                    iAdd = 1;
                else if (sComp == "2")
                    iAdd = 2;
                else if (sComp == "3")
                    iAdd = 3;
                else if (sComp == "4")
                    iAdd = 4;
                else if (sComp == "5")
                    iAdd = 5;
                else if (sComp == "6")
                    iAdd = 6;
                else if (sComp == "7")
                    iAdd = 7;
                else if (sComp == "8")
                    iAdd = 8;
                else if (sComp == "9")
                    iAdd = 9;

                if (iTimes == 0)
                {
                    iNum = iAdd;
                    iTimes = 1;
                }
                else
                    iNum += iAdd * iTimes;
                iTimes *= 10;
            }
            return iNum;
        }
        public bool ReadMyBool(string sTing)
        {
            bool b = false;
            if (sTing.Contains("True") || sTing.Contains("true") || sTing.Contains("TRUE"))
                b = true;
            return b;
        }
        private int GetFormArea(Size size)
        {
            return size.Height * size.Width;
        }
        private void ResizeAlize(object sender, EventArgs e)
        {
            if (GetFormArea(this.Size) > _lastFormSize + 100 || GetFormArea(this.Size) < _lastFormSize - 100)
            {
                Control control = (Control)sender;
                float scaleFactor = (float)GetFormArea(control.Size) / (float)_lastFormSize;
                ResizeFont(this.Controls, scaleFactor);
                _lastFormSize = GetFormArea(control.Size);
            }
        }
        private void ResizeFont(Control.ControlCollection coll, float scaleFactor)
        {
            foreach (Control c in coll)
            {
                if (c.HasChildren)
                {
                    ResizeFont(c.Controls, scaleFactor);
                }
                else
                {
                    if (true)
                    {
                        c.Font = new Font(c.Font.FontFamily.Name, c.Font.Size * scaleFactor);
                    }
                }
            }
        }
        public Form1()
        {
            ControlList();
            InitializeComponent();
            MyIniSettings = SetBuild();
            this.Resize += new EventHandler(ResizeAlize);
            _lastFormSize = GetFormArea(this.Size);
            LoadIniSettings();
        }
        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            MyIniSettings.Aggression = (int)numericUpDown1.Value;
            MyIni.Write("Aggression", "" + MyIniSettings.Aggression + "", "Settings");
        }
        private void numericUpDown2_ValueChanged(object sender, EventArgs e)
        {
            MyIniSettings.MaxPlayers = (int)numericUpDown2.Value;
            MyIni.Write("MaxPlayers", "" + MyIniSettings.MaxPlayers + "", "Settings");
        }
        private void numericUpDown3_ValueChanged(object sender, EventArgs e)
        {
            if (numericUpDown3.Value > numericUpDown4.Value)
                numericUpDown3.Value = numericUpDown4.Value;
            MyIniSettings.MinWait = SetSeconds(false, numericUpDown3.Value);
            MyIni.Write("MinWait", "" + MyIniSettings.MinWait + "", "Settings");
        }
        private void numericUpDown4_ValueChanged(object sender, EventArgs e)
        {
            if (numericUpDown4.Value < numericUpDown3.Value)
                numericUpDown4.Value = numericUpDown3.Value;
            MyIniSettings.MaxWait = SetSeconds(false, numericUpDown4.Value);
            MyIni.Write("MaxWait", "" + MyIniSettings.MaxWait + "", "Settings");
        }
        private void numericUpDown5_ValueChanged(object sender, EventArgs e)
        {
            if (numericUpDown5.Value > numericUpDown6.Value)
                numericUpDown5.Value = numericUpDown6.Value;
            MyIniSettings.MinSession = SetSeconds(true, numericUpDown5.Value);
            MyIni.Write("MinSession", "" + MyIniSettings.MinSession + "", "Settings");
        }
        private void numericUpDown6_ValueChanged(object sender, EventArgs e)
        {
            if (numericUpDown6.Value < numericUpDown5.Value)
                numericUpDown6.Value = numericUpDown5.Value;
            MyIniSettings.MaxSession = SetSeconds(true, numericUpDown6.Value);
            MyIni.Write("MaxSession", "" + MyIniSettings.MaxSession + "", "Settings");
        }
        private void numericUpDown7_ValueChanged(object sender, EventArgs e)
        {
            if (numericUpDown7.Value > numericUpDown8.Value)
                numericUpDown7.Value = numericUpDown8.Value;
            MyIniSettings.MinAccuracy = (int)numericUpDown7.Value;
            MyIni.Write("MinAccuracy", "" + MyIniSettings.MinAccuracy + "", "Settings");
        }
        private void numericUpDown8_ValueChanged(object sender, EventArgs e)
        {
            if (numericUpDown8.Value < numericUpDown7.Value)
                numericUpDown8.Value = numericUpDown7.Value;
            MyIniSettings.MaxAccuracy = (int)numericUpDown8.Value;
            MyIni.Write("MaxAccuracy", "" + MyIniSettings.MaxAccuracy + "", "Settings");
        }
        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            bool MySpace = false;
            if (checkBox1.Checked)
                MySpace = true;
            MyIniSettings.SpaceWeaps = MySpace;
            MyIni.Write("SpaceWeaps", "" + MySpace + "", "Settings");
        }
        private void numericUpDown9_ValueChanged(object sender, EventArgs e)
        {
            MyIniSettings.GetlayList = (int)numericUpDown9.Value;
            MyIni.Write("Players", "" + MyIniSettings.GetlayList + "", "Controls");
            textBox1.Text = ControlerSet[MyIniSettings.GetlayList];
        }
        private void numericUpDown10_ValueChanged(object sender, EventArgs e)
        {
            MyIniSettings.ClearPlayList = (int)numericUpDown10.Value;
            MyIni.Write("ClearPlayers", "" + MyIniSettings.ClearPlayList + "", "Controls");
            textBox2.Text = ControlerSet[MyIniSettings.ClearPlayList];
        }
        private void numericUpDown11_ValueChanged(object sender, EventArgs e)
        {
            MyIniSettings.DisableMod = (int)numericUpDown11.Value;
            MyIni.Write("DisableMod", "" + MyIniSettings.DisableMod + "", "Controls");
            textBox3.Text = ControlerSet[MyIniSettings.DisableMod];
        }
        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            bool MySpace = false;
            if (checkBox2.Checked)
                MySpace = true;
            MyIniSettings.Debugger = MySpace;
            MyIni.Write("Debug", "" + checkBox2.Checked + "", "Settings");
        }
        private void button1_Click(object sender, EventArgs e)
        {
            Close();
        }
        private void ControlList()
        {
            ControlerSet.Clear();
            ControlerSet.Add("INPUT_NEXT_CAMERA 	0 	V");
            ControlerSet.Add("INPUT_LOOK_LR 	1 	Left Mouse Button");
            ControlerSet.Add("INPUT_LOOK_UD 	2 	Right Mouse Button");
            ControlerSet.Add("INPUT_LOOK_UP_ONLY 	3 	Control-Break Processing");
            ControlerSet.Add("INPUT_LOOK_DOWN_ONLY 	4 	Middle Mouse Button");
            ControlerSet.Add("INPUT_LOOK_LEFT_ONLY 	5");
            ControlerSet.Add("INPUT_LOOK_RIGHT_ONLY 	6 	Right Mouse Button");
            ControlerSet.Add("INPUT_CINEMATIC_SLOWMO 	7 	L");
            ControlerSet.Add("INPUT_SCRIPTED_FLY_UD 	8 	S");
            ControlerSet.Add("INPUT_SCRIPTED_FLY_LR 	9 	D");
            ControlerSet.Add("INPUT_SCRIPTED_FLY_ZUP 	10 	PAGE UP");
            ControlerSet.Add("INPUT_SCRIPTED_FLY_ZDOWN 	11 	PAGE DOWN");
            ControlerSet.Add("INPUT_WEAPON_WHEEL_UD 	12 	MOUSE DOWN");
            ControlerSet.Add("INPUT_WEAPON_WHEEL_LR 	13 	MOUSE RIGHT");
            ControlerSet.Add("INPUT_WEAPON_WHEEL_NEXT 	14 	MOUSE SCROLL WHEEL DOWN");
            ControlerSet.Add("INPUT_WEAPON_WHEEL_PREV 	15 	MOUSE SCROLL WHEEL UP");
            ControlerSet.Add("INPUT_SELECT_NEXT_WEAPON 	16 	MOUSE SCROLL WHEEL DOWN");
            ControlerSet.Add("INPUT_SELECT_PREV_WEAPON 	17 	MOUSE SCROLL WHEEL UP");
            ControlerSet.Add("INPUT_SKIP_CUTSCENE 	18 	ENTER / LEFT MOUSE BUTTON / SPACEBAR");
            ControlerSet.Add("INPUT_CHARACTER_WHEEL 	19 	LEFT ALT");
            ControlerSet.Add("INPUT_MULTIPLAYER_INFO 	20 	Z");
            ControlerSet.Add("INPUT_SPRINT 	21 	Shift Key");
            ControlerSet.Add("INPUT_JUMP 	22 	Space Key");
            ControlerSet.Add("INPUT_ENTER 	23 	Enter Key");
            ControlerSet.Add("INPUT_ATTACK 	24 	Left Mouse Button");
            ControlerSet.Add("INPUT_AIM 	25 	Right Mouse Button");
            ControlerSet.Add("INPUT_LOOK_BEHIND 	26 	C");
            ControlerSet.Add("INPUT_PHONE 	27 	ARROW UP/SCROLLWHEEL BUTTON (press)");
            ControlerSet.Add("INPUT_SPECIAL_ABILITY 	28 	");
            ControlerSet.Add("INPUT_SPECIAL_ABILITY_SECONDARY 	29 	B");
            ControlerSet.Add("INPUT_MOVE_LR 	30 	D");
            ControlerSet.Add("INPUT_MOVE_UD 	31 	S");
            ControlerSet.Add("INPUT_MOVE_UP_ONLY 	32 	W");
            ControlerSet.Add("INPUT_MOVE_DOWN_ONLY 	33 	S");
            ControlerSet.Add("INPUT_MOVE_LEFT_ONLY 	34 	A");
            ControlerSet.Add("INPUT_MOVE_RIGHT_ONLY 	35 	D");
            ControlerSet.Add("INPUT_DUCK 	36 	LEFT CONTROL");
            ControlerSet.Add("INPUT_SELECT_WEAPON 	37 	TAB");
            ControlerSet.Add("INPUT_PICKUP 	38 	E");
            ControlerSet.Add("INPUT_SNIPER_ZOOM 	39 	[");
            ControlerSet.Add("INPUT_SNIPER_ZOOM_IN_ONLY 	40 	]");
            ControlerSet.Add("INPUT_SNIPER_ZOOM_OUT_ONLY 	41 	[");
            ControlerSet.Add("INPUT_SNIPER_ZOOM_IN_SECONDARY 	42 	]");
            ControlerSet.Add("INPUT_SNIPER_ZOOM_OUT_SECONDARY 	43 	[");
            ControlerSet.Add("INPUT_COVER 	44 	Q");
            ControlerSet.Add("INPUT_RELOAD 	45 	R");
            ControlerSet.Add("INPUT_TALK 	46 	E");
            ControlerSet.Add("INPUT_DETONATE 	47 	G");
            ControlerSet.Add("INPUT_HUD_SPECIAL 	48 	Z");
            ControlerSet.Add("INPUT_ARREST 	49 	F");
            ControlerSet.Add("INPUT_ACCURATE_AIM 	50 	SCROLLWHEEL DOWN");
            ControlerSet.Add("INPUT_CONTEXT 	51 	E");
            ControlerSet.Add("INPUT_CONTEXT_SECONDARY 	52 	Q");
            ControlerSet.Add("INPUT_WEAPON_SPECIAL 	53 	");
            ControlerSet.Add("INPUT_WEAPON_SPECIAL_TWO 	54 	E");
            ControlerSet.Add("INPUT_DIVE 	55 	SPACEBAR");
            ControlerSet.Add("INPUT_DROP_WEAPON 	56 	F9");
            ControlerSet.Add("INPUT_DROP_AMMO 	57 	F10");
            ControlerSet.Add("INPUT_THROW_GRENADE 	58 	G");
            ControlerSet.Add("INPUT_VEH_MOVE_LR 	59 	D");
            ControlerSet.Add("INPUT_VEH_MOVE_UD 	60 	LEFT CTRL");
            ControlerSet.Add("INPUT_VEH_MOVE_UP_ONLY 	61 	LEFT SHIFT");
            ControlerSet.Add("INPUT_VEH_MOVE_DOWN_ONLY 	62 	LEFT CTRL");
            ControlerSet.Add("INPUT_VEH_MOVE_LEFT_ONLY 	63 	A");
            ControlerSet.Add("INPUT_VEH_MOVE_RIGHT_ONLY 	64 	D");
            ControlerSet.Add("INPUT_VEH_SPECIAL 	65 	");
            ControlerSet.Add("INPUT_VEH_GUN_LR 	66 	MOUSE RIGHT");
            ControlerSet.Add("INPUT_VEH_GUN_UD 	67 	MOUSE DOWN");
            ControlerSet.Add("INPUT_VEH_AIM 	68 	RIGHT MOUSE BUTTON");
            ControlerSet.Add("INPUT_VEH_ATTACK 	69 	LEFT MOUSE BUTTON");
            ControlerSet.Add("INPUT_VEH_ATTACK2 	70 	RIGHT MOUSE BUTTON");
            ControlerSet.Add("INPUT_VEH_ACCELERATE 	71 	W");
            ControlerSet.Add("INPUT_VEH_BRAKE 	72 	S");
            ControlerSet.Add("INPUT_VEH_DUCK 	73 	X");
            ControlerSet.Add("INPUT_VEH_HEADLIGHT 	74 	H");
            ControlerSet.Add("INPUT_VEH_EXIT 	75 	F");
            ControlerSet.Add("INPUT_VEH_HANDBRAKE 	76 	SPACEBAR");
            ControlerSet.Add("INPUT_VEH_HOTWIRE_LEFT 	77 	W");
            ControlerSet.Add("INPUT_VEH_HOTWIRE_RIGHT 	78 	S");
            ControlerSet.Add("INPUT_VEH_LOOK_BEHIND 	79 	C");
            ControlerSet.Add("INPUT_VEH_CIN_CAM 	80 	R");
            ControlerSet.Add("INPUT_VEH_NEXT_RADIO 	81 	.");
            ControlerSet.Add("INPUT_VEH_PREV_RADIO 	82 	,");
            ControlerSet.Add("INPUT_VEH_NEXT_RADIO_TRACK 	83 	=");
            ControlerSet.Add("INPUT_VEH_PREV_RADIO_TRACK 	84 	-");
            ControlerSet.Add("INPUT_VEH_RADIO_WHEEL 	85 	Q");
            ControlerSet.Add("INPUT_VEH_HORN 	86 	E");
            ControlerSet.Add("INPUT_VEH_FLY_THROTTLE_UP 	87 	W");
            ControlerSet.Add("INPUT_VEH_FLY_THROTTLE_DOWN 	88 	S");
            ControlerSet.Add("INPUT_VEH_FLY_YAW_LEFT 	89 	A");
            ControlerSet.Add("INPUT_VEH_FLY_YAW_RIGHT 	90 	D");
            ControlerSet.Add("INPUT_VEH_PASSENGER_AIM 	91 	RIGHT MOUSE BUTTON");
            ControlerSet.Add("INPUT_VEH_PASSENGER_ATTACK 	92 	LEFT MOUSE BUTTON");
            ControlerSet.Add("INPUT_VEH_SPECIAL_ABILITY_FRANKLIN 	93 	");
            ControlerSet.Add("INPUT_VEH_STUNT_UD 	94 	");
            ControlerSet.Add("INPUT_VEH_CINEMATIC_UD 	95 	MOUSE DOWN");
            ControlerSet.Add("INPUT_VEH_CINEMATIC_UP_ONLY 	96 	NUMPAD- / SCROLLWHEEL UP");
            ControlerSet.Add("INPUT_VEH_CINEMATIC_DOWN_ONLY 	97 	NUMPAD+ / SCROLLWHEEL DOWN");
            ControlerSet.Add("INPUT_VEH_CINEMATIC_LR 	98 	MOUSE RIGHT");
            ControlerSet.Add("INPUT_VEH_SELECT_NEXT_WEAPON 	99 	SCROLLWHEEL UP");
            ControlerSet.Add("INPUT_VEH_SELECT_PREV_WEAPON 	100 	[");
            ControlerSet.Add("INPUT_VEH_ROOF 	101 	H");
            ControlerSet.Add("INPUT_VEH_JUMP 	102 	SPACEBAR");
            ControlerSet.Add("INPUT_VEH_GRAPPLING_HOOK 	103 	E");
            ControlerSet.Add("INPUT_VEH_SHUFFLE 	104 	H");
            ControlerSet.Add("INPUT_VEH_DROP_PROJECTILE 	105 	X");
            ControlerSet.Add("INPUT_VEH_MOUSE_CONTROL_OVERRIDE 	106 	LEFT MOUSE BUTTON");
            ControlerSet.Add("INPUT_VEH_FLY_ROLL_LR 	107 	NUMPAD 6");
            ControlerSet.Add("INPUT_VEH_FLY_ROLL_LEFT_ONLY 	108 	NUMPAD 4");
            ControlerSet.Add("INPUT_VEH_FLY_ROLL_RIGHT_ONLY 	109 	NUMPAD 6");
            ControlerSet.Add("INPUT_VEH_FLY_PITCH_UD 	110 	NUMPAD 5");
            ControlerSet.Add("INPUT_VEH_FLY_PITCH_UP_ONLY 	111 	NUMPAD 8");
            ControlerSet.Add("INPUT_VEH_FLY_PITCH_DOWN_ONLY 	112 	NUMPAD 5");
            ControlerSet.Add("INPUT_VEH_FLY_UNDERCARRIAGE 	113 	G");
            ControlerSet.Add("INPUT_VEH_FLY_ATTACK 	114 	RIGHT MOUSE BUTTON");
            ControlerSet.Add("INPUT_VEH_FLY_SELECT_NEXT_WEAPON 	115 	SCROLLWHEEL UP");
            ControlerSet.Add("INPUT_VEH_FLY_SELECT_PREV_WEAPON 	116 	[");
            ControlerSet.Add("INPUT_VEH_FLY_SELECT_TARGET_LEFT 	117 	NUMPAD 7");
            ControlerSet.Add("INPUT_VEH_FLY_SELECT_TARGET_RIGHT 	118 	NUMPAD 9");
            ControlerSet.Add("INPUT_VEH_FLY_VERTICAL_FLIGHT_MODE 	119 	E");
            ControlerSet.Add("INPUT_VEH_FLY_DUCK 	120 	X");
            ControlerSet.Add("INPUT_VEH_FLY_ATTACK_CAMERA 	121 	INSERT");
            ControlerSet.Add("INPUT_VEH_FLY_MOUSE_CONTROL_OVERRIDE 	122 	LEFT MOUSE BUTTON");
            ControlerSet.Add("INPUT_VEH_SUB_TURN_LR 	123 	NUMPAD 6");
            ControlerSet.Add("INPUT_VEH_SUB_TURN_LEFT_ONLY 	124 	NUMPAD 4");
            ControlerSet.Add("INPUT_VEH_SUB_TURN_RIGHT_ONLY 	125 	NUMPAD 6");
            ControlerSet.Add("INPUT_VEH_SUB_PITCH_UD 	126 	NUMPAD 5");
            ControlerSet.Add("INPUT_VEH_SUB_PITCH_UP_ONLY 	127 	NUMPAD 8");
            ControlerSet.Add("INPUT_VEH_SUB_PITCH_DOWN_ONLY 	128 	NUMPAD 5");
            ControlerSet.Add("INPUT_VEH_SUB_THROTTLE_UP 	129 	W");
            ControlerSet.Add("INPUT_VEH_SUB_THROTTLE_DOWN 	130 	S");
            ControlerSet.Add("INPUT_VEH_SUB_ASCEND 	131 	LEFT SHIFT");
            ControlerSet.Add("INPUT_VEH_SUB_DESCEND 	132 	LEFT CTRL");
            ControlerSet.Add("INPUT_VEH_SUB_TURN_HARD_LEFT 	133 	A");
            ControlerSet.Add("INPUT_VEH_SUB_TURN_HARD_RIGHT 	134 	D");
            ControlerSet.Add("INPUT_VEH_SUB_MOUSE_CONTROL_OVERRIDE 	135 	LEFT MOUSE BUTTON");
            ControlerSet.Add("INPUT_VEH_PUSHBIKE_PEDAL 	136 	W");
            ControlerSet.Add("INPUT_VEH_PUSHBIKE_SPRINT 	137 	CAPSLOCK");
            ControlerSet.Add("INPUT_VEH_PUSHBIKE_FRONT_BRAKE 	138 	Q");
            ControlerSet.Add("INPUT_VEH_PUSHBIKE_REAR_BRAKE 	139 	S");
            ControlerSet.Add("INPUT_MELEE_ATTACK_LIGHT 	140 	R");
            ControlerSet.Add("INPUT_MELEE_ATTACK_HEAVY 	141 	Q");
            ControlerSet.Add("INPUT_MELEE_ATTACK_ALTERNATE 	142 	LEFT MOUSE BUTTON");
            ControlerSet.Add("INPUT_MELEE_BLOCK 	143 	SPACEBAR");
            ControlerSet.Add("INPUT_PARACHUTE_DEPLOY 	144 	F / LEFT MOUSE BUTTON");
            ControlerSet.Add("INPUT_PARACHUTE_DETACH 	145 	F");
            ControlerSet.Add("INPUT_PARACHUTE_TURN_LR 	146 	D");
            ControlerSet.Add("INPUT_PARACHUTE_TURN_LEFT_ONLY 	147 	A");
            ControlerSet.Add("INPUT_PARACHUTE_TURN_RIGHT_ONLY 	148 	D");
            ControlerSet.Add("INPUT_PARACHUTE_PITCH_UD 	149 	S");
            ControlerSet.Add("INPUT_PARACHUTE_PITCH_UP_ONLY 	150 	W");
            ControlerSet.Add("INPUT_PARACHUTE_PITCH_DOWN_ONLY 	151 	S");
            ControlerSet.Add("INPUT_PARACHUTE_BRAKE_LEFT 	152 	Q");
            ControlerSet.Add("INPUT_PARACHUTE_BRAKE_RIGHT 	153 	E");
            ControlerSet.Add("INPUT_PARACHUTE_SMOKE 	154 	X");
            ControlerSet.Add("INPUT_PARACHUTE_PRECISION_LANDING 	155 	LEFT SHIFT");
            ControlerSet.Add("INPUT_MAP 	156 	");
            ControlerSet.Add("INPUT_SELECT_WEAPON_UNARMED 	157 	1");
            ControlerSet.Add("INPUT_SELECT_WEAPON_MELEE 	158 	2");
            ControlerSet.Add("INPUT_SELECT_WEAPON_HANDGUN 	159 	6");
            ControlerSet.Add("INPUT_SELECT_WEAPON_SHOTGUN 	160 	3");
            ControlerSet.Add("INPUT_SELECT_WEAPON_SMG 	161 	7");
            ControlerSet.Add("INPUT_SELECT_WEAPON_AUTO_RIFLE 	162 	8");
            ControlerSet.Add("INPUT_SELECT_WEAPON_SNIPER 	163 	9");
            ControlerSet.Add("INPUT_SELECT_WEAPON_HEAVY 	164 	4");
            ControlerSet.Add("INPUT_SELECT_WEAPON_SPECIAL 	165 	5");
            ControlerSet.Add("INPUT_SELECT_CHARACTER_MICHAEL 	166 	F5");
            ControlerSet.Add("INPUT_SELECT_CHARACTER_FRANKLIN 	167 	F6");
            ControlerSet.Add("INPUT_SELECT_CHARACTER_TREVOR 	168 	F7");
            ControlerSet.Add("INPUT_SELECT_CHARACTER_MULTIPLAYER 	169 	F8");
            ControlerSet.Add("INPUT_SAVE_REPLAY_CLIP 	170 	F3");
            ControlerSet.Add("INPUT_SPECIAL_ABILITY_PC 	171 	CAPSLOCK");
            ControlerSet.Add("INPUT_CELLPHONE_UP 	172 	ARROW UP");
            ControlerSet.Add("INPUT_CELLPHONE_DOWN 	173 	ARROW DOWN");
            ControlerSet.Add("INPUT_CELLPHONE_LEFT 	174 	ARROW LEFT");
            ControlerSet.Add("INPUT_CELLPHONE_RIGHT 	175 	ARROW RIGHT");
            ControlerSet.Add("INPUT_CELLPHONE_SELECT 	176 	ENTER/LEFT MOUSE BUTTON");
            ControlerSet.Add("INPUT_CELLPHONE_CANCEL 	177 	BACKSPACE/ESC/RIGHT MOUSE BUTTON");
            ControlerSet.Add("INPUT_CELLPHONE_OPTION 	178 	DELETE");
            ControlerSet.Add("INPUT_CELLPHONE_EXTRA_OPTION 	179 	SPACEBAR");
            ControlerSet.Add("INPUT_CELLPHONE_SCROLL_FORWARD 	180 	SCROLLWHEEL DOWN");
            ControlerSet.Add("INPUT_CELLPHONE_SCROLL_BACKWARD 	181 	SCROLLWHEEL UP");
            ControlerSet.Add("INPUT_CELLPHONE_CAMERA_FOCUS_LOCK 	182 	L");
            ControlerSet.Add("INPUT_CELLPHONE_CAMERA_GRID 	183 	G");
            ControlerSet.Add("INPUT_CELLPHONE_CAMERA_SELFIE 	184 	E");
            ControlerSet.Add("INPUT_CELLPHONE_CAMERA_DOF 	185 	F");
            ControlerSet.Add("INPUT_CELLPHONE_CAMERA_EXPRESSION 	186 	X");
            ControlerSet.Add("INPUT_FRONTEND_DOWN 	187 	ARROW DOWN");
            ControlerSet.Add("INPUT_FRONTEND_UP 	188 	ARROW UP");
            ControlerSet.Add("INPUT_FRONTEND_LEFT 	189 	ARROW LEFT");
            ControlerSet.Add("INPUT_FRONTEND_RIGHT 	190 	ARROW RIGHT");
            ControlerSet.Add("INPUT_FRONTEND_RDOWN 	191 	ENTER");
            ControlerSet.Add("INPUT_FRONTEND_RUP 	192 	TAB");
            ControlerSet.Add("INPUT_FRONTEND_RLEFT 	193 	");
            ControlerSet.Add("INPUT_FRONTEND_RRIGHT 	194 	BACKSPACE");
            ControlerSet.Add("INPUT_FRONTEND_AXIS_X 	195 	D");
            ControlerSet.Add("INPUT_FRONTEND_AXIS_Y 	196 	S");
            ControlerSet.Add("INPUT_FRONTEND_RIGHT_AXIS_X 	197 	]");
            ControlerSet.Add("INPUT_FRONTEND_RIGHT_AXIS_Y 	198 	SCROLLWHEEL DOWN");
            ControlerSet.Add("INPUT_FRONTEND_PAUSE 	199 	P");
            ControlerSet.Add("INPUT_FRONTEND_PAUSE_ALTERNATE 	200 	ESC");
            ControlerSet.Add("INPUT_FRONTEND_ACCEPT 	201 	ENTER/NUMPAD ENTER");
            ControlerSet.Add("INPUT_FRONTEND_CANCEL 	202 	BACKSPACE/ESC");
            ControlerSet.Add("INPUT_FRONTEND_X 	203 	SPACEBAR");
            ControlerSet.Add("INPUT_FRONTEND_Y 	204 	TAB");
            ControlerSet.Add("INPUT_FRONTEND_LB 	205 	Q");
            ControlerSet.Add("INPUT_FRONTEND_RB 	206 	E");
            ControlerSet.Add("INPUT_FRONTEND_LT 	207 	PAGE DOWN");
            ControlerSet.Add("INPUT_FRONTEND_RT 	208 	PAGE UP");
            ControlerSet.Add("INPUT_FRONTEND_LS 	209 	LEFT SHIFT");
            ControlerSet.Add("INPUT_FRONTEND_RS 	210 	LEFT CTRL");
            ControlerSet.Add("INPUT_FRONTEND_LEADERBOARD 	211 	TAB");
            ControlerSet.Add("INPUT_FRONTEND_SOCIAL_CLUB 	212 	HOME");
            ControlerSet.Add("INPUT_FRONTEND_SOCIAL_CLUB_SECONDARY 	213 	HOME");
            ControlerSet.Add("INPUT_FRONTEND_DELETE 	214 	DELETE");
            ControlerSet.Add("INPUT_FRONTEND_ENDSCREEN_ACCEPT 	215 	ENTER");
            ControlerSet.Add("INPUT_FRONTEND_ENDSCREEN_EXPAND 	216 	SPACEBAR");
            ControlerSet.Add("INPUT_FRONTEND_SELECT 	217 	CAPSLOCK");
            ControlerSet.Add("INPUT_SCRIPT_LEFT_AXIS_X 	218 	D");
            ControlerSet.Add("INPUT_SCRIPT_LEFT_AXIS_Y 	219 	S");
            ControlerSet.Add("INPUT_SCRIPT_RIGHT_AXIS_X 	220 	MOUSE RIGHT");
            ControlerSet.Add("INPUT_SCRIPT_RIGHT_AXIS_Y 	221 	MOUSE DOWN");
            ControlerSet.Add("INPUT_SCRIPT_RUP 	222 	RIGHT MOUSE BUTTON");
            ControlerSet.Add("INPUT_SCRIPT_RDOWN 	223 	LEFT MOUSE BUTTON");
            ControlerSet.Add("INPUT_SCRIPT_RLEFT 	224 	LEFT CTRL");
            ControlerSet.Add("INPUT_SCRIPT_RRIGHT 	225 	RIGHT MOUSE BUTTON");
            ControlerSet.Add("INPUT_SCRIPT_LB 	226 	");
            ControlerSet.Add("INPUT_SCRIPT_RB 	227 	");
            ControlerSet.Add("INPUT_SCRIPT_LT 	228 	");
            ControlerSet.Add("INPUT_SCRIPT_RT 	229 	LEFT MOUSE BUTTON");
            ControlerSet.Add("INPUT_SCRIPT_LS 	230 	");
            ControlerSet.Add("INPUT_SCRIPT_RS 	231 	");
            ControlerSet.Add("INPUT_SCRIPT_PAD_UP 	232 	W");
            ControlerSet.Add("INPUT_SCRIPT_PAD_DOWN 	233 	S");
            ControlerSet.Add("INPUT_SCRIPT_PAD_LEFT 	234 	A");
            ControlerSet.Add("INPUT_SCRIPT_PAD_RIGHT 	235 	D");
            ControlerSet.Add("INPUT_SCRIPT_SELECT 	236 	V");
            ControlerSet.Add("INPUT_CURSOR_ACCEPT 	237 	LEFT MOUSE BUTTON");
            ControlerSet.Add("INPUT_CURSOR_CANCEL 	238 	RIGHT MOUSE BUTTON");
            ControlerSet.Add("INPUT_CURSOR_X 	239 	");
            ControlerSet.Add("INPUT_CURSOR_Y 	240 	");
            ControlerSet.Add("INPUT_CURSOR_SCROLL_UP 	241 	SCROLLWHEEL UP");
            ControlerSet.Add("INPUT_CURSOR_SCROLL_DOWN 	242 	SCROLLWHEEL DOWN");
            ControlerSet.Add("INPUT_ENTER_CHEAT_CODE 	243 	~ / `");
            ControlerSet.Add("INPUT_INTERACTION_MENU 	244 	M");
            ControlerSet.Add("INPUT_MP_TEXT_CHAT_ALL 	245 	T");
            ControlerSet.Add("INPUT_MP_TEXT_CHAT_TEAM 	246 	Y");
            ControlerSet.Add("INPUT_MP_TEXT_CHAT_FRIENDS 	247 	");
            ControlerSet.Add("INPUT_MP_TEXT_CHAT_CREW 	248 	");
            ControlerSet.Add("INPUT_PUSH_TO_TALK 	249 	N");
            ControlerSet.Add("INPUT_CREATOR_LS 	250 	R");
            ControlerSet.Add("INPUT_CREATOR_RS 	251 	F");
            ControlerSet.Add("INPUT_CREATOR_LT 	252 	X");
            ControlerSet.Add("INPUT_CREATOR_RT 	253 	C");
            ControlerSet.Add("INPUT_CREATOR_MENU_TOGGLE 	254 	LEFT SHIFT");
            ControlerSet.Add("INPUT_CREATOR_ACCEPT 	255 	SPACEBAR");
            ControlerSet.Add("INPUT_CREATOR_DELETE 	256 	DELETE");
            ControlerSet.Add("INPUT_ATTACK2 	257 	LEFT MOUSE BUTTON");
            ControlerSet.Add("INPUT_RAPPEL_JUMP 	258 	");
            ControlerSet.Add("INPUT_RAPPEL_LONG_JUMP 	259 	");
            ControlerSet.Add("INPUT_RAPPEL_SMASH_WINDOW 	260 	");
            ControlerSet.Add("INPUT_PREV_WEAPON 	261 	SCROLLWHEEL UP");
            ControlerSet.Add("INPUT_NEXT_WEAPON 	262 	SCROLLWHEEL DOWN");
            ControlerSet.Add("INPUT_MELEE_ATTACK1 	263 	R");
            ControlerSet.Add("INPUT_MELEE_ATTACK2 	264 	Q");
            ControlerSet.Add("INPUT_WHISTLE 	265 	");
            ControlerSet.Add("INPUT_MOVE_LEFT 	266 	D");
            ControlerSet.Add("INPUT_MOVE_RIGHT 	267 	D");
            ControlerSet.Add("INPUT_MOVE_UP 	268 	S");
            ControlerSet.Add("INPUT_MOVE_DOWN 	269 	S");
            ControlerSet.Add("INPUT_LOOK_LEFT 	270 	MOUSE RIGHT");
            ControlerSet.Add("INPUT_LOOK_RIGHT 	271 	MOUSE RIGHT");
            ControlerSet.Add("INPUT_LOOK_UP 	272 	MOUSE DOWN");
            ControlerSet.Add("INPUT_LOOK_DOWN 	273 	MOUSE DOWN");
            ControlerSet.Add("INPUT_SNIPER_ZOOM_IN 	274 	[");
            ControlerSet.Add("INPUT_SNIPER_ZOOM_OUT 	275 	[");
            ControlerSet.Add("INPUT_SNIPER_ZOOM_IN_ALTERNATE 	276 	[");
            ControlerSet.Add("INPUT_SNIPER_ZOOM_OUT_ALTERNATE 	277 	[");
            ControlerSet.Add("INPUT_VEH_MOVE_LEFT 	278 	D");
            ControlerSet.Add("INPUT_VEH_MOVE_RIGHT 	279 	D");
            ControlerSet.Add("INPUT_VEH_MOVE_UP 	280 	LEFT CTRL");
            ControlerSet.Add("INPUT_VEH_MOVE_DOWN 	281 	LEFT CTRL");
            ControlerSet.Add("INPUT_VEH_GUN_LEFT 	282 	MOUSE RIGHT");
            ControlerSet.Add("INPUT_VEH_GUN_RIGHT 	283 	MOUSE RIGHT");
            ControlerSet.Add("INPUT_VEH_GUN_UP 	284 	MOUSE RIGHT");
            ControlerSet.Add("INPUT_VEH_GUN_DOWN 	285 	MOUSE RIGHT");
            ControlerSet.Add("INPUT_VEH_LOOK_LEFT 	286 	MOUSE RIGHT");
            ControlerSet.Add("INPUT_VEH_LOOK_RIGHT 	287 	MOUSE RIGHT");
            ControlerSet.Add("INPUT_REPLAY_START_STOP_RECORDING 	288 	F1");
            ControlerSet.Add("INPUT_REPLAY_START_STOP_RECORDING_SECONDARY 	289 	F2");
            ControlerSet.Add("INPUT_SCALED_LOOK_LR 	290 	MOUSE RIGHT");
            ControlerSet.Add("INPUT_SCALED_LOOK_UD 	291 	MOUSE DOWN");
            ControlerSet.Add("INPUT_SCALED_LOOK_UP_ONLY 	292 	");
            ControlerSet.Add("INPUT_SCALED_LOOK_DOWN_ONLY 	293 	");
            ControlerSet.Add("INPUT_SCALED_LOOK_LEFT_ONLY 	294 	");
            ControlerSet.Add("INPUT_SCALED_LOOK_RIGHT_ONLY 	295 	");
            ControlerSet.Add("INPUT_REPLAY_MARKER_DELETE 	296 	DELETE");
            ControlerSet.Add("INPUT_REPLAY_CLIP_DELETE 	297 	DELETE");
            ControlerSet.Add("INPUT_REPLAY_PAUSE 	298 	SPACEBAR");
            ControlerSet.Add("INPUT_REPLAY_REWIND 	299 	ARROW DOWN");
            ControlerSet.Add("INPUT_REPLAY_FFWD 	300 	ARROW UP");
            ControlerSet.Add("INPUT_REPLAY_NEWMARKER 	301 	M");
            ControlerSet.Add("INPUT_REPLAY_RECORD 	302 	S");
            ControlerSet.Add("INPUT_REPLAY_SCREENSHOT 	303 	U");
            ControlerSet.Add("INPUT_REPLAY_HIDEHUD 	304 	H");
            ControlerSet.Add("INPUT_REPLAY_STARTPOINT 	305 	B");
            ControlerSet.Add("INPUT_REPLAY_ENDPOINT 	306 	N");
            ControlerSet.Add("INPUT_REPLAY_ADVANCE 	307 	ARROW RIGHT");
            ControlerSet.Add("INPUT_REPLAY_BACK 	308 	ARROW LEFT");
            ControlerSet.Add("INPUT_REPLAY_TOOLS 	309 	T");
            ControlerSet.Add("INPUT_REPLAY_RESTART 	310 	R");
            ControlerSet.Add("INPUT_REPLAY_SHOWHOTKEY 	311 	K");
            ControlerSet.Add("INPUT_REPLAY_CYCLEMARKERLEFT 	312 	[");
            ControlerSet.Add("INPUT_REPLAY_CYCLEMARKERRIGHT 	313 	]");
            ControlerSet.Add("INPUT_REPLAY_FOVINCREASE 	314 	NUMPAD+");
            ControlerSet.Add("INPUT_REPLAY_FOVDECREASE 	315 	NUMPAD-");
            ControlerSet.Add("INPUT_REPLAY_CAMERAUP 	316 	PAGE UP");
            ControlerSet.Add("INPUT_REPLAY_CAMERADOWN 	317 	PAGE DOWN");
            ControlerSet.Add("INPUT_REPLAY_SAVE 	318 	F5");
            ControlerSet.Add("INPUT_REPLAY_TOGGLETIME 	319 	C");
            ControlerSet.Add("INPUT_REPLAY_TOGGLETIPS 	320 	V");
            ControlerSet.Add("INPUT_REPLAY_PREVIEW 	321 	SPACEBAR");
            ControlerSet.Add("INPUT_REPLAY_TOGGLE_TIMELINE 	322 	ESC");
            ControlerSet.Add("INPUT_REPLAY_TIMELINE_PICKUP_CLIP 	323 	X");
            ControlerSet.Add("INPUT_REPLAY_TIMELINE_DUPLICATE_CLIP 	324 	C");
            ControlerSet.Add("INPUT_REPLAY_TIMELINE_PLACE_CLIP 	325 	V");
            ControlerSet.Add("INPUT_REPLAY_CTRL 	326 	LEFT CTRL");
            ControlerSet.Add("INPUT_REPLAY_TIMELINE_SAVE 	327 	F5");
            ControlerSet.Add("INPUT_REPLAY_PREVIEW_AUDIO 	328 	SPACEBAR");
            ControlerSet.Add("INPUT_VEH_DRIVE_LOOK 	329 	LEFT MOUSE BUTTON");
            ControlerSet.Add("INPUT_VEH_DRIVE_LOOK2 	330 	RIGHT MOUSE BUTTON");
            ControlerSet.Add("INPUT_VEH_FLY_ATTACK2 	331 	RIGHT MOUSE BUTTON");
            ControlerSet.Add("INPUT_RADIO_WHEEL_UD 	332 	MOUSE DOWN");
            ControlerSet.Add("INPUT_RADIO_WHEEL_LR 	333 	MOUSE RIGHT");
            ControlerSet.Add("INPUT_VEH_SLOWMO_UD 	334 	SCROLLWHEEL DOWN");
            ControlerSet.Add("INPUT_VEH_SLOWMO_UP_ONLY 	335 	SCROLLWHEEL UP");
            ControlerSet.Add("INPUT_VEH_SLOWMO_DOWN_ONLY 	336 	SCROLLWHEEL DOWN");
            ControlerSet.Add("INPUT_VEH_HYDRAULICS_CONTROL_TOGGLE 	337 	X");
            ControlerSet.Add("INPUT_VEH_HYDRAULICS_CONTROL_LEFT 	338 	A");
            ControlerSet.Add("INPUT_VEH_HYDRAULICS_CONTROL_RIGHT 	339 	D");
            ControlerSet.Add("INPUT_VEH_HYDRAULICS_CONTROL_UP 	340 	LEFT SHIFT");
            ControlerSet.Add("INPUT_VEH_HYDRAULICS_CONTROL_DOWN 	341 	LEFT CTRL");
            ControlerSet.Add("INPUT_VEH_HYDRAULICS_CONTROL_LR 	342 	D");
            ControlerSet.Add("INPUT_VEH_HYDRAULICS_CONTROL_UD 	343 	LEFT CTRL");
            ControlerSet.Add("INPUT_SWITCH_VISOR 	344 	F11");
            ControlerSet.Add("INPUT_VEH_MELEE_HOLD 	345 	X");
            ControlerSet.Add("INPUT_VEH_MELEE_LEFT 	346 	LEFT MOUSE BUTTON");
            ControlerSet.Add("INPUT_VEH_MELEE_RIGHT 	347 	RIGHT MOUSE BUTTON");
            ControlerSet.Add("INPUT_MAP_POI 	348 	SCROLLWHEEL BUTTON (PRESS)");
            ControlerSet.Add("INPUT_REPLAY_SNAPMATIC_PHOTO 	349 	TAB");
            ControlerSet.Add("INPUT_VEH_CAR_JUMP 	350 	E");
            ControlerSet.Add("INPUT_VEH_ROCKET_BOOST 	351 	E");
            ControlerSet.Add("INPUT_VEH_FLY_BOOST 	352 	LEFT SHIFT");
            ControlerSet.Add("INPUT_VEH_PARACHUTE 	353 	SPACEBAR");
            ControlerSet.Add("INPUT_VEH_BIKE_WINGS 	354 	X");
            ControlerSet.Add("INPUT_VEH_FLY_BOMB_BAY 	355 	E");
            ControlerSet.Add("INPUT_VEH_FLY_COUNTER 	356 	E");
            ControlerSet.Add("INPUT_VEH_TRANSFORM 	357 	X");
            ControlerSet.Add("INPUT_QUAD_LOCO_REVERSE 	358 	");
            ControlerSet.Add("INPUT_RESPAWN_FASTER 	359 	");
            ControlerSet.Add("INPUT_HUDMARKER_SELECT 	360 ");
        }
        private void LoadUpForms(bool bDefault)
        {
            if (bDefault)
                MyIniSettings = SetBuild();

            decimal Num01 = MyIniSettings.Aggression;
            decimal Num02 = MyIniSettings.MaxPlayers;
            decimal Num03 = GetSeconds(false, MyIniSettings.MinWait);
            decimal Num04 = GetSeconds(false, MyIniSettings.MaxWait);
            decimal Num05 = GetSeconds(true, MyIniSettings.MinSession);
            decimal Num06 = GetSeconds(true, MyIniSettings.MaxSession);
            decimal Num07 = MyIniSettings.MinAccuracy;
            decimal Num08 = MyIniSettings.MaxAccuracy;
            decimal Num09 = MyIniSettings.GetlayList;
            decimal Num10 = MyIniSettings.ClearPlayList;
            decimal Num11 = MyIniSettings.DisableMod;

            numericUpDown1.Value = Num01;
            numericUpDown2.Value = Num02;
            numericUpDown3.Value = Num03;
            numericUpDown4.Value = Num04;
            numericUpDown5.Value = Num05;
            numericUpDown6.Value = Num06;
            numericUpDown7.Value = Num07;
            numericUpDown8.Value = Num08;
            checkBox1.Checked = MyIniSettings.SpaceWeaps;
            numericUpDown9.Value = Num09;
            numericUpDown10.Value = Num10;
            numericUpDown11.Value = Num11;
            checkBox2.Checked = MyIniSettings.Debugger;
            textBox1.Text = ControlerSet[MyIniSettings.GetlayList];
            textBox2.Text = ControlerSet[MyIniSettings.ClearPlayList];
            textBox3.Text = ControlerSet[MyIniSettings.DisableMod];

            if (bDefault)
                SaveAllSetttings();
        }       
        private void LoadIniSettings()
        {
            if (File.Exists(sPath))
            {
                MyIni = new IniFile();
                MyIniSettings.Aggression = ReadMyInt(MyIni.Read("Aggression", "Settings"));
                MyIniSettings.MaxPlayers = ReadMyInt(MyIni.Read("MaxPlayers", "Settings"));
                MyIniSettings.MinWait = ReadMyInt(MyIni.Read("MinWait", "Settings"));
                MyIniSettings.MaxWait = ReadMyInt(MyIni.Read("MaxWait", "Settings"));
                MyIniSettings.MinSession = ReadMyInt(MyIni.Read("MinSession", "Settings"));
                MyIniSettings.MaxSession = ReadMyInt(MyIni.Read("MaxSession", "Settings"));
                MyIniSettings.MinAccuracy = ReadMyInt(MyIni.Read("MinAccuracy", "Settings"));
                MyIniSettings.MaxAccuracy = ReadMyInt(MyIni.Read("MaxAccuracy", "Settings"));
                MyIniSettings.SpaceWeaps = ReadMyBool(MyIni.Read("SpaceWeaps", "Settings"));
                MyIniSettings.Debugger = ReadMyBool(MyIni.Read("Debug", "Settings"));
                MyIniSettings.GetlayList = ReadMyInt(MyIni.Read("Players", "Controls"));
                MyIniSettings.ClearPlayList = ReadMyInt(MyIni.Read("ClearPlayers", "Controls"));
                MyIniSettings.DisableMod = ReadMyInt(MyIni.Read("DisableMod", "Controls"));
                LoadUpForms(false);
            }
        }
        private void SaveAllSetttings()
        {
            MyIni = new IniFile();
            MyIni.Write("Aggression", "" + MyIniSettings.Aggression + "", "Settings");
            MyIni.Write("MaxPlayers", "" + MyIniSettings.MaxPlayers + "", "Settings");
            MyIni.Write("MinWait", "" + MyIniSettings.MinWait + "", "Settings");
            MyIni.Write("MaxWait", "" + MyIniSettings.MaxWait + "", "Settings");
            MyIni.Write("MinSession", "" + MyIniSettings.MinSession + "", "Settings");
            MyIni.Write("MaxSession", "" + MyIniSettings.MaxSession + "", "Settings");
            MyIni.Write("MinAccuracy", "" + numericUpDown7.Value + "", "Settings");
            MyIni.Write("MaxAccuracy", "" + numericUpDown8.Value + "", "Settings");
            MyIni.Write("SpaceWeaps", "" + checkBox1.Checked + "", "Settings");
            MyIni.Write("Debug", "" + checkBox2.Checked + "", "Settings");
            MyIni.Write("Players", "" + numericUpDown9.Value + "", "Controls");
            MyIni.Write("ClearPlayers", "" + numericUpDown10.Value + "", "Controls");
            MyIni.Write("DisableMod", "" + numericUpDown11.Value + "", "Controls");
        }
        public class IniSettings
        {
            public int Aggression { get; set; }
            public int MaxPlayers { get; set; }
            public int MinWait { get; set; }
            public int MaxWait { get; set; }
            public int MinSession { get; set; }
            public int MaxSession { get; set; }
            public int MinAccuracy { get; set; }
            public int MaxAccuracy { get; set; }
            public bool SpaceWeaps { get; set; }
            public int GetlayList { get; set; }
            public int ClearPlayList { get; set; }
            public int DisableMod { get; set; }
            public bool Debugger { get; set; }
        }
        public IniSettings SetBuild()
        {
            IniSettings NewSet = new IniSettings
            {
                Aggression = 5,
                MaxPlayers = 29,
                MinWait = 15000,
                MaxWait = 45000,
                MinSession = 300000,
                MaxSession = 360000,
                MinAccuracy = 25,
                MaxAccuracy = 75,
                GetlayList = 19,
                ClearPlayList = 131,
                DisableMod = 73,
                SpaceWeaps = false,
                Debugger = false
        };

            return NewSet;
        }
        private void button2_Click(object sender, EventArgs e)
        {
            LoadUpForms(true);
        }
    }
}
