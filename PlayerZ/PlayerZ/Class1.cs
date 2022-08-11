using GTA;
using GTA.Native;
using PlayerZero;
using System;

namespace OnlinePlayerz
{
    public class Main : Script
    {
        private bool bStartMod = true;

        public Main()
        {
            DataStore.LoadSettings();
            ClearUp.RedundentAssets();
            Tick += OnTick;
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

            ReturnValues.SetBTimer(30000, -1);
            ReturnValues.SetBTimer(ReturnValues.RandInt(DataStore.MySettings.iMinWait, DataStore.MySettings.iMaxWait), -1);
        }
        private void OnTick(object sender, EventArgs e)
        {
            if (bStartMod)
            {
                if (!Game.IsLoading)
                {
                    bStartMod = false;
                    LoadUp();
                    DataStore.bLoadUp = true;
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
                if (ReturnValues.ButtonDown(DataStore.MySettings.iGetlayList, false) && !DataStore.bPlayerList && !DataStore.bPlayerPiggyBack)
                    ScaleDisp.PlayScales();
                else
                {
                    if (DataStore.bPlayerPiggyBack)
                    {
                        if (Game.Player.Character.IsAttached())
                        {
                            ScaleDisp.TopLeftUI("Press" + DataStore.ControlSym[DataStore.MySettings.iClearPlayList] + "to dismount player");
                            if (ReturnValues.WhileButtonDown(DataStore.MySettings.iClearPlayList, true) || Game.Player.Character.GetEntityAttachedTo().IsDead)
                            {
                                Game.Player.Character.Detach();
                                Game.Player.Character.Task.ClearAllImmediately();
                                DataStore.bPlayerPiggyBack = false;
                            }
                        }
                        else
                            DataStore.bPlayerPiggyBack = false;
                    }
                    PedActions.PlayerZerosAI();
                }
            }
        }
    }
}