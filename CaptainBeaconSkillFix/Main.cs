using BepInEx;
using RoR2;
using System.Diagnostics;

namespace CaptainBeaconSkillFix
{
    [BepInPlugin(PluginGUID, PluginName, PluginVersion)]
    public class Main : BaseUnityPlugin
    {
        public const string PluginGUID = PluginAuthor + "." + PluginName;
        public const string PluginAuthor = "Gorakh";
        public const string PluginName = "CaptainBeaconSkillFix";
        public const string PluginVersion = "1.0.0";

        void Awake()
        {
            Stopwatch stopwatch = Stopwatch.StartNew();

            Log.Init(Logger);

            On.RoR2.CaptainSupplyDropController.Awake += CaptainSupplyDropController_Awake;

            stopwatch.Stop();
            Log.Info_NoCallerPrefix($"Initialized in {stopwatch.Elapsed.TotalSeconds:F2} seconds");
        }

        void OnDestroy()
        {
            On.RoR2.CaptainSupplyDropController.Awake -= CaptainSupplyDropController_Awake;
        }

        static void CaptainSupplyDropController_Awake(On.RoR2.CaptainSupplyDropController.orig_Awake orig, CaptainSupplyDropController self)
        {
            orig(self);

            CaptainSupplyDropAuthorityTracker authorityTracker = self.gameObject.AddComponent<CaptainSupplyDropAuthorityTracker>();
            authorityTracker.SupplyDropController = self;
        }
    }
}
