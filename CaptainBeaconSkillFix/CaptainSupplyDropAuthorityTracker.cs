using RoR2;
using UnityEngine;

namespace CaptainBeaconSkillFix
{
    public class CaptainSupplyDropAuthorityTracker : MonoBehaviour
    {
        public CaptainSupplyDropController SupplyDropController;

        bool _lastHasAuthority;

        void Start()
        {
#pragma warning disable Publicizer001 // Accessing a member that was not originally public
            _lastHasAuthority = SupplyDropController.hasEffectiveAuthority;
#pragma warning restore Publicizer001 // Accessing a member that was not originally public
        }

        void FixedUpdate()
        {
            if (!SupplyDropController || !SupplyDropController.enabled)
                return;

#pragma warning disable Publicizer001 // Accessing a member that was not originally public
            bool currentHasAuthority = SupplyDropController.hasEffectiveAuthority;
#pragma warning restore Publicizer001 // Accessing a member that was not originally public

            if (currentHasAuthority != _lastHasAuthority)
            {
#if DEBUG
                Log.Debug($"Effective authority changed: {_lastHasAuthority}->{currentHasAuthority}, refreshing supply drop controller");
#endif

                SupplyDropController.enabled = false;
                SupplyDropController.enabled = true;

                _lastHasAuthority = currentHasAuthority;
            }
        }
    }
}
