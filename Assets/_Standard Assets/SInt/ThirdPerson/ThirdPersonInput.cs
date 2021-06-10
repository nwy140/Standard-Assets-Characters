using System;
using Rewired;
using StandardAssets.Characters.Common;
using UnityEngine;


namespace StandardAssets.Characters.ThirdPerson
{
    /// <summary>
    /// Implementation of the Third Person input
    /// </summary>
    public class ThirdPersonInput : CharacterInput, IThirdPersonInput
    {
        /// <summary>
        /// Fired when strafe input is started
        /// </summary>
        public event Action strafeStarted;

        /// <summary>
        /// Fired when the strafe input is ended
        /// </summary>
        public event Action strafeEnded;

        /// <summary>
        /// Fired when the recentre camera input is applied
        /// </summary>
        public event Action recentreCamera;

        // Tracks if the character is strafing 
        bool m_IsStrafing;

        // Handles the recentre input 
        public override void OnRecentre()
        {
            if (recentreCamera != null)
            {
                recentreCamera();
            }
        }

        // Handles the strafe input
        public override void OnStrafe(InputActionEventData data)
        {
            {
                BroadcastInputAction(ref m_IsStrafing, strafeStarted, strafeEnded);
            }
        }
    }
}