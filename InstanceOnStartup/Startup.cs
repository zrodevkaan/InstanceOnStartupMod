using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using ABI_RC.Core.InteractionSystem;
using ABI_RC.Core.Networking.IO.Instancing;
using ABI_RC.Core.Player;
using Harmony;
using MelonLoader;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using UnityEngine;

[assembly: MelonInfo(typeof(InstanceOnStartup.Startup), "InstanceOnStartup", "1.0", "Divinity")]

namespace InstanceOnStartup
{
    public class Startup : MelonMod
    {

        public override void OnApplicationStart()
        {
            MelonLogger.Msg("Mod Started");
            //
            // PlayerStartedLocalInstance();

            HarmonyInstance.Patch(AccessTools.Constructor(typeof(PlayerSetup)), null,
                new HarmonyLib.HarmonyMethod(typeof(Startup).GetMethod(nameof(PlayerStartedLocalInstance),
                    BindingFlags.NonPublic | BindingFlags.Static)));
        }
        private static void PlayerStartedLocalInstance()
        {
            MelonCoroutines.Start(RunMe());

            IEnumerator RunMe()
            {

                yield return new WaitForSeconds(10f);

                Instances.RequestNew("423f3612-3938-474c-a3be-600ac5510109", "Friends", "US", "SFW");

                yield return new WaitForSeconds(2f);

                Instances.SetJoinTarget(Instances.RequestedInstance, "423f3612-3938-474c-a3be-600ac5510109");

                MelonLogger.Msg("Instance Created");


            }

        }

    }
}