using GTA;
using GTA.Math;
using GTA.Native;
using PlayerZero.Classes;
using iFruitAddon2;
using System;
using System.Collections.Generic;

namespace PlayerZero
{
    public class OnlinePlayerz : Script
    {
        private static bool bStartMod = true;
        private static bool bAddContacts = false;
        private static int iDelay = 0;
        private static int iPick = 0;
        private static List<PlayerBrain> playerz = new List<PlayerBrain>();
        private static CustomiFruit iFruity = new CustomiFruit();

        public OnlinePlayerz()
        {
            DataStore.LoadSettings();
            Tick += OnTick;
            //KeyDown += OnKeyDown;
            Interval = 1;
        }
        private void LoadUp()
        {
            LoggerLight.GetLogging("LoadUp");

            Function.Call(Hash.SET_RELATIONSHIP_BETWEEN_GROUPS, 0, DataStore.GP_Player, DataStore.Gp_Follow);
            Function.Call(Hash.SET_RELATIONSHIP_BETWEEN_GROUPS, 0, DataStore.Gp_Follow, DataStore.GP_Player);

            Function.Call(Hash.SET_RELATIONSHIP_BETWEEN_GROUPS, 2, DataStore.Gp_Follow, DataStore.Gp_Friend);
            Function.Call(Hash.SET_RELATIONSHIP_BETWEEN_GROUPS, 2, DataStore.Gp_Friend, DataStore.Gp_Follow);

            Function.Call(Hash.SET_RELATIONSHIP_BETWEEN_GROUPS, 5, DataStore.GP_Attack, DataStore.Gp_Follow);
            Function.Call(Hash.SET_RELATIONSHIP_BETWEEN_GROUPS, 5, DataStore.Gp_Follow, DataStore.GP_Attack);

            Function.Call(Hash.SET_RELATIONSHIP_BETWEEN_GROUPS, 5, DataStore.GP_Mental, DataStore.Gp_Follow);
            Function.Call(Hash.SET_RELATIONSHIP_BETWEEN_GROUPS, 5, DataStore.Gp_Follow, DataStore.GP_Mental);

            Function.Call(Hash.SET_RELATIONSHIP_BETWEEN_GROUPS, 2, DataStore.GP_Player, DataStore.Gp_Friend);
            Function.Call(Hash.SET_RELATIONSHIP_BETWEEN_GROUPS, 2, DataStore.Gp_Friend, DataStore.GP_Player);

            Function.Call(Hash.SET_RELATIONSHIP_BETWEEN_GROUPS, 5, DataStore.GP_Attack, DataStore.Gp_Friend);
            Function.Call(Hash.SET_RELATIONSHIP_BETWEEN_GROUPS, 5, DataStore.Gp_Friend, DataStore.GP_Attack);

            Function.Call(Hash.SET_RELATIONSHIP_BETWEEN_GROUPS, 5, DataStore.GP_Player, DataStore.GP_Attack);
            Function.Call(Hash.SET_RELATIONSHIP_BETWEEN_GROUPS, 5, DataStore.GP_Attack, DataStore.GP_Player);

            Function.Call(Hash.SET_RELATIONSHIP_BETWEEN_GROUPS, 5, DataStore.GP_Mental, DataStore.Gp_Friend);
            Function.Call(Hash.SET_RELATIONSHIP_BETWEEN_GROUPS, 5, DataStore.Gp_Friend, DataStore.GP_Mental);

            Function.Call(Hash.SET_RELATIONSHIP_BETWEEN_GROUPS, 5, DataStore.GP_Attack, DataStore.GP_Mental);
            Function.Call(Hash.SET_RELATIONSHIP_BETWEEN_GROUPS, 5, DataStore.GP_Mental, DataStore.GP_Attack);

            Function.Call(Hash.SET_RELATIONSHIP_BETWEEN_GROUPS, 5, DataStore.GP_Player, DataStore.GP_Mental);
            Function.Call(Hash.SET_RELATIONSHIP_BETWEEN_GROUPS, 5, DataStore.GP_Mental, DataStore.GP_Player);

            Function.Call(Hash.SET_RELATIONSHIP_BETWEEN_GROUPS, 5, DataStore.GP_Mental, DataStore.GP_Mental);

            Function.Call(Hash.SET_PED_AS_GROUP_LEADER, Game.Player.Character.Handle, DataStore.iFollowMe);
            Function.Call(Hash.SET_GROUP_FORMATION, DataStore.iFollowMe, 3);

            AddContacts();
        }
        private static void AddiPlayerAns(iFruitContact contact)
        {
            LoggerLight.GetLogging("AddiPlayerAns");
            ShutThatPhone("", true);
        }
        private static void GetMyContact(iFruitContact contact)
        {
            LoggerLight.GetLogging("GetMyContact");
            string sContact = contact.Name;
            ShutThatPhone(sContact, false);
        }
        private static void AddContacts()
        {
            LoggerLight.GetLogging("AddContacts");

            iFruity.CenterButtonColor = System.Drawing.Color.Orange;
            iFruity.LeftButtonColor = System.Drawing.Color.LimeGreen;
            iFruity.RightButtonColor = System.Drawing.Color.Purple;
            iFruity.CenterButtonIcon = SoftKeyIcon.Fire;
            iFruity.LeftButtonIcon = SoftKeyIcon.Police;
            iFruity.RightButtonIcon = SoftKeyIcon.Call;

            iFruitContact AddiPlayer = new iFruitContact("Add Friends");
            AddiPlayer.Answered += AddiPlayerAns;                   // Linking the Answered event with our function
            AddiPlayer.DialTimeout = 1000;                       // Delay before answering
            AddiPlayer.Active = true;                            // true = the contact is available and will answer the phone
            AddiPlayer.Icon = ContactIcon.Multiplayer;              // Contact's icon
            iFruity.Contacts.Add(AddiPlayer);                     // Add the contact to the phone
            ScaleDisp.BottomLeft("New contact added...");

            if (DataStore.PlayContList.PedBrain.Count > 0)
            {
                for (int i = 0; i < DataStore.PlayContList.PedBrain.Count; i++)
                    NewContact(DataStore.PlayContList.PedBrain[i]);
            }
        }
        private static void NewContact(MakeContact thisContact)
        {
            LoggerLight.GetLogging("NewContact");

            iFruitContact MySets = new iFruitContact(thisContact.MyName);
            MySets.Answered += GetMyContact;
            MySets.DialTimeout = 1000;
            MySets.Active = true;
            MySets.Icon = ContactIcon.Blank;
            iFruity.Contacts.Add(MySets);
            ScaleDisp.BottomLeft(thisContact.MyName + " added...");
        }
        private static void ShutThatPhone(string sCont,bool bAdd)
        {
            LoggerLight.GetLogging("ShutThatPhone, sCont == " + sCont);

            iFruity.Close();
            while (Function.Call<bool>(Hash.IS_PED_RUNNING_MOBILE_PHONE_TASK, Game.Player.Character))
                Script.Wait(1);

            Script.Wait(1000);

            if (bAdd)
                SaveMyFriend();
            else
            {
                for (int i = 0; i < DataStore.PlayContList.PedBrain.Count; i++)
                {
                    if (sCont == DataStore.PlayContList.PedBrain[i].MyName)
                    {
                        LoadMyFriend(i);
                        break;
                    }
                }
            }
        }
        private static void SaveMyFriend()
        {
            LoggerLight.GetLogging("SaveMyFriend");
            playerz.Clear();

            for (int i = 0; i < DataStore.PedList.Count; i++)
            {
                if (DataStore.PedList[i].Follower)
                    playerz.Add(DataStore.PedList[i]);
            }

            for (int i = 0; i < DataStore.PlayContList.PedBrain.Count; i++)
            {
                for (int ii = 0; ii < playerz.Count; ii++)
                {
                    if (playerz[ii].MyIdentity == DataStore.PlayContList.PedBrain[i].MyIdentity)
                        playerz.RemoveAt(ii);
                }
            }
            iPick = 0;
            bAddContacts = true;
        }
        private static void AddNewToList(PlayerBrain thisContact)
        {
            LoggerLight.GetLogging("AddNewToList + " + thisContact.MyName);
            MakeContact newContact = new MakeContact
            {
                BlipColour = thisContact.BlipColour,

                Level = thisContact.Level,

                PrefredVehicle = thisContact.PrefredVehicle,

                IsPlane = thisContact.IsPlane,
                IsHeli = thisContact.IsHeli,
                MyName = thisContact.MyName,
                MyIdentity = thisContact.MyIdentity,
                PFMySetting = thisContact.PFMySetting
            };

            NewContact(newContact);
            DataStore.PlayContList.PedBrain.Insert(0, newContact);
            XmlReadWrite.SaveMyContacts(DataStore.PlayContList, DataStore.sSaveCont);
        }
        private static void LoadMyFriend(int iFriend)
        {
            LoggerLight.GetLogging("LoadMyFriend, " + DataStore.PlayContList.PedBrain[iFriend].MyName);

            if (PlayerAI.ReteaveBrain(DataStore.PlayContList.PedBrain[iFriend].MyIdentity) == -1)
            {
                if (DataStore.iFollow < 7 && !DataStore.bDisabled)
                {
                    MakeContact MK = DataStore.PlayContList.PedBrain[iFriend];
                    PlayerBrain NewBran = new PlayerBrain();

                    NewBran.BlipColour = MK.BlipColour;
                    NewBran.TimeOn = Game.GameTime + DataStore.MySettings.MaxSession;
                    NewBran.PrefredVehicle = MK.PrefredVehicle;
                    NewBran.Level = MK.Level;
                    NewBran.Follower = true;
                    NewBran.MyName = MK.MyName;
                    NewBran.MyIdentity = MK.MyIdentity;
                    NewBran.PFMySetting = MK.PFMySetting;

                    if (NewBran.PrefredVehicle > 0)
                    {
                        NewBran.ApprochPlayer = true;
                        NewBran.WanBeFriends = false;
                        NewBran.Horny = false;
                        FindVeh MyFinda = new FindVeh(35.00f, 220.00f, true, false, NewBran);
                        FindStuff.MakeCarz.Add(MyFinda);
                    }
                    else
                    {
                        FindPed MyFinda = new FindPed(35.00f, 220.00f, NewBran);
                        FindStuff.MakeFrenz.Add(MyFinda);
                    }
                }
                else
                    ScaleDisp.BottomLeft("I can't conect to this session.");

            }
            else
                ScaleDisp.BottomLeft("I'm already in this session.");
        }
        public static void RemoveMyFriend(string sId)
        {
            int iAm = PlayerAI.ReteveContact(sId);
            if (iAm != -1)
            {
                for (int i = 0; i < iFruity.Contacts.Count; i++)
                {
                    if (DataStore.PlayContList.PedBrain[iAm].MyName == iFruity.Contacts[i].Name)
                    {
                        iFruity.Contacts.RemoveAt(i);
                        break;
                    }
                }
                DataStore.PlayContList.PedBrain.RemoveAt(iAm);
                XmlReadWrite.SaveMyContacts(DataStore.PlayContList, DataStore.sSaveCont);
            }
        }
        private void OnTick(object sender, EventArgs e)
        {
            if (bStartMod)
            {
                if (!Game.IsLoading)
                {
                    bStartMod = false;
                    LoadUp();
                }
            }
            else if (bAddContacts)
            {
                if (playerz.Count == 0)
                    bAddContacts = false;
                else
                {
                    ScaleDisp.TopLeftUI("Press ~INPUT_SPRINT~ to add ~r~" + playerz[iPick].MyName + " ~w~ ~INPUT_TALK~ too contact list. ~INPUT_RELOAD~ to Exit.");
                    if (iDelay < Game.GameTime)
                    {
                        if (ReturnValues.ButtonDown(21, true))
                        {
                            iDelay = Game.GameTime + 600;
                            AddNewToList(playerz[iPick]);
                            playerz.RemoveAt(iPick);
                            iPick = 0;
                        }
                        else if (ReturnValues.ButtonDown(47, true))
                        {
                            iDelay = Game.GameTime + 600;
                            iPick--;
                            if (iPick < 0)
                                iPick = playerz.Count - 1;
                        }
                        else if (ReturnValues.ButtonDown(46, true))
                        {
                            iDelay = Game.GameTime + 600;
                            iPick++;
                            if (iPick == playerz.Count)
                                iPick = 0;
                        }
                        else if (ReturnValues.ButtonDown(45, true))
                        {
                            playerz.Clear();
                            bAddContacts = false;
                            iPick = 0;
                        }
                    }
                }
            }
            else if (DataStore.bMenuOpen)
            {
                if (HackazMenu.PzPool.IsAnyMenuOpen())
                    HackazMenu.PzPool.ProcessMenus();
                else
                {
                    IniSettings.WriteSettingsIni();
                    DataStore.bMenuOpen = false;
                }
            }
            else
            {
                iFruity.Update();

                if (!DataStore.bScan)
                {
                    DataStore.bScan = true;
                    PlayerAI.PlayerZerosAI();
                }

                if (DataStore.bPlayerPiggyBack)
                {
                    if (Game.Player.Character.IsAttached())
                    {
                        ScaleDisp.TopLeftUI("Press" + DataStore.ControlSym[23] + "to dismount player");
                        if (ReturnValues.WhileButtonDown(23, true) || Game.Player.Character.GetEntityAttachedTo().IsDead)
                        {
                            Game.Player.Character.Detach();
                            Game.Player.Character.Task.ClearAllImmediately();
                            DataStore.bPlayerPiggyBack = false;
                        }
                    }
                    else
                        DataStore.bPlayerPiggyBack = false;
                }

                if (DataStore.iMoneyDrops != -1)
                {
                    if (DataStore.iMoneyDrops < Game.GameTime)
                        PedActions.RemoveMoneyDrop(DataStore.sMoneyPicker);
                    else
                        PedActions.MoneyDrops(DataStore.sMoneyPicker);
                }
            }
        }
        //private void OnKeyDown(object sender, KeyEventArgs e)
        //{
            //if (e.KeyCode == Keys.I)
        //}
    }
}