using System;

using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CitizenFX.Core;
using static CitizenFX.Core.Native.API;


namespace CoffinDanceClient
{
    public class Class1 : BaseScript
    {
        public int xPedMainLeader;
        public int xPed1;
        public int xPed2;
        public int xPed3;
        public int xPed4;
        public int xPed5;
        public int xPed6;
        public int xObj3;

        public Class1()
        {
            EventHandlers["onClientResourceStart"] += new Action<string>(OnClientResourceStart);
            EventHandlers["gameEventTriggered"] += new Action<string, List<dynamic>>(OnGameEventTriggered);
        }
        private void OnGameEventTriggered(string name, List<dynamic> args)
        {
            Debug.WriteLine($"game event {name} ({String.Join(", ", args.ToArray())})");

            Wait(100);
            if (true == Game.PlayerPed.IsDead)
            {
                Debug.WriteLine($"You Died");
                AnimateGameplayCamZoom(0.0f, 30f);
                SetFollowPedCamViewMode(2);
                int x = GetRoomKeyFromGameplayCam();
                PointCamAtEntity(x, xPedMainLeader, 0.0f, 0.0f, 0.0f, true);

                DoDo();
            }
        }

        public void OnClientResourceStart(string resourceName)
        {

            if (GetCurrentResourceName() != resourceName) return;
            RegisterCommand("p1", new Action<int, List<object>, string>(async (source, args, raw) =>
            {
                DoDo();
            }
            ), false);

            RegisterCommand("p1reset", new Action<int, List<object>, string>(async (source, args, raw) =>
            {
                DeleteEntity(ref xPedMainLeader);
                DeleteEntity(ref xPed1);
                DeleteEntity(ref xPed2);
                DeleteEntity(ref xPed3);
                DeleteEntity(ref xPed4);
                DeleteEntity(ref xPed5);
                DeleteEntity(ref xPed6);
                DeleteEntity(ref xObj3);
                SetPedAsNoLongerNeeded(ref xPedMainLeader);
                SetPedAsNoLongerNeeded(ref xPed1);
                SetPedAsNoLongerNeeded(ref xPed2);
                SetPedAsNoLongerNeeded(ref xPed3);
                SetPedAsNoLongerNeeded(ref xPed4);
                SetPedAsNoLongerNeeded(ref xPed5);
                SetPedAsNoLongerNeeded(ref xPed6);
                SetObjectAsNoLongerNeeded(ref xObj3);
            }
            ), false);



        }
        public void ResetAllBack()
        {


            DeleteEntity(ref xPedMainLeader);
            DeleteEntity(ref xPed1);
            DeleteEntity(ref xPed2);
            DeleteEntity(ref xPed3);
            DeleteEntity(ref xPed4);
            DeleteEntity(ref xPed5);
            DeleteEntity(ref xPed6);
            DeleteEntity(ref xObj3);
            SetPedAsNoLongerNeeded(ref xPedMainLeader);
            SetPedAsNoLongerNeeded(ref xPed1);
            SetPedAsNoLongerNeeded(ref xPed2);
            SetPedAsNoLongerNeeded(ref xPed3);
            SetPedAsNoLongerNeeded(ref xPed4);
            SetPedAsNoLongerNeeded(ref xPed5);
            SetPedAsNoLongerNeeded(ref xPed6);
            SetObjectAsNoLongerNeeded(ref xObj3);
        }

        public void DoDo()
        {
            ResetAllBack();
            Vector3 playerPosse = Game.PlayerPed.Position;
            Vector3 pedPosse = playerPosse;
            pedPosse.Y += 2.0f;

            string str = "CoffinDance";

            TriggerServerEvent("InteractSound_SV:PlayOnAll", str, 0.1);

            Vector3 coffinPosse = pedPosse;
            //coffinPosse.Z += 0.5f;
            coffinPosse.X += 0.45f;

            uint pedHash = (uint)GetHashKey("s_m_m_strpreach_01");//469792763

            RequestModel(pedHash);
            if (HasModelLoaded(pedHash) != true)
            {
                TriggerEvent("chat:addMessage", new
                {

                    cholor = new[] { 255, 0, 0 },
                    args = new[] { "[Not Valid]", $"Not existing {pedHash}" }
                });
            }


            var danceBase = "anim@amb@nightclub@mini@dance@dance_solo@male@var_b@";//"anim@amb@nightclub@dancers@crowddance_single_props_transitions@from_hi_intensity";//"anim@amb@nightclub@dancers@black_madonna_entourage@"; //"anim@amb@nightclub@dancers@crowddance_single_props@hi_intensity";   //"special_ped@mountain_dancer@monologue_1@monologue_1a";
            var danceName = "high_center";//"trans_crowd_prop_hi_to_li_09_v1_male^1";//"li_dance_facedj_15_v2_male^2"; //"hi_dance_prop_09_v1_male^1";  //"mtn_dnc_if_you_want_to_get_to_heaven";
            RequestAnimDict(danceBase);

            if (HasAnimDictLoaded(danceBase) != true)
            {
                TriggerEvent("chat:addMessage", new
                {
                    cholor = new[] { 255, 0, 0 },
                    args = new[] { "[Not Valid]", $"Not existing {danceBase} DanceBase" }
                });
            }

            xPedMainLeader = CreatePed(4, pedHash, pedPosse.X + 0.425f, pedPosse.Y + 1f, pedPosse.Z, 0.0f, true, false);
            Wait(200);

            SetEntityVisible(xPedMainLeader, false, false);

            xPed1 = CreatePed(4, pedHash, pedPosse.X, pedPosse.Y, pedPosse.Z, 0.0f, true, false);
            Wait(200);
            xPed2 = CreatePed(4, pedHash, pedPosse.X + 0.85f, pedPosse.Y, pedPosse.Z, 0.0f, true, false);
            Wait(200);
            xPed3 = CreatePed(4, pedHash, pedPosse.X, pedPosse.Y - 0.6f, pedPosse.Z, 0.0f, true, false);
            Wait(200);
            xPed4 = CreatePed(4, pedHash, pedPosse.X + 0.85f, pedPosse.Y - 0.6f, pedPosse.Z, 0.0f, true, false);
            Wait(200);
            xPed5 = CreatePed(4, pedHash, pedPosse.X, pedPosse.Y - 1.2f, pedPosse.Z, 0.0f, true, false);
            Wait(200);
            xPed6 = CreatePed(4, pedHash, pedPosse.X + 0.85f, pedPosse.Y - 1.2f, pedPosse.Z, 0.0f, true, false);

            int xObj3HashKey = (int)GetHashKey("prop_coffin_01");

            xObj3 = CreateObject(xObj3HashKey, coffinPosse.X, coffinPosse.Y, coffinPosse.Z + 0.5f, true, true, false);


            int pedBoneId = GetPedBoneIndex(xPed1, 0x54AF);


            AttachEntityToEntity(xObj3, xPedMainLeader, 28422, 0.0f, -1.7f, 0.85f, 0.0f, 0.0f, 0.0f, true, true, false, true, 2, true);

            // Old one ! AttachEntityToEntity(xObj3, xPedMainLeader, 28422, 0.45f, -0.65f, 0.65f, 0.0f, 0.0f, 0.0f,true,true,false,true,2,true);

            AttachEntityToEntity(xPed1, xObj3, 28422, -0.55f, 1.0f, -0.81f, 0.0f, 0.0f, 0.0f, true, true, false, true, 2, true);
            AttachEntityToEntity(xPed2, xObj3, 28422, 0.55f, 1.0f, -0.81f, 0.0f, 0.0f, 0.0f, true, true, false, true, 2, true);
            AttachEntityToEntity(xPed3, xObj3, 28422, -0.55f, 0.0f, -0.81f, 0.0f, 0.0f, 0.0f, true, true, false, true, 2, true);
            AttachEntityToEntity(xPed4, xObj3, 28422, 0.55f, 0.0f, -0.81f, 0.0f, 0.0f, 0.0f, true, true, false, true, 2, true);
            AttachEntityToEntity(xPed5, xObj3, 28422, -0.55f, -1.0f, -0.81f, 0.0f, 0.0f, 0.0f, true, true, false, true, 2, true);
            AttachEntityToEntity(xPed6, xObj3, 28422, 0.55f, -1.0f, -0.81f, 0.0f, 0.0f, 0.0f, true, true, false, true, 2, true);

            if (true == Game.PlayerPed.IsDead)
            {
                TaskGoToCoordAnyMeans(xPedMainLeader, playerPosse.X, playerPosse.Y, playerPosse.Z, 0.1f, 0, true, 1, 0.5f);
            }
            TaskPlayAnim(xPed1, danceBase, danceName, 8.0f, 0.0f, -1, 1, 0, false, false, false);
            TaskPlayAnim(xPed2, danceBase, danceName, 8.0f, 0.0f, -1, 1, 0, false, false, false);
            TaskPlayAnim(xPed3, danceBase, danceName, 8.0f, 0.0f, -1, 1, 0, false, false, false);
            TaskPlayAnim(xPed4, danceBase, danceName, 8.0f, 0.0f, -1, 1, 0, false, false, false);
            TaskPlayAnim(xPed5, danceBase, danceName, 8.0f, 0.0f, -1, 1, 0, false, false, false);
            TaskPlayAnim(xPed6, danceBase, danceName, 8.0f, 0.0f, -1, 1, 0, false, false, false);

            int playerPed = PlayerId();
            int playerGroup = GetPlayerGroup(playerPed);
            SetPedAsGroupLeader(playerPed, playerGroup);
            SetPedAsGroupMember(xPedMainLeader, playerGroup);
        }
    }
}
